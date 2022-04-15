// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.core.shared.domain;
using E4C.shared.references;
using System.Runtime.InteropServices;

namespace E4C.core.facades;

/// <summary>Facade for retrieving Julian Day number from date and time, using the Swiss Ephemeris.</summary>
/// <remarks>Enables accessing the SE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface IJulDayFacade
{
    /// <summary>Retrieve Julian Day number from Swiss Ephemeris.</summary>
    /// <param name="dateTime">Date, time and calendar.</param>
    /// <returns>The calculated Julian Day number.</returns>
    public double JdFromSe(SimpleDateTime dateTime);
}


/// <inheritdoc/>
public class JulDayFacade : IJulDayFacade
{
    /// <inheritdoc/>
    public double JdFromSe(SimpleDateTime dateTime)
    {
        int _cal = (dateTime.Calendar == Calendars.Gregorian) ? 1 : 0;
        return ext_swe_julday(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Ut, _cal);
    }



    /// <summary>Access dll to retrieve Julian Day number.</summary>
    /// <param name="year">The astronomical year.</param>
    /// <param name="month">The month, counting from 1..12.</param>
    /// <param name="day">The number of the day.</param>
    /// <param name="hour">The hour: integer part and fraction.</param>
    /// <param name="gregflag">Type of calendar: Gregorian = 1, Julian = 0.</param>
    /// <returns>The calculated Julian Day Number.</returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_julday")]
    private extern static double ext_swe_julday(int year, int month, int day, double hour, int gregflag);

}