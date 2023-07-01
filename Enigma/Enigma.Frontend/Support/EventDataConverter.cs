// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Persistency;
using Enigma.Domain.Progressive;
using Enigma.Frontend.Ui.Interfaces;

namespace Enigma.Frontend.Ui.SUpport;

/// <inheritdoc/>
public sealed class EventDataConverter : IEventDataConverter
{
    private readonly ILocationConversion _locationConversion;

    public EventDataConverter(ILocationConversion locationConversion)
    {
        _locationConversion = locationConversion;
    }


    /// <inheritdoc/>
    public EventData FromPersistableEventData(PersistableEventData persistableEventData)
    {
        return HandleConversion(persistableEventData);
    }

    /// <inheritdoc/>
    public PersistableEventData ToPersistableEventData(EventData eventData)
    {
        return HandleConversion(eventData);
    }

    private EventData HandleConversion(PersistableEventData persistableEventData)
    {
        string description = persistableEventData.Description;
        string locationName = persistableEventData.LocationName;
        string locationFullName = _locationConversion.CreateLocationDescription(locationName, persistableEventData.GeoLat, persistableEventData.GeoLong);
        Location location = new(locationFullName, persistableEventData.GeoLong, persistableEventData.GeoLat);
        FullDateTime fullDateTime = new(persistableEventData.DateText, persistableEventData.TimeText, persistableEventData.JulianDayEt);
        return new EventData(persistableEventData.Id, description, locationName, location, fullDateTime);
    }

    private static PersistableEventData HandleConversion(EventData eventData)
    {
        return new PersistableEventData( 
            eventData.Id,
            eventData.Description,
            eventData.FullDateTime.JulianDayForEt,
            eventData.FullDateTime.DateText,
            eventData.FullDateTime.TimeText,
            eventData.LocationName,
            eventData.Location.GeoLong,
            eventData.Location.GeoLat
        );
    }
}