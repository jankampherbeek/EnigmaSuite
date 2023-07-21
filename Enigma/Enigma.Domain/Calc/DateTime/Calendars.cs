// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Calc.DateTime;

/// <summary>Enum for Gregorian and Julian Calendar.</summary>
public enum Calendars
{
    Gregorian = 0, Julian = 1
}


/// <summary>Details for a calendar</summary>
/// <param name="Calendar">The calendar</param>
/// <param name="TextShort">Abbreviated descriptive text (one character)</param>
/// <param name="TextFull">Descriptive text</param>
public record CalendarDetails(Calendars Calendar, string TextShort, string TextFull);


/// <summary>Extension class for enum Calendars.</summary>
public static class CalendarsExtensions
{
    /// <summary>Retrieve details for calendar.</summary>
    /// <param name="cal">The calendar, is automatically filled.</param>
    /// <returns>Details for the calendar.</returns>
    public static CalendarDetails GetDetails(this Calendars cal)
    {
        return cal switch
        {
            Calendars.Gregorian => new CalendarDetails(cal, "G", "Gregorian"),
            Calendars.Julian => new CalendarDetails(cal, "J", "Julian"),
            _ => throw new ArgumentException("Calendar unknown : " + cal.ToString())
        };
    }

    /// <summary>Retrieve details for items in the enum Calendars.</summary>
    /// <returns>All details.</returns>
    public static List<CalendarDetails> AllDetails(this Calendars _)
    {
        return (from Calendars calendar in Enum.GetValues(typeof(Calendars)) select calendar.GetDetails()).ToList();
    }
}





