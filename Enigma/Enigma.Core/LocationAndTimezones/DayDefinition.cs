// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;
using Serilog;

namespace Enigma.Core.LocationAndTimeZones;

public interface IDayDefHandler
{
    public int DayFromDefinition(int year, int month, string def);
}

public class DayDefHandler(IJulDayFacade jdFacade) : IDayDefHandler
{
    private const string PF_LAST = "last";
    private const string PF_GE_PATTERN = ">=(\\d)";

    public int DayFromDefinition(int year, int month, string def)
    {
        if (def.Length <= 2)
        {
            if (int.TryParse(def, out var preDefinedDay))
            {
                return preDefinedDay;
            }
            throw new FormatException($"Could not parse predefined day: {def}");
        }

        var defDay = "";
        string defType;
        int geNumber = 1;
        
        if (def.Contains(PF_LAST))
        {
            defDay = def[4..];
            defType = PF_LAST;
        }
        else
        {
            var geMatch = System.Text.RegularExpressions.Regex.Match(def, PF_GE_PATTERN);
            if (geMatch.Success)
            {
                defDay = def.Substring(geMatch.Index - 1, 1);
                defType = geMatch.Value;
                geNumber = int.Parse(geMatch.Groups[1].Value);
            }
            else
            {
                defType = "Unknown defDay";
            }
        }

        if (!int.TryParse(defDay, out var targetDayOfWeek)) // Monday=0 ... Sunday=6
        {
            Log.Error($"Could not parse defDay: {def}");
            throw new FormatException($"Could not parse DefDay: {def}");
        }
        
        var sdt = new SimpleDateTime(year, month, 1, 12.0, Calendars.Gregorian);
        var jd = jdFacade.JdFromSe(sdt);
        var firstDayOfWeek = jdFacade.DayOfWeek(jd); // Monday=0 ... Sunday=6
        int actualDay;
        int daysInMonth;

        switch (defType)
        {
            case PF_LAST:
                daysInMonth = DateTime.DaysInMonth(year, month);
                var lastDayJd = jdFacade.JdFromSe(new SimpleDateTime(year, month, daysInMonth, 12.0, Calendars.Gregorian));
                var lastDayOfWeek = jdFacade.DayOfWeek(lastDayJd);
                
                var diff = (lastDayOfWeek - targetDayOfWeek + 7) % 7;
                actualDay = daysInMonth - diff;
                break;
                
            case string s when s.StartsWith(">="):
                var daysUntilFirstOccurrence = (targetDayOfWeek - firstDayOfWeek + 7) % 7;
                var firstOccurrence = 1 + daysUntilFirstOccurrence;
                if (firstOccurrence < geNumber)
                {
                    actualDay = firstOccurrence + (((geNumber - firstOccurrence) + 6) / 7) * 7;
                }
                else
                {
                    actualDay = firstOccurrence;
                }
                daysInMonth = DateTime.DaysInMonth(year, month);
                if (actualDay > daysInMonth)
                {
                    actualDay -= 7;
                }
                break;
                
            default:
                Log.Error($"unknown def type: {def}");
                throw new ArgumentException($"Unknown def type: {def}");
        }
        return actualDay;
    }
}
