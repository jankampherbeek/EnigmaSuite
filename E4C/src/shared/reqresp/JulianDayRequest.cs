// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Shared.Domain;

namespace E4C.Shared.ReqResp;

/// <summary>Request to calculate Julian Day.</summary>
public record JulianDayRequest
{
    public SimpleDateTime DateTime { get; }
    public bool UseJdForUt { get; }

    /// <summary>Calculate Julian day.</summary>
    /// <param name="simpleDateTime">Date and time.</param>
    /// <param name="useJdForUt">True if JD should be calculated for Universal time, false if JD should be calculatred for ephemeris time.</param>
    public JulianDayRequest(SimpleDateTime simpleDateTime, bool useJdForUt)
    {
        DateTime = simpleDateTime;
        UseJdForUt = useJdForUt;
    }

}