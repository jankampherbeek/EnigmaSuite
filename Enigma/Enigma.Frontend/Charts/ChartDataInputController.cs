// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Constants;
using Enigma.Domain.Enums;
using Enigma.Domain.RequestResponse;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.State;
using Serilog;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.Charts;

public class ChartDataInputController
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
    private readonly IChartAllPositionsApi _chartAllPositionsApi;
    private readonly DataVault _dataVault;
    private readonly Rosetta _rosetta = Rosetta.Instance;

    public ChartDataInputController(IDateInputParser dateInputParser, ITimeInputParser timeInputParser, IGeoLatInputParser geoLatInputParser, IGeoLongInputParser geoLongInputParser,
                                    IJulianDayApi julianDayApi, IChartAllPositionsApi chartAllPositionsApi)
    {
        _dateInputParser = dateInputParser;
        _timeInputParser = timeInputParser;
        _geoLatInputParser = geoLatInputParser;
        _geoLongInputParser = geoLongInputParser;
        _julianDayApi = julianDayApi;
        _chartAllPositionsApi = chartAllPositionsApi;
        _dataVault = DataVault.Instance;
    }

    /// <summary>
    /// Retrieve calculation preferences from active modus. Currently uses hardcoded values.
    /// </summary>
    /// TODO: replace hardocded values with a lookup from the active settings.
    private static CalculationPreferences RetrieveCalculationPreferences()
    {

        List<CelPoints> celPoints = new() {
            CelPoints.Sun,
            CelPoints.Moon,
            CelPoints.Mercury,
            CelPoints.Venus,
            CelPoints.Mars,
            CelPoints.Jupiter,
            CelPoints.Saturn,
            CelPoints.Uranus,
            CelPoints.Neptune,
            CelPoints.Pluto,
            CelPoints.Chiron,
            CelPoints.MeanNode
        };
        return new CalculationPreferences(celPoints, ZodiacTypes.Tropical, Ayanamshas.None, CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ProjectionTypes.TwoDimensional, HouseSystems.Placidus);
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
        bool lmtSuccess = _geoLongInputParser.HandleGeoLong(LmtOffset, LmtDirection4GeoLong, out FullGeoLongitude? fullLmtOffset);

        if (!dateSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
        if (!timeSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_TIME);
        if (!geoLongSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_GEOLON);
        if (!geoLatSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_GEOLAT);
        if (!lmtSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_GEOLON_LMT);

        if (dateSuccess && timeSuccess && geoLongSuccess && geoLatSuccess && lmtSuccess && fullDate != null && fullTime != null)
        {
            SimpleDateTime dateTime = new(fullDate.YearMonthDay[0], fullDate.YearMonthDay[1], fullDate.YearMonthDay[2], fullTime.Ut, cal);
            JulianDayRequest julianDayRequest = new(dateTime);
            double julianDayUt = _julianDayApi.GetJulianDay(julianDayRequest).JulDayUt;
            string locNameCheckedForEmpty = string.IsNullOrEmpty(LocationName) ? "" : LocationName + " ";
            string fullLocationName = locNameCheckedForEmpty + fullGeoLongitude.GeoLongFullText + " " + fullGeoLatitude.GeoLatFullText;
            Location location = new(fullLocationName, fullGeoLongitude.Longitude, fullGeoLatitude.Latitude);
            CelPointsRequest celPointsRequest = new(julianDayUt, location, RetrieveCalculationPreferences());
            ChartAllPositionsRequest chartAllPositionsRequest = new(celPointsRequest, HouseSystems.Placidus);   // TODO remove housesystem, is already part of CalculationPreferences
            ChartAllPositionsResponse chartAllPositionsResponse = _chartAllPositionsApi.GetChart(chartAllPositionsRequest);
            if (chartAllPositionsResponse.Success)
            {
                List<FullCelPointPos> celPointPositions = chartAllPositionsResponse.CelPointPositions;
                FullHousesPositions _mundanePositions = chartAllPositionsResponse.MundanePositions;
                FullDateTime fullDateTime = new(fullDate.DateFullText, fullTime.TimeFullText, julianDayUt);


                MetaData metaData = CreateMetaData(NameId, Description, Source, ChartCategory, RoddenRating);
                // Todo retrieve id from database, use local counter for tempId
                int id = 1000;
                int tempId = 1;
                ChartData chartData = new(id, tempId, metaData, location, fullDateTime);
                CalculatedChart chart = new(celPointPositions, _mundanePositions, chartData);
                _dataVault.AddNewChart(chart);
                _dataVault.SetNewChartAdded(true);
                return true;
            }
            else
            {
                Log.Error("");
                return false;
            }
        }
        else return false;
    }

    private MetaData CreateMetaData(string nameId, string description, string source, ChartCategories chartCategory, RoddenRatings rating)
    {
        string nameIdText = string.IsNullOrEmpty(nameId) ? _rosetta.TextForId("charts.positions.chartname.empty") : nameId;
        string descriptionText = string.IsNullOrEmpty(description) ? _rosetta.TextForId("charts.positions.description.empty") : description;
        string sourceText = string.IsNullOrEmpty(source) ? _rosetta.TextForId("charts.positions.source.empty") : source;
        return new MetaData(nameIdText, descriptionText, sourceText, chartCategory, rating);

    }






}