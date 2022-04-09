// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.calc.util;
using E4C.domain.shared.references;
using System;

namespace E4C.calc.elements;

/// <summary>
/// Record with orbital elements for celestial points that are not calculated by the SE.
/// </summary>
public record OrbitDefinition
{
    public readonly double[] MeanAnomaly;
    public readonly double[] EccentricAnomaly;
    public readonly double SemiMajorAxis;
    public readonly double[] ArgumentPerihelion;
    public readonly double[] AscNode;
    public readonly double[] Inclination;

    public OrbitDefinition(double[] meanAnomaly, double[] eccentricAnomaly, double semiMajorAxis, double[] argumentPerihelion, double[] ascNode, double[] inclination)
    {
        MeanAnomaly = meanAnomaly;
        EccentricAnomaly = eccentricAnomaly;
        SemiMajorAxis = semiMajorAxis;
        ArgumentPerihelion = argumentPerihelion;
        AscNode = ascNode;
        Inclination = inclination;
    }
}






/// <summary>
/// Calculate heliocentric position for celestial points that are not supported by the SE.
/// </summary>
public interface ICalcHelioPos
{
    public RectAngCoordinates CalcEclipticPosition(double factorT, OrbitDefinition orbitDefinition);
}

/// <summary>
/// Calculate geocentric ecliptical position for celestial points that are not supported by the SE.
/// </summary>
public interface IPosSolSysPointsElementsCalc
{
    public double[] Calculate(SolarSystemPoints planet, double jdUt);
}



public class CalcHelioPos : ICalcHelioPos
{

    private double _meanAnomaly1;
    private double _meanAnomaly2;
    private double _semiAxis;
    private double _inclination;
    private double _eccentricity;
    private double _eccAnomaly;
    private double _factorT;
    private PolarCoordinates _trueAnomalyPol;
    private OrbitDefinition _orbitDefinition;

    public RectAngCoordinates CalcEclipticPosition(double factorT, OrbitDefinition orbitDefinition)
    {
        _factorT = factorT;
        _orbitDefinition = orbitDefinition;
        _meanAnomaly1 = MathExtra.DegToRad(ProcessTermsForFractionT(factorT, orbitDefinition.MeanAnomaly));
        if (_meanAnomaly1 < 0.0) _meanAnomaly1 += (Math.PI * 2);
        _eccentricity = ProcessTermsForFractionT(factorT, orbitDefinition.EccentricAnomaly);
        _eccAnomaly = EccAnomalyFromKeplerEquation(_meanAnomaly1, _eccentricity);
        _trueAnomalyPol = CalcPolarTrueAnomaly();
        ReduceToEcliptic();
        return CalcRectAngHelioCoordinates(_semiAxis, _inclination, _eccAnomaly, _eccentricity, _meanAnomaly2, orbitDefinition);
    }

    private double EccAnomalyFromKeplerEquation(double meanAnomaly, double eccentricity)
    {
        int count;
        double eccAnomaly = meanAnomaly;
        for (count = 1; count < 6; count++)
            eccAnomaly = meanAnomaly + eccentricity * Math.Sin(eccAnomaly);
        return eccAnomaly;
    }


    private PolarCoordinates CalcPolarTrueAnomaly()
    {
        double xCoord = _orbitDefinition.SemiMajorAxis * (Math.Cos(_eccAnomaly) - _eccentricity);
        double yCoord = _orbitDefinition.SemiMajorAxis * Math.Sin(_eccAnomaly) * Math.Sqrt(1 - _eccentricity * _eccentricity);
        double zCoord = 0.0;
        RectAngCoordinates anomalyVec = new(xCoord, yCoord, zCoord);
        return MathExtra.Rectangular2Polar(anomalyVec);
    }

    private RectAngCoordinates CalcRectAngHelioCoordinates(double semiAxis, double inclination, double eccAnomaly, double eccentricity, double meanAnomaly, OrbitDefinition orbitDefinition)
    {
        double phiCoord = MathExtra.DegToRad(semiAxis);
        if (phiCoord < 0.0) phiCoord += (Math.PI * 2);
        double thetaCoord = Math.Atan(Math.Sin(phiCoord - meanAnomaly) * Math.Tan(inclination));
        double rCoord = MathExtra.DegToRad(orbitDefinition.SemiMajorAxis) * (1 - eccentricity * Math.Cos(eccAnomaly));
        PolarCoordinates helioPol = new PolarCoordinates(phiCoord, thetaCoord, rCoord);
        return MathExtra.Polar2Rectangular(helioPol);
    }

    private void ReduceToEcliptic()
    {
        _semiAxis = MathExtra.RadToDeg(_trueAnomalyPol.PhiCoord) + ProcessTermsForFractionT(_factorT, _orbitDefinition.ArgumentPerihelion);
        _meanAnomaly2 = MathExtra.DegToRad(ProcessTermsForFractionT(_factorT, _orbitDefinition.AscNode));
        double factorVDeg = _semiAxis + MathExtra.RadToDeg(_meanAnomaly2);
        if (factorVDeg < 0.0) factorVDeg += 360.0;
        double factorVRad = MathExtra.DegToRad(factorVDeg);

        _inclination = MathExtra.DegToRad(ProcessTermsForFractionT(_factorT, _orbitDefinition.Inclination));
        _semiAxis = Math.Atan(Math.Cos(_inclination) * Math.Tan(factorVRad - _meanAnomaly2));
        if (_semiAxis < Math.PI) _semiAxis += Math.PI;
        _semiAxis = MathExtra.RadToDeg(_semiAxis + _meanAnomaly2);
        if (Math.Abs(factorVDeg - _semiAxis) > 10.0) _semiAxis -= 180.0;
    }

    private double ProcessTermsForFractionT(double fractionT, double[] elements)
    { 
        return elements[0] + elements[1] * fractionT + elements[2] * fractionT * fractionT; 
    }
}



public class PosSolSysPointsElementsCalc : IPosSolSysPointsElementsCalc
{

    private readonly ICalcHelioPos _calcHelioPos;

    public PosSolSysPointsElementsCalc(ICalcHelioPos calcHelioPos)
    {
        _calcHelioPos = calcHelioPos;
    }

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





