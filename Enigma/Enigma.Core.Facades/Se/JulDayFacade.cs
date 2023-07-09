// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Constants;
using Enigma.Facades.Interfaces;
using System.Runtime.InteropServices;

namespace Enigma.Facades.Se;


/// <inheritdoc/>
public sealed class JulDayFacade : IJulDayFacade
{
    /// <inheritdoc/>
    public double JdFromSe(SimpleDateTime dateTime)
    {
        int _cal = (dateTime.Calendar == Calendars.Gregorian) ? 1 : 0;
        return ext_swe_julday(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Ut, _cal);
    }

    /// <inheritdoc/>
    public double DeltaTFromSe(double JulianDayUT)
    {
        int flag = EnigmaConstants.SeflgSwieph;
        return ext_swe_deltat_ex(JulianDayUT, flag);
    }


    /// <summary>Access dll to retrieve Julian Day number.</summary>
    /// <param name="year">The astronomical year.</param>
    /// <param name="month">The month, counting from 1..12.</param>
    /// <param name="day">The number of the day.</param>
    /// <param name="hour">The hour: integer part and fraction.</param>
    /// <param name="gregflag">Type of calendar: Gregorian = 1, Julian = 0.</param>
    /// <returns>The calculated Julian Day Number.</returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_julday")]
    private extern static double ext_swe_julday(int year, int month, int day, double hour, int gregflag);


    /// <summary>Access dll to retrieve Delta T.</summary>
    /// <param name="julianDayUt">Julian day for UT.</param>
    /// <param name="flag">Always Constants.SEFLG_SWIEPH, unless the JPL or Moshier is used.</param>
    /// <returns></returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_deltat")]
    private extern static double ext_swe_deltat_ex(double julianDayUt, int flag);
}