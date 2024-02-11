// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Core.Analysis;

/// <summary>Handle the calculartion of occupied midpoints.</summary>
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
}

/// <inheritdoc/>
public sealed class OccupiedMidpointsFinder : IOccupiedMidpointsFinder
{

    private readonly IPointsForMidpoints _analysisPointsForMidpoints;
    private readonly IBaseMidpointsCreator _baseMidpointsCreator;

    public OccupiedMidpointsFinder(IPointsForMidpoints analysisPointsForMidpoints, IBaseMidpointsCreator baseMidpointsCreator)
    {
        _analysisPointsForMidpoints = analysisPointsForMidpoints;
        _baseMidpointsCreator = baseMidpointsCreator;
    }

    /// <inheritdoc/>
    public List<OccupiedMidpoint> CalculateOccupiedMidpoints(CalculatedChart chart, double dialSize, double baseOrb)
    {
        List<PositionedPoint> analysisPointsInActualDial = _analysisPointsForMidpoints.CreateAnalysisPoints(chart, dialSize);
        return CalculateOccupiedMidpoints(analysisPointsInActualDial, dialSize, baseOrb);
    }

    /// <inheritdoc/>
    public List<OccupiedMidpoint> CalculateOccupiedMidpoints(List<PositionedPoint> posPoints, double dialSize, double orb)
    {
        List<BaseMidpoint> baseMidpointsIn360Dial = _baseMidpointsCreator.CreateBaseMidpoints(posPoints);
        List<BaseMidpoint> baseMidpointsInActualDial = _baseMidpointsCreator.ConvertBaseMidpointsToDial(baseMidpointsIn360Dial, dialSize);

        return (from baseMidpoint in baseMidpointsInActualDial 
            let positionInDial = baseMidpoint.Position 
            from analysisPoint in posPoints 
            let actualOrb = MeasureMidpointDeviation(positionInDial, analysisPoint.Position, dialSize) 
            where actualOrb <= orb 
            let exactness = 100.0 - (actualOrb / orb * 100.0) 
            select new OccupiedMidpoint(baseMidpoint, analysisPoint, actualOrb, exactness)).ToList();
    }


    private static double MeasureMidpointDeviation(double midpointPos, double posCelPoint, double dialSize)
    {
        double smallPos = (posCelPoint < midpointPos) ? posCelPoint : midpointPos;
        double largePos = (posCelPoint < midpointPos) ? midpointPos : posCelPoint;
        double deviation = largePos - smallPos;
        if (deviation >= (dialSize / 2.0)) deviation = dialSize - deviation;
        return deviation;
    }
}