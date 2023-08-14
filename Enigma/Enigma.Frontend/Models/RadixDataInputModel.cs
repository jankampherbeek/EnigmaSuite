// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Charts;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for data input for a chart</summary>
public class RadixDataInputModel
{
    public List<string> AllRatings { get; }
    public List<string> AllCategories { get; }
    public List<string> AllDirectionsForLatitude { get; }
    public List<string> AllDirectionsForLongitude { get; }
    public List<string> AllCalendars { get; }
    public List<string> AllYearCounts { get; }
    public List<string> AllTimeZones { get; }

    private readonly IGeoLatInputParser _geoLatInputParser;
    private readonly IGeoLongInputParser _geoLongInputParser;
    private readonly IDateInputParser _dateInputParser;
    private readonly ITimeInputParser _timeInputParser;
    private readonly IJulianDayApi _julianDayApi;
    private readonly IChartCalculation _chartCalculation;

    private FullGeoLatitude? _fullGeoLatitude;
    private FullGeoLongitude? _fullGeoLongitude;
    private FullGeoLongitude? _fullLmtGeoLongitude;
    private FullDate? _fullDate;
    private FullTime? _fullTime;



    public RadixDataInputModel(IGeoLongInputParser geoLongInputParser, IGeoLatInputParser geoLatInputParser,
        IDateInputParser dateInputParser, ITimeInputParser timeInputParser, IJulianDayApi julianDayApi,
        IChartCalculation chartCalculation)
    {
        _geoLongInputParser = geoLongInputParser;
        _geoLatInputParser = geoLatInputParser;
        _dateInputParser = dateInputParser;
        _timeInputParser = timeInputParser;
        _julianDayApi = julianDayApi;
        _chartCalculation = chartCalculation;

        AllRatings = new List<string>();
        AllCategories = new List<string>();
        AllDirectionsForLatitude = new List<string>();
        AllDirectionsForLongitude = new List<string>();
        AllCalendars = new List<string>();
        AllYearCounts = new List<string>();
        AllTimeZones = new List<string>();
        PopulateDirectionsForLatitude();
        PopulateRatings();
        PopulateCategories();
        PopulateDirectionsForLongitude();
        PopulateCalendars();
        PopulateYearCounts();
        PopulateTimezones();
    }

    public void CreateChartData(string nameId, string description, string source, string locationName, ChartCategories chartCat, RoddenRatings rating)
    {
        if (_fullDate == null || _fullTime == null) return;
        int id = ChartsIndexSequence.NewSequenceId();
        MetaData metaData = CreateMetaData(nameId, description, source, locationName, chartCat, rating);
        SimpleDateTime dateTime = new(_fullDate.YearMonthDay[0], _fullDate.YearMonthDay[1], _fullDate.YearMonthDay[2], 
            _fullTime.Ut, _fullDate.Calendar);
        double julianDayUt = _julianDayApi.GetJulianDay(dateTime).JulDayUt;         
        string locNameCheckedForEmpty = string.IsNullOrEmpty(locationName) ? "" : locationName + " ";        
        string fullLocationName = locNameCheckedForEmpty + _fullGeoLongitude!.GeoLongFullText + " " + _fullGeoLatitude!.GeoLatFullText;        
        Location location = new(fullLocationName, _fullGeoLongitude.Longitude, _fullGeoLatitude.Latitude);        
        FullDateTime fullDateTime = new(_fullDate.DateFullText, _fullTime.TimeFullText, julianDayUt);
        ChartData chartData = new(id, metaData, location, fullDateTime);
        CalculatedChart chart = _chartCalculation.CalculateChart(chartData);
        DataVault dataVault = DataVault.Instance;
        dataVault.AddNewChart(chart);
        dataVault.SetNewChartAdded(true);


    }

    private static MetaData CreateMetaData(string nameId, string description, string source, string locationName, 
        ChartCategories chartCategory, RoddenRatings rating)
    {
        string nameIdText = string.IsNullOrWhiteSpace(nameId) ? "Anonymous" : nameId;
        string descriptionText = string.IsNullOrWhiteSpace(description) ? "No description" : description;
        string sourceText = string.IsNullOrWhiteSpace(source) ? "Source not defined" : source;
        string locationNameText = string.IsNullOrWhiteSpace(locationName) ? "No name for location" : locationName;
        return new MetaData(nameIdText, descriptionText, sourceText, locationNameText, chartCategory, rating);
    }
    
    private void PopulateRatings()
    {
        List<RoddenRatingDetails> ratingDetails = RoddenRatings.Unknown.AllDetails();
        foreach (var ratingDetail in ratingDetails)
        {
            AllRatings.Add(ratingDetail.Text);
        }
    }

    private void PopulateCategories()
    {
        List<ChartCategoryDetails> catDetails = ChartCategories.Unknown.AllDetails();
        foreach (var catDetail in catDetails)
        {
            AllCategories.Add(catDetail.Text);
        }
    }

    private void PopulateDirectionsForLatitude()
    {
        List<Directions4GeoLatDetails> geoLatDetails = Directions4GeoLat.North.AllDetails();
        foreach (var geoLatDetail in geoLatDetails)
        {
            AllDirectionsForLatitude.Add(geoLatDetail.Text);
        }
    }
    
    private void PopulateDirectionsForLongitude()
    {
        List<Directions4GeoLongDetails> geoLongDetails = Directions4GeoLong.East.AllDetails();
        foreach (var geoLongDetail in geoLongDetails)
        {
            AllDirectionsForLongitude.Add(geoLongDetail.Text);
        }
    }

    private void PopulateCalendars()
    {
        List<CalendarDetails> calDetails = Calendars.Gregorian.AllDetails();
        foreach (var calDetail in calDetails)
        {
            AllCalendars.Add(calDetail.TextFull);
        }
    }

    private void PopulateYearCounts()
    {
        List<YearCountDetails> ycDetails = YearCounts.Astronomical.AllDetails();
        foreach (var ycDetail in ycDetails)
        {
            AllYearCounts.Add(ycDetail.Text);
        }
    }

    private void PopulateTimezones()
    {
        List<TimeZoneDetails> tzDetails = TimeZones.Ut.AllDetails();
        foreach (var tzDetail in tzDetails)
        {
            AllTimeZones.Add(tzDetail.Text);
        }
    }

    public bool IsGeoLatValid(string latitude, Directions4GeoLat dir)
    {
        bool isValid = _geoLatInputParser.HandleGeoLat(latitude, dir, out FullGeoLatitude? fullGeoLatitude);
        if (isValid) _fullGeoLatitude = fullGeoLatitude;
        return isValid;
    }
    
    public bool IsGeoLongValid(string longitude, Directions4GeoLong dir)
    {
        bool isValid = _geoLongInputParser.HandleGeoLong(longitude, dir, out FullGeoLongitude? fullGeoLongitude);
        if (isValid) _fullGeoLongitude = fullGeoLongitude;
        return isValid;
    }
    
    public bool IsLmtGeoLongValid(string lmtLongitude, Directions4GeoLong dir)
    {
        bool isValid = _geoLongInputParser.HandleGeoLong(lmtLongitude, dir, out FullGeoLongitude? fullLmtGeoLongitude);
        if (isValid) _fullLmtGeoLongitude = fullLmtGeoLongitude;
        return isValid;
    }
    
    public bool IsDateValid(string inputDate, Calendars calendar, YearCounts yearCount)
    {
        bool isValid = _dateInputParser.HandleDate(inputDate, calendar, yearCount, out FullDate? fullDate);
        if (isValid) _fullDate = fullDate;
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
        if (isValid) _fullTime = fullTime;
        return isValid;
    }
    
}