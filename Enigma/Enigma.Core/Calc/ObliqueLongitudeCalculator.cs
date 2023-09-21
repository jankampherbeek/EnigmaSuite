// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;

namespace Enigma.Core.Calc;


/// <inheritdoc/>
public sealed class ObliqueLongitudeCalculator : IObliqueLongitudeCalculator
{
    private readonly ISouthPointCalculator _southPointCalculator;

    public ObliqueLongitudeCalculator(ISouthPointCalculator southPointCalculator) => _southPointCalculator = southPointCalculator;

    /// <inheritdoc/>
    public List<NamedEclipticLongitude> CalcObliqueLongitudes(ObliqueLongitudeRequest request)
    {
        EclipticCoordinates southPoint = _southPointCalculator.CalculateSouthPoint(request.Armc, request.Obliquity, request.GeoLat);
        return (from celPointCoordinate in request.CelPointCoordinates 
            let oblLong = OblLongForCelPoint(celPointCoordinate, southPoint, request.AyanamshaOffset) - request.AyanamshaOffset 
            select new NamedEclipticLongitude(celPointCoordinate.CelPoint, oblLong)).ToList();
    }

    private static double OblLongForCelPoint(NamedEclipticCoordinates namedEclipticCoordinate, EclipticCoordinates southPoint, double ayanamshaOffset)
    {
        double absLatSp = Math.Abs(southPoint.Latitude);
        double longSp = southPoint.Longitude;
        double longPl = namedEclipticCoordinate.EclipticCoordinate.Longitude + ayanamshaOffset;
        double latPl = namedEclipticCoordinate.EclipticCoordinate.Latitude;
        double longSouthPMinusPlanet = Math.Abs(longSp - longPl);
        double longPlanetMinusSouthP = Math.Abs(longPl - longSp);
        double latSouthPMinusPlanet = absLatSp - latPl;
        double latSouthPPlusPlanet = absLatSp + latPl;
        double s = Math.Min(longSouthPMinusPlanet, longPlanetMinusSouthP) / 2.0;
        double tanSRad = Math.Tan(MathExtra.DegToRad(s));
        double qRad = Math.Sin(MathExtra.DegToRad(latSouthPMinusPlanet)) / Math.Sin(MathExtra.DegToRad(latSouthPPlusPlanet));
        double v = MathExtra.RadToDeg(Math.Atan(tanSRad * qRad)) - s;
        double absoluteV = RangeUtil.ValueToRange(Math.Abs(v), -90.0, 90.0);
        absoluteV = Math.Abs(absoluteV); // TODO 0.2 Check if this is required, copied this from my original Java version. It partially repeats the line above.
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



