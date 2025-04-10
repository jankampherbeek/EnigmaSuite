// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;

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
        var tzName = actualZone.Format;
        var dstRule = actualZone.Rules;
        if (!string.IsNullOrEmpty(dstRule) && dstRule.Length >= 2) // ignoring hyphen and empty string
        {
            var dst = dstHandler.CurrentDst(dateTime, dstRule);
            dstOffset = dst.Offset;
            dstUsed = Math.Abs(dstOffset - 0.0) > 1E-8;
            var replacement = dstUsed ? dst.Letter : "";
            tzName = tzName.Replace("%s", replacement);
        }

        if (tzName.Contains("%z"))
        {
            tzName = DateTimeConversion.ParseSexTextFromFloat(zoneOffset);
        }

        if (string.IsNullOrEmpty(tzName))
        {
            tzName = "Zone " + zoneOffset.ToString("0.000", CultureInfo.InvariantCulture);
        }

        return new ZoneInfo(zoneOffset + dstOffset, tzName, dstUsed);
    }

    private TzLine FindZone(DateTimeHms dateTime, List<TzLine> lines)
    {
        var time = dateTime.Hour + dateTime.Min / MINUTES_PER_HOUR + dateTime.Sec / SECONDS_PER_HOUR;
        var sdt = new SimpleDateTime(dateTime.Year, dateTime.Month, dateTime.Day, time, Calendars.Gregorian);
        var jd = jdFacade.JdFromSe(sdt);
        var line = new TzLine("", 0.0, "", "", 0.0);
        if (lines[0].Until > jd)
        {
            return lines[0];
        }

        for (var i = 1; i < lines.Count; i++)
        {
            if (lines[i].Until > jd)
            {
                return lines[i];
            }
        }

        return line;
    }
}