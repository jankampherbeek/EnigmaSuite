// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.PreNatal;

public class RetroDirectMoments
{
    private static readonly CalcUtFacade CalcUtFacade = new CalcUtFacade();
    
    public static List<RetroDirect> FindRetroDirectMoments(List<ChartPoints> factors, double startJd, double endJd)
    {
        var retroDirectMoments = new List<RetroDirect>();
        foreach (var factor in factors)
        {
            var rdsForFactor = FindRetroDirectMomentsForFactor(factor, startJd, endJd);
            retroDirectMoments.AddRange(rdsForFactor);
        }
        retroDirectMoments.Sort();
        return retroDirectMoments;
    }

    /* Prompt: Scan for the moment that a celestial body (factor) changes direction: becomes either direct or retrograde.
     * The method CalcLongitudeAndSpeed() can be used to calculate the longitude and speed of a celestial point at a given jd.
     * The accuracy should be better than 0.1 second of arc.
     * Uses a bisection algorithm to find the exact moment when speed becomes zero.
     */
    private static List<RetroDirect> FindRetroDirectMomentsForFactor(ChartPoints factor, double startJd,
        double endJd)
    {
        var retroDirectMoments = new List<RetroDirect>();
        const double stepSize = 1.0; // 1 day step for initial scan
        const double tolerance = 1.0 / 3600.0 / 360.0 / 10; // 0.1 arcsecond in degrees
        
        var currentJd = startJd;
        var (_, currentSpeed) = CalcLongitudeAndSpeed(factor, currentJd);
        
        while (currentJd <= endJd)
        {
            var nextJd = Math.Min(currentJd + stepSize, endJd);
            var (_, nextSpeed) = CalcLongitudeAndSpeed(factor, nextJd);
            
            // Check if speed changed sign (direction change)
            if (currentSpeed * nextSpeed < 0)
            {
                // A direction change occurred, find the exact moment
                var moment = FindExactDirectionChangeMoment(factor, currentJd, nextJd, tolerance);
                retroDirectMoments.Add(moment);
            }
            
            currentJd = nextJd;
            currentSpeed = nextSpeed;
        }
        
        return retroDirectMoments;
    }
    
    private static RetroDirect FindExactDirectionChangeMoment(ChartPoints factor, double jd1, double jd2, double tolerance)
    {
        var low = jd1;
        var high = jd2;
        var (_, speedLow) = CalcLongitudeAndSpeed(factor, low);
        
        // Determine what direction it's becoming (if speed was negative, it's becoming direct)
        var becomingDirect = speedLow < 0;
        
        // Use bisection to find when speed = 0
        while (high - low > tolerance / 360.0)
        {
            var mid = (low + high) / 2.0;
            var (_, speedMid) = CalcLongitudeAndSpeed(factor, mid);
            
            if (Math.Abs(speedMid) < tolerance * 0.001) // Very close to zero
            {
                var (lonAtChange, _) = CalcLongitudeAndSpeed(factor, mid);
                return new RetroDirect(mid, lonAtChange, becomingDirect ? 'D' : 'R');
            }
            
            // If speed has same sign as low, move low up; otherwise move high down
            if (speedMid * speedLow > 0)
            {
                low = mid;
                speedLow = speedMid;
            }
            else
            {
                high = mid;
            }
        }
        
        var finalJd = (low + high) / 2.0;
        var (finalLon, _) = CalcLongitudeAndSpeed(factor, finalJd);
        return new RetroDirect(finalJd, finalLon, becomingDirect ? 'D' : 'R');
    }
    
    /// <summary>
    /// Calculate the longitude and speed of a celestial point at a given jd.
    /// </summary>
    /// <param name="factor">The celestial point</param>
    /// <param name="jd">Julian Day Number</param>
    /// <returns>A typle with the longitude (item1) and speed (item2)</returns>
    private static (double, double) CalcLongitudeAndSpeed(ChartPoints factor, double jd)
    {
        var seId = factor.GetDetails().CalcId;
        var flags = 2 + 256;      // Use SE and speed
        var positions = CalcUtFacade.PositionFromSe(jd, seId, flags);
        return (positions[0], positions[3]);  // respectively longitude and speed
    }
}