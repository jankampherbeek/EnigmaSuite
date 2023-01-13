// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;

namespace Enigma.Frontend.Helpers.Interfaces;

// interfaces for conversions of inputted data.

/// <summary>
/// Converter from double values to presentable strings.
/// </summary>
public interface IDoubleToDmsConversions
{
    /// <summary>
    /// Convert value for longitude to longitude in degrees and minutes, within a sign (0..29 degrees) but without a glyph.
    /// </summary>
    /// <param name="position">Longitude.</param>
    /// <returns>Text for longitude in degrees (0 .. 29) and minutes.</returns>
    public string ConvertDoubleToDmInSignNoGlyph(double position);
    /// <summary>
    /// Convert value for longitude to longitude in degrees, minutes and seconds, within a sign, accompanied with a string for a glyph.
    /// </summary>
    /// <param name="position">Longitude.</param>
    /// <returns>Tuple with text for longitude in degrees (0 .. 29), minutes and seconds and a string for the glyph.</returns>
    public (string longTxt, char glyph) ConvertDoubleToDmsWithGlyph(double position);

    /// <summary>
    /// Convert value for longitude to longitude in degrees, minutes and seconds, within a sign (0..29 degrees) but without a glyph.
    /// </summary>
    /// <param name="position">Longitude.</param>
    /// <returns>Text for longitude in degrees (0 .. 29), minutes and seconds.</returns>
    public string ConvertDoubleToDmsInSignNoGlyph(double position);

    /// <summary>
    /// Convert value to sexagesimal text. Negative values are indicated with a minus sign.
    /// </summary>
    /// <param name="position">The value to convert.</param>
    /// <returns>The saexagesimal result in degrees, minutes and seconds.</returns>
    public string ConvertDoubleToPositionsDmsText(double position);
}


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


/// <summary>
/// Convert array with range of values from one type to another type.
/// </summary>
public interface IValueRangeConverter
{
    /// <summary>
    /// Convert a string with a separator character to a range of integers.
    /// </summary>
    /// <param name="text">The string containing the substrings with the values, separated with the separator char.
    /// An empty or null string is considered an error.</param>
    /// <param name="separator">The character that separates the parts in the string 'text'.</param>
    /// <returns>A tuple with the converted numbers and an indication if the conversions was successful. 
    /// If the conversion was not successful the value of the numbers can be undefined.</returns>
    public (int[] numbers, bool success) ConvertStringRangeToIntRange(string text, char separator);
}

