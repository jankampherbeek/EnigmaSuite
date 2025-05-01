// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Core.Persistency;
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
    public CountOfUnaspectedResponse CountUnaspected(List<CalculatedResearchChart> charts,
        GeneralResearchRequest request);
}

public sealed class UnaspectedCounting(
    IAspectsHandler aspectsHandler,
    IPointsMapping pointsMapping,
    IResearchMethodUtils researchMethodUtils,
    IProjectDao projectDao)
    : IUnaspectedCounting
{
    public CountOfUnaspectedResponse CountUnaspected(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        return PerformCount(charts, request);
    }

    private CountOfUnaspectedResponse PerformCount(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        var ctrlGroupFactor = projectDao.ReadProject(request.ProjectName)!.MultiFactor;
        var config = request.Config;
        var configSelectedAspects = researchMethodUtils.DefineConfigSelectedAspects(config);
        var selectedCelPointSize = request.PointSelection.SelectedPoints.Count;
        var allCounts = new int[selectedCelPointSize, charts.Count];
        var chartIndex = 0;
        foreach (var calcResearchChart in charts)
        {
            var relevantChartPointPositions =
                researchMethodUtils.DefineSelectedPointPositions(calcResearchChart, request.PointSelection);
            var posPoints =
                pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Ecliptical,
                    true);
            List<PositionedPoint> cuspPoints = []; // use empty list
            var definedAspects = aspectsHandler.AspectsForPosPoints(posPoints, cuspPoints, configSelectedAspects,
                config.ChartPoints, config.BaseOrbAspects);

            foreach (var posPoint in posPoints)
            {
                var point = posPoint.Point;
                var aspectCount =
                    definedAspects.Count(defAspect => defAspect.Point1 == point || defAspect.Point2 == point);

                if (aspectCount != 0) continue;
                var aspectIndex = 0;
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
        var selectedPoints = request.PointSelection.SelectedPoints;
        var i = 0;
        foreach (var point in selectedPoints)
        {
            var unaspectedCount = 0;
            for (var j = 0; j < charts.Count; j++)
            {
                unaspectedCount += allCounts[i, j];
            }

            resultingCounts.Add(new SimpleCount(point, unaspectedCount));
            i++;
        }

        return new CountOfUnaspectedResponse(request, ctrlGroupFactor, resultingCounts);
    }
}