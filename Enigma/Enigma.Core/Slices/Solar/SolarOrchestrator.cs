// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Core.Calc;
using Enigma.Domain.Requests;

namespace Enigma.Core.Slices.Solar;

/// <summary>
/// Orchestrator for the calculation of a solar
/// </summary>
public class SolarOrchestrator(
    IJdForPositionFinder jdForPositionFinder,
    ISeFlags seFlags,
    IChartAllPositionsHandler chartAllPositionsHandler,
    ICelPointSeCalc celPointSeCalc)
{
    /// <summary>
    /// Calculate a solar return chart
    /// </summary>
    /// <param name="request">The solar request containing all necessary parameters</param>
    /// <returns>A dictionary of chart points and their positions</returns>
    public Dictionary<ChartPoints, FullPointPos> CalculateSolar(SolarRequest request)
    {
        // Get the Sun's position at the radix time
        var radixSunPosition = GetSunPositionAtRadixTime(request.JdRadix, request.TropicalReturn);
        
        // Calculate the target Julian Day for the solar return
        var targetJd = CalculateTargetJulianDay(radixSunPosition, request.JdRadix, request.Age, request.TropicalReturn);
        
        // Determine which location to use
        var locationToUse = DetermineLocationToUse(request.RadixLocation, request.RelocateLocation);
        
        // Create the chart calculation request
        var celPointsRequest = new CelPointsRequest(targetJd, locationToUse, request.CalculationPreferences);
        
        // Calculate the full chart
        return chartAllPositionsHandler.CalcFullChart(celPointsRequest);
    }

    private double GetSunPositionAtRadixTime(double radixJd, bool tropicalReturn)
    {
        var flags = seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, 
            tropicalReturn ? ZodiacTypes.Tropical : ZodiacTypes.Sidereal);
        
        var sunPositions = celPointSeCalc.CalculateCelPoint(0, radixJd, flags);
        return sunPositions[0].Position; // Main position (longitude)
    }

    private double CalculateTargetJulianDay(double radixSunPosition, double radixJd, int age, bool tropicalReturn)
    {
        var flags = seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, 
            tropicalReturn ? ZodiacTypes.Tropical : ZodiacTypes.Sidereal);
        
        // Calculate the target Julian Day when the Sun returns to the radix position
        return jdForPositionFinder.FindJulianDay(radixSunPosition, radixJd + 365.25 * age, flags);
    }

    private Location DetermineLocationToUse(Location radixLocation, Location? relocateLocation)
    {
        // If no relocate location is specified, use the radix location
        if (relocateLocation == null)
            return radixLocation;

        // If the relocate location is the same as the radix location, use the radix location
        return relocateLocation.Equals(radixLocation) ? radixLocation :
            // Otherwise, use the relocate location
            relocateLocation;
    }
}