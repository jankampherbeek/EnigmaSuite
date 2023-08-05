// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.using System;

using Enigma.Frontend.Ui.Interfaces;
using System;

namespace Enigma.Frontend.Ui.Support;

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

