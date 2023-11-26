// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Dtos;

/// <summary>Details for a harmonic</summary>
/// <param name="HarmonicNumber">The number for the harmonic, integer or double</param>
/// <param name="Orb">The orb</param>
public record HarmonicDetailsSelection(double HarmonicNumber,double Orb);