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
    public FullDate? FullStartDate { get; set; }
    public FullDate? FullEndDate { get; set; }
    public Calendars Calendar { get; set; }
    public YearCounts YearCount { get; set; }

    private readonly IDateInputParser _dateInputParser;

    public ProgInputPeriodController(IDateInputParser dateInputParser)
    {
        _dateInputParser = dateInputParser;
    }

    public bool ProcessInput()
    {
        ActualErrorCodes = new List<int>();
        bool startDateSuccess = _dateInputParser.HandleDate(InputStartDate, Calendar, YearCount, out FullDate? fullStartDate);
        bool endDateSuccess = _dateInputParser.HandleDate(InputEndDate, Calendar, YearCount, out FullDate? fullEndDate);
        if (!startDateSuccess) ActualErrorCodes.Add(ErrorCodes.InvalidStartdate);
        if (!endDateSuccess) ActualErrorCodes.Add(ErrorCodes.InvalidEnddate);

        if (startDateSuccess && endDateSuccess && fullStartDate != null && fullEndDate != null)
        {
            FullStartDate = fullStartDate;
            FullEndDate = fullEndDate;
            return true;
        }
        else return false;
    }

}
