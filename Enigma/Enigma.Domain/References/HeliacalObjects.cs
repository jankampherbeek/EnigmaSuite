// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;

/// <summary>Celestial objects that can be used for heliacal calculations</summary>
public enum HeliacalObjects
{
    Moon = 0, Mercury = 1, Venus = 2, Mars = 3, Jupiter = 4, Saturn = 5
}

/// <summary>Details for a HeliacalObject</summary>
/// <param name="HeliacalObject">Instance of HeliacalObjects</param>
/// <param name="ChartPoint">Corresponding instance of ChartPoints</param>
public record HeliacalObjectDetails(HeliacalObjects HeliacalObject, ChartPoints ChartPoint);


/// <summary>Extension class for the enum HeliacalObjects</summary>
public static class HeliacalObjectsExtensions
{
    /// <summary>Retrieve details for a heliacal object</summary>
    /// <param name="heliacalObject">The heliacal object, is automatically filled</param>
    /// <returns>Details for heliacal object</returns>
    public static HeliacalObjectDetails GetDetails(this HeliacalObjects heliacalObject)
    {
        return heliacalObject switch
        {
            HeliacalObjects.Moon => new HeliacalObjectDetails(heliacalObject, ChartPoints.Moon),
            HeliacalObjects.Mercury => new HeliacalObjectDetails(heliacalObject, ChartPoints.Mercury),
            HeliacalObjects.Venus => new HeliacalObjectDetails(heliacalObject, ChartPoints.Venus),
            HeliacalObjects.Mars => new HeliacalObjectDetails(heliacalObject, ChartPoints.Mars),
            HeliacalObjects.Jupiter => new HeliacalObjectDetails(heliacalObject, ChartPoints.Jupiter),
            HeliacalObjects.Saturn => new HeliacalObjectDetails(heliacalObject, ChartPoints.Saturn),
            _ => throw new ArgumentException("Heliacal Object unknown : " + heliacalObject)
        };
    }


    /// <summary>Retrieve details for items in the enum HeliacalObjects</summary>
    /// <returns>All details</returns>
    public static IEnumerable<HeliacalObjectDetails> AllDetails()
    {
        return (from HeliacalObjects heliacalObject in Enum.GetValues(typeof(HeliacalObjects))
            select heliacalObject.GetDetails()).ToList();
    }

    /// <summary>Find heliacal object type for a given index</summary>
    /// <param name="index">The index</param>
    /// <returns>The heliacal object</returns>
    /// <exception cref="ArgumentException">Thrown if the heliacal object was not found</exception>
    public static HeliacalObjects HeliacalObjectForIndex(int index)
    {
        foreach (HeliacalObjects heliacalObject in Enum.GetValues(typeof(HeliacalObjects)))
        {
            if ((int)heliacalObject == index ) return heliacalObject;
        }
        Log.Error("HeliacalObjectsExtensions.HeliacalObjectForIndex(): Could not find Heliacal Object " +
                  "for index : {Index}", index );
        throw new ArgumentException("No heliacal object for given index");
    }
    
}

