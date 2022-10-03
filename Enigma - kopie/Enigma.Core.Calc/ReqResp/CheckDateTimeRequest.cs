// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.DateTime;

namespace Enigma.Core.Calc.ReqResp;

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