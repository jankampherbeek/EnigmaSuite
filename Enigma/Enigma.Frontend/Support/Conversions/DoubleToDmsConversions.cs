// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using Enigma.Core.Calc;
using Enigma.Domain.Constants;

namespace Enigma.Frontend.Ui.Support.Conversions;

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

/// <inheritdoc/>
public sealed class DoubleToDmsConversions : IDoubleToDmsConversions
{
    /// <inheritdoc/>
    public string ConvertDoubleToDmsInSignNoGlyph(double position)
    {
        return ConvertDoubleToDmsWithGlyph(position).longTxt;
    }

    /// <inheritdoc/>
    public string ConvertDoubleToDmInSignNoGlyph(double position)
    {
        double posInRange = RangeUtil.ValueToRange(position, 0.0, 360.0);
        double remaining = posInRange;
        int degrees = (int)posInRange;
        int nrOfSigns = 1 + (degrees / 30);
        int degreesInSign = degrees - ((nrOfSigns - 1) * 30);
        remaining = Math.Abs(remaining - degrees);
        int minutes = (int)(remaining * 60.0);
        return CreateDmString(degreesInSign, minutes);
    }

    /// <inheritdoc/>
    public (string longTxt, char glyph) ConvertDoubleToDmsWithGlyph(double position)
    {
        double posInRange = RangeUtil.ValueToRange(position, 0.0, 360.0);
        double remaining = posInRange;
        int degrees = (int)posInRange;
        int nrOfSigns = 1 + (degrees / 30);
        int degreesInSign = degrees - ((nrOfSigns - 1) * 30);
        remaining = Math.Abs(remaining - degrees);
        int minutes = (int)(remaining * 60.0);
        remaining -= minutes / 60.0;
        int seconds = (int)(remaining * 3600.0);
        string longTxt = CreateDmsString(degreesInSign, minutes, seconds);
        char glyph = DefineGlyph(nrOfSigns);
        return (longTxt, glyph);

    }

    /// <inheritdoc/>
    public string ConvertDoubleToPositionsDmsText(double position)
    {
        string minusSign = position < 0.0 ? "-" : "";
        const double correctionForDouble = 0.00000001;    // correction to prevent double values like 0.99999999999
        double remaining = Math.Abs(position) + correctionForDouble;
        if (remaining >= 360.0) remaining-= 360.0;
        int degrees = (int)remaining;
        remaining -= degrees;
        int minutes = (int)(remaining * 60.0);
        remaining -= minutes / 60.0;
        int seconds = (int)(remaining * 3600.0);
        return minusSign + CreateDmsString(degrees, minutes, seconds);

    }

    private static string CreateDmsString(int degrees, int minutes, int seconds)
    {
        string degreeText = degrees.ToString();
        string minuteText = $"{minutes:00}";
        string secondText = $"{seconds:00}";
        return degreeText + EnigmaConstants.DEGREE_SIGN + minuteText + EnigmaConstants.MINUTE_SIGN + secondText + EnigmaConstants.SECOND_SIGN;
    }

    private static string CreateDmString(int degrees, int minutes)
    {
        string degreeText = degrees.ToString();
        string minuteText = $"{minutes:00}";
        return degreeText + EnigmaConstants.DEGREE_SIGN + minuteText + EnigmaConstants.MINUTE_SIGN;
    }

    private static char DefineGlyph(int nrOfSigns)
    {
        var allGlyphs = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=' };
        return allGlyphs[nrOfSigns - 1];
    }


}