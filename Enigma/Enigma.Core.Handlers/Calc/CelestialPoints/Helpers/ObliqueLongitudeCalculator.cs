﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.Util;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Calc.CelestialPoints.Helpers;


/// <inheritdoc/>
public sealed class ObliqueLongitudeCalculator : IObliqueLongitudeCalculator
{
    private readonly ISouthPointCalculator _southPointCalculator;

    public ObliqueLongitudeCalculator(ISouthPointCalculator southPointCalculator) => _southPointCalculator = southPointCalculator;

    /// <inheritdoc/>
    public List<NamedEclipticLongitude> CalcObliqueLongitudes(ObliqueLongitudeRequest request)
    {
        List<NamedEclipticLongitude> oblLongitudes = new();
        EclipticCoordinates southPoint = _southPointCalculator.CalculateSouthPoint(request.Armc, request.Obliquity, request.GeoLat);
        foreach (NamedEclipticCoordinates celPointCoordinate in request.CelPointCoordinates)
        {
            double oblLong = OblLongForCelPoint(celPointCoordinate, southPoint);
            oblLongitudes.Add(new NamedEclipticLongitude(celPointCoordinate.CelPoint, oblLong));
        }
        return oblLongitudes;
    }

    private static double OblLongForCelPoint(NamedEclipticCoordinates namedEclipticCoordinate, EclipticCoordinates southPoint)
    {
        double absLatSp = Math.Abs(southPoint.Latitude);
        double longSp = southPoint.Longitude;
        double longPl = namedEclipticCoordinate.EclipticCoordinate.Longitude;
        double latPl = namedEclipticCoordinate.EclipticCoordinate.Latitude;
        double longSouthPMinusPlanet = Math.Abs(longSp - longPl);
        double longPlanetMinusSouthP = Math.Abs(longPl - longSp);
        double latSouthPMinusPlanet = absLatSp - latPl;
        double latSouthPPLusPlanet = absLatSp + latPl;
        double s = Math.Min(longSouthPMinusPlanet, longPlanetMinusSouthP) / 2;
        double tanSRad = Math.Tan(MathExtra.DegToRad(s));
        double qRad = Math.Sin(MathExtra.DegToRad(latSouthPMinusPlanet)) / Math.Sin(MathExtra.DegToRad(latSouthPPLusPlanet));
        double v = MathExtra.RadToDeg(Math.Atan(tanSRad * qRad)) - s;
        double absoluteV = RangeUtil.ValueToRange(Math.Abs(v), -90.0, 90.0);
        absoluteV = Math.Abs(absoluteV); // TODO Check if this is required, copied this from my original Java version. It partially repeats the line above.
        double correctedV;
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
        return diff < 180.0;
    }


}


