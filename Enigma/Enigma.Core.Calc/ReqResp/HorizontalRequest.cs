// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Locational;
using Enigma.Domain.Positional;

namespace Enigma.Core.Calc.ReqResp;

/// <summary>Request to calculate horizontal positions.</summary>
public record HorizontalRequest
{
    public double JdUt { get; }
    public Location ChartLocation { get; }
    public EclipticCoordinates EclCoord { get; }

    /// <param name="jdUt"/>
    /// <param name="location"/>
    /// <param name="eclipticCoordinates">Longitude and latitude in tropical zodiac.</param>
    public HorizontalRequest(double jdUt, Location location, EclipticCoordinates eclipticCoordinates)
    {
        JdUt = jdUt;
        ChartLocation = location;
        EclCoord = eclipticCoordinates;
    }

}