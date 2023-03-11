// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Domain.Points;

public class FullPointPosFactory: IFullPointPosFactory
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