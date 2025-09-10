// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Points in houses that are ruled by a specific sign
/// </summary>
public class HouseCusps
{
    /// <summary>
    /// Define HouseCusps: the counts in houses with a specific sign on the cusp (not intercepted signs)
    /// </summary>
    /// <param name="houseCounts">The number of points in each house</param>
    /// <param name="signsOnCusps">The signs that are on the cusp</param>
    /// <returns>Dictionary with the index of the cusp and the number of points</returns>
    public static Dictionary<int, int> DefineHouseCusps(Dictionary<int, int> houseCounts, Dictionary<int, int> signsOnCusps)
    {
        var hCusps = new Dictionary<int, int>()
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
        foreach (var (house, sign) in signsOnCusps)
        {
            hCusps[sign]+= houseCounts[house];
        } 
        return hCusps;
    }
}