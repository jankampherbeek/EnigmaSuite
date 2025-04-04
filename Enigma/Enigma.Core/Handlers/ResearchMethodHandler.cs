﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Research;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Persistables;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Newtonsoft.Json;
using Serilog;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Enigma.Core.Handlers;

/// <summary>Handlers for performing research methods.</summary>
public interface IResearchMethodHandler
{
    /// <summary>Start running a test.</summary>
    /// <param name="request">Instance of GeneralResearchRequest or one of its children.</param>
    /// <returns>Results of the test as instance of MethodResponse or one of its children.</returns>
    public MethodResponse HandleResearch(GeneralResearchRequest request);

}

/// <inheritdoc/>
public sealed class ResearchMethodHandler : IResearchMethodHandler
{
    private readonly ICalculatedResearchPositions _researchPositions;
    private readonly IPointsInPartsCounting _pointsInPartsCounting;
    private readonly IFilePersistencyHandler _filePersistencyHandler;
    private readonly IResearchDataHandler _researchDataHandler;
    private readonly IResearchPaths _researchPaths;
    private readonly IAspectsCounting _aspectsCounting;
    private readonly IUnaspectedCounting _unaspectedCounting;
    private readonly IOccupiedMidpointsCounting _occupiedMidpointsCounting;
    private readonly IHarmonicConjunctionsCounting _harmonicConjunctionsCounting;
    private readonly IOobCounting _oobCounting;
    private readonly IOccupiedMidpointsDeclinationCounting _occupiedMidpointsDeclinationCounting;
    private readonly IDeclinationParallelsCounting _declinationParallelsCounting;


    public ResearchMethodHandler(ICalculatedResearchPositions researchPositions,
        IPointsInPartsCounting pointsInZodiacPartsCounting,
        IFilePersistencyHandler filePersistencyHandler,
        IResearchDataHandler researchDataHandler,
        IResearchPaths researchPaths,
        IAspectsCounting aspectsCounting,
        IUnaspectedCounting unaspectedCounting,
        IOccupiedMidpointsCounting occupiedMidpointsCounting,
        IHarmonicConjunctionsCounting harmonicConjunctionsCounting,
        IOobCounting oobCounting,
        IOccupiedMidpointsDeclinationCounting occupiedMidpointsDeclinationCounting,
        IDeclinationParallelsCounting declinationParallelsCounting)
    {
        _researchPositions = researchPositions;
        _pointsInPartsCounting = pointsInZodiacPartsCounting;
        _filePersistencyHandler = filePersistencyHandler;
        _researchDataHandler = researchDataHandler;
        _researchPaths = researchPaths;
        _aspectsCounting = aspectsCounting;
        _unaspectedCounting = unaspectedCounting;
        _occupiedMidpointsCounting = occupiedMidpointsCounting;
        _harmonicConjunctionsCounting = harmonicConjunctionsCounting;
        _oobCounting = oobCounting;
        _occupiedMidpointsDeclinationCounting = occupiedMidpointsDeclinationCounting;
        _declinationParallelsCounting = declinationParallelsCounting;
    }

    /// <inheritdoc/>
    public MethodResponse HandleResearch(GeneralResearchRequest request)
    {
        ResearchMethods method = request.Method;
        Log.Information("ResearchMethodHandler HandleResearch, using method {M} for project {P}", method, request.ProjectName);
        List<CalculatedResearchChart> allCalculatedResearchCharts = CalculateAllCharts(request.ProjectName, request.UseControlGroup);
        WriteCalculatedChartsToJson(request.ProjectName, method.ToString(), request.UseControlGroup, allCalculatedResearchCharts);
        switch (request)
        {
            case CountHarmonicConjunctionsRequest conjunctionsRequest:
                return _harmonicConjunctionsCounting.CountHarmonicConjunctions(allCalculatedResearchCharts, conjunctionsRequest); 
            case CountOccupiedMidpointsRequest midpointsRequest:
                return _occupiedMidpointsCounting.CountMidpoints(allCalculatedResearchCharts, midpointsRequest);
            case CountOccupiedMidpointsDeclinationRequest midpointsDeclinationRequest:
                return _occupiedMidpointsDeclinationCounting.CountMidpointsInDeclination(allCalculatedResearchCharts, midpointsDeclinationRequest );
                
        }
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (method)
        {
            case ResearchMethods.CountUnaspected:
                return _unaspectedCounting.CountUnaspected(allCalculatedResearchCharts, request);
            case ResearchMethods.CountAspects:
                return _aspectsCounting.CountAspects(allCalculatedResearchCharts, request);
            case ResearchMethods.CountPosInSigns:
                return _pointsInPartsCounting.CountPointsInParts(allCalculatedResearchCharts, request);
            case ResearchMethods.CountPosInHouses:
                return _pointsInPartsCounting.CountPointsInParts(allCalculatedResearchCharts, request);
            case ResearchMethods.CountOob:
                return _oobCounting.CountOob(allCalculatedResearchCharts, request);
            case ResearchMethods.CountDeclinationParallels:
                return _declinationParallelsCounting.CountParallels(allCalculatedResearchCharts, request);
            default:
                Log.Error("ResearchMethodHandler.HandleResearch() received an unrecognized request : {Request}", request);
                throw new EnigmaException("Unrecognized ResearchMethod in request for ResearchMethodHandler");
        }
    }

    private List<CalculatedResearchChart> CalculateAllCharts(string projectName, bool controlGroup)
    {
        List<CalculatedResearchChart> allCalculatedResearchCharts = new();
        string fullPath = _researchPaths.DataPath(projectName, controlGroup);
        Log.Information("Reading Json from path : {Fp}", fullPath);
        string json = _filePersistencyHandler.ReadFile(fullPath);
        if (json != "")
        {
            StandardInput standardInput = _researchDataHandler.GetStandardInputFromJson(json);
            allCalculatedResearchCharts = _researchPositions.CalculatePositions(standardInput);
            Log.Information("Calculation completed");            
        }
        else
        {
            Log.Error("Could not find data for project {ProjName}", projectName);
        }
        return allCalculatedResearchCharts;
    }

    private void WriteCalculatedChartsToJson(string projectName, string methodName, bool controlGroup,
        List<CalculatedResearchChart> allCharts)
    {
        string pathForResults = _researchPaths.ResultPath(projectName, methodName, controlGroup);
        using (var stream = new FileStream(pathForResults, FileMode.Create))
        using (var writer = new StreamWriter(stream))
        using (var jsonWriter = new JsonTextWriter(writer))
        {
            jsonWriter.Formatting = Formatting.Indented;
            var serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, allCharts);
        }

        Log.Information("Json with positions written to {Path}", pathForResults);
    }
}

