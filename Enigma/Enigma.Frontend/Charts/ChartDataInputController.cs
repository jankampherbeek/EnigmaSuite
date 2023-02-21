// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;
using Enigma.Domain.Constants;
using Enigma.Domain.Points;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.Charts;

public sealed class ChartDataInputController
{

    public string NameId { get; set; } = "";
    public string Source { get; set; } = "";
    public string Description { get; set; } = "";
    public string LocationName { get; set; } = "";
    public string Longitude { get; set; } = "";
    public string Latitude { get; set; } = "";
    public string InputDate { get; set; } = "";
    public string InputTime { get; set; } = "";
    public ChartCategories ChartCategory { get; set; } = ChartCategories.Unknown;
    public RoddenRatings RoddenRating { get; set; } = RoddenRatings.Unknown;
    public Directions4GeoLat Direction4GeoLat { get; set; } = Directions4GeoLat.North;
    public Directions4GeoLong Direction4GeoLong { get; set; } = Directions4GeoLong.East;
    public Directions4GeoLong LmtDirection4GeoLong { get; set; } = Directions4GeoLong.East;
    public Calendars Calendar { get; set; } = Calendars.Gregorian;
    public YearCounts YearCount { get; set; } = YearCounts.CE;
    public bool Dst { get; set; } = false;
    public TimeZones TimeZone { get; set; } = TimeZones.UT;
    public string LmtOffset { get; set; } = "";
    public List<int> ActualErrorCodes { get; set; } = new();
    public int ERR_INVALID_DATE { get; private set; } = 0;

    private readonly IDateInputParser _dateInputParser;
    private readonly ITimeInputParser _timeInputParser;
    private readonly IGeoLatInputParser _geoLatInputParser;
    private readonly IGeoLongInputParser _geoLongInputParser;
    private readonly IJulianDayApi _julianDayApi;
    private readonly DataVault _dataVault;
    private readonly IChartCalculation _chartCalculation;

    public ChartDataInputController(IDateInputParser dateInputParser, ITimeInputParser timeInputParser, IGeoLatInputParser geoLatInputParser, IGeoLongInputParser geoLongInputParser,
                                    IJulianDayApi julianDayApi, IChartCalculation chartCalculation)
    {
        _dateInputParser = dateInputParser;
        _timeInputParser = timeInputParser;
        _geoLatInputParser = geoLatInputParser;
        _geoLongInputParser = geoLongInputParser;
        _julianDayApi = julianDayApi;
        _dataVault = DataVault.Instance;
        _chartCalculation = chartCalculation;
    }

    public void InitializeDataVault()
    {
        _dataVault.SetNewChartAdded(false);
    }


    public bool ProcessInput()
    {
        ActualErrorCodes = new List<int>();
        Calendars cal = Calendar;

        // todo validate and define lmtoffset
        double lmtOffset = 0.0;
        bool dateSuccess = _dateInputParser.HandleGeoLong(InputDate, Calendar, YearCount, out FullDate? fullDate);
        bool timeSuccess = _timeInputParser.HandleTime(InputTime, TimeZone, lmtOffset, out FullTime? fullTime);
        bool geoLongSuccess = _geoLongInputParser.HandleGeoLong(Longitude, Direction4GeoLong, out FullGeoLongitude? fullGeoLongitude);
        bool geoLatSuccess = _geoLatInputParser.HandleGeoLat(Latitude, Direction4GeoLat, out FullGeoLatitude? fullGeoLatitude);
        bool lmtSuccess = _geoLongInputParser.HandleGeoLong(LmtOffset, LmtDirection4GeoLong, out FullGeoLongitude? _);

        if (!dateSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
        if (!timeSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_TIME);
        if (!geoLongSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_GEOLON);
        if (!geoLatSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_GEOLAT);
        if (!lmtSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_GEOLON_LMT);

        if (dateSuccess && timeSuccess && geoLongSuccess && geoLatSuccess && lmtSuccess && fullDate != null && fullTime != null)
        {
            SimpleDateTime dateTime = new(fullDate.YearMonthDay[0], fullDate.YearMonthDay[1], fullDate.YearMonthDay[2], fullTime.Ut, cal);
            double julianDayUt = _julianDayApi.GetJulianDay(dateTime).JulDayUt;
            string locNameCheckedForEmpty = string.IsNullOrEmpty(LocationName) ? "" : LocationName + " ";
            string fullLocationName = locNameCheckedForEmpty + fullGeoLongitude!.GeoLongFullText + " " + fullGeoLatitude!.GeoLatFullText;
            Location location = new(fullLocationName, fullGeoLongitude.Longitude, fullGeoLatitude.Latitude);
            FullDateTime fullDateTime = new(fullDate.DateFullText, fullTime.TimeFullText, julianDayUt);
            MetaData metaData = CreateMetaData(NameId, Description, Source, LocationName, ChartCategory, RoddenRating);
            // Todo 0.1   retrieve id from database, use local counter for tempId
            int id = ChartsIndexSequence.NewSequenceId();
            int tempId = 1;
            ChartData chartData = new(id, tempId, metaData, location, fullDateTime);
            CalculatedChart chart = _chartCalculation.CalculateChart(chartData);
            _dataVault.AddNewChart(chart);
            _dataVault.SetNewChartAdded(true);
            return true;
        }
        else return false;
    }

    private static MetaData CreateMetaData(string nameId, string description, string source, string locationName, ChartCategories chartCategory, RoddenRatings rating)
    {
        string nameIdText = string.IsNullOrWhiteSpace(nameId) ? Rosetta.TextForId("charts.positions.chartname.empty") : nameId;
        string descriptionText = string.IsNullOrWhiteSpace(description) ? Rosetta.TextForId("charts.positions.description.empty") : description;
        string sourceText = string.IsNullOrWhiteSpace(source) ? Rosetta.TextForId("charts.positions.source.empty") : source;
        string locationNameText = string.IsNullOrWhiteSpace(locationName) ? Rosetta.TextForId("charts.positions.locationname.empty") : locationName; 
        return new MetaData(nameIdText, descriptionText, sourceText, locationNameText, chartCategory, rating);

    }

    public static void ShowHelp()
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("ChartsDataInput");
        helpWindow.ShowDialog();
    }




}