// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;

namespace Enigma.Core.Charts.Prog.PrimDir;

/// <summary>Mathematical and other static calculations for primary directions.</summary>
public static class PrimDirCalcAssist
{
    
    
    /// <summary>Checks if a point is in the eastern hemisphere.</summary>
    /// <param name="posPoint">Position of the point to check, in Right Ascension for directions in mundo,
    /// in longitude for directions in zodiaco.</param>
    /// <param name="posMc">Positions of the MC, in Right Ascension for directions in mundo,
    /// in longitude for directions in zodiaco</param>
    /// <returns>True if the point is in the eastern hemisphere, otherwise false.</returns>
    public static bool IsChartLeft(double posPoint, double posMc)
    {
        double posDiff = posPoint - posMc;
        if (posDiff < 0.0) posDiff += 360.0;
        return posDiff < 180.0;
    }


    /// <summary>Calculates horizontal distance for a point.</summary>
    /// <param name="oaPoint">Oblique ascension for the point.</param>
    /// <param name="oaAsc">Oblique ascension for the ascendant.</param>
    /// <param name="chartLeft">True if point is in the left demi-circle of the chart, otherwise false.</param>
    /// <returns>Calculated value for horizontal distance.</returns>
    public static double HorizontalDistance(double oaPoint, double oaAsc, bool chartLeft)
    {
        if (chartLeft) {
            return oaPoint - oaAsc;
        }
        return RangeUtil.ValueToRange(oaPoint + 180.0, 0.0, 180.0) 
               - RangeUtil.ValueToRange(oaAsc + 180.0, 0.0, 180.0);
    }

    /// <summary>Calculate ascensional difference (AD). Uses the formula AD = arcsin(tan(decl) * tan(geoLat).</summary>
    /// <param name="decl">Declination.</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <returns>Calculated ascensional difference.</returns>
    public static double AscensionalDifference(double decl, double geoLat)
    {
        double declRad = MathExtra.DegToRad(decl);
        double latRad = MathExtra.DegToRad(geoLat);
        double adRad = Math.Asin(Math.Tan(declRad) * Math.Tan(latRad));
        return MathExtra.RadToDeg(adRad);
    }

    /// <summary>Calculate oblique ascension (or descension).</summary>
    /// <param name="raPoint">Right ascention of point.</param>
    /// <param name="ascDiff">Ascension difference of point.</param>
    /// <param name="chartLeft">Indicates if point is in the left part of the chart.</param>
    /// <param name="north">Indficates if point is in northern hemisphere.</param>
    /// <returns>Calculated oblique ascension.</returns>
    public static double ObliqueAscdesc(double raPoint, double ascDiff, bool chartLeft, bool north)
    {
        if ((north && chartLeft) || (!north && !chartLeft))
        {
            return RangeUtil.ValueToRange(raPoint - ascDiff, 0.0, 360.0);
        } 
        return RangeUtil.ValueToRange(raPoint + ascDiff, 0.0, 360.0);
        
    }

    /// <summary>Calculates the elevation undere the pole for directions using the method Placidus under the pole.</summary>
    /// <param name="adPole">Ascensional difference under the pole.</param>
    /// <param name="decl">Declination.</param>
    /// <returns>Calculated value for evaluation of the pool.</returns>
    public static double ElevationOfThePolePlac(double adPole, double decl)
    {
        double adPoleRad = MathExtra.DegToRad(adPole);
        double declRad = MathExtra.DegToRad(decl);
        double elevRad = Math.Atan(Math.Sin(adPoleRad) / Math.Tan(declRad));
        return MathExtra.RadToDeg(elevRad);
    }


    /// <summary>Pole for a specific point, using Regiomontanus directions.</summary>
    /// <param name="decl">Declination.</param>
    /// <param name="mdUpper">Upper meridian distance.</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <returns>The calculated pole.</returns>
    public static double PoleRegiomontanus(double decl, double mdUpper, double geoLat)
    {
        // Based on formulas by Martin Gansten, Primary Directions, p. 156
        // AngleX, angleY and angleZ are auxiliary angles that hold intermediate values.
        double md = Math.Abs(mdUpper);
        double angleX = MathExtra.RadToDeg(Math.Atan( Math.Tan(MathExtra.DegToRad(decl))/ 
            Math.Cos(MathExtra.DegToRad(mdUpper))));
        double angleY = geoLat - angleX;
        double cosY = Math.Cos(MathExtra.DegToRad(angleY));
        double tanMd = Math.Tan(MathExtra.DegToRad(md));
        double cosX = Math.Cos(MathExtra.DegToRad(angleX));
        double angleZ = MathExtra.RadToDeg(Math.Atan(cosY / (tanMd * cosX)));
        double sinGeoLat = Math.Sin(MathExtra.DegToRad(geoLat));
        double cosZ = Math.Cos(MathExtra.DegToRad(angleZ));
        return MathExtra.RadToDeg(Math.Asin(sinGeoLat * cosZ));
    }



    public static double AdUnderRegPole(double regPole, double decl)
    {
        double tanPole = Math.Tan(MathExtra.DegToRad(regPole));
        double tanDecl = Math.Tan(MathExtra.DegToRad(decl));
        return MathExtra.RadToDeg(Math.Asin(tanDecl * tanPole));
    }
}