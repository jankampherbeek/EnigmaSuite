// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Enigma.Frontend.Ui.Interfaces;

namespace Enigma.Frontend.Ui.Support;

/// <inheritdoc/>
public sealed class EventDataConverter : IEventDataConverter
{
    private readonly ILocationConversion _locationConversion;

    public EventDataConverter(ILocationConversion locationConversion)
    {
        _locationConversion = locationConversion;
    }


    /// <inheritdoc/>
    public ProgEvent FromPersistableEventData(PersistableEventData persistableEventData)
    {
        return HandleConversion(persistableEventData);
    }

    /// <inheritdoc/>
    public PersistableEventData ToPersistableEventData(ProgEvent progEvent)
    {
        return HandleConversion(progEvent);
    }

    private ProgEvent HandleConversion(PersistableEventData persistableEventData)
    {
        string description = persistableEventData.Description;
        string locationName = persistableEventData.LocationName;
        string locationFullName = _locationConversion.CreateLocationDescription(locationName, persistableEventData.GeoLat, persistableEventData.GeoLong);
        Location location = new(locationFullName, persistableEventData.GeoLong, persistableEventData.GeoLat);
        FullDateTime fullDateTime = new(persistableEventData.DateText, persistableEventData.TimeText, persistableEventData.JulianDayEt);
        return new ProgEvent(persistableEventData.Id, description, locationName, location, fullDateTime);
    }

    private static PersistableEventData HandleConversion(ProgEvent progEvent)
    {
        PersistableEventData persEventData = new();
        persEventData.Description = progEvent.Description;
        persEventData.LocationName = progEvent.LocationName;
        persEventData.JulianDayEt = progEvent.DateTime.JulianDayForEt;
        persEventData.DateText = progEvent.DateTime.DateText;
        persEventData.TimeText = progEvent.DateTime.TimeText;
        persEventData.GeoLat = progEvent.Location.GeoLat;
        persEventData.GeoLong = progEvent.Location.GeoLong;
        return persEventData;
    }
}