// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.
// The code in this file is based on an example in C++ by Ingmar de Boer.

using Enigma.Core.Calc.CalcDomain;
using Enigma.Core.Calc.Util;

namespace Enigma.Core.Calc.SolSysPoints;


/// <summary>
/// Calculate heliocentric rectangular positions for celestial points that are not supported by the SE.
/// </summary>
public interface ICalcHelioPos
{
    /// <param name="factorT">Fraction of century, mostly simply called 'T'.</param>
    /// <param name="orbitDefinition">Orbital elements to calculate the position.</param>
    /// <returns>Calculated rectangualr coördinates.</returns>
    public RectAngCoordinates CalcEclipticPosition(double factorT, OrbitDefinition orbitDefinition);
}


/// <inheritdoc/>
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

    /// <inheritdoc/>
    public RectAngCoordinates CalcEclipticPosition(double factorT, OrbitDefinition orbitDefinition)
    {
        _factorT = factorT;
        _orbitDefinition = orbitDefinition;
        _meanAnomaly1 = MathExtra.DegToRad(ProcessTermsForFractionT(factorT, orbitDefinition.MeanAnomaly));
        if (_meanAnomaly1 < 0.0) _meanAnomaly1 += (Math.PI * 2);
        _eccentricity = ProcessTermsForFractionT(factorT, orbitDefinition.Eccentricity);
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
        PolarCoordinates helioPol = new(phiCoord, thetaCoord, rCoord);
        return MathExtra.Polar2Rectangular(helioPol);
    }

    private void ReduceToEcliptic()
    {
        _semiAxis = MathExtra.RadToDeg(_trueAnomalyPol.PhiCoord) + ProcessTermsForFractionT(_factorT, _orbitDefinition.ArgumentPerihelion);
        _meanAnomaly2 = MathExtra.DegToRad(ProcessTermsForFractionT(_factorT, _orbitDefinition.AscendingNode));
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
