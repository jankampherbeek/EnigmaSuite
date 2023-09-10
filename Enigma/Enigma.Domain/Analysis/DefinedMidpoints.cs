// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Points;
using Enigma.Domain.References;

namespace Enigma.Domain.Analysis;

/// <summary>Details for a base Midpoint, a Midpoint Position that is not necessarily occupied.</summary>
/// <param name="Point1">First point.</param>
/// <param name="Point2">Second point.</param>
/// <param name="Position">Midpoint using the shortest arc.</param>
public record BaseMidpoint(PositionedPoint Point1, PositionedPoint Point2, double Position);


/// <summary>Details for an occupied Midpoint.</summary>
/// <param name="Midpoint">The base Midpoint, consisting of two points.</param>
/// <param name="OccupyingPoint">The point that is at the Midpoint Position.</param>
/// <param name="Orb">Actual Orb.</param>
/// <param name="Exactness">Percentage of Exactness, based on actual Orb.</param>
public record OccupiedMidpoint(BaseMidpoint Midpoint, PositionedPoint OccupyingPoint, double Orb, double Exactness);


/// <summary>An occupied midpoint structure without positions.</summary>
/// <remarks>This record is mainly used to enable counting.</remarks>
/// <param name="FirstPoint">One of the two points that form the midpoint.</param>
/// <param name="SecondPoint">Another one of the two points that form the midpoint.</param>
/// <param name="OccupyingPoint">The point that occupied the midpoint position.</param>
public record OccupiedMidpointStructure(ChartPoints FirstPoint, ChartPoints SecondPoint, ChartPoints OccupyingPoint);
