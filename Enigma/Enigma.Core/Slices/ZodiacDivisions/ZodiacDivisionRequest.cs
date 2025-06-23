// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.ZodiacDivisions;

/// <summary>
/// Request for te calculation of zodiac divisions
/// </summary>
/// <param name="Longitude">The ecliptic longitude which should be minimal 0.0 and smaller than 360.0</param>
/// <param name="Method">The method to be used</param>
public record ZodiacDivisionRequest(double Longitude, ZodiacDivisionMethods Method);