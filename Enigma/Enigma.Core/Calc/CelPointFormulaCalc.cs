// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.References;

namespace Enigma.Core.Calc;

/// <summary>Calculate geocentric ecliptical position for celestial points that are not supported by the SE,
/// using specific formulas.</summary>
public interface ICelPointFormulaCalc
{
    /// <param name="planet">A point that should be calculated with a specific formula.</param>
    /// <param name="jdUt">Julian day for UT.</param>
    /// <returns>Array with longitude, latitude and distance in that sequence.</returns>
    public double Calculate(ChartPoints planet, double jdUt);
}


public sealed class CelPointFormulaCalc: ICelPointFormulaCalc
{
    private const double JD1900 = 2415020.5;    // Julian day for 1900/1/1 0:00:00 UT
    private readonly ICelPointSeCalc _celPointSeCalc;

    public CelPointFormulaCalc(ICelPointSeCalc celPointSeCalc)
    {
        _celPointSeCalc = celPointSeCalc;
    }
    
    public double Calculate(ChartPoints planet, double jdUt)
    {
        return planet switch
        {
            ChartPoints.PersephoneCarteret => CalcCarteretHypPlanet(jdUt, 212.0, 1.0),
            ChartPoints.VulcanusCarteret => CalcCarteretHypPlanet(jdUt, 15.7, 0.55),
            ChartPoints.ApogeeCorrected => // should always be Duval
                CalcApogeeDuval(jdUt),
            _ => 0.0
        };
    }
    

    private static double CalcCarteretHypPlanet(double jdUt, double startPoint, double yearlySpeed)
    {
        return startPoint + ((jdUt - JD1900) * (yearlySpeed / EnigmaConstants.TROPICAL_YEAR_IN_DAYS));
    }
    
    private double CalcApogeeDuval(double jdUt)
    {
        const int flagsEcl = 2 + 256; // use SE + speed
        var longSun = _celPointSeCalc.CalculateCelPoint(ChartPoints.Sun.GetDetails().CalcId, jdUt, flagsEcl)[0].Position;
        var longApogeeMean = _celPointSeCalc.CalculateCelPoint(ChartPoints.ApogeeMean.GetDetails().CalcId,jdUt, flagsEcl)[0].Position;
        var diff = RangeUtil.ValueToRange(longSun - longApogeeMean, -180.0, 180.0);
        const double factor1 = 12.37;
        var sin2Diff = Math.Sin(MathExtra.DegToRad(2 * diff));
        var factor2 = Math.Sin(MathExtra.DegToRad((2 * (diff - 11.726 * sin2Diff))));
        var sin6Diff = Math.Sin(MathExtra.DegToRad(6 * diff));
        var factor3 = (8.8 / 60.0) * sin6Diff;
        var corrFactor = factor1 * factor2 + factor3;
        return RangeUtil.ValueToRange(longApogeeMean + corrFactor, 0.0, 360.0);
    }
}