// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.DateTime;

/// <summary>
/// Record for a full definition of a data.
/// </summary>
/// <remarks>Assumes an astronomical year count.</remarks>
public record FullDate
{
    public readonly int[] YearMonthDay;
    public readonly Calendars Calendar;
    public readonly string DateFullText;

    /// <summary>
    /// Constructor for FullDate.
    /// </summary>
    /// <param name="yearMonthDay">Texts for year, month and day, in that sequence.</param>
    /// <param name="calendar">Instane of enu Calendars.</param>
    /// <param name="dateFullText">Text for the date, includes texts between [] that needs to be replaced with texts from Rosetta.</param>
    public FullDate(int[] yearMonthDay, Calendars calendar, string dateFullText)
    {
        YearMonthDay = yearMonthDay;
        Calendar = calendar;
        DateFullText = dateFullText;
    }
}

