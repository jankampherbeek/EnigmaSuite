// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using System.Text.Json;
using Enigma.Core.Handlers;
using Enigma.Domain.Persistables;
using Enigma.Domain.References;
using Serilog;

namespace Enigma.Core.Persistency;

/// <summary>Reads data from a csv file, converts it, and writes the result to a Json file.</summary>
public interface ICsv2JsonConverter
{
    /// <summary>Processes data in the 'standard' csv-format and converts it to Json.</summary>
    /// <remarks>Creates a list of lines that could not be processed.</remarks>
    /// <param name="csvLines">The csv lines to convert.</param>
    /// <param name="dataName">Name for the data.</param>
    /// <param name="dataType">Type of research data.</param>
    /// <returns>Tuple with three items: a boolean that indicates if the conversion was succesfull,
    /// a string with the json, and a list with csv-lines that caused an error.
    /// If the first item is true, the list with error-lines should be empty.</returns>
    public Tuple<bool, string, List<string>> ConvertResearchDataCsvToJson(List<string> csvLines, string dataName, 
        ResearchDataTypes dataType);
    
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
    public Tuple<bool, string, List<string>> ConvertResearchDataCsvToJson(List<string> csvLines, string dataName, ResearchDataTypes dataType)
    {
        bool noErrors = true;
        int count = csvLines.Count;
        List<StandardInputItem> allInput = new();
        List<string> resultLines = new();
        for (int i = 1; i < count; i++)           // skip first line that contains header
        {
            if (dataType == ResearchDataTypes.PlanetDance && i <= 1) continue; // skip second line from PlanetDance data
            Tuple<StandardInputItem?, bool> processedLine;
            switch (dataType)
            {

                case ResearchDataTypes.StandardEnigma:
                    processedLine = ProcessStandardLine(csvLines[i]);
                    break;
                case ResearchDataTypes.PlanetDance:
                    processedLine = ProcessPlanetDanceLine(csvLines[i]);                        
                    break;
                default:
                    Log.Error("Csv2JsonConverter.ConvertResearchDataToJson receive an unsupported instance of ResearchDataTypes: {DataType}", dataType);
                    throw new ArgumentException("Unknown type for research data.");
            }
            
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


    private Tuple<StandardInputItem?, bool> ProcessPlanetDanceLine(string csvLine)
    {
        bool success = true;
        StandardInputItem? inputItem = null;
        try
        {   // Rumen Kolev;1960;11;29;3;0;50;Varna;Bulgaria;27.916667;43.216667;2.000000;
            
            
            
            string[] csvElements = csvLine.Split(";");
            string id = csvElements[0];
            string name = csvElements[0];
            success = double.TryParse(csvElements[9].Replace(',', '.'), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out double geoLong);
            double geoLat = 0.0;
            success = success && double.TryParse(csvElements[10].Replace(',', '.'), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out geoLat);
            string yearTxt = csvElements[1];    
            string monthTxt = csvElements[2];
            string dayTxt = csvElements[3];
            string dateTxt = yearTxt + "/" + monthTxt + "/" + dayTxt;
            string hourTxt = csvElements[4];
            string minuteTxt = csvElements[5];
            string secondTxt = csvElements[6];
            string timeTxt = hourTxt + ":" + minuteTxt + ":" + secondTxt;
            string calendarTxt = "G";
            string zoneOffsetTxt = csvElements[11];
            string dstTxt = "0";        // the DST is already included in the offset
            
            PersistableDate? date = null;
            Tuple<PersistableDate, bool> dateResult = _dateCheckedConversion.StandardCsvToDate(dateTxt, calendarTxt);
            if (dateResult.Item2) date = dateResult.Item1;
            else success = false;
            PersistableTime? time = null;
            Tuple<PersistableTime, bool> timeResult = _timeCheckedConversion.StandardCsvToTime(timeTxt, zoneOffsetTxt, dstTxt);
            if (timeResult.Item2) time = timeResult.Item1;
            else success = false;
            if (success) inputItem = new StandardInputItem(id, name, geoLong, geoLat, date, time);
        }
        catch (Exception)
        {
            success = false;
        }
        return new Tuple<StandardInputItem?, bool>(inputItem, success);
    }
    

}

