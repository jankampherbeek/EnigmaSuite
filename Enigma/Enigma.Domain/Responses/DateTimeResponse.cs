// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Dtos;

namespace Enigma.Domain.Responses;

public record DateTimeResponse    // TODO 0.3 remove DateTimeResponse and replcae with SimpleDateTime
{
    public SimpleDateTime DateTime { get; }

    public DateTimeResponse(SimpleDateTime dateTime, bool success, string errorText)
    {
        DateTime = dateTime;
    }

}