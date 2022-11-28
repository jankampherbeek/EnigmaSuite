// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Enums;

namespace Enigma.Domain.AstronCalculations;

/// <summary>Ecliptic position, consisting of longitude and latitude.</summary>
public record EclipticCoordinates
{

    public double Longitude { get; }
    public double Latitude { get; }

    /// <param name="longitude">Ecliptic longitude.</param>
    /// <param name="latitude">Ecliptic latitude.</param>
    public EclipticCoordinates(double longitude, double latitude)
    {
        Longitude = longitude;
        Latitude = latitude;
    }
}


/// <summary>Ecliptic position for a specific clestial point.</summary>
public record NamedEclipticCoordinates
{
    public readonly CelPoints CelPoint;
    public readonly EclipticCoordinates EclipticCoordinates;

    /// <param name="celPoint"/>
    /// <param name="eclipticCoordinate"/>
    public NamedEclipticCoordinates(CelPoints celPoint, EclipticCoordinates eclipticCoordinate)
    {
        CelPoint = celPoint;
        EclipticCoordinates = eclipticCoordinate;
    }
}

/// <summary>Ecliptic longitude for a specific celestial point.</summary>
public record NamedEclipticLongitude
{
    public readonly CelPoints CelPoint;
    public readonly double EclipticLongitude;

    /// <param name="celPoint"/>
    /// <param name="eclipticLongitude"/>
    public NamedEclipticLongitude(CelPoints celPoint, double eclipticLongitude)
    {
        CelPoint = celPoint;
        EclipticLongitude = eclipticLongitude;
    }
}
