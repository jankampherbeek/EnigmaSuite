// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Enigma.Core.Conversion;
using Enigma.Domain.Persistables;
using Enigma.Domain.References;
using Serilog;

namespace Enigma.Core.Data;

/// <summary>Read csv files and returns standardized input data</summary>
public interface ICsvImporter
{
    /// <summary>Read a list with Enigma standard data from a file and return standardized input data</summary>
    /// <param name="fullPath">Full path and filename for csv file</param>
    /// <returns>A list of standard input items</returns>
    public List <StandardInputItem> ProcessStandardData(string fullPath);
    
    /// <summary>Read a list with data exported from PlanetDance and return standardized input data</summary>
    /// <param name="fullPath">Full path and filename for csv file</param>
    /// <returns>A list of standard input items</returns>
    public List <StandardInputItem> ProcessPlanetDanceData(string fullPath);
}

/// <inheritdoc/>
public class CsvImporter : ICsvImporter
{
    private const string WEST = "W";
    private const string SOUTH = "S";
    private const string GREG_CAL = "G";
    private const string JUL_CAL = "J";
    private const string ENIGMA_DELIMITER = ",";
    private const string PLANETDANCE_DELIMITER = ";";
    private const string HMS_SEPARATOR = ":";
    private const string YMD_SEPARATOR = "/";
    
    
    /// <inheritdoc/>
    public List<StandardInputItem> ProcessStandardData(string fullPath)
    {
        if (string.IsNullOrEmpty(fullPath))
        {
            Log.Error("ProcessEnigmaData encountered a fullPath that is null or empty ");
            throw new ArgumentNullException(nameof(fullPath));
        }

        if (!File.Exists(fullPath))
        {
            Log.Error($"ProcessEnigmaData encountered a fullPath {fullPath} but that file does not exist");
            throw new FileNotFoundException("CSV file not found", fullPath);
        }
        Log.Information($"CsvImporter started processing of Enigma data from {fullPath}");
        var standardData = new List<StandardInputItem>();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ENIGMA_DELIMITER,
            HasHeaderRecord = true,
            HeaderValidated = null,
            IgnoreBlankLines = true,
        };

