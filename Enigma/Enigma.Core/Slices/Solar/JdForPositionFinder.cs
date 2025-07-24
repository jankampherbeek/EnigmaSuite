// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;

namespace Enigma.Core.Slices.Solar;

/// <summary>
/// Interface for finding the Julian Day when the Sun reaches a predefined point
/// </summary>
public interface IJdForPositionFinder
{
    /// <summary>
    /// Find the Julian Day when the Sun reaches a given position
    /// </summary>
    /// <param name="position">The position in longitude</param>
    /// <param name="startJd">Estimated start position of the Julian Day</param>
    /// <param name="flags">Calculation flags</param>
    /// <returns>The Julian Day when the Sun reaches the specified position</returns>
    double FindJulianDay(double position, double startJd, int flags);
}

/// <summary>
/// Find the Jd in a given period of one day when the Sun reaches a predefined point
/// </summary>
public class JdForPositionFinder(ICelPointSeCalc celPointSeCalc) : IJdForPositionFinder
{
    private const double SUN_SE_ID = 0; // Sun's SE ID
    private const double MAX_DIFFERENCE = 1E-10; // Maximum difference in degrees (much more precise)
    private const double SEARCH_PERIOD = 1.0; // Search period in days (±0.5 days)
    private const int PORTIONS = 10; // Number of portions to divide the search period

    /// <summary>
    /// Perform a recursive search to find the JD when the Sun reaches a given position. The max difference should be
    /// 1E-10 which results in very high precision.
    /// </summary>
    /// <remarks>
    /// Prompt: find the Julian day when the Sun reaches exactly the positions as defined in the parameters. The position
    /// of the Sum can be calculated with CelPointSECalc.CalculateCelPoint. I would suggest to check a period from
    /// 0.5 day before the startJd until 0.5 day later in about 10 portions, find the correct startJd and endJd for the
    /// portionthat contains the longitude and repeart the process until the difference is negligeable: the difference
    /// should be less than 1E-10 degrees.
    /// </remarks>
    /// <param name="position">The position in longitude</param>
    /// <param name="startJd">Estimated start position of the Julian Day</param>
    /// <param name="flags">Calculation flags</param>
    /// <returns>The Julian Day when the Sun reaches the specified position</returns>
    public double FindJulianDay(double position, double startJd, int flags)
    {
        var startSearchJd = startJd - 0.5; // Start 0.5 days before
        var endSearchJd = startJd + 0.5;   // End 0.5 days after
        var stepSize = SEARCH_PERIOD / PORTIONS;
        position = NormalizeLongitude(position);
        
        // Check if we're already at the target position at the start JD
        var startPosition = GetSunLongitude(startJd, flags);
        var startDifference = Math.Abs(NormalizeLongitude(startPosition - position));
        if (startDifference < MAX_DIFFERENCE)
        {
            return startJd;
        }
        const int maxIterations = 1000; // Prevent infinite loops
        var iterationCount = 0;
        
        while (true)
        {
            iterationCount++;
            if (iterationCount > maxIterations)
            {
                throw new InvalidOperationException($"Search did not converge after {maxIterations} iterations. Target: {position}, StartJD: {startJd}");
            }
            
            // Find the portion that contains the target position
            var (portionStartJd, portionEndJd) = FindPortionWithPosition(position, startSearchJd, stepSize, flags);
            
            // Check if we have reached the required precision using the midpoint of the found portion
            var midJd = (portionStartJd + portionEndJd) / 2.0;
            var sunPosition = GetSunLongitude(midJd, flags);
            var difference = Math.Abs(NormalizeLongitude(sunPosition - position));
            
            if (difference < MAX_DIFFERENCE)
            {
                return midJd;
            }
            
            // If the portion is very small, we're close enough
            if (portionEndJd - portionStartJd < 1E-12) 
            {
                return midJd;
            }
            
            // Refine search to the portion that contains the target
            startSearchJd = portionStartJd;
            endSearchJd = portionEndJd;
            stepSize = (endSearchJd - startSearchJd) / PORTIONS;
        }
    }
    
    /// <summary>
    /// Find the portion (time segment) that contains the target position
    /// </summary>
    private (double startJd, double endJd) FindPortionWithPosition(double targetPosition, double startJd, double stepSize, int flags)
    {
        const double epsilon = 1E-10; // Small tolerance for floating-point comparisons
        var currentJd = startJd;
        var previousPosition = GetSunLongitude(currentJd, flags);
        (double startJd, double endJd) bestPortion = (startJd, startJd + stepSize);
        var bestMidpointDistance = double.MaxValue;
        
        for (var i = 0; i < PORTIONS; i++)
        {
            var nextJd = currentJd + stepSize;
            var currentPosition = GetSunLongitude(nextJd, flags);
            
            // Check if target is exactly at one of the segment boundaries (with epsilon tolerance)
            if (Math.Abs(targetPosition - previousPosition) <= epsilon || 
                Math.Abs(targetPosition - currentPosition) <= epsilon)
            {
                return (currentJd, nextJd);
            }
            
            // Check if the target position is between the two values (with epsilon tolerance)
            if (IsPositionBetween(previousPosition, currentPosition, targetPosition, epsilon))
            {
                return (currentJd, nextJd);
            }
            
            // Check the midpoint of this segment
            var midJd = (currentJd + nextJd) / 2.0;
            var midPosition = GetSunLongitude(midJd, flags);
            var midDistance = Math.Abs(NormalizeLongitude(midPosition - targetPosition));
            if (midDistance < bestMidpointDistance)
            {
                bestMidpointDistance = midDistance;
                bestPortion = (currentJd, nextJd);
            }
            
            previousPosition = currentPosition;
            currentJd = nextJd;
        }
        
        // If no exact match found, return the segment whose midpoint is closest to the target
        return bestPortion;
    }

    
    /// <summary>
    /// Get the Sun's longitude at the specified Julian Day
    /// </summary>
    private double GetSunLongitude(double jd, int flags)
    {
        var positions = celPointSeCalc.CalculateCelPoint((int)SUN_SE_ID, jd, flags);
        return positions[0].Position; // Main position (longitude)
    }
    
    /// <summary>
    /// Check if the target position is between two positions, handling the 0°/360° boundary
    /// </summary>
    private static bool IsPositionBetween(double pos1, double pos2, double target, double epsilon)
    {
        // Normalize all positions to 0-360 range
        pos1 = NormalizeLongitude(pos1);
        pos2 = NormalizeLongitude(pos2);
        target = NormalizeLongitude(target);
        
        // Handle the case where positions cross the 0°/360° boundary
        if (Math.Abs(pos1 - pos2) > 180)
        {
            // One position is near 0°, the other near 360°
            if (pos1 > pos2)
            {
                // pos1 is near 360°, pos2 is near 0°
                return (target >= pos2 - epsilon && target <= 360) || (target >= 0 && target <= pos1 + epsilon);
            }
            // pos2 is near 360°, pos1 is near 0°
            return (target >= pos1 - epsilon && target <= 360) || (target >= 0 && target <= pos2 + epsilon);
        }

        // Normal case: positions are within 180° of each other
        if (pos1 <= pos2)
        {
            return target >= pos1 - epsilon && target <= pos2 + epsilon;
        }
        return target >= pos2 - epsilon && target <= pos1 + epsilon;
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