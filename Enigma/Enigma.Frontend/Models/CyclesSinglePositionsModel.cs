// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Interfaces;

namespace Enigma.Frontend.Ui.Models;

public class CyclesSinglePositionsModel
{
    public FullDate StartDate { get; set; }
    public FullDate EndDate { get; set; }
    public List<string> AllCalendars { get; set; } = new();
    public List<string> AllCoordinates { get; set; } = new();

    public List<string> AllAyanamshas { get; set; } = new();
    
    public List<string> AllObserverPositions { get; set; } = new();
    
    private readonly IDateInputParser _dateInputParser;
    
    public CyclesSinglePositionsModel(IDateInputParser dateInputParser)
    {
        _dateInputParser = dateInputParser;
        PopulateCalendars();
        PopulateCoordinates();
        PopulateAyanamshas();
        PopulateObserverPositions();
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
        List<CoordinateDetails> coordDetails = CoordinatesExtensions.AllDetails();
        foreach (CoordinateDetails coordDetail in 
                 coordDetails.Where(coordDetail => coordDetail.CoordinateSystem 
                     is CoordinateSystems.Ecliptical or CoordinateSystems.Equatorial))
        {
            AllCoordinates.Add(coordDetail.Text);
        }
    }
    
    private void PopulateAyanamshas()
    {
        List<AyanamshaDetails> ayanamshaDetails = AyanamshaExtensions.AllDetails();
        foreach (var ayanamshaDetail in ayanamshaDetails)
        {
            AllAyanamshas.Add(ayanamshaDetail.Text);
        }
    }
    
    private void PopulateObserverPositions()
    {
        IEnumerable<ObserverPositionDetails> obsPosDetails = ObserverPositionsExtensions.AllDetails();
        IEnumerable<ObserverPositionDetails> obsPosList = obsPosDetails.ToList();
 //       foreach (ObserverPositionDetails obsPosDetail in obsPosList.Where(obsPosDetail =>
 //                    obsPosDetail.Position is ObserverPositions.GeoCentric or ObserverPositions.HelioCentric))
 //       {
 //           AllObserverPositions.Add(obsPosDetail.Text);             
 //       }
    }
    
}