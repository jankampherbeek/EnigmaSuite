// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Enigma.Api;
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
        long catId = _retrievedCats.Keys.ToList()[chartCat];
        long ratingId = _retrievedRatings.Keys.ToList()[rating];
        if (FullDate == null || FullTime == null) return;
        int id = ChartsIndexSequence.NewSequenceId();
        MetaData metaData = CreateMetaData(nameId, description, source, locationName, catId, ratingId);
       
        SimpleDateTime dateTime = new(FullDate.YearMonthDay[0], FullDate.YearMonthDay[1], FullDate.YearMonthDay[2], 
            FullTime.Ut, FullDate.Calendar);
        double julianDayUt = _julianDayApi.GetJulianDay(dateTime).JulDayUt + FullTime.CorrectionForDay;
        string locNameCheckedForEmpty = string.IsNullOrEmpty(locationName) ? "Location undefined " : locationName + " ";        
        string fullLocationName = _locationConversion.CreateLocationDescription(locNameCheckedForEmpty, 
            FullGeoLatitude.Latitude, FullGeoLongitude.Longitude);
        
        
        Location? location = new(fullLocationName, FullGeoLongitude.Longitude, FullGeoLatitude.Latitude);        
        FullDateTime fullDateTime = new(FullDate.DateFullText, FullTime.TimeFullText, julianDayUt);
        ChartData chartData = new(id, metaData, location, fullDateTime);
        Log.Information("RadixDataInputModel.CreateChartData(): calculating chart via ChartCalculation");
        CalculatedChart chart = _chartCalculation.CalculateChart(chartData);
        DataVaultCharts dataVaultCharts = DataVaultCharts.Instance;
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
    

    
}