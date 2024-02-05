// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using System.Collections.Generic;
using Enigma.Api.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Interfaces;
using Serilog;

namespace Enigma.Frontend.Ui.Support.Validations;


public class DateValidator : IDateValidator
{

    private readonly IDateTimeApi _dateTimeApi;

    public DateValidator(IDateTimeApi dateTimeHandler)
    {
        _dateTimeApi = dateTimeHandler;
    }

    public bool CreateCheckedDate(int[] dateValues, Calendars calendar, YearCounts yearCount, out FullDate? fullDate)
    {
        Log.Information("DateValidator.CreateCheckedDate(): calls private method CheckCalendarRules()");
        bool success = dateValues is { Length: 3 } && CheckCalendarRules(dateValues, calendar, yearCount);
        fullDate = null;

        if (!success) return success;
        string fullDateText = CreateFullDateText(dateValues, calendar);
        fullDate = new FullDate(dateValues, calendar, fullDateText);
        return success;
    }

    private static string CreateFullDateText(IReadOnlyList<int> dateValues, Calendars calendar)
    {
        string yearText = $"{dateValues[0]:D4}";
        if (dateValues[0] > 9999 || dateValues[0] < -9999)
        {
            yearText = $"{dateValues[0]:D5}";
        }
        string monthText = GetPostFixIdForResourceBundle(dateValues[1]);
        string calendarText = calendar == Calendars.Gregorian ? "g" : "j";
        return $"[{monthText}] {yearText}, {dateValues[2]} [{calendarText}]";
    }

    private static string GetPostFixIdForResourceBundle(int monthId)
    {
        string[] postFixes = { "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" };
        return postFixes[monthId - 1];
    }

    private bool CheckCalendarRules(IList<int> dateValues, Calendars calendar, YearCounts yearCount)
    {
        if (yearCount == YearCounts.BCE) dateValues[0] = -dateValues[0] + 1;
        SimpleDateTime simpleDateTime = new(dateValues[0], dateValues[1], dateValues[2], 0.0, calendar);
        Log.Information("DateValidator.CheckCalendarRules(): calling DateTaimeApi.CheckDateTime()");
        return _dateTimeApi.CheckDateTime(simpleDateTime);
    }

}

