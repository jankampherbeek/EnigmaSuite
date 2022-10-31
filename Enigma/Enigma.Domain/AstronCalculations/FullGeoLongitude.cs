// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;

namespace Enigma.Domain.AstronCalculations;

/// <summary>
/// Record for a full definition of geographic longitude.
/// </summary>
public record FullGeoLongitude
{
    public readonly int[] DegreeMinuteSecond;
    public readonly double Longitude;
    public readonly Directions4GeoLong Direction;
    public readonly string GeoLongFullText;

    /// <summary>
    /// Constructor for FullGeoLongitude.
    /// </summary>
    /// <param name="degreeMinuteSecond">Texts for degree, minute and second in that sequence.</param>
    /// <param name="longitude">Value of geographic longitude.</param>
    /// <param name="direction">Direction for geographic longitude: east or west.</param>
    /// <param name="geoLongFullText">Text for the geographic longitude.</param>
    public FullGeoLongitude(int[] degreeMinuteSecond, double longitude, Directions4GeoLong direction, string geoLongFullText)
    {
        DegreeMinuteSecond = degreeMinuteSecond;
        Longitude = longitude;
        Direction = direction;
        GeoLongFullText = geoLongFullText;
    }

}