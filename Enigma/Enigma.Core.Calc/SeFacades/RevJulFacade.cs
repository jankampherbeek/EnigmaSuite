// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using System.Runtime.InteropServices;

namespace Enigma.Core.Calc.SeFacades;


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


    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_revjul")]
    private extern static void ext_swe_revjul(double tjd, int gregflag, ref int year, ref int month, ref int day, ref double hour);

}