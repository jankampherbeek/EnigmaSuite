// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Persistency;
using Enigma.Domain.Research;
using Enigma.Research.Domain;
using Newtonsoft.Json;
using Serilog;


namespace Enigma.Core.Handlers.Research;

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
        Log.Information("ResearchMethodHandler HandleResearch, using method {m} for project {p}", method, request.ProjectName);
        List<CalculatedResearchChart> allCalculatedResearchCharts = CalculateAllCharts(request.ProjectName, request.UseControlGroup);
        WriteCalculatedChartsToJson(request.ProjectName, method.ToString(), request.UseControlGroup, allCalculatedResearchCharts);
        if (request is CountHarmonicConjunctionsRequest)
        {
            CountHarmonicConjunctionsRequest? qualifiedRequest = request as CountHarmonicConjunctionsRequest;
            if (qualifiedRequest != null)
            {
                return _harmonicConjunctionsCounting.CountHarmonicConjunctions(allCalculatedResearchCharts, qualifiedRequest);
            }
            else
            {
                string errorText = "ResearchMethodHandler.HandleResearch() contains woring request for CountHarmonicConjunctions : " + request;
                Log.Error(errorText);
                throw new EnigmaException(errorText);
            }
        }
        else if (request is CountOccupiedMidpointsRequest)
        {
            CountOccupiedMidpointsRequest? qualifiedRequest = request as CountOccupiedMidpointsRequest;
            if (qualifiedRequest != null)
            {
                return _occupiedMidpointsCounting.CountMidpoints(allCalculatedResearchCharts, qualifiedRequest);
            }
            else
            {
                string errorText = "ResearchMethodHandler.HandleResearch() contains woring request for CountOccupiedMIdpoints : " + request;
                Log.Error(errorText);
                throw new EnigmaException(errorText);
            }
        }
        else if (request != null && method == ResearchMethods.CountUnaspected)
        {
            return _unaspectedCounting.CountUnaspected(allCalculatedResearchCharts, request);
        }
        else if (request != null && method == ResearchMethods.CountAspects)
        {
            return _aspectsCounting.CountAspects(allCalculatedResearchCharts, request);
        }
        else if (request != null && method == ResearchMethods.CountPosInSigns)
        {
            return _pointsInPartsCounting.CountPointsInParts(allCalculatedResearchCharts, request);
        }
        else if (request != null && method == ResearchMethods.CountPosInHouses)
        {
            return _pointsInPartsCounting.CountPointsInParts(allCalculatedResearchCharts, request);
        }
        string errorTxt = "ResearchMethodHandler.HandleResearch() received an unrecognized request : " + request;
        Log.Error(errorTxt);
        throw new EnigmaException(errorTxt);
    }

    private List<CalculatedResearchChart> CalculateAllCharts(string projectName, bool controlGroup)
    {
        string fullPath = _researchPaths.DataPath(projectName, controlGroup);
        Log.Information("Reading Json from path : {fp}.", fullPath);
        string json = _filePersistencyHandler.ReadFile(fullPath);
        StandardInput standardInput = _researchDataHandler.GetStandardInputFromJson(json);
        List<CalculatedResearchChart> allCalculatedResearchCharts = _researchPositions.CalculatePositions(standardInput);
        Log.Information("Calculation completed.");
        return allCalculatedResearchCharts;
    }

    private void WriteCalculatedChartsToJson(string projectName, string methodName, bool controlGroup, List<CalculatedResearchChart> allCharts)
    {
        string jsonText = JsonConvert.SerializeObject(allCharts, Formatting.Indented);
        string pathForResults = _researchPaths.ResultPath(projectName, methodName, controlGroup);
        _filePersistencyHandler.WriteFile(pathForResults, jsonText);
        Log.Information("Json with positions written to {path}.", pathForResults);
    }



}

