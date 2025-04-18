﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Frontend.Ui.Support.Validations;

public interface ITimeValidator
{
    /// <summary>
    /// Validate input and create a record FullTime.
    /// </summary>
    /// <param name="timeValues">Array with integers for time in the sequence hour, minute, second. The value for second is optional.</param>
    /// <param name="timezone">The timezone that is used.</param>
    /// <param name="lmtOffset">If TimeZone is LMT this parameter contains a value for the time offset. Will be zero if TimeZone != LMT.</param>
    /// <param name="fullTime">Resulting record FullTime.</param>
    /// <param name="dst">True if daylight saving tme is used, otherwise false. Dst is considered to be always 1 hour.</param>
    /// <returns>True if no error was found, otherwise false.</returns>
    public bool CreateCheckedTime(int[] timeValues, TimeZones timezone, double lmtOffset, bool dst, out FullTime fullTime);
}

/// <inheritdoc/>
public class TimeValidator : ITimeValidator
{
    Rosetta _rosetta = Rosetta.Instance;
    private readonly int[] _timeValues = { 0, 0, 0 };
    private double _ut;
    private int _correctionForDay;


    /// <inheritdoc/>
    public bool CreateCheckedTime(int[] timeValues, TimeZones timezone, double lmtOffset, bool dst, out FullTime fullTime)
    {

        string fullText = string.Empty;
        bool success = timeValues.Length is 3 or 2;

        if (success)
        {
            for (int i = 0; i < timeValues.Length; i++)
            {
                _timeValues[i] = timeValues[i];
            }
            success = CheckMinAndMaxValues(_timeValues);
        }
        if (success)
        {
            CalculateUtAndCorrectionForDay(timezone, lmtOffset, dst);
            fullText = CreateFullText(timezone, lmtOffset, dst);
        }
        fullTime = new FullTime(_timeValues, _ut, _correctionForDay, fullText);

        return success;
    }

    private string CreateFullText(TimeZones timezone, double lmtOffset, bool dst)
    {
        string timeZoneTextId = _rosetta.GetText(timezone.GetDetails().RbKey);
        string dstText = dst ? "DST 1 hour" : "No DST";
        string lmtOffsetText = "";
        if (timezone == TimeZones.Lmt)
        {
            lmtOffsetText = " " + lmtOffset;
        }
        string fullText = $"{_timeValues[0]:d2}:{_timeValues[1]:d2}:{_timeValues[2]:d2} [{timeZoneTextId}]{lmtOffsetText} {dstText}";
        return fullText;
    }

    private static bool CheckMinAndMaxValues(IReadOnlyList<int> valuesToCheck)
    {
        bool result = !(valuesToCheck[0] < 0 || valuesToCheck[0] > 23);
        if (valuesToCheck[1] < 0 || valuesToCheck[1] > 59) result = false;
        if (valuesToCheck[2] < 0 || valuesToCheck[2] > 59) result = false;
        return result;
    }

    private void CalculateUtAndCorrectionForDay(TimeZones timezone, double lmtOffset, bool dst)
    {
        _ut = _timeValues[0] + ((double)_timeValues[1] / EnigmaConstants.MINUTES_PER_HOUR_DEGREE) + ((double)_timeValues[2] / EnigmaConstants.SECONDS_PER_HOUR_DEGREE);
        double offset = timezone == TimeZones.Lmt ? lmtOffset : timezone.GetDetails().OffsetFromUt;
        _ut -= offset;
        if (dst) _ut--;
        switch (_ut)
        {
            case < 0.0:
                _ut += 24.0;
                _correctionForDay = -1;
                break;
            case >= 24.0:
                _ut -= 24.0;
                _correctionForDay = 1;
                break;
        }
    }
}


