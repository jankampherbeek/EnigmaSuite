// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Conversion;
using Microsoft.VisualBasic;
using Serilog;

namespace Enigma.Core.LocationAndTimeZones;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public interface ITzHandler
{
    ZoneInfo CurrentTime(DateTimeHms dateTime, string tzName);
}

public class TzHandling(IJulDayCalculator jdCalc, IDstHandler dstHandler) : ITzHandler
{
    private static readonly string PathSep = Path.DirectorySeparatorChar.ToString();
    private static readonly string FilePathZones = $"tz-coord{PathSep}zones.csv";

    public ZoneInfo CurrentTime(DateTimeHms dateTime, string tzGroupName)
    {
        // find Offset, TzName and dstRuleName for given dateTime
        var dstOffset = 0.0;
        var dstUsed = false;

        var (zoneOffset, tzName, dstRule) = ZoneData(dateTime, tzGroupName);


        if (!string.IsNullOrEmpty(dstRule) && dstRule.Length >= 2) // ignoring hyphen and empty string
        {
            dstUsed = true;
            var dst = dstHandler.CurrentDst(dateTime, dstRule);
            dstOffset = dst.Offset;
            tzName = tzName.Replace("%s", dst.Letter);
        }

        if (tzName.Contains("%z"))
        {
            tzName = DateTimeConversion.ParseSexTextFromFloat(zoneOffset);
        }

        return new ZoneInfo(zoneOffset + dstOffset, tzName, dstUsed);
    }

    private (double Offset, string TzName, string DstRule) ZoneData(DateTimeHms dateTime, string tzName)
    {
        var (zoneTxtLines, error) = ReadTzLines(tzName);
        if (error != null)
        {
            // Log error: "Reading lines from the tz file returns an error"
            return (0.0f, "", "");
        }

        var (zoneLines, parseError) = ParseTzLines(zoneTxtLines, tzName);
        if (parseError != null)
        {
            // Log error: "Parsing lines from the tz file returns an error"
            return (0.0f, "", "");
        }

        var (actualZone, findError) = FindZone(dateTime, zoneLines);
        if (findError != null)
        {
            // Log error: "Finding zone from the tz file returns an error"
            return (0.0f, "", "");
        }

        return (actualZone.StdOff, actualZone.Name, actualZone.Rules);
    }

    private (List<string> Lines, Exception Error) ReadTzLines(string tzName)
    {
        var searchTxt1 = "Zone\t" + tzName;
        var searchTxt2 = "Zone " + tzName;
        var tzLines = new List<string>();

        try
        {
            using var tzFile = new StreamReader(FilePathZones);
            var startLineFound = false;
            string line;
            while ((line = tzFile.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.StartsWith(searchTxt1) || line.StartsWith(searchTxt2))
                {
                    tzLines.Add(line);
                    startLineFound = true;
                }
                else
                {
                    if (startLineFound)
                    {
                        if (!line.StartsWith("Zone"))
                        {
                            tzLines.Add(line);
                        }
                        else
                        {
                            startLineFound = false;
                        }
                    }
                }
            }

            return (tzLines, null);
        }
        catch (Exception ex)
        {
            // Log error: "Could not open tz file"
            return (new List<string>(), ex);
        }
    }

    private (List<TzLine> Lines, Exception Error) ParseTzLines(List<string> lines, string name)
    {
        var parsedLines = new List<TzLine>();
        foreach (var line in lines)
        {
            var dataLine = line;
            if (line.StartsWith("Zone;"))
            {
                dataLine = line.Substring("Zone;".Length);
                var index = dataLine.IndexOf(';');
                dataLine = dataLine.Substring(index + 1); // remove tz name
            }

            var items = dataLine.Split(';');
            var offset = DateTimeConversion.ParseHmsFromText(items[0], items[1], items[2]);
            var sdt = DateTimeConversion.ParseDateTimeFromText(items.Skip(3).ToArray());
            var until = jdCalc.CalcJd(sdt.Year, sdt.Month, sdt.Day, sdt.Ut, true); // always Gregorian

            var tzLine = new TzLine
            {
                Name = name,
                StdOff = offset,
                Rules = items[3],
                Format = items[4],
                Until = until
            };
            parsedLines.Add(tzLine);
        }

        return (parsedLines, null);
    }

    private (TzLine Line, Exception Error) FindZone(DateTimeHms dateTime, List<TzLine> lines)
    {
        var time = dateTime.Hour + dateTime.Min / 60.0 + dateTime.Sec / 3600.0;
        var jd = jdCalc.CalcJd(dateTime.Year, dateTime.Month, dateTime.Day, time, true);
        var counter = 0;
        var line = lines[0];
        foreach (var newLine in lines.Skip(1))
        {
            if (newLine.Until < jd)
            {
                line = lines[counter];
                continue;
            }

            counter++;
        }

        return (line, null);
    }
}

public record TzLine
{
    public string Name { get; set; }
    public double StdOff { get; set; }
    public string Rules { get; set; }
    public string Format { get; set; }
    public double Until { get; set; }
}

public record ZoneInfo(double offset, string tzName, bool dst)
{
    public double Offset { get; } = offset;
    public string TzName { get; } = tzName;
    public bool Dst { get; } = dst;
}

// Assuming these interfaces and classes exist in C#
public interface IJulDayCalculator
{
    double CalcJd(int year, int month, int day, double hour, bool gregorian);
}


public record DateTimeHms
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public int Hour { get; set; }
    public int Min { get; set; }
    public int Sec { get; set; }
}

