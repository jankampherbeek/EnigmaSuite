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

/// <summary>Counting for occupied midpoints.</summary>
public interface IOccupiedMidpointsCounting
{
    /// <summary>Perform a count for occupied midpoints.</summary>
    /// <param name="charts">The calculated charts to check.</param>
    /// <param name="request">The original request.</param>
    /// <returns>The calculated counts.</returns>
    public CountOfOccupiedMidpointsResponse CountMidpoints(IEnumerable<CalculatedResearchChart> charts, CountOccupiedMidpointsRequest request);
}


// ========================== Implementation ======================================

/// <inheritdoc/>
public sealed class OccupiedMidpointsCounting(
    IMidpointsHandler midpointsHandler,
    IPointsMapping pointsMapping,
    IResearchMethodUtils researchMethodUtils,
    IProjectDao projectDao)
    : IOccupiedMidpointsCounting
{
    /// <inheritdoc/>
    public CountOfOccupiedMidpointsResponse CountMidpoints(IEnumerable<CalculatedResearchChart> charts,
        CountOccupiedMidpointsRequest request)
    {
        return PerformCount(charts, request);
    }

    private CountOfOccupiedMidpointsResponse PerformCount(IEnumerable<CalculatedResearchChart> charts,
        CountOccupiedMidpointsRequest request)
    {
        var ctrlGroupFactor = projectDao.ReadProject(request.ProjectName)!.MultiFactor;
        var selectedPoints = request.PointSelection.SelectedPoints;
        Dictionary<OccupiedMidpointStructure, int> allCounts = InitializeAllCounts(selectedPoints);

        var dialSize = 360.0 / request.DivisionForDial;
        var config = request.Config;
        var orb = config.BaseOrbMidpoints;

        foreach (var mpStructure in from calcResearchChart in charts
                 let commonPositions = (
                     from posPoint in calcResearchChart.Positions
                     where (posPoint.Key.GetDetails().PointCat == PointCats.Common ||
                            posPoint.Key.GetDetails().PointCat == PointCats.Angle ||
                            posPoint.Key.GetDetails().PointCat == PointCats.Lots)
                     select posPoint).ToDictionary(x => x.Key, x => x.Value)
                 select researchMethodUtils.DefineSelectedPointPositions(calcResearchChart, request.PointSelection)
                 into relevantChartPointPositions
                 select pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions,
                     CoordinateSystems.Ecliptical, true)
                 into posPoints
                 select midpointsHandler.RetrieveOccupiedMidpoints(posPoints, dialSize, orb)
                 into occupiedMidpoints
                 from mpStructure
                     in occupiedMidpoints.Select(occupiedMidpoint
                         => new OccupiedMidpointStructure(occupiedMidpoint.Midpoint.Point1.Point,
                             occupiedMidpoint.Midpoint.Point2.Point, occupiedMidpoint.OccupyingPoint.Point))
                 select mpStructure)
        {
            allCounts[mpStructure]++;
        }

        return new CountOfOccupiedMidpointsResponse(request, ctrlGroupFactor, allCounts);
    }

    private static Dictionary<OccupiedMidpointStructure, int> InitializeAllCounts(List<ChartPoints> selectedPoints)
    {
        const int countValue = 0;
        Dictionary<OccupiedMidpointStructure, int> allCounts = new();
        foreach (var firstPoint in selectedPoints)
        {
            foreach (var secondPoint in selectedPoints)
            {
                foreach (var occupyingPoint in selectedPoints)
                {
                    allCounts.Add(new OccupiedMidpointStructure(firstPoint, secondPoint, occupyingPoint), countValue);
                }
            }
        }

        return allCounts;
    }

}