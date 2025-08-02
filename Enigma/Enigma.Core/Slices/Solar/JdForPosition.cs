// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.References;
using Enigma.Facades.Se;
using Serilog;

namespace Enigma.Core.Slices.Solar;

public class JdForPosition(SunCalculator sunCalculator, ISeFlags seFlags) 
{

    public double JdForTropicalTopocentricPosition(double estimatedJd, double targetPosition)
    {
        var flags = seFlags.DefineFlags(CoordinateSystems.Ecliptical, 
            ObserverPositions.GeoCentric, ZodiacTypes.Tropical);
        return DefineJd(estimatedJd, targetPosition, flags);
    }
    
    public double JdForSiderealTopocentricPosition(double estimatedJd, double targetPosition)
    {
        var flags = seFlags.DefineFlags(CoordinateSystems.Ecliptical, 
            ObserverPositions.GeoCentric, ZodiacTypes.Sidereal);
        SeInitializer.SetAyanamsha(Ayanamshas.Fagan.GetDetails().SeId);
        return DefineJd(estimatedJd, targetPosition, flags);
    }
    
    
    
    private double DefineJd(double estimatedJd, double targetPosition, int flags)
    {
        Log.Information($"TargetPosition: {targetPosition}");
        
        // Get the Sun's position at the estimated JD
        var startPosition = sunCalculator.CalcPositionSun(estimatedJd, flags);
        Log.Information($"Start position at estimated JD: {startPosition}");
        
        // Calculate the angular difference and determine search direction
        var angularDiff = targetPosition - startPosition;
        
        // Handle the 0°/360° boundary - find the shortest path
        if (angularDiff > 180) angularDiff -= 360;
        if (angularDiff < -180) angularDiff += 360;
        
        // Convert angular difference to time (Sun moves ~1° per day)
        // Use a more precise factor of 1.0 days per degree for better accuracy
        var timeDiff = Math.Abs(angularDiff) * 1.0;
        
        double jdLow, jdHigh;
        if (angularDiff < 0)
        {
            // Target is before start position, search backward
            jdHigh = estimatedJd;
            jdLow = estimatedJd - timeDiff;
        }
        else
        {
            // Target is after start position, search forward
            jdLow = estimatedJd;
            jdHigh = estimatedJd + timeDiff;
        }
        
        // Verify our search bounds
        var lowPosition = sunCalculator.CalcPositionSun(jdLow, flags);
        var highPosition = sunCalculator.CalcPositionSun(jdHigh, flags);
        
        Log.Information($"Angular diff: {angularDiff}°, Time diff: {timeDiff} days");
        Log.Information($"Search bounds: jdLow={jdLow} (pos={lowPosition}), jdHigh={jdHigh} (pos={highPosition})");
        
        const double maxDelta = 1E-8; // Target precision: ~0.01 arc-seconds (1E-8 degrees)
        const double minInterval = 1E-12; // Minimum interval size to prevent endless loops
        double tempJd;
        double delta;
        var counter = 0;
        
        do
        {
            counter++;
            if (counter > 1000)
            {
                Log.Error("JdForPositions: Counter reached 1000, this is probably an endless loop");
                throw new Exception("JdForPositions: Counter reached 1000, this is probably an endless loop");
            }
            
            tempJd = (jdLow + jdHigh) / 2.0;
            
            // Check if the search interval has become too small
            if (Math.Abs(jdHigh - jdLow) < minInterval)
            {
                Log.Information($"Search interval too small ({jdHigh - jdLow}), returning best approximation");
                break;
            }
            
            var trialPosition = sunCalculator.CalcPositionSun(tempJd, flags);
            Log.Information($">>>>>>> trialPosition : {trialPosition}, jdLow: {jdLow}, jdHigh: {jdHigh}");
            
            // Calculate the shortest angular distance
            var diff = targetPosition - trialPosition;
            
            // Handle the 0°/360° boundary
            if (diff > 180) diff -= 360;
            if (diff < -180) diff += 360;
            
            delta = Math.Abs(diff);
            if (diff < 0.0) 
            {
                jdHigh = tempJd; // Target is before trial position
            }
            else 
            {
                jdLow = tempJd;  // Target is after trial position
            }
            
        } while (delta > maxDelta);
        
        return tempJd;
    }
    
    /// <summary>
    /// Normalize longitude to 0-360 range
    /// </summary>
    private static double NormalizeLongitude(double longitude)
    {
        while (longitude < 0)
            longitude += 360;
        while (longitude >= 360)
            longitude -= 360;
        return longitude;
    }
}