        using var reader = new StreamReader(fullPath);
        using var csv = new CsvReader(reader, config);
        csv.Read();
        csv.ReadHeader();
        while (csv.Read())
        {
            var eData = csv.GetRecord<EnigmaData>();
            var (gLong, gLat) = ParseCoordinates(eData.Lon, eData.Lat);
            var (pDate, pTime) = ParseDateTime(eData.Date, eData.Time, eData.Zone, eData.Dst); 
            var sItem = new StandardInputItem(eData.Id.Trim(), eData.Name.Trim(), gLong, gLat,
                pDate.Year, pDate.Month, pDate.Day, pDate.Calendar, pTime.Hour, pTime.Minute, pTime.Second, pTime.ZoneOffset, pTime.Dst);
            standardData.Add((sItem));
        }
        Log.Information($"CsvImporter completed processing of Enigma data from {fullPath}");
        return standardData;
    }

    /// <inheritdoc/>
    public List<StandardInputItem> ProcessPlanetDanceData(string fullPath)
    {
        if (string.IsNullOrEmpty(fullPath)) throw new ArgumentNullException(nameof(fullPath));
        if (!File.Exists(fullPath)) throw new FileNotFoundException("CSV file not found", fullPath);
        Log.Information($"CsvImporter started processing of PlanetDance data from {fullPath}");
        var standardData = new List<StandardInputItem>();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = PLANETDANCE_DELIMITER,
            HasHeaderRecord = true,
            HeaderValidated = null,
            IgnoreBlankLines = true,
        };

        using var reader = new StreamReader(fullPath);
        using var csv = new CsvReader(reader, config);
        csv.Read();
        csv.ReadHeader();
        var counter = 1;
        while (csv.Read())
        {
            var pdData = csv.GetRecord<PlanetDanceData>();
            // convert pdData to standardItem
            var cal = pdData.Year < 1582 ? JUL_CAL : GREG_CAL;  // TODO find solution for swithch between Julian and Gregorian calendar
            // PlanetDance does not export DST but gives a combined offset for zone and DST. DST is set to zero.
            var sItem = new StandardInputItem(counter++.ToString(), pdData.Name, pdData.Lon, pdData.Lat,
                pdData.Year, pdData.Month, pdData.Day, cal,  pdData.Hour, pdData.Min, pdData.Sec, pdData.Zone, 0.0);
            standardData.Add((sItem));
        }
        Log.Information($"CsvImporter completed processing of PlanetDance data from {fullPath}");
        return standardData;
    }


    private static (double longitude, double latitude) ParseCoordinates(string lonText, string latText)
    {
        var gLongItems = lonText.Split(HMS_SEPARATOR);
        var sec = gLongItems.Length > 2 ? gLongItems[2] : "0";
        var gLong = DateTimeConversion.ParseDHmsToDoubleFromText(gLongItems[0], gLongItems[1], sec);
        if (gLongItems[3].ToUpper().Equals(WEST)) gLong *= -1;
        var gLatItems = latText.Split(HMS_SEPARATOR);
        sec = gLatItems.Length > 2 ? gLatItems[2] : "0";
        var gLat = DateTimeConversion.ParseDHmsToDoubleFromText(gLatItems[0], gLatItems[1], sec);
        if (gLatItems[3].ToUpper().Equals(SOUTH)) gLat *= -1;
        return (gLong, gLat);
    }


    private static (PersistableDate pDate, PersistableTime pTime) ParseDateTime(string date, string time, double zone, double dst)
    {
        var dateItems = date.Split(YMD_SEPARATOR);
        var timeItems = time.Split(HMS_SEPARATOR);
        var checkedTimeItems = new string[3];
        checkedTimeItems[0] = timeItems[0];
        checkedTimeItems[1] = timeItems[1];
        checkedTimeItems[2] = timeItems.Length > 2 ? timeItems[2] : "0";
        // Date is always Gregorian
        var sdt = DateTimeConversion.ParseDateTimeFromText(dateItems.Concat(checkedTimeItems).ToArray());
        var cal = sdt.Calendar == Calendars.Gregorian ? GREG_CAL : JUL_CAL;
        var pDate = new PersistableDate(sdt.Year, sdt.Month, sdt.Day, cal);
        var hms = DateTimeConversion.ParseDhmsToTupleFromText(checkedTimeItems[0], checkedTimeItems[1],
            checkedTimeItems[2]);
        var pTime = new PersistableTime(hms.Item1, hms.Item2, hms.Item3, zone, dst);
        return (pDate, pTime);
    }
}



/// <summary>Standard data format for Enigma</summary>
/// <param name="Id">Id of chart</param>
/// <param name="Name">Name or description</param>
/// <param name="Lon">Geographic longitude in format ddd:mm:ss:E (or W)</param>
/// <param name="Lat">Geographic latitude in format dd:mm:ss N (or S)</param>
/// <param name="Cal">Calendar, 'J' for Julian, 'G' for Gregorian</param>
/// <param name="Date">Date in format yyyy/mm/dd</param>
/// <param name="Time">Time in format hh:mm:ss</param>
/// <param name="Zone">Offset for zone</param>
/// <param name="Dst">Offset for DST</param>
public record EnigmaData(  
    string Id,
    string Name,
    string Lon,
    string Lat,
    string Date,
    string Cal,
    string Time,
    double Zone,
    double Dst);




/// <summary>Data as exported from PlanetDance</summary>
/// <remarks>Example line: Rumen Kolev;1960;11;29;3;0;50;Varna;Bulgaria;27.916667;43.216667;2.000000;</remarks>
/// <param name="Name">Name or id</param>
/// <param name="Year">Year of birth</param>
/// <param name="Month">Month of birth</param>
/// <param name="Day">Day in month</param>
/// <param name="Hour">Hour 0..23</param>
/// <param name="Min">Minute</param>
/// <param name="Sec">Second</param>
/// <param name="Place">Name of location</param>
/// <param name="Country">Name of country</param>
/// <param name="Lon">Geographic longitude</param>
/// <param name="Lat">Geographic latitude</param>
/// <param name="Zone">Total offset, including offset for DST</param>
public record PlanetDanceData(
    string Name,
    int Year,
    int Month,
    int Day,
    int Hour,
    int Min,
    int Sec,
    string Place,
    string Country,
    double Lon,
    double Lat,
    double Zone);
    