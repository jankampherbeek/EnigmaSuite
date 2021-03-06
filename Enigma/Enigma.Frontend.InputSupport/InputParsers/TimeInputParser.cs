// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.DateTime;
using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.InputSupport.Validations;

namespace Enigma.Frontend.InputSupport.InputParsers;

/// <summary>Parse, validate and convert input for a date.</summary>
public interface ITimeInputParser
{
    public bool HandleTime(string inputTime, TimeZones timeZone, double lmtOffset, out FullTime? fullTime);
}


/// <inheritdoc/>
public class TimeInputParser : ITimeInputParser
{
    private readonly IValueRangeConverter _valueRangeConverter;
    private readonly ITimeValidator _timeValidator;

    public TimeInputParser(IValueRangeConverter valueRangeConverter, ITimeValidator timeValidator)
    {
        _valueRangeConverter = valueRangeConverter;
        _timeValidator = timeValidator;
    }


    public bool HandleTime(string inputTime, TimeZones timeZone, double lmtOffset, out FullTime? fullTime)
    {
        fullTime = null;
        bool validationSuccess;
        (int[] timeValues, bool timeSuccess) = _valueRangeConverter.ConvertStringRangeToIntRange(inputTime, EnigmaConstants.SEPARATOR_TIME);
        if (timeSuccess)
        {
            validationSuccess = _timeValidator.CreateCheckedTime(timeValues, timeZone, lmtOffset, out fullTime);
        }
        else
        {
            validationSuccess = false;
        }
        return validationSuccess;
    }
}

