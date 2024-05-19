// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.References;

/// <summary>Out of bounds event types.</summary>
public enum OobEventTypes
{
    StartOobNorth = 0,
    StartOobSouth = 1,
    EndOobNorth = 2,
    EndOobSouth = 3,
    InitialOobNorth = 4,
    InitialOobSouth = 5,
    InitialInBoundsNorth = 6,
    InitialInBoundsSouth = 7
}



/// <summary>Details for an OobEventtype.</summary>
/// <param name="OobEventType">The Out of bounds event type.</param>
/// <param name="RbKey">Key for descriptive text in rsource bundle.</param>
public record OobEventTypesDetails(OobEventTypes OobEventType, string RbKey);


public static class OobEventtypesExtensions
{
    /// <summary>Retrieve details for OobEventTypes.</summary>
    /// <param name="oobEventType">The Out of Bounds event type.</param>
    /// <returns>Details for the Out of Bounds Event Type.</returns>
    public static OobEventTypesDetails GetDetails(this OobEventTypes oobEventType)
    {
        return oobEventType switch
        {
            OobEventTypes.StartOobNorth => new OobEventTypesDetails(oobEventType, "ref_oobeventtype_startnorth"),
            OobEventTypes.StartOobSouth => new OobEventTypesDetails(oobEventType, "ref_oobeventtype_startsouth"),
            OobEventTypes.EndOobNorth => new OobEventTypesDetails(oobEventType, "ref_oobeventtype_endnorth"),
            OobEventTypes.EndOobSouth => new OobEventTypesDetails(oobEventType, "ref_oobeventtype_endsouth"),
            OobEventTypes.InitialOobNorth => new OobEventTypesDetails(oobEventType, "ref_oobeventtype_initoobnorth"),
            OobEventTypes.InitialOobSouth => new OobEventTypesDetails(oobEventType, "ref_oobeventtype_initoobsouth"),
            OobEventTypes.InitialInBoundsNorth => new OobEventTypesDetails(oobEventType, "ref_oobeventtype_initinboundsnorth"),
            OobEventTypes.InitialInBoundsSouth => new OobEventTypesDetails(oobEventType, "ref_oobeventtype_initinboundssouth"),
            _ => throw new ArgumentException("OobEventTypes unknown : " + oobEventType)
        };
    }
    
    /// <summary>Retrieve details for items in the enum OobEventTypes.</summary>
    /// <returns>All details.</returns>
    public static List<OobEventTypesDetails> AllDetails()
    {
        return (from OobEventTypes currentEventType in Enum.GetValues(typeof(OobEventTypes)) 
            select currentEventType.GetDetails()).ToList();
    }


    /// <summary>Find Out of Bounds Event Type for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The OobEventTypes for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static OobEventTypes OobEventTypeForIndex(int index)
    {
        foreach (OobEventTypes currentEventType in Enum.GetValues(typeof(OobEventTypes)))
        {
            if ((int)currentEventType == index) return currentEventType;
        }
        throw new ArgumentException("Could not find OobEventTypes.");
    }

}
