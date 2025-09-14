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
        var cardinal = FindCycles([1, 4, 7, 10], rulerHouseRuledHouse);
        var fix = FindCycles([2, 5, 8, 11], rulerHouseRuledHouse);  
        var mutable = FindCycles([3, 6, 9, 12], rulerHouseRuledHouse);
        var fire = FindCycles([1, 5, 9], rulerHouseRuledHouse);
        var earth = FindCycles([2, 6, 10], rulerHouseRuledHouse);
        var air = FindCycles([3, 7, 11], rulerHouseRuledHouse);
        var water = FindCycles([4, 8, 12], rulerHouseRuledHouse);
        
        return new BlaCyclesData(cardinal, fix, mutable, fire, earth, air, water);
    }

    // Return a dictionary with the index of the cusp and a list of rulers
    private static Dictionary<int, List<ChartPoints>> CreateRulersForHouses(Dictionary<int, int> signsOnCusps)
    {
        var rulersForHouses = new Dictionary<int, List<ChartPoints>>();
        foreach (var cusps in signsOnCusps)
        {
            var rulerPair = BlaDomain.RulerPairs()[cusps.Value];
            rulersForHouses.Add(cusps.Key, [rulerPair.MainRuler, rulerPair.SubRuler]);
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
    
    // Find cycles in a specific group of houses
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