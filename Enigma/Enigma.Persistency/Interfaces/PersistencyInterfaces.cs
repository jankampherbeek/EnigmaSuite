// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Persistency;
using Enigma.Domain.RequestResponse;

namespace Enigma.Persistency.Interfaces;


/// <summary>Conversions to date for csv-data.</summary>
public interface IDateCheckedConversion
{
    public Tuple<PersistableDate, bool> StandardCsvToDate(string csvDate, string csvCalendar);
}

/// <summary>Conversions to time for csv-data.</summary>
public interface ITimeCheckedConversion
{
    public Tuple<PersistableTime, bool> StandardCsvToTime(string csvTime, string zoneOffset, string dst);
}

/// <summary>
/// Conversions for latitude and longitude for csv-data.
/// </summary>
public interface ILocationCheckedConversion
{
    /// <summary>Convert csv text for longitude into a double.</summary>
    /// <param name="csvLocation">Csv text: in the format dd:mm:ss:dir (122:34:56:E)</param>
    /// <returns>Calculated value and an indication of errors. If errors did occutr the value will be zero.</returns>
    public Tuple<double, bool> StandardCsvToLongitude(string csvLocation);

    /// <summary>Convert csv text for latitude into a double.</summary>
    /// <param name="csvLocation">Csv text: in the format dd:mm:ss:dir (12:34:56:N)</param>
    /// <returns>Calculated value and an indication of errors. If errors did occutr the value will be zero.</returns>
    public Tuple<double, bool> StandardCsvToLatitude(string csvLocation);
}


