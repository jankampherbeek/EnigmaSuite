// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Core.Research;

/// <summary>Calculate midpoints in declination.</summary>
public interface IOccupiedMidpointsDeclinationCounting
{
    /// <summary>Perform a count for occupied midpoints in declination.</summary>
    /// <param name="charts">The calculated charts to check.</param>
    /// <param name="request">The original request.</param>
    /// <returns>The calculated counts.</returns>
    public CountOfOccupiedMidpointsDeclResponse CountMidpointsInDeclination(
        IEnumerable<CalculatedResearchChart> charts, 
        CountOccupiedMidpointsDeclinationRequest request);
}


// ====================================== Implementation ========================================================


/// <inheritdoc/>
public sealed class OccupiedMidpointsDeclinationCounting: IOccupiedMidpointsDeclinationCounting
{
    
    private readonly IMidpointsHandler _midpointsHandler;
    private readonly IPointsMapping _pointsMapping;
    private readonly IResearchMethodUtils _researchMethodUtils;


    public OccupiedMidpointsDeclinationCounting(
        IMidpointsHandler midpointsHandler, 
        IPointsMapping pointsMapping, 
        IResearchMethodUtils researchMethodUtils)
    {
        _midpointsHandler = midpointsHandler;
        _pointsMapping = pointsMapping;
        _researchMethodUtils = researchMethodUtils;
    }
    
    /// <inheritdoc/>
    public CountOfOccupiedMidpointsDeclResponse CountMidpointsInDeclination(
        IEnumerable<CalculatedResearchChart> charts, 
        CountOccupiedMidpointsDeclinationRequest request)
    {
        return PerformCount(charts, request);
    }

    private CountOfOccupiedMidpointsDeclResponse PerformCount(IEnumerable<CalculatedResearchChart> charts, CountOccupiedMidpointsDeclinationRequest request)
    {
        List<ChartPoints> selectedPoints = request.PointSelection.SelectedPoints;
        Dictionary<OccupiedMidpointStructure, int> allCounts = InitializeAllCounts(selectedPoints);
        AstroConfig config = request.Config;
        double orb = config.OrbMidpointsDecl;

        foreach (OccupiedMidpointStructure mpStructure in from calcResearchChart in charts 
                 let commonPositions = (
                     from posPoint in calcResearchChart.Positions
                     where (posPoint.Key.GetDetails().PointCat == PointCats.Common || posPoint.Key.GetDetails().PointCat == PointCats.Angle)
                     select posPoint).ToDictionary(x => x.Key, x => x.Value) 
                 select _researchMethodUtils.DefineSelectedPointPositions(calcResearchChart, request.PointSelection) 
                 into relevantChartPointPositions 
                 select _pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Equatorial, false) 
                 into posPoints select _midpointsHandler.RetrieveOccupiedMidpointsInDeclination(posPoints, orb) 
                 into occupiedMidpoints 
                 from mpStructure 
                     in occupiedMidpoints.Select(occupiedMidpoint 
                     => new OccupiedMidpointStructure(occupiedMidpoint.Midpoint.Point1.Point, 
                         occupiedMidpoint.Midpoint.Point2.Point, occupiedMidpoint.OccupyingPoint.Point)) select mpStructure)
        {
            allCounts[mpStructure]++;
        }
        return new CountOfOccupiedMidpointsDeclResponse(request, allCounts);
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