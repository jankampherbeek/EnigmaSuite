// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Constants;
using Enigma.Domain.DateTime;

namespace Enigma.Frontend.InputSupport.Validations;

public interface IDateValidator
{
    /// <summary>
    /// Validate input and create a record FullDate.
    /// </summary>
    /// <param name="dateValues>Array with values for the date in the sequence: year, month, day.</param>
    /// <param name="calendar">The calendar that is used (Gregorian or Julian).</param>
    /// <param name="yearCount">The year count, this will be converted to an astronomical year count.</param>
    /// <param name="fullDate">The resulting record FullDate.</param>
    /// <param name="errorCodes">Errorcodes, if any.</param> 
    /// <returns>True if no error occurred, otherwise false.</returns>
    public bool CreateCheckedDate(int[] dateValues, Calendars calendar, YearCounts yearCount, out FullDate fullDate, out List<int> errorCodes);
}


public class DateValidator : IDateValidator
{
    private readonly ICheckDateTimeApi _checkDateTimeApi;
    private bool _success = true;
    private readonly List<int> _errorCodes = new();

    public DateValidator(ICheckDateTimeApi checkDateTimeApi)
    {
        _checkDateTimeApi = checkDateTimeApi;
    }

    public bool CreateCheckedDate(int[] dateValues, Calendars calendar, YearCounts yearCount, out FullDate? fullDate, out List<int> errorCodes)
    {
        _success = dateValues != null && dateValues.Length == 3 && CheckCalendarRules(dateValues, calendar, yearCount);
        fullDate = null;

        if (_success)
        {
            string _fullDateText = CreateFullDateText(dateValues, calendar);
            fullDate = new FullDate(dateValues, calendar, _fullDateText);
        }
        else _errorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
        errorCodes = _errorCodes;
        return _success;
    }

    private string CreateFullDateText(int[] dateValues, Calendars calendar)
    {
        string _yearText = $"{dateValues[0]:D4}";
        if (dateValues[0] > 9999 || dateValues[0] < -9999)
        {
            _yearText = $"{dateValues[0]:D5}";
        }
        string _monthText = GetPostFixIdForResourceBundle(dateValues[1]);
        string _calendarText = calendar == Calendars.Gregorian ? "g" : "j";
        return $"[month:{_monthText}] {_yearText}, {dateValues[2]} [{_calendarText}]";
    }

    private static string GetPostFixIdForResourceBundle(int monthId)
    {
        string[] postFixes = new string[] { "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" };
        return postFixes[monthId - 1];
    }

    private bool CheckCalendarRules(int[] dateValues, Calendars calendar, YearCounts yearCount)
    {
        if (yearCount == YearCounts.BCE) dateValues[0] = -(dateValues[0]) + 1;
        SimpleDateTime simpleDateTime = new(dateValues[0], dateValues[1], dateValues[2], 0.0, calendar);
        CheckDateTimeRequest checkDateTimeRequest = new(simpleDateTime);
        CheckDateTimeResponse responseValidated = _checkDateTimeApi.CheckDateTime(checkDateTimeRequest);
        return responseValidated.Validated;
    }

}

