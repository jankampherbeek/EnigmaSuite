// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Persistency;
using Newtonsoft.Json;

namespace Enigma.Core.Handlers.Persistency.Helpers;


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
        Tuple<StandardInputItem?, bool> processedLine;
        List<StandardInputItem> allInput = new();
        List<string> resultLines = new();
        for (int i = 1; i < count; i++)           // skip first line that contains header
        {
            processedLine = ProcessLine(csvLines[i]);
            if (!processedLine.Item2 || processedLine.Item1 == null)
            {
                resultLines.Add("Error: " + processedLine.Item1);
                noErrors = false;
            }
            else
            {
                allInput.Add(processedLine.Item1);
            }
        }
        string jsonText = "";
        if (noErrors)
        {
            string creation = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            StandardInput standardInput = new(dataName, creation, allInput);
            jsonText = JsonConvert.SerializeObject(standardInput, Formatting.Indented);
        }
        return new Tuple<bool, string, List<string>>(noErrors, jsonText, resultLines);
    }

    private Tuple<StandardInputItem?, bool> ProcessLine(string csvLine)
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
            inputItem = new(id, name, geoLongitude, geoLatitude, date, time);
        }
        catch (Exception)
        {
            noErrors = false;
        }
        return new Tuple<StandardInputItem?, bool>(inputItem, noErrors);
    }




}

