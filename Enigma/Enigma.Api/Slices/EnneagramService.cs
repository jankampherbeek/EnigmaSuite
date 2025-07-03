// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.Enneagram;
using Serilog;

namespace Enigma.Api.Slices;

/// <summary>
/// Service for thealculation of an Enneagram
/// </summary>
public class EnneagramService
{

    /// <summary>
    /// Define the strengths for the 9 Enneagram-types
    /// </summary>
    /// <remarks>
    /// Prompt: This service should act as a facade for the calculation of an Enneagram. It should check if the
    /// request is not null, geoLon is not larger than 180.0 and not smaller than -180.0 and also that the geoLat is
    /// smaller than 66.0 and larger than -66.0. If not, it should return an empty list and log the error. If the input
    /// is ok, it should call EnneagramOrchestrator and return the results.
    /// </remarks>
    /// <param name="request">Request with data</param>
    /// <returns>List of calculated Enneagram strengths</returns>
    public List<KeyValuePair<int, double>> DefineEnneagramStrengths(EnneagramRequest request)
    {
        // Check if request is null
        if (request == null)
        {
            Log.Error("EnneagramService.DefineEnneagramStrengths: Request is null");
            return new List<KeyValuePair<int, double>>();
        }

        // Validate longitude bounds (-180.0 to 180.0)
        if (request.GeoLon < -180.0 || request.GeoLon > 180.0)
        {
            Log.Error("EnneagramService.DefineEnneagramStrengths: Invalid longitude {Longitude}. Must be between -180.0 and 180.0 degrees", request.GeoLon);
            return new List<KeyValuePair<int, double>>();
        }

        // Validate latitude bounds (-90.0 to 90.0, exclusive)
        if (request.GeoLat <= -66.0 || request.GeoLat >= 66.0)
        {
            Log.Error("EnneagramService.DefineEnneagramStrengths: Invalid latitude {Latitude}. Must be between -66.0 and 60.0 degrees (exclusive)", request.GeoLat);
            return new List<KeyValuePair<int, double>>();
        }

        // Input validation passed, proceed with calculation
        Log.Information("EnneagramService.DefineEnneagramStrengths: Calculating Enneagram strengths for Julian Day {JulianDay}, Longitude {Longitude}, Latitude {Latitude}, TimeKnown {TimeKnown}, PlutoDouble {PlutoDouble}", 
            request.JulianDay, request.GeoLon, request.GeoLat, request.IsTimeKnown, request.IsDoublePluto);

        try
        {
            var orchestrator = new EnneagramOrchestrator();
            var result = orchestrator.CalcEnneagramStrengths(request);
            
            // Convert from List<KeyValuePair<int, double[]>> to List<KeyValuePair<int, double>>
            var convertedResult = result.Select(kvp => new KeyValuePair<int, double>(kvp.Key, kvp.Value[0])).ToList();
            
            Log.Information("EnneagramService.DefineEnneagramStrengths: Successfully calculated {Count} Enneagram strengths", convertedResult.Count);
            return convertedResult;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "EnneagramService.DefineEnneagramStrengths: Exception occurred during calculation");
            return new List<KeyValuePair<int, double>>();
        }
    }
    
}