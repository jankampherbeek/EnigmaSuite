// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

// The code in this file is based on an example in C++ by Ingmar de Boer.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Calc;


/// <summary>
/// Calculate geocentric ecliptical position for celestial points that are not supported by the CommonSE.
/// </summary>
public interface ICelPointsElementsCalc
{
    /// <param name="planet">Currently supported hypothetical planets: Persephone, Hermes, Demeter (School of Ram).</param>
    /// <param name="jdUt">Julian day for UT.</param>
    /// <param name="observerPosition">Geocentric/topcentric or heliocentric. No difference for topocentric because ofd the large distance of the planets involved.</param>
    /// <returns>Array with longitude, latitude and distance in that sequence.</returns>
    public double[] Calculate(ChartPoints planet, double jdUt, ObserverPositions observerPosition);
}

/// <inheritdoc/>
public sealed class CelPointsElementsCalc : ICelPointsElementsCalc
{

    private readonly ICalcHelioPos _calcHelioPos;

    public CelPointsElementsCalc(ICalcHelioPos calcHelioPos)
    {
        _calcHelioPos = calcHelioPos;
    }

    /// <inheritdoc/>
    public double[] Calculate(ChartPoints planet, double jdUt, ObserverPositions observerPosition)
    {
        double centuryFractionT = FactorT(jdUt);
        if (observerPosition is ObserverPositions.GeoCentric or ObserverPositions.TopoCentric)  // no difference for plantes with a large distance.
        {
            PolarCoordinates polarPlanetGeo = CalcGeoPolarCoord(planet, centuryFractionT);
            return DefinePosition(polarPlanetGeo);
        }
        PolarCoordinates polarPlanetHelio = CalcHelioPolarCoord(planet, centuryFractionT);
        return DefinePosition(polarPlanetHelio);
    }


    private PolarCoordinates CalcGeoPolarCoord(ChartPoints planet, double centuryFractionT)
    {
        RectAngCoordinates rectAngEarthHelio = _calcHelioPos.CalcEclipticPosition(centuryFractionT, DefineOrbitDefinition(ChartPoints.Earth));
        RectAngCoordinates rectAngPlanetHelio = _calcHelioPos.CalcEclipticPosition(centuryFractionT, DefineOrbitDefinition(planet));
        RectAngCoordinates rectAngPlanetGeo = new(rectAngPlanetHelio.XCoord - rectAngEarthHelio.XCoord, rectAngPlanetHelio.YCoord - rectAngEarthHelio.YCoord, rectAngPlanetHelio.ZCoord - rectAngEarthHelio.ZCoord);
        return MathExtra.Rectangular2Polar(rectAngPlanetGeo);
    }

    private PolarCoordinates CalcHelioPolarCoord(ChartPoints planet, double centuryFractionT)
    {
        RectAngCoordinates rectAngPlanetHelio = _calcHelioPos.CalcEclipticPosition(centuryFractionT, DefineOrbitDefinition(planet));
        return MathExtra.Rectangular2Polar(rectAngPlanetHelio);
    }


    private static double[] DefinePosition(PolarCoordinates polarPlanetGeo)
    {
        double posLong = MathExtra.RadToDeg(polarPlanetGeo.PhiCoord);
        if (posLong < 0.0) posLong += 360.0;
        double posLat = MathExtra.RadToDeg(polarPlanetGeo.ThetaCoord);
        double posDist = MathExtra.RadToDeg(polarPlanetGeo.RCoord);
        return new[] { posLong, posLat, posDist };
    }

    private static double FactorT(double jdUt)
    {
        return (jdUt - 2415020.5) / 36525;
    }

    private static OrbitDefinition DefineOrbitDefinition(ChartPoints planet)
    {
        double[] meanAnomaly;
        double[] eccentricAnomaly = { 0, 0, 0 };
        double[] argumentPerihelion = { 0, 0, 0 };
        double[] ascNode = { 0, 0, 0 };
        double[] inclination = { 0, 0, 0 };
        double semiMajorAxis;
        switch (planet)
        {
            case ChartPoints.Earth:
                meanAnomaly = new[] { 358.47584, 35999.0498, -.00015 };
                eccentricAnomaly = new[] { .016751, -.41e-4, 0 };
                semiMajorAxis = 1.00000013;
                argumentPerihelion = new[] { 101.22083, 1.71918, .00045 };
                break;
            case ChartPoints.PersephoneRam:
                meanAnomaly = new[] { 295.0, 60, 0 };
                semiMajorAxis = 71.137866;
                break;
            case ChartPoints.HermesRam:
                meanAnomaly = new[] { 134.7, 50.0, 0 };
                semiMajorAxis = 80.331954;
                break;
            case ChartPoints.DemeterRam:
                meanAnomaly = new[] { 114.6, 40, 0 };
                semiMajorAxis = 93.216975;
                ascNode = new double[] { 125, 0, 0 };
                inclination = new[] { 5.5, 0, 0 };
                break;
            default:
                throw new ArgumentException("Unrecognized planet for OrbitDefinition: {0}", planet.ToString());
        }
        return new OrbitDefinition(meanAnomaly, eccentricAnomaly, semiMajorAxis, argumentPerihelion, ascNode, inclination);
    }

}





