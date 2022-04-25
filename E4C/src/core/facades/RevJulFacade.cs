// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Shared.Domain;
using E4C.Shared.References;
using System.Runtime.InteropServices;

namespace E4C.Core.Facades;

/// <summary>Facade for retrieving date and time from a Julian Day number, using the Swiss Ephemeris.</summary>
/// <remarks>Enables accessing the SE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface IRevJulFacade
{

    /// <summary>Retrieve date and time (UT) from a given Julian Day Number.</summary>
    /// <param name="julianDayNumber"/>
    /// <param name="calendar">Gregorian or Julian calendar.</param>
    /// <returns>An instance of SimpleDateTime that reflects the Julian Day nr and the calendar.</returns>
    public SimpleDateTime DateTimeFromJd(double julianDayNumber, Calendars calendar);
}


/// <inheritdoc/>
public class RevJulFacade : IRevJulFacade
{

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


    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_revjul")]
    private extern static void ext_swe_revjul(double tjd, int gregflag, ref int year, ref int month, ref int day, ref double hour);

}