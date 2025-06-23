// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.ZodiacDivisions;

namespace Enigma.Api.Slices;

/// <summary>
/// Service for zodiac divisions
/// </summary>
public class ZodiacDivisionsService
{

    /// <summary>
    /// Calculate the index, either planetary or zodiacal, for a given longitude and using a specified method.
    /// If the result is zodiacal, it will return 0..12 for Aries..Pisces.
    /// If the result is planetary it will rutern 0..6 for Sun, Moon, Mercury, Venus, Mars, Jupiter and Saturn
    /// </summary>
    /// <remarks>Prompt: Check the longitude, it should be equal or larger than 0 and smaller than 360.
    /// Return an Exception if the longitude is out of bounds. Call ZodiacDivisionsOrchestrator to perform the calculations.
    /// Handle the exception that could be thrown by the orchestrator.
    /// </remarks>
    /// <param name="longitude">The ecliptic longitude which should be minimal 0.0 and smaller than 360.0</param>
    /// <param name="method">The method to be used</param>
    /// <returns>The index for the longtude/method combination or an exception if the longitude is out of range or the
    /// method is unknown.</returns>
    public int FindIndexForDivision(double longitude, ZodiacDivisionMethods method)
    {
        // Check if longitude is within bounds
        if (longitude is < 0.0 or >= 360.0)
        {
            throw new ArgumentException($"Longitude must be between 0.0 and 360.0 (exclusive), but was {longitude}");
        }
        
        try
        {
            // Create the orchestrator and request
            var orchestrator = new ZodiacDivisionsOrchestrator();
            var request = new ZodiacDivisionRequest(longitude, method);
            
            // Call the orchestrator to perform the calculation
            return orchestrator.Calculate(request);
        }
        catch (ArgumentException ex)
        {
            // Re-throw the exception with additional context
            throw new ArgumentException($"Error calculating zodiac division for longitude {longitude} with method {method}: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            throw new InvalidOperationException($"Unexpected error calculating zodiac division for longitude {longitude} with method {method}: {ex.Message}", ex);
        }
    }
    
    
}