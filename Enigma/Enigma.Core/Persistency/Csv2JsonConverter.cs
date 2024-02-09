// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Text.Json;
using Enigma.Core.Interfaces;
using Enigma.Domain.Persistables;

namespace Enigma.Core.Persistency;

/// <summary>Reads data from a csv file, converts it, and writes the result to a Json file.</summary>
public interface ICsv2JsonConverter
{
    /// <summary>Processes data in the 'standard' csv-format and converts it to Json.</summary>
    /// <remarks>Creates a list of lines that could not be processed.</remarks>
    /// <param name="csvLines">The csv lines to convert.</param>
    /// <param name="dataName">Name for the data.</param>
    /// <returns>Tuple with three items: a boolean that indicates if the conversion was succesfull, a string with the json,  
    /// and a list with csv-lines that caused an error. It the first item is true, the list with error-lines should be empty.</returns>
    public Tuple<bool, string, List<string>> ConvertStandardDataCsvToJson(List<string> csvLines, string dataName);
    
}

/// <inheritdoc/>
public sealed class Csv2JsonConverter : ICsv2JsonConverter
{
    private readonly ILocationCheckedConversion _locationCheckedConversion;
    private readonly IDateCheckedConversion _dateCheckedConversion;
    private readonly ITimeCheckedConversion _timeCheckedConversion;

    public Csv2JsonConverter(ILocationCheckedConversion locationCheckedConversion,
        IDateCheckedConversion dateCheckedConversion,
        ITimeCheckedConversion timeCheckedConversion)
    {
        _locationCheckedConversion = locationCheckedConversion;
        _dateCheckedConversion = dateCheckedConversion;
        _timeCheckedConversion = timeCheckedConversion;
    }

   
    /// <inheritdoc/>
    public Tuple<bool, string, List<string>> ConvertStandardDataCsvToJson(List<string> csvLines, string dataName)
    {
        bool noErrors = true;
        int count = csvLines.Count;
        List<StandardInputItem> allInput = new();
        List<string> resultLines = new();
        for (int i = 1; i < count; i++)           // skip first line that contains header
        {
            Tuple<StandardInputItem?, bool> processedLine = ProcessStandardLine(csvLines[i]);
            if (!processedLine.Item2 || processedLine.Item1 == null)
            {
                resultLines.Add("Error: " + csvLines[i]);
                noErrors = false;
            }
            else
            {
                allInput.Add(processedLine.Item1);
            }
        }
        string jsonText = "";
        if (!noErrors) return new Tuple<bool, string, List<string>>(noErrors, jsonText, resultLines);
        string creation = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        StandardInput standardInput = new(dataName, creation, allInput);
        var options = new JsonSerializerOptions { WriteIndented = true };
        jsonText = JsonSerializer.Serialize(standardInput, options);
        return new Tuple<bool, string, List<string>>(noErrors, jsonText, resultLines);
    }

    private Tuple<StandardInputItem?, bool> ProcessStandardLine(string csvLine)
    {
        bool noErrors = true;
        StandardInputItem? inputItem = null;
        try
        {
            string[] csvElements = csvLine.Split(",");
            string id = csvElements[0];
            string name = csvElements[1];
            string geoLongText = csvElements[2];
            string geoLatText = csvElements[3];
            string dateText = csvElements[4];
            string calendarText = csvElements[5];
            string timeText = csvElements[6];
            string zoneOffsetText = csvElements[7];
            string dstText = csvElements[8];
            Tuple<double, bool> result = _locationCheckedConversion.StandardCsvToLongitude(geoLongText);
            double geoLongitude = 0.0;
            if (result.Item2) geoLongitude = result.Item1;
            else noErrors = false;
            result = _locationCheckedConversion.StandardCsvToLatitude(geoLatText);
            double geoLatitude = 0.0;
            if (result.Item2) geoLatitude = result.Item1;
            else noErrors = false;
            PersistableDate? date = null;
            Tuple<PersistableDate, bool> dateResult = _dateCheckedConversion.StandardCsvToDate(dateText, calendarText);
            if (dateResult.Item2) date = dateResult.Item1;
            else noErrors = false;
            PersistableTime? time = null;
            Tuple<PersistableTime, bool> timeResult = _timeCheckedConversion.StandardCsvToTime(timeText, zoneOffsetText, dstText);
            if (timeResult.Item2) time = timeResult.Item1;
            else noErrors = false;
            inputItem = new StandardInputItem(id, name, geoLongitude, geoLatitude, date, time);
        }
        catch (Exception)
        {
            noErrors = false;
        }
        return new Tuple<StandardInputItem?, bool>(inputItem, noErrors);
    }

    /*Id,Name,longitude,latitude,date,cal,time,zone,dst
107, Leonardo da Vinci, 10:55:0:E, 43:47:0:N, 1452/4/14, J, 21:40, 0.7277778, 0
108, Albrecht Dürer, 11:04:0:E, 49:27:0:N, 1471/5/21, J, 11:00, 0.7377778, 0
109, Michelangelo Buonarotti, 11:59:0:E, 43:39:0:N, 1475/3/6, J, 1:45, 0.7988888, 0*/

    private Tuple<StandardInputItem?, bool> ProcessPlanetDanceLine(string csvLine)
    {
        bool noErrors = true;
        StandardInputItem? inputItem = null;
        try
        {
            string[] csvElements = csvLine.Split(",");
            string id = csvElements[0];
            string name = csvElements[1];
            string geoLongText = csvElements[2];
            string geoLatText = csvElements[3];
            string dateText = csvElements[4];
            string calendarText = csvElements[5];
            string timeText = csvElements[6];
            string zoneOffsetText = csvElements[7];
            string dstText = csvElements[8];
            Tuple<double, bool> result = _locationCheckedConversion.StandardCsvToLongitude(geoLongText);
            double geoLongitude = 0.0;
            if (result.Item2) geoLongitude = result.Item1;
            else noErrors = false;
            result = _locationCheckedConversion.StandardCsvToLatitude(geoLatText);
            double geoLatitude = 0.0;
            if (result.Item2) geoLatitude = result.Item1;
            else noErrors = false;
            PersistableDate? date = null;
            Tuple<PersistableDate, bool> dateResult = _dateCheckedConversion.StandardCsvToDate(dateText, calendarText);
            if (dateResult.Item2) date = dateResult.Item1;
            else noErrors = false;
            PersistableTime? time = null;
            Tuple<PersistableTime, bool> timeResult = _timeCheckedConversion.StandardCsvToTime(timeText, zoneOffsetText, dstText);
            if (timeResult.Item2) time = timeResult.Item1;
            else noErrors = false;
            inputItem = new StandardInputItem(id, name, geoLongitude, geoLatitude, date, time);
        }
        catch (Exception)
        {
            noErrors = false;
        }
        return new Tuple<StandardInputItem?, bool>(inputItem, noErrors);
    }
}

