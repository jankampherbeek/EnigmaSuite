// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Shared.Domain;
using E4C.Shared.References;
using System.Runtime.InteropServices;

namespace E4C.Core.Facades;

/// <summary>Facade for date/time conversion functionality in the Swiss Ephemeris.</summary>
/// <remarks>Enables accessing the SE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface IDateConversionFacade
{
    /// <summary>Checks if a date and time are valid.</summary>
    /// <param name="dateTime"/>
    /// <returns>True if date is a valid date and ut between 0.0 (inclusive) and 24.0 (exclusive).</returns>
    public bool DateTimeIsValid(SimpleDateTime dateTime);
}


/// <inheritdoc/>
public class DateConversionFacade: IDateConversionFacade
{
    /// <inheritdoc/>
    public bool DateTimeIsValid(SimpleDateTime dateTime)
    {
        double _julianDay = 0.0;
        char _calendar = dateTime.Calendar == Calendars.Gregorian ? 'g' : 'j';
        int _result = ext_swe_date_conversion(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Ut, _calendar, ref _julianDay);
        return (_result == 0) && (0.0 <= dateTime.Ut) && (dateTime.Ut < 24.0);
    }

    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_date_conversion")]
    private extern static int ext_swe_date_conversion(int year, int month, int day, double time, char calendar, ref double julianday);

}