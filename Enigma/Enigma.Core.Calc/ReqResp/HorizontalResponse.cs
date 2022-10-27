// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Positional;
using Enigma.Domain.ReqResp;

namespace Enigma.Core.Calc.ReqResp;

public record HorizontalResponse : ValidatedResponse
{
    public HorizontalCoordinates HorizontalAzimuthAltitude { get; }


    public HorizontalResponse(HorizontalCoordinates horizontalCoordinates, bool success, string errorText) : base(success, errorText)
    {
        HorizontalAzimuthAltitude = horizontalCoordinates;
    }
}

