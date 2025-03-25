// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.LocationsZones;


/// <summary>Data for a country, to be used in finding coordinates.</summary>
public class Country
{
    public string Code { get; set; }
    public string Name { get; set; }
}

/// <summary>Data for a city, to be used in finding coordinates.</summary>
public class City
{
    public string Country { get; set; }
    public string Name { get; set; }
    public string GeoLat { get; set; }
    public string GeoLong { get; set; }
    public string Region { get; set; }
    public string Elevation { get; set; }
    public string IndicationTz { get; set; }
}