// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.Enneagram;

/// <summary>
/// Enneagram details for a specific combination
/// </summary>
/// <param name="Point">The chart point</param>
/// <param name="PositionIndex">Index 1..12 for signs or for houses</param>
/// <param name="InSigns">True is sings are used, false for houses</param>
/// <param name="Factors">Array with 9 factors for this combination</param>
public record EnneagramDetailsLine(ChartPoints Point, int PositionIndex, bool InSigns, double[] Factors);