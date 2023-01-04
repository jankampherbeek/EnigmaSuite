// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.AstronCalculations;

/// <summary>Equatorial position, consisting of right ascension and Declination.</summary>
/// <param name="RightAscension">Equatorial distance in degrees.</param>
/// <param name="Declination">Declination, deviation from equator.</param>
public record EquatorialCoordinates(double RightAscension, double Declination);
