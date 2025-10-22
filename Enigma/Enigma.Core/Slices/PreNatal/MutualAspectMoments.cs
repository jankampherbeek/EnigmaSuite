// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.PreNatal;

/// <summary>
/// Handle mutaul aspects for pre natal
/// </summary>
public static class MutualAspectMoments
{
    private static readonly CalcUtFacade CalcUtFacade = new CalcUtFacade();

    public static List<MutualAspect> FindMutualAspectMoments(List<ChartPoints> factors, List<AspectTypes> aspects,
        double startJd, double endJd)
    {
        var mutualAspectMoments = new List<MutualAspect>();
        for (var i = 0; i < factors.Count; i++)
        {
            for (var j = i+1; j < factors.Count; j++)
            {
                var factor1 = factors[i];
                var factor2 = factors[j];
                var aspectsFound = FindMutualAspectMomentsForFactors(factor1, factor2, aspects, startJd, endJd);
                mutualAspectMoments.AddRange(aspectsFound);
            }
        }
        return mutualAspectMoments;
    }
    
    /* Prompt: Scan for the moment that an exact aspect is formed between two celestial points (factor1 and factor2).
     * The angle of the aspects is defined by the AspectTypes.
     * The method CalcLongitude() can be used to calculate the longitude of a celestial point at a given jd.
     * The accuracy should be better than 0.1 second of arc.
     * Uses a bisection algorithm to find the exact moment when the aspect is exact.
     */
    private static List<MutualAspect> FindMutualAspectMomentsForFactors(ChartPoints factor1, ChartPoints factor2,
        List<AspectTypes> aspects, double startJd, double endJd)
    {
        var aspectsFound = new List<MutualAspect>();
        const double stepSize = 1.0; // 1 day step for initial scan
        const double tolerance = 1.0 / 3600.0 / 10.0; // 0.1 arcsecond in degrees
        const double duplicateThreshold = 0.6; // Within ~14.4 hours - should be same aspect
        
        // Get all aspect angles we're looking for
        var aspectAngles = aspects.Select(a => a.GetDetails().Angle).Distinct().ToList();
        
        // Build a list of all distance measurements first
        var measurements = new List<(double jd, double dist)>();
        for (double jd = startJd; jd <= endJd; jd += stepSize)
        {
            double lon1 = CalcLongitude(factor1, jd);
            double lon2 = CalcLongitude(factor2, jd);
            double dist = CalculateAngularDistance(lon1, lon2);
            measurements.Add((jd, dist));
        }
        
        // Now scan for aspect crossings
        for (int i = 0; i < measurements.Count - 1; i++)
        {
            foreach (var aspectAngle in aspectAngles)
            {
                bool crossed = false;
                double searchStartJd = measurements[i].jd;
                double searchEndJd = measurements[i + 1].jd;
                
                if (Math.Abs(aspectAngle) < 0.01) // Conjunction (0 degrees)
                {
                    // For conjunction, detect local minimum (valley) at position i
                    // Need i-1, i, and i+1 to confirm a valley
                    // Only detect at the TRUE minimum to avoid duplicates
                    if (i > 0 && i < measurements.Count - 1)
                    {
                        double beforeDist = measurements[i - 1].dist;
                        double currentDist = measurements[i].dist;
                        double nextDist = measurements[i + 1].dist;
                        
                        // Valley detected: before > current < next with clear differences
                        // Use a threshold to ensure it's a real valley, not noise
                        const double valleyThreshold = 0.05; // degrees
                        bool isValley = (beforeDist - currentDist > valleyThreshold) && 
                                       (nextDist - currentDist > valleyThreshold) && 
                                       currentDist < 5.0;
                        
                        if (isValley)
                        {
                            crossed = true;
                            // Search around the valley: from before to next
                            searchStartJd = measurements[i - 1].jd;
                            searchEndJd = measurements[i + 1].jd;
                        }
                    }
                }
                else
                {
                    // For other aspects, check if crossed between measurements[i] and measurements[i+1]
                    double dist1 = measurements[i].dist;
                    double dist2 = measurements[i + 1].dist;
                    double minDist = Math.Min(dist1, dist2);
                    double maxDist = Math.Max(dist1, dist2);
                    
                    // Aspect is crossed if it lies between the two measurements
                    if (aspectAngle >= minDist - 0.001 && aspectAngle <= maxDist + 0.001)
                    {
                        // Make sure there's actual movement (not just noise)
                        if (Math.Abs(maxDist - minDist) > 0.01)
                        {
                            crossed = true;
                            searchStartJd = measurements[i].jd;
                            searchEndJd = measurements[i + 1].jd;
                        }
                    }
                }
                
                if (crossed)
                {
                    // Find the exact moment using bisection
                    var exactMoment = FindExactAspectMoment(factor1, factor2, aspectAngle, searchStartJd, searchEndJd, tolerance);
                    
                    // Check for duplicates - aspects within duplicateThreshold days with same type
                    bool isDuplicate = aspectsFound.Any(a => 
                        Math.Abs(a.PreNatalJd - exactMoment.PreNatalJd) < duplicateThreshold && 
                        a.Aspect == exactMoment.Aspect &&
                        a.Factor1 == exactMoment.Factor1 &&
                        a.Factor2 == exactMoment.Factor2);
                    
                    if (!isDuplicate)
                    {
                        aspectsFound.Add(exactMoment);
                    }
                }
            }
        }
        
        return aspectsFound;
    }
    
    private static double CalculateAngularDistance(double lon1, double lon2)
    {
        var distance = Math.Abs(lon2 - lon1);
        // Normalize to 0-360
        while (distance >= 360.0) distance -= 360.0;
        // Take the shortest arc
        if (distance > 180.0) distance = 360.0 - distance;
        return distance;
    }
    
    private static bool AspectCrossed(double distance1, double distance2, double aspectAngle)
    {
        // Check if the aspect angle was crossed between the two time points
        // The aspect is crossed if it lies between the two distance measurements
        
        var min = Math.Min(distance1, distance2);
        var max = Math.Max(distance1, distance2);
        
        // The aspect was crossed if the aspect angle is between the two distances
        // We also check that we're not exactly at the aspect angle at both points (avoid edge cases)
        return (aspectAngle >= min && aspectAngle <= max) && (distance1 != distance2);
    }
    
    private static MutualAspect FindExactAspectMoment(ChartPoints factor1, ChartPoints factor2, 
        double aspectAngle, double jd1, double jd2, double tolerance)
    {
        var low = jd1;
        var high = jd2;
        AspectTypes aspectType = AspectTypes.Conjunction;
        
        // Find the aspect type from the angle
        foreach (AspectTypes asp in Enum.GetValues(typeof(AspectTypes)))
        {
            if (Math.Abs(asp.GetDetails().Angle - aspectAngle) < 0.001)
            {
                aspectType = asp;
                break;
            }
        }
        
        const int maxIterations = 100;
        var iterations = 0;
        const double minTimeStep = 0.00001; // About 0.86 seconds in JD
        
        while (high - low > minTimeStep && iterations < maxIterations)
        {
            iterations++;
            
            // Calculate distances at the three points
            var lon1Low = CalcLongitude(factor1, low);
            var lon2Low = CalcLongitude(factor2, low);
            var distanceLow = CalculateAngularDistance(lon1Low, lon2Low);
            
            var mid = (low + high) / 2.0;
            var lon1Mid = CalcLongitude(factor1, mid);
            var lon2Mid = CalcLongitude(factor2, mid);
            var distanceMid = CalculateAngularDistance(lon1Mid, lon2Mid);
            
            var lon1High = CalcLongitude(factor1, high);
            var lon2High = CalcLongitude(factor2, high);
            var distanceHigh = CalculateAngularDistance(lon1High, lon2High);
            
            // Check if we're close enough to the exact aspect at mid
            if (Math.Abs(distanceMid - aspectAngle) < tolerance)
            {
                return new MutualAspect(mid, factor1, factor2, lon1Mid, lon2Mid, aspectType);
            }
            
            // For conjunction (aspectAngle = 0), find the minimum distance
            if (Math.Abs(aspectAngle) < 0.01)
            {
                // Use ternary search logic to find minimum
                // If mid is less than both endpoints, we're getting close
                if (distanceMid < distanceLow && distanceMid < distanceHigh)
                {
                    // Mid is the best so far, but we need to determine which side to search
                    // Calculate a point slightly to the left and right to determine slope
                    var quarter1 = (low + mid) / 2.0;
                    var quarter3 = (mid + high) / 2.0;
                    
                    var lon1Q1 = CalcLongitude(factor1, quarter1);
                    var lon2Q1 = CalcLongitude(factor2, quarter1);
                    var distanceQ1 = CalculateAngularDistance(lon1Q1, lon2Q1);
                    
                    var lon1Q3 = CalcLongitude(factor1, quarter3);
                    var lon2Q3 = CalcLongitude(factor2, quarter3);
                    var distanceQ3 = CalculateAngularDistance(lon1Q3, lon2Q3);
                    
                    // Search the side with the smaller distance
                    if (distanceQ1 < distanceQ3)
                    {
                        high = mid;
                    }
                    else
                    {
                        low = mid;
                    }
                }
                else if (distanceLow < distanceHigh)
                {
                    // Minimum is in the lower half
                    high = mid;
                }
                else
                {
                    // Minimum is in the upper half
                    low = mid;
                }
            }
            else
            {
                // For other aspects, find where the aspect angle is crossed
                // Determine which half contains the aspect angle
                var deltaLow = Math.Abs(distanceLow - aspectAngle);
                var deltaMid = Math.Abs(distanceMid - aspectAngle);
                var deltaHigh = Math.Abs(distanceHigh - aspectAngle);
                
                // The aspect angle should be between low and mid, or between mid and high
                bool inLowerHalf = (aspectAngle >= Math.Min(distanceLow, distanceMid) && 
                                   aspectAngle <= Math.Max(distanceLow, distanceMid));
                bool inUpperHalf = (aspectAngle >= Math.Min(distanceMid, distanceHigh) && 
                                   aspectAngle <= Math.Max(distanceMid, distanceHigh));
                
                if (inLowerHalf && !inUpperHalf)
                {
                    high = mid;
                }
                else if (inUpperHalf && !inLowerHalf)
                {
                    low = mid;
                }
                else
                {
                    // If ambiguous, use the half with smaller error
                    if (deltaLow + deltaMid < deltaMid + deltaHigh)
                    {
                        high = mid;
                    }
                    else
                    {
                        low = mid;
                    }
                }
            }
        }
        
        // Final calculation - use the best point among low, mid, high
        var finalMid = (low + high) / 2.0;
        
        var finalLon1Low = CalcLongitude(factor1, low);
        var finalLon2Low = CalcLongitude(factor2, low);
        var finalDistanceLow = CalculateAngularDistance(finalLon1Low, finalLon2Low);
        
        var finalLon1Mid = CalcLongitude(factor1, finalMid);
        var finalLon2Mid = CalcLongitude(factor2, finalMid);
        var finalDistanceMid = CalculateAngularDistance(finalLon1Mid, finalLon2Mid);
        
        var finalLon1High = CalcLongitude(factor1, high);
        var finalLon2High = CalcLongitude(factor2, high);
        var finalDistanceHigh = CalculateAngularDistance(finalLon1High, finalLon2High);
        
        // Choose the point closest to the aspect angle
        var errorLow = Math.Abs(finalDistanceLow - aspectAngle);
        var errorMid = Math.Abs(finalDistanceMid - aspectAngle);
        var errorHigh = Math.Abs(finalDistanceHigh - aspectAngle);
        
        if (errorLow <= errorMid && errorLow <= errorHigh)
        {
            return new MutualAspect(low, factor1, factor2, finalLon1Low, finalLon2Low, aspectType);
        }
        else if (errorMid <= errorHigh)
        {
            return new MutualAspect(finalMid, factor1, factor2, finalLon1Mid, finalLon2Mid, aspectType);
        }
        else
        {
            return new MutualAspect(high, factor1, factor2, finalLon1High, finalLon2High, aspectType);
        }
    }
    
    
    
    private static double CalcLongitude(ChartPoints factor, double jd)
    {
        var seId = factor.GetDetails().CalcId;
        var flags = 2;      // Use SE, no speed
        var positions = CalcUtFacade.PositionFromSe(jd, seId, flags);
        return positions[0];
    }
    
}