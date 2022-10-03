// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.DateTime;
using Enigma.Domain.ReqResp;

namespace Enigma.Core.Calc.ReqResp;

public record DateTimeResponse : ValidatedResponse
{
    public SimpleDateTime DateTime { get; }

    public DateTimeResponse(SimpleDateTime dateTime, bool success, string errorText) : base(success, errorText)
    {
        DateTime = dateTime;
    }

}