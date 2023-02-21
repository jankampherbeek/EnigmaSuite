// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.using System;

using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Interfaces;
using System;

namespace Enigma.Frontend.Ui.Support;

public sealed class LocationConversion: ILocationConversion
{

    public string CreateLocationDescription(string locationName, double geoLat, double geoLong)
    {
        string qualifiedLocationName = (locationName == null || locationName == "") ? Rosetta.TextForId("common.location.noname") : locationName;
        string latDir = geoLat >= 0.0 ? Rosetta.TextForId("ref.enum.direction4geolat.north") : Rosetta.TextForId("ref.enum.direction4geolat.south"); 
        string longDir = geoLong >= 0.0 ? Rosetta.TextForId("ref.enum.direction4geolong.east") : Rosetta.TextForId("ref.enum.direction4geolong.west");  
        return qualifiedLocationName + " " + Math.Abs(geoLat) + " " + latDir + " / " + Math.Abs(geoLong) + " " + longDir ;
    }

}

