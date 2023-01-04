// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Points;

namespace Enigma.Domain.AstronCalculations;

/// <summary>Ecliptic position, consisting of Longitude and Latitude.</summary>
/// <param name="Longitude">Ecliptic Longitude.</param>
/// <param name="Latitude">Ecliptic Latitude.</param>
public record EclipticCoordinates(double Longitude, double Latitude);



/// <summary>Ecliptic position for a specific clestial point.</summary>
/// <param name="CelPoint"/>
/// <param name="EclipticCoordinate"/>
public record NamedEclipticCoordinates(CelPoints CelPoint, EclipticCoordinates EclipticCoordinate);


/// <summary>Ecliptic Longitude for a specific celestial point.</summary>
/// <param name="CelPoint"/>
/// <param name="EclipticLongitude"/>
public record NamedEclipticLongitude(CelPoints CelPoint, double EclipticLongitude);

