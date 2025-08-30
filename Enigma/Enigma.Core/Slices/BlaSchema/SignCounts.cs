// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Count points in signs
/// </summary>
public static class SignCounts
{

    /// <summary>
    /// Calculate the number of points in each sign
    /// </summary>
    /// <param name="positions"></param>
    /// <returns></returns>
    public static Dictionary<int, int> CountPointsInSigns(List<BlaPositions> positions)
    {
        ArgumentNullException.ThrowIfNull(positions);

        var result = new Dictionary<int, int>
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
        
        foreach (var position in positions)
        {
            result[position.Sign]++;
        }
        return result;
    }
}