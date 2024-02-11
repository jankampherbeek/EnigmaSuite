// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Core.Calc;

/// <summary>Factory for the construction of an instance of FullPointPos.</summary>
public interface IFullPointPosFactory
{
    /// <summary>Constract instance of FullPointPos.</summary>
    /// <param name="eclipticPosSpeed">Eclitpical vlaues: longitude, latitude, distance.</param>
    /// <param name="equatorialPosSpeed">Equatorial values: right ascension, declination, distance.</param>
    /// <param name="horCoord">Horizontal coordinates: azimuth and altitude.</param>
    /// <returns>Instance of FullPointPos.</returns>
    public FullPointPos CreateFullPointPos(PosSpeed[] eclipticPosSpeed, PosSpeed[] equatorialPosSpeed, HorizontalCoordinates horCoord);
}

public class FullPointPosFactory : IFullPointPosFactory
{

    public FullPointPos CreateFullPointPos(PosSpeed[] eclipticPosSpeed, PosSpeed[] equatorialPosSpeed, HorizontalCoordinates horCoord)
    {
        PosSpeed longPosSpeed = eclipticPosSpeed[0];
        PosSpeed latPosSpeed = eclipticPosSpeed[1];
        PosSpeed distPosSpeed = eclipticPosSpeed[2];
        PosSpeed raPosSpeed = equatorialPosSpeed[0];
        PosSpeed declPosSpeed = equatorialPosSpeed[1];
        PosSpeed aziPosSpeed = new(horCoord.Azimuth, 0.0);
        PosSpeed altPosSpeed = new(horCoord.Altitude, 0.0);
        PointPosSpeeds ppsEcliptical = new(longPosSpeed, latPosSpeed, distPosSpeed);
        PointPosSpeeds ppsEquatorial = new(raPosSpeed, declPosSpeed, distPosSpeed);
        PointPosSpeeds ppsHorizontal = new(aziPosSpeed, altPosSpeed, distPosSpeed);
        return new FullPointPos(ppsEcliptical, ppsEquatorial, ppsHorizontal);
    }

}