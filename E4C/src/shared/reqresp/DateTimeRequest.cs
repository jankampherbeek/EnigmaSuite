// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.core.shared.domain;
using E4C.shared.references;

namespace E4C.shared.reqresp;

/// <summary>Request to calculate day and time from Julian Day.</summary>
public record DateTimeRequest
{
    public double JulDay { get; }
    public bool UseJdForUt { get; }
    public Calendars Calendar { get; }

    /// <summary>Calculate date and time.</summary>
    /// <param name="julDay">Julian day number.</param>
    /// <param name="useJdForUt">True if JD is defined in Universal time, false if JD is defined in ephemeris time.</param>
    public DateTimeRequest(double julDay, bool useJdForUt, Calendars calendar )
    {
        JulDay = julDay;
        UseJdForUt = useJdForUt;
        Calendar = calendar;
    }

}