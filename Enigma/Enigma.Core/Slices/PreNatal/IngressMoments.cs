// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.PreNatal;

/// <summary>
/// Handle moments of ingresses in signs
/// </summary>
public static class IngressMoments
{
    private static readonly CalcUtFacade CalcUtFacade = new CalcUtFacade();
    
    /// <summary>
    /// Find the ingresses in a sign for a given period.
    /// </summary>
    /// <param name="factors">The celestial points to use</param>
    /// <param name="startJd">Julian Day Number to start with</param>
    /// <param name="endJd">Julian Day Number that ends the period</param>
    /// <returns>Sorted list of ingresses that were found</returns>
    public static List<Ingress> FindIngressMoments(List<ChartPoints> factors, double startJd, double endJd)
    {
        var ingresses = new List<Ingress>();
        foreach (var factor in factors)
        {
            var ingressesForFactor = FindIngressesForFactors(factor, startJd, endJd);
            ingresses.AddRange(ingressesForFactor);
        }
        ingresses.Sort();
        return ingresses;
    }

    /* Prompt: Scan for the moment that a celestial body (factor) has an ingress (enters a new sign). 
     * If the longitude can exactly be divided by 30, there is an ingress.
     * The accuracy should be better than 0.1 second of arc.
     * Uses a bisection algorithm to find the exact moment of ingress.
     */
    private static List<Ingress> FindIngressesForFactors(ChartPoints factor, double startJd, double endJd)
    {
        var ingresses = new List<Ingress>();
        const double stepSize = 1.0; // 1 day step for initial scan
        const double tolerance = 1.0 / 3600.0 / 360.0 / 10; // 0.1 arcsecond in degrees
        
        var currentJd = startJd;
        var currentLon = CalcLongitude(factor, currentJd);
        
        while (currentJd <= endJd)
        {
            var nextJd = Math.Min(currentJd + stepSize, endJd);
            var nextLon = CalcLongitude(factor, nextJd);
            
            // Check if a sign boundary was crossed by comparing sign numbers
            var currentSign = (int)Math.Floor(currentLon / 30.0);
            var nextSign = (int)Math.Floor(nextLon / 30.0);
            
            // Handle retrograde motion and forward motion
            if (currentSign != nextSign)
            {
                // One or more ingresses occurred, find each one
                var ingressesInInterval = FindIngressInInterval(factor, currentJd, nextJd, currentLon, nextLon, tolerance);
                ingresses.AddRange(ingressesInInterval);
            }
            
            currentJd = nextJd;
            currentLon = nextLon;
        }
        return ingresses;
    }
    
    private static List<Ingress> FindIngressInInterval(ChartPoints factor, double jd1, double jd2, 
        double lon1, double lon2, double tolerance)
    {
        var ingresses = new List<Ingress>();
        
        // Determine the direction of motion
        var deltaLon = lon2 - lon1;
        
        // Handle the 360/0 degree wraparound
        if (deltaLon > 180.0) deltaLon -= 360.0;
        if (deltaLon < -180.0) deltaLon += 360.0;
        
        var isRetrograde = deltaLon < 0;
        
        // Find all sign boundaries crossed in this interval
        var startSign = (int)Math.Floor(lon1 / 30.0);
        var endSign = (int)Math.Floor(lon2 / 30.0);
        
        // Handle wraparound for signs
        if (lon1 < 0) startSign = (int)Math.Floor((lon1 % 360 + 360) / 30.0);
        if (lon2 < 0) endSign = (int)Math.Floor((lon2 % 360 + 360) / 30.0);
        
        // Collect all boundaries crossed
        var boundaries = new List<double>();
        
        if (isRetrograde)
        {
            // Going backwards
            for (var sign = startSign; sign > endSign; sign--)
            {
                boundaries.Add(sign * 30.0);
            }
        }
        else
        {
            // Going forwards
            for (var sign = startSign + 1; sign <= endSign; sign++)
            {
                boundaries.Add(sign * 30.0);
            }
        }
        
        // Find exact JD for each boundary crossing using bisection
        foreach (var boundary in boundaries)
        {
            var ingressJd = FindExactIngressMoment(factor, jd1, jd2, boundary, tolerance);
            var sign = ((int)(boundary / 30.0) % 12) + 1;
            if (sign < 1) sign += 12;
            if (sign > 12) sign -= 12;
            
            ingresses.Add(new Ingress(ingressJd, factor, sign));
        }
        
        return ingresses;
    }
    
    private static double FindExactIngressMoment(ChartPoints factor, double jd1, double jd2, 
        double targetLon, double tolerance)
    {
        var low = jd1;
        var high = jd2;
        
        while (high - low > tolerance / 360.0) // Convert arcsecond tolerance to JD tolerance
        {
            var mid = (low + high) / 2.0;
            var midLon = CalcLongitude(factor, mid);
            
            var lowLon = CalcLongitude(factor, low);
            
            // Determine which half contains the target
            var deltaLow = Math.Abs(NormalizeLongitudeDifference(targetLon - lowLon));
            var deltaMid = Math.Abs(NormalizeLongitudeDifference(targetLon - midLon));
            
            if (deltaMid < tolerance)
            {
                return mid; // Found it with sufficient accuracy
            }
            
            if (deltaMid < deltaLow)
            {
                low = mid;
            }
            else
            {
                high = mid;
            }
        }
        
        return (low + high) / 2.0;
    }
    
    private static double NormalizeLongitudeDifference(double diff)
    {
        while (diff > 180.0) diff -= 360.0;
        while (diff < -180.0) diff += 360.0;
        return diff;
    }
    
    
    private static double CalcLongitude(ChartPoints factor, double jd)
    {
        var seId = factor.GetDetails().CalcId;
        var flags = 2;      // Use SE, no speed
        var positions = CalcUtFacade.PositionFromSe(jd, seId, flags);
        return positions[0];
    }
}