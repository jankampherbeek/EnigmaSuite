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
    private readonly int[] _timeValues = { 0, 0, 0 };
    private double _ut;
    private int _correctionForDay;


    /// <inheritdoc/>
    public bool CreateCheckedTime(int[] timeValues, TimeZones timezone, double lmtOffset, bool dst, out FullTime fullTime)
    {

        string fullText = "";
        bool success = timeValues.Length is 3 or 2;

        if (success)
        {
            for (int i = 0; i < timeValues.Length; i++)
            {
                _timeValues[i] = timeValues[i];
            }
            success = CheckMinAndMaxValues(this._timeValues);
        }
        if (success)
        {
            CalculateUtAndCorrectionForDay(timezone, lmtOffset, dst);
            fullText = CreateFullText(timezone, lmtOffset, dst);
        }
        fullTime = new FullTime(this._timeValues, _ut, _correctionForDay, fullText);

        return success;
    }

    private string CreateFullText(TimeZones timezone, double lmtOffset, bool dst)
    {
        string timeZoneTextId = timezone.GetDetails().Text;
        string dstText = dst ? Rosetta.TextForId("common.dst.used") : Rosetta.TextForId("common.dst.notused");
        string lmtOffsetText = "";
        if (timezone == TimeZones.Lmt)
        {
            lmtOffsetText = " " + lmtOffset;
        }
        string fullText = $"{_timeValues[0]:d2}:{_timeValues[1]:d2}:{_timeValues[2]:d2} [{timeZoneTextId}]{lmtOffsetText} {dstText}";
        return fullText;
    }

    private static bool CheckMinAndMaxValues(int[] valuesToCheck)
    {
        bool result = !(valuesToCheck[0] < 0 || valuesToCheck[0] > 23);
        if (valuesToCheck[1] < 0 || valuesToCheck[1] > 59) result = false;
        if (valuesToCheck[2] < 0 || valuesToCheck[2] > 59) result = false;
        return result;
    }

    private void CalculateUtAndCorrectionForDay(TimeZones timezone, double lmtOffset, bool dst)
    {
        _ut = _timeValues[0] + ((double)_timeValues[1] / EnigmaConstants.MinutesPerHourDegree) + ((double)_timeValues[2] / EnigmaConstants.SecondsPerHourDegree);
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


