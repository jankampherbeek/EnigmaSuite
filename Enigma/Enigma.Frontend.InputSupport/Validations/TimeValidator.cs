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
    /// <param name="lmtOffset">If TimeZone is LMT this parameter contains a value for the time offset. Will be zero if TimeZone != LMT.</param>
    /// <param name="fullTime">Resulting record FullTime.</param>
    /// <returns>True if no error was found, otherwise false.</returns>
    public bool CreateCheckedTime(int[] timeValues, TimeZones timezone, double lmtOffset, out FullTime fullTime);
}


/// <inheritdoc/>
public class TimeValidator : ITimeValidator
{
    private readonly ITimeZoneSpecifications _timeZoneSpecifications;
    private bool _success = true;
    readonly int[] timeValues = new int[] { 0, 0, 0 };
    readonly int[] lmtOffsetValues = new int[] { 0, 0, 0 };
    private double _ut = 0.0;
    private int _correctionForDay = 0;

    public TimeValidator(ITimeZoneSpecifications timeZoneSpecifications)
    {
        _timeZoneSpecifications = timeZoneSpecifications;
    }

    /// <inheritdoc/>
    public bool CreateCheckedTime(int[] inputTimeValues, TimeZones timezone, double lmtOffset, out FullTime fullTime)
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
 
   //     if (timezone == TimeZones.LMT)
   //     {
   //         if (!CheckLmtOffset(inputLmtOffsetValues))
   //         {
   //             _success = false;
   //         }
   //     }

        if (_success)
        {
            CalculateUtAndCorrectionForDay(timezone, lmtOffset);
            _fullText = CreateFullText(timezone, lmtOffset);
        }
        fullTime = new FullTime(timeValues, _ut, _correctionForDay, _fullText);

        return _success;
    }

    private string CreateFullText(TimeZones timezone, double lmtOffset)
    {
        string _timeZoneTextId = _timeZoneSpecifications.DetailsForTimeZone(timezone).TextId;
        string _fullText = $"{timeValues[0]:d2}:{timeValues[1]:d2}:{timeValues[2]:d2} [{_timeZoneTextId}]";

        if (timezone == TimeZones.LMT)
        {
            _fullText += lmtOffset.ToString();
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

  /*  private bool CheckLmtOffset(int[] inputLmtOffsetValues)
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
  */
    private void CalculateUtAndCorrectionForDay(TimeZones timezone, double lmtOffset)
    {
        double _offset;
        _ut = timeValues[0] + (double)timeValues[1] / EnigmaConstants.MINUTES_PER_HOUR_DEGREE + (double)timeValues[2] / EnigmaConstants.SECONDS_PER_HOUR_DEGREE;
        if (timezone == TimeZones.LMT)
        {
            _offset = lmtOffset;
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


