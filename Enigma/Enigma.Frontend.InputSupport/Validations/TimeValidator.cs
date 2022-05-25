// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.DateTime;

namespace Enigma.Frontend.InputSupport.Validations;

public interface ITimeValidator
{
    /// <summary>
    /// Validate input and create a record FullTime.
    /// </summary>
    /// <param name="timeValues">Array with integers for time in the sequence hour, minute, second. The value for second is optional.</param>
    /// <param name="timezone">The timezone that is used.</param>
    /// <param name="inputLmtOffsetValues">If TimeZone is LMT this parameter contains values for the time offset in the sequence hours, minutes, seconds. Ignore if TimeZone != LMT.</param>
    /// <param name="offSetPlus">True is offset for LMT is positieve (east), otherwise false. Ignore if TimeZone != LMT.</param>
    /// <param name="fullTime">Resulting record FullTime.</param>
    /// <param name="errorCodes">Errorcodes, if any.</param>
    /// <returns>True if no error was found, otherwise false.</returns>
    public bool CreateCheckedTime(int[] timeValues, TimeZones timezone, int[] inputLmtOffsetValues, bool offSetPlus, out FullTime fullTime, out List<int> errorCodes);
}


/// <inheritdoc/>
public class TimeValidator : ITimeValidator
{
    private readonly ITimeZoneSpecifications _timeZoneSpecifications;
    private int[] _lmtOffsetValues = new int[] {0, 0, 0 };
    private bool _success = true;
    private readonly List<int> _errorCodes = new();
    readonly int[] timeValues = new int[] { 0, 0, 0 };
    readonly int[] lmtOffsetValues = new int[] { 0, 0, 0 };
    private double _ut = 0.0;
    private int _correctionForDay = 0;

    public TimeValidator(ITimeZoneSpecifications timeZoneSpecifications)
    {
        _timeZoneSpecifications = timeZoneSpecifications;
    }

    /// <inheritdoc/>
    public bool CreateCheckedTime(int[] inputTimeValues, TimeZones timezone, int[] inputLmtOffsetValues, bool offSetPlus, out FullTime fullTime, out List<int> errorCodes)
    {

        string _fullText = "";
        _success = (inputTimeValues.Length == 3) || (inputTimeValues.Length == 2);

        if (_success)
        {
            for (int i = 0; i < inputTimeValues.Length; i++)
            {
                timeValues[i] = inputTimeValues[i];
            }
            _success = CheckMinAndMaxValues(timeValues);
        }

        if (!_success)
        {
            _errorCodes.Add(ErrorCodes.ERR_INVALID_TIME);
        }
 
        if (timezone == TimeZones.LMT)
        {
            if (!CheckLmtOffset(inputLmtOffsetValues))
            {
                _success = false;
                _errorCodes.Add(ErrorCodes.ERR_INVALID_OFFSET);
            }
        }

        if (_success)
        {
            CalculateUtAndCorrectionForDay(timezone);
            _fullText = CreateFullText(timezone, offSetPlus);
        }
        fullTime = new FullTime(timeValues, _ut, _correctionForDay, _fullText);
        errorCodes = _errorCodes;
        return _success;
    }

    private string CreateFullText(TimeZones timezone, bool offSetPlus)
    {
        string _timeZoneTextId = _timeZoneSpecifications.DetailsForTimeZone(timezone).TextId;
        string _fullText = $"{timeValues[0]:d2}:{timeValues[1]:d2}:{timeValues[2]:d2} [{_timeZoneTextId}]";

        if (timezone == TimeZones.LMT)
        {
            string _plusMinus = offSetPlus ? "+" : "-";
            _fullText += $" {_plusMinus}{_lmtOffsetValues[0]:d2}:{_lmtOffsetValues[1]:d2}:{_lmtOffsetValues[2]:d2}";
        }
        return _fullText;
    }

    private bool CheckMinAndMaxValues(int[] valuesToCheck)
    {
        bool result = true;
        if (valuesToCheck[0] < 0 || valuesToCheck[0] > 23) result = false;
        if (valuesToCheck[1] < 0 || valuesToCheck[1] > 59) result = false;
        if (valuesToCheck[2] < 0 || valuesToCheck[2] > 59) result = false;
        return result;
    }

    private bool CheckLmtOffset(int[] inputLmtOffsetValues)
    {
        bool lmtOffsetOk = (inputLmtOffsetValues.Length == 3) || (inputLmtOffsetValues.Length == 2);
        if (lmtOffsetOk)
        {
            for (int i = 0; i < inputLmtOffsetValues.Length; i++)
            {
                lmtOffsetValues[i] = inputLmtOffsetValues[i];
            }
            lmtOffsetOk = CheckMinAndMaxValues(lmtOffsetValues);
        }
        return lmtOffsetOk;
    }

    private void CalculateUtAndCorrectionForDay(TimeZones timezone)
    {
        double _offset;
        _ut = timeValues[0] + (double)timeValues[1] / EnigmaConstants.MINUTES_PER_HOUR_DEGREE + (double)timeValues[2] / EnigmaConstants.SECONDS_PER_HOUR_DEGREE;
        if (timezone == TimeZones.LMT)
        {
            _offset = _lmtOffsetValues[0] + (double)_lmtOffsetValues[1] / EnigmaConstants.MINUTES_PER_HOUR_DEGREE + (double)_lmtOffsetValues[2] / EnigmaConstants.SECONDS_PER_HOUR_DEGREE;
        }
        else
        {
            _offset = _timeZoneSpecifications.DetailsForTimeZone(timezone).OffsetFromUt;
        }
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


