// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Util;
using Enigma.Domain.Positional;

namespace Enigma.Core.Calc.ObliqueLongitude;



/// <summary>Calculations for the south-point.</summary>
public interface ISouthPointCalculator
{
    /// <summary>Calculate longitude and latitude for the south-point.</summary>
    /// <param name="armc">Right ascension for the MC.</param>
    /// <param name="obliquity">Obliquity of the earths axis.</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <returns>An instance of EclipticCoodinates with values for longitude and latitude.</returns>
    public EclipticCoordinates CalculateSouthPoint(double armc, double obliquity, double geoLat);
}


public class SouthPointCalculator : ISouthPointCalculator
{
    public EclipticCoordinates CalculateSouthPoint(double armc, double obliquity, double geoLat)
    {
        double declSP = -(90.0 - geoLat);
        double arsp = armc;
        if (geoLat < 0.0)
        {
            arsp = RangeUtil.ValueToRange(armc + 180.0, 0.0, 360.0);
            declSP = -90.0 - geoLat;
        }

        double sinSP = Math.Sin(MathExtra.DegToRad(arsp));
        double cosEps = Math.Cos(MathExtra.DegToRad(obliquity));
        double tanDecl = Math.Tan(MathExtra.DegToRad(declSP));
        double sinEps = Math.Sin(MathExtra.DegToRad(obliquity));
        double cosArsp = Math.Cos(MathExtra.DegToRad(arsp));
        double sinDecl = Math.Sin(MathExtra.DegToRad(declSP));
        double cosDecl = Math.Cos(MathExtra.DegToRad(declSP));
        double longSP = RangeUtil.ValueToRange(MathExtra.RadToDeg(Math.Atan2((sinSP * cosEps) + (tanDecl * sinEps), cosArsp)), 0.0, 360.0);
        double latSP = MathExtra.RadToDeg(Math.Asin((sinDecl * cosEps) - (cosDecl * sinEps * sinSP)));
        return new EclipticCoordinates(longSP, latSP);
    }

}


