// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace E4C.core.shared.domain;

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