// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.domain.shared.specifications;
using E4C.Shared.References;
using System.Collections.Generic;

namespace E4C.Shared.ReqResp;

/// <summary>
/// Data for a request to calculate one or more positions for a solar system point.
/// </summary>
public record SolSysPointsRequest
{
    public readonly double JulianDayUt;
    public readonly Location ChartLocation;
    public readonly List<SolarSystemPoints> SolarSystemPoints;
    public readonly ZodiacTypes ZodiacType;
    public readonly Ayanamshas Ayanamsha;
    public readonly ObserverPositions ObserverPosition;
    public readonly ProjectionTypes ProjectionType;

    /// <param name="julianDayUt">Julian day for universal time.</param>
    /// <param name="location">Location (only latitude and longitude are used).</param>
    /// <param name="solarSystemPoints">List with the Solar System Points to calculate.</param>
    /// <param name="zodiacType">The zodiac type: tropical or sidereal.</param>
    /// <param name="ayanamsha">The ayanamsha to be applied.</param>
    /// <param name="observerPosition">Observer position (geocentric, topocentric, heliocentric).</param>
    /// <param name="projectionType">Projection type (standard or oblique longitude).</param>
    public SolSysPointsRequest(double julianDayUt, Location location, List<SolarSystemPoints> solarSystemPoints,
        ZodiacTypes zodiacType, Ayanamshas ayanamsha, ObserverPositions observerPosition, ProjectionTypes projectionType)
    {
        JulianDayUt = julianDayUt;
        ChartLocation = location;
        SolarSystemPoints = solarSystemPoints;
        ZodiacType = zodiacType;
        Ayanamsha = ayanamsha;
        ObserverPosition = observerPosition;
        ProjectionType = projectionType;
    }
}