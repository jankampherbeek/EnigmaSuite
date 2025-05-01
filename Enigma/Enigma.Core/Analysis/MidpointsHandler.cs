// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Core.Analysis;

/// <summary>Handler for midpoints.</summary>
public interface IMidpointsHandler
{
    /// <summary>Retrieve list with all base midpoints between two items, regardless if the midpoint is occupied.</summary>
    /// <param name="chart">Calculated chart with positions.</param>
    /// <returns>All base midpoints.</returns>
    public IEnumerable<BaseMidpoint> RetrieveBaseMidpoints(CalculatedChart chart);

    /// <summary>Retrieve list with all occupied midpoints for a specified dial.</summary>
    /// <param name="chart">Calculated chart with positions.</param>
    /// <param name="dialSize">Degrees for specified dial.</param>
    /// <param name="orb">Base orb from configuration.</param>
    /// <returns>All occupied midpoints.</returns>
    public IEnumerable<OccupiedMidpoint> RetrieveOccupiedMidpoints(CalculatedChart chart, double dialSize, double orb);

    /// <summary>Retrieve list with occupied midpoints for a given set of points and for a specified dial.</summary>
    /// <param name="posPoints">List with points.</param>
    /// <param name="dialSize">Degrees for specified dial.</param>
    /// <param name="orb">User defined orb.</param>
    /// <returns>Occupied midpoints for the given set of points.</returns>
    public List<OccupiedMidpoint> RetrieveOccupiedMidpoints(List<PositionedPoint> posPoints, double dialSize, double orb);

    /// <summary>Retrieve list with occupied midpoints in declination for a given set  fpoints.</summary>
    /// <param name="posPoints">List with points.</param>
    /// <param name="orb">User defined orb.</param>
    /// <returns>Occupied midpoints.</returns>
    public List<OccupiedMidpoint> RetrieveOccupiedMidpointsInDeclination(List<PositionedPoint> posPoints, double orb);

}


/// <inheritdoc/>
public sealed class MidpointsHandler(
    IPointsForMidpoints analysisPointsForMidpoints,
    IBaseMidpointsCreator baseMidpointsCreator,
    IOccupiedMidpointsFinder occupiedMidpoints)
    : IMidpointsHandler
{
    /// <inheritdoc/>
    public IEnumerable<BaseMidpoint> RetrieveBaseMidpoints(CalculatedChart chart)
    {
        const double dialSize = 360.0;
        var analysisPoints = analysisPointsForMidpoints.CreatePositionedPoints(chart, dialSize);
        return baseMidpointsCreator.CreateBaseMidpoints(analysisPoints);
    }

    /// <inheritdoc/>
    public IEnumerable<OccupiedMidpoint> RetrieveOccupiedMidpoints(CalculatedChart chart, double dialSize, double orb)
    {
        return occupiedMidpoints.CalculateOccupiedMidpoints(chart, dialSize, orb);
    }

    /// <inheritdoc/>
    public List<OccupiedMidpoint> RetrieveOccupiedMidpoints(List<PositionedPoint> posPoints, double dialSize, double orb)
    {
        return occupiedMidpoints.CalculateOccupiedMidpoints(posPoints, dialSize, orb);
    }

    public List<OccupiedMidpoint> RetrieveOccupiedMidpointsInDeclination(List<PositionedPoint> posPoints, double orb)
    {
        return occupiedMidpoints.CalculateOccupiedMidpointsInDeclination(posPoints, orb);
    }
}