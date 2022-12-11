// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Work.Research.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Configuration;
using Enigma.Domain.Enums;
using Enigma.Domain.Persistency;
using Enigma.Domain.RequestResponse;
using Enigma.Domain.Research;
using Enigma.Research.Domain;
using Newtonsoft.Json;
using Serilog;

namespace Enigma.Core.Handlers.Research;

public sealed class TestMethodHandler : ITestMethodHandler
{
    private IFilePersistencyHandler _filePersistencyHandler;
    private IResearchDataHandler _researchDataHandler;
    private IConfigurationHandler _configurationHandler;
    private IChartAllPositionsHandler _chartAllPositionsHandler;
    private IJulDayHandler _julDayHandler;
    private IResearchPaths _researchPaths;


    public TestMethodHandler(IFilePersistencyHandler filePersistencyHandler, 
        IResearchDataHandler researchDataHandler, 
        IConfigurationHandler configurationHandler,
        IChartAllPositionsHandler chartAllPositionsHandler,
        IJulDayHandler julDayHandler,
        IResearchPaths researchPaths)
    {
        _filePersistencyHandler = filePersistencyHandler;
        _researchDataHandler = researchDataHandler;
        _configurationHandler = configurationHandler;
        _chartAllPositionsHandler = chartAllPositionsHandler;
        _julDayHandler = julDayHandler;
        _researchPaths = researchPaths;
    }

    public MethodResponse HandleTestMethod(TestMethodRequest request)
    {

        ResearchMethods method = request.Method;
        Log.Information("TestMethodHandler HandleTestMethod, using method {m} for project {p}", method, request.ProjectName);
        string fullPath = _researchPaths.DataPath(request.ProjectName, request.UseControlGroup);
        Log.Information("Reading Json from path : {fp}.", fullPath);
        string json = _filePersistencyHandler.ReadFile(fullPath);
        StandardInput standardInput = _researchDataHandler.GetStandardInputFromJson(json);

        Log.Information("Start of calculation.");
        List<CalculatedResearchChart> allCalculatedResearchCharts = Calculate(standardInput);
        Log.Information("Calculation completed.");

        string jsonText = JsonConvert.SerializeObject(allCalculatedResearchCharts, Formatting.Indented);
        string pathForResults = _researchPaths.ResultPath(request.ProjectName, method.ToString(), request.UseControlGroup);
        _filePersistencyHandler.WriteFile(pathForResults, jsonText);
        Log.Information("Json with positions written to {path}.", pathForResults);

        switch (method)
        {
            case ResearchMethods.CountPosInSigns: return TestPointsInSign(allCalculatedResearchCharts, request);
            case ResearchMethods.CountPosInHouses: return TestPointsInHouses(request);
            case ResearchMethods.CountAspects: return TestAspects(request);
            case ResearchMethods.CountUnaspected: return TestUnaspected(request);
            case ResearchMethods.CountOccupiedMidpoints: return TestOccupiedMidpoints(request);
            case ResearchMethods.CountHarmonicConjunctions: return TestHarmonicConjunctions(request);
            default:
                string errorTxt = "An error occurred while using a testmethod in TestMethodHandler.HandleTestMethod(). An unrecognized ResearchMethod was encountered.";
                Log.Error(errorTxt);
                throw new Exception(errorTxt);   // TODO create specific exception.
        }
 
    }


    private List<CalculatedResearchChart> Calculate(StandardInput standardInput)  // TODO extract class
    {
        AstroConfig config = _configurationHandler.ReadConfig();
        List<CalculatedResearchChart> calculatedCharts = new();
        List<CelPointSpecs> cpSpecs = config.CelPoints;
        List<CelPoints> celPoints = new();
        foreach (CelPointSpecs cpSpec in cpSpecs)
        {
            celPoints.Add(cpSpec.CelPoint);
        }
        CalculationPreferences calcPref = new(celPoints, config.ZodiacType, config.Ayanamsha, CoordinateSystems.Ecliptical, config.ObserverPosition, config.ProjectionType, config.HouseSystem);
        
        foreach (StandardInputItem inputItem in standardInput.ChartData)
        {
            Location location = new("", inputItem.GeoLongitude, inputItem.GeoLatitude);
            double jdUt = CalcJdUt(inputItem);
            CelPointsRequest cpRequest = new(jdUt, location, calcPref);
            ChartAllPositionsRequest chartAllPosRequest = new(cpRequest, config.HouseSystem);
            ChartAllPositionsResponse response = _chartAllPositionsHandler.CalcFullChart(chartAllPosRequest);
            // TODO check for null for MundanePositions.

            calculatedCharts.Add(new CalculatedResearchChart(response.CelPointPositions, response.MundanePositions, inputItem));
        }
        return calculatedCharts;
    }

    private double CalcJdUt(StandardInputItem inputItem)
    {
        double ut = inputItem.Time.Hour + inputItem.Time.Minute * 60.0 + inputItem.Time.Second * 3600.0 + inputItem.Time.Dst + inputItem.Time.ZoneOffset;
        // TODO check for overflow
        Calendars cal = inputItem.Date.Calendar == "G" ? Calendars.Gregorian : Calendars.Julian;
        SimpleDateTime simpleDateTime = new(inputItem.Date.Year, inputItem.Date.Month, inputItem.Date.Day, ut, cal);
        JulianDayRequest jdRequest = new(simpleDateTime);
        return _julDayHandler.CalcJulDay(jdRequest).JulDayUt;
    }


    private MethodResponse TestPointsInSign(List<CalculatedResearchChart> charts, TestMethodRequest request)
    {
        int NrOfCelPoints = request.PointsSelection.SelectedCelPoints.Count;
        int[] tempCounts = new int[NrOfCelPoints];
        List<int> counts = tempCounts.ToList();


        // Initialize counts
        List<ResearchPointCounts> allCounts = new();
        foreach (CelPoints selectedCelPoint in request.PointsSelection.SelectedCelPoints)
        {
            int Id = selectedCelPoint.GetDetails().SeId;
            ResearchPoint researchPoint = new ResearchCelPoint(Id, selectedCelPoint);
            ResearchPointCounts researchPointCounts = new(researchPoint, tempCounts.ToList());
            allCounts.Add(researchPointCounts);
        }


        string fullPath = _researchPaths.DataPath(request.ProjectName, request.UseControlGroup);
        string json = _filePersistencyHandler.ReadFile(fullPath);
        StandardInput standardInput = _researchDataHandler.GetStandardInputFromJson(json);
        AstroConfig config = _configurationHandler.ReadConfig();
        ResearchPointsSelection PointsSelection = request.PointsSelection;

        foreach (CalculatedResearchChart chart in charts)
        {
            if (PointsSelection.SelectedCelPoints.Count > 0)
            {
                int cpIndex = 0;
                foreach (CelPoints selectedCelPoint in PointsSelection.SelectedCelPoints)
                {
                    foreach (FullCelPointPos chartCelPointPos in chart.CelPointPositions)
                    {
                        if (chartCelPointPos.CelPoint == selectedCelPoint)
                        {
                            double longitude = chartCelPointPos.Longitude.Position;
                            int signIndex = (int)longitude / 30;   // signs counted from 0..11
                            allCounts[cpIndex].Counts[signIndex]++; 
                        }
                    }
                    cpIndex++;
                }
            }




        }




        if (PointsSelection.SelectedMundanePoints.Count > 0 || PointsSelection.IncludeCusps)
        {
            // calculate mundane points and/or cusps for standardinput, use only longitude
        }
        if (PointsSelection.SelectedZodiacalPoints.Count > 0)
        {
            // handle zodiacal points for standardinput
        }
        // count positions in signs
        // construct response

        return null;
    }

    private MethodResponse TestPointsInHouses(TestMethodRequest request)
    {
        return null;
    }

    private MethodResponse TestAspects(TestMethodRequest request)
    {
        return null;
    }

    private MethodResponse TestUnaspected(TestMethodRequest request)
    {
        return null;
    }

    private MethodResponse TestOccupiedMidpoints(TestMethodRequest request)
    {
        return null;
    }

    private MethodResponse TestHarmonicConjunctions(TestMethodRequest request)
    {
        return null;
    }




}

