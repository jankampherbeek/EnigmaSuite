// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Enigma.Domain.Persistables;

namespace Enigma.Core.Persistency;

/// <summary>Read csv files and returns standardized input data</summary>
public interface ICsvImporter
{
    /// <summary>Read a list with Enigma standard data from a file and return standardized input data</summary>
    /// <param name="fullPath">Full path and filename for csv file</param>
    /// <returns>A list of standard input items</returns>
    public List <StandardInputItem> ProcessStandardData(string fullPath);
    public List <StandardInputItem> ProcessPlanetDanceData(string fullPath);
}

/// <inheritdoc/>
public class CsvImporter: ICsvImporter
{
    /// <inheritdoc/>
    public List<StandardInputItem> ProcessStandardData(string fullPath)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public List<StandardInputItem> ProcessPlanetDanceData(string fullPath)
    {
        var standardData = new List<StandardInputItem>();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";", 
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
            var cal = pdData.Year < 1582 ? "J" : "G";
            var date = new PersistableDate(pdData.Year, pdData.Month, pdData.Day, cal);
            // PlanetDance does not export DST but gives a combined offset for zone and DST. DST is set to zero.
            var time = new PersistableTime(pdData.Hour, pdData.Min, pdData.Sec, pdData.Zone, 0.0);
            var sItem = new StandardInputItem(counter++.ToString(), pdData.Name, pdData.Lon, pdData.Lat,
                date, time);
            standardData.Add((sItem));
        }
        return standardData;
    }
}


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
/// <param name="Country">Nam eof country</param>
/// <param name="GeoLong">Geographic longitude</param>
/// <param name="GeoLat">Geographic latitude</param>
/// <param name="Offset">Total offset, including offset for DST</param>
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
    