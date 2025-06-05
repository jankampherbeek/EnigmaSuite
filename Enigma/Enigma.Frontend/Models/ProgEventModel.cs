// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api.Calc;
using Enigma.Api.LocationAndTimeZones;
using Enigma.Api.Persistency;
using Enigma.Domain.Dtos;
using Enigma.Domain.LocationsZones;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support.Conversions;
using Enigma.Frontend.Ui.Support.Parsers;
using Enigma.Frontend.Ui.Support.Validations;

namespace Enigma.Frontend.Ui.Models;

public class ProgEventModel: DateTimeLocationModelBase
{
    private readonly IJulianDayApi _julianDayApi;
    public List<Country> AllCountries;
    private readonly ILocationApi _locationApi;
    
    public ProgEventModel(IGeoLongInputParser geoLongInputParser, 
        IGeoLatInputParser geoLatInputParser,
        IDateInputParser dateInputParser, 
        ITimeInputParser timeInputParser, 
        IJulianDayApi julianDayApi,
        ILocationApi locationApi,
        IEventDataPersistencyApi eventDataPersistencyApi,
        IValueRangeConverter valueRangeConverter,
        ITimeValidator timeValidator) 
        : base(dateInputParser, timeInputParser, geoLongInputParser, geoLatInputParser, valueRangeConverter, timeValidator)
    {
        _julianDayApi = julianDayApi;
        _locationApi = locationApi;
        PopulateCountries();
        
    }

    public void CreateEventData(string description, string locationName)
    {
        if (FullDate == null) return;
        if (FullTime == null)
        {
            int[] hms = { 0, 0, 0 };
            FullTime = new FullTime(hms, 0.0, 0, string.Empty);
        }
        SimpleDateTime dateTime = new(FullDate.YearMonthDay[0], FullDate.YearMonthDay[1], FullDate.YearMonthDay[2],
            FullTime.Ut, FullDate.Calendar);
        var julianDayUt = _julianDayApi.GetJulianDay(dateTime).JulDayUt;
        var locNameCheckedForEmpty = string.IsNullOrEmpty(locationName) ? "" : locationName + " ";
        var fullLocationName = locNameCheckedForEmpty + FullGeoLongitude!.GeoLongFullText + " " +
                               FullGeoLatitude!.GeoLatFullText;
        Location? location = new(fullLocationName, FullGeoLongitude.Longitude, FullGeoLatitude.Latitude);
        FullDateTime fullDateTime = new(FullDate.DateFullText, FullTime.TimeFullText, julianDayUt);
        ProgEvent progEvent = new(0, description, locationName, location, fullDateTime);
        DataVaultProg.Instance.CurrentProgEvent = progEvent;
        
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