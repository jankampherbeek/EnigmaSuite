﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Helpers.Interfaces;

namespace Enigma.Frontend.Ui.Support.Parsers;

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
        (int[] geoLongNumbers, bool geoLongSuccess) = _valueRangeConverter.ConvertStringRangeToIntRange(inputGeoLong, EnigmaConstants.SEPARATOR_GEOLONG);
        bool validationSuccess = geoLongSuccess && _geoLongValidator.CreateCheckedLongitude(geoLongNumbers, direction, out fullGeoLongitude);
        return validationSuccess;
    }
}



