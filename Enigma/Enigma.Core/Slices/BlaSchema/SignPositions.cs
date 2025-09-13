// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Positions in signs
/// </summary>
public static class SignPositions
{

    /// <summary>
    /// Count the points in the signs
    /// </summary>
    /// <param name="pointDetails">Details for each point, including the sign</param>
    /// <returns>Dictionary with the index for the signs (1..12) and the count for each sign</returns>
    public static Dictionary<int,int> DefineSignCounts(List<BlaPointDetails> pointDetails)
    {
        var signCounts = new Dictionary<int, int>()
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
        foreach (var detail in pointDetails)
        {
            signCounts[detail.Sign]++;
        }
        return signCounts;
    }
    
    public static Dictionary<ChartPoints, int> CreatePointsInSign(ChartLongitudes chart)
    { 
        Dictionary<ChartPoints, int> planetsInSign = new();
        foreach (var planet in chart.Points)
        {
            if (planet.Key.GetDetails().PointCat != PointCats.Common && planet.Key.GetDetails().PointCat != PointCats.Angle) continue;
            var sign = (int)Math.Truncate(planet.Value / 30.0) + 1;
            planetsInSign.Add(planet.Key, sign);
        } 
        return planetsInSign;
    }
    
}