// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.Solar;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Core.Calc;
using Enigma.Facades.Se;
using Serilog;

namespace Enigma.Api.Slices;

/// <summary>
/// Service for the calculation of a solar
/// </summary>
public class SolarService
{
    private readonly SolarOrchestrator _orchestrator;

    public SolarService(SolarOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    /// <summary>
    /// Calculate the jd for a solar
    /// </summary>
    /// <remarks>
    /// Prompt: check the request: it should not be null, it should contain an age that is larger than zero, and the
    /// CalculationPreferences should not be null. Log any error and throw an exception if an error occurs.
    /// If no errors occurred, call SolarOrchestrator and perform the calculation. Make no unit test but do make
    /// integration tests that should live in Enigma.Test.Integration
    /// </remarks>
    /// <param name="request">The request</param>
    /// <returns>The calculated jd</returns>
    public double CalculateJdForSolar(SolarRequest request)
    {
        if (request == null)
        {
            Log.Error("SolarService.CalculateSolar: Request is null");
            throw new ArgumentNullException(nameof(request), "Request cannot be null");
        }
        if (request.Age <= 0)
        {
            Log.Error("SolarService.CalculateSolar: Age must be larger than zero, but was {Age}", request.Age);
            throw new ArgumentException("Age must be larger than zero", nameof(request.Age));
        }

        // Validate CalculationPreferences is not null
        if (request.CalculationPreferences == null)
        {
            Log.Error("SolarService.CalculateSolar: CalculationPreferences cannot be null");
            throw new ArgumentException("CalculationPreferences cannot be null", nameof(request.CalculationPreferences));
        }

        try
        {
            // Call the orchestrator to perform the calculation
            var result = _orchestrator.CalculateJdForSolar(request);
            
            Log.Information("SolarService.CalculateSolar: Successfully calculated jd for solar for age {Age}, sidereal return: {SiderealReturn}, jdRadix: {JdRadix}", 
                request.Age, request.SiderealReturn, request.JdRadix);
            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "SolarService.CalculateSolar: Exception occurred during calculation for age {Age}", request.Age);
            throw;
        }
    }
    
}