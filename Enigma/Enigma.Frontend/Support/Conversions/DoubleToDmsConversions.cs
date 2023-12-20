// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using Enigma.Domain.Constants;
using Enigma.Frontend.Ui.Interfaces;

namespace Enigma.Frontend.Ui.Support.Conversions;


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
        double remaining = Math.Abs(position);
        int degrees = (int)position;
        int nrOfSigns = 1 + (degrees / 30);
        int degreesInSign = degrees - ((nrOfSigns - 1) * 30);
        remaining = Math.Abs(remaining - degrees);
        int minutes = (int)(remaining * 60.0);
        return CreateDmString(degreesInSign, minutes);
    }

    /// <inheritdoc/>
    public (string longTxt, char glyph) ConvertDoubleToDmsWithGlyph(double position)
    {
        double remaining = position;
        int degrees = (int)position;
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