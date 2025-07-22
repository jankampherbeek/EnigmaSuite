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
    ICelPointsHandler celPointsHandler,
    ICelPointSeCalc celPointSeCalc)
{
    /// <summary>
    /// Calculate the jd for a solar return chart
    /// </summary>
    /// <param name="request">The solar request containing all necessary parameters</param>
    /// <returns>The calculted jd</returns>
    public double CalculateJdForSolar(SolarRequest request)
    {
        var radixSunPosition = GetSunPositionAtRadixTime(request.JdRadix, request.SiderealReturn, request);
        var targetJd = request.JdRadix + (request.Age + 1) * EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
        var locationToUse = DetermineLocationToUse(request.RadixLocation, request.RelocateLocation);
        var flags = seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical);
        var newJd = jdForPositionFinder.FindJulianDay(radixSunPosition, targetJd, flags);
        return newJd;
    }

    private double GetSunPositionAtRadixTime(double radixJd, bool siderealReturn, SolarRequest request)
    {
        // TODO Observerpositions should depend on configuration
        // var zodiacType = siderealReturn ? ZodiacTypes.Sidereal : ZodiacTypes.Tropical;
        // var flags = seFlags.DefineFlags(CoordinateSystems.Ecliptical, request.CalculationPreferences.ActualObserverPosition, zodiacType);
        
        var locationToUse = DetermineLocationToUse(request.RadixLocation, request.RelocateLocation);
        var sunPositions =
            celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, radixJd, locationToUse, request.CalculationPreferences);
        return sunPositions.Ecliptical.MainPosSpeed.Position; 
    }


    private Location DetermineLocationToUse(Location radixLocation, Location? relocateLocation)
    {
        return relocateLocation ?? radixLocation;
    }
}