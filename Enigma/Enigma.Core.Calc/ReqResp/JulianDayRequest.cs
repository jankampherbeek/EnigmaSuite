// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Enigma.Domain.DateTime;

namespace Enigma.Core.Calc.ReqResp;

public record JulianDayRequest
{
    public SimpleDateTime DateTime { get; }

    public JulianDayRequest(SimpleDateTime simpleDateTime)
    {
        DateTime = simpleDateTime;
    }

}