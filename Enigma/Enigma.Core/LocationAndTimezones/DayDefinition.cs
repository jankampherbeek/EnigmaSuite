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
    private const string PF_GE1 = ">=1";
    private const string PF_GE2 = ">=2";

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
        if (def.Contains(PF_LAST))
        {
            defDay = def[4..];
            defType = PF_LAST;
        }
        else if (def.Contains(PF_GE1))
        {
            var index = def.IndexOf(PF_GE1, StringComparison.Ordinal);
            defDay = def.Substring(index - 1, 1);
            defType = PF_GE1;
        }
        else if (def.Contains(PF_GE2))
        {
            var index = def.IndexOf(PF_GE2, StringComparison.Ordinal);
            defDay = def.Substring(index - 1, 1);
            defType = PF_GE2;
        }
        else
        {
            defType = "Unknown defDay";
        }

        if (!int.TryParse(defDay, out var switchDay))
        {
            Log.Error($"Could not parse defDay: {def}");
            throw new FormatException($"Could not parse DefDay: {def}");
        }
        
        var sdt = new SimpleDateTime(year, month, 1, 12.0, Calendars.Gregorian);
        var jd = jdFacade.JdFromSe(sdt);    // jd for first day of month
        var firstDow = jdFacade.DayOfWeek(jd); // index for first day of month, Mon=0...Sun=7
        int actualDay;

        switch (defType)
        {
            case PF_LAST:
                var m31 = new[] { 1, 3, 5, 7, 8, 10, 12 };
                if (m31.Contains(month))
                {
                    var lastDayOfMonth = firstDow + 30;
                    var diff = lastDayOfMonth % 7 - switchDay;
                    if (diff < 0)
                    {
                        diff += 7;
                    }
                    actualDay = 31 - diff;
                }
                else
                {
                    // assuming the last days of February are never used for a DST switch
                    var lastDayOfMonth = firstDow + 29;
                    var diff = lastDayOfMonth % 7 - switchDay;
                    if (diff < 0)
                    {
                        diff += 7;
                    }
                    actualDay = 30 - diff;
                }
                break;
            case PF_GE1:
                var diff1 = switchDay - firstDow;
                actualDay = 1 + diff1;
                break;
            case PF_GE2:
                var diff2 = switchDay - firstDow;
                actualDay = 8 + diff2;
                break;
            default:
                Log.Error($"unknown def type: {def}");
                throw new ArgumentException($"Unknown def type: {def}");
        }
        return actualDay;
    }
}

