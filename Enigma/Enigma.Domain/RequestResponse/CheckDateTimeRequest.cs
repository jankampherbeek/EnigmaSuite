// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.AstronCalculations;

namespace Enigma.Domain.RequestResponse;

/// <summary>Request to check the validity of a date.</summary>
public record CheckDateTimeRequest
{
    public SimpleDateTime DateTime { get; }
    public bool UseJdForUt { get; }

    /// <summary>Calculate Julian day.</summary>
    /// <param name="simpleDateTime">Date and time.</param>
    public CheckDateTimeRequest(SimpleDateTime simpleDateTime)
    {
        DateTime = simpleDateTime;
    }

}