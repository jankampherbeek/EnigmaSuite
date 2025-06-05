// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;

namespace Enigma.Frontend.Ui.Support.Validations;

public interface ITimeValidator
{
    /// <summary>
    /// Validate input and create a record FullTime.
    /// </summary>
    /// <param name="timeValues">Array with integers for time in the sequence hour, minute, second. The value for second is optional.</param>
    /// <param name="offset">The offset from UT, including the effect of DST.</param>
    /// <param name="fullTime">Resulting record FullTime.</param>
    /// <param name="dst">True if daylight saving tme is used, otherwise false. Dst is considered to be always 1 hour.</param>
    /// <returns>True if no error was found, otherwise false.</returns>
    public bool CreateCheckedTime(int[] timeValues, double offset, bool dst, out FullTime fullTime);

    /// <summary>Validate input for time</summary>
    /// <param name="timeValues"></param>
    /// <returns>True if validated, otherwise false</returns>
    public bool CheckTime(int[] timeValues);

}

/// <inheritdoc/>
public class TimeValidator : ITimeValidator
{
    Rosetta _rosetta = Rosetta.Instance;
    private readonly int[] _timeValues = { 0, 0, 0 };
    private double _ut;
    private int _correctionForDay;


    /// <inheritdoc/>
    /// TODO function has two responsibilities and needs to be split into two separate functions, probably in separate classes
    public bool CreateCheckedTime(int[] timeValues, double offset, bool dst, out FullTime fullTime)
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
            CalculateUtAndCorrectionForDay(offset);
            fullText = CreateFullText(offset, dst);
        }

        fullTime = new FullTime(_timeValues, _ut, _correctionForDay, fullText);

        return success;
    }

    public bool CheckTime(int[] timeValues) {
        var success = timeValues.Length is 3 or 2;
        if (!success) return success;
        for (var i = 0; i < timeValues.Length; i++)
        {
            _timeValues[i] = timeValues[i];
        }

        success = CheckMinAndMaxValues(_timeValues);

        return success;
    }



    private string CreateFullText(double offset, bool dst)
    {
        string dstText = dst ? "DST 1 hour" : "No DST";
        string lmtOffsetText = "";
        string fullText = $"{_timeValues[0]:d2}:{_timeValues[1]:d2}:{_timeValues[2]:d2} [{offset} {dstText}]";
        return fullText;
    }

    private static bool CheckMinAndMaxValues(IReadOnlyList<int> valuesToCheck)
    {
        bool result = !(valuesToCheck[0] < 0 || valuesToCheck[0] > 23);
        if (valuesToCheck[1] < 0 || valuesToCheck[1] > 59) result = false;
        if (valuesToCheck[2] < 0 || valuesToCheck[2] > 59) result = false;
        return result;
    }

    private void CalculateUtAndCorrectionForDay(double offset)
    {
        _ut = _timeValues[0] + ((double)_timeValues[1] / EnigmaConstants.MINUTES_PER_HOUR_DEGREE) + ((double)_timeValues[2] / EnigmaConstants.SECONDS_PER_HOUR_DEGREE);
        _ut -= offset;
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


