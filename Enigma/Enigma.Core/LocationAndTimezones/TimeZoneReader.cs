// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Exceptions;

namespace Enigma.Core.LocationAndTimeZones;

/// <summary>Reader for time zone lines</summary>
public interface ITimeZoneReader
{
    /// <summary>Reads time zone lines for a specific timezone</summary>
    /// <param name="tzIndication">The time zone to read</param>
    /// <returns>The lines from the time zone</returns>
    public List<string> ReadLinesForTzIndication(string tzIndication);
}

public class TimeZoneReader: ITimeZoneReader
{
    private static readonly string PathSep = Path.DirectorySeparatorChar.ToString();
    private static readonly string FilePathZones = $"tz-coord{PathSep}tzdata.csv";

    public List<string> ReadLinesForTzIndication(string tzIndication)
    {
        if (string.IsNullOrEmpty(tzIndication))
        {
            throw new ArgumentException("tzIndication has no value");
        }
        
        var searchTxt1 = "Zone;" + tzIndication;
        var searchTxt2 = "Zone ;" + tzIndication;
        var tzLines = new List<string>();

        try
        {
            using var tzFile = new StreamReader(FilePathZones);
            var startLineFound = false;
            while (tzFile.ReadLine() is { } line)
            {
                line = line.Trim();
                if (line.StartsWith(searchTxt1) || line.StartsWith(searchTxt2))
                {
                    tzLines.Add(line);
                    startLineFound = true;
                }
                else
                {
                    if (!startLineFound) continue;
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
        catch (Exception e)
        {
            var errorTxt = $"Encountered exception {e.Message} when reading tz lines for {tzIndication}";
            throw new TimeZoneException(errorTxt);
        }
        return tzLines;
    }
}