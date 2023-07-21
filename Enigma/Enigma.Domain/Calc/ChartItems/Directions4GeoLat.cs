// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Calc.ChartItems;

public enum Directions4GeoLat
{
    /// <summary>Northern geographic latitude.</summary>
    North = 1,
    /// <summary>Southern geographic latitude.</summary>
    South = -1
}


/// <summary>Details for the Directions of geographic latitude</summary>
/// <param name="Direction">Direction from the enum DirectionsGeo4Lat</param>
/// <param name="Text">Descriptive text</param>
public record Directions4GeoLatDetails(Directions4GeoLat Direction, string Text);

/// <summary>Extension class for enum Directions4GeoLat.</summary>
public static class Directions4GeoLatExtensions
{
    /// <summary>Retrieve details for Directions4GeoLat.</summary>
    /// <param name="direction">The Directions4GeoLat.</param>
    /// <returns>Details for the Directions4GeoLat.</returns>
    public static Directions4GeoLatDetails GetDetails(this Directions4GeoLat direction)
    {
        return direction switch
        {
            Directions4GeoLat.North => new Directions4GeoLatDetails(direction, "North"),
            Directions4GeoLat.South => new Directions4GeoLatDetails(direction, "South"),
            _ => throw new ArgumentException("Direction for latitude unknown : " + direction)
        };
    }

    /// <summary>Retrieve details for items in the enum Directions4GeoLat.</summary>
    /// <returns>All details.</returns>
    public static List<Directions4GeoLatDetails> AllDetails(this Directions4GeoLat _)
    {
        var allDetails = new List<Directions4GeoLatDetails>();
        foreach (Directions4GeoLat currentDir in Enum.GetValues(typeof(Directions4GeoLat)))
        {
            allDetails.Add(currentDir.GetDetails());
        }
        return allDetails;
    }


    /// <summary>Find Directions4GeoLat for an index</summary>
    /// <param name="_">Instance of enum Directions4GeoLat</param>
    /// <param name="index">Index to look for</param>
    /// <returns>The Directions4GeoLat</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static Directions4GeoLat DirectionForIndex(this Directions4GeoLat _, int index)
    {
        foreach (Directions4GeoLat currentDir in Enum.GetValues(typeof(Directions4GeoLat)))
        {
            if ((int)currentDir == index) return currentDir;
        }
        Log.Error("Directions4GeoLat.DirectionFordex(): Could not find Directions4GeoLat for index : {Index}", index);
        throw new ArgumentException("Wrong direction for geographic latitude");
    }

}




