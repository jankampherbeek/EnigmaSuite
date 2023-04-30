// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.CelestialPoints.Helpers;
using Serilog;

namespace Enigma.Core.Handlers.Calc.Util;


/// <summary>Representation of rectangular coordinates.</summary>
public record RectAngCoordinates(double XCoord, double YCoord, double ZCoord);


/// <summary>Representation of polar coordinates.</summary>
public record PolarCoordinates(double PhiCoord, double ThetaCoord, double RCoord);



/// <summary>Several mathematical functions that are not covered by the standard Math library.</summary>
/// <remarks>All functions are static.</remarks>
public static class MathExtra
{

    /// <summary>Convert radians to degrees.</summary>
    public static double RadToDeg(double radians)
    {
        return 180 / Math.PI * radians;
    }

    /// <summary>Converts degrees to radians.</summary>
    public static double DegToRad(double degrees)
    {
        return Math.PI / 180 * degrees;
    }

    /// <summary>Convert array with rectangular coordinates to array with polar coordinates.</summary>
    /// <param name="rectangularValues">Array with values for the rectangular coordinates,  respectively x, y and z.</param>
    /// <returns>Array with values for the polar coordinates, respectively phi, theta and r.</returns>
    /// <exception cref="ArgumentException">Thrown if rectangularValues is null or contains more or less than 3 values.</exception>
    public static double[] Rectangular2Polar(double[] rectangularValues)
    {
        if ((rectangularValues == null) || (rectangularValues.Length != 3))
        {
            string errorText = "MathExtra.Rectangular2Polar(): Invalud input for rectangularValues: " + rectangularValues;
            Log.Error(errorText);
            throw new ArgumentException(errorText);
        }
        double x = rectangularValues[0];
        double y = rectangularValues[1];
        double z = rectangularValues[2];
        double r = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        if (r == 0) r = double.MinValue;
        if (x == 0) x = double.MinValue;
        double phi = Math.Atan2(y, x);
        double theta = Math.Asin(z / r);

        return new double[] { phi, theta, r };
    }

    /// <summary>Convert record with rectangular coordinates to record with polar coordinates.</summary>
    /// <param name="rectAngCoordinates">Record with rectangular coordinates.</param>
    /// <returns>Record with polar coordinates.</returns>
    public static PolarCoordinates Rectangular2Polar(RectAngCoordinates rectAngCoordinates)
    {
        double xCoord = rectAngCoordinates.XCoord;
        double yCoord = rectAngCoordinates.YCoord;
        double zCoord = rectAngCoordinates.ZCoord;
        double polarRCoord = Math.Sqrt(Math.Pow(xCoord, 2) + Math.Pow(yCoord, 2) + Math.Pow(zCoord, 2));
        if (polarRCoord == 0) polarRCoord = double.MinValue;
        if (xCoord == 0) xCoord = double.MinValue;
        double polarPhiCoord = Math.Atan2(yCoord, xCoord);
        double polarThetaCoord = Math.Asin(zCoord / polarRCoord);
        return new PolarCoordinates(polarPhiCoord, polarThetaCoord, polarRCoord);
    }

    /// <summary>Convert array with polar coordinates to array with rectangular coordinates.</summary>
    /// <param name="polarValues">Polar coordinates, respectively phi, theta and r.</param>
    /// <exception cref="ArgumentException">Thrown if polarvalues is null or contains more or less than 3 values.</exception>
    /// <returns>Rectangular coordinates, respectively x, y and z.</returns>
    public static double[] Polar2Rectangular(double[] polarValues)
    {
        if ((polarValues == null) || (polarValues.Length != 3))
        {
            string errorText = "MathExtra.Polar2Rectangular(): Invalud input for polar values: " + polarValues;
            Log.Error(errorText);
            throw new ArgumentException(errorText);
        }
        double phi = polarValues[0];
        double theta = polarValues[1];
        double r = polarValues[2];
        double x = r * Math.Cos(theta) * Math.Cos(phi);
        double y = r * Math.Cos(theta) * Math.Sin(phi);
        double z = r * Math.Sin(theta);

        return new double[] { x, y, z };

    }


    /// <summary>Convert record with polar coordinates to record with rectangular coordinates.</summary>
    /// <param name="polarCoordinates">The polar coordinates.</param>
    /// <returns>The rectangular coordinates.</returns>
    public static RectAngCoordinates Polar2Rectangular(PolarCoordinates polarCoordinates)
    {
        double phiCoord = polarCoordinates.PhiCoord;
        double thetaCoord = polarCoordinates.ThetaCoord;
        double rCoord = polarCoordinates.RCoord;
        double rectAngXCoord = rCoord * Math.Cos(thetaCoord) * Math.Cos(phiCoord);
        double rectAngYCoord = rCoord * Math.Cos(thetaCoord) * Math.Sin(phiCoord);
        double rectAngZCoord = rCoord * Math.Sin(thetaCoord);
        return new RectAngCoordinates(rectAngXCoord, rectAngYCoord, rectAngZCoord);
    }


    /// <summary>Calculate ascensional difference (AD). Uses the formula AD = arcsin(tan(decl) * tan(geoLat).</summary>
    /// <param name="decl">Declination.</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <returns>Calculated ascensional difference.</returns>
    public static double AscensionalDifference(double decl, double geoLat)
    {
        double declRad = DegToRad(decl);
        double latRad = DegToRad(geoLat);
        double adRad = Math.Asin(Math.Tan(declRad) * Math.Tan(latRad));
        return RadToDeg(adRad);
    }

    /// <summary>Calculate oblique ascension.</summary>
    /// <param name="raPoint">Right ascention of point.</param>
    /// <param name="ascDiff">Ascension difference of point.</param>
    /// <param name="raMc">Right ascension of MC.</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <returns><Calculated oblique ascension./returns>
    public static double ObliqueAscension(double raPoint, double ascDiff, double raMc, double geoLat)
    {
        bool north = geoLat > 0.0;
        double raDiff = raMc - raPoint;
        if (raDiff < 0.0) raDiff += 360.0;
        bool east = raDiff < 180.0;
        if ((north && east) || (!north && !east))
        {
            return RangeUtil.ValueToRange(raPoint + ascDiff, 0.0, 360.0);
        } 
        else
        {
            return RangeUtil.ValueToRange(raPoint - ascDiff, 0.0, 360.0);
        }
    }
   

}
