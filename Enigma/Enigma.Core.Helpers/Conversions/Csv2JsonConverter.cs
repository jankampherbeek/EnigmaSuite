// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Persistency;
using Enigma.Domain.RequestResponse;
using Enigma.Core.Helpers.Interfaces;
using Enigma.Persistency.Interfaces;
using Newtonsoft.Json;
using Enigma.Core.Helpers.Persistency;

namespace Enigma.Core.Helpers.Conversions;


/// <inheritdoc/>
public class Csv2JsonConverter : ICsv2JsonConverter
{
    private readonly ITextFileWriter _fileWriter;
    private readonly ILocationCheckedConversion _locationCheckedConversion;
    private readonly IDateCheckedConversion _dateCheckedConversion;
    private readonly ITimeCheckedConversion _timeCheckedConversion;

    public Csv2JsonConverter(ITextFileWriter fileWriter,
        ILocationCheckedConversion locationCheckedConversion,
        IDateCheckedConversion dateCheckedConversion,
        ITimeCheckedConversion timeCheckedConversion)
    {
        _fileWriter = fileWriter;
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



    public ResultMessage Old_ConvertStandardCsvToJson(string dataName, string fullPathCsv, string fullPathJson)
    {
        bool noErrors = true;
        int errorCode = ErrorCodes.ERR_NONE;
        string conversionReport = "";
        List<StandardInputItem> allInput = new();
        Tuple<StandardInputItem, bool> processedLine;
        string[] lines = File.ReadAllLines(fullPathCsv);
        List<string> resultLines = new()
        {
            "Result for processing : " + fullPathCsv + " for data: " + fullPathJson
        };
        int count = lines.Length;
        for (int i = 1; i < count - 1; i++)           // skip first line that contains header
        {
            processedLine = ProcessLine(lines[i]);
            if (!processedLine.Item2)
            {
                resultLines.Add("Error, " + processedLine.Item1);
                noErrors = false;
            }
            else
            {
                allInput.Add(processedLine.Item1);
            }
        }
        if (noErrors)
        {
            string creation = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            StandardInput standardInput = new(dataName, creation, allInput);
            var result = JsonConvert.SerializeObject(standardInput, Formatting.Indented);
            _fileWriter.WriteFile(fullPathJson + @"\date_time_loc.json", result);
            conversionReport = "Succesfully converted " + fullPathCsv + ". Number of lines processed: " + allInput.Count;
        }
        else
        {
            errorCode = ErrorCodes.ERR_CSV_JSON_CONVERSION;
            conversionReport = "Errors while trying to convert " + fullPathCsv + ". Numer of lines with errors: " + (resultLines.Count - 1);
        }

        resultLines.Add("Number of lines processed (including header): " + lines.Length.ToString());
        _fileWriter.WriteFile(fullPathCsv + "_result", resultLines);
        return new ResultMessage(errorCode, conversionReport);

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

