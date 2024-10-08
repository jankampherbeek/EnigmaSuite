﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Core.Research;

/// <summary>Counting for unaspected points.</summary>
public interface IUnaspectedCounting
{
    /// <summary>Perform a count of unaspected points.</summary>
    /// <param name="charts">The calculated charts to check.</param>
    /// <param name="request">The original request.</param>
    /// <returns>The calculated counts.</returns>
    public CountOfUnaspectedResponse CountUnaspected(List<CalculatedResearchChart> charts, GeneralResearchRequest request);
}


public sealed class UnaspectedCounting : IUnaspectedCounting
{
    private readonly IAspectsHandler _aspectsHandler;
    private readonly IPointsMapping _pointsMapping;
    private readonly IResearchMethodUtils _researchMethodUtils;

    public UnaspectedCounting(IAspectsHandler aspectsHandler, IPointsMapping pointsMapping, IResearchMethodUtils researchMethodUtils)
    {
        _aspectsHandler = aspectsHandler;
        _pointsMapping = pointsMapping;
        _researchMethodUtils = researchMethodUtils;

    }
    public CountOfUnaspectedResponse CountUnaspected(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        return PerformCount(charts, request);
    }

    private CountOfUnaspectedResponse PerformCount(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        AstroConfig config = request.Config;

        Dictionary<AspectTypes, AspectConfigSpecs> configSelectedAspects = _researchMethodUtils.DefineConfigSelectedAspects(config);
        int selectedCelPointSize = request.PointSelection.SelectedPoints.Count;
        int[,] allCounts = new int[selectedCelPointSize, charts.Count];

        int chartIndex = 0;
        foreach (CalculatedResearchChart calcResearchChart in charts)
        {
            Dictionary<ChartPoints, FullPointPos> relevantChartPointPositions = _researchMethodUtils.DefineSelectedPointPositions(calcResearchChart, request.PointSelection);
            List<PositionedPoint> posPoints = _pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Ecliptical, true);
            List<PositionedPoint> cuspPoints = new();       // use empty list
            List<DefinedAspect> definedAspects = _aspectsHandler.AspectsForPosPoints(posPoints, cuspPoints, configSelectedAspects, config.ChartPoints, config.BaseOrbAspects);

            foreach (PositionedPoint posPoint in posPoints)
            {
                ChartPoints point = posPoint.Point;
                int aspectCount = definedAspects.Count(defAspect => defAspect.Point1 == point || defAspect.Point2 == point);

                if (aspectCount != 0) continue;
                int aspectIndex = 0;
                foreach (var rcpPos in relevantChartPointPositions)
                {
                    if (rcpPos.Key == point)
                    {
                        allCounts[aspectIndex, chartIndex]++;
                    }
                    aspectIndex++;
                }
            }
            chartIndex++;
        }
        List<SimpleCount> resultingCounts = new();
        List<ChartPoints> selectedPoints = request.PointSelection.SelectedPoints;
        int i = 0;
        foreach (var point in selectedPoints)
        {
            int unaspectedCount = 0;
            for (int j = 0; j < charts.Count; j++)
            {
                unaspectedCount += allCounts[i, j];
            }
            resultingCounts.Add(new SimpleCount(point, unaspectedCount));
            i++;
        }
        return new CountOfUnaspectedResponse(request, resultingCounts);
    }



}