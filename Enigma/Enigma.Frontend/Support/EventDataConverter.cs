// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;

namespace Enigma.Frontend.Ui.Support;

/// <summary>Conversion to and from EventData/PersistebleEventData.</summary>
public interface IEventDataConverter
{
    /// <summary>Convert PersistableEventData to EventData.</summary>
    /// <param name="persistableEventData"/>
    /// <returns>Resulting EventData.</returns>
    public ProgEvent FromPersistableEventData(PersistableEventData persistableEventData);

    /// <summary>Convert EventData to PersistableEventData.</summary>
    /// <param name="progEvent"/>
    /// <returns>Resulting PersistableEventData.</returns>
    public PersistableEventData ToPersistableEventData(ProgEvent progEvent);

}

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
        Location? location = new(locationFullName, persistableEventData.GeoLong, persistableEventData.GeoLat);
        FullDateTime fullDateTime = new(persistableEventData.DateText, persistableEventData.TimeText, persistableEventData.JdForEt);
        return new ProgEvent(persistableEventData.Id, description, locationName, location, fullDateTime);
    }

    private static PersistableEventData HandleConversion(ProgEvent progEvent)
    {
        PersistableEventData persEventData = new()
        {
            Description = progEvent.Description,
            LocationName = progEvent.LocationName,
            JdForEt = progEvent.DateTime.JulianDayForEt,
            DateText = progEvent.DateTime.DateText,
            TimeText = progEvent.DateTime.TimeText,
            GeoLat = progEvent.Location.GeoLat,
            GeoLong = progEvent.Location.GeoLong
        };
        return persEventData;
    }
}