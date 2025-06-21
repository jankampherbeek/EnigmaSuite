// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.ZodiacDivisions;

/// <summary>
/// Define sign index for decans based on planets
/// </summary>
public static class DecanPlanets
{
    private static readonly int[] Planets = [0, 2, 3, 1, 4, 5, 6];  // Index numbers for Sun, Mercury, Venus, Moon, Mars,
                                                              // Jupiter and Saturn
    
    /// <summary>
    /// Define sign index for decans based on planets
    /// </summary>
    /// <remarks>Prompt: Check the longitude, it should be equal or larger than 0 and smaller than 360.
    /// Return -1 if the longitude is out of bounds. Otherwise, calculate the index. To do so divide the longitude in 36
    /// portions of 10 degrees. Each portion should get a number, following the sequence of the array planets.
    /// The first portion shold get the number 4, followed by 5, 6, 0, 2 etc.</remarks>
    /// <param name="longitude">The ecliptic longitude which should be minimal 0.0 and smaller than 360.0</param>
    /// <returns>The planet index for the decan based on the planets array</returns>
    public static int IndexForDecanPlanet(double longitude)
    {
        // Check bounds: longitude should be >= 0 and < 360
        if (longitude is < 0.0 or >= 360.0)
        {
            return -1;
        }
        
        // Calculate which decan (0-35) within the 360-degree circle
        // Each decan is 10 degrees, so divide longitude by 10
        var decanIndex = (int)(longitude / 10.0);
        
        // Use the decan index to get the planet index from the planets array
        // The array has 7 elements, so we need to use modulo to cycle through it
        return Planets[decanIndex % Planets.Length];
    }
    
    
}