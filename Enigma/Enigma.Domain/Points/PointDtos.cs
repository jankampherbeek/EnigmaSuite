﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Points;


/// <summary>A GenericPoint combined with a single position.</summary>
/// <param name="Point">The point.</param>
/// <param name="Position">The position in decimal degrees.</param>
public record PositionedPoint(ChartPoints Point, double Position);
