// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.Charts.Progressive.InputPeriod;

public sealed class ProgInputPeriodController
{
    public List<int> ActualErrorCodes { get; set; } = new();
    public string InputStartDate { get; set; } = "";
    public string InputEndDate { get; set; } = "";

    private readonly IDateInputParser _dateInputParser;

    public ProgInputPeriodController(IDateInputParser dateInputParser)
    {
        _dateInputParser = dateInputParser;
    }

    public bool ProcessInput()
    {
        Calendars calendar = Calendars.Gregorian;
        YearCounts yearCount = YearCounts.CE;

        ActualErrorCodes = new List<int>();
        bool startDateSuccess = _dateInputParser.HandleDate(InputStartDate, calendar, yearCount, out FullDate? fullStartDate);
        bool endDateSuccess = _dateInputParser.HandleDate(InputEndDate, calendar, yearCount, out FullDate? fullEndDate);
        if (!startDateSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_STARTDATE);
        if (!endDateSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_ENDDATE);

        if (startDateSuccess && endDateSuccess)
        {
            // TODO functionality: define simpledate, jd etc.

            return true;
        }
        else return false;
    }

}
