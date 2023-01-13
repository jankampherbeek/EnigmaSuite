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


/// <summary>Details for a calendar.</summary>
/// <param name="calendar">The calendar.</param>
/// <param name="textId">Id to find a descriptive text in a resource bundle. Uses an abbreviated version.</param>
/// <param name="textIdFull">Id to find a descriptive text in a resource bundle. Uses a full version.</param>
public record CalendarDetails(Calendars Calendar, string TextId, string TextIdFull);


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
            Calendars.Gregorian => new CalendarDetails(cal, "ref.enum.calendar.gregorian", "ref.enum.calendar.gregorian.full"),
            Calendars.Julian => new CalendarDetails(cal, "ref.enum.calendar.julian", "ref.enum.calendar.julian.full"),
            _ => throw new ArgumentException("Calendar unknown : " + cal.ToString())
        };
    }

    /// <summary>Retrieve details for items in the enum Calendars.</summary>
    /// <returns>All details.</returns>
    public static List<CalendarDetails> AllDetails(this Calendars _)
    {
        var allDetails = new List<CalendarDetails>();
        foreach (Calendars calendar in Enum.GetValues(typeof(Calendars)))
        {
            allDetails.Add(calendar.GetDetails());
        }
        return allDetails;
    }
}





