// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Constants;
using Enigma.Domain.Locational;
using Enigma.InputSupport.Interfaces;

namespace Enigma.InputSupport.Validations;


/// <inheritdoc/>
public class GeoLongValidator : IGeoLongValidator
{
    private readonly int[] _longValues = new int[] { 0, 0, 0 };
    private double _longitude = 0.0;
    private Directions4GeoLong _direction;

    /// <inheritdoc/>
    public bool CreateCheckedLongitude(int[] inputLongValues, Directions4GeoLong direction, out FullGeoLongitude fullLongitude)
    {
        _direction = direction;
        string _fullText = "";
        bool success = (inputLongValues.Length == 3) || (inputLongValues.Length == 2);

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
            _fullText = CreateFullText();  
        }
        fullLongitude = new (_longValues, _longitude, _direction, _fullText);
        return success;
    }

    private string CreateFullText()
    {
        string directionIndicator = _direction == Directions4GeoLong.East ? "+" : "-";
        return $"{directionIndicator}{_longValues[0]}:{_longValues[1]:d2}:{_longValues[2]:d2}";
    }

    private bool CheckMinAndMaxValues(int[] valuesToCheck)
    {
        bool result = true;
        if (valuesToCheck[0] < 0 || valuesToCheck[0] > 180) result = false;
        if (valuesToCheck[1] < 0 || valuesToCheck[1] > 59) result = false;
        if (valuesToCheck[2] < 0 || valuesToCheck[2] > 59) result = false;
        if (valuesToCheck[0] == 180 && (valuesToCheck[1] > 0 || valuesToCheck[2] > 0)) result = false;
        return result;
    }

    private void CalculateLongitude()
    {
        int factor = _direction == Directions4GeoLong.East ? 1 : -1;
        _longitude = (_longValues[0] + (double)_longValues[1] / EnigmaConstants.MINUTES_PER_HOUR_DEGREE + (double)_longValues[2] / EnigmaConstants.SECONDS_PER_HOUR_DEGREE) * factor;

    }
}


