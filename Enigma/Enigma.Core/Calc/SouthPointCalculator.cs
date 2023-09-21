// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Calc;


/// <inheritdoc/>

public sealed class SouthPointCalculator : ISouthPointCalculator
{
    /// <inheritdoc/>
    public EclipticCoordinates CalculateSouthPoint(double armc, double obliquity, double geoLat)
    {
        double declSp = -(90.0 - geoLat);
        double arsp = armc;
        if (geoLat < 0.0)
        {
            arsp = RangeUtil.ValueToRange(armc + 180.0, 0.0, 360.0);
            declSp = -90.0 - geoLat;
        }

        double sinSp = Math.Sin(MathExtra.DegToRad(arsp));
        double cosEps = Math.Cos(MathExtra.DegToRad(obliquity));
        double tanDecl = Math.Tan(MathExtra.DegToRad(declSp));
        double sinEps = Math.Sin(MathExtra.DegToRad(obliquity));
        double cosArsp = Math.Cos(MathExtra.DegToRad(arsp));
        double sinDecl = Math.Sin(MathExtra.DegToRad(declSp));
        double cosDecl = Math.Cos(MathExtra.DegToRad(declSp));
        double longSp = RangeUtil.ValueToRange(MathExtra.RadToDeg(Math.Atan2((sinSp * cosEps) + (tanDecl * sinEps), cosArsp)), 0.0, 360.0);
        double latSp = MathExtra.RadToDeg(Math.Asin((sinDecl * cosEps) - (cosDecl * sinEps * sinSp)));
        return new EclipticCoordinates(longSp, latSp);
    }

}


