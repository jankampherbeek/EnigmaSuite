// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Data.Entity.Infrastructure;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.LocationAndTimeZones;

/// <summary>Handle retrieving DST for a given rule and date/time</summary>
public interface IDstHandler
{
    /// <summary>Define actual DST for a given rule and date/time</summary>
    /// <remarks>The times for DST changes are given in UT, so the zoneoffset needs to be taken into account</remarks>
    /// <param name="dateTime">Date and time</param>
    /// <param name="dstRule">The dst rule</param>
    /// <param name="zoneOffset">OFfset from GMT/UT</param>
    /// <returns>Actual DST</returns>
    DstInfo CurrentDst(DateTimeHms dateTime, string dstRule, double zoneOffset);
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
    public DstInfo CurrentDst(DateTimeHms dateTime, string dstRule, double zoneOffset)
    {
        if (string.IsNullOrEmpty(dstRule))
        {
            throw new ArgumentException("Dst rule cannot be null or empty");
        }

        var isInvalid = false;
        var isAmbiguous = false;
        var emptyDstInfo = new DstInfo(0, "", false, false);
        DstLine? actDstLine = null;
        DstLine? prevDstLine = null;
        try
        {
            var dstTxtLines = dstLineReader.ReadDstLinesMatchingRule(dstRule);
            var dstLines = dstLinesParser.ProcessDstLines(dstTxtLines);
            var clockTime = dateTime.Hour + dateTime.Min / MINUTES_PER_HOUR + dateTime.Sec / SECONDS_PER_HOUR;
            var sdt = new SimpleDateTime(dateTime.Year, dateTime.Month, dateTime.Day, clockTime, Calendars.Gregorian);
            var eventJd = jdFacade.JdFromSe(sdt);
            if (eventJd < dstLines[0]!.StartJd)
            {
                return emptyDstInfo;
            }
            prevDstLine = dstLines[0];
            foreach (var line in dstLines)
            {
                var initJd = line!.StartJd;
                var correctedStartJd = line!.IsUt ? initJd + zoneOffset / 24.0 : initJd;
                var prevJd = correctedStartJd;
                if (correctedStartJd >= eventJd)
                {
                    actDstLine = prevDstLine;
                    var marginForChange = zoneOffset > 0.0001 ? zoneOffset / 24.0 : 1 / 24.0;   // Check for difference of 1 hour if there is no DST
                    var previousCorrectedStartJd =
                        line!.IsUt ? actDstLine!.StartJd + zoneOffset / 24.0 : actDstLine!.StartJd;
                    
                    if (eventJd - previousCorrectedStartJd < marginForChange)  
                    {
                        if (actDstLine!.Offset > 0.0001)   // DST starts, use 0.0001 to avoid rounding errors
                        {
                            isInvalid = true;
                        }
                        else   // DST ends
                        {
                            isAmbiguous = true;                            
                        }
                    }
                    break;
                }
                prevDstLine = line;
            }
        }
        catch (Exception e)
        {
            throw new TimeZoneException($"DstHandler encountered an exception {e} when reading/parsing dstLines");
        }
        return actDstLine is null ? emptyDstInfo : new DstInfo(actDstLine.Offset, actDstLine.Letter, isInvalid, isAmbiguous);
    }
}



