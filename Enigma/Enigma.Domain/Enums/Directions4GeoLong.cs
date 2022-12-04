// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Enums;


/// <summary>Enum with directions for geographic longitude.</summary>
public enum Directions4GeoLong
{
    /// <summary>Eastern geographiuc longitude.</summary>
    East = 1,
    /// <summary>Western geographic longitude.</summary>
    West = -1
}

public static class Directions4GeoLongExtensions
{
    /// <summary>Retrieve details for Directions4GeoLong.</summary>
    /// <param name="direction">The Directions4GeoLong, is automatically filled.</param>
    /// <returns>Details for the Directions4GeoLong.</returns>
    public static Directions4GeoLongDetails GetDetails(this Directions4GeoLong direction)
    {
        return direction switch
        {
            Directions4GeoLong.East => new Directions4GeoLongDetails(direction, "ref.enum.direction4geolong.east"),
            Directions4GeoLong.West => new Directions4GeoLongDetails(direction, "ref.enum.direction4geolong.west"),
            _ => throw new ArgumentException("Direction for Longitude unknown : " + direction.ToString())
        };
    }

    /// <summary>Retrieve details for items in the enum Directions4GeoLong.</summary>
    /// <param name="direction.">The Directions4GeoLong, is automatically filled.</param>
    /// <returns>All details.</returns>
    public static List<Directions4GeoLongDetails> AllDetails(this Directions4GeoLong direction)
    {
        var allDetails = new List<Directions4GeoLongDetails>();
        foreach (Directions4GeoLong currentDir in Enum.GetValues(typeof(Directions4GeoLong)))
        {
            allDetails.Add(currentDir.GetDetails());
        }
        return allDetails;
    }


    /// <summary>Find Directions4GeoLong for an index.</summary>
    /// <param name="direction.">Any Directions4GeoLong, is automatically filled.</param>
    /// <param name="index">Index to look for.</param>
    /// <returns>The Directions4GeoLong.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static Directions4GeoLong DirectionForIndex(this Directions4GeoLong direction, int index)
    {
        foreach (Directions4GeoLong currentDir in Enum.GetValues(typeof(Directions4GeoLong)))
        {
            if ((int)currentDir == index) return currentDir;
        }
        throw new ArgumentException("Could not find Directions4GeoLong for index : " + index);
    }

}


/// <summary>Details for the Directions of geographic longitude.</summary>
/// <param name="Direction">Direction from the enum DirectionsGeo4Long.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record Directions4GeoLongDetails(Directions4GeoLong Direction, string TextId);

