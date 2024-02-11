// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Core.Calc;


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


