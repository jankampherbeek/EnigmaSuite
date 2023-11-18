// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Helpers.Interfaces;

namespace Enigma.Frontend.Ui.Models;

public class CyclesSinglePositionsModel
{
    public FullDate StartDate { get; set; }
    public FullDate EndDate { get; set; }
    public List<string> AllCalendars { get; set; } = new();
    public List<string> AllCoordinates { get; set; } = new();
    
    private readonly IDateInputParser _dateInputParser;
    
    public CyclesSinglePositionsModel(IDateInputParser dateInputParser)
    {
        _dateInputParser = dateInputParser;
        PopulateCalendars();
        PopulateCoordinates();
        
    }
    
    public bool IsStartDateValid(string inputStartDate, Calendars calendar)
    {
        bool isValid = _dateInputParser.HandleDate(inputStartDate, calendar, YearCounts.Astronomical, out FullDate? fullDate);
        if (isValid) StartDate = fullDate;
        return isValid;
    }
    
    public bool IsEndDateValid(string inputEndDate, Calendars calendar)
    {
        bool isValid = _dateInputParser.HandleDate(inputEndDate, calendar, YearCounts.Astronomical, out FullDate? fullDate);
        if (isValid) EndDate = fullDate;
        return isValid;
    }
    
    private void PopulateCalendars()
    {
        List<CalendarDetails> calDetails = CalendarsExtensions.AllDetails();
        foreach (var calDetail in calDetails)
        {
            AllCalendars.Add(calDetail.TextFull);
        }
    }
    
    private void PopulateCoordinates()
    {
        List<CoordinateSystemDetails> coordDetails = CoordinateSystemsExtensions.AllDetails();
        foreach (var coordDetail in coordDetails)
        {
            AllCoordinates.Add(coordDetail.Text);
        }
    }
    
}