// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.ProgCalendar;

/// <summary>
/// Find moments that a progressive point makes an aspect with a radix point
/// </summary>
public static class AspectMoments
{

    private static readonly CalcUtFacade CalcUtFacade = new CalcUtFacade();
    
    public static List<ProgCalAspectMatch> FindAspectMoments(ProgressionTypes progType, CalculatedChart calcChart,
        List<ProgCalAspectPoint> aspectPoints, List<ChartPoints> progPoints, double jdStart, double jdEnd)
    {
        var aspectMoments = new List<ProgCalAspectMatch>();
        
        foreach (var progPoint in progPoints)
        {
            var initialStepSize = DefineInitialStepSize(progPoint);
            foreach (var aspectPoint in aspectPoints)
            {
                var radixLongitude = FindRadixLongitude(aspectPoint.ChartPoint, calcChart);
                var newAspectsFound = BinarySearchForAspectMoment(progType, progPoint, aspectPoint, radixLongitude, 
                    jdStart, jdEnd, initialStepSize);
                aspectMoments.AddRange(newAspectsFound);
            }
        }
        
        return aspectMoments;
    }

    public static List<ProgCalAspectPeriodMatch> FindAspectPeriods(ProgressionTypes progType, CalculatedChart calcChart,
        List<ProgCalAspectPoint> aspectPoints, List<ChartPoints> progPoints, double jdStart, double jdEnd, double orb)
    {
        var aspectPeriods = new List<ProgCalAspectPeriodMatch>();
        foreach (var progPoint in progPoints)
        {
            if (progPoint == ChartPoints.Moon) continue;   // skip Moon for periods
            var initialStepSize = DefineInitialStepSize(progPoint);
            foreach (var aspectPoint in aspectPoints)
            {                
                var radixLongitude = FindRadixLongitude(aspectPoint.ChartPoint, calcChart);
                var newPeriodsFound = SearchForAspectPeriod(progType, aspectPoint.ChartPoint, 
                    progPoint, aspectPoint, radixLongitude, jdStart, jdEnd, orb);
                aspectPeriods.AddRange(newPeriodsFound);
            }
        }
        return aspectPeriods;
    }

    private static List<ProgCalAspectMatch> BinarySearchForAspectMoment(ProgressionTypes progType, ChartPoints progPoint, 
        ProgCalAspectPoint aspectPoint, double radixLongitude, double jdStart, double jdEnd, double stepSize)
    {
        const double marginForJd = 0.000001;  // better than 0.1 second of time
        var newAspects = new List<ProgCalAspectMatch>();
        var targetLongitude = aspectPoint.Longitude;
        var jdCurrent = jdStart - stepSize;  // start early to be able to check the first step
        
        while (jdCurrent <= jdEnd)
        {
            if (jdCurrent >= jdStart)
            {
                var jdNew = jdCurrent + stepSize;
                var longProgCurrent = CalcLongitude(progPoint, jdCurrent);
                var longProgNew = CalcLongitude(progPoint, jdNew);
                
                var aspectDetected = CheckForAspectCrossing(longProgCurrent, longProgNew, targetLongitude);
                
                if (aspectDetected)
                {
                    if (Math.Abs(jdNew - jdCurrent) < marginForJd)
                    {
                        newAspects.Add(new ProgCalAspectMatch(
                            progPoint,
                            aspectPoint.ChartPoint,
                            longProgCurrent,
                            radixLongitude,
                            aspectPoint.Aspect,
                            progType,
                            jdCurrent));
                        // no return yet, as more aspects can be found, especially with Moon or retrograde motion
                    }
                    else
                    {
                        newAspects.AddRange(BinarySearchForAspectMoment(progType, progPoint, aspectPoint, radixLongitude,
                            jdCurrent, jdNew, stepSize / 10.0));
                    }
                }
            }
            jdCurrent += stepSize;
        }
        return newAspects;
    }

    private static List<ProgCalAspectPeriodMatch> SearchForAspectPeriod(ProgressionTypes progType, ChartPoints radixPoint, 
        ChartPoints progPoint, ProgCalAspectPoint aspectPoint, double radixLongitude, double jdStart, double jdEnd, double orb)
    {
        var newPeriods = new List<ProgCalAspectPeriodMatch>();
        const double stepSize = 1.0;
        var targetLongitude = aspectPoint.Longitude;
        var jdCurrent = jdStart - stepSize;  // start early to be able to check the first step

        var startDateFound = false;
        var endDateFound = false;
        var startDate = 0.0;
        var endDate = 0.0;
        while (jdCurrent <= jdEnd)
        {
            var jdNew = jdCurrent + stepSize;
            var longProg = CalcLongitude(progPoint, jdCurrent);
            if (CheckForAspectPeriod(longProg, targetLongitude, orb))
            {
                if (!startDateFound)
                {
                    startDateFound = true;
                    startDate = jdCurrent;
                }
            }
            else
            {
                if (startDateFound && !endDateFound)
                {
                    endDateFound = true;
                    endDate = jdCurrent;    
                }                
            }

            jdCurrent++;
            if (!startDateFound || !endDateFound) continue;
            newPeriods.Add(new ProgCalAspectPeriodMatch(progPoint, radixPoint, aspectPoint.Aspect, progType, startDate, endDate));
            Console.WriteLine($"Aspect period found: {progPoint} {aspectPoint.Aspect} {startDate} {endDate}");
            startDateFound = false;     // Continue search for more aspects
            endDateFound = false;
        }
        return newPeriods;
    }
    
    
    /// <summary>
    /// Define initial stepsize based on the ProgPoint involved
    /// </summary>
    /// <param name="factor">The progressive chart point</param>
    /// <returns>Step size: 0.2 for Moon, 2.0 for fast planets, 4.0 for slow planets</returns>
    private static double DefineInitialStepSize(ChartPoints factor)
    {
        var fastFactors = new List<ChartPoints>
        {
            ChartPoints.Sun,
            ChartPoints.Mercury,
            ChartPoints.Venus,
            ChartPoints.Mars
        };
        
        if (factor == ChartPoints.Moon) return 0.2;
        if (fastFactors.Contains(factor)) return 2.0;
        return 4.0;
    }

    /// <summary>
    /// Check if a progressive point crosses a target longitude between two positions
    /// </summary>
    private static bool CheckForAspectCrossing(double longCurrent, double longNew, double targetLon)
    {
        // Normalize to handle 0/360 boundary
        var distanceCurrent = DefineDistance(longCurrent, targetLon);
        var distanceNew = DefineDistance(longNew, targetLon);
        
        // Check if we crossed the target (distance changed from one side to the other)
        // Need a small margin to detect the crossing
        const double detectionMargin = 0.5;  // degrees
        
        if (distanceCurrent < detectionMargin || distanceNew < detectionMargin)
        {
            // Very close, check for actual crossing by sign change
            var diff1 = NormalizeDifference(longCurrent - targetLon);
            var diff2 = NormalizeDifference(longNew - targetLon);
            
            // If signs differ, we crossed
            if ((diff1 < 0 && diff2 > 0) || (diff1 > 0 && diff2 < 0))
            {
                return true;
            }
        }
        
        return false;
    }

    private static bool CheckForAspectPeriod(double longCurrent, double targetLon, double orb)
    {
        var distanceCurrent = DefineDistance(longCurrent, targetLon);
        return distanceCurrent < orb;
    }
    
    
    /// <summary>
    /// Define the shortest distance between two longitudes
    /// </summary>
    private static double DefineDistance(double longitude1, double longitude2)
    {
        var distance = Math.Abs(longitude1 - longitude2);
        if (distance > 360.0) distance -= 360.0;
        if (distance > 180.0) distance = 360.0 - distance;
        return distance;
    }

    /// <summary>
    /// Normalize difference to range -180 to +180
    /// </summary>
    private static double NormalizeDifference(double diff)
    {
        while (diff > 180.0) diff -= 360.0;
        while (diff < -180.0) diff += 360.0;
        return diff;
    }

    /// <summary>
    /// Find the radix longitude for a given chart point from the calculated chart
    /// </summary>
    private static double FindRadixLongitude(ChartPoints chartPoint, CalculatedChart calcChart)
    {
        if (calcChart.Positions.TryGetValue(chartPoint, out var position))
        {
            return position.Ecliptical.MainPosSpeed.Position;
        }
        return 0.0;  // Default if not found
    }

    /// <summary>
    /// Calculate the ecliptic longitude for a given chart point at a specific Julian Day
    /// </summary>
    public static double CalcLongitude(ChartPoints factor, double jd)
    {
        var seId = factor.GetDetails().CalcId;
        const int flags = 2;      // Use SE, no speed
        var positions = CalcUtFacade.PositionFromSe(jd, seId, flags);
        return positions[0];
    }
    
}