
using E4C.Core.Shared.Domain;
using System.Collections.Generic;


namespace E4C.Shared.ReqResp;
    /// <summary>
    /// Data for a request to calculate oblique longitudes.
    /// </summary>
public record ObliqueLongitudeRequest
{
    public readonly double Armc;
    public readonly double Obliquity;
    public readonly double GeoLat;
    public readonly List<NamedEclipticCoordinates> SolSysPointCoordinates;

    /// <param name="armc">Right ascension of the MC (in degrees).</param>
    /// <param name="obliquity">True obliquity of the earths axis.</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <param name="solSysPointCoordinates">Solar system for which to calculate the oblique longitude, incoluding their ecliptical coordinates.</param>
    public ObliqueLongitudeRequest(double armc, double obliquity, double geoLat, List<NamedEclipticCoordinates> solSysPointCoordinates)
    {
        Armc = armc;
        Obliquity = obliquity;
        GeoLat = geoLat;
        SolSysPointCoordinates = solSysPointCoordinates;
    }

}
