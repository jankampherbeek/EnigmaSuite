// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.DateTime;

/// <summary>Enum for Gregorian and Julian Calendar.</summary>
public enum Calendars
{
    Gregorian = 0, Julian = 1
}

/// <summary>Details for a calendar.</summary>
public record CalendarDetails
{
    readonly public Calendars Calendar;
    readonly public string TextId;
    readonly public string TextIdFull;

    /// <param name="calendar">The calendar.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle. Uses an abbreviated version.</param>
    /// <param name="textIdFull">Id to find a descriptive text in a resource bundle. Uses a full version.</param>
    public CalendarDetails(Calendars calendar, string textId, string textIdFull)
    {
        Calendar = calendar;
        TextId = textId;
        TextIdFull = textIdFull;

    }
}

/// <summary>Specifications for a calendar.</summary>
public interface ICalendarSpecifications
{
    /// <param name="calendar">The calendar, from the enum Calendars.</param>
    /// <returns>A record CalendarDetails with the specifications.</returns>
    public CalendarDetails DetailsForCalendar(Calendars calendar);

    /// <returns>Calendardetails for all items in the enum Calendars.</returns>
    public List<CalendarDetails> AllCalendarDetails();

    /// <param name="calendarIndex">The index for the requested item from Calendars. 
    /// Throws an exception if no Calendar for the given index does exist.</param>
    /// <returns>Instance from enum Calendars that corresponds with the given index.</returns>
    public Calendars CalendarForIndex(int calendarIndex);



}
/// <inheritdoc/>
public class CalendarSpecifications : ICalendarSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the calendar was not recognized.</exception>
    public CalendarDetails DetailsForCalendar(Calendars calendar)
    {
        return calendar switch
        {
            Calendars.Gregorian => new CalendarDetails(calendar, "ref.enum.calendar.gregorian", "ref.enum.calendar.gregorian.full"),
            Calendars.Julian => new CalendarDetails(calendar, "ref.enum.calendar.julian", "ref.enum.calendar.julian.full"),
            _ => throw new ArgumentException("Calendar unknown : " + calendar.ToString())
        };
    }

    /// <inheritdoc/>
    public Calendars CalendarForIndex(int calendarIndex)
    {
        foreach (Calendars calendar in Enum.GetValues(typeof(Calendars)))
        {
            if ((int)calendar == calendarIndex) return calendar;
        }
        throw new ArgumentException("Could not find Calendars for index : " + calendarIndex);
    }

    /// <inheritdoc/>
    public List<CalendarDetails> AllCalendarDetails()
    {
        var allDetails = new List<CalendarDetails>();
        foreach (Calendars calendar in Enum.GetValues(typeof(Calendars)))
        {
            allDetails.Add(DetailsForCalendar(calendar));
        }
        return allDetails;
    }
}