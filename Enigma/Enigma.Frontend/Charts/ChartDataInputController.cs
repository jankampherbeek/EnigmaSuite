// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.Astron;
using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.CalcVars;
using Enigma.Domain.Charts;
using Enigma.Domain.Constants;
using Enigma.Domain.DateTime;
using Enigma.Domain.Locational;
using Enigma.Domain.Positional;
using Enigma.Frontend.InputSupport.InputParsers;
using Enigma.Frontend.State;
using Enigma.Frontend.UiDomain;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Enigma.Frontend.Charts;

public class ChartDataInputController
{

    public string NameId { get; set; }
    public string Source { get; set; }
    public string Description { get; set; }
    public string LocationName { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public string InputDate { get; set; }
    public string InputTime { get; set; }
    public ChartCategories ChartCategory { get; set; }
    public RoddenRatings RoddenRating { get; set; }
    public Directions4GeoLat Direction4GeoLat { get; set; }
    public Directions4GeoLong Direction4GeoLong { get; set; }
    public Directions4GeoLong LmtDirection4GeoLong { get; set; }
    public Calendars Calendar { get; set; }
    public YearCounts YearCount { get; set; }
    public bool Dst { get; set; }
    public TimeZones TimeZone { get; set; }
    public string LmtOffset { get; set; }
    public List<int> ActualErrorCodes { get; set; }
    public int ERR_INVALID_DATE { get; private set; }

    private readonly IDateInputParser _dateInputParser;
    private readonly ITimeInputParser _timeInputParser;
    private readonly IGeoLatInputParser _geoLatInputParser;
    private readonly IGeoLongInputParser _geoLongInputParser;
    private readonly IJulianDayApi _julianDayApi;
    private readonly IChartAllPositionsApi _chartAllPositionsApi;
   // private readonly MainController _chartStartController;
    private Calendars _cal;
    private List<FullSolSysPointPos> _solarSystemPointPositions;
    private FullHousesPositions _mundanePositions;

    public ChartDataInputController(IDateInputParser dateInputParser, ITimeInputParser timeInputParser, IGeoLatInputParser geoLatInputParser, IGeoLongInputParser geoLongInputParser,
                                    IJulianDayApi julianDayApi, IChartAllPositionsApi chartAllPositionsApi)
    {
        _dateInputParser = dateInputParser;
        _timeInputParser = timeInputParser;
        _geoLatInputParser = geoLatInputParser;
        _geoLongInputParser = geoLongInputParser;
        _julianDayApi = julianDayApi;
        _chartAllPositionsApi = chartAllPositionsApi;
      //  _chartStartController = chartsStartController;
    }

    /// <summary>
    /// Retrieve calculation preferences from active modus. Currently uses hardcoded values.
    /// </summary>
    /// TODO: replace hardocded values with a lookup from the active modus.
    private CalculationPreferences RetrieveCalculationPreferences() {

        ImmutableArray<SolarSystemPoints> solarSystemPoints = ImmutableArray.Create(new SolarSystemPoints[] { 
            SolarSystemPoints.Sun,
            SolarSystemPoints.Moon,
            SolarSystemPoints.Mercury,
            SolarSystemPoints.Venus,
            SolarSystemPoints.Mars,
            SolarSystemPoints.Jupiter,
            SolarSystemPoints.Saturn,
            SolarSystemPoints.Uranus,
            SolarSystemPoints.Neptune,
            SolarSystemPoints.Pluto,
            SolarSystemPoints.Chiron,
            SolarSystemPoints.TrueNode
        });
        return new CalculationPreferences(solarSystemPoints, ZodiacTypes.Tropical, Ayanamshas.None, ObserverPositions.GeoCentric, ProjectionTypes.twoDimensional, HouseSystems.Placidus);
    }


    public bool ProcessInput()
    {
        ActualErrorCodes = new List<int>();
        _cal = Calendar;
        FullDate? fullDate = null;
        FullTime? fullTime = null;
        FullGeoLongitude? fullGeoLongitude = null;
        FullGeoLatitude? fullGeoLatitude = null;
        FullGeoLongitude? fullLmtOffset = null;

        // todo validate and define lmtoffset
        double lmtOffset = 0.0;
        bool dateSuccess = _dateInputParser.HandleGeoLong(InputDate, Calendar, YearCount, out fullDate);
        bool timeSuccess = _timeInputParser.HandleTime(InputTime, TimeZone, lmtOffset, out fullTime);
        bool geoLongSuccess = _geoLongInputParser.HandleGeoLong(Longitude, Direction4GeoLong, out fullGeoLongitude);
        bool geoLatSuccess = _geoLatInputParser.HandleGeoLat(Latitude, Direction4GeoLat, out fullGeoLatitude);
        bool lmtSuccess = _geoLongInputParser.HandleGeoLong(LmtOffset, LmtDirection4GeoLong, out fullLmtOffset);
        
        if (!dateSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
        if (!timeSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_TIME);
        if (!geoLongSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_GEOLON);
        if (!geoLatSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_GEOLAT);
        if (!lmtSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_GEOLON_LMT);

        if (dateSuccess && timeSuccess && geoLongSuccess && geoLatSuccess && lmtSuccess)
        {
            SimpleDateTime dateTime = new(fullDate.YearMonthDay[0], fullDate.YearMonthDay[1], fullDate.YearMonthDay[2], fullTime.Ut, _cal);
            JulianDayRequest julianDayRequest = new JulianDayRequest(dateTime);
            double julianDayUt = _julianDayApi.getJulianDay(julianDayRequest).JulDayUt;
            Location location = new Location(LocationName, fullGeoLongitude.Longitude, fullGeoLatitude.Latitude);
            SolSysPointsRequest solSysPointsRequest = new SolSysPointsRequest(julianDayUt, location, RetrieveCalculationPreferences());
            ChartAllPositionsRequest chartAllPositionsRequest = new ChartAllPositionsRequest(solSysPointsRequest, HouseSystems.Placidus);   // TODO remove housesystem, is already part of CalculationPreferences
            ChartAllPositionsResponse chartAllPositionsResponse = _chartAllPositionsApi.getChart(chartAllPositionsRequest);
            if (chartAllPositionsResponse.Success)
            {
                _solarSystemPointPositions = chartAllPositionsResponse.SolarSystemPointPositions;
                _mundanePositions = chartAllPositionsResponse.MundanePositions;
                FullDateTime fullDateTime = new FullDateTime(fullDate.DateFullText, fullTime.TimeFullText, julianDayUt);
                MetaData metaData = new MetaData(NameId, Description, Source, ChartCategory, RoddenRating);
                // Todo retrieve id from database, use local counter for tempId
                int id = 1000;
                int tempId = 1;
                ChartData chartData = new ChartData(id, tempId, metaData, location, fullDateTime);
                CalculatedChart chart = new CalculatedChart(_solarSystemPointPositions, _mundanePositions, chartData);

                DataVault dataVault = DataVault.Instance;
                dataVault.AddNewChart(chart);

                return true;
            }
            else
            {
                // TODO handle error conditions
                return false;
            }
        }
        else return false;
    }






}