// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.shared.references;

namespace E4C.core.shared.domain;


/// <summary>Representation for a date and time, including calendar.</summary>
/// <remarks>For ut (Universal Time) add the time in 0..23 hours and a decimal fraction for the total of minutes and seconds.</remarks>
public record SimpleDateTime
{
    public int Year { get; }
    public int Month { get; }
    public int Day { get; }
    public double Ut { get; }
    public Calendars Calendar { get; }

    public SimpleDateTime(int year, int month, int day, double ut, Calendars calendar)
    {
        Year = year;
        Month = month;
        Day = day;
        Ut = ut;
        Calendar = calendar;
    }
}