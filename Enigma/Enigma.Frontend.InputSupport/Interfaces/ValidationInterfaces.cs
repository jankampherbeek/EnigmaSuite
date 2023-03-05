// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;

namespace Enigma.Frontend.Helpers.Interfaces;

// Interfaces for validators.


public interface IDateValidator
{
    /// <summary>
    /// Validate input and create a record FullDate.
    /// </summary>
    /// <param name="dateValues>Array with values for the date in the sequence: year, month, day.</param>
    /// <param name="calendar">The calendar that is used (Gregorian or Julian).</param>
    /// <param name="yearCount">The year count, this will be converted to an astronomical year count.</param>
    /// <param name="fullDate">The resulting record FullDate.</param>
    /// <param name="errorCodes">Errorcodes, if any.</param> 
    /// <returns>True if no error occurred, otherwise false.</returns>
    public bool CreateCheckedDate(int[] dateValues, Calendars calendar, YearCounts yearCount, out FullDate? fullDate);
}



public interface IGeoLatValidator
{
    /// <summary>
    /// Validate input and create a record FullGeoLatitude.
    /// </summary>
    /// <param name="latValues">Array with integers for the latitude in the sequence degree, minute, second. The value for second is optional.</param>
    /// <param name="direction">The direction: north or south.</param>
    /// <param name="fullLatitude">Resulting record FullGeoLatitude.</param>
    /// <returns>True if no error was found, otherwise false.</returns>
    public bool CreateCheckedLatitude(int[] inputLatValues, Directions4GeoLat direction, out FullGeoLatitude fullLatitude);
}


public interface IGeoLongValidator
{
    /// <summary>
    /// Validate input and create a record FullGeoLongitude.
    /// </summary>
    /// <param name="inputLongValues">Array with integers for the longitude in the sequence degree, minute, second. The value for second is optional.</param>
    /// <param name="direction">The direction: east or west.</param>
    /// <param name="fullLongitude">Resulting record FullGeoLongitude.</param>
    /// <returns>True if no error was found, otherwise false.</returns>
    public bool CreateCheckedLongitude(int[] inputLongValues, Directions4GeoLong direction, out FullGeoLongitude fullLongitude);
}

public interface ITimeValidator
{
    /// <summary>
    /// Validate input and create a record FullTime.
    /// </summary>
    /// <param name="timeValues">Array with integers for time in the sequence hour, minute, second. The value for second is optional.</param>
    /// <param name="timezone">The timezone that is used.</param>
    /// <param name="lmtOffset">If TimeZone is LMT this parameter contains a value for the time offset. Will be zero if TimeZone != LMT.</param>
    /// <param name="fullTime">Resulting record FullTime.</param>
    /// <param name="dst">True if daylight saving tme is used, otherwise false. Dst is considered to be always 1 hour.</param>
    /// <returns>True if no error was found, otherwise false.</returns>
    public bool CreateCheckedTime(int[] timeValues, TimeZones timezone, double lmtOffset, bool dst, out FullTime fullTime);
}
