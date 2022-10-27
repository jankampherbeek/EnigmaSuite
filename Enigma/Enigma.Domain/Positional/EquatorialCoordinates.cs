// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Positional;

/// <summary>Equatorial position, consisting of right ascension and declination.</summary>
public record EquatorialCoordinates
{

    public double RightAscension { get; }
    public double Declination { get; }

    /// <param name="rightAscension">Equatorial distance in degrees.</param>
    /// <param name="declination">Declination, deviation from equator.</param>
    public EquatorialCoordinates(double rightAscension, double declination)
    {
        RightAscension = rightAscension;
        Declination = declination;
    }
}