// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Facades.Se;
using Serilog;

namespace Enigma.Core.Slices.VenusStarPoint;

/// <summary>
/// The exact date for a Sun-Venus conjunction.
/// </summary>
public class ExactConjunctionDate(ICalcUtFacade calcUtFacade)
{
    
    private const int SE_ID_SUN = 0;
    private const int SE_ID_VENUS = 3;
    private const int SE_FLAGS = 258;
    private const double MARGIN = 1E-6;   // < 1 second 
    private const double MIN_INTERVAL = 1E-12; // Minimum interval size to prevent endless loops
    private const int MAX_ITERATIONS = 1000; // Maximum iterations to prevent endless loops
    
    public double CalculateConjunctiondate(double estimatedJd)
    {
        // Define range from estimatedJD ± 1 day
        var startJd = estimatedJd - 1;
        var endJd = estimatedJd + 1;
        
        // Use binary search to find exact conjunction date
        return FindExactConjunctionDate(startJd, endJd);
    }

    /// <summary>
    /// Finds the exact Julian Day when Sun and Venus have the same longitude within the specified margin.
    /// </summary>
    /// <param name="jdLow">Lower bound Julian Day</param>
    /// <param name="jdHigh">Upper bound Julian Day</param>
    /// <returns>The exact Julian Day of the conjunction</returns>
    private double FindExactConjunctionDate(double jdLow, double jdHigh)
    {
        var counter = 0;
        double tempJd = (jdLow + jdHigh) / 2.0; // Initialize with midpoint
        double longitudeDifference;
        
        do
        {
            counter++;
            if (counter > MAX_ITERATIONS)
            {
                Log.Error("ExactConjunctionDate: Counter reached {MaxIterations}, this is probably an endless loop", MAX_ITERATIONS);
                throw new Exception($"ExactConjunctionDate: Counter reached {MAX_ITERATIONS}, this is probably an endless loop");
            }
            
            // Check if the search interval has become too small
            if (Math.Abs(jdHigh - jdLow) < MIN_INTERVAL)
            {
                Log.Information("Search interval too small ({Interval}), returning best approximation", jdHigh - jdLow);
                break;
            }
            
            tempJd = (jdLow + jdHigh) / 2.0;
            longitudeDifference = CalculateLongitudeDifference(tempJd);
            
            // Determine which half to search next
            if (longitudeDifference > 0)
            {
                // Sun is ahead of Venus, search in the earlier half
                jdHigh = tempJd;
            }
            else
            {
                // Venus is ahead of Sun, search in the later half
                jdLow = tempJd;
            }
            
        } while (Math.Abs(longitudeDifference) > MARGIN);
        
        return tempJd;
    }
    
    /// <summary>
    /// Calculates the difference between Sun and Venus longitudes.
    /// Positive value means Sun is ahead of Venus, negative means Venus is ahead of Sun.
    /// </summary>
    /// <param name="jd">Julian Day</param>
    /// <returns>Longitude difference (Sun - Venus)</returns>
    private double CalculateLongitudeDifference(double jd)
    {
        var sunLongitude = LongitudeSun(jd);
        var venusLongitude = LongitudeVenus(jd);
        
        // Calculate the shortest angular distance between the two longitudes
        var difference = sunLongitude - venusLongitude;
        
        // Handle the 0°/360° boundary - find the shortest path
        if (difference > 180) difference -= 360;
        if (difference < -180) difference += 360;
        
        return difference;
    }

    private double LongitudeSun(double jd)
    {
        return calcUtFacade.PositionFromSe(jd, SE_ID_SUN, SE_FLAGS)[0];
    }
    
    private double LongitudeVenus(double jd)
    {
        return calcUtFacade.PositionFromSe(jd, SE_ID_VENUS, SE_FLAGS)[0];
    }
    
}