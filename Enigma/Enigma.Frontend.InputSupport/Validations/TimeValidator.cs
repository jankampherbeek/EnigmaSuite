// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Constants;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;
using Enigma.Frontend.Helpers.Interfaces;

namespace Enigma.Frontend.Helpers.Validations;


/// <inheritdoc/>
public class TimeValidator : ITimeValidator
{
    readonly int[] timeValues = new int[] { 0, 0, 0 };
    private double _ut = 0.0;
    private int _correctionForDay = 0;


    /// <inheritdoc/>
    public bool CreateCheckedTime(int[] timeValues, TimeZones timezone, double lmtOffset, out FullTime fullTime)
    {

        string _fullText = "";
        bool success = (timeValues.Length == 3) || (timeValues.Length == 2);

        if (success)
        {
            for (int i = 0; i < timeValues.Length; i++)
            {
                this.timeValues[i] = timeValues[i];
            }
            success = CheckMinAndMaxValues(this.timeValues);
        }
        if (success)
        {
            CalculateUtAndCorrectionForDay(timezone, lmtOffset);
            _fullText = CreateFullText(timezone, lmtOffset);
        }
        fullTime = new FullTime(this.timeValues, _ut, _correctionForDay, _fullText);

        return success;
    }

    private string CreateFullText(TimeZones timezone, double lmtOffset)
    {
        string _timeZoneTextId =  timezone.GetDetails().TextId;
        string _fullText = $"{timeValues[0]:d2}:{timeValues[1]:d2}:{timeValues[2]:d2} [{_timeZoneTextId}]";

        if (timezone == TimeZones.LMT)
        {
            _fullText += lmtOffset.ToString();
        }
        return _fullText;
    }

    private static bool CheckMinAndMaxValues(int[] valuesToCheck)
    {
        bool result = true;
        if (valuesToCheck[0] < 0 || valuesToCheck[0] > 23) result = false;
        if (valuesToCheck[1] < 0 || valuesToCheck[1] > 59) result = false;
        if (valuesToCheck[2] < 0 || valuesToCheck[2] > 59) result = false;
        return result;
    }

    private void CalculateUtAndCorrectionForDay(TimeZones timezone, double lmtOffset)
    {
        double _offset;
        _ut = timeValues[0] + ((double)timeValues[1] / EnigmaConstants.MINUTES_PER_HOUR_DEGREE) + ((double)timeValues[2] / EnigmaConstants.SECONDS_PER_HOUR_DEGREE);
        _offset = timezone == TimeZones.LMT ? lmtOffset : timezone.GetDetails().OffsetFromUt;
        _ut -= _offset;
        if (_ut < 0.0)
        {
            _ut += 24.0;
            _correctionForDay = -1;
        }
        else if (_ut >= 24.0)
        {
            _ut -= 24.0;
            _correctionForDay = 1;
        }
    }
}


