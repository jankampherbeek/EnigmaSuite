// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api;
using Enigma.Api.Calc;
using Enigma.Api.LocationAndTimeZones;
using Enigma.Domain.Dtos;
using Enigma.Domain.LocationsZones;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Parsers;
using Serilog;
using Location = Enigma.Domain.Dtos.Location;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for data input for a chart</summary>
public class RadixDataInputModel: DateTimeLocationModelBase
{
    private Dictionary<long, string> _retrievedRatings = new();
    private Dictionary<long, string> _retrievedCats = new();
    public List<string> AllRatings { get; }
    public List<string> AllCategories { get; }
    public List<Country> AllCountries;
    
    private readonly IJulianDayApi _julianDayApi;
    private readonly ILocationApi _locationApi;
    private readonly IChartCalculation _chartCalculation;
    private readonly ILocationConversion _locationConversion;
    private readonly IReferencesApi _referencesApi;

    public RadixDataInputModel(IGeoLongInputParser geoLongInputParser, IGeoLatInputParser geoLatInputParser,
        IDateInputParser dateInputParser, ITimeInputParser timeInputParser, IJulianDayApi julianDayApi, ILocationApi locationApi,
        IChartCalculation chartCalculation, ILocationConversion locationConversion, IReferencesApi referencesApi) : 
        base(dateInputParser, timeInputParser, geoLongInputParser, geoLatInputParser)
    {
        _locationConversion = locationConversion;
        _julianDayApi = julianDayApi;
        _locationApi = locationApi;
        _chartCalculation = chartCalculation;
        _referencesApi = referencesApi;
        AllRatings = new List<string>();
        AllCategories = new List<string>();

        PopulateRatings();
        PopulateCategories();
        PopulateCountries(); ;
    }

    public void CreateChartData(string nameId, string description, string source, string locationName, int chartCat, int rating)
    {
        var catId = _retrievedCats.Keys.ToList()[chartCat];
        var ratingId = _retrievedRatings.Keys.ToList()[rating];
        if (FullDate == null || FullTime == null) return;
        var id = ChartsIndexSequence.NewSequenceId();
        var metaData = CreateMetaData(nameId, description, source, locationName, catId, ratingId);
       
        SimpleDateTime dateTime = new(FullDate.YearMonthDay[0], FullDate.YearMonthDay[1], FullDate.YearMonthDay[2], 
            FullTime.Ut, FullDate.Calendar);
        var julianDayUt = _julianDayApi.GetJulianDay(dateTime).JulDayUt + FullTime.CorrectionForDay;
        var locNameCheckedForEmpty = string.IsNullOrEmpty(locationName) ? "Location undefined " : locationName + " ";        
        var fullLocationName = _locationConversion.CreateLocationDescription(locNameCheckedForEmpty, 
            FullGeoLatitude.Latitude, FullGeoLongitude.Longitude);
        
        
        Location? location = new(fullLocationName, FullGeoLongitude.Longitude, FullGeoLatitude.Latitude);        
        FullDateTime fullDateTime = new(FullDate.DateFullText, FullTime.TimeFullText, julianDayUt);
        ChartData chartData = new(id, metaData, location, fullDateTime);
        Log.Information("RadixDataInputModel.CreateChartData(): calculating chart via ChartCalculation");
        var chart = _chartCalculation.CalculateChart(chartData);
        var dataVaultCharts = DataVaultCharts.Instance;
        dataVaultCharts.AddNewChart(chart);
        dataVaultCharts.SetNewChartAdded(true);
    }

    private static MetaData CreateMetaData(string nameId, string description, string source, string locationName, 
        long chartCategory, long rating)
    {
        string nameIdText = string.IsNullOrWhiteSpace(nameId) ? "Anonymous" : nameId;
        string descriptionText = string.IsNullOrWhiteSpace(description) ? "No description" : description;
        string sourceText = string.IsNullOrWhiteSpace(source) ? "Source not defined" : source;
        string locationNameText = string.IsNullOrWhiteSpace(locationName) ? "No name for location" : locationName;
        return new MetaData(nameIdText, descriptionText, sourceText, locationNameText, chartCategory, rating);
    }
    
    private void PopulateRatings()
    {
        AllRatings.Clear();
        _retrievedRatings = _referencesApi.ReadAllRatings();
        foreach (var rating in _retrievedRatings)
        {
            AllRatings.Add(rating.Value);
        }
    }

    private void PopulateCategories()
    {
        AllCategories.Clear();
        _retrievedCats = _referencesApi.ReadAllChartCategories();
        foreach (var cat in _retrievedCats)
        {
            AllCategories.Add(cat.Value);
        }
    }

    private void PopulateCountries()
    {
        AllCountries = new List<Country>();
        AllCountries = _locationApi.GetAllCountries();
    }
    

    public List<City> CitiesForCountry(string countryCode)
    {
        return _locationApi.GetAllCitiesForCountry(countryCode);
    }

    public bool IsTimeZoneValid(string timeZone)
    {
        if (string.IsNullOrEmpty(timeZone)) return false;
        
        // Check format: [-][hh]:mm[:ss]
        var pattern = @"^-?\d{2}:\d{2}(:\d{2})?$";
        if (!System.Text.RegularExpressions.Regex.IsMatch(timeZone, pattern)) return false;
    
        // Parse the components
        var parts = timeZone.Split(':');
        var hours = int.Parse(parts[0].Replace("-", ""));
        var minutes = int.Parse(parts[1]);
        var seconds = parts.Length > 2 ? int.Parse(parts[2]) : 0;
    
        // Validate ranges
        if (hours > 23 || minutes > 59 || seconds > 59) return false;
    
        return true;
    }
}