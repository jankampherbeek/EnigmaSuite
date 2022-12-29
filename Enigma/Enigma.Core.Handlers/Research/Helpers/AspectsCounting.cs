// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Core.Work.Analysis.Interfaces;
using Enigma.Core.Work.Research.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Configuration;
using Enigma.Domain.Enums;
using Enigma.Domain.RequestResponse.Research;
using Enigma.Domain.Research;
using Enigma.Research.Domain;
using Newtonsoft.Json;
using Serilog;
using static Enigma.Core.Work.Analysis.Interfaces.IAspectChecker;

namespace Enigma.Core.Handlers.Research.Helpers;


public class AspectsCounting
{
    private readonly IResearchPaths _researchPaths;
    private readonly IFilePersistencyHandler _filePersistencyHandler;
    private readonly IResearchDataHandler _researchDataHandler;
    private readonly IAspectsHandler _aspectsHandler;
    private readonly IAspectOrbConstructor _orbConstructor;

    public AspectsCounting(IResearchPaths researchPaths,
        IFilePersistencyHandler filePersistencyHandler,
        IResearchDataHandler researchDataHandler,
        IAspectsHandler aspectsHandler,
        IAspectOrbConstructor orbConstructor)
    {
        _researchPaths = researchPaths;
        _filePersistencyHandler = filePersistencyHandler;
        _researchDataHandler = researchDataHandler;
        _aspectsHandler = aspectsHandler;
        _orbConstructor = orbConstructor;
    }

    public CountAspectsResponse CountAspects(List<CalculatedResearchChart> charts, CountAspectsRequest request)
    {
        return PerformCounts(charts, request);
    }

    private CountAspectsResponse PerformCounts(List<CalculatedResearchChart> charts, CountAspectsRequest request)
    {
        ResearchMethods researchMethod = request.Method;
        List<AspectDetails> aspectDetails = new();
        for (int i = 0; i < request.Config.Aspects.Count; i++)
        {
            aspectDetails.Add(request.Config.Aspects[i].AspectType.GetDetails());
        }

        for (int i = 0; i < charts.Count; i++)
        {
            List<FullCelPointPos> fullCelPointPositions = new();
            for (int j = 0; j < request.PointsSelection.SelectedCelPoints.Count; j++)
            {
                CelPoints point = request.PointsSelection.SelectedCelPoints[j];
                for (int k = 0; k < charts[j].CelPointPositions.Count; k++)
                {
                    if (charts[i].CelPointPositions[k].CelPoint == point)
                    {
                        fullCelPointPositions.Add(charts[i].CelPointPositions[k]);
                    }
                }

            }
            List<EffectiveAspect> effectiveAspects = _aspectsHandler.AspectsForCelPoints(aspectDetails, fullCelPointPositions);

            // TODO add effectiveAspects to allCounts (refactor allCounts)
        }
        


        



            ////////////////

            List<AspectSpecs> aspects = new();
        int counter = 0;
        for (int i = 0; i < request.Config.Aspects.Count; i++)
        {
            if (request.Config.Aspects[i].IsUsed)
            {
                aspects[counter++] = request.Config.Aspects[i];
            }
        }
        List<ResearchPoint> selectedResearchPoints = InitializeCounts(request);
        int[,] allCounts = new int[selectedResearchPoints.Count, selectedResearchPoints.Count];


        foreach (CalculatedResearchChart chart in charts)
        {
            HandleChart(researchMethod, chart, selectedResearchPoints, aspects, ref allCounts);
        }



        /*

                List<int> totals = CountTotals(allCounts);
                CountOfAspectsResponse response = new(request, allCounts, totals);

                string jsonText = JsonConvert.SerializeObject(response, Formatting.Indented);
                string pathForResults = _researchPaths.CountResultsPath(request.ProjectName, researchMethod.ToString(), request.UseControlGroup);
                _filePersistencyHandler.WriteFile(pathForResults, jsonText);
                Log.Information("Json with countings for aspects written to {path}.", pathForResults);
                return response;

                */

        return null;
    }


    private static List<ResearchPoint> InitializeCounts(CountAspectsRequest request)
    {
        List<ResearchPoint> researchPoints = new();
        foreach (CelPoints selectedCelPoint in request.PointsSelection.SelectedCelPoints)
        {
            int Id = selectedCelPoint.GetDetails().SeId;
            researchPoints.Add(new ResearchCelPoint(Id, selectedCelPoint));
        }
        foreach (MundanePoints selectedMundanePoint in request.PointsSelection.SelectedMundanePoints)
        {
            int Id = (int)selectedMundanePoint;
            researchPoints.Add(new ResearchMundanePoint(Id, selectedMundanePoint));
        }
        if (request.PointsSelection.IncludeCusps)
        {
            int nrOfCusps = request.Config.HouseSystem.GetDetails().NrOfCusps;
            for (int i = 0; i < nrOfCusps; i++)
            {
                int index = i + 1;
                string name = "Cusp " + index;
                researchPoints.Add(new ResearchCuspPoint(index, name));
            }
        }
        return researchPoints;
    }


    private void HandleChart(ResearchMethods researchMethod, CalculatedResearchChart chart, List<ResearchPoint> selectedResearchPoints, List<AspectSpecs> aspects, ref int[,] allCounts)
    {
        List<PositionedResearchPoint> posResearchPoints = CreatePosResearchPoints(chart, selectedResearchPoints);
        for (int i = 0; i < posResearchPoints.Count; i++)
        {
            PositionedResearchPoint point1 = posResearchPoints[i];
            for (int j = i + 1; j < posResearchPoints.Count; j++)
            {
                PositionedResearchPoint point2 = posResearchPoints[j];
                double distance = point1.Position - point2.Position;
                if (distance < 0.0) distance += 360.0;
                if (distance > 180.0) distance = 360 - distance;

                /*            for (int k = 0; k < aspects.Count; k++)
                            {
                                AspectDetails aspectToCheck = aspects[k].AspectType.GetDetails();
                                double angle = aspectToCheck.Angle;
                                double maxOrb = _orbConstructor.DefineOrb(point1.Point.   celPointPos1.CelPoint, celPointPos2.CelPoint, aspectToCheck);
                                double actualOrb = Math.Abs(angle - distance);
                                if (actualOrb < maxOrb)
                                {
                                    effectiveAspects.Add(new EffectiveAspect(celPointPos1.CelPoint, celPointPos2.CelPoint, aspectToCheck, maxOrb, actualOrb));
                                }
                            }
                    */

            }

        }


      
    }


    private static List<PositionedResearchPoint> CreatePosResearchPoints(CalculatedResearchChart chart, List<ResearchPoint> selectedResearchPoints)
    {
        List<PositionedResearchPoint> posResearchPoints = new();
        for (int i = 0; i < selectedResearchPoints.Count; i++)
        {
            double longitude = -1.0;
            if (selectedResearchPoints[i] is ResearchCelPoint)
            {
                for (int j = 0; j < chart.CelPointPositions.Count; j++)
                {
                    if (chart.CelPointPositions[j].CelPoint == ((ResearchCelPoint)selectedResearchPoints[j]).CelPoint)
                    {
                        longitude = chart.CelPointPositions[j].Longitude.Position;
                    }
                }
            }
            else if (selectedResearchPoints[i] is ResearchMundanePoint point)
            {
                switch (point.MundanePoint)
                {
                    case MundanePoints.Ascendant:
                        longitude = chart.FullHousePositions.Ascendant.Longitude;
                        break;
                    case MundanePoints.Mc:
                        longitude = chart.FullHousePositions.Mc.Longitude;
                        break;
                    case MundanePoints.EastPoint:
                        longitude = chart.FullHousePositions.EastPoint.Longitude;
                        break;
                    case MundanePoints.Vertex:
                        longitude = chart.FullHousePositions.Vertex.Longitude;
                        break;
                    default:
                        string errorString = "AspectsCounting.CreatePosResearchPoints(): encountered unknown MundanePoint: " +  point.MundanePoint.ToString();
                        Log.Error(errorString);
                        throw new ArgumentException(errorString);
                }
            }
            else if (selectedResearchPoints[i] is ResearchCuspPoint)
            {
                ResearchCuspPoint cuspPoint = (ResearchCuspPoint)selectedResearchPoints[i];
                longitude = chart.FullHousePositions.Cusps[cuspPoint.Id].Longitude;
            }
            if (longitude >= 0.0)
            {
                posResearchPoints.Add(new PositionedResearchPoint(selectedResearchPoints[i], longitude));
            }
        }
        return posResearchPoints;
    }

}