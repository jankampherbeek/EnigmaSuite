// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.Enneagram;

/// <summary>
/// Request for the calculation of an Enneagram
/// </summary>
/// <param name="JulianDay">The Julian day</param>
/// <param name="GeoLon">Geographic longitude</param>
/// <param name="GeoLat">Geographic latitude</param>
/// <param name="IsTimeKnown">true if birth time is known</param>
/// <param name="IsDoublePluto">True if Pluto should be calculated twice</param>
public record EnneagramRequest(double JulianDay, double GeoLon, double GeoLat, bool IsTimeKnown, bool IsDoublePluto);