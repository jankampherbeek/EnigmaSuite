// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.Enneagram;

/// <summary>
/// Data for an Enneagram calculation
/// </summary>
/// <param name="Point">Indication for a chartpoint</param>
/// <param name="Index">Index 1..12 that indicates a sign or a house</param>
/// <param name="Factors">Factors for the calculation</param>
public record EnneagramData(int Point, int Index, double[] Factors);