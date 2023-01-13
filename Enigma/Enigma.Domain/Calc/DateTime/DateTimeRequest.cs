// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Calc.DateTime;

/// <summary>Request to calculate day and time from Julian Day.</summary>
/// <param name="JulDay"/>
/// <param name="UseJdForUt">True if JD is defined in Universal time, false if JD is defined in ephemeris time.</param>
/// <param name="Calendar"/>
public record DateTimeRequest(double JulDay, bool UseJdForUt, Calendars Calendar);
