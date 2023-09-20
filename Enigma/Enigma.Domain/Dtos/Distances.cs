// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Dtos;

/// <summary>Definition of two points and the Distance between these two points.</summary>
/// <param name="Point1">The first point.</param>
/// <param name="Point2">The second point.</param>
/// <param name="Distance">The Distance.</param>
public record DistanceBetween2Points(PositionedPoint Point1, PositionedPoint Point2, double Distance);