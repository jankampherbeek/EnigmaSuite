// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;

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
        double posDiff = RangeUtil.ValueToRange(posPoint - posMc, 0.0, 360.0);
        return posDiff < 180.0;
    }


    /// <summary>Checks if a point is in the upper hemisphere.</summary>
    /// <param name="posPoint">Position of the point to check, in Right Ascension for directions in mundo,
    /// in longitude for directions in zodiaco.</param>
    /// <param name="posAsc">Positions of the ascendant, in Right Ascension for directions in mundo,
    /// in longitude for directions in zodiaco</param>
    /// <returns>True if the point is in the upper hemisphere, otherwise false.</returns>
    public static bool IsChartTop(double posPoint, double posAsc)
    {
        double posDiff = RangeUtil.ValueToRange(posPoint - posAsc, 0.0, 360.0);
        return posDiff > 180.0;
    }

    /// <summary>Calculate Meridian distance for a point. Compares with raMC if the point is in the top part of the chart,
    /// otherwise compares with the raIC.</summary>
    /// <param name="raPoint">Right Ascension of the point.</param>
    /// <param name="raMc">Right Ascension of the MC.</param>
    /// <param name="raIc">Right Ascension of the IC.</param>
    /// <param name="isTop">True if point is in upper half of the chart.</param>
    /// <returns>Meridian distance.</returns>
    public static double MeridianDistance(double raPoint, double raMc, double raIc, bool isTop)
    {
        double raMcIc = isTop ? raMc : raIc;
        double shortArc = RangeUtil.ValueToRange(Math.Abs(raPoint - raMcIc), 0.0, 360.0);
        if (shortArc >= 180.0) shortArc = Math.Abs(360.0 - shortArc);
        return shortArc;
    }
    

    /// <summary>Calculates horizontal distance for a point.</summary>
    /// <param name="oaPoint">Oblique ascension for the point.</param>
    /// <param name="oaAsc">Oblique ascension for the ascendant.</param>
    /// <param name="chartLeft">True if point is in the left demi-circle of the chart, otherwise false.</param>
    /// <param name="north">True for northern latitude.</param>
    /// <returns>Calculated value for horizontal distance.</returns>
    public static double HorizontalDistance(double oaPoint, double oaAsc, bool chartLeft, bool north)
    {
        double hd = 0.0;
        if (north)
        {
            if (chartLeft)
            {
                hd = Math.Abs(oaPoint - oaAsc);
            }
            else
            {
                hd = Math.Abs(RangeUtil.ValueToRange(oaPoint + 180.0, 0.0, 180.0) 
                              - RangeUtil.ValueToRange(oaAsc + 180.0, 0.0, 180.0));            
            }            
        }
        else
        {
            if (!chartLeft)
            {
                hd = Math.Abs(oaPoint - oaAsc);
            }
            else
            {
                hd = Math.Abs(RangeUtil.ValueToRange(oaPoint + 180.0, 0.0, 180.0) 
                              - RangeUtil.ValueToRange(oaAsc + 180.0, 0.0, 180.0));            
            } 
        }
        if (hd >= 180.0) hd = 360.0 - hd;
        if (hd >= 90.0) hd = 180.0 - hd;
        return hd;
    }

    /// <summary>Calculate ascensional difference (AD). Uses the formula AD = arcsin(tan(decl) * tan(geoLat).</summary>
    /// <param name="decl">Declination.</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <returns>Calculated ascensional difference.</returns>
    public static double AscensionalDifference(double decl, double geoLat)
    {
        double declRad = MathExtra.DegToRad(decl);
        double latRad = MathExtra.DegToRad(geoLat);
        double x = Math.Tan(declRad) * Math.Tan(latRad);
        if (Math.Abs(x) > 1.0)
            return 0.0; // Checked this in the speculum of Morinus, this can take effect if a planet is severely OOB. 
        double adRad = Math.Asin(Math.Tan(declRad) * Math.Tan(latRad));
        return MathExtra.RadToDeg(adRad);
    }

    /// <summary>Calculate oblique ascension (or descension).</summary>
    /// <param name="raPoint">Right ascention of point.</param>
    /// <param name="ascDiff">Ascension difference of point.</param>
    /// <param name="chartLeft">Indicates if point is in the left part of the chart.</param>
    /// <param name="north">Indicates if point is in northern hemisphere.</param>
    /// <returns>Calculated oblique ascension.</returns>
    public static double ObliqueAscDesc(double raPoint, double ascDiff, bool chartLeft, bool north)
    {
        double oa = 0.0;
        if (north)
        {
            if (chartLeft) oa = raPoint - ascDiff;
            else  oa = raPoint + ascDiff;           
        }
        else
        {
            if (chartLeft) oa = raPoint + ascDiff;
            else oa = raPoint - ascDiff;            
        }
        return RangeUtil.ValueToRange(oa, 0.0, 360.0); 
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


    /// <summary>AD promissor under elevated pole of significator.</summary>
    /// <param name="elevPoleSign">Elevated pole significator.</param>
    /// <param name="declProm">Declination promissor.</param>
    /// <returns>The calculated AD in degrees.</returns>
    public static double AdPromUnderElevPoleSign(double elevPoleSign, double declProm)
    {
        double signElevPoleRad = MathExtra.DegToRad(elevPoleSign);
        double promDeclRad = MathExtra.DegToRad(declProm);
        double result = Math.Asin(Math.Tan(promDeclRad) * Math.Tan(signElevPoleRad));
        return MathExtra.RadToDeg(result);
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


   
    public static double DeclFromLongNoLat(double longitude, double obliquity)
    {
        double longRad = MathExtra.DegToRad(longitude);
        double oblRad = MathExtra.DegToRad(obliquity);
        double decl = MathExtra.RadToDeg(Math.Asin(Math.Sin(oblRad) * Math.Sin(longRad)));
        return decl;
    }

    public static double RightAscFromLongNoLat(double longitude, double obliquity)
    {
        double longRad = MathExtra.DegToRad(longitude);
        double oblRad = MathExtra.DegToRad(obliquity);
        double ra = RangeUtil.ValueToRange(MathExtra.RadToDeg(Math.Atan2(Math.Sin(longRad) * Math.Cos(oblRad), Math.Cos(longRad))), 0.0, 360.0);
        return ra;

    }


    public static double ZenithDistReg(double decl, double merDist, double geoLat, bool isTop)
    {
        double declRad = MathExtra.DegToRad(decl);
        double mdRad = MathExtra.DegToRad(merDist);
        double glRad = MathExtra.DegToRad(geoLat);
        if (Math.Abs(merDist - 90.0) < 0.000001) return 90 - MathExtra.RadToDeg(Math.Atan(Math.Sin(Math.Abs(glRad)) * Math.Tan(declRad)));
        double a = MathExtra.RadToDeg(Math.Atan(Math.Cos(glRad) * Math.Tan(mdRad)));
        double b = MathExtra.RadToDeg(Math.Atan(Math.Tan(Math.Abs(glRad)) * Math.Cos(mdRad)));
        double c = 0.0;
        if ((decl >= 0.0 && geoLat >= 0.0) || (decl < 0.0 && geoLat < 0.0))    // decl and geoLat same sign
        {
            if (isTop) c = b - Math.Abs(decl);
            else c = b + Math.Abs(decl);
        }
        else      // decl and geoLat of different sign
        {
            if (isTop) c = b + Math.Abs(decl);
            else c = b - Math.Abs(decl);
        }

        double cRad = MathExtra.DegToRad(c);
        double f = 0.0;
        if (Math.Abs(b - decl) < 0.0000001) c = 0;
        else f = MathExtra.RadToDeg(Math.Atan(Math.Sin(Math.Abs(glRad)) * Math.Sin(mdRad) * Math.Tan(cRad)));
        double zd = a + f;
        if (zd < 0.0) zd += 180.0;
        return zd;
    }

   
    
}