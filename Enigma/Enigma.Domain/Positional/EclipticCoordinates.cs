// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.CalcVars;

namespace Enigma.Domain.Positional;

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


/// <summary>Ecliptic position for a specific Solar system point.</summary>
public record NamedEclipticCoordinates
{
    public readonly SolarSystemPoints SolarSystemPoint;
    public readonly EclipticCoordinates EclipticCoordinates;

    /// <param name="solarSystemPoint"/>
    /// <param name="eclipticCoordinate"/>
    public NamedEclipticCoordinates(SolarSystemPoints solarSystemPoint, EclipticCoordinates eclipticCoordinate)
    {
        SolarSystemPoint = solarSystemPoint;
        EclipticCoordinates = eclipticCoordinate;
    }
}

/// <summary>Ecliptic longitude for a specific Solar system point.</summary>
public record NamedEclipticLongitude
{
    public readonly SolarSystemPoints SolarSystemPoint;
    public readonly double EclipticLongitude;

    /// <param name="solarSystemPoint"/>
    /// <param name="eclipticLongitude"/>
    public NamedEclipticLongitude(SolarSystemPoints solarSystemPoint, double eclipticLongitude)
    {
        SolarSystemPoint = solarSystemPoint;
        EclipticLongitude = eclipticLongitude;
    }
}
