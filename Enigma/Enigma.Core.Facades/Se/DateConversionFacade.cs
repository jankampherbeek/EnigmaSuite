// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;
using Enigma.Facades.Interfaces;
using System.Runtime.InteropServices;

namespace Enigma.Facades.Se;


/// <inheritdoc/>
public sealed class DateConversionFacade : IDateConversionFacade
{
    /// <inheritdoc/>
    public bool DateTimeIsValid(SimpleDateTime dateTime)
    {
        double _julianDay = 0.0;
        char _calendar = dateTime.Calendar == Calendars.Gregorian ? 'g' : 'j';
        int _result = ext_swe_date_conversion(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Ut, _calendar, ref _julianDay);
        return (_result == 0) && (0.0 <= dateTime.Ut) && (dateTime.Ut < 24.0);

    }

    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_date_conversion")]
    private extern static int ext_swe_date_conversion(int year, int month, int day, double time, char calendar, ref double julianday);

}