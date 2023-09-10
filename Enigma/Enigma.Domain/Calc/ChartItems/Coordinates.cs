// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Points;
using Enigma.Domain.References;

namespace Enigma.Domain.Calc.ChartItems;


/// <summary>Ecliptic position, consisting of Longitude and Latitude.</summary>
/// <param name="Longitude">Ecliptic Longitude.</param>
/// <param name="Latitude">Ecliptic Latitude.</param>
public record EclipticCoordinates(double Longitude, double Latitude);


/// <summary>Ecliptic position for a specific clestial point.</summary>
/// <param name="CelPoint"/>
/// <param name="EclipticCoordinate"/>
public record NamedEclipticCoordinates(ChartPoints CelPoint, EclipticCoordinates EclipticCoordinate);


/// <summary>Ecliptic Longitude for a specific celestial point.</summary>
/// <param name="CelPoint"/>
/// <param name="EclipticLongitude"/>
public record NamedEclipticLongitude(ChartPoints CelPoint, double EclipticLongitude);


/// <summary>Equatorial position, consisting of right ascension and Declination.</summary>
/// <param name="RightAscension">Equatorial distance in degrees.</param>
/// <param name="Declination">Declination, deviation from equator.</param>
public record EquatorialCoordinates(double RightAscension, double Declination);


/// <summary>Horizontal coordinates.</summary>
/// <param name="Azimuth">Azimuth, starts at south (0 degrees) in western direction (90 degrees) etc. North = 180 degrees and east = 270 degrees.</param>
/// <param name="Altitude">Altitude (height above horizon, negative is below horizon).</param>
public record HorizontalCoordinates(double Azimuth, double Altitude);