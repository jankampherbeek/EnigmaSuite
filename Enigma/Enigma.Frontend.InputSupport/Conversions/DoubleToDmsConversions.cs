// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.InputSupport.Interfaces;

namespace Enigma.InputSupport.Conversions;


public class DoubleToDmsConversions : IDoubleToDmsConversions
{
    public string ConvertDoubleToDmsInSignNoGlyph(double position)
    {
        return ConvertDoubleToDmsWithGlyph(position).longTxt;
    }
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


    public (string longTxt, string glyph) ConvertDoubleToDmsWithGlyph(double position)
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
        string glyph = DefineGlyph(nrOfSigns);
        return (longTxt, glyph);

    }

    public string ConvertDoubleToPositionsDmsText(double position)
    {
        string minusSign = position < 0.0 ? "-" : "";
        double remaining = Math.Abs(position);
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
        string minuteText = string.Format("{0:00}", minutes);
        string secondText = string.Format("{0:00}", seconds);
        return degreeText + EnigmaConstants.DEGREE_SIGN + minuteText + EnigmaConstants.MINUTE_SIGN + secondText + EnigmaConstants.SECOND_SIGN;
    }

    private static string CreateDmString(int degrees, int minutes)
    {
        string degreeText = degrees.ToString();
        string minuteText = string.Format("{0:00}", minutes);
        return degreeText + EnigmaConstants.DEGREE_SIGN + minuteText + EnigmaConstants.MINUTE_SIGN;
    }

    private static string DefineGlyph(int nrOfSigns)
    {
        var AllGlyphs = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=" };
        return AllGlyphs[nrOfSigns - 1];
    }


}