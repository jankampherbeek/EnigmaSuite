// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;

namespace E4C.Shared.References;

/// <summary>
/// Enum for Gregorian and Julian Calendar.
/// </summary>
public enum Calendars
{
    Gregorian = 0, Julian = 1
}

/// <summary>
/// Details for a calendar.
/// </summary>
public record CalendarDetails
{
    readonly public Calendars Calendar;
    readonly public string TextId;

    /// <summary>
    /// Construct details for a calendar.
    /// </summary>
    /// <param name="calendar">The calendar.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public CalendarDetails(Calendars calendar, string textId)
    {
        Calendar = calendar;
        TextId = textId;
    }
}

/// <summary>
/// Specifications for a calendar.
/// </summary>
public interface ICalendarSpecifications
{
    /// <summary>
    /// Returns the details for a Calendar.
    /// </summary>
    /// <param name="calendar">The calendar, from the enum Calendars.</param>
    /// <returns>A record CalendarDetails with the specifications.</returns>
    public CalendarDetails DetailsForCalendar(Calendars calendar);

    /// <summary>
    /// Returns a value from the enum Calendars that corresponds with an index.
    /// </summary>
    /// <param name="calendarIndex">The index for the requested item from Calendars. 
    /// Throws an exception if no Calendar for the given index does exist.</param>
    /// <returns>Instance from enum Calendars that corresponds with the given index.</returns>
    public Calendars CalendarForIndex(int calendarIndex);
}

public class CalendarSpecifications : ICalendarSpecifications
{
    /// <exception cref="ArgumentException">Is thrown if the calendar was not recognized.</exception>
    CalendarDetails ICalendarSpecifications.DetailsForCalendar(Calendars calendar)
    {
        return calendar switch
        {
            Calendars.Gregorian => new CalendarDetails(calendar, "ref.enum.calendar.gregorian"),
            Calendars.Julian => new CalendarDetails(calendar, "ref.enum.calendar.julian"),
            _ => throw new ArgumentException("Calendar unknown : " + calendar.ToString())
        };
    }

    public Calendars CalendarForIndex(int calendarIndex)
    {
        foreach (Calendars calendar in Enum.GetValues(typeof(Calendars)))
        {
            if ((int)calendar == calendarIndex) return calendar;
        }
        throw new ArgumentException("Could not find Calendars for index : " + calendarIndex);
    }
}