// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.using System;

using System;

namespace Enigma.Frontend.Ui.Support;
/// <summary>Textual conversions ofr location and coördinates.</summary>
public interface ILocationConversion
{
    /// <summary>Convert name and coördinatres to a string that presesents all infor for the location.</summary>
    /// <remarks>If no name is entered the name is replaced with a text that incidcates theo omission of the name.</remarks>
    /// <param name="locationName">Name for the location.</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <param name="geoLong">Geographic longitude.</param>
    /// <returns>A text in the form: Enschede 52.21666666667 N / 6.9 E .</returns>
    public string CreateLocationDescription(string locationName, double geoLat, double geoLong);
}

public sealed class LocationConversion : ILocationConversion
{

    public string CreateLocationDescription(string locationName, double geoLat, double geoLong)
    {
        string qualifiedLocationName = locationName is null or "" ? "No name for location" : locationName;
        string latDir = geoLat >= 0.0 ? "N" : "S";
        string longDir = geoLong >= 0.0 ? "E" : "W";
        return qualifiedLocationName + " " + Math.Abs(geoLat) + " " + latDir + " / " + Math.Abs(geoLong) + " " + longDir;
    }

}

