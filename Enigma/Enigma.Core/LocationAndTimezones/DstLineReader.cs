// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Exceptions;

namespace Enigma.Core.LocationAndTimeZones;

public interface IDstLineReader
{
    public List<string> ReadDstLinesMatchingRule(string ruleName);
}


public class DstLineReader: IDstLineReader
{
    private static readonly string PathSep = Path.DirectorySeparatorChar.ToString();
    private static readonly string FilePathRules = $"tz-coord{PathSep}dstdata.csv";

    public List<string> ReadDstLinesMatchingRule(string ruleName)
    {
        if (string.IsNullOrEmpty(ruleName))
        {
            throw new ArgumentException("Rule name cannot be null or empty");
        }
        var matchingLines = new List<string>();
        try
        {
            foreach (var line in File.ReadLines(FilePathRules))
            {
                var trimmedLine = line.Trim();
                var indexFirstSep = trimmedLine.IndexOf(';');
                var ruleFound = trimmedLine[..indexFirstSep];
                if (ruleFound.Equals(ruleName))
                {
                    matchingLines.Add(trimmedLine);
                }
            }
        }
        catch (Exception e)
        {
            var errorTxt = $"Encountered exception {e.Message} when reading dst lines for {ruleName}";
            throw new TimeZoneException(errorTxt);
        }
        return matchingLines;
    }
}