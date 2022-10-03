// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.ReqResp;
using Enigma.Core.Calc.Util;
using Enigma.Domain.Positional;

namespace Enigma.Core.Calc.ObliqueLongitude;

/// <summary>Calculator for oblique longitudes (School of Ram).</summary>
public interface IObliqueLongitudeCalculator
{
    /// <summary>Perform calculations to obtain oblique longitudes.</summary>
    /// <param name="request">Specifications for the calculation.</param>
    /// <returns>Solar System Points with the oblique longitude.</returns>
    public List<NamedEclipticLongitude> CalcObliqueLongitudes(ObliqueLongitudeRequest request);
}



/// <inheritdoc/>
public class ObliqueLongitudeCalculator : IObliqueLongitudeCalculator
{
    private readonly ISouthPointCalculator _southPointCalculator;

    public ObliqueLongitudeCalculator(ISouthPointCalculator southPointCalculator) => _southPointCalculator = southPointCalculator;

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
        absoluteV = Math.Abs(absoluteV); // TODO Check if this is required, copied this from my original Java version. It partially repeats the line above.
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



