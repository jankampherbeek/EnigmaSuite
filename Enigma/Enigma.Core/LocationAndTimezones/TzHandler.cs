// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.LocationAndTimeZones;

using Enigma.Core.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;

public interface ITzHandler
{
    ZoneInfo CurrentTime(DateTimeHms dateTime, string tzName);
}

public class TzHandling(
    IJulDayFacade jdFacade, 
    IDstHandler dstHandler, 
    ITimeZoneReader tzReader,
    ITimeZoneLineParser tzLineParser) : ITzHandler
{


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

    private (double Offset, string TzName, string DstRule) ZoneData(DateTimeHms dateTime, string tzIndication)
    {
        var zoneTxtLines = tzReader.ReadLinesForTzIndication(tzIndication);
        var zoneLines = tzLineParser.ParseTzLines(zoneTxtLines, tzIndication);
        var actualZone = FindZone(dateTime, zoneLines);
        return (actualZone.StdOff, actualZone.Name, actualZone.Rules);
    }
    

    private TzLine FindZone(DateTimeHms dateTime, List<TzLine> lines)
    {
        var time = dateTime.Hour + dateTime.Min / 60.0 + dateTime.Sec / 3600.0;
        var sdt = new SimpleDateTime(dateTime.Year, dateTime.Month, dateTime.Day, time, Calendars.Gregorian);
        var jd = jdFacade.JdFromSe(sdt); 
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

