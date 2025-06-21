// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.ZodiacDivisions;


/// <summary>
/// Define the dodecatemoria in the original version (method B according to Otto Neugebauer)
/// </summary>
public static class DodecatOriginal
{

    /// <summary>
    /// Define sign index for dodecateoria
    /// </summary>
    /// <remarks>Prompt: Check the longitude, it should be equal or larger than 0 and smaller than 360.
    /// Return -1 if the longitude is out of bounds. Otherwise, calculate the index. To do so divide the longitude in
    /// 12 parts (signs of the zodiac). Each part gets a number, 0 for Aries up to 11 for Pisces.
    /// Each portion should be divided in twelce subportions of 2.5 degrees each. The first portion should get the same
    /// number as the number for the sign, for the second portion add 1 etc. If the number is larger than 11,
    /// subtract 12. </remarks>
    /// <param name="longitude">The ecliptic longitude which should be minimal 0.0 and smaller than 360.0</param>
    /// <returns>The sign index for the decan, 0 = Aries..11 = Pisces</returns>
    public static int IndexForDodecat(double longitude)
    {
        // Check bounds: longitude should be >= 0 and < 360
        if (longitude is < 0.0 or >= 360.0)
        {
            return -1;
        }
        
        // Calculate which sign (0-11) the longitude falls into
        // Each sign is 30 degrees, so divide longitude by 30
        var signIndex = (int)(longitude / 30.0);
        
        // Calculate which subportion within the sign (0-11)
        // Each subportion is 2.5 degrees, so divide the remainder by 2.5
        var remainderInSign = longitude % 30.0;
        var subportionIndex = (int)(remainderInSign / 2.5);
        
        // Calculate the final index: sign index + subportion index
        var finalIndex = signIndex + subportionIndex;
        
        // If the number is larger than 11, subtract 12
        if (finalIndex > 11)
        {
            finalIndex -= 12;
        }
        
        return finalIndex;
    }
    
}