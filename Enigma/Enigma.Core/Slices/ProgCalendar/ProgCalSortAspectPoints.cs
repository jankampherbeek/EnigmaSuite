// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.ProgCalendar;

/// <summary>
/// Sorted aspect points
/// </summary>
public static class ProgCalSortAspectPoints
{
    /// <summary>
    /// Create a sorted list of points that form an aspect
    /// </summary>
    /// <param name="chartPoints">Radix chartpoints</param>
    /// <param name="aspectTypes">Aspects</param>
    /// <returns>List with aspected points, sorted on longitude</returns>
    public static List<ProgCalAspectPoint> CreateSortedAspectPoints(CalculatedChart calcChart, List<ChartPoints> chartPoints, List<AspectTypes> aspectTypes)
    {
        var aspPoints = new List<ProgCalAspectPoint>();
        var aspPointsSorted = new List<ProgCalAspectPoint>();
        foreach (var point in chartPoints)
        {
            
            var longitude = FindLongitude(point, calcChart);
            foreach (var aspect in aspectTypes)
            {
                if (aspect is AspectTypes.Conjunction or AspectTypes.Opposition) // only add one angle for opposition and one angel of zero degrees for conjunction
                {
                    aspPoints.Add(new ProgCalAspectPoint(point, aspect, longitude + aspect.GetDetails().Angle));
                }
                else
                {
                    aspPoints.Add(new ProgCalAspectPoint(point, aspect, longitude + aspect.GetDetails().Angle));
                    aspPoints.Add(new ProgCalAspectPoint(point, aspect, longitude - aspect.GetDetails().Angle));   
                }
            }
        }
        
        foreach (var ap in aspPoints)     // handle out of range longitudes
        {
            var longitude = ap.Longitude;
            if (longitude < 0.0) longitude += 360.0;
            if(longitude > 360.0) longitude -= 360.0;     
            aspPointsSorted.Add(new ProgCalAspectPoint(ap.ChartPoint, ap.Aspect, longitude));
        }
        
        // Sort by longitude in ascending order
        aspPointsSorted = aspPointsSorted.OrderBy(ap => ap.Longitude).ToList();
        
        return aspPointsSorted;
    }


    
    private static double FindLongitude(ChartPoints point, CalculatedChart chart)
    {
        foreach (var pos in chart.Positions)
        {
            if (pos.Key == point)
            {
                return pos.Value.Ecliptical.MainPosSpeed.Position;   // longitude found
            }            
        }
        return -1.0;   // not found
    }
    
}