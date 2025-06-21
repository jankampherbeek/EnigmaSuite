// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.ZodiacDivisions;

/// <summary>
/// Define the dodecatemoria according to Paulus, essentially the 13th harmonic (method A according to Abraham Sachs)
/// </summary>
public static class DodecatPaulus
{

    /// <summary>
    /// Define sign index for dodecatemoria
    /// </summary>
    /// <remarks>Prompt: Check the longitude, it should be equal or larger than 0 and smaller than 360.
    /// Return -1 if the longitude is out of bounds. Otherwise, calculate the index. To do so multiply the longitude 
    /// with 13. Subtract 360 n times to make sure that the result is smaller than 360 and larger or equal than zero.
    /// Divide the result by 30 (integer division) and return the resulting number, which should be 0..11.
    ///</remarks>
    /// <param name="longitude">The ecliptic longitude which should be minimal 0.0 and smaller than 360.0</param>
    /// <returns>The sign index for the decan, 0 = Aries..11 = Pisces</returns>
    public static int IndexForDodecat(double longitude)
    {
        // Check bounds: longitude should be >= 0 and < 360
        if (longitude is < 0.0 or >= 360.0)
        {
            return -1;
        }
        
        // Multiply the longitude by 13 (13th harmonic)
        var multipliedLongitude = longitude * 13.0;
        
        // Subtract 360 repeatedly until the result is < 360 and >= 0
        while (multipliedLongitude >= 360.0)
        {
            multipliedLongitude -= 360.0;
        }
        
        // Ensure the result is >= 0 (should already be the case, but just to be safe)
        if (multipliedLongitude < 0.0)
        {
            multipliedLongitude += 360.0;
        }
        
        // Divide the result by 30 (integer division) and return the resulting number (0-11)
        return (int)(multipliedLongitude / 30.0);
    }
    
}