// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.LocationsZones;
using Enigma.Domain.References;

namespace Enigma.Core.LocationAndTimeZones;

public interface IDstHandler
{
    DstInfo CurrentDst(DateTimeHms dateTime, string dstRule);
}

public class DstHandler(IJulDayCalc jdCalc, IDstParser dstLinesParser): IDstHandler
{
    private static readonly string PathSep = Path.DirectorySeparatorChar.ToString();
    private static readonly string FILE_PATH_RULES = $"tz-coord{PathSep}dstdata.csv";

    public DstInfo CurrentDst(DateTimeHms dateTime, string dstRule)
    {
        var emptyDstInfo = new DstInfo(0, "");
        DstLine? actDstLine = null;

        var dstLines = DstData(dstRule);
        
        dstLines = dstLines.OrderBy(line => line!.StartJd).ToList();

        var clockTime = dateTime.Hour + dateTime.Min / 60.0 + dateTime.Sec / 3600.0;
        // always use Gregorian cal.
        var sdt = new SimpleDateTime(dateTime.Year, dateTime.Month, dateTime.Day, clockTime, Calendars.Gregorian); 
        var jd = jdCalc.CalcJulDayUt(sdt); 
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
        var dstTxtLines = ReadDstLines(dstRule);
        var processedLines = dstLinesParser.ProcessDstLines(dstTxtLines);
        return processedLines;
    }

    private List<string> ReadDstLines(string ruleName)
    {
        var dstTxtLines = new List<string>();
            using var dstFile = new StreamReader(FILE_PATH_RULES);
            while (dstFile.ReadLine() is { } line)
            {
                line = line.Trim();
                if (line.StartsWith(ruleName))
                {
                    dstTxtLines.Add(line);
                }
            }
            return dstTxtLines;
    }
}

public record DstElementsLine(string Name, int From, int To, int In, string On, double At, double Save, string Letter);

public record DstLine(double StartJd, double Offset, string Letter);

public record DstInfo(double Offset, string Letter);

