// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Frontend.Helpers.Support;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.Charts.Progressive;

public class ChartProgInputSolarController
{

    public List<string> GetDirections4GeoLong()
    {
        List<string> geoLongDirections = new();
        List<Directions4GeoLongDetails> _directions4GeoLongDetails = Directions4GeoLong.East.AllDetails();
        for (int i = 0; i < _directions4GeoLongDetails.Count; i++)
        {
            Directions4GeoLongDetails? direction4GeoLongDetail = _directions4GeoLongDetails[i];
            geoLongDirections.Add(Rosetta.TextForId(direction4GeoLongDetail.Text));
        }
        return geoLongDirections;
    }

    public List<string> GetDirections4GeoLat()
    {
        List<string> geoLatDirections = new();
        List<Directions4GeoLatDetails> _directions4GeoLatDetails = Directions4GeoLat.North.AllDetails();
        for (int i = 0; i < _directions4GeoLatDetails.Count; i++)
        {
            Directions4GeoLatDetails? direction4GeoLongDetail = _directions4GeoLatDetails[i];
            geoLatDirections.Add(Rosetta.TextForId(direction4GeoLongDetail.Text));
        }
        return geoLatDirections;
    }


}
