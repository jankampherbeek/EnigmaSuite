// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Conversion;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;
using Serilog;

namespace Enigma.Core.LocationAndTimeZones;

public interface IDstParser
{
    List<DstLine?> ProcessDstLines(List<string> lines);
}

public class DstParser : IDstParser
{
  //  private readonly IJulDayCalculator _jdCalc;
    private readonly IJulDayFacade _jdFacade;
 
    private readonly IDayDefHandler _dayNrCalc;

    public DstParser(IJulDayFacade jdFacade,
        IDayDefHandler dayNrCalc)
    {
        _jdFacade = jdFacade;
        _dayNrCalc = dayNrCalc;
    }

    public List<DstLine?> ProcessDstLines(List<string> lines)
    {
        var elementsLines = ParseDstElementsLines(lines);
        var processedLines = ParseDstLines(elementsLines);
        return processedLines;
    }

    private List<DstElementsLine> ParseDstElementsLines(List<string> lines)
    {
        var parsedLines = new List<DstElementsLine>();

        foreach (var line in lines)
        {
            var dataLine = line;
            var items = dataLine.Split(';');
            if (items.Length < 13)
            {
                var errorTxt = $"Error ParseDstElementsLines: Invalid dataLine: {dataLine}";
                Log.Error(errorTxt);
                throw new FormatException(errorTxt);
            }

            if (!int.TryParse(items[1], out var from))
            {
                var errorTxt = $"Error ParseDstElementsLines: Invalid value for from in dataLine: {dataLine}";
                Log.Error(errorTxt);
                throw new FormatException(errorTxt);
            }

            var toValue = items[2].Equals("max") ? "2100" : items[2]; // Assume the year 2100 for max
            if (!int.TryParse(toValue, out var to))
            {
                var errorTxt = $"Error ParseDstElementsLines: Invalid value for to in dataLine: {dataLine}";
                Log.Error(errorTxt);
                throw new FormatException(errorTxt);
            }

            if (!int.TryParse(items[3], out var @in))
            {
                var errorTxt = $"Error ParseDstElementsLines: Invalid value for in dataLine: {dataLine}";
                Log.Error(errorTxt);
                throw new FormatException(errorTxt);
            }

            var startTime = DateTimeConversion.ParseDHmsToDoubleFromText(items[5], items[6], items[7]); 
            var offset = DateTimeConversion.ParseDHmsToDoubleFromText(items[9], items[10], items[11]);
            parsedLines.Add(new DstElementsLine(items[0], from, to, @in, items[4], startTime, items[8],offset, items[12]));
        }
        return parsedLines;
    }

    private List<DstLine?> ParseDstLines(List<DstElementsLine> lines)
    {
        var parsedLines = new List<DstLine?>();
        foreach (var line in lines)
        {
            for (var year = line.From; year <= line.To; year++)
            {
                try
                {
                    var newLine = CreateSingleDstLine(line, year);
                    parsedLines.Add(newLine);
                }
                catch (FormatException fe)
                {
                    Log.Error($"ParseDstLines encountered FormatException in line {line} and year {year}");
                }
            }
        }
        parsedLines.Sort((x, y) => x!.StartJd.CompareTo(y!.StartJd));
        return parsedLines;
    }

    private DstLine? CreateSingleDstLine(DstElementsLine line, int year)
    {
        var day =
            _dayNrCalc.DayFromDefinition(year, line.In, line.On); // resp. year, month and day definition
        var sdt = new SimpleDateTime(year, line.In, day, line.At, Calendars.Gregorian);
        var jd = _jdFacade.JdFromSe(sdt); // always Gregorian
        return new DstLine(jd, line.Save, line.Letter, line.Ut == "u");
    }
}

