
// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Position in a quadrants
/// </summary>
public static class QuadrantPositions
{

    public static Dictionary<int, int> DefineQuadrants(ChartLongitudes chart)
    {
        if (chart == null)
            throw new ArgumentNullException(nameof(chart));
            
        var quadrantLongitudes = new Dictionary<int, double>();
        var quadrantPositions = new Dictionary<ChartPoints, int>();
        var quadrantCounts = new Dictionary<int, int>
        {
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 }
        };

        // Define longitude for quadrants
        foreach (var pos in chart.Points)
        {
            if (pos.Key.GetDetails().PointCat == PointCats.Angle)
            {
                var longitude = pos.Value;
                if (pos.Key == ChartPoints.Ascendant)
                {
                    quadrantLongitudes.Add(1, longitude); // Quadrant 1 starts at Ascendant
                    quadrantLongitudes.Add(3, RangeUtil.ValueToRange(longitude + 180.0, 0.0, 360.0)); // Quadrant 3 starts at Descendant
                }

                if (pos.Key == ChartPoints.Mc)
                {
                    quadrantLongitudes.Add(2, longitude); // Quadrant 2 starts at MC
                    quadrantLongitudes.Add(4, RangeUtil.ValueToRange(longitude + 180.0, 0.0, 360.0)); // Quadrant 4 starts at IC
                }
            }                
        }
        
        // Find quadrant positions for each ChartPoint that is not an angle
        foreach (var pos in chart.Points)
        {
            if (pos.Key.GetDetails().PointCat == PointCats.Common)
            {
                var longitude = pos.Value;
                var quadrantNumber = FindQuadrantForLongitude(longitude, quadrantLongitudes);
                if (quadrantNumber > 0)
                {
                    quadrantCounts[quadrantNumber]++;
                }
            }           
        }

        return quadrantCounts;
    }
    
    /// <summary>
    /// Find which quadrant a longitude belongs to 
    /// </summary>
    /// <param name="longitude">The longitude to check</param>
    /// <param name="quadrantLongitudes">Dictionary with quadrant numbers and their starting longitudes</param>
    /// <returns>Quadrant number (1-4) or 0 if not found</returns>
    private static int FindQuadrantForLongitude(double longitude, Dictionary<int, double> quadrantLongitudes)
    {
        if (quadrantLongitudes.Count < 4) return 0;
        
        // Sort quadrants by their starting longitude
        var sortedQuadrants = quadrantLongitudes.OrderBy(x => x.Value).ToList();
        

        
        // Check each quadrant
        for (int i = 0; i < sortedQuadrants.Count; i++)
        {
            var currentQuadrant = sortedQuadrants[i];
            var nextQuadrant = sortedQuadrants[(i + 1) % sortedQuadrants.Count];
            
            var currentLongitude = currentQuadrant.Value;
            var nextLongitude = nextQuadrant.Value;
            
            // Handle overflow across 0°
            if (currentLongitude > nextLongitude)
            {
                // Quadrant spans across 0° (e.g., 350° to 10°)
                if (longitude >= currentLongitude || longitude < nextLongitude)
                {
                    return currentQuadrant.Key;
                }
            }
            else
            {
                // Normal quadrant (e.g., 10° to 100°)
                if (longitude >= currentLongitude && longitude < nextLongitude)
                {
                    return currentQuadrant.Key;
                }
            }
        }
        
        return 0;
    }
}