// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.ZodiacDivisions;


/// <summary>
/// Define the index of a sign,
/// </summary>
public static class ZodiacSign
{
    /// <summary>
    /// Define the index for a sign
    /// </summary>
    /// <param name="longitude">The ecliptic longitude which should be minimal 0.0 and smaller than 360.0</param>
    /// <returns>0 for Aries up to 11 for Pisces. Return -1 for longitude that is out of bounds.</returns>
    public static int IndexForSign(double longitude)
    {
        // Check bounds: longitude should be >= 0 and < 360
        if (longitude is < 0.0 or >= 360.0)
        {
            return -1;
        }
        
        // Calculate index by integer division by 30
        // 0-29.999... -> 0 (Aries)
        // 30-59.999... -> 1 (Taurus)
        // ...
        // 330-359.999... -> 11 (Pisces)
        return (int)(longitude / 30.0);
    }
    
}