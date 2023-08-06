// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Calc.ChartItems;


/// <summary>Request to calculate horizontal positions.</summary>
/// <param name="JdUt"/>
/// <param name="Location"/>
/// <param name="EquCoordinates">RA and declination.</param>
public record HorizontalRequest(double JdUt, Location Location, EquatorialCoordinates EquCoordinates);
