// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.Enneagram;
using Enigma.Domain.References;
using Serilog;
using System.Linq;

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
    /// smaller than 66.0 and larger than -66.0. It should also check if the numer of points in the request is larger
    /// than zero. Additionally, it should check the points in the request: the only supported values are the
    /// ChartPoints Sun, Moon, Mercury, Venus, Mars, Jupiter, Saturn, Uranus, Neptune, Pluto, Chiron, TrueNode, ApogeeMean.
    /// If these conditions are not met, it should return an empty list and log the error.
    /// If the input is ok, it should call EnneagramOrchestrator and return the results.
    /// </remarks>
    /// <param name="request">Request with data</param>
    /// <returns>List of calculated Enneagram strengths</returns>
    public List<KeyValuePair<int, double>> DefineEnneagramStrengths(EnneagramRequest request)
    {

        // Validate longitude bounds (-180.0 to 180.0)
        if (request.GeoLon < -180.0 || request.GeoLon > 180.0)
        {
            Log.Error("EnneagramService.DefineEnneagramStrengths: Invalid longitude {Longitude}. Must be between -180.0 and 180.0 degrees", request.GeoLon);
            return [];
        }

        // Validate latitude bounds (-66.0 to 66.0, exclusive)
        if (request.GeoLat <= -66.0 || request.GeoLat >= 66.0)
        {
            Log.Error("EnneagramService.DefineEnneagramStrengths: Invalid latitude {Latitude}. Must be between -66.0 and 66.0 degrees (exclusive)", request.GeoLat);
            return [];
        }

        // Check if the number of points in the request is larger than zero
        if (request.Points.Count == 0)
        {
            Log.Error("EnneagramService.DefineEnneagramStrengths: No points provided in request");
            return [];
        }

        // Validate that only supported ChartPoints are used
        var supportedPoints = new HashSet<ChartPoints>
        {
            ChartPoints.Sun,
            ChartPoints.Moon,
            ChartPoints.Mercury,
            ChartPoints.Venus,
            ChartPoints.Mars,
            ChartPoints.Jupiter,
            ChartPoints.Saturn,
            ChartPoints.Uranus,
            ChartPoints.Neptune,
            ChartPoints.Pluto,
            ChartPoints.Chiron,
            ChartPoints.TrueNode,
            ChartPoints.ApogeeMean
        };

        var unsupportedPoints = request.Points.Where(point => !supportedPoints.Contains(point)).ToList();
        if (unsupportedPoints.Count != 0)
        {
            Log.Error("EnneagramService.DefineEnneagramStrengths: Unsupported chart points found: {UnsupportedPoints}", string.Join(", ", unsupportedPoints));
            return [];
        }

        // Input validation passed, proceed with calculation
        Log.Information("EnneagramService.DefineEnneagramStrengths: Calculating Enneagram strengths for Julian Day {JulianDay}, Longitude {Longitude}, Latitude {Latitude}, TimeKnown {TimeKnown}, PlutoDouble {PlutoDouble}", 
            request.JulianDay, request.GeoLon, request.GeoLat, request.UseHouses, request.IsDoublePluto);

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
            return [];
        }
    }
    
}