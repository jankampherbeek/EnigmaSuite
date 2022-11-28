// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Core.Work.Calc.Util;
using Enigma.Domain.AstronCalculations;

namespace Enigma.Core.Work.Calc.CelestialPoints.ObliqueLongitude;


/// <inheritdoc/>

public class SouthPointCalculator : ISouthPointCalculator
{
    /// <inheritdoc/>
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


