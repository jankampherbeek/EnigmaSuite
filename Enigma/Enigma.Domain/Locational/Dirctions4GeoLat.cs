// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Locational;

public enum Directions4GeoLat
{
    /// <summary>Northern geographic latitude.</summary>
    North = 1,
    /// <summary>Southern geographic latitude.</summary>
    South = -1
}



/// <summary>Details for the Directions of geographic latitude.</summary>
public record Directions4GeoLatDetails
{
    readonly public Directions4GeoLat Direction;
    readonly public string TextId;

    /// <param name="direction">Direction from the enum DirectionsGeo4Lat.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public Directions4GeoLatDetails(Directions4GeoLat direction, string textId)
    {
        Direction = direction;
        TextId = textId;
    }
}

/// <summary>Specifications for the Direction of geographic latitude.</summary>
public interface IDirections4GeoLatSpecifications
{
    /// <param name="direction">The direction, from the enum Directions4GeoLat.</param>
    /// <returns>A record with the specifications.</returns>
    public Directions4GeoLatDetails DetailsForDirection(Directions4GeoLat direction);

    ///<returns>Details for all items in enum Directions4GeoLat.</returns>
    public List<Directions4GeoLatDetails> AllDirectionDetails();

    /// <summary>
    /// Returns a value from the enum Directions4GeoLat that corresponds with an index.
    /// </summary>
    /// <param name="directionIndex">The index for the requested item from Directions4GeoLat. 
    /// Throws an exception if no direction for the given index does exist.</param>
    /// <returns>Instance from enum Directions4GeoLat that corresponds with the given index.</returns>
    public Directions4GeoLat DirectionForIndex(int directionIndex);
}

///<inheritdoc/>
public class Directions4GeoLatSpecifications : IDirections4GeoLatSpecifications
{
    public List<Directions4GeoLatDetails> AllDirectionDetails()
    {
        var allDetails = new List<Directions4GeoLatDetails>();
        foreach (Directions4GeoLat direction in Enum.GetValues(typeof(Directions4GeoLat)))
        {
            allDetails.Add(DetailsForDirection(direction));
        }
        return allDetails;
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the direction was not recognized.</exception>
    public Directions4GeoLatDetails DetailsForDirection(Directions4GeoLat direction)
    {
        return direction switch
        {
            Directions4GeoLat.North => new Directions4GeoLatDetails(direction, "ref.enum.direction4geolat.north"),
            Directions4GeoLat.South => new Directions4GeoLatDetails(direction, "ref.enum.direction4geolat.south"),
            _ => throw new ArgumentException("Direction for latitude unknown : " + direction.ToString())
        };
    }

    /// <inheritdoc/>
    public Directions4GeoLat DirectionForIndex(int directionIndex)
    {
        foreach (Directions4GeoLat direction in Enum.GetValues(typeof(Directions4GeoLat)))
        {
            if ((int)direction == directionIndex) return direction;
        }
        throw new ArgumentException("Could not find direction of latitude for index : " + directionIndex);
    }


}