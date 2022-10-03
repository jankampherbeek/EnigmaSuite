// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Positional;

namespace Enigma.Core.Calc.ReqResp;

public record CoordinateConversionRequest
{
    /// <summary>Ecliptical longitude and latitude.</summary>
    public EclipticCoordinates EclCoord { get; }
    /// <summary>Obliquity of the aerth's axis.</summary>
    public double Obliquity { get; }

    public CoordinateConversionRequest(EclipticCoordinates eclCoord, double obliquity)
    {
        EclCoord = eclCoord;
        Obliquity = obliquity;
    }

}