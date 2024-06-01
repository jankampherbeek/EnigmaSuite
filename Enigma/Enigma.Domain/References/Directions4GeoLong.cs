// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;


/// <summary>Enum with directions for geographic longitude.</summary>
public enum Directions4GeoLong
{
    /// <summary>Eastern geographiuc longitude.</summary>
    East = 1,
    /// <summary>Western geographic longitude.</summary>
    West = -1
}


/// <summary>Details for the Directions of geographic longitude.</summary>
/// <param name="Direction">Direction from the enum DirectionsGeo4Long.</param>
/// <param name="RbKey">Key to descriptive text in resource bundle.</param>
public record Directions4GeoLongDetails(Directions4GeoLong Direction, string RbKey);


/// <summary>Extension class for enum Directions4GeoLong.</summary>
public static class Directions4GeoLongExtensions
{
    /// <summary>Retrieve details for Directions4GeoLong.</summary>
    /// <param name="direction">The Directions4GeoLong, is automatically filled.</param>
    /// <returns>Details for the Directions4GeoLong.</returns>
    public static Directions4GeoLongDetails GetDetails(this Directions4GeoLong direction)
    {
        return direction switch
        {
            Directions4GeoLong.East => new Directions4GeoLongDetails(direction, "ref.directions4geoLong.east"),
            Directions4GeoLong.West => new Directions4GeoLongDetails(direction, "ref.directions4geoLong.west"),
            _ => throw new ArgumentException("Direction for Longitude unknown : " + direction)
        };
    }

    /// <summary>Retrieve details for items in the enum Directions4GeoLong.</summary>
    /// <returns>All details.</returns>
    public static List<Directions4GeoLongDetails> AllDetails()
    {
        return (from Directions4GeoLong currentDir in Enum.GetValues(typeof(Directions4GeoLong)) 
            select currentDir.GetDetails()).ToList();
    }


    /// <summary>Find Directions4GeoLong for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The Directions4GeoLong.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static Directions4GeoLong DirectionForIndex(int index)
    {
        foreach (Directions4GeoLong currentDir in Enum.GetValues(typeof(Directions4GeoLong)))
        {
            if ((int)currentDir == index) return currentDir;
        }
        Log.Error("Directions4GeoLong.DirectionForIndex(): Could not find Directions4GeoLong for index : {Index} ", index);
        throw new ArgumentException("Wrong Directions4GeoLong");
    }

}


