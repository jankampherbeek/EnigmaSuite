// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.PreNatal;

public class MutualConjunctionsOppositions
{
    private const double ArcsecondToDegrees = 1.0 / 3600.0;
    private const double HighAccuracyThreshold = 0.1 / 3600.0; // 0.1 arcseconds
    
    public List<MutualAspect> FindAspects(ChartPoints[] factors, double startJD, double endJD, AspectTypes aspectType)
    {
        var mutAspects = new List<MutualAspect>();
        var aspects = new List<AspectEvent>();
        
        // Start with 1-day intervals for initial search
        double currentJD = startJD;
        while (currentJD < endJD)
        {
            double intervalEnd = Math.Min(currentJD + 1.0, endJD);
            
            // Check if an aspect occurs in this 1-day interval
            var aspect = FindAspectInInterval(factors, currentJD, intervalEnd, aspectType, 0);
            if (aspect != null)
            {
                aspects.Add(aspect);
                // Skip ahead to avoid finding the same aspect multiple times
                currentJD = aspect.JulianDay + 1.0;
            }
            else
            {
                currentJD = intervalEnd;
            }
        }

        foreach (var aspect in aspects)
        {
            var mutAspect = new MutualAspect(aspect.JulianDay, factors[0], factors[1], aspect.Longitude1, aspect.Longitude2,
                aspectType);
            mutAspects.Add(mutAspect);       
        }
        return mutAspects;
        
    }

    private AspectEvent FindAspectInInterval(ChartPoints[] factors, double jdStart, double jdEnd, AspectTypes aspectType, int recursionDepth)
    {
        const int maxRecursionDepth = 30; // Prevents infinite recursion
        
        // Calculate angular differences at start and end of interval
        double diffStart = GetTargetAngleDifference(factors, jdStart, aspectType);
        double diffEnd = GetTargetAngleDifference(factors, jdEnd, aspectType);
        
        // Check if we crossed the aspect point (sign change)
        if (diffStart * diffEnd > 0)
        {
            // No aspect in this interval - both differences have same sign
            return null;
        }
        
        // If interval is small enough and accurate enough, return the result
        double intervalSize = jdEnd - jdStart;
        double midJD = (jdStart + jdEnd) / 2.0;
        double midDiff = GetTargetAngleDifference(factors, midJD, aspectType);
        
        if (intervalSize < 1e-8 || Math.Abs(midDiff) < HighAccuracyThreshold || recursionDepth >= maxRecursionDepth)
        {
            // Found aspect with sufficient accuracy
            double longitude1 = NormalizeAngle(LongitudeCalculator.CalcLongitude(factors[0], midJD));
            double longitude2 = NormalizeAngle(LongitudeCalculator.CalcLongitude(factors[1], midJD));
            
            return new AspectEvent
            {
                JulianDay = midJD,
                Longitude1 = longitude1,
                Longitude2 = longitude2,
                AspectType = aspectType,
                AngularSeparation = Math.Abs(CalculateSmallestAngularSeparation(longitude1, longitude2)),
                Accuracy = Math.Abs(midDiff)
            };
        }
        
        // Recursively search in the half that contains the aspect
        if (diffStart * midDiff <= 0)
        {
            // Aspect is in the first half
            return FindAspectInInterval(factors, jdStart, midJD, aspectType, recursionDepth + 1);
        }
        else
        {
            // Aspect is in the second half
            return FindAspectInInterval(factors, midJD, jdEnd, aspectType, recursionDepth + 1);
        }
    }
    
    public List<MutualAspect> FindAllAspects(ChartPoints[] factors, double startJD, double endJD)
    {
        var allAspects = new List<MutualAspect>();
        
        // Find conjunctions
        var conjunctions = FindAspects(factors, startJD, endJD, AspectTypes.Conjunction);
        allAspects.AddRange(conjunctions);
        
        // Find oppositions
        var oppositions = FindAspects(factors, startJD, endJD, AspectTypes.Opposition);
        allAspects.AddRange(oppositions);
        
        // Sort by Julian Day
        // allAspects.Sort((a, b) => a.JulianDay.CompareTo(b.JulianDay));
        
        return allAspects;
    }
    
    private double GetTargetAngleDifference(ChartPoints[] factors, double jd, AspectTypes aspectType)
    {
        double actualDiff = CalculateAngularDifference(factors, jd);
        double target = aspectType == AspectTypes.Conjunction ? 0.0 : 180.0;
        
        // Calculate how far we are from the target aspect
        double deviation = actualDiff - target;
        
        // For conjunctions, return as-is (already properly normalized)
        if (aspectType == AspectTypes.Conjunction)
        {
            return deviation; // This is just actualDiff since target is 0
        }
        
        // For oppositions, we need special handling to avoid the discontinuity at ±180°
        // The issue: when actualDiff wraps from -180 to +180 (or vice versa), we get false triggers
        // Solution: Only normalize if we're not near the ±180 boundary
        if (deviation > 180.0)
        {
            deviation -= 360.0;
        }
        else if (deviation < -180.0)
        {
            deviation += 360.0;
        }
        
        // Additional check: ensure we're actually close to an opposition (within ±90°)
        // This prevents detecting oppositions at conjunctions
        double absActualDiff = Math.Abs(actualDiff);
        if (absActualDiff < 90.0)
        {
            // We're close to a conjunction, not an opposition
            // Return a value that won't trigger a sign change for opposition detection
            // Map [0, 90] to a consistent sign (e.g., always negative)
            return -90.0 - absActualDiff; // Always negative for separations 0-90°
        }
        
        return deviation;
    }
    
    private double CalculateAngularDifference(ChartPoints[] factors, double jd)
    {
        double lon1 = NormalizeAngle(LongitudeCalculator.CalcLongitude(factors[0], jd));
        double lon2 = NormalizeAngle(LongitudeCalculator.CalcLongitude(factors[1], jd));
        // Calculate the signed angular difference
        double signedDiff = lon1 - lon2;
        
        // Normalize to [-180, 180) range
        if (signedDiff >= 180.0)
            signedDiff -= 360.0;
        else if (signedDiff < -180.0)
            signedDiff += 360.0;
        
        return signedDiff;
    }
    
    private double CalculateSmallestAngularSeparation(double lon1, double lon2)
    {
        double diff = Math.Abs(lon1 - lon2);
        return Math.Min(diff, 360.0 - diff);
    }
    
    private double NormalizeAngle(double angle)
    {
        // Normalize angle to [0, 360) range
        angle %= 360.0;
        if (angle < 0)
        {
            angle += 360.0;
        }
        return angle;
    }
    
    private double NormalizeAngleDifference(double difference)
    {
        // Normalize angle difference to [-180, 180) range
        difference %= 360.0;
        if (difference > 180.0)
            difference -= 360.0;
        else if (difference <= -180.0)
            difference += 360.0;
        
        return difference;
    }
    
}

public class AspectEvent
{
    public double JulianDay { get; set; }
    public double Longitude1 { get; set; }
    public double Longitude2 { get; set; }
    public AspectTypes AspectType { get; set; }
    public double AngularSeparation { get; set; }
    public double Accuracy { get; set; }
    
    public override string ToString()
    {
        string aspectName = AspectType == AspectTypes.Conjunction ? "Conjunction" : "Opposition";
        return $"{aspectName} at JD {JulianDay:F8} " +
               $"(Separation: {AngularSeparation * 3600:F3}\", " +
               $"Accuracy: {Accuracy * 3600:F3}\")";
    }
}

