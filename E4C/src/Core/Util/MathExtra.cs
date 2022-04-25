// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;


namespace E4C.Core.Util;


/// <summary>Representation of rectangular coordinates.</summary>
public record RectAngCoordinates
{
    public readonly double XCoord;
    public readonly double YCoord;
    public readonly double ZCoord;

    public RectAngCoordinates(double xCoord, double yCoord, double zCoord)
    {
        XCoord = xCoord;
        YCoord = yCoord;
        ZCoord = zCoord;
    }
}

/// <summary>Representation of polar coordinates.</summary>
public record PolarCoordinates
{
    public readonly double PhiCoord;
    public readonly double ThetaCoord;
    public readonly double RCoord;

    public PolarCoordinates(double phiCoord, double thetaCoord, double rCoord)
    {
        PhiCoord = phiCoord;
        ThetaCoord = thetaCoord;
        RCoord = rCoord;
    }
}


/// <summary>
/// Several mathematical functions that are not covered by the standard Math library.
/// All functions are static.
/// </summary>
public static class MathExtra
{

    /// <summary>Convert radians to degrees.</summary>
    public static double RadToDeg(double radians)
    {
        return (180 / Math.PI) * radians;
    }

    /// <summary>Converts degrees to radians.</summary>
    public static double DegToRad(double degrees)
    {
        return (Math.PI / 180) * degrees;
    }


    public static double[] Rectangular2Polar(double[] rectangularValues)
    {
        if ((rectangularValues == null) || (rectangularValues.Length != 3))
        {
            throw new ArgumentException("Invalud input for rectangularValues.");
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

    public static double[] Polar2Rectangular(double[] polarValues)
    {
        double phi = polarValues[0];
        double theta = polarValues[1];
        double r = polarValues[2];
        double x = r * Math.Cos(theta) * Math.Cos(phi);
        double y = r * Math.Cos(theta) * Math.Sin(phi);
        double z = r * Math.Sin(theta);

        return new double[] { x, y, z };

    }

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

}
