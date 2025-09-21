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
                if ((planet.Key == mainRuler || planet.Key == subRuler) &&
                    (planet.Value == signMain || planet.Value == signMain))
                {
                    pointsInOwnSign.Add(planet.Key, planet.Value);
                }
            }
        }

        return pointsInOwnSign;
    }

    /// <summary>
    /// Finds points that are in the house they rule
    /// </summary>
    /// <param name="planetsInHouses">All points and the houses where they are located</param>
    /// <param name="signsOnCusps">All signs and the cusps where they are located</param>
    /// <returns>A dictionary with ChartPoints that are in their own house and indexes for the houses (1..12)</returns>
    public static Dictionary<ChartPoints, int> FindPointsInOwnHouses(Dictionary<ChartPoints, int> planetsInHouses,
        Dictionary<int, int> signsOnCusps)
    {
        var pointsInOwnHouse = new Dictionary<ChartPoints, int>();

        foreach (var planetInHouse in planetsInHouses)
        {
            ChartPoints? mainRuler = null;
            ChartPoints? subRuler = null;
            foreach (var rulerPair in BlaDomain.RulerPairs())
            {
                if (rulerPair.SignIndex != signsOnCusps[planetInHouse.Value]) continue;
                mainRuler = rulerPair.MainRuler;
                subRuler = rulerPair.SubRuler;
            }

            if (mainRuler == null || subRuler == null) continue;
            if (planetInHouse.Key == mainRuler || planetInHouse.Key == subRuler)
            {
                pointsInOwnHouse.Add(planetInHouse.Key, planetInHouse.Value);
            }
        }
        return pointsInOwnHouse;
    }

    /// <summary>
    /// Find points that are in the mundane houses they rule 
    /// </summary>
    /// <param name="planetsInHouses">All planets and the houses where they are located</param>
    /// <returns>Dictionary with ChartPoints that are in the house they rule mandanely, and the house index (1..12)</returns>
    public static Dictionary<ChartPoints, int> FindPointsInMundaneHouses(Dictionary<ChartPoints, int> planetsInHouses)
    {
        var pointsInMundaneHouse = new Dictionary<ChartPoints, int>();

        foreach (var planetInHouse in planetsInHouses)
        {
            var house = planetInHouse.Value;
            var point = planetInHouse.Key;
            var houseRulers = BlaDomain.RulerPairs();
            if (houseRulers.Any(r => (r.MainRuler == point || r.SubRuler == point) && r.SignIndex == house))
            {
                pointsInMundaneHouse.Add(planetInHouse.Key, planetInHouse.Value);
            }
        }
        return pointsInMundaneHouse;
        
    }

    /// <summary>
    /// Find rulers that are in a sign with the same index as the house they rule
    /// </summary>
    /// <param name="signsOnCusps">All cusps and the signs on each cusp, both indexed 1..12</param>
    /// <param name="planetsInSigns">All planets and the signs (1..12) where they are located</param>   
    /// <returns>Dictionary with ChartPoints and the sign index (1..12)</returns>
    public static Dictionary<ChartPoints, int> FindRulerInHouseAsSign(Dictionary<int, int> signsOnCusps, Dictionary<ChartPoints, int> planetsInSigns)
    {
        var signHouseRulers = new Dictionary<ChartPoints, int>();
        // Voor alle cusps
        // Bepaal teken dat op cusp heerst
        foreach (var (house, sign) in signsOnCusps)
        {
            ChartPoints? mainRulingPoint = null;
            ChartPoints? subRulingPoint = null;
            // Bepaal punt dat teken, en dus cusp beheert
            foreach (var rulerPair in BlaDomain.RulerPairs())
            {
                if (rulerPair.SignIndex != sign) continue;
                mainRulingPoint = rulerPair.MainRuler;
                subRulingPoint = rulerPair.SubRuler;
            }
            var signMainRuler = 0;
            var signSubRuler = 0;
            foreach (var planetInSign in planetsInSigns)
            {
                if (planetInSign.Key == mainRulingPoint) signMainRuler = planetInSign.Value;
                if (planetInSign.Key == subRulingPoint) signSubRuler = planetInSign.Value;
            }
            if (mainRulingPoint != null && signMainRuler == house)
            {
                signHouseRulers.Add((ChartPoints)mainRulingPoint, signMainRuler);
            }
            if (subRulingPoint != null && signSubRuler == house)
            {
                signHouseRulers.Add((ChartPoints)subRulingPoint, signSubRuler);
            }
        }        
        return signHouseRulers;
    }
    
}