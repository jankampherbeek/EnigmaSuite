// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Helpers.Support;

namespace Enigma.Frontend.Helpers.Validations;


/// <inheritdoc/>
public class TimeValidator : ITimeValidator
{
    readonly int[] timeValues = new int[] { 0, 0, 0 };
    private double _ut = 0.0;
    private int _correctionForDay = 0;


    /// <inheritdoc/>
    public bool CreateCheckedTime(int[] timeValues, TimeZones timezone, double lmtOffset, bool dst, out FullTime fullTime)
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
            CalculateUtAndCorrectionForDay(timezone, lmtOffset, dst);
            _fullText = CreateFullText(timezone, lmtOffset, dst);
        }
        fullTime = new FullTime(this.timeValues, _ut, _correctionForDay, _fullText);

        return success;
    }

    private string CreateFullText(TimeZones timezone, double lmtOffset, bool dst)
    {
        string _timeZoneTextId = timezone.GetDetails().TextId;
        string dstText = dst ? Rosetta.TextForId("common.dst.used") : Rosetta.TextForId("common.dst.notused");
        string lmtOffsetText = "";
        if (timezone == TimeZones.LMT)
        {
            lmtOffsetText = " " + lmtOffset.ToString();
        }
        string _fullText = $"{timeValues[0]:d2}:{timeValues[1]:d2}:{timeValues[2]:d2} [{_timeZoneTextId}]{lmtOffsetText} {dstText}";


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

    private void CalculateUtAndCorrectionForDay(TimeZones timezone, double lmtOffset, bool dst)
    {
        double _offset;
        _ut = timeValues[0] + ((double)timeValues[1] / EnigmaConstants.MinutesPerHourDegree) + ((double)timeValues[2] / EnigmaConstants.SecondsPerHourDegree);
        _offset = timezone == TimeZones.LMT ? lmtOffset : timezone.GetDetails().OffsetFromUt;
        _ut -= _offset;
        if (dst) _ut--;
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


