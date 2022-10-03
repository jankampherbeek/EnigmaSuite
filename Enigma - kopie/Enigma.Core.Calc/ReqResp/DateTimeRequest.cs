// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.DateTime;

namespace Enigma.Core.Calc.ReqResp;

/// <summary>Request to calculate day and time from Julian Day.</summary>
public record DateTimeRequest
{
    public double JulDay { get; }
    public bool UseJdForUt { get; }
    public Calendars Calendar { get; }

    /// <summary>Calculate date and time.</summary>
    /// <param name="julDay"/>
    /// <param name="useJdForUt">True if JD is defined in Universal time, false if JD is defined in ephemeris time.</param>
    /// <param name="calendar"/>
    public DateTimeRequest(double julDay, bool useJdForUt, Calendars calendar)
    {
        JulDay = julDay;
        UseJdForUt = useJdForUt;
        Calendar = calendar;
    }

}