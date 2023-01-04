// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Points;

namespace Enigma.Domain.Analysis;

/// <summary>Details for a base midpoint, a midpoint position that is not necessarily occupied.</summary>
public record BaseMidpoint
{
    public readonly AnalysisPoint Point1;
    public readonly AnalysisPoint Point2;
    public readonly double Position;

    /// <summary>Constructs a base midpoint, id sgnostic about the dial in use.</summary>
    /// <param name="point1">First point.</param>
    /// <param name="point2">Second point.</param>
    /// <param name="position">Midpoint using the shortest arc.</param>
    public BaseMidpoint(AnalysisPoint point1, AnalysisPoint point2, double position)
    {
        Point1 = point1;
        Point2 = point2;
        Position = position;
    }
}


/// <summary>Details for an occupied midpoint.</summary>
public record OccupiedMidpoint
{
    public readonly BaseMidpoint Midpoint;
    public readonly AnalysisPoint OccupyingPoint;
    public readonly double OccupyingPointPosition;
    public readonly double Orb;
    public readonly double Exactness;

    /// <summary>Constructs an occupied midpoint.</summary>
    /// <param name="baseMidpoint">The base midpoint, consisting of two points.</param>
    /// <param name="occupyingPoint">The point that is at the midpoint position.</param>
    /// <param name="orb">Actual orb.</param>
    /// <param name="exactness">Percentage of exactness, based on actual orb.</param>
    public OccupiedMidpoint(BaseMidpoint midpoint, AnalysisPoint occupyingPoint, double orb, double exactness)
    {
        Midpoint = midpoint;
        OccupyingPoint = occupyingPoint;
        Orb = orb;
        Exactness = exactness;
    }

}
