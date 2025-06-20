// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.ZodiacDivisions;

/// <summary>
/// Define sign index for decans based on signs
/// </summary>
public class DecanSign
{
    /// <summary>
    /// Define sign index for decans based on signs
    /// </summary>
    /// <remarks>Prompt: Check the longitude, it should be equal or larger than 0 and smaller than 360.
    /// Return -1 if the longitude is out of bounds. Otherwise, calculate the index. To do so divide the longitude in
    /// 12 parts (signs of the zodiac). Each part gets a number, 0 for Aries up to 11 for Pisces.
    /// Each portion should be divided in three subportions of 10 degrees each. The first portion should get the same
    /// number as the number for the sign, for the second portion add 4 and again 4 for the third portion. If the number
    /// is larger than 11, subtract 12. </remarks>
    /// <param name="longitude">The ecliptic longitude which should be minimal 0.0 and smaller than 360.0</param>
    /// <returns>The sign index for the decan, 0 = Aries..11 = Pisces</returns>
    public int IndexForDecanSign(double longitude)
    {
        // Check bounds: longitude should be >= 0 and < 360
        if (longitude is < 0.0 or >= 360.0)
        {
            return -1;
        }
        
        // Calculate the zodiac sign index (0-11)
        var signIndex = (int)(longitude / 30.0);
        
        // Calculate which decan within the sign (0-2)
        // Each sign is divided into three 10-degree portions
        var decanWithinSign = (int)((longitude % 30.0) / 10.0);
        
        // Apply decan rules:
        // First decan (0): same as sign index
        // Second decan (1): sign index + 4
        // Third decan (2): sign index + 8
        var decanSignIndex = signIndex + (decanWithinSign * 4);
        
        // If result is larger than 11, subtract 12
        if (decanSignIndex > 11)
        {
            decanSignIndex -= 12;
        }
        
        return decanSignIndex;
    }
    
}