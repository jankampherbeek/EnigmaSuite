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
        for (int i = 0; i < factors.Count; i++)
        {
            for (int j = i+1; j < factors.Count; j++)
            {
                var factor1 = factors[i];
                var factor2 = factors[j];
                var aspectsFound = FindMutualAspectMomentsForFactors(factor1, factor2, aspects, startJd, endJd);
                mutualAspectMoments.AddRange(aspectsFound);
            }
        }
        mutualAspectMoments.Sort();
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
        
        // Convert aspect types to their angles
        var aspectAngles = aspects.Select(a => new { Type = a, Angle = a.GetDetails().Angle }).ToList();
        
        var currentJd = startJd;
        var lon1Current = CalcLongitude(factor1, currentJd);
        var lon2Current = CalcLongitude(factor2, currentJd);
        var currentDistance = CalculateAngularDistance(lon1Current, lon2Current);
        
        while (currentJd <= endJd)
        {
            var nextJd = Math.Min(currentJd + stepSize, endJd);
            var lon1Next = CalcLongitude(factor1, nextJd);
            var lon2Next = CalcLongitude(factor2, nextJd);
            var nextDistance = CalculateAngularDistance(lon1Next, lon2Next);
            
            // Check each aspect to see if it was crossed
            foreach (var aspect in aspectAngles)
            {
                if (AspectCrossed(currentDistance, nextDistance, aspect.Angle))
                {
                    var exactMoment = FindExactAspectMoment(factor1, factor2, aspect.Angle, currentJd, nextJd, tolerance);
                    aspectsFound.Add(exactMoment);
                }
            }
            
            currentJd = nextJd;
            currentDistance = nextDistance;
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
        // Check if the aspect angle is between distance1 and distance2
        
        // For conjunction, we need to detect when we're very close to 0
        // This happens when the distance reaches a minimum
        if (aspectAngle == 0.0)
        {
            // Check if either point is close to 0
            // The bisection will find the exact minimum
            var threshold = 2.0; // degrees - should be larger than typical daily motion
            return distance1 < threshold || distance2 < threshold;
        }
        
        // For other aspects, check if the aspect angle is crossed
        var min = Math.Min(distance1, distance2);
        var max = Math.Max(distance1, distance2);
        
        return aspectAngle >= min && aspectAngle <= max;
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
        
        while (high - low > tolerance / 360.0)
        {
            var mid = (low + high) / 2.0;
            var lon1Mid = CalcLongitude(factor1, mid);
            var lon2Mid = CalcLongitude(factor2, mid);
            var distanceMid = CalculateAngularDistance(lon1Mid, lon2Mid);
            
            // Check if we're close enough to the exact aspect
            if (Math.Abs(distanceMid - aspectAngle) < tolerance)
            {
                return new MutualAspect(mid, factor1, factor2, lon1Mid, lon2Mid, aspectType);
            }
            
            var lon1Low = CalcLongitude(factor1, low);
            var lon2Low = CalcLongitude(factor2, low);
            var distanceLow = CalculateAngularDistance(lon1Low, lon2Low);
            
            // Determine which half to search
            var deltaLow = Math.Abs(distanceLow - aspectAngle);
            var deltaMid = Math.Abs(distanceMid - aspectAngle);
            
            if (deltaMid < deltaLow)
            {
                low = mid;
            }
            else
            {
                high = mid;
            }
        }
        
        // Final calculation
        var finalJd = (low + high) / 2.0;
        var lon1Final = CalcLongitude(factor1, finalJd);
        var lon2Final = CalcLongitude(factor2, finalJd);
        
        return new MutualAspect(finalJd, factor1, factor2, lon1Final, lon2Final, aspectType);
    }
    
    
    
    private static double CalcLongitude(ChartPoints factor, double jd)
    {
        var seId = factor.GetDetails().CalcId;
        var flags = 2;      // Use SE, no speed
        var positions = CalcUtFacade.PositionFromSe(jd, seId, flags);
        return positions[0];
    }
    
}