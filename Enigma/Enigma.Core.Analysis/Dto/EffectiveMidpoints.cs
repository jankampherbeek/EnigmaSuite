// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis.Dto;

namespace Enigma.Domain.Analysis;

/// <summary>
/// Details for an effective midpoint.
/// </summary>
public record EffectiveMidpoint
{
    public readonly AnalysisPoint Point1;
    public readonly AnalysisPoint Point2;
    public readonly double Position;

/// <summary>
/// Constructs an effective midpoint.
/// </summary>
/// <param name="point1">First point.</param>
/// <param name="point2">Second point.</param>
/// <param name="position">Midpoint using the shortest arc.</param>
    public EffectiveMidpoint(AnalysisPoint point1, AnalysisPoint point2, double position)
    {
        Point1 = point1;
        Point2 = point2;
        Position = position;
    }
}


/// <summary>
/// Details for an effective occupied midpoint.
/// </summary>
public record EffOccupiedMidpoint
{
    public readonly EffectiveMidpoint EffMidpoint;
    public readonly AnalysisPoint OccupyingPoint;
    public readonly double OccupyingPointPosition;
    public readonly double Exactness;

    /// <summary>
    /// Constructs an effective occupied midpoint.
    /// </summary>
    /// <param name="effMidpoint">THe effective midpoint, consisting of two points.</param>
    /// <param name="occupyingPoint">The point that is at the midpoint position.</param>
    /// <param name="occupyingPointPosition">Longitude of the point at the midpoint position.</param>
    /// <param name="exactness">Percentage of exactness, based on actual orb.</param>
    public EffOccupiedMidpoint(EffectiveMidpoint effMidpoint, AnalysisPoint occupyingPoint, double occupyingPointPosition, double exactness)
    {
        EffMidpoint = effMidpoint;
        OccupyingPoint = occupyingPoint;
        OccupyingPointPosition = occupyingPointPosition;
        Exactness = exactness;
    }

}
