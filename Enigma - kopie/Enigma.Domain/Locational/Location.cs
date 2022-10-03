// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Domain.Locational;

/// <summary>Data about geographical location.</summary>
public class Location
{
    /// <summary>Optional description of location.</summary>
    public string LocationFullName { get; }
    /// <summary>Decimal value for geographic longitude.</summary>
    public double GeoLong { get; }
    /// <summary>Decimal value for geographic latitude.</summary>
    public double GeoLat { get; }
    /// <summary>East or west for geographic longitude.</summary>
    public Directions4GeoLong DirLong { get; }
    /// <summary>North or south for geographic latitude.</summary>
    public Directions4GeoLat DirLat { get; }

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