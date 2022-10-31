// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.AstronCalculations;

namespace Enigma.Domain.RequestResponse;

public record DateTimeResponse : ValidatedResponse
{
    public SimpleDateTime DateTime { get; }

    public DateTimeResponse(SimpleDateTime dateTime, bool success, string errorText) : base(success, errorText)
    {
        DateTime = dateTime;
    }

}