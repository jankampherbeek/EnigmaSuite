// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Diagnostics.CodeAnalysis;
using Enigma.Domain.Constants;
using System.Runtime.InteropServices;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Facades.Se;

/// <summary>Facade for retrieving Julian Day number from date and time, using the Swiss Ephemeris.</summary>
/// <remarks>Enables accessing the CommonSE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface IJulDayFacade
{
    /// <summary>Retrieve Julian Day number from Swiss Ephemeris.</summary>
    /// <param name="dateTime">Date, time and calendar.</param>
    /// <returns>The calculated Julian Day number.</returns>
    public double JdFromSe(SimpleDateTime dateTime);

    /// <summary>Retrieve date and time from Julian Day number.</summary>
    /// <param name="jd">Julian Day number</param>
    /// <param name="cal">Calendar</param>
    /// <returns>Date and time.</returns>
    public SimpleDateTime DateTimeFromJd(double jd, Calendars cal);

    /// <summary>Retrieve value for Delta T.</summary>
    /// <param name="julianDayUt">Value for Julian Day in UT.</param>
    /// <returns>The value for Delta T in seconds and fractions of seconds.</returns>
    public double DeltaTFromSe(double julianDayUt);
}

/// <inheritdoc/>
[SuppressMessage("Interoperability", "SYSLIB1054:Use \'LibraryImportAttribute\' instead of \'DllImportAttribute\' to generate P/Invoke marshalling code at compile time")]
public sealed class JulDayFacade : IJulDayFacade
{
    /// <inheritdoc/>
    public double JdFromSe(SimpleDateTime dateTime)
    {
        int cal = (dateTime.Calendar == Calendars.Gregorian) ? 1 : 0;
        return ext_swe_julday(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Ut, cal);
    }

    /// <inheritdoc/>
    public SimpleDateTime DateTimeFromJd(double jd, Calendars cal)
    {
        int calFlag = cal == Calendars.Gregorian ? 1 : 0;
        /*double[] dateAndTime = new double[4];
        ext_swe_revjul(jd, calFlag, dateAndTime );*/
        int year = 0;
        int month = 0;
        int day = 0;
        double hour = 0.0;
        ext_swe_revjul(jd, calFlag, ref year, ref month, ref day, ref hour);        
        
        return new SimpleDateTime(year, month, day, hour, cal);
    }

    /// <inheritdoc/>
    public double DeltaTFromSe(double julianDayUt)
    {
        const int flag = EnigmaConstants.SEFLG_SWIEPH;
        return ext_swe_deltat_ex(julianDayUt, flag);
    }
    

    /// <summary>Access dll to retrieve Julian Day number.</summary>
    /// <param name="year">The astronomical year.</param>
    /// <param name="month">The month, counting from 1..12.</param>
    /// <param name="day">The number of the day.</param>
    /// <param name="hour">The hour: integer part and fraction.</param>
    /// <param name="gregflag">Type of calendar: Gregorian = 1, Julian = 0.</param>
    /// <returns>The calculated Julian Day Number.</returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_julday")]
    private static extern double ext_swe_julday(int year, int month, int day, double hour, int gregflag);


    /// <summary>Access dll to retrieve Delta T.</summary>
    /// <param name="julianDayUt">Julian day for UT.</param>
    /// <param name="flag">Always Constants.SEFLG_SWIEPH, unless the JPL or Moshier is used.</param>
    /// <returns></returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_deltat")]
    private static extern double ext_swe_deltat_ex(double julianDayUt, int flag);

    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_revjul")]
    private static extern void ext_swe_revjul(double julianDayUt, int gregFlag, ref int year, ref int month, ref int day, ref double hour);

    
    
    
}