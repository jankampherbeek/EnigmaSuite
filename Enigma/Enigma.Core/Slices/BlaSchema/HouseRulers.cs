// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Rulers for the houses
/// </summary>
public static class HouseRulers
{
    /// <summary>
    /// Calculate all rulers for each cusp
    /// </summary>
    /// <param name="chart">The calculated chart</param>
    /// <returns>Dictionary with the cusps and a list of RulerPairs.
    /// The list contains one pair, or more if intercepted signs are involved</returns>
    public static Dictionary<int, List<RulerPair>> DefineHouseRulers(ChartLongitudes chart)
    {
        var houseRulers = new Dictionary<int, List<RulerPair>>();
        var cusps = DefineCusps(chart);
        var signRulers = BlaDomain.RulerPairs();
        
        // Process each house (1-12)
        for (var houseIndex = 1; houseIndex <= 12; houseIndex++)
        {
            var rulers = new List<RulerPair>();
            
            // Get the sign on the current house cusp
            if (cusps.TryGetValue(houseIndex, out int cuspSign))
            {
                foreach (var sr in signRulers)
                {
                    if (sr.SignIndex == cuspSign) rulers.Add(sr);
                }
                // Add rulers for the sign on the cusp
                // if (signRulers.TryGetValue(cuspSign, out var cuspRulers))
                // {
                //     rulers.Add(cuspRulers);
                // }
            }
            
            var interceptedSignsInHouse = FindInterceptedSignsInHouse(houseIndex, cusps);
            
            // Add rulers for each intercepted sign
            foreach (var interceptedSign in interceptedSignsInHouse)
            {
                foreach (var sr in signRulers)
                {
                    
                }
                // if (signRulers.TryGetValue(interceptedSign, out var interceptedRulers))
                // {
                //     rulers.Add(interceptedRulers);
                // }
            }
            
            houseRulers.Add(houseIndex, rulers);
        }
        
        return houseRulers;
    }
    
    /// <summary>
    /// Define cusps and their corresponding signs
    /// </summary>
    /// <param name="chart">The calculated chart</param>
    /// <returns>Dictionary with cusp index and sign index (both 1-12)</returns>
    private static Dictionary<int, int> DefineCusps(ChartLongitudes chart)
    {
        var cusps = new Dictionary<int, int>();
        foreach (var pos in chart.Cusps)
        {
            var cuspIndex = pos.Key;
            var longitude = pos.Value;
            var signIndex = (int)(longitude / 30.0) + 1;
            cusps.Add(cuspIndex, signIndex);
        }
        return cusps;
    }
    
    /// <summary>
    /// Find intercepted signs within a specific house
    /// </summary>
    /// <param name="houseIndex">The house index (1-12)</param>
    /// <param name="cusps">Dictionary of cusps and their signs</param>
    /// <returns>List of intercepted sign indices within the house</returns>
    private static List<int> FindInterceptedSignsInHouse(int houseIndex, Dictionary<int, int> cusps)
    {
        var interceptedSigns = new List<int>();
        
        // Get the current house cusp sign
        if (!cusps.TryGetValue(houseIndex, out int currentSign)) return interceptedSigns;
        
        // Get the next house cusp sign
        var nextHouseIndex = houseIndex == 12 ? 1 : houseIndex + 1;
        if (!cusps.TryGetValue(nextHouseIndex, out int nextSign)) return interceptedSigns;
        
        // If the same sign is on both cusps, no signs are intercepted
        if (currentSign == nextSign) return interceptedSigns;
        
        // Find all signs that should be between currentSign and nextSign (exclusive)
        var expectedSigns = new List<int>();
        
        // Handle the case where we need to wrap around from 12 to 1
        if (currentSign > nextSign)
        {
            // Current sign is after next sign (e.g., Aquarius to Aries)
            // Add all signs from currentSign+1 to 12, then from 1 to nextSign-1
            for (int i = currentSign + 1; i <= 12; i++)
            {
                expectedSigns.Add(i);
            }
            for (int i = 1; i < nextSign; i++)
            {
                expectedSigns.Add(i);
            }
        }
        else
        {
            // Normal case: current sign is before next sign
            // Add all signs between currentSign and nextSign (exclusive)
            for (int i = currentSign + 1; i < nextSign; i++)
            {
                expectedSigns.Add(i);
            }
        }
        
        // The signs in expectedSigns are intercepted
        interceptedSigns.AddRange(expectedSigns);
        
        return interceptedSigns;
    }
    

}