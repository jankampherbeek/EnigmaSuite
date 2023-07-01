// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Helpers.Support;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.Charts.Progressive.InputEvent;

public sealed class ProgInputEventController
{

    public List<int> ActualErrorCodes { get; set; } = new();

    public FullDate CheckedDate { get; set; }
    public FullTime CheckedTime { get; set; }
    public FullGeoLongitude CheckedLongitude { get; set; }  
    public FullGeoLongitude CheckedLongitudeLmt { get; set; }
    public FullGeoLatitude CheckedLatitude { get; set; }


    public string Description { get; set; } = "";
    public string LocationName { get; set; } = "";
    public string Longitude { get; set; } = "";
    public string Latitude { get; set; } = "";
    public string InputDate { get; set; } = "";
    public string InputTime { get; set; } = "";
    public Directions4GeoLat Direction4GeoLat { get; set; } = Directions4GeoLat.North;
    public Directions4GeoLong Direction4GeoLong { get; set; } = Directions4GeoLong.East;
    public Directions4GeoLong LmtDirection4GeoLong { get; set; } = Directions4GeoLong.East;
    public Calendars Calendar { get; set; } = Calendars.Gregorian;
    public YearCounts YearCount { get; set; } = YearCounts.CE;
    public bool Dst { get; set; } = false;
    public TimeZones TimeZone { get; set; } = TimeZones.UT;
    public string LmtOffset { get; set; } = "";

    private IDateInputParser _dateInputParser;
    private ITimeInputParser _timeInputParser;
    private IGeoLatInputParser _geoLatInputParser;
    private IGeoLongInputParser _geoLongInputParser;



    public ProgInputEventController(IDateInputParser dateInputParser, ITimeInputParser timeInputParser, IGeoLatInputParser geoLatInputParser, IGeoLongInputParser geoLongInputParser)
    {
        _dateInputParser = dateInputParser;
        _timeInputParser = timeInputParser;
        _geoLatInputParser = geoLatInputParser;
        _geoLongInputParser = geoLongInputParser;
    }


    public bool ProcessInput()
    {
        ActualErrorCodes = new List<int>();


        if (Description == null || Description.Trim().Length == 0) { 
            Description = Rosetta.TextForId("charts.prog.event.defaultdescription") + " " + InputDate + " " + (InputTime);  
        }
        if (LocationName == null || LocationName.Trim().Length == 0)
        {
            LocationName = Rosetta.TextForId("charts.prog.event.defaultlocationname") + " " + InputDate + " " + (InputTime);
        }

        bool dateSuccess = _dateInputParser.HandleDate(InputDate, Calendar, YearCount, out FullDate? fullDate);
        bool geoLongSuccess = _geoLongInputParser.HandleGeoLong(Longitude, Direction4GeoLong, out FullGeoLongitude? fullGeoLongitude);
        bool geoLatSuccess = _geoLatInputParser.HandleGeoLat(Latitude, Direction4GeoLat, out FullGeoLatitude? fullGeoLatitude);
        bool lmtSuccess = _geoLongInputParser.HandleGeoLong(LmtOffset, LmtDirection4GeoLong, out FullGeoLongitude? fullGeoLongForLmt);
        double lmtOffset = 0.0;
        if (lmtSuccess && fullGeoLongForLmt != null) lmtOffset = ParseLmtOffset(fullGeoLongForLmt);
        bool timeSuccess = _timeInputParser.HandleTime(InputTime, TimeZone, lmtOffset, Dst, out FullTime? fullTime);

        if (!dateSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
        if (!timeSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_TIME);
        if (!geoLongSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_GEOLON);
        if (!geoLatSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_GEOLAT);
        if (!lmtSuccess) ActualErrorCodes.Add(ErrorCodes.ERR_INVALID_GEOLON_LMT);

        if (dateSuccess && geoLongSuccess && geoLatSuccess && lmtSuccess && timeSuccess && fullDate != null && fullTime != null &&  fullGeoLongitude != null && fullGeoLatitude != null && fullGeoLongForLmt != null)
        {
            CheckedDate = fullDate;
            CheckedTime = fullTime;
            CheckedLongitude = fullGeoLongitude;
            CheckedLatitude = fullGeoLatitude;
            CheckedLongitudeLmt = fullGeoLongForLmt;


            return true;
        }
        else return false;
    }



    private static double ParseLmtOffset(FullGeoLongitude fullGeoLongitude)
    {
        return fullGeoLongitude.Longitude / 15.0;
    }

}
