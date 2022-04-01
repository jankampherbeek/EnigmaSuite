// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
using System.Runtime.InteropServices;

namespace E4C.calc.seph.sefacade;

/// <summary>
/// Facade for date/time functionality in the Swiss Ephemeris.
/// </summary>
/// <remarks>
/// Enables accessing the SE dll. Passes any result without checking, exceptions are automatically propagated. 
/// </remarks>
public interface ISeDateTimeFacade
{
    /// <summary>
    /// Retrieve Julian Day number from Swiss Ephemeris.
    /// </summary>
    /// <param name="dateTime">Date, time and calendar.</param>
    /// <returns>The calculated and validated Julian Day number.</returns>
    public double JdFromSe(SimpleDateTime dateTime);

    /// <summary>
    /// Retrieve date and time (UT) from a given Julian Day Number.
    /// </summary>
    /// <param name="julianDayNumber"/>
    /// <param name="calendar">Gregorian or Julian calendar.</param>
    /// <returns>An instance of SimpleDateTime that reflects the Julian Day nr and the calendar.</returns>
    public SimpleDateTime DateTimeFromJd(double julianDayNumber, Calendars calendar);

    /// <summary>
    /// Checks if a date and time are valid.
    /// </summary>
    /// <param name="dateTime"/>
    /// <returns>True if date is a valid date and ut between 0.0 (inclusive) and 24.0 (exclusive).</returns>
    public bool DateTimeIsValid(SimpleDateTime dateTime);

}


/// <inheritdoc/>
public class SeDateTimeFacade : ISeDateTimeFacade
{
    /// <inheritdoc/>
    public double JdFromSe(SimpleDateTime dateTime)
    {
        int _cal = (dateTime.Calendar == Calendars.Gregorian) ? 1 : 0;
        return ext_swe_julday(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Ut, _cal);
    }

    /// <inheritdoc/>
    public SimpleDateTime DateTimeFromJd(double julianDayNumber, Calendars calendar)
    {
        int _calId = (calendar == Calendars.Gregorian) ? 1 : 0;
        int _year = 0;
        int _month = 0;
        int _day = 0;
        double _ut = 0.0;
        ext_swe_revjul(julianDayNumber, _calId, ref _year, ref _month, ref _day, ref _ut);
        return new SimpleDateTime(_year, _month, _day, _ut, calendar);
    }

    /// <inheritdoc/>
    public bool DateTimeIsValid(SimpleDateTime dateTime)
    {
        double _julianDay = 0.0;
        char _calendar = dateTime.Calendar == Calendars.Gregorian ? 'g' : 'j';
        int _result = ext_swe_date_conversion(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Ut, _calendar, ref _julianDay);
        return (_result == 0) && (0.0 <= dateTime.Ut) && (dateTime.Ut < 24.0);
    }


    /// <summary>
    /// Access dll to retrieve Julian Day number.
    /// </summary>
    /// <param name="year">The astronomical year.</param>
    /// <param name="month">The month, counting from 1..12.</param>
    /// <param name="day">The number of the day.</param>
    /// <param name="hour">The hour: integer part and fraction.</param>
    /// <param name="gregflag">Type of calendar: Gregorian = 1, Julian = 0.</param>
    /// <returns>The calculated Julian Day Number.</returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_julday")]
    private extern static double ext_swe_julday(int year, int month, int day, double hour, int gregflag);

    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_revjul")]
    private extern static void ext_swe_revjul(double tjd, int gregflag, ref int year, ref int month, ref int day, ref double hour);

    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_date_conversion")]
    private extern static int ext_swe_date_conversion(int year, int month, int day, double time, char calendar, ref double julianday);

}