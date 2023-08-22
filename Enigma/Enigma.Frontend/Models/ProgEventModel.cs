// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Persistency;
using Enigma.Domain.Progressive;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

public class ProgEventModel: DateTimeLocationModelBase
{
    private readonly IJulianDayApi _julianDayApi;
    
    public ProgEventModel(IGeoLongInputParser geoLongInputParser, 
        IGeoLatInputParser geoLatInputParser,
        IDateInputParser dateInputParser, 
        ITimeInputParser timeInputParser, 
        IJulianDayApi julianDayApi) 
        : base(dateInputParser, timeInputParser, geoLongInputParser, geoLatInputParser)
    {
        _julianDayApi = julianDayApi;
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
        double julianDayUt = _julianDayApi.GetJulianDay(dateTime).JulDayUt;
        string locNameCheckedForEmpty = string.IsNullOrEmpty(locationName) ? "" : locationName + " ";
        string fullLocationName = locNameCheckedForEmpty + FullGeoLongitude!.GeoLongFullText + " " +
                                  FullGeoLatitude!.GeoLatFullText;
        Location location = new(fullLocationName, FullGeoLongitude.Longitude, FullGeoLatitude.Latitude);
        FullDateTime fullDateTime = new(FullDate.DateFullText, FullTime.TimeFullText, julianDayUt);
        ProgEvent progEvent = new(0, description, locationName, location, fullDateTime);
        DataVault.Instance.CurrentProgEvent = progEvent;
    }


    
    
}