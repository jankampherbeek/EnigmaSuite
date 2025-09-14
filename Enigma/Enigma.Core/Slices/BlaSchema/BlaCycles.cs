// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Cycles for BLA schema calculations
/// </summary>
public static class BlaCycles
{

    /// <summary>
    /// Create cycles data
    /// </summary>
    /// <param name="planetsInHouses">Planets in houses</param>
    /// <param name="signsOnCusps">Signs on cusps</param>
    /// <returns>Cycles data</returns>
    public static BlaCyclesData CreateCyclesData(Dictionary<ChartPoints, int> planetsInHouses, Dictionary<int, int> signsOnCusps)
    {
        var rulersForHouses = CreateRulersForHouses(signsOnCusps);
        var rulerHouseRuledHouse = RuledHouseRulerInHouse(rulersForHouses, planetsInHouses);
        var cardinalHouses = new List<int>(){1, 4, 7, 10};
        var cardinal = FindCycles(cardinalHouses, rulerHouseRuledHouse);
        var fixHouses = new List<int>(){2, 5, 8, 11};
        var fix = FindCycles(fixHouses, rulerHouseRuledHouse);  
        var mutableHouses = new List<int>(){3, 6, 9, 12};
        var mutable = FindCycles(mutableHouses, rulerHouseRuledHouse);
        var fireHouses = new List<int>(){1, 5, 9};
        var fire = FindCycles(fireHouses, rulerHouseRuledHouse);
        var earthHouses = new List<int>(){2, 6, 10};
        var earth = FindCycles(earthHouses, rulerHouseRuledHouse);
        var airHouses = new List<int>(){3, 7, 11};
        var air = FindCycles(airHouses, rulerHouseRuledHouse);
        var waterHouses = new List<int>(){4, 8, 12};
        var water = FindCycles(waterHouses, rulerHouseRuledHouse);
        
        return new BlaCyclesData(cardinal, fix, mutable, fire, earth, air, water);
    }

    // Return a dictionary with the index of the cusp and a list of rulers
    private static Dictionary<int, List<ChartPoints>> CreateRulersForHouses(Dictionary<int, int> signsOnCusps)
    {
        var rulersForHouses = new Dictionary<int, List<ChartPoints>>();
        foreach (var cusps in signsOnCusps)
        {
            var rulerPair = BlaDomain.RulerPairs()[cusps.Value];
            rulersForHouses.Add(cusps.Key, new List<ChartPoints>() { rulerPair.MainRuler, rulerPair.SubRuler });
        }
        return rulersForHouses;
    }

    // Return a dictionary with chartpoints, the house it rules and the house where it is located
    private static Dictionary<ChartPoints, (int, int)> RuledHouseRulerInHouse(
        Dictionary<int, List<ChartPoints>> rulersForHouses,
        Dictionary<ChartPoints, int> planetsInHouses)
    {
        var rulerHouseRuledHouse = new Dictionary<ChartPoints, (int, int)>();
        foreach (var ruler in rulersForHouses)
        {
            foreach (var house in ruler.Value)
            {
                rulerHouseRuledHouse.Add(house, (ruler.Key, planetsInHouses[house]));
            }
        }
        return rulerHouseRuledHouse;
    }
    
    
    private static List<(int, int)> FindCycles(List<int> houses, Dictionary<ChartPoints, (int, int)> ruledHousesRulerInHouse)
    {
        var cycles = new List<(int, int)>();
        foreach (var ruler in ruledHousesRulerInHouse)
        {
            if (houses.Contains(ruler.Value.Item1) && houses.Contains(ruler.Value.Item2))
            {
                cycles.Add(ruler.Value);
            }
        }
        return cycles;
    }
    
}