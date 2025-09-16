// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Calculations to support the construction of reinforcements for the BLA schema
/// </summary>
public static class ReinforcementCalc
{
    /// <summary>
    /// Finds points that are in the sign they rule
    /// </summary>
    /// <param name="planetsInSigns">All points and the signs where they are located</param>
    /// <returns>A dictionary with ChartPoints that are in their own sign and indexes for the signs (1..12)</returns>
    public static Dictionary<ChartPoints, int> FindPointsInOwnSign(Dictionary<ChartPoints, int> planetsInSigns)
    {
        var pointsInOwnSign = new Dictionary<ChartPoints, int>();
        foreach (var (signMain, mainRuler, subRuler) in BlaDomain.RulerPairs())
        {
            foreach (var planet in planetsInSigns)
            {
                if ((planet.Key == mainRuler || planet.Key == subRuler) && (planet.Value == signMain || planet.Value == signMain))
                {
                    pointsInOwnSign.Add(planet.Key, planet.Value);
                }
            }
        }
        return pointsInOwnSign;
    }
    
}