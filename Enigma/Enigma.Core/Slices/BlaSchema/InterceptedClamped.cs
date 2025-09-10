// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;


/// <summary>
/// Intercepted signs and clamped houses
/// </summary>
public static class InterceptedClamped
{

    public static List<int> DefineInterceptedSigns(ChartLongitudes chart)
    {
        var intercepted = new List<int>();
        var cusps = DefineCusps(chart);
        
        // Check each sign (1-12) to see if it appears on any cusp
        for (var signIndex = 1; signIndex <= 12; signIndex++)
        {
            var signOnCusp = cusps.ContainsValue(signIndex);
            if (!signOnCusp)
            {
                intercepted.Add(signIndex);
            }
        }
        return intercepted;
    }

    public static List<int> DefineClampedHouses(ChartLongitudes chart)
    {
        var clampedHouses = new List<int>();
        var cusps = DefineCusps(chart);
        
        // Check each house (1-12) to see if it is clamped
        for (var houseIndex = 1; houseIndex <= 12; houseIndex++)
        {
            if (!cusps.TryGetValue(houseIndex, out int currentSign)) continue;
            // Get the sign on the next house cusp (handle overflow from 12 to 1)
            var nextHouseIndex = houseIndex == 12 ? 1 : houseIndex + 1;

            if (!cusps.TryGetValue(nextHouseIndex, out int nextSign)) continue;
            // If the sign on the current cusp is the same as the sign on the next cusp,
            // then this house is clamped (doesn't contain the start of a sign)
            if (currentSign == nextSign)
            {
                clampedHouses.Add(houseIndex);
            }
        }
        
        return clampedHouses;
    }
    
    

    private static Dictionary<int, int> DefineCusps(ChartLongitudes chart)
    {
        var cusps = new Dictionary<int, int>();    // index of cusp, index of sign, both 1..12
        foreach (var pos in chart.Cusps)
        {
            var cuspIndex = pos.Key;
            var longitude = pos.Value;
            var signIndex = (int)(longitude / 30.0) + 1;
            cusps.Add(cuspIndex, signIndex);
        }
        return cusps;
    }
    
}