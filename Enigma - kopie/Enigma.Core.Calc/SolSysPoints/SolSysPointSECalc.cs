// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Enigma.Core.Calc.SeFacades;
using Enigma.Domain.CalcVars;
using Enigma.Domain.Locational;
using Enigma.Domain.Positional;

namespace Enigma.Core.Calc.SolSysPoints;

/// <summary>Calculations for Solar System points.</summary>
public interface ISolSysPointSECalc
{
    /// <summary>Calculate a single Solar System point.</summary>
    /// <param name="solarSystemPoint">The Solar System point that will be calcualted.</param>
    /// <param name="jdnr">The Julian day number.</param>
    /// <param name="location">Location with coordinates.</param>
    /// <param name="flags">Flags that contain the settings for ecliptic or equatorial based calculations.</param>
    /// <returns>Array with position and speed for mainposition, deviation and distance, in that sequence. Typically: longitude, latitude, distance or right ascension, declination and distance.</returns>
    public PosSpeed[] CalculateSolSysPoint(SolarSystemPoints solarSystemPoint, double jdnr, Location location, int flags);
}


/// <inheritdoc/>
public class SolSysPointSECalc : ISolSysPointSECalc
{
    private readonly ICalcUtFacade _calcUtFacade;
    private readonly ISolarSystemPointSpecifications _solarSystemPointSpecifications;

    public SolSysPointSECalc(ICalcUtFacade calcUtFacade, ISolarSystemPointSpecifications solarSystemPointSpecifications)
    {
        _calcUtFacade = calcUtFacade;
        _solarSystemPointSpecifications = solarSystemPointSpecifications;
    }


    public PosSpeed[] CalculateSolSysPoint(SolarSystemPoints solarSystemPoint, double jdnr, Location location, int flags)
    {
        int pointId = _solarSystemPointSpecifications.DetailsForPoint(solarSystemPoint).SeId;
        double[] positions = _calcUtFacade.PosCelPointFromSe(jdnr, pointId, flags);
        var mainPos = new PosSpeed(positions[0], positions[3]);
        var deviation = new PosSpeed(positions[1], positions[4]);
        var distance = new PosSpeed(positions[2], positions[5]);
        return new PosSpeed[] { mainPos, deviation, distance };
    }

}