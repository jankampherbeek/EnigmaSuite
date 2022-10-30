// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Constants;
using Enigma.Domain.Locational;
using Enigma.InputSupport.Conversions;
using Enigma.InputSupport.Interfaces;
using Enigma.InputSupport.Validations;

namespace Enigma.InputSupport.InputParsers;

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
        (int[] geoLongNumbers, bool geoLongSuccess) = _valueRangeConverter.ConvertStringRangeToIntRange(inputGeoLong, EnigmaConstants.SEPARATOR_GEOLONG);
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




