// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Count points in houses
/// </summary>
public static class HouseCounts
{

    public static Dictionary<int, int> CountPosInHouses(List<BlaPositions> positions)
    {
        ArgumentNullException.ThrowIfNull(positions);
        
        var result = new Dictionary<int, int>()
        {
            {1, 0},
            {2, 0},
            {3, 0},
            {4, 0},
            {5, 0},
            {6, 0},
            {7, 0},
            {8, 0},
            {9, 0},
            {10, 0},
            {11, 0},
            {12, 0}
        };
        foreach (var position in positions)
        {
            result[position.House]++;
        }

        return result;
    }
    
}