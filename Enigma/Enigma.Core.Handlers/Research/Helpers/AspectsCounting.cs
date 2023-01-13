// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Points;
using Enigma.Domain.RequestResponse.Research;
using Enigma.Domain.Research;
using Enigma.Research.Domain;

namespace Enigma.Core.Handlers.Research.Helpers;

// TODO 0.1 Analysis

public sealed class AspectsCounting
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
        List<AspectTypes> aspectTypes = new();
        List<AspectsPerChart> aspectsPerChart = new();
        List<DefinedAspect> selectedDefinedAspects = new();


        List<ChartPoints> selectedCelPoints = request.PointsSelection.SelectedPoints;
        List<ChartPoints> selectedMundanePoints = request.PointsSelection.SelectedMundanePoints;

        List<ChartPoints> selectedPoints = new();
        /*      foreach (ChartPoints celPoint in selectedCelPoints)
              {
                  selectedPoints.Add(_pointMappings.GeneralPointForIndex((int)celPoint));
              }
              foreach (MundanePoints mundanePoint in selectedMundanePoints)
              {
                  selectedPoints.Add(_pointMappings.GeneralPointForIndex((int)mundanePoint + 3000));     // todo add offsets for general points to constants
              }
        */

        for (int i = 0; i < request.Config.Aspects.Count; i++)
        {
            //    aspectDetails.Add(request.Config.Aspects[i].AspectType.GetDetails());
            aspectTypes.Add(request.Config.Aspects[i].AspectType);
        }


        // todo posities rechtstreeks toekennen aan General point









        /*



                for (int i = 0; i < charts.Count; i++)
                {
                    List<FullChartPointPos> fullCelPointPositions = new();
                    for (int j = 0; j < selectedCelPoints.Count; j++)
                    {
                        ChartPoints point = selectedCelPoints[j];
                        for (int k = 0; k < charts[j].CelPointPositions.Count; k++)
                        {
                            if (charts[i].CelPointPositions[k].ChartPoint == point)
                            {
                                fullCelPointPositions.Add(charts[i].CelPointPositions[k]);
                            }
                        }
                    }



                    List<EffectiveAspect> effectiveAspects = _aspectsHandler.AspectsForChartPoints(aspectDetails, fullCelPointPositions);

                    List<AspectConfigSpecs> selectedAspects = request.Aspects;
                    foreach (EffectiveAspect effAspect in effectiveAspects)
                    {
                        foreach (AspectConfigSpecs aspectSpec in selectedAspects)
                        {
                            if (effAspect.EffAspectDetails.Aspect.GetDetails().Aspect == aspectSpec.AspectType.GetDetails().Aspect)
                            {
                                selectedDefinedAspects.Add(effAspect);
                            }
                        }
                    }
                    string chartId = charts[i].InputItem.Id;
                    aspectsPerChart.Add(new AspectsPerChart(chartId, selectedDefinedAspects));
                }

                List<int> selectedCusps = new();
                if (request.PointsSelection.IncludeCusps)
                {
                    int nrOfCusps = request.Config.HouseSystem.GetDetails().NrOfCusps;
                    for (int i = 0; i < nrOfCusps; i++)
                    {
                        selectedCusps.Add(0);
                    }
                }

                int[,] totals = DefineTotals(selectedCelPoints, selectedMundanePoints, selectedCusps, aspectTypes, selectedDefinedAspects);

                // todo define totals
                // todo fill aspectTypes

                AspectTotals aspectTotals = new(selectedCelPoints, selectedMundanePoints, selectedCusps, aspectTypes, totals);
                CountAspectsResponse response = new CountAspectsResponse(request, aspectsPerChart, aspectTotals);



                string jsonText = JsonConvert.SerializeObject(response, Formatting.Indented);
                string pathForResults = _researchPaths.CountResultsPath(request.ProjectName, researchMethod.ToString(), request.UseControlGroup);
                _filePersistencyHandler.WriteFile(pathForResults, jsonText);
                Log.Information("Json with countings for aspects written to {path}.", pathForResults);

                return response;   */
        return null;

    }

    int[,] DefineTotals(List<ChartPoints> celPoints, List<ChartPoints> mundanePoints, List<int> cusps, List<AspectTypes> aspectTypes, List<DefinedAspect> effectiveAspects)
    {

        int combinedPointSize = celPoints.Count + mundanePoints.Count + cusps.Count;
        int aspectSize = aspectTypes.Count;
        int[,] totals = new int[combinedPointSize, aspectSize];

        int point1Index;
        int point2Index;
        /*     foreach (EffectiveAspect effAspect in effectiveAspects)
             {

                 if (effAspect.IsMundane)
                 {
                     point1Index = FindIndexForMundanePoint(celPoints.Count, effAspect.MundanePoint, mundanePoints);
                 } else
                 {
                     point1Index = FindIndexForCelPoint((ChartPoints)effAspect.Point1, celPoints);
                 }
                 // todo handle cusps
                 point2Index = FindIndexForCelPoint(effAspect.Point2, celPoints);
                 totals[point1Index, point2Index] += 1;
             }
        */
        return totals;
    }

    private int FindIndexForCelPoint(ChartPoints celPoint, List<ChartPoints> celPoints)
    {
        int index = -1;
        for (int i = 0; i < celPoints.Count; i++)
        {
            if (celPoint == celPoints[i]) index = i;
        }
        return index;
    }

    private int FindIndexForMundanePoint(int offset, string mundanePoint, List<ChartPoints> mundanePoints)  // TODO use enum for mundanepoints and not strings.
    {
        switch (mundanePoint)
        {
            case "Ascendant":
                return offset + 1;
            case "Mc":
                return offset + 2;
            case "EastPoint":
                return offset + 3;
            case "Vertex":
                return offset + 4;
            default:
                return -1;
        }
    }

    private int FindIndexForCusp(int offset, int cusp)
    {
        return cusp + offset;
    }


}