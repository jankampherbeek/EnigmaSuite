// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Calc.DateTime;
using Enigma.Facades.Interfaces;
using System.Runtime.InteropServices;

namespace Enigma.Facades.Se;


/// <inheritdoc/>
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