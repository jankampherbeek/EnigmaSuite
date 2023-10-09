// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Dtos;

/// <summary>Progressive positions, both ecliptical and equatorial. To be used for serveral progressive techniques.</summary>
/// <param name="Longitude">Ecliptical position in longitude.</param>
/// <param name="Latitude">Ecliptical position in latitude.</param>
/// <param name="Ra">Equatorial position in right ascension.</param>
/// <param name="Declination">Equatorial position in declination.</param> 
public record ProgPositions(double Longitude, double Latitude, double Ra, double Declination);