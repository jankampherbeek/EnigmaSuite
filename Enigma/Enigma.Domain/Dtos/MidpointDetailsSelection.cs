// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Dtos;

/// <summary>Details for midpoints.</summary>
/// <param name="DialDivision">The amount to divide 360 degrees by to get the correct midpoint dial</param>
/// <param name="Orb">The orb</param>
public record MidpointDetailsSelection(int DialDivision, double Orb);