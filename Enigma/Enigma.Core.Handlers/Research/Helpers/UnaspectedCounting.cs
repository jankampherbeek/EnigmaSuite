// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Domain.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Enigma.Research.Domain;

namespace Enigma.Core.Handlers.Research.Helpers;


public sealed class UnaspectedCounting: IUnaspectedCounting
{
    private readonly IAspectsHandler _aspectsHandler;
    private readonly IAspectPointSelector _aspectPointSelector;
    private readonly IPointsMapping _pointsMapping;

    public UnaspectedCounting(IAspectsHandler aspectsHandler, IAspectPointSelector aspectPointSelector, IPointsMapping pointsMapping)
    {
        _aspectsHandler = aspectsHandler;
        _aspectPointSelector = aspectPointSelector;
        _pointsMapping = pointsMapping;

    }
    public CountOfUnaspectedResponse CountUnaspected(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        return PerformCount(charts, request);
    }

    private CountOfUnaspectedResponse PerformCount(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        ResearchPointsSelection pointsSelection = request.PointsSelection;
        AstroConfig config = request.Config;
        List<AspectConfigSpecs> allAspects = config.Aspects;
        List<AspectConfigSpecs> relevantAspects = new();
        foreach (AspectConfigSpecs acSpec in allAspects)
        {
            if (acSpec.IsUsed) relevantAspects.Add(acSpec);
        }
        List<ChartPointConfigSpecs> chartPointConfigSpecs = config.ChartPoints;
        List<ChartPoints> relevantPoints = new();
        foreach (ChartPointConfigSpecs cpConfigSpec in chartPointConfigSpecs)
        {
            if (cpConfigSpec.IsUsed) relevantPoints.Add(cpConfigSpec.Point);
        }

        int celPointSize = relevantPoints.Count;
        int[,] allCounts = new int[celPointSize, charts.Count];

        // todo combine main part of this method with comparable methods in other classes (AspectsCounting)
        int chartIndex = 0;
        foreach (CalculatedResearchChart calcResearchChart in charts)
        {
            List<FullChartPointPos> chartPointPositions = calcResearchChart.CelPointPositions;
            FullHousesPositions fullHousesPositions = calcResearchChart.FullHousePositions;
            List<FullChartPointPos> configChartPointPositions = _aspectPointSelector.SelectPoints(chartPointPositions, fullHousesPositions, chartPointConfigSpecs);
            List<FullChartPointPos> relevantChartPointPositions = new();

            foreach (FullChartPointPos configPoint in configChartPointPositions)
            {
                foreach (ChartPoints selectPoint in relevantPoints)
                {
                    if (configPoint.ChartPoint == selectPoint)
                    {
                        relevantChartPointPositions.Add(configPoint);
                    }
                }
            }
            if (request.PointsSelection.SelectedMundanePoints.Contains(ChartPoints.Mc))
            {
                relevantChartPointPositions.Add(calcResearchChart.FullHousePositions.Mc);
            };
            if (request.PointsSelection.SelectedMundanePoints.Contains(ChartPoints.Ascendant))
            {
                relevantChartPointPositions.Add(calcResearchChart.FullHousePositions.Ascendant);
            };
            if (request.PointsSelection.SelectedMundanePoints.Contains(ChartPoints.Vertex))
            {
                relevantChartPointPositions.Add(calcResearchChart.FullHousePositions.Vertex);
            };
            if (request.PointsSelection.SelectedMundanePoints.Contains(ChartPoints.EastPoint))
            {
                relevantChartPointPositions.Add(calcResearchChart.FullHousePositions.EastPoint);
            };
            List<PositionedPoint> posPoints = _pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Ecliptical, true);
            List<PositionedPoint> cuspPoints = new();       // use empty list
            List<DefinedAspect> definedAspects = _aspectsHandler.AspectsForPosPoints(posPoints, cuspPoints, relevantAspects, config.BaseOrbAspects);


            foreach (PositionedPoint posPoint in posPoints)
            {
                ChartPoints point = posPoint.Point;
                int aspectCount = 0;
                foreach (DefinedAspect defAspect in definedAspects)
                {
                    if (defAspect.Point1 == point || defAspect.Point2 == point)
                    {
                        aspectCount++;
                    }
                }
                if (aspectCount == 0) 
                {
                    for (int i = 0; i < celPointSize; i++)
                    {
                        if (relevantChartPointPositions[i].ChartPoint == point)
                        {
                            allCounts[chartIndex, i]++;
                        }
                    }
                }
            }
            chartIndex++;
        }
        List<SimpleCount> resultingCounts = new();
        for (int i = 0; i < celPointSize; i++)
        {
            ChartPoints point = chartPointConfigSpecs[i].Point;
            int unaspectedCount = 0;
            for (int j = 0; j < charts.Count; j++)
            {
                unaspectedCount+= allCounts[i, j];
            }
            resultingCounts.Add(new SimpleCount(point, unaspectedCount));
        }
        return new CountOfUnaspectedResponse(request, resultingCounts);
    }
}