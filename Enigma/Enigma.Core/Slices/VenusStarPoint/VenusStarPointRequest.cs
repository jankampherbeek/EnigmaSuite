// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.VenusStarPoint;

/// <summary>
/// Request for the calculation of a Venus Star Point
/// </summary>
/// <param name="Jd">Julian day of birth</param>
/// <param name="Ayanamsha">Ayanamsha, 'None' for tropical</param>
public record VenusStarPointRequest(double Jd, Ayanamshas Ayanamsha);