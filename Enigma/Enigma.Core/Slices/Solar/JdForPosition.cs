// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;
using Serilog;

namespace Enigma.Core.Slices.Solar;

public class JdForPosition(SunCalculator sunCalculator, ISeFlags seFlags) 
{

    public double FindJdForPosition(double estimatedJd, double targetPosition, SolarRequest request)
    {
        SeInitializer.SetTopocentric(0.0, 0.0, 0.0);
        var zodiacType = request.CalculationPreferences.ActualZodiacType;
        if (zodiacType == ZodiacTypes.Sidereal)
        {
            SeInitializer.SetAyanamsha(Ayanamshas.Fagan.GetDetails().SeId);
        }
        var observerPos = request.CalculationPreferences.ActualObserverPosition;
        if (observerPos == ObserverPositions.TopoCentric)
        {
            var location = request.RelocateLocation ?? request.RadixLocation;
            SeInitializer.SetTopocentric(location.GeoLong, location.GeoLat, 0.0);
        }
        
        var flags = seFlags.DefineFlags(CoordinateSystems.Ecliptical, observerPos, zodiacType);
        return DefineJd(estimatedJd, targetPosition, flags);
    }
    
    
    
    private double DefineJd(double estimatedJd, double targetPosition, int flags)
    {
        var startPosition = sunCalculator.CalcPositionSun(estimatedJd, flags);
        var angularDiff = targetPosition - startPosition;
        
        // Handle the 0°/360° boundary - find the shortest path
        if (angularDiff > 180) angularDiff -= 360;
        if (angularDiff < -180) angularDiff += 360;
        var timeDiff = Math.Abs(angularDiff);
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
            var diff = targetPosition - trialPosition;
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