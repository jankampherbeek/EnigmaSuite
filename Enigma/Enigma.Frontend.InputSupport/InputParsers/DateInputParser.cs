// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Interfaces;

namespace Enigma.Frontend.Helpers.InputParsers;




/// <inheritdoc/>
public class DateInputParser : IDateInputParser
{
    private readonly IValueRangeConverter _valueRangeConverter;
    private readonly IDateValidator _dateValidator;

    public DateInputParser(IValueRangeConverter valueRangeConverter, IDateValidator dateValidator)
    {
        _valueRangeConverter = valueRangeConverter;
        _dateValidator = dateValidator;
    }


    public bool HandleDate(string inputDate, Calendars calendar, YearCounts yearCount, out FullDate? fullDate)
    {
        fullDate = null;
        bool validationSuccess;
        (int[] dateNumbers, bool dateSuccess) = _valueRangeConverter.ConvertStringRangeToIntRange(inputDate, EnigmaConstants.SEPARATOR_DATE);
        if (dateSuccess)
        {
            validationSuccess = _dateValidator.CreateCheckedDate(dateNumbers, calendar, yearCount, out fullDate);
        }
        else
        {
            validationSuccess = false;
        }
        return validationSuccess;
    }
}




