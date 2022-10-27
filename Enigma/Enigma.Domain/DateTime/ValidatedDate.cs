// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.DateTime;

/// <summary>
/// Wrapper for a date with error information.
/// </summary>
public record ValidatedDate
{
    public readonly int Year;
    public readonly int Month;
    public readonly int Day;
    public readonly Calendars Calendar;
    public readonly List<int> ErrorCodes;

    public ValidatedDate(int year, int month, int day, Calendars calendar, List<int> errorCodes)
    {
        Year = year;
        Month = month;
        Day = day;
        Calendar = calendar;
        ErrorCodes = errorCodes;
    }
}