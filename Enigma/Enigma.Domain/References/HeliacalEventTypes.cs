// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Serilog;

namespace Enigma.Domain.References;

/// <summary>TYpes of heliacal events</summary>
public enum HeliacalEventTypes
{ 
    HeliacalRising = 0, HeliacalSetting = 1, EveningFirst = 2, MorningLast = 3
}

/// <summary>Details for a HeliacalEventType</summary>
/// <param name="EventType">Instance of HeliacalEventType</param>
/// <param name="IndexForSe">Index for this event tuype as used by the Swiss Ephemeris</param>
/// <param name="Text">Descriptive text</param>
public record HeliacalEventTypeDetails(HeliacalEventTypes EventType, int IndexForSe, string Text);

/// <summary>Extension class for the enum HeliacalEventTypes</summary>
public static class HeliacalEventTypesExtensions
{
    /// <summary>Retrieve details for a heliacal event</summary>
    /// <param name="eventType">The event type, is automatically filled</param>
    /// <returns>Details for event type</returns>
    public static HeliacalEventTypeDetails GetDetails(this HeliacalEventTypes eventType)
    {
        return eventType switch
        {
            HeliacalEventTypes.HeliacalRising => new HeliacalEventTypeDetails(eventType,
                EnigmaConstants.SE_HELIACAL_RISING, "Heliacal rising"),
            HeliacalEventTypes.HeliacalSetting => new HeliacalEventTypeDetails(eventType,
                EnigmaConstants.SE_HELIACAL_SETTING, "Heliacal setting"),
            HeliacalEventTypes.EveningFirst => new HeliacalEventTypeDetails(eventType,
                EnigmaConstants.SE_EVENING_FIRST, "Evening first (Mercury, Venus, Moon)"),
            HeliacalEventTypes.MorningLast => new HeliacalEventTypeDetails(eventType,
                EnigmaConstants.SE_MORNING_LAST, "Morning last (Mercury, Venus, Moon)"),
            _ => throw new ArgumentException("Heliacal Event Type unknown : " + eventType)
        };
    }


    /// <summary>Retrieve details for items in the enum HeliacalEventTypes</summary>
    /// <returns>All details</returns>
    public static IEnumerable<HeliacalEventTypeDetails> AllDetails()
    {
        return (from HeliacalEventTypes eventType in Enum.GetValues(typeof(HeliacalEventTypes))
            select eventType.GetDetails()).ToList();
    }

    /// <summary>Find heliacal event type for a given index</summary>
    /// <param name="index">The index</param>
    /// <returns>The event type</returns>
    /// <exception cref="ArgumentException">Thrown if the event type position was not found.</exception>
    public static HeliacalEventTypes EventTypeForIndex(int index)
    {
        foreach (HeliacalEventTypes eventType in Enum.GetValues(typeof(HeliacalEventTypes)))
        {
            if ((int)eventType == index ) return eventType;
        }
        Log.Error("HeliacalEventTypesExtensions.EventTypeForIndex(): Could not find Heliacal Event Type " +
                  "for index : {Index}", index );
        throw new ArgumentException("No heliacal event type for given index");
    }
    
}