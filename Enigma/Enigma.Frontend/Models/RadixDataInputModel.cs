// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for data input for a chart</summary>
public class RadixDataInputModel: DateTimeLocationModelBase
{
    public List<string> AllRatings { get; }
    public List<string> AllCategories { get; }
    private readonly IJulianDayApi _julianDayApi;
    private readonly IChartCalculation _chartCalculation;


    public RadixDataInputModel(IGeoLongInputParser geoLongInputParser, IGeoLatInputParser geoLatInputParser,
        IDateInputParser dateInputParser, ITimeInputParser timeInputParser, IJulianDayApi julianDayApi,
        IChartCalculation chartCalculation) : 
        base(dateInputParser, timeInputParser, geoLongInputParser, geoLatInputParser)
    {
        _julianDayApi = julianDayApi;
        _chartCalculation = chartCalculation;
        AllRatings = new List<string>();
        AllCategories = new List<string>();

        PopulateRatings();
        PopulateCategories();
    }

    public void CreateChartData(string nameId, string description, string source, string locationName, ChartCategories chartCat, RoddenRatings rating)
    {
        if (FullDate == null || FullTime == null) return;
        int id = ChartsIndexSequence.NewSequenceId();
        MetaData metaData = CreateMetaData(nameId, description, source, locationName, chartCat, rating);
        SimpleDateTime dateTime = new(FullDate.YearMonthDay[0], FullDate.YearMonthDay[1], FullDate.YearMonthDay[2], 
            FullTime.Ut, FullDate.Calendar);
        double julianDayUt = _julianDayApi.GetJulianDay(dateTime).JulDayUt;         
        string locNameCheckedForEmpty = string.IsNullOrEmpty(locationName) ? "" : locationName + " ";        
        string fullLocationName = locNameCheckedForEmpty + FullGeoLongitude!.GeoLongFullText + " " + FullGeoLatitude!.GeoLatFullText;        
        Location location = new(fullLocationName, FullGeoLongitude.Longitude, FullGeoLatitude.Latitude);        
        FullDateTime fullDateTime = new(FullDate.DateFullText, FullTime.TimeFullText, julianDayUt);
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
        List<RoddenRatingDetails> ratingDetails = RoddenRatingsExtensions.AllDetails();
        foreach (var ratingDetail in ratingDetails)
        {
            AllRatings.Add(ratingDetail.Text);
        }
    }

    private void PopulateCategories()
    {
        List<ChartCategoryDetails> catDetails = ChartCategoriesExtensions.AllDetails();
        foreach (var catDetail in catDetails)
        {
            AllCategories.Add(catDetail.Text);
        }
    }
    
}