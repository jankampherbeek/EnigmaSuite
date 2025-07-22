// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Core.Calc;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Core.Slices.Solar;

/// <summary>
/// Orchestrator for the calculation of the jd for a solar
/// </summary>
public class SolarOrchestrator(
    IJdForPositionFinder jdForPositionFinder,
    ISeFlags seFlags,
    IChartAllPositionsHandler chartAllPositionsHandler,
    ICelPointSeCalc celPointSeCalc)
{
    /// <summary>
    /// Calculate the jd for a solar return chart
    /// </summary>
    /// <param name="request">The solar request containing all necessary parameters</param>
    /// <returns>The calculted jd</returns>
    public double CalculateJdForSolar(SolarRequest request)
    {
        var radixSunPosition = GetSunPositionAtRadixTime(request.JdRadix, request.SiderealReturn);
        var targetJd = request.JdRadix + (request.Age + 1) * EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
        var locationToUse = DetermineLocationToUse(request.RadixLocation, request.RelocateLocation);
        var flags = seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical);
        var newJd = jdForPositionFinder.FindJulianDay(radixSunPosition, targetJd, flags);
        return newJd;
    }

    private double GetSunPositionAtRadixTime(double radixJd, bool siderealReturn)
    {
        // TODO Observerpositions should depend on configuration
        // TODO sidereal should depend on configuration
        // var flags = seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, 
        //     siderealReturn ? ZodiacTypes.Sidereal : ZodiacTypes.Tropical);
        var flags = seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical);
        
        var sunPositions = celPointSeCalc.CalculateCelPoint(0, radixJd, flags);
        return sunPositions[0].Position; 
    }


    private Location DetermineLocationToUse(Location radixLocation, Location? relocateLocation)
    {
        return relocateLocation ?? radixLocation;
    }
}