// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;
using System.Linq;

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
    public static BlaCyclesData CreateCyclesData(Dictionary<ChartPoints, int> planetsInHouses,
        Dictionary<int, int> signsOnCusps)
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

    /// <summary>
    /// Create shortened cycles data 
    /// </summary>
    /// <param name="planetsInHouses">Planets in houses</param>
    /// <param name="signsOnCusps">Signs on cusps</param>
    /// <returns>Shortened cycles data</returns>
    public static BlaCyclesData CreateShortenedCyclesData(Dictionary<ChartPoints, int> planetsInHouses,
        Dictionary<int, int> signsOnCusps)
    {
        var rulersForHouses = CreateRulersForHouses(signsOnCusps);
        var rulerHouseRuledHouse = RuledHouseRulerInHouse(rulersForHouses, planetsInHouses);
        var cardinal = FindShortenedCycles([1, 4, 7, 10], rulersForHouses);
        var fix = FindShortenedCycles([2, 5, 8, 11], rulersForHouses);
        var mutable = FindShortenedCycles([3, 6, 9, 12], rulersForHouses);
        var fire = FindShortenedCycles([1, 5, 9], rulersForHouses);
        var earth = FindShortenedCycles([2, 6, 10], rulersForHouses);
        var air = FindShortenedCycles([3, 7, 11], rulersForHouses);
        var water = FindShortenedCycles([4, 8, 12], rulersForHouses);
        return new BlaCyclesData(cardinal, fix, mutable, fire, earth, air, water);
    }


    // Return a dictionary with the index of the cusp and a list of rulers
    private static Dictionary<int, List<ChartPoints>> CreateRulersForHouses(Dictionary<int, int> signsOnCusps)
    {
        var rulersForHouses = new Dictionary<int, List<ChartPoints>>();
        foreach (var cusps in signsOnCusps)
        {
            var rulerPair = BlaDomain.RulerPairs()[cusps.Value - 1];
            rulersForHouses.Add(cusps.Key, [rulerPair.MainRuler, rulerPair.SubRuler]);
        }

        return rulersForHouses;
    }

    // Return a list with chartpoints, the house it rules and the house where it is located
    private static List<(ChartPoints, int, int)> RuledHouseRulerInHouse(
        Dictionary<int, List<ChartPoints>> rulersForHouses,
        Dictionary<ChartPoints, int> planetsInHouses)
    {
        var rulerHouseRuledHouse = new List<(ChartPoints, int, int)>();
        foreach (var ruler in rulersForHouses)
        {
            foreach (var house in ruler.Value)
            {
                rulerHouseRuledHouse.Add((house, ruler.Key, planetsInHouses[house]));
            }
        }

        return rulerHouseRuledHouse;
    }

    // Find cycles in a specific group of houses
    private static List<(int, int)> FindCycles(List<int> houses, List<(ChartPoints, int, int)> ruledHousesRulerInHouse)
    {
        var cycles = new List<(int, int)>();
        foreach (var ruler in ruledHousesRulerInHouse)
        {
            if (houses.Contains(ruler.Item2) && houses.Contains(ruler.Item3))
            {
                cycles.Add((ruler.Item2, ruler.Item3));
            }
        }

        return cycles;
    }

    // Find shortened cycles in a specific group of houses
    // A shortened cycle exists if houses from the same element or cross are ruled by points from the same ruler pair
    private static List<(int, int)> FindShortenedCycles(List<int> houses,
        Dictionary<int, List<ChartPoints>> rulersForHouses)
    {
        var cycles = new List<(int, int)>();

        var rulers = new List<(ChartPoints, int)>(); // ruler and house
        foreach (var house in houses) // houses belong to one element or one cross
        {
            var ruler = rulersForHouses[house];
            rulers.Add((ruler[0], house));
            rulers.Add((ruler[1], house));
        }

        for (var i = 0; i < rulers.Count; i++)
        {
            for (var j = i + 1; j < rulers.Count; j++)
            {
                foreach (var rulerPair in BlaDomain.RulerPairs())
                {
                    if ((rulerPair.MainRuler != rulers[i].Item1 || rulerPair.SubRuler != rulers[j].Item1) &&
                        (rulerPair.MainRuler != rulers[j].Item1 || rulerPair.SubRuler != rulers[i].Item1)) continue;
                    if (rulers[i].Item2 == rulers[j].Item2) continue;
                    if (!houses.Contains(rulers[i].Item2) || !houses.Contains(rulers[j].Item2)) continue;
                    if (!cycles.Contains((rulers[i].Item2, rulers[j].Item2)))
                        cycles.Add((rulers[i].Item2, rulers[j].Item2));
                }
            }
        }

        return cycles;
    }
}