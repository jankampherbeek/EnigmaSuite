// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Net.Cache;
using Enigma.Core.Handlers;
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
public sealed class OccupiedMidpointsCounting : IOccupiedMidpointsCounting
{
    private readonly IMidpointsHandler _midpointsHandler;
    private readonly IPointsMapping _pointsMapping;
    private readonly IResearchMethodUtils _researchMethodUtils;

    public OccupiedMidpointsCounting(IMidpointsHandler midpointsHandler, IPointsMapping pointsMapping, IResearchMethodUtils researchMethodUtils)
    {
        _midpointsHandler = midpointsHandler;
        _pointsMapping = pointsMapping;
        _researchMethodUtils = researchMethodUtils;
    }


    /// <inheritdoc/>
    public CountOfOccupiedMidpointsResponse CountMidpoints(IEnumerable<CalculatedResearchChart> charts, CountOccupiedMidpointsRequest request)
    {
        return PerformCount(charts, request);
    }


    private CountOfOccupiedMidpointsResponse PerformCount(IEnumerable<CalculatedResearchChart> charts, CountOccupiedMidpointsRequest request)
    {
        List<ChartPoints> selectedPoints = request.PointSelection.SelectedPoints;
        Dictionary<OccupiedMidpointStructure, int> allCounts = InitializeAllCounts(selectedPoints);

        double dialSize = 360.0 / request.DivisionForDial;
        AstroConfig config = request.Config;
        double orb = config.BaseOrbMidpoints; 

        foreach (OccupiedMidpointStructure mpStructure in from calcResearchChart in charts 
                 let commonPositions = (
                     from posPoint in calcResearchChart.Positions
                     where (posPoint.Key.GetDetails().PointCat == PointCats.Common || posPoint.Key.GetDetails().PointCat == PointCats.Angle)
                     select posPoint).ToDictionary(x => x.Key, x => x.Value) 
                 select _researchMethodUtils.DefineSelectedPointPositions(calcResearchChart, request.PointSelection) 
                 into relevantChartPointPositions 
                 select _pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Ecliptical, true) 
                 into posPoints select _midpointsHandler.RetrieveOccupiedMidpoints(posPoints, dialSize, orb) 
                 into occupiedMidpoints 
                 from mpStructure 
                     in occupiedMidpoints.Select(occupiedMidpoint 
                     => new OccupiedMidpointStructure(occupiedMidpoint.Midpoint.Point1.Point, 
                         occupiedMidpoint.Midpoint.Point2.Point, occupiedMidpoint.OccupyingPoint.Point)) select mpStructure)
        {
            allCounts[mpStructure]++;
        }
        return new CountOfOccupiedMidpointsResponse(request, allCounts);
    }


    private static Dictionary<OccupiedMidpointStructure, int> InitializeAllCounts(List<ChartPoints> selectedPoints)
    {
        const int countValue = 0;
        Dictionary<OccupiedMidpointStructure, int> allCounts = new();
        foreach (ChartPoints firstPoint in selectedPoints)
        {
            foreach (ChartPoints secondPoint in selectedPoints)
            {
                foreach (ChartPoints occupyingPoint in selectedPoints)
                {
                    allCounts.Add(new OccupiedMidpointStructure(firstPoint, secondPoint, occupyingPoint), countValue);
                }
            }
        }
        return allCounts;
    }




}