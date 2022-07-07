// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.DateTime;
using System.Runtime.InteropServices;

namespace Enigma.Core.Calc.SeFacades;

/// <summary>Facade for retrieving Julian Day number from date and time, using the Swiss Ephemeris.</summary>
/// <remarks>Enables accessing the SE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface IJulDayFacade
{
    /// <summary>Retrieve Julian Day number from Swiss Ephemeris.</summary>
    /// <param name="dateTime">Date, time and calendar.</param>
    /// <returns>The calculated Julian Day number.</returns>
    public double JdFromSe(SimpleDateTime dateTime);

    /// <summary>Retrieve value for Delta T.</summary>
    /// <param name="JulianDayUT">Value for Julian Day in UT.</param>
    /// <returns>The value for Delta T in seconds and fractions of seconds.</returns>
    public double DeltaTFromSe(double JulianDayUT);
}


/// <inheritdoc/>
public class JulDayFacade : IJulDayFacade
{
    /// <inheritdoc/>
    public double JdFromSe(SimpleDateTime dateTime)
    {
        /*
        int _cal = (dateTime.Calendar == Calendars.Gregorian) ? 1 : 0;
        return ext_swe_julday(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Ut, _cal);
        */
        return 2000123.5;
    }

    /// <inheritdoc/>
    public double DeltaTFromSe(double julianDayUt)
    {
        /* int flag = EnigmaConstants.SEFLG_SWIEPH;
         return ext_swe_deltat_ex(julianDayUt, flag);
        */
        return 0.001;
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


    /// <summary>Access dll to retrieve Delta T.</summary>
    /// <param name="julianDayUt">Julian day for UT.</param>
    /// <param name="flag">Always Constants.SEFLG_SWIEPH, unless the JPL or Moshier is used.</param>
    /// <returns></returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_deltat")]
    private extern static double ext_swe_deltat_ex(double julianDayUt, int flag);
}