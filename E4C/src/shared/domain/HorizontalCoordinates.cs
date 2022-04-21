// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace E4C.shared.domain;

/// <summary>
/// Horizontal coordinates.
/// </summary>
public record HorizontalCoordinates
{
    /// <summary>Azimuth, starts at south (0 degrees) in western direction (90 degrees) etc. North = 180 degrees and east = 270 degrees.</summary>
    public readonly double Azimuth;
    /// <summary>Alitude (height above horizon, negative is below horizon).</summary>
    public readonly double Altitude;

    /// <param name="azimuth"/>
    /// <param name="altitude"/>
    public HorizontalCoordinates(double azimuth, double altitude)
    {
        Azimuth = azimuth;
        Altitude = altitude;
    }
}