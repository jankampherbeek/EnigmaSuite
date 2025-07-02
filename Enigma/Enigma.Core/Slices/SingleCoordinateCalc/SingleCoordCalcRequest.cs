// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.SingleCoordinateCalc;

/// <summary>
/// Request for the calculation of positions using only one coordinate
/// </summary>
/// <param name="Points">The chart points to calculate</param>
/// <param name="JulianDay">Julian day</param>
/// <param name="Coordinate">The coordinate</param>
public record SingleCoordCalcRequest(List<ChartPoints> Points, double JulianDay, Coordinates Coordinate);