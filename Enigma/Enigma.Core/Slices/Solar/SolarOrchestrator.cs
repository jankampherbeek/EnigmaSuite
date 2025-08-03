// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Core.Calc;
using Enigma.Domain.Requests;
using Enigma.Facades.Se;
using Serilog;

namespace Enigma.Core.Slices.Solar;

/// <summary>
/// Orchestrator for the calculation of the jd for a solar
/// </summary>
public class SolarOrchestrator(
    JdForPosition jdForPosition,
    ICelPointSeCalc positionCelPointSeCalc,
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
        // add 0.1 to prevent searching in previous year
        var estimatedJd = request.JdRadix + 0.1 + request.Age * EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
        
        var newJd = jdForPosition.FindJdForPosition(estimatedJd, radixSunPosition, request);
        return newJd;
    }

    private double GetSunPositionAtRadixTime(double radixJd, bool siderealReturn, SolarRequest request)
    {
        // Six possibilites
        // Tropical, geocentric, tropical return
        // Tropical, topocentric, tropical return
        // Tropical, geocentric, sidereal return
        // Tropical, topocentric, sidereal return
        // Sidereal, geocentric, sidereal return
        // Sidereal, topocentric, sidereal return
        // (Sidereal should never have a tropical return)
        
        var zodiacType = request.CalculationPreferences.ActualZodiacType;      
        if (zodiacType == ZodiacTypes.Sidereal || siderealReturn)
        {
            SeInitializer.SetAyanamsha(Ayanamshas.Fagan.GetDetails().SeId);
            zodiacType = ZodiacTypes.Sidereal;
        }
        SeInitializer.SetTopocentric(0.0, 0.0, 0.0); // (Re)set to geocentric
        var observerPos = request.CalculationPreferences.ActualObserverPosition;
        if (observerPos == ObserverPositions.TopoCentric)
        {        
            var location = request.RelocateLocation ?? request.RadixLocation;
            SeInitializer.SetTopocentric(location.GeoLong, location.GeoLat, 0.0);
        }
        
        var radixFlags = seFlags.DefineFlags(CoordinateSystems.Ecliptical, observerPos, zodiacType);

        var pos = CreateLongitudeForSun(radixJd, radixFlags);
        return pos;
    }


    
    private double CreateLongitudeForSun(double julDay, int flags)
    {
        var seId = ChartPoints.Sun.GetDetails().CalcId;
        var pos = positionCelPointSeCalc.CalculatePosForSingleCoord(seId, julDay, flags, true);
        return pos;
    }
    
}