// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;

namespace Enigma.Core.Slices.VenusStarPoint;

/// <summary>
/// Calculates the estimated date of a conjunction between Sun and Venus.
/// </summary>
/// <remarks>
/// Based on a frmula by Jean Meeus, Astronomical algorithms, p. 250/251
/// </remarks>
public class EstimatedConjunctionDate
{

    public static double CalcEstimatedConjunctionDate(double yearFraction, VenusPhenomena phenomenon)
    {
        var a = phenomenon == VenusPhenomena.InferiorConjunction 
            ? VenusData.VenusInferiorConjunctionFactorA 
            : VenusData.VenusSuperiorConjunctionFactorA;
        var b = phenomenon == VenusPhenomena.InferiorConjunction
            ? VenusData.VenusInferiorConjunctionFactorB
            : VenusData.VenusSuperiorConjunctionFactorB;
        var m0 = phenomenon == VenusPhenomena.InferiorConjunction
            ? VenusData.VenusInferiorConjunctionFactorM0
            : VenusData.VenusSuperiorConjunctionFactorM0;
        var m1 = phenomenon == VenusPhenomena.InferiorConjunction
            ? VenusData.VenusInferiorConjunctionFactorM1
            : VenusData.VenusSuperiorConjunctionFactorM1;
        var k = Math.Round((365.2425 * yearFraction + 1721_060 - a) / b);
        var jde0 = a + k * b;
        var m = m0 + k * m1;
        var t = (jde0 - 2451_545.0) / 36_525.0;
        var correction = phenomenon == VenusPhenomena.InferiorConjunction 
            ? JdCorrectionInferior(t, m) 
            : JdCorrectionSuperior(t, m);
        return jde0 + correction;

    }

    private static double JdCorrectionInferior(double t, double m)
    {
        var mRad = MathExtra.DegToRad(m);
        
        var correction = -0.0096 + 0.0002 * t - 0.00001 * t * t;
        correction += (2.0009 - 0.0033 * t - 0.00001 * t * t) * Math.Sin(mRad);
        correction += (0.5980 - 0.0104 * t + 0.00001 * t * t) * Math.Cos(mRad);
        correction += (0.0967 - 0.0018 * t - 0.00003 * t * t) * Math.Sin(mRad * 2.0);
        correction += (0.0913 + 0.0009 * t - 0.00002 * t * t) * Math.Cos(mRad * 2.0);
        correction += (0.0046 - 0.0002 * t) * Math.Sin(mRad * 3.0);
        correction += (0.0079 + 0.0002 * t) * Math.Cos(mRad * 3.0);
        return correction;
    }

    private static double JdCorrectionSuperior(double t, double m)
    {
        var mRad = MathExtra.DegToRad(m);
        var correction = 0.0099 - 0.0002 * t - 0.00001 * t * t;
        correction += (4.1991 - 0.0121 * t - 0.00003 * t * t) * Math.Sin(mRad);
        correction += (-0.6095 + 0.0102 * t - 0.00002 * t * t) * Math.Cos(mRad);
        correction += (0.25 - 0.0028 * t - 0.00003 * t * t) * Math.Sin(mRad * 2.0);
        correction += (0.0063 - 0.0025 * t - 0.00002 * t * t) * Math.Cos(mRad * 2.0);
        correction += (0.0232 - 0.0005 * t - 0.00001 * t * t) * Math.Sin(mRad * 3.0);
        correction += (0.0031 + 0.0004 * t) * Math.Cos(mRad * 3.0);
        return correction;

    }
    
    
}