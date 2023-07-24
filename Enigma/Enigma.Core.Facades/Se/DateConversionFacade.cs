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
public sealed class DateConversionFacade : IDateConversionFacade
{
    /// <inheritdoc/>
    public bool DateTimeIsValid(SimpleDateTime dateTime)
    {
        double julianDay = 0.0;
        char calendar = dateTime.Calendar == Calendars.Gregorian ? 'g' : 'j';
        int result = ext_swe_date_conversion(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Ut, calendar, ref julianDay);
        bool validPeriod = julianDay is >= EnigmaConstants.PERIOD_TOTAL_START and < EnigmaConstants.PERIOD_TOTAL_END;
        return (result == 0) && dateTime.Ut is >= 0.0 and < 24.0 && validPeriod;

    }

    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_date_conversion")]
    private static extern int ext_swe_date_conversion(int year, int month, int day, double time, char calendar, ref double julianday);

}