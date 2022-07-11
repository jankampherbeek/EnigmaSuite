// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Positional;

namespace Enigma.Frontend.InputSupport.Conversions;

/// <summary>
/// Converter from double values to presentable strings.
/// </summary>
public interface IDoubleToDmsConversions
{
    /// <summary>
    /// Convert value for longitude to longitude within a sign accompanied with a string for a glyph.
    /// </summary>
    /// <param name="position">Longitude.</param>
    /// <returns>Tuple with text for longitude in degrees (0 .. 30), minutes and seconds and a string for the glyph.</returns>
    public (string longTxt, string glyph) ConvertDoubleToLongWithGlyph(double position);

    /// <summary>
    /// Convert value to sexagesimal text. Negative values are indicated with a minus sign.
    /// </summary>
    /// <param name="position">The value to convert.</param>
    /// <returns>The saexagesimal result in degrees, minutes and seconds.</returns>
    public string ConvertDoubleToPositionsText(double position);
}

public class DoubleToDmsConversions : IDoubleToDmsConversions
{

    public (string longTxt, string glyph) ConvertDoubleToLongWithGlyph(double position)
    {
        double remaining = position;
        int degrees = (int)position;
        int nrOfSigns = 1 + degrees / 30;
        int degreesInSign = degrees - ((nrOfSigns - 1) * 30);
        remaining = Math.Abs(remaining - degrees);
        int minutes = (int)(remaining * 60.0);
        remaining -= minutes / 60.0;
        int seconds = (int)(remaining * 3600.0);
        string longTxt = CreateDmsString(degreesInSign, minutes, seconds);
        string glyph = DefineGlyph(nrOfSigns);
        return (longTxt, glyph);

    }

    public string ConvertDoubleToPositionsText(double position)
    {
        string minusSign = position < 0.0 ? "-" : "";
        double remaining = Math.Abs(position);              
        int degrees = (int)remaining;              
        remaining = remaining - degrees;
        int minutes = (int)(remaining * 60.0); 
        remaining -= minutes / 60.0;
        int seconds = (int)(remaining * 3600.0);
        return minusSign + CreateDmsString(degrees, minutes, seconds);

    }

    private string CreateDmsString(int degrees, int minutes, int seconds)
    {
        string degreeText = degrees.ToString();
        string minuteText = string.Format("{0:00}", minutes);
        string secondText = string.Format("{0:00}", seconds);
        return degreeText + EnigmaConstants.DEGREE_SIGN + minuteText + EnigmaConstants.MINUTE_SIGN + secondText + EnigmaConstants.SECOND_SIGN;
    }


    private string DefineGlyph(int nrOfSigns)
    {
        var AllGlyphs = new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=" };
        return AllGlyphs[nrOfSigns - 1];
    }


}