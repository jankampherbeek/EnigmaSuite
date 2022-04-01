// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.domain.shared.references;

namespace E4C.domain.shared.specifications;

/// <summary>
/// Location related data.
/// </summary>
public class Location
{
    public readonly string LocationFullName;
    public readonly double GeoLong;
    public readonly double GeoLat;
    public readonly Directions4GeoLong DirLong;
    public readonly Directions4GeoLat DirLat;

    /// <summary>
    /// Constructor for Location.
    /// </summary>
    /// <param name="locationFullName">Name and sexagesimal coordinatevalues for a location. Directions are defined between [] and need to be replaced with texts from Rosetta.</param>
    /// <param name="geoLong">Value for geographic longitude.</param>
    /// <param name="geoLat">Value for geographic latitude.</param>
    public Location(string locationFullName, double geoLong, double geoLat)
    {
        LocationFullName = locationFullName;
        GeoLong = geoLong;
        GeoLat = geoLat;
        DirLong = GeoLong >= 0.0 ? Directions4GeoLong.East : Directions4GeoLong.West;
        DirLat = GeoLat >= 0.0 ? Directions4GeoLat.North : Directions4GeoLat.South;
    }




}