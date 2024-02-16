// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.using System;

using System;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.Support;
/// <summary>Textual conversions ofr location and coördinates.</summary>
public interface ILocationConversion
{
    /// <summary>Convert name and coördinatres to a string that presesents all infor for the location.</summary>
    /// <remarks>If no name is entered the name is replaced with a text that incidcates theo omission of the name.</remarks>
    /// <param name="locationName">Name for the location.</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <param name="geoLong">Geographic longitude.</param>
    /// <returns>Formatted text.</returns>
    public string CreateLocationDescription(string locationName, double geoLat, double geoLong);
}

public sealed class LocationConversion : ILocationConversion
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;

    public LocationConversion(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
    }
    
    
    public string CreateLocationDescription(string locationName, double geoLat, double geoLong)
    {
        string qualifiedLocationName = locationName is null or "" ? "No name for location" : locationName;
        string latDir = geoLat >= 0.0 ? "N" : "S";
        string longDir = geoLong >= 0.0 ? "E" : "W";
        string latSexag = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(Math.Abs(geoLat));
        string longSexag = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(Math.Abs(geoLong));
        return qualifiedLocationName + " " + latSexag + " " + latDir + " / " + longSexag + " " + longDir;
    }

}

