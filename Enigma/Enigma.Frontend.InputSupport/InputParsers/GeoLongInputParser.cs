// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Interfaces;

namespace Enigma.Frontend.Helpers.InputParsers;

/// <inheritdoc/>
public class GeoLongInputParser : IGeoLongInputParser
{
    private readonly IValueRangeConverter _valueRangeConverter;
    private readonly IGeoLongValidator _geoLongValidator;

    public GeoLongInputParser(IValueRangeConverter valueRangeConverter, IGeoLongValidator geoLongValidator)
    {
        _valueRangeConverter = valueRangeConverter;
        _geoLongValidator = geoLongValidator;
    }


    public bool HandleGeoLong(string inputGeoLong, Directions4GeoLong direction, out FullGeoLongitude? fullGeoLongitude)
    {
        fullGeoLongitude = null;
        bool validationSuccess;
        (int[] geoLongNumbers, bool geoLongSuccess) = _valueRangeConverter.ConvertStringRangeToIntRange(inputGeoLong, EnigmaConstants.SeparatorGeolong);
        if (geoLongSuccess)
        {
            validationSuccess = _geoLongValidator.CreateCheckedLongitude(geoLongNumbers, direction, out fullGeoLongitude);
        }
        else
        {
            validationSuccess = false;
        }
        return validationSuccess;
    }
}




