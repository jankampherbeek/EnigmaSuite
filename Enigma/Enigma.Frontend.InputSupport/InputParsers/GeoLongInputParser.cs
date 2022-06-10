// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Locational;
using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.InputSupport.Validations;

namespace Enigma.Frontend.InputSupport.InputParsers;

/// <summary>Parse, validate and convert input for geographic longitude.</summary>
public interface IGeoLongInputParser
{
    public bool HandleGeoLong(string inputGeoLong, Directions4GeoLong direction, out FullGeoLongitude fullGeoLongitude);
}


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




