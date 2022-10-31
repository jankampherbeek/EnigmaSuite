// Jan Kampherbeek, (c) 2022.
// TEnigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Constants;
using Enigma.Domain.Enums;
using Enigma.InputSupport.Interfaces;

namespace Enigma.InputSupport.InputParsers;


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

