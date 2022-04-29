// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.
// The code in this file is based on an example in C++ by Ingmar de Boer.

using E4C.Core.Domain;
using E4C.Core.Util;
using E4C.Shared.References;
using System;

namespace E4C.Core.Astron.SolSysPoints;


/// <summary>
/// Calculate geocentric ecliptical position for celestial points that are not supported by the SE.
/// </summary>
public interface ISolSysPointsElementsCalc
{
    /// <param name="planet">Currently supported hypothetical planets: Persephone, Hermes, Demeter (School of Ram).</param>
    /// <param name="jdUt">Julian day for UT.</param>
    /// <returns>Array with longitude, latitude and distance in that sequence.</returns>
    public double[] Calculate(SolarSystemPoints planet, double jdUt);
}


/// <inheritdoc/>
public class SolSysPointsElementsCalc : ISolSysPointsElementsCalc
{

    private readonly ICalcHelioPos _calcHelioPos;

    public SolSysPointsElementsCalc(ICalcHelioPos calcHelioPos)
    {
        _calcHelioPos = calcHelioPos;
    }

    /// <inheritdoc/>
    public double[] Calculate(SolarSystemPoints planet, double jdUt)
    {
        double centuryFractionT = FactorT(jdUt);
        PolarCoordinates polarPlanetGeo = CalcGeoPolarCoord(planet, centuryFractionT);
        return DefinePosition(polarPlanetGeo);
    }


    private PolarCoordinates CalcGeoPolarCoord(SolarSystemPoints planet, double centuryFractionT)
    {
        RectAngCoordinates rectAngEarthHelio = _calcHelioPos.CalcEclipticPosition(centuryFractionT, DefineOrbitDefinition(SolarSystemPoints.Earth));
        RectAngCoordinates rectAngPlanetHelio = _calcHelioPos.CalcEclipticPosition(centuryFractionT, DefineOrbitDefinition(planet));
        RectAngCoordinates rectAngPlanetGeo = new RectAngCoordinates(rectAngPlanetHelio.XCoord - rectAngEarthHelio.XCoord, rectAngPlanetHelio.YCoord - rectAngEarthHelio.YCoord, rectAngPlanetHelio.ZCoord - rectAngEarthHelio.ZCoord);
        return MathExtra.Rectangular2Polar(rectAngPlanetGeo);
    }


    private double[] DefinePosition(PolarCoordinates polarPlanetGeo)
    {
        double posLong = MathExtra.RadToDeg(polarPlanetGeo.PhiCoord);
        if (posLong < 0.0) posLong += 360.0;
        double posLat = MathExtra.RadToDeg(polarPlanetGeo.ThetaCoord);
        double posDist = MathExtra.RadToDeg(polarPlanetGeo.RCoord);
        return new double[] { posLong, posLat, posDist };
    }

    private double FactorT(double jdUt)
    {
        return (jdUt - 2415020.5) / 36525;
    }

    private OrbitDefinition DefineOrbitDefinition(SolarSystemPoints planet)
    {
        double[] meanAnomaly;
        double[] eccentricAnomaly = new double[] { 0, 0, 0 };
        double[] argumentPerihelion = new double[] { 0, 0, 0 };
        double[] ascNode = new double[] { 0, 0, 0 };
        double[] inclination = new double[] { 0, 0, 0 };
        double semiMajorAxis = 0.0;
        if (planet == SolarSystemPoints.Earth)
        {
            meanAnomaly = new double[] { 358.47584, 35999.0498, -.00015 };
            eccentricAnomaly = new double[] { .016751, -.41e-4, 0 };
            semiMajorAxis = 1.00000013;
            argumentPerihelion = new double[] { 101.22083, 1.71918, .00045 };
        }
        else if (planet == SolarSystemPoints.PersephoneRam)
        {
            meanAnomaly = new double[] { 295.0, 60, 0 };
            semiMajorAxis = 71.137866;
        }
        else if (planet == SolarSystemPoints.HermesRam)
        {
            meanAnomaly = new double[] { 134.7, 50.0, 0 };
            semiMajorAxis = 80.331954;
        }
        else if (planet == SolarSystemPoints.DemeterRam)
        {
            meanAnomaly = new double[] { 114.6, 40, 0 };
            semiMajorAxis = 93.216975;
            ascNode = new double[] { 125, 0, 0 };
            inclination = new double[] { 5.5, 0, 0 };
        }
        else throw new ArgumentException($"Unrecognized planet for OrbitDefinition: {0}", planet.ToString());
        return new OrbitDefinition(meanAnomaly, eccentricAnomaly, semiMajorAxis, argumentPerihelion, ascNode, inclination);
    }

}





