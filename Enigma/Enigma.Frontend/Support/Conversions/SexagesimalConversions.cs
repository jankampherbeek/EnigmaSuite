// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using Enigma.Domain.Constants;
using Enigma.Domain.References;

namespace Enigma.Frontend.Ui.Support.Conversions;

public interface ISexagesimalConversions
{
    /// <summary>
    /// Convert input values for geographic longitude to a double indicating the longitude.
    /// </summary>
    /// <param name="inputLong">String array with degrees, minutes and seconds, in that sequence.</param>
    /// <param name="direction">North results in a positive value, south in a negative value.</param>
    /// <returns>A double representing the value. Positive for north, negative for south.</returns>
    public double InputGeoLongToDouble(string[] inputLong, Directions4GeoLong direction);

    /// <summary>
    /// Convert input values for geographic latitude to a double indicating the latitude.
    /// </summary>
    /// <param name="inputLat">String array with degrees, minutes and seconds, in that sequence.</param>
    /// <param name="direction">East results in a positive value, west in a negative value.</param>
    /// <returns>A double representing the value. Positive for east, negative for west.</returns>
    public double InputGeoLatToDouble(string[] inputLat, Directions4GeoLat direction);

    /// <summary>
    /// Convert input values for time to a double with the hour and fraction.
    /// </summary>
    /// <param name="inputTime">String array with hours, minnutes and seconds, in that sequence.</param>
    /// <returns>A double representing the hour and fractions of the hour.</returns>
    public double InputTimeToDoubleHours(string[] inputTime);

}


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
