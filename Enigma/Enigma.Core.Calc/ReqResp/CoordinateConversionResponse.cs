// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Positional;
using Enigma.Domain.ReqResp;

namespace Enigma.Core.Calc.ReqResp;

public record CoordinateConversionResponse : ValidatedResponse
{
    public EquatorialCoordinates equatorialCoord { get; }

    public CoordinateConversionResponse(EquatorialCoordinates equatorialCoordinates, bool success, string errorText) : base(success, errorText)
    {
        equatorialCoord = equatorialCoordinates;
    }

}
