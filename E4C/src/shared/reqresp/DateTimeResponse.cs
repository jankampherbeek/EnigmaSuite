// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.core.shared.domain;
using E4C.domain.shared.reqresp;

namespace E4C.shared.reqresp;

public record DateTimeResponse : ValidatedResponse
{
    public SimpleDateTime DateTime { get; }

    public DateTimeResponse(SimpleDateTime dateTime, bool success, string errorText) : base(success, errorText)
    {
        DateTime = dateTime;
    }

}