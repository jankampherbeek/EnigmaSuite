// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Locational;

namespace Enigma.Frontend.InputSupport.Validations;

public interface IGeoLatValidator
{
    /// <summary>
    /// Validate input and create a record FullGeoLatitude.
    /// </summary>
    /// <param name="latValues">Array with integers for the latitude in the sequence degree, minute, second. The value for second is optional.</param>
    /// <param name="direction">The direction: north or south.</param>
    /// <param name="fullLatitude">Resulting record FullGeoLatitude.</param>
    /// <returns>True if no error was found, otherwise false.</returns>
    public bool CreateCheckedLatitude(int[] inputLatValues, Directions4GeoLat direction, out FullGeoLatitude fullLatitude);
}


/// <inheritdoc/>
public class GeoLatValidator : IGeoLatValidator
{
    private bool _success = true;
    private readonly int[] _latValues = new int[] { 0, 0, 0 };
    private double _latitude = 0.0;
    private Directions4GeoLat _direction;

    /// <inheritdoc/>
    public bool CreateCheckedLatitude(int[] inputLatValues, Directions4GeoLat direction, out FullGeoLatitude fullLatitude)
    {
        _direction = direction;
        string _fullText = "";
        _success = (inputLatValues.Length == 3) || (inputLatValues.Length == 2);

        if (_success)
        {
            for (int i = 0; i < inputLatValues.Length; i++)
            {
                _latValues[i] = inputLatValues[i];
            }
            _success = CheckMinAndMaxValues(_latValues);
        }

        if (_success)
        {
            CalculateLatitude();
            _fullText = CreateFullText();
        }
        fullLatitude = new(_latValues, _latitude, _direction, _fullText);
        return _success;
    }

    private string CreateFullText()
    {
        string directionTextId = _direction == Directions4GeoLat.North ? "common.direction.north" : "common.direction.south";
        return $"{_latValues[0]:d2}:{_latValues[1]:d2}:{_latValues[2]:d2} [{directionTextId}]";
    }

    private bool CheckMinAndMaxValues(int[] valuesToCheck)
    {
        bool result = true;
        if (valuesToCheck[0] < 0 || valuesToCheck[0] > 89) result = false;
        if (valuesToCheck[1] < 0 || valuesToCheck[1] > 59) result = false;
        if (valuesToCheck[2] < 0 || valuesToCheck[2] > 59) result = false;
        return result;
    }

    private void CalculateLatitude()
    {
        int factor = _direction == Directions4GeoLat.North ? 1 : -1;
        _latitude = (_latValues[0] + (double)_latValues[1] / EnigmaConstants.MINUTES_PER_HOUR_DEGREE + (double)_latValues[2] / EnigmaConstants.SECONDS_PER_HOUR_DEGREE) * factor;

    }
}


