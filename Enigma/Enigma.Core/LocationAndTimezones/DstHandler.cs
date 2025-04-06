// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.LocationAndTimeZones;

public interface IDstHandler
{
    DstInfo CurrentDst(DateTimeHms dateTime, string dstRule);
}

public class DstHandler(
    IJulDayFacade jdFacade,
    IDstParser dstLinesParser,
    IDstLineReader dstLineReader)
    : IDstHandler
{
    
    public DstInfo CurrentDst(DateTimeHms dateTime, string dstRule)
    {
        var emptyDstInfo = new DstInfo(0, "");
        DstLine? actDstLine = null;
        var dstLines = DstData(dstRule);
        dstLines = dstLines.OrderBy(line => line!.StartJd).ToList();

        var clockTime = dateTime.Hour + dateTime.Min / 60.0 + dateTime.Sec / 3600.0;
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
        return actDstLine is null ? emptyDstInfo : new DstInfo(actDstLine.Offset, actDstLine.Letter);
    }

    private List<DstLine?> DstData(string dstRule)
    {
        var dstTxtLines = dstLineReader.ReadDstLinesMatchingRule(dstRule);
        var processedLines = dstLinesParser.ProcessDstLines(dstTxtLines);
        return processedLines;
    }
}



