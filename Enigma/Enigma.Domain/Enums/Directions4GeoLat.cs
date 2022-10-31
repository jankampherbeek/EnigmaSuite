// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Enums;

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