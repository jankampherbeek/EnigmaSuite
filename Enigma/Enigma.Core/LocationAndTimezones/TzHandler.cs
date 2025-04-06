// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.LocationAndTimeZones;

using System.Collections.Generic;
using Conversion;
using System.Linq;
using Domain.Dtos;
using Domain.References;
using Facades.Se;

/// <summary>Handle retrieving info about time zones and DST.</summary>
public interface ITzHandler
{
    /// <summary>Find Offset, TzName and dstRuleName for given dateTime</summary>
    /// <param name="dateTime">Date and time to check</param>
    /// <param name="tzGroupName">Time zone indication</param>
    /// <returns>Record ZoneInfo with offset, tzName and dstRuleName</returns>
    ZoneInfo CurrentTime(DateTimeHms dateTime, string tzGroupName);
}

/// <inhertidoc/>
public class TzHandler(
    IJulDayFacade jdFacade, 
    IDstHandler dstHandler, 
    ITimeZoneReader tzReader,
    ITimeZoneLineParser tzLineParser) : ITzHandler
{
    private const double MINUTES_PER_HOUR = 60.0;
    private const double SECONDS_PER_HOUR = 3600.0; 

    /// <inhertidoc/>
    public ZoneInfo CurrentTime(DateTimeHms dateTime, string tzGroupName)
    {
        var dstOffset = 0.0;
        var dstUsed = false;
        var zoneTxtLines = tzReader.ReadLinesForTzIndication(tzGroupName);
        var zoneLines = tzLineParser.ParseTzLines(zoneTxtLines, tzGroupName);
        var actualZone = FindZone(dateTime, zoneLines);
        var zoneOffset = actualZone.StdOff;
        var tzName = actualZone.Name;
        var dstRule = actualZone.Rules;
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

    private TzLine FindZone(DateTimeHms dateTime, List<TzLine> lines)
    {
        var time = dateTime.Hour + dateTime.Min / MINUTES_PER_HOUR + dateTime.Sec / SECONDS_PER_HOUR;
        var sdt = new SimpleDateTime(dateTime.Year, dateTime.Month, dateTime.Day, time, Calendars.Gregorian);
        var jd = jdFacade.JdFromSe(sdt); 
        // var counter = 0;
        // var line = lines[0];
        // foreach (var newLine in lines.Skip(1))
        // {
        //     if (newLine.Until < jd)
        //     {
        //         line = lines[counter];
        //         continue;
        //     }
        //     counter++;
        // }
        // return line;
        return lines.LastOrDefault(line => line.Until >= jd) ?? lines[0];
    }
}

