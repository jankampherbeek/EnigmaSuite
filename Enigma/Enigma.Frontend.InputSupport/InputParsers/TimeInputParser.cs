// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Interfaces;

namespace Enigma.Frontend.Helpers.InputParsers;


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


    public bool HandleTime(string inputTime, TimeZones timeZone, double lmtOffset, bool dst, out FullTime? fullTime)
    {
        fullTime = null;
        bool validationSuccess;
        (int[] timeValues, bool timeSuccess) = _valueRangeConverter.ConvertStringRangeToIntRange(inputTime, EnigmaConstants.SeparatorTime);
        validationSuccess = timeSuccess && _timeValidator.CreateCheckedTime(timeValues, timeZone, lmtOffset, dst, out fullTime);
        return validationSuccess;
    }
}

