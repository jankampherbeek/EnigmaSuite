// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Helpers.Interfaces;

namespace Enigma.Frontend.Ui.Support.Validations;

/// <inheritdoc/>
public class GeoLatValidator : IGeoLatValidator
{
    private readonly int[] _latValues = { 0, 0, 0 };
    private double _latitude;
    private Directions4GeoLat _direction;

    /// <inheritdoc/>
    public bool CreateCheckedLatitude(int[] inputLatValues, Directions4GeoLat direction, out FullGeoLatitude fullLatitude)
    {
        _direction = direction;
        string fullText = "";
        bool success = inputLatValues.Length is 3 or 2;

        if (success)
        {
            for (int i = 0; i < inputLatValues.Length; i++)
            {
                _latValues[i] = inputLatValues[i];
            }
            success = CheckMinAndMaxValues(_latValues);
        }

        if (success)
        {
            CalculateLatitude();
            fullText = CreateFullText();
        }
        fullLatitude = new FullGeoLatitude(_latValues, _latitude, _direction, fullText);
        return success;
    }

    private string CreateFullText()
    {
        string directionIndicator = _direction == Directions4GeoLat.North ? "+" : "-";
        return $"{directionIndicator}{_latValues[0]}:{_latValues[1]:d2}:{_latValues[2]:d2}";
    }

    private static bool CheckMinAndMaxValues(IReadOnlyList<int> valuesToCheck)
    {
        bool result = !(valuesToCheck[0] < 0 || valuesToCheck[0] > 89);
        if (valuesToCheck[1] < 0 || valuesToCheck[1] > 59) result = false;
        if (valuesToCheck[2] < 0 || valuesToCheck[2] > 59) result = false;
        return result;
    }

    private void CalculateLatitude()
    {
        int factor = _direction == Directions4GeoLat.North ? 1 : -1;
        _latitude = (_latValues[0] + ((double)_latValues[1] / EnigmaConstants.MINUTES_PER_HOUR_DEGREE) + ((double)_latValues[2] / EnigmaConstants.SECONDS_PER_HOUR_DEGREE)) * factor;

    }
}


