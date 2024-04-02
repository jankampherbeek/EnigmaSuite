// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Analysis;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Handlers;

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
}

/// <inheritdoc/>
public sealed class MidpointsHandler : IMidpointsHandler
{
    private readonly IPointsForMidpoints _analysisPointsForMidpoints;
    private readonly IBaseMidpointsCreator _baseMidpointsCreator;
    private readonly IOccupiedMidpointsFinder _occupiedMidpoints;


    public MidpointsHandler(IPointsForMidpoints analysisPointsForMidpoints,
        IBaseMidpointsCreator baseMidpointsCreator,
        IOccupiedMidpointsFinder occupiedMidpoints)
    {
        _analysisPointsForMidpoints = analysisPointsForMidpoints;
        _baseMidpointsCreator = baseMidpointsCreator;
        _occupiedMidpoints = occupiedMidpoints;
    }

    /// <inheritdoc/>
    public IEnumerable<BaseMidpoint> RetrieveBaseMidpoints(CalculatedChart chart)
    {
        const double dialSize = 360.0;
        List<PositionedPoint> analysisPoints = _analysisPointsForMidpoints.CreatePositionedPoints(chart, dialSize);
        return _baseMidpointsCreator.CreateBaseMidpoints(analysisPoints);
    }

    /// <inheritdoc/>
    public IEnumerable<OccupiedMidpoint> RetrieveOccupiedMidpoints(CalculatedChart chart, double dialSize, double orb)
    {
        return _occupiedMidpoints.CalculateOccupiedMidpoints(chart, dialSize, orb);
    }

    /// <inheritdoc/>
    public List<OccupiedMidpoint> RetrieveOccupiedMidpoints(List<PositionedPoint> posPoints, double dialSize, double orb)
    {
        return _occupiedMidpoints.CalculateOccupiedMidpoints(posPoints, dialSize, orb);
    }

}