// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;
using Enigma.Domain.DateTime;
using Enigma.Domain.Locational;
using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.InputSupport.InputParsers;
using System.Collections.Generic;

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



    public List<int> _errorCodes { get; set; }
    private readonly IValueRangeConverter _valueRangeConverter;
    private readonly IDateInputParser _dateInputParser;
    private readonly ITimeInputParser _timeInputParser;
    private readonly IGeoLatInputParser _geoLatInputParser;
    private readonly IGeoLongInputParser _geoLongInputParser;
    private Calendars _cal;
    private int[] _yearMonthDay;
    private double _ut = 0.0;

    public ChartDataInputController(IValueRangeConverter valueRangeConverter, IDateInputParser dateInputParser, ITimeInputParser timeInputParser,
        IGeoLatInputParser geoLatInputParser, IGeoLongInputParser geoLongInputParser)
    {
        _dateInputParser = dateInputParser;
        _timeInputParser = timeInputParser;
        _geoLatInputParser = geoLatInputParser;
        _geoLongInputParser = geoLongInputParser;
        _valueRangeConverter = valueRangeConverter;
    }


    public bool ProcessInput()
    {
        _cal = Calendar;
        _errorCodes = new List<int>();
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

        if (dateSuccess && timeSuccess && geoLongSuccess && geoLatSuccess && lmtSuccess)
        {
            SimpleDateTime dateTime = new(_yearMonthDay[0], _yearMonthDay[1], _yearMonthDay[2], _ut, _cal);


            // TODO create request and call API, process result

            return true;
        }
        else return false;
    }






}