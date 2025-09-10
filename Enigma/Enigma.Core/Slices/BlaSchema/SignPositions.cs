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
    /// <param name="chart">The calculated chart</param>
    /// <returns>Dictionary with the index for the signs (1..12) and the count for each sign</returns>
    public static Dictionary<int,int> DefineSignCounts(ChartLongitudes chart)
    {
        var signCounts = new Dictionary<int, int>();
        foreach (var (chartPoint, value) in chart.Points)
        {
            var longitude = value;
            var sign = (int)Math.Truncate(longitude / 30.0) + 1;
            signCounts[sign]++;
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