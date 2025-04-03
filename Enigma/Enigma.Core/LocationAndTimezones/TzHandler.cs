// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Conversion;
using Enigma.Domain.Dtos;
using Enigma.Domain.LocationsZones;
using Enigma.Domain.References;
using Microsoft.VisualBasic;
using NUnit.Framework;
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

public class TzHandler(IJulDayCalc jdCalc, IDstHandler dstHandler) : ITzHandler
{
    private static readonly string PathSep = Path.DirectorySeparatorChar.ToString();
    private static readonly string FilePathZones = $"tz-coord{PathSep}tzdata.csv";

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
        var zoneTxtLines = ReadTzLines(tzName);
        var zoneLines = ParseTzLines(zoneTxtLines, tzName);
        var actualZone = FindZone(dateTime, zoneLines);
        return (actualZone.StdOff, actualZone.Name, actualZone.Rules);
    }

    private List<string> ReadTzLines(string tzName)
    {
        var searchTxt1 = "Zone\t" + tzName;
        var searchTxt2 = "Zone " + tzName;
        var searchTxt3 = "Zone;" + tzName;
        var tzLines = new List<string>();

        try
        {
            using var tzFile = new StreamReader(FilePathZones);
            var startLineFound = false;
            string line;
            while ((line = tzFile.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.StartsWith(searchTxt1) || line.StartsWith(searchTxt2) || line.StartsWith(searchTxt3))
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

            return tzLines;
        }
        catch (Exception ex)
        {
            Log.Error("Could not open tz file");
            return new List<string>();
        }
    }

    private List<TzLine> ParseTzLines(List<string> lines, string name)
    {
        var rules = "";
        var parsedLines = new List<TzLine>();
        foreach (var line in lines)
        {
            var dataLine = line;
            try
            {
                if (line.StartsWith("Zone;"))
                {
                    dataLine = line.Substring("Zone;".Length);
                    var index = dataLine.IndexOf(';');
                    dataLine = dataLine.Substring(index + 1); // remove tz name
                }

                var items = dataLine.Split(';');
                if (items.Length < 7)
                {
                    // todo log error
                    continue;
                }
                var offset = DateTimeConversion.ParseHmsFromText(items[0], items[1], items[2]);
                var dateTimeItems = new string[] { items[4], items[5], items[6], items[0], items[1], items[2] };
                var sdt = DateTimeConversion.ParseDateTimeFromText(dateTimeItems);
                var until = jdCalc.CalcJulDayUt(sdt);
                ; // always Gregorian
                var format = items[1].Replace("/r", "").Replace("/n", "").Replace("/t", "");
                var tzLine = new TzLine
                {
                    Name = name,
                    StdOff = offset,
                    Rules = rules,
                    Format = items[1],
                    Until = until
                };
                parsedLines.Add(tzLine);
            }
            catch (Exception e)
            {
                // TODO log error
                continue;
            }
        }

        return parsedLines;
    }

    private TzLine FindZone(DateTimeHms dateTime, List<TzLine> lines)
    {
        var time = dateTime.Hour + dateTime.Min / 60.0 + dateTime.Sec / 3600.0;
        var sdt = new SimpleDateTime(dateTime.Year, dateTime.Month, dateTime.Day, time, Calendars.Gregorian);
        var jd = jdCalc.CalcJulDayUt(sdt);
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

        return line;
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







