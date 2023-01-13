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

// TODO 0.1 Analysis

public sealed class ResearchMethodHandler : IResearchMethodHandler
{
    private readonly ICalculatedResearchPositions _researchPositions;
    private readonly IPointsInPartsCounting _pointsInPartsCounting;
    private readonly IFilePersistencyHandler _filePersistencyHandler;
    private readonly IResearchDataHandler _researchDataHandler;
    private readonly IResearchPaths _researchPaths;


    public ResearchMethodHandler(ICalculatedResearchPositions researchPositions,
        IPointsInPartsCounting pointsInZodiacPartsCounting,
        IFilePersistencyHandler filePersistencyHandler,
        IResearchDataHandler researchDataHandler,
        IResearchPaths researchPaths)
    {
        _researchPositions = researchPositions;
        _pointsInPartsCounting = pointsInZodiacPartsCounting;
        _filePersistencyHandler = filePersistencyHandler;
        _researchDataHandler = researchDataHandler;
        _researchPaths = researchPaths;
    }

    public CountOfPartsResponse HandleTestMethod(GeneralCountRequest request)
    {

        ResearchMethods method = request.Method;
        Log.Information("ResearchMethodHandler HandleTestMethod, using method {m} for project {p}", method, request.ProjectName);
        string fullPath = _researchPaths.DataPath(request.ProjectName, request.UseControlGroup);
        Log.Information("Reading Json from path : {fp}.", fullPath);
        string json = _filePersistencyHandler.ReadFile(fullPath);
        StandardInput standardInput = _researchDataHandler.GetStandardInputFromJson(json);
        List<CalculatedResearchChart> allCalculatedResearchCharts = _researchPositions.CalculatePositions(standardInput);
        Log.Information("Calculation completed.");

        string jsonText = JsonConvert.SerializeObject(allCalculatedResearchCharts, Formatting.Indented);
        string pathForResults = _researchPaths.ResultPath(request.ProjectName, method.ToString(), request.UseControlGroup);
        _filePersistencyHandler.WriteFile(pathForResults, jsonText);
        Log.Information("Json with positions written to {path}.", pathForResults);

        switch (method)
        {
            case ResearchMethods.CountPosInSigns: return _pointsInPartsCounting.CountPointsInParts(allCalculatedResearchCharts, request);
            case ResearchMethods.CountPosInHouses: return _pointsInPartsCounting.CountPointsInParts(allCalculatedResearchCharts, request);
            //                 case ResearchMethods.CountAspects: return TestAspects(request);
            //                  case ResearchMethods.CountUnaspected: return TestUnaspected(request);
            /*           case ResearchMethods.CountOccupiedMidpoints: return TestOccupiedMidpoints(request);
                       case ResearchMethods.CountHarmonicConjunctions: return TestHarmonicConjunctions(request);
             */
            default:
                string errorTxt = "An error occurred while using a testmethod in ResearchMethodHandler.HandleTestMethod(). An unrecognized ResearchMethod was encountered.";
                Log.Error(errorTxt);
                throw new Exception(errorTxt);   // TODO create specific exception.
        }

    }


}

