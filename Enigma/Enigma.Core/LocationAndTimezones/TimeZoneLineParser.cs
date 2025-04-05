// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Conversion;
using Enigma.Domain.Dtos;
using Enigma.Facades.Se;

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
public class TimeZoneLineParser(IJulDayFacade jdFacade): ITimeZoneLineParser
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
                offset = DateTimeConversion.ParseHmsFromText(headerItems[2], headerItems[3], headerItems[4]);
                var dateTime = new string[]
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
                offset = DateTimeConversion.ParseHmsFromText(items[0], items[1], items[2]);
                var dateTime = new string[]
                {
                    items[5],     // year
                    items[6],     // month
                    items[7],     // day
                    items[0],     // hour
                    items[1],     // minute
                    items[2]      // second
                };
                
                sdt = DateTimeConversion.ParseDateTimeFromText(dateTime);
            }
            var until = jdFacade.JdFromSe(sdt);  

            var tzLine = new TzLine
            {
                Name = name,
                StdOff = offset,
                Rules = rules,
                Format = format,
                Until = until
            };
            parsedLines.Add(tzLine);
        }
        return parsedLines;
    }
}