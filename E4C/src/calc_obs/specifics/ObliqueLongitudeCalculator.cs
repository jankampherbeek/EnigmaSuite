// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.calc.util;
using E4C.core.shared.domain;
using E4C.Models.Domain;
using System;
using System.Collections.Generic;

namespace E4C.calc.specifics;

/// <summary>
/// Calculator for oblique longitudes (School of Ram).
/// </summary>
public interface IObliqueLongitudeCalculator
{
    /// <summary>
    /// Perform calculations to obtain oblique longitudes.
    /// </summary>
    /// <param name="request">Specifications for the calculation.</param>
    /// <returns>Solar System Points with the oblique longitude.</returns>
    public List<NamedEclipticLongitude> CalcObliqueLongitudes(ObliqueLongitudeRequest request);
}



/// <summary>
/// Calculations for the south-point.
/// </summary>
public interface ISouthPointCalculator
{
    /// <summary>
    /// Calculate longitude and latitude for the south-point.
    /// </summary>
    /// <param name="armc">Right ascension for the MC.</param>
    /// <param name="obliquity">Obliquity of the earths axis.</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <returns>An instance of EclipticCoodinates with values for longitude and latitude.</returns>
    public EclipticCoordinates CalculateSouthPoint(double armc, double obliquity, double geoLat);
}



public class ObliqueLongitudeCalculator : IObliqueLongitudeCalculator
{
    private ISouthPointCalculator _southPointCalculator;

    public ObliqueLongitudeCalculator(ISouthPointCalculator southPointCalculator)
    {
        _southPointCalculator = southPointCalculator;
    }

    public List<NamedEclipticLongitude> CalcObliqueLongitudes(ObliqueLongitudeRequest request)
    {
        List<NamedEclipticLongitude> oblLongitudes = new();
        EclipticCoordinates southPoint = _southPointCalculator.CalculateSouthPoint(request.Armc, request.Obliquity, request.GeoLat);
        foreach (NamedEclipticCoordinates solSysPointCoordinate in request.SolSysPointCoordinates)
        {
            double oblLong = OblLongForSolSysPoint(solSysPointCoordinate, southPoint);
            oblLongitudes.Add(new NamedEclipticLongitude(solSysPointCoordinate.SolarSystemPoint, oblLong));
        }
        return oblLongitudes;
    }

    private double OblLongForSolSysPoint(NamedEclipticCoordinates namedEclipticCoordinate, EclipticCoordinates southPoint)
    {
        double absLatSp = Math.Abs(southPoint.Latitude);
        double longSp = southPoint.Longitude;
        double longPl = namedEclipticCoordinate.EclipticCoordinates.Longitude;
        double latPl = namedEclipticCoordinate.EclipticCoordinates.Latitude;
        double longSouthPMinusPlanet = Math.Abs(longSp - longPl);
        double longPlanetMinusSouthP = Math.Abs(longPl - longSp);
        double latSouthPMinusPlanet = absLatSp - latPl;
        double latSouthPPLusPlanet = absLatSp + latPl;
        double s = Math.Min(longSouthPMinusPlanet, longPlanetMinusSouthP) / 2;
        double tanSRad = Math.Tan(MathExtra.DegToRad(s));
        double qRad = Math.Sin(MathExtra.DegToRad(latSouthPMinusPlanet)) / Math.Sin(MathExtra.DegToRad(latSouthPPLusPlanet));
        double v = MathExtra.RadToDeg(Math.Atan(tanSRad * qRad)) - s;
        double absoluteV = RangeUtil.ValueToRange(Math.Abs(v), -90.0, 90.0);
        absoluteV = Math.Abs(absoluteV); // again?
        double correctedV = 0.0;
        if (IsRising(longSp, longPl))
        {
            correctedV = latPl < 0.0 ? absoluteV : -absoluteV;
        }
        else
        {
            correctedV = latPl > 0.0 ? absoluteV : -absoluteV;
        }
        return RangeUtil.ValueToRange(longPl + correctedV, 0.0, 360.0);
    }


    private static bool IsRising(double longSp, double longPl)
    {
        double diff = longPl - longSp;
        if (diff < 0.0) diff += 360.0;
        if (diff >= 360.0) diff -= 360.0;
        return (diff < 180.0);
    }
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


