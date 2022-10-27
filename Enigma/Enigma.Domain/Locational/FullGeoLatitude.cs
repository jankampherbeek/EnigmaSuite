// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Locational;

/// <summary>
/// Record for a full definition of geographic latitude.
/// </summary>
public record FullGeoLatitude
{
    public readonly int[] DegreeMinuteSecond;
    public readonly double Latitude;
    public readonly Directions4GeoLat Direction;
    public readonly string GeoLatFullText;

    /// <summary>
    /// Constructor for FullGeoLatitude.
    /// </summary>
    /// <param name="degreeMinuteSecond">Texts for degree, minute and second in that sequence.</param>
    /// <param name="latitude">Value of geographic latitude.</param>
    /// <param name="direction">Direction for geographic latitude: north or south.</param>
    /// <param name="geoLatFullText">Text for the geographic latitude.</param>
    public FullGeoLatitude(int[] degreeMinuteSecond, double latitude, Directions4GeoLat direction, string geoLatFullText)
    {
        DegreeMinuteSecond = degreeMinuteSecond;
        Latitude = latitude;
        Direction = direction;
        GeoLatFullText = geoLatFullText;
    }

}