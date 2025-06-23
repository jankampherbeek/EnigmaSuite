// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.ZodiacDivisions;

/// <summary>
/// Orchestrator for the calculation of zodiac divisions
/// </summary>
public class ZodiacDivisionsOrchestrator
{
    
    /// <summary>
    /// Calculate the sign index or planetary index for a longitude, using a method for zodiacal divisions
    /// </summary>
    /// <remarks>Prompt: Based on the request, calculate the index for the given longitude. Depending on the values
    /// of Method in the request select the following classes: Signs -> ZodiacSign, DecansPlanet -> DecanPlanets,
    /// DecansSign -> DecanSign, DodecatsOriginal -> DodecatOriginal, DodecatsPaulus -> DodecatPaulus,
    /// BoundsEgyptian -> Bounds and true for the parameter Egyptian, BoundsPtolemy -> Bounds and false for the parameter
    /// Egyptian
    /// </remarks>
    /// <param name="request">Contains the longitude and the method</param>
    /// <returns>The calculated index</returns>
    public int Calculate(ZodiacDivisionRequest request)
    {
        return request.Method switch
        {
            ZodiacDivisionMethods.Signs => ZodiacSign.IndexForSign(request.Longitude),
            ZodiacDivisionMethods.DecansPlanet => DecanPlanets.IndexForDecanPlanet(request.Longitude),
            ZodiacDivisionMethods.DecansSign => DecanSign.IndexForDecanSign(request.Longitude),
            ZodiacDivisionMethods.DodecatsOriginal => DodecatOriginal.IndexForDodecat(request.Longitude),
            ZodiacDivisionMethods.DodecatsPaulus => DodecatPaulus.IndexForDodecat(request.Longitude),
            ZodiacDivisionMethods.BoundsEgyptian => Bounds.IndexBound(request.Longitude, true),
            ZodiacDivisionMethods.BoundsPtolemy => Bounds.IndexBound(request.Longitude, false),
            _ => throw new ArgumentException($"Unknown zodiac division method: {request.Method}")
        };
    }
    
    
}