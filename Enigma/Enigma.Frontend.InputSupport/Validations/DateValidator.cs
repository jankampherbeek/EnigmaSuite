// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.RequestResponse;
using Enigma.InputSupport.Interfaces;

namespace Enigma.InputSupport.Validations;


public class DateValidator : IDateValidator
{

    private readonly ICheckDateTimeHandler _checkDateTimeHandler;

    public DateValidator(ICheckDateTimeHandler dateTimeHandler)
    {
        _checkDateTimeHandler = dateTimeHandler;
    }

    public bool CreateCheckedDate(int[] dateValues, Calendars calendar, YearCounts yearCount, out FullDate? fullDate)
    {
        bool success = dateValues != null && dateValues.Length == 3 && CheckCalendarRules(dateValues, calendar, yearCount);
        fullDate = null;

        if (success)
        {
            string _fullDateText = CreateFullDateText(dateValues, calendar);
            fullDate = new FullDate(dateValues, calendar, _fullDateText);
        }
        return success;
    }

    private static string CreateFullDateText(int[] dateValues, Calendars calendar)
    {
        string _yearText = $"{dateValues[0]:D4}";
        if (dateValues[0] > 9999 || dateValues[0] < -9999)
        {
            _yearText = $"{dateValues[0]:D5}";
        }
        string _monthText = GetPostFixIdForResourceBundle(dateValues[1]);
        string _calendarText = calendar == Calendars.Gregorian ? "g" : "j";
        return $"[{_monthText}] {_yearText}, {dateValues[2]} [{_calendarText}]";
    }

    private static string GetPostFixIdForResourceBundle(int monthId)
    {
        string[] postFixes = new string[] { "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" };
        return postFixes[monthId - 1];
    }

    private bool CheckCalendarRules(int[] dateValues, Calendars calendar, YearCounts yearCount)
    {
        if (yearCount == YearCounts.BCE) dateValues[0] = -dateValues[0] + 1;
        SimpleDateTime simpleDateTime = new(dateValues[0], dateValues[1], dateValues[2], 0.0, calendar);
        CheckDateTimeRequest checkDateTimeRequest = new(simpleDateTime);
        CheckDateTimeResponse responseValidated = _checkDateTimeHandler.CheckDateTime(checkDateTimeRequest);
        return responseValidated.Validated;
    }

}

