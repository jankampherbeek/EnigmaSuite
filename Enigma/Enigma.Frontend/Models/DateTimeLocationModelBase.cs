using System.Collections.Generic;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Parsers;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Parent for models that handle date, time and/or location. </summary>
public abstract class DateTimeLocationModelBase
{
    private Rosetta _rosetta = Rosetta.Instance;
    private readonly IDateInputParser _dateInputParser;
    private readonly ITimeInputParser _timeInputParser;
    private readonly IGeoLongInputParser _geoLongInputParser;
    private readonly IGeoLatInputParser _geoLatInputParser;
    protected FullDate? FullDate;
    protected FullDate? SecondFullDate;
    protected FullTime? FullTime;
    private FullGeoLongitude? _fullLmtGeoLongitude;
    protected FullGeoLongitude? FullGeoLongitude;
    protected FullGeoLatitude? FullGeoLatitude;

    public List<string> AllDirectionsForLatitude { get; }
    public List<string> AllDirectionsForLongitude { get; }
    public List<string> AllCalendars { get; }
    public List<string> AllYearCounts { get; }
    public List<string> AllTimeZones { get; }

    protected DateTimeLocationModelBase(IDateInputParser dateInputParser, 
        ITimeInputParser timeInputParser,
        IGeoLongInputParser geoLongInputParser,
        IGeoLatInputParser geoLatInputParser)
    {
        _dateInputParser = dateInputParser;
        _timeInputParser = timeInputParser;
        _geoLongInputParser = geoLongInputParser;
        _geoLatInputParser = geoLatInputParser;
        AllDirectionsForLatitude = new List<string>();
        AllDirectionsForLongitude = new List<string>();
        AllCalendars = new List<string>();
        AllYearCounts = new List<string>();
        AllTimeZones = new List<string>();
        PopulateDirectionsForLatitude();
        PopulateDirectionsForLongitude();
        PopulateCalendars();
        PopulateYearCounts();
        PopulateTimezones();
    }
    
    
    public bool IsDateValid(string inputDate, Calendars calendar, YearCounts yearCount)
    {
        Log.Information("DateTimeLocationModelBase.IsDateValid(): calls DateInputParser.HandleDate()");
        bool isValid = _dateInputParser.HandleDate(inputDate, calendar, yearCount, out FullDate? fullDate);
        if (isValid) FullDate = fullDate;
        return isValid;
    }
    
    public bool IsSecondDateValid(string inputDate, Calendars calendar, YearCounts yearCount)
    {
        bool isValid = _dateInputParser.HandleDate(inputDate, calendar, yearCount, out FullDate? fullDate);
        if (isValid) SecondFullDate = fullDate;
        return isValid;
    }
    
    public bool IsTimeValid(string inputTime, TimeZones timeZone, bool dst)
    {
        double offsetLmt = 0.0;
        if (timeZone == TimeZones.Lmt)
        {
            
            if (_fullLmtGeoLongitude != null) offsetLmt = _fullLmtGeoLongitude.Longitude / 15.0;
        }
        bool isValid = _timeInputParser.HandleTime(inputTime, timeZone, offsetLmt, dst, out FullTime? fullTime);
        if (isValid) FullTime = fullTime;
        return isValid;
    }
    
    public bool IsLmtGeoLongValid(string lmtLongitude, Directions4GeoLong dir)
    {
        bool isValid = _geoLongInputParser.HandleGeoLong(lmtLongitude, dir, out FullGeoLongitude? fullLmtGeoLongitude);
        if (isValid) _fullLmtGeoLongitude = fullLmtGeoLongitude;
        return isValid;
    }
    
    public bool IsGeoLongValid(string longitude, Directions4GeoLong dir)
    {
        bool isValid = _geoLongInputParser.HandleGeoLong(longitude, dir, out FullGeoLongitude? fullGeoLongitude);
        if (isValid) FullGeoLongitude = fullGeoLongitude;
        return isValid;
    }
    
    public bool IsGeoLatValid(string latitude, Directions4GeoLat dir)
    {
        bool isValid = _geoLatInputParser.HandleGeoLat(latitude, dir, out FullGeoLatitude? fullGeoLatitude);
        if (isValid) FullGeoLatitude = fullGeoLatitude;
        return isValid;
    }
    
    private void PopulateDirectionsForLatitude()
    {
        List<Directions4GeoLatDetails> geoLatDetails = Directions4GeoLatExtensions.AllDetails();
        foreach (var geoLatDetail in geoLatDetails)
        {
            AllDirectionsForLatitude.Add(geoLatDetail.Text);
        }
    }
    
    private void PopulateDirectionsForLongitude()
    {
        List<Directions4GeoLongDetails> geoLongDetails = Directions4GeoLongExtensions.AllDetails();
        foreach (var geoLongDetail in geoLongDetails)
        {
            AllDirectionsForLongitude.Add(geoLongDetail.Text);
        }
    }

    private void PopulateCalendars()
    {
        List<CalendarDetails> calDetails = CalendarsExtensions.AllDetails();
        foreach (var calDetail in calDetails)
        {
            AllCalendars.Add(_rosetta.GetText(calDetail.RbKey));
        }
    }

    private void PopulateYearCounts()
    {
        List<YearCountDetails> ycDetails = YearCountsExtensions.AllDetails();
        foreach (var ycDetail in ycDetails)
        {
            AllYearCounts.Add(ycDetail.Text);
        }
    }

    private void PopulateTimezones()
    {
        List<TimeZoneDetails> tzDetails = TimeZonesExtensions.AllDetails();
        foreach (var tzDetail in tzDetails)
        {
            AllTimeZones.Add(tzDetail.Text);
        }
    }
}