﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Frontend.Ui.Support.Validations;

public interface IGeoLongValidator
{
    /// <summary>
    /// Validate input and create a record FullGeoLongitude.
    /// </summary>
    /// <param name="inputLongValues">Array with integers for the longitude in the sequence degree, minute, second. The value for second is optional.</param>
    /// <param name="direction">The direction: east or west.</param>
    /// <param name="fullLongitude">Resulting record FullGeoLongitude.</param>
    /// <returns>True if no error was found, otherwise false.</returns>
    public bool CreateCheckedLongitude(int[] inputLongValues, Directions4GeoLong direction, out FullGeoLongitude fullLongitude);
}

/// <inheritdoc/>
public class GeoLongValidator : IGeoLongValidator
{
    private readonly int[] _longValues = { 0, 0, 0 };
    private double _longitude;
    private Directions4GeoLong _direction;

    /// <inheritdoc/>
    public bool CreateCheckedLongitude(int[] inputLongValues, Directions4GeoLong direction, out FullGeoLongitude fullLongitude)
    {
        _direction = direction;
        string fullText = "";
        bool success = inputLongValues.Length is 3 or 2;

        if (success)
        {
            for (int i = 0; i < inputLongValues.Length; i++)
            {
                _longValues[i] = inputLongValues[i];
            }
            success = CheckMinAndMaxValues(_longValues);
        }
        if (success)
        {
            CalculateLongitude();
            fullText = CreateFullText();
        }
        fullLongitude = new FullGeoLongitude(_longValues, _longitude, _direction, fullText);
        return success;
    }

    private string CreateFullText()
    {
        string directionIndicator = _direction == Directions4GeoLong.East ? "+" : "-";
        return $"{directionIndicator}{_longValues[0]}:{_longValues[1]:d2}:{_longValues[2]:d2}";
    }

    private static bool CheckMinAndMaxValues(IReadOnlyList<int> valuesToCheck)
    {
        bool result = !(valuesToCheck[0] < 0 || valuesToCheck[0] > 180);
        if (valuesToCheck[1] < 0 || valuesToCheck[1] > 59) result = false;
        if (valuesToCheck[2] < 0 || valuesToCheck[2] > 59) result = false;
        if (valuesToCheck[0] == 180 && (valuesToCheck[1] > 0 || valuesToCheck[2] > 0)) result = false;
        return result;
    }

    private void CalculateLongitude()
    {
        int factor = _direction == Directions4GeoLong.East ? 1 : -1;
        _longitude = (_longValues[0] + ((double)_longValues[1] / EnigmaConstants.MINUTES_PER_HOUR_DEGREE) + ((double)_longValues[2] / EnigmaConstants.SECONDS_PER_HOUR_DEGREE)) * factor;

    }
}


