// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
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


    public ResearchMethodHandler(ICalculatedResearchPositions researchPositions,
        IPointsInPartsCounting pointsInZodiacPartsCounting,
        IFilePersistencyHandler filePersistencyHandler,
        IResearchDataHandler researchDataHandler,
        IResearchPaths researchPaths,
        IAspectsCounting aspectsCounting,
        IUnaspectedCounting unaspectedCounting)
    {
        _researchPositions = researchPositions;
        _pointsInPartsCounting = pointsInZodiacPartsCounting;
        _filePersistencyHandler = filePersistencyHandler;
        _researchDataHandler = researchDataHandler;
        _researchPaths = researchPaths;
        _aspectsCounting = aspectsCounting;
        _unaspectedCounting = unaspectedCounting;
    }

    /// <inheritdoc/>
    public CountOfPartsResponse HandleTestForPartsMethod(GeneralResearchRequest request)
    {

        ResearchMethods method = request.Method;
        Log.Information("ResearchMethodHandler HandleTestForPartsMethod, using method {m} for project {p}", method, request.ProjectName);

        List<CalculatedResearchChart> allCalculatedResearchCharts = CalculateAllCharts(request.ProjectName, request.UseControlGroup);
        WriteCalculatedChartsToJson(request.ProjectName, method.ToString(), request.UseControlGroup, allCalculatedResearchCharts);

        switch (method)
        {
            case ResearchMethods.CountPosInSigns: return _pointsInPartsCounting.CountPointsInParts(allCalculatedResearchCharts, request);
            case ResearchMethods.CountPosInHouses: return _pointsInPartsCounting.CountPointsInParts(allCalculatedResearchCharts, request);
            default:
                string errorTxt = "An error occurred while using a testmethod in ResearchMethodHandler.HandleTestForPartsMethod(). An unsupported ResearchMethod was encountered.";
                Log.Error(errorTxt);
                throw new Exception(errorTxt);   // TODO create specific exception.
        }

    }


    /// <inheritdoc/>
    public CountOfUnaspectedResponse HandleTestForUnaspectedMethod(GeneralResearchRequest request)
    {
        ResearchMethods method = request.Method;
        Log.Information("ResearchMethodHandler HandleTestForUnaspectedMethod, using method {m} for project {p}", method, request.ProjectName);
        List<CalculatedResearchChart> allCalculatedResearchCharts = CalculateAllCharts(request.ProjectName, request.UseControlGroup);
        WriteCalculatedChartsToJson(request.ProjectName, method.ToString(), request.UseControlGroup, allCalculatedResearchCharts);
        return _unaspectedCounting.CountUnaspected(allCalculatedResearchCharts, request);
    }


    public CountOfOccupiedMidpointsResponse HandleTestForOccupiedMidpoints(CountMidpointsPerformRequest request)
    {
        ResearchMethods method = request.Method;
        Log.Information("ResearchMethodHandler HandleTestForOccupiedMidpointMethod, using method {m} for project {p}", method, request.ProjectName);
        List<CalculatedResearchChart> allCalculatedResearchCharts = CalculateAllCharts(request.ProjectName, request.UseControlGroup);
        WriteCalculatedChartsToJson(request.ProjectName, method.ToString(), request.UseControlGroup, allCalculatedResearchCharts);

        // todo call OccupiedMidpointsCounting

        return null;
    }



    /// <inheritdoc/>
    public CountOfAspectsResponse HandleTestForAspectsMethod(GeneralResearchRequest request)
    {
        ResearchMethods method = request.Method;
        Log.Information("ResearchMethodHandler HandleTestForAspectsMethod, using method {m} for project {p}", method, request.ProjectName);
        List<CalculatedResearchChart> allCalculatedResearchCharts = CalculateAllCharts(request.ProjectName, request.UseControlGroup);
        WriteCalculatedChartsToJson(request.ProjectName, method.ToString(), request.UseControlGroup, allCalculatedResearchCharts);
        return _aspectsCounting.CountAspects(allCalculatedResearchCharts, request);
    }


    private List<CalculatedResearchChart> CalculateAllCharts(string projectName, bool controlGroup) {
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

