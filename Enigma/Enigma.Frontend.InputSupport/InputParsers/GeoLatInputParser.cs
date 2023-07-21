// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Interfaces;


namespace Enigma.Frontend.Helpers.InputParsers;


/// <inheritdoc/>
public class GeoLatInputParser : IGeoLatInputParser
{
    private readonly IValueRangeConverter _valueRangeConverter;
    private readonly IGeoLatValidator _geoLatValidator;

    public GeoLatInputParser(IValueRangeConverter valueRangeConverter, IGeoLatValidator geoLatValidator)
    {
        _valueRangeConverter = valueRangeConverter;
        _geoLatValidator = geoLatValidator;
    }


    public bool HandleGeoLat(string inputGeoLat, Directions4GeoLat direction, out FullGeoLatitude? fullGeoLatitude)
    {
        fullGeoLatitude = null;
        bool validationSuccess;
        (int[] geoLatNumbers, bool geoLatSuccess) = _valueRangeConverter.ConvertStringRangeToIntRange(inputGeoLat, EnigmaConstants.SEPARATOR_GEOLAT);
        if (geoLatSuccess)
        {
            validationSuccess = _geoLatValidator.CreateCheckedLatitude(geoLatNumbers, direction, out fullGeoLatitude);
        }
        else
        {
            validationSuccess = false;
        }
        return validationSuccess;
    }
}




