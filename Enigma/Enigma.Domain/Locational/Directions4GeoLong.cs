// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Locational;


/// <summary>Enum with directions for geographic longitude.</summary>
public enum Directions4GeoLong
{
    /// <summary>Eastern geographiuc longitude.</summary>
    East = 1,
    /// <summary>Western geographic longitude.</summary>
    West = -1
}

/// <summary>Details for the Directions of geographic longitude.</summary>
public record Directions4GeoLongDetails
{
    readonly public Directions4GeoLong Direction;
    readonly public string TextId;

    /// <param name="direction">Direction from the enum DirectionsGeo4Long.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public Directions4GeoLongDetails(Directions4GeoLong direction, string textId)
    {
        Direction = direction;
        TextId = textId;
    }
}

/// <summary>Specifications for the Direction of geographic longitude.</summary>
public interface IDirections4GeoLongSpecifications
{
    /// <param name="direction">The direction, from the enum Directions4GeoLong.</param>
    /// <returns>A record with the specifications.</returns>
    public Directions4GeoLongDetails DetailsForDirection(Directions4GeoLong direction);

    ///<returns>Details for all items in enum Directions4GeoLong.</returns>
    public List<Directions4GeoLongDetails> AllDirectionDetails();

    /// <summary>
    /// Returns a value from the enum Directions4GeoLong that corresponds with an index.
    /// </summary>
    /// <param name="directionIndex">The index for the requested item from Directions4GeoLong. 
    /// Throws an exception if no direction for the given index does exist.</param>
    /// <returns>Instance from enum Directions4GeoLong that corresponds with the given index.</returns>
    public Directions4GeoLong DirectionForIndex(int directionIndex);
}

///<inheritdoc/>
public class Directions4GeoLongSpecifications : IDirections4GeoLongSpecifications
{
    public List<Directions4GeoLongDetails> AllDirectionDetails()
    {
        var allDetails = new List<Directions4GeoLongDetails>();
        foreach (Directions4GeoLong direction in Enum.GetValues(typeof(Directions4GeoLong)))
        {
            allDetails.Add(DetailsForDirection(direction));
        }
        return allDetails;
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the direction was not recognized.</exception>
    public Directions4GeoLongDetails DetailsForDirection(Directions4GeoLong direction)
    {
        return direction switch
        {
            Directions4GeoLong.East => new Directions4GeoLongDetails(direction, "ref.enum.direction4geolong.east"),
            Directions4GeoLong.West => new Directions4GeoLongDetails(direction, "ref.enum.direction4geolong.west"),
            _ => throw new ArgumentException("Direction for longitude unknown : " + direction.ToString())
        };
    }

    /// <inheritdoc/>
    public Directions4GeoLong DirectionForIndex(int directionIndex)
    {
        foreach (Directions4GeoLong direction in Enum.GetValues(typeof(Directions4GeoLong)))
        {
            if ((int)direction == directionIndex) return direction;
        }
        throw new ArgumentException("Could not find direction of longitude for index : " + directionIndex);
    }
}