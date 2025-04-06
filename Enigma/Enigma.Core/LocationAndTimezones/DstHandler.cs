// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.LocationAndTimeZones;

/// <summary>Handle retrieving DST for a given rule and date/time</summary>
public interface IDstHandler
{
    /// <summary>Define actual DST for a given rule and date/time</summary>
    /// <param name="dateTime">Date and time</param>
    /// <param name="dstRule">The dst rule</param>
    /// <returns>Actual DST</returns>
    DstInfo CurrentDst(DateTimeHms dateTime, string dstRule);
}

/// <inheritdoc/>
public class DstHandler(
    IJulDayFacade jdFacade,
    IDstParser dstLinesParser,
    IDstLineReader dstLineReader)
    : IDstHandler
{
    private const double MINUTES_PER_HOUR = 60.0;
    private const double SECONDS_PER_HOUR = 3600.0; 
    
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Thrown if rule is empty or null</exception>
    /// <exception cref="TimeZoneException">Thrown if an error occurs when reading/parsing dst lines</exception>
    public DstInfo CurrentDst(DateTimeHms dateTime, string dstRule)
    {
        if (string.IsNullOrEmpty(dstRule))
        {
            throw new ArgumentException("Dst rule cannot be null or empty");
        }
        var emptyDstInfo = new DstInfo(0, "");
        DstLine? actDstLine = null;
        try
        {
            var dstTxtLines = dstLineReader.ReadDstLinesMatchingRule(dstRule);
            var dstLines = dstLinesParser.ProcessDstLines(dstTxtLines);
            dstLines = dstLines.OrderBy(line => line!.StartJd).ToList();

            var clockTime = dateTime.Hour + dateTime.Min / MINUTES_PER_HOUR + dateTime.Sec / SECONDS_PER_HOUR;
            var sdt = new SimpleDateTime(dateTime.Year, dateTime.Month, dateTime.Day, clockTime, Calendars.Gregorian);
            var jd = jdFacade.JdFromSe(sdt);
            if (jd < dstLines[0]!.StartJd)
            {
                return emptyDstInfo;
            }

            var prevDstLine = dstLines[0];
            foreach (var line in dstLines)
            {
                if (line!.StartJd < jd)
                {
                    actDstLine = prevDstLine;
                }
                prevDstLine = line;
            }
        }
        catch (Exception e)
        {
            throw new TimeZoneException($"DstHandler encountered an exception {e} when reading/parsing dstLines");
        }
        return actDstLine is null ? emptyDstInfo : new DstInfo(actDstLine.Offset, actDstLine.Letter);
    }
}



