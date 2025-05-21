// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Conversion;
using Enigma.Domain.Dtos;
using Enigma.Facades.Se;
using Serilog;

namespace Enigma.Core.LocationAndTimeZones;

/// <summary>Parser for time zone lines</summary>
public interface ITimeZoneLineParser
{
    /// <summary>Parse lines with time zones</summary>
    /// <param name="lines">The lines to parse</param>
    /// <param name="name">The tz name for these lines</param>
    /// <returns>A list with record TzLine</returns>
    public List<TzLine> ParseTzLines(List<string> lines, string name);
}

/// <inheritdoc/>
public class TimeZoneLineParser(IJulDayFacade jdFacade, IDayDefHandler dayDefHandler): ITimeZoneLineParser
{
    private const string SEPARATOR = ";";
    private const string ZONE = "Zone;";
    
    /// <inheritdoc/>
    public List<TzLine> ParseTzLines(List<string> lines, string name)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        var parsedLines = new List<TzLine>(lines.Count);
        foreach (var line in lines)
        {
            var dataLine = line;
            double offset;
            string rules;
            string format;
            SimpleDateTime sdt;
            if (line.StartsWith(ZONE))
            {
                var headerItems = line.Split(SEPARATOR);
                offset = DateTimeConversion.ParseDHmsToDoubleFromText(headerItems[2], headerItems[3], headerItems[4]);
 
                var dateTime = new[]
                {
                    headerItems[6],     // year
                    headerItems[7],     // month
                    headerItems[8],     // day
                };
                sdt = DateTimeConversion.ParseDateTimeFromText(dateTime);
                rules = "-";
                format = headerItems[5];
            }
            else
            {
                var items = dataLine.Split(SEPARATOR);
                rules = items[3];
                format = items[4];
                offset = DateTimeConversion.ParseDHmsToDoubleFromText(items[0], items[1], items[2]);
                
                var year = items[5] == "0" ? "2100" : items[5]; 

                var dayDef = items[7];
                var day = dayDef;
                if (!int.TryParse(items[6], out int monthValue))
                {
                    var errorTxt = $"ParseTzLines: wrong value for month: {items[6]}";
                    Log.Error(errorTxt);
                    throw new FormatException(errorTxt);
                }
                if (!int.TryParse(year, out int yearValue))
                {
                    var errorTxt = $"ParseTzLines: wrong value for year: {year}";
                    Log.Error(errorTxt);
                    throw new FormatException(errorTxt);
                }
                if (dayDef.Contains("last") || dayDef.Contains("<=") || dayDef.Contains(">="))
                {
                    var dayValue = dayDefHandler.DayFromDefinition(yearValue, monthValue, dayDef);
                    day = dayValue.ToString();
                } 
                var dateTime = new[]
                {
                    year,         // year
                    items[6],     // month
                    day,          // day
                    items[0],     // hour
                    items[1],     // minute
                    items[2]      // second
                };
                sdt = DateTimeConversion.ParseDateTimeFromText(dateTime);
            }
            var until = jdFacade.JdFromSe(sdt);

            var tzLine = new TzLine(name, offset, rules, format, until);
            parsedLines.Add(tzLine);
        }
        return parsedLines;
    }
}