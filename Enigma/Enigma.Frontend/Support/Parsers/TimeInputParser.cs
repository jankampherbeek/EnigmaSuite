﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support.Conversions;
using Enigma.Frontend.Ui.Support.Validations;

namespace Enigma.Frontend.Ui.Support.Parsers;

/// <summary>Parse, validate and convert input for a date.</summary>
public interface ITimeInputParser
{
    public bool HandleTime(string inputTime, TimeZones timeZone, double lmtOffset, bool dst, out FullTime? fullTime);
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


    public bool HandleTime(string inputTime, TimeZones timeZone, double lmtOffset, bool dst, out FullTime? fullTime)
    {
        fullTime = null;
        (int[] timeValues, bool timeSuccess) = _valueRangeConverter.ConvertStringRangeToIntRange(inputTime, EnigmaConstants.SEPARATOR_TIME);
        bool validationSuccess = timeSuccess && _timeValidator.CreateCheckedTime(timeValues, timeZone, lmtOffset, dst, out fullTime);
        return validationSuccess;
    }
}

