// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using System.Text.Json;
using Enigma.Core.Interfaces;
using Enigma.Core.Research.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Persistables;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Core.Handlers;

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


    public ResearchMethodHandler(ICalculatedResearchPositions researchPositions,
        IPointsInPartsCounting pointsInZodiacPartsCounting,
        IFilePersistencyHandler filePersistencyHandler,
        IResearchDataHandler researchDataHandler,
        IResearchPaths researchPaths,
        IAspectsCounting aspectsCounting,
        IUnaspectedCounting unaspectedCounting,
        IOccupiedMidpointsCounting occupiedMidpointsCounting,
        IHarmonicConjunctionsCounting harmonicConjunctionsCounting)
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
            {
                    return _harmonicConjunctionsCounting.CountHarmonicConjunctions(allCalculatedResearchCharts, conjunctionsRequest); 
            }
            case CountOccupiedMidpointsRequest midpointsRequest:
            {
                    return _occupiedMidpointsCounting.CountMidpoints(allCalculatedResearchCharts, midpointsRequest);
            }
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
            default:
                Log.Error("ResearchMethodHandler.HandleResearch() received an unrecognized request : {Request}", request);
                throw new EnigmaException("Unrecognized ResearchMethod in request for ResearchMethodHandler");
        }
    }

    private List<CalculatedResearchChart> CalculateAllCharts(string projectName, bool controlGroup)
    {
        string fullPath = _researchPaths.DataPath(projectName, controlGroup);
        Log.Information("Reading Json from path : {Fp}", fullPath);
        string json = _filePersistencyHandler.ReadFile(fullPath);
        StandardInput standardInput = _researchDataHandler.GetStandardInputFromJson(json);
        List<CalculatedResearchChart> allCalculatedResearchCharts = _researchPositions.CalculatePositions(standardInput);
        Log.Information("Calculation completed");
        return allCalculatedResearchCharts;
    }

    private void WriteCalculatedChartsToJson(string projectName, string methodName, bool controlGroup, List<CalculatedResearchChart> allCharts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonText = JsonSerializer.Serialize(allCharts, options);
        string pathForResults = _researchPaths.ResultPath(projectName, methodName, controlGroup);
        _filePersistencyHandler.WriteFile(pathForResults, jsonText);
        Log.Information("Json with positions written to {Path}", pathForResults);
    }



}

