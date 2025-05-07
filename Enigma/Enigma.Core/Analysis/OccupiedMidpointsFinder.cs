// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Core.Analysis;

/// <summary>Handle the calculation of occupied midpoints.</summary>
public interface IOccupiedMidpointsFinder
{
    /// <summary>Calculate occupied midpoints for a specific dial.</summary>
    /// <param name="chart">Calculated chart.</param>
    /// <param name="dialSize">Dial size in degrees.</param>
    /// <param name="baseOrb">Base orb from configuration.</param>
    /// <returns>Calculated occupied midpoints.</returns>
    public List<OccupiedMidpoint> CalculateOccupiedMidpoints(CalculatedChart chart, double dialSize, double baseOrb);

    /// <summary>Calculate occupied midpoints for a specific dial.</summary>
    /// <param name="posPoints">List with points.</param>
    /// <param name="dialSize">Dial size in degrees.</param>
    /// <param name="orb">Orb.</param>
    /// <returns>Calculated occupied midpoints.</returns>
    public List<OccupiedMidpoint> CalculateOccupiedMidpoints(List<PositionedPoint> posPoints, double dialSize, double orb);

    /// <summary>Calculate occupied midpoints in declination.</summary>
    /// <param name="chart">Calculatred chart.</param>
    /// <param name="orb">Orb.</param>
    /// <returns>Calculated occupied midpoints in declination.</returns>
    public List<OccupiedMidpoint> CalculateOccupiedMidpointsInDeclination(CalculatedChart chart, double orb);

    /// <summary>Calculate occupied midpoints in declination.</summary>
    /// <param name="posPoints">The points.</param>
    /// <param name="orb">Orb.</param>
    /// <returns>Calculated occupied midpoints in declination.</returns>
    public List<OccupiedMidpoint> CalculateOccupiedMidpointsInDeclination(List<PositionedPoint> posPoints, double orb);

}

/// <inheritdoc/>
public sealed class OccupiedMidpointsFinder(
    IPointsForMidpoints analysisPointsForMidpoints,
    IBaseMidpointsCreator baseMidpointsCreator)
    : IOccupiedMidpointsFinder
{
    /// <inheritdoc/>
    public List<OccupiedMidpoint> CalculateOccupiedMidpoints(CalculatedChart chart, double dialSize, double baseOrb)
    {
        var analysisPointsInActualDial = analysisPointsForMidpoints.CreatePositionedPoints(chart, dialSize);
        return CalculateOccupiedMidpoints(analysisPointsInActualDial, dialSize, baseOrb);
    }

    /// <inheritdoc/>
    public List<OccupiedMidpoint> CalculateOccupiedMidpoints(List<PositionedPoint> posPoints, double dialSize, double orb)
    {
        var baseMidpointsIn360Dial = baseMidpointsCreator.CreateBaseMidpoints(posPoints);
        var baseMidpointsInActualDial = baseMidpointsCreator.ConvertBaseMidpointsToDial(baseMidpointsIn360Dial, dialSize);

        return (from baseMidpoint in baseMidpointsInActualDial 
            let positionInDial = baseMidpoint.Position 
            from analysisPoint in posPoints 
            let actualOrb = MeasureMidpointDeviation(positionInDial, analysisPoint.Position, dialSize) 
            where actualOrb <= orb 
            let exactness = 100.0 - (actualOrb / orb * 100.0) 
            select new OccupiedMidpoint(baseMidpoint, analysisPoint, actualOrb, exactness)).ToList();
    }

    public List<OccupiedMidpoint> CalculateOccupiedMidpointsInDeclination(CalculatedChart chart, double orb)
    {
        var analysisPointsInDeclination = analysisPointsForMidpoints.CreatePositionedPointsForDecl(chart);
        return CalculateOccupiedMidpointsInDeclination(analysisPointsInDeclination, orb);
    }

    /// <inheritdoc/>
    public List<OccupiedMidpoint> CalculateOccupiedMidpointsInDeclination(List<PositionedPoint> posPoints, double orb)
    {
        var baseMidpointsInDeclination = baseMidpointsCreator.CreateBaseMidpointsInDeclination(posPoints);
        return (from baseMidpoint in baseMidpointsInDeclination 
            let positionInDial = baseMidpoint.Position 
            from analysisPoint in posPoints 
            let actualOrb = MeasureMidpointDeviation(positionInDial, analysisPoint.Position) 
            where actualOrb <= orb 
            let exactness = 100.0 - (actualOrb / orb * 100.0) 
            select new OccupiedMidpoint(baseMidpoint, analysisPoint, actualOrb, exactness)).ToList();
    }

    private static double MeasureMidpointDeviation(double midpointPos, double posCelPoint, double dialSize)
    {
        var smallPos = (posCelPoint < midpointPos) ? posCelPoint : midpointPos;
        var largePos = (posCelPoint < midpointPos) ? midpointPos : posCelPoint;
        var deviation = largePos - smallPos;
        if (deviation >= (dialSize / 2.0)) deviation = dialSize - deviation;
        return deviation;
    }
    
    private static double MeasureMidpointDeviation(double midpointPos, double posCelPoint)
    {
        var smallPos = (posCelPoint < midpointPos) ? posCelPoint : midpointPos;
        var largePos = (posCelPoint < midpointPos) ? midpointPos : posCelPoint;
        return largePos - smallPos;
    }
}