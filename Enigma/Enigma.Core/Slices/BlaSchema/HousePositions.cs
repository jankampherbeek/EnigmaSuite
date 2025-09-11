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
public static class HousePositions
{

    /// <summary>
    /// Find the house position for a point with a specific longitude
    /// </summary>
    /// <param name="chart"></param>
    /// <param name="longitude"></param>
    /// <returns></returns>
    public static int FindSingleHousePosition(ChartLongitudes chart, double longitude)
    {
        var houseLongitudes = chart.Cusps;
        var house = FindHouseForLongitude(longitude, houseLongitudes);
        return house;
    }
    
    
    /// <summary>
    /// Define the position in a house
    /// </summary>
    /// <param name="chart">The calculated chart</param>
    /// <returns>Dictionary with chart points and the index of the house: 1 .. 12. Zero if no house was found.</returns>
    public static Dictionary<ChartPoints, int> DefineHousePositions(ChartLongitudes chart)
    {
        var houseLongitudes = chart.Cusps;
        var housePositions = new Dictionary<ChartPoints, int>();
        
        // find house positions for each ChartPoint that is not a cusp
        foreach (var pos in chart.Points)
        {
            if (pos.Key.GetDetails().PointCat == PointCats.Common)    // Exclude angles
            {
                var longitude = pos.Value;
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
    /// Count the points in the houses
    /// </summary>
    /// <param name="pointDetails">Details for the chartpoints, inlcuding the house</param>
    /// <returns>Dictionary with the index for the houses (1..12) and the count for each house</returns>
    public static Dictionary<int,int> DefineHouseCounts(List<BlaPointDetails> pointDetails)
    {
        var houseCounts = new Dictionary<int, int>()
        {
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 },
            { 5, 0 },
            { 6, 0 },
            { 7, 0 },
            { 8, 0 },
            { 9, 0 },
            { 10, 0 },
            { 11, 0 },
            { 12, 0 }
        };
            
        foreach (var details in pointDetails)
        {
            var house = details.House; 
            houseCounts[house]++;
        }
        return houseCounts;
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
        
        // Sort cusps
        var sortedCusps = houseLongitudes.OrderBy(x => x.Key).ToList();
        
        for (var i = 0; i < nrOfHouses; i++)
        {
            var currentCusp = sortedCusps[i].Value;
            var nextCusp = (i == nrOfHouses - 1) ? sortedCusps[0].Value : sortedCusps[i + 1].Value;
            var houseNumber = sortedCusps[i].Key;
            
           
            if (currentCusp > nextCusp)     // Handle the case where the cusps cross Zero Aries
            {
                if ((longitude >= currentCusp && longitude <= 360.0) || (longitude >= 0.0 && longitude < nextCusp))
                {
                    return houseNumber;
                }
            }
            else
            {
                if (longitude >= currentCusp && longitude < nextCusp)
                {
                    return houseNumber;
                }
            }
        }
        return 0; // Not found in any house
    }
}