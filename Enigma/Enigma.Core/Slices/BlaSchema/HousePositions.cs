// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Position in houses
/// </summary>
public class HousePositions
{

    /// <summary>
    /// Find the house position for a point withna specific longitude
    /// </summary>
    /// <param name="chart"></param>
    /// <param name="longitude"></param>
    /// <returns></returns>
    public int FindSingleHousePosition(CalculatedChart chart, double longitude)
    {
        var houseLongitudes = new Dictionary<int, double>();

        foreach (var pos in chart.Positions)
        {
            if (pos.Key.GetDetails().PointCat == PointCats.Cusp)
            {
                var index = pos.Key.GetDetails().CalcId;
                var houseLongitude = pos.Value.Ecliptical.MainPosSpeed.Position;
                houseLongitudes.Add(index, houseLongitude);
            }                
        }
        var house = FindHouseForLongitude(longitude, houseLongitudes);
        return house;
    }
    
    
    
    
    /// <summary>
    /// Define the position in a house
    /// </summary>
    /// <param name="chart">The calculated chart</param>
    /// <returns>Dictionary with chart points and the index of the house: 1 .. 12. Zero if no house was found.</returns>
    public Dictionary<ChartPoints, int> DefineHousePositions(CalculatedChart chart)
    {
        var houseLongitudes = new Dictionary<int, double>();
        var housePositions = new Dictionary<ChartPoints, int>();
        
        // define longitude for house cusps
        foreach (var pos in chart.Positions)
        {
            if (pos.Key.GetDetails().PointCat == PointCats.Cusp)
            {
                var index = pos.Key.GetDetails().CalcId;
                var longitude = pos.Value.Ecliptical.MainPosSpeed.Position;
                houseLongitudes.Add(index, longitude);
            }                
        }
        
        // find house positions for each ChartPoint that is not a cusp
        foreach (var pos in chart.Positions)
        {
            if (pos.Key.GetDetails().PointCat != PointCats.Cusp)
            {
                var longitude = pos.Value.Ecliptical.MainPosSpeed.Position;
                var houseNumber = FindHouseForLongitude(longitude, houseLongitudes);
                if (houseNumber > 0)
                {
                    housePositions.Add(pos.Key, houseNumber);
                }
            }           
        }

        return housePositions;
    }
    
    /// <summary>
    /// Find which house a longitude belongs to 
    /// </summary>
    /// <param name="longitude">The longitude to check</param>
    /// <param name="houseLongitudes">Dictionary of house cusp longitudes</param>
    /// <returns>House number (1-12) or 0 if not found</returns>
    private static int FindHouseForLongitude(double longitude, Dictionary<int, double> houseLongitudes)
    {
        var nrOfHouses = houseLongitudes.Count;
        if (nrOfHouses == 0) return 0;
        
        // Sort cusps by house number to ensure proper order
        var sortedCusps = houseLongitudes.OrderBy(x => x.Key).ToList();
        
        for (var i = 0; i < nrOfHouses; i++)
        {
            var currentCusp = sortedCusps[i].Value;
            var nextCusp = (i == nrOfHouses - 1) ? sortedCusps[0].Value : sortedCusps[i + 1].Value;
            var houseNumber = sortedCusps[i].Key;
            
            // Handle the case where the current cusp is greater than the next cusp (overflow across 0°)
            if (currentCusp > nextCusp)
            {
                // Longitude is in this house if it's greater than current cusp OR less than next cusp
                if ((longitude >= currentCusp && longitude <= 360.0) || (longitude >= 0.0 && longitude < nextCusp))
                {
                    return houseNumber;
                }
            }
            else
            {
                // Normal case: longitude is in this house if it's between current and next cusp
                if (longitude >= currentCusp && longitude < nextCusp)
                {
                    return houseNumber;
                }
            }
        }
        return 0; // Not found in any house
    }
}