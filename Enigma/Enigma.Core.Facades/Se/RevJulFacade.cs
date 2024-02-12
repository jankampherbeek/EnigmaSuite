// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Facades.Se;

/// <summary>Facade for retrieving date and time from a Julian Day number, using the Swiss Ephemeris.</summary>
/// <remarks>Enables accessing the CommonSE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface IRevJulFacade
{

    /// <summary>Retrieve date and time (UT) from a given Julian Day Number.</summary>
    /// <param name="julianDayNumber"/>
    /// <param name="calendar">Gregorian or Julian calendar.</param>
    /// <returns>An instance of SimpleDateTime that reflects the Julian Day nr and the calendar.</returns>
    public SimpleDateTime DateTimeFromJd(double julianDayNumber, Calendars calendar);
}


/// <inheritdoc/>
[SuppressMessage("Interoperability", "SYSLIB1054:Use \'LibraryImportAttribute\' instead of \'DllImportAttribute\' to generate P/Invoke marshalling code at compile time")]
public sealed class RevJulFacade : IRevJulFacade
{

    /// <inheritdoc/>
    public SimpleDateTime DateTimeFromJd(double julianDayNumber, Calendars calendar)
    {
        int calId = (calendar == Calendars.Gregorian) ? 1 : 0;
        int year = 0;
        int month = 0;
        int day = 0;
        double ut = 0.0;
        ext_swe_revjul(julianDayNumber, calId, ref year, ref month, ref day, ref ut);
        return new SimpleDateTime(year, month, day, ut, calendar);
    }


    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_revjul")]
    private static extern void ext_swe_revjul(double tjd, int gregflag, ref int year, ref int month, ref int day, ref double hour);

}