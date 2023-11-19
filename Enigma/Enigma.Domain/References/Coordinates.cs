// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.References;

/// <summary>Single coordinates</summary>
public enum Coordinates
{
    Longitude = 0,
    Latitude = 1,
    RightAscension = 2,
    Declination = 3,
    Azimuth = 4,
    Altitude = 5
}


/// <summary>Details for Coordinates</summary>
/// <param name="Coordinate">Instance from enum Coordinates</param>
/// <param name="CoordinateSystem">Coordinate system where this coordinate is a part of</param>
/// <param name="Text">Descriptive text</param>
public record CoordinateDetails(Coordinates Coordinate, CoordinateSystems CoordinateSystem, string Text);


/// <summary>Extension class for enum Coordinates</summary>
public static class CoordinatesExtensions
{
    /// <summary>Retrieve details for Coordinates</summary>
    /// <param name="coordinate">The coordinate, is automatically filled</param>
    /// <returns>Details for the coordinate</returns>
    public static CoordinateDetails GetDetails(this Coordinates coordinate)
    {
        return coordinate switch
        {
            Coordinates.Longitude => new CoordinateDetails(coordinate, CoordinateSystems.Ecliptical, "Longitude"),
            Coordinates.Latitude => new CoordinateDetails(coordinate, CoordinateSystems.Ecliptical, "Latitude"),
            Coordinates.RightAscension => new CoordinateDetails(coordinate, CoordinateSystems.Equatorial, "Right Ascension"),
            Coordinates.Declination => new CoordinateDetails(coordinate, CoordinateSystems.Equatorial, "Declination"),
            Coordinates.Azimuth => new CoordinateDetails(coordinate, CoordinateSystems.Horizontal, "Azimuth"),
            Coordinates.Altitude => new CoordinateDetails(coordinate, CoordinateSystems.Horizontal, "Altitude"),
            _ => throw new ArgumentException("Coordinate unknown : " + coordinate)
        };
    }

    /// <summary>Retrieve details for items in the enum Coordinates</summary>
    /// <returns>All details</returns>
    public static List<CoordinateDetails> AllDetails()
    {
        return (from Coordinates currentCoordinate in Enum.GetValues(typeof(Coordinates)) 
            select currentCoordinate.GetDetails()).ToList();
    }

    /// <summary>Find coordinate for an index</summary>
    /// <param name="index">Index to look for</param>
    /// <returns>The coordinate for the index</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static Coordinates CoordinateForIndex(int index)
    {
        foreach (Coordinates currentCoordinate in Enum.GetValues(typeof(Coordinates)))
        {
            if ((int)currentCoordinate == index) return currentCoordinate;
        }
        throw new ArgumentException("Could not find coordinate for index : " + index);
    }

}
