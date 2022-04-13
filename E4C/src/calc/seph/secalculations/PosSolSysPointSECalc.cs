// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.calc.seph.sefacade;
using E4C.core.facades;
using E4C.domain.shared.positions;
using E4C.domain.shared.references;
using E4C.domain.shared.specifications;
using E4C.Models.Domain;

namespace E4C.calc.seph.secalculations;

/// <summary>
/// Calculations for Solar System points.
/// </summary>
public interface IPositionSolSysPointSECalc
{
    /// <summary>
    /// Calculate a single Solar System point.
    /// </summary>
    /// <param name="solarSystemPoint">The Solar System point that will be calcualted.</param>
    /// <param name="jdnr">The Julian day number.</param>
    /// <param name="location">Location with coordinates.</param>
    /// <param name="flagsEcliptical">Flags that contain the settings for ecliptic based calculations.</param>
    /// <param name="flagsEquatorial">Flags that contain the settings for equatorial based calculations.</param>
    /// <returns>Instance of CalculatedFullSolSysPointPosition.</returns>
    public FullSolSysPointPos CalculateSolSysPoint(SolarSystemPoints solarSystemPoint, double jdnr, Location location, int flagsEcliptical, int flagsEquatorial);
}


public class PositionSolSysPointSECalc : IPositionSolSysPointSECalc
{
    private readonly ICalcUtFacade _posCelPointFacade;
    private readonly IHorizontalCoordinatesFacade _horizontalCoordinatesFacade;
    private readonly ISolarSystemPointSpecifications _solarSystemPointSpecifications;

    public PositionSolSysPointSECalc(ICalcUtFacade posCelPointFacade, IHorizontalCoordinatesFacade horizontalCoordinatesFacade,
        ISolarSystemPointSpecifications solarSystemPointSpecifications)
    {
        _posCelPointFacade = posCelPointFacade;
        _horizontalCoordinatesFacade = horizontalCoordinatesFacade;
        _solarSystemPointSpecifications = solarSystemPointSpecifications;
    }


    public FullSolSysPointPos CalculateSolSysPoint(SolarSystemPoints solarSystemPoint, double jdnr, Location location, int flagsEcliptical, int flagsEquatorial)
    {

        // todo handle actions for sidereal and/or topocentric 
        // todo define flags
        double heightAboveSeaLevel = 0.0;
        int pointId = _solarSystemPointSpecifications.DetailsForPoint(solarSystemPoint).SeId;
        double[] _fullEclipticPositions = _posCelPointFacade.PosCelPointFromSe(jdnr, pointId, flagsEcliptical);
        double[] _fullEquatorialPositions = _posCelPointFacade.PosCelPointFromSe(jdnr, pointId, flagsEquatorial);
        var _eclCoordinatesForHorCalculation = new double[] { _fullEclipticPositions[0], _fullEclipticPositions[1], _fullEclipticPositions[2] };
        var _geoGraphicCoordinates = new double[] { location.GeoLong, location.GeoLat, heightAboveSeaLevel };
        HorizontalPos _horizontalPos = _horizontalCoordinatesFacade.CalculateHorizontalCoordinates(jdnr, _geoGraphicCoordinates, _eclCoordinatesForHorCalculation, flagsEcliptical);
        var _longitude = new PosSpeed(_fullEclipticPositions[0], _fullEclipticPositions[3]);
        var _latitude = new PosSpeed(_fullEclipticPositions[1], _fullEclipticPositions[4]);
        var _distance = new PosSpeed(_fullEclipticPositions[2], _fullEclipticPositions[5]);
        var _rightAscension = new PosSpeed(_fullEquatorialPositions[0], _fullEquatorialPositions[3]);
        var _declination = new PosSpeed(_fullEquatorialPositions[1], _fullEquatorialPositions[4]);
        return new FullSolSysPointPos(solarSystemPoint, _longitude, _latitude, _rightAscension, _declination, _distance, _horizontalPos);
    }

}