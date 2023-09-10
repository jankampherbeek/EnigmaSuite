// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Constants;
using Enigma.Domain.References;
using Enigma.Frontend.Helpers.Interfaces;

namespace Enigma.Frontend.Helpers.Conversions;

/// <inheritdoc/>
public class SexagesimalConversions : ISexagesimalConversions
{
    /// <inheritdoc/>
    public double InputGeoLatToDouble(string[] inputLat, Directions4GeoLat direction)
    {
        return SexagesimalToDouble(inputLat) * (int)direction;
    }

    /// <inheritdoc/>
    public double InputGeoLongToDouble(string[] inputLong, Directions4GeoLong direction)
    {
        return SexagesimalToDouble(inputLong) * (int)direction;
    }

    /// <inheritdoc/>
    public double InputTimeToDoubleHours(string[] inputTime)
    {
        return SexagesimalToDouble(inputTime);
    }

    private static double SexagesimalToDouble(IReadOnlyList<string> texts)
    {
        try
        {
            double value1 = double.Parse(texts[0]);
            double value2 = double.Parse(texts[1]);
            double value3 = double.Parse(texts[2]);
            return value1 + (value2 / EnigmaConstants.MINUTES_PER_HOUR_DEGREE) + (value3 / EnigmaConstants.SECONDS_PER_HOUR_DEGREE);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Error converting to decimal geoLong, using values : " + texts 
                + ". Original exception message : " + e.Message);
        }
    }
}
