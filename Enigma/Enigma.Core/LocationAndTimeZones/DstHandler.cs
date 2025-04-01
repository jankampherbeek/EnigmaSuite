// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.LocationAndTimeZones;

public interface IDstHandler
{
    DstInfo CurrentDst(DateTimeHms dateTime, string dstRule);
}

public class DstHandling(
    IJulDayCalculator jdCalc,
    // IJulDayFacade dowCalc,
    // IDayDefHandler dayNrCalc,
    IDstParser dstLinesParser)
    : IDstHandler
{
    private const string FILE_PATH_RULES = @"..\..\data\rules.csv";
    // private readonly IJulDayFacade _dowCalc = dowCalc;
    // private readonly IDayDefHandler _dayNrCalc = dayNrCalc;

    public DstInfo CurrentDst(DateTimeHms dateTime, string dstRule)
    {
        var emptyDstInfo = new DstInfo(0, "");
        DstLine? actDstLine = null;

        var dstLines = DstData(dstRule);
        
        dstLines = dstLines.OrderBy(line => line!.StartJd).ToList();

        var clockTime = dateTime.Hour + dateTime.Min / 60.0 + dateTime.Sec / 3600.0;
        var jd = jdCalc.CalcJd(dateTime.Year, dateTime.Month, dateTime.Day, clockTime,
            true); // always use Gregorian cal.
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

