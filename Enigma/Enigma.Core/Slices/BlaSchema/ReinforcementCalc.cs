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
    /// Data for receptions in houses
    /// </summary>
    /// <param name="Point">The chart point</param>
    /// <param name="HousePos">House that contains the chart point</param>
    /// <param name="HouseRuled">House ruled by the chart point</param>
    private record HousesPosAndRuler(
        ChartPoints Point,
        int HousePos,
        List<int> HouseRuled);
    
    
    
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
    


    /// <summary>
    /// Find factorpairs with analogous house and sign
    /// </summary>
    /// <param name="planetsInSigns">Planets in signs</param>
    /// <param name="planetsInHouses">Planets in houses</param>
    /// <returns>List with records FactorPairAnalogHouseSign</returns>
    public static List<FactorPairAnalogHouseSign> FindFactorPairs(Dictionary<ChartPoints, int> planetsInSigns,
        Dictionary<ChartPoints, int> planetsInHouses)
    {
        var factorPairs = new List<FactorPairAnalogHouseSign>();

        foreach (var (point1, point2) in BlaDomain.FactorPairs())
        {
            if (planetsInSigns.ContainsKey(point1) && planetsInSigns.ContainsKey(point2)
                                                   && planetsInHouses.ContainsKey(point1)
                                                   && planetsInHouses.ContainsKey(point2))
            {
                var house1 = planetsInHouses[point1];
                var house2 = planetsInHouses[point2];
                var sign1 = planetsInSigns[point1];
                var sign2 = planetsInSigns[point2];
                
                if (sign1 == house2)
                {
                    factorPairs.Add(new FactorPairAnalogHouseSign(point1, sign1, point2, house2));
                }
                else if (sign2 == house1)
                {
                    factorPairs.Add(new FactorPairAnalogHouseSign(point2, sign2, point1, house1));
                }
            }
        }
        return factorPairs;
    }


    /// <summary>
    /// Find reception in signs
    /// </summary>
    /// <param name="planetsInSigns">PLanets in signs</param>
    /// <returns>The receptions</returns>
    public static List<Reception> FindReceptionInSigns(Dictionary<ChartPoints, int> planetsInSigns)
    {
        var receptions = new List<Reception>();

        foreach (var rulerPair1 in BlaDomain.RulerPairs())
        {
            var point1 = rulerPair1.MainRuler;          // use only main ruler, otherwise we get duplicates
            foreach (var rulerPair2 in BlaDomain.RulerPairs())
            {
                var point2 = rulerPair2.MainRuler;
                if (point1 == point2) continue;  // avoid identical points
                var sign1 = 0;
                var sign2 = 0;
                foreach (var planet in planetsInSigns)
                {
                    if (planet.Key == point1)
                    {
                        sign1 = planet.Value;
                    }
                    if (planet.Key == point2)
                    {
                        sign2 = planet.Value;
                    }
                }
                
                var ruledBy1 = new List<int>();
                var ruledBy2 = new List<int>();
                foreach (var rulerPair in BlaDomain.RulerPairs())
                {
                    if (rulerPair.MainRuler == point1 || rulerPair.SubRuler == point1)
                    {
                        ruledBy1.Add(rulerPair.SignIndex);
                    }
                    if (rulerPair.MainRuler == point2 || rulerPair.SubRuler == point2)
                    {
                        ruledBy2.Add(rulerPair.SignIndex);
                    }
                }
                if ((sign1 == ruledBy2[0] || sign1 == ruledBy2[1]) && (sign2 == ruledBy1[0] || sign2 == ruledBy1[1]))
                {
                    var reception = new Reception(point1, sign1, point2, sign2);
                    var reception2 = new Reception(point2, sign2, point1, sign1);
                    if (receptions.Contains(reception) || receptions.Contains(reception2)) continue;
                    var samePair = false;
                    foreach (var rulerPair in BlaDomain.RulerPairs())
                    {
                        if ((point1 == rulerPair.MainRuler && point2 == rulerPair.SubRuler) ||
                            (point1 == rulerPair.SubRuler && point2 == rulerPair.MainRuler))
                        {
                            samePair = true;
                        }
                    }
                    if (!samePair) receptions.Add(new Reception(point1, sign1, point2, sign2));
                }

            }
        }
        
        return receptions;
    }
    
    
    /// <summary>
    /// Find reception in houses
    /// </summary>
    /// <param name="planetsInHouses">Planets in houses</param>
    /// <param name="signsOnCusps">Signs on the cusps of houses</param>
    /// <returns>The receptions</returns>
    public static List<Reception> FindReceptionInHouses(Dictionary<ChartPoints, int> planetsInHouses, 
        Dictionary<int, int> signsOnCusps)
    {
        var housesPosAndRuler = new List<HousesPosAndRuler>();
        foreach (var rulerAndSigns in BlaDomain.AllRulerAndSigns())
        {
            var housePos = planetsInHouses[rulerAndSigns.Ruler];
            var housesRuled = new List<int>();
            foreach (var signOnCusp in signsOnCusps)
            {
               
                if (signOnCusp.Value == rulerAndSigns.MainSign || signOnCusp.Value == rulerAndSigns.SubSign)
                {
                    housesRuled.Add(signOnCusp.Key);
                }
            }
            if (housesRuled.Count == 0) continue;   // sign is intercepted: no house is ruled
            var hpr = new HousesPosAndRuler(rulerAndSigns.Ruler, housePos, housesRuled);
            if (!housesPosAndRuler.Contains(hpr)) housesPosAndRuler.Add(hpr);
        }
        
        var receptions = new List<Reception>();
        var count = housesPosAndRuler.Count;

        for (var i = 0; i < count; i++)
        {
            var hpr1 = housesPosAndRuler[i];
            for (var j = i + 1; j < count; j++)
            {
                var hpr2 = housesPosAndRuler[j];
                if (hpr2.HouseRuled.Contains(hpr1.HousePos) && hpr1.HouseRuled.Contains(hpr2.HousePos))
                {
                    var samePair = false;
                    foreach (var rulerPair in BlaDomain.RulerPairs())
                    {
                        if ((hpr1.Point == rulerPair.MainRuler && hpr2.Point == rulerPair.SubRuler) ||
                            (hpr1.Point == rulerPair.SubRuler && hpr2.Point == rulerPair.MainRuler))
                        {
                            samePair = true;
                        }
                    }
                    if (!samePair) receptions.Add(new Reception(hpr1.Point, hpr1.HousePos, hpr2.Point, hpr2.HousePos));
                }
            }            
        }
        
        return receptions;
    }

    public static List<Reception> FindReceptionInMundaneHouses(Dictionary<ChartPoints, int> planetsInHouses,
        Dictionary<int, int> signsOnCusps)
    {
        var housesPosAndRuler = new List<HousesPosAndRuler>();
        foreach (var rulerAndSigns in BlaDomain.AllRulerAndSigns())
        {
            var housePos = planetsInHouses[rulerAndSigns.Ruler];
            var mundaneHousesRuled = new List<int> {
                rulerAndSigns.MainSign,
                rulerAndSigns.SubSign,
            };
            
            
            
            
            housesPosAndRuler.Add(new HousesPosAndRuler(rulerAndSigns.Ruler, housePos, mundaneHousesRuled));
        }
        
        var receptions = new List<Reception>();
        var count = housesPosAndRuler.Count;

        for (var i = 0; i < count; i++)
        {
            var hpr1 = housesPosAndRuler[i];
            for (var j = i + 1; j < count; j++)
            {
                var hpr2 = housesPosAndRuler[j];
                if (hpr2.HouseRuled.Contains(hpr1.HousePos) && hpr1.HouseRuled.Contains(hpr2.HousePos))
                {
                    var samePair = false;
                    foreach (var rulerPair in BlaDomain.RulerPairs())
                    {
                        if ((hpr1.Point == rulerPair.MainRuler && hpr2.Point == rulerPair.SubRuler) ||
                            (hpr1.Point == rulerPair.SubRuler && hpr2.Point == rulerPair.MainRuler))
                        {
                            samePair = true;
                        }
                    }
                    if (!samePair) receptions.Add(new Reception(hpr1.Point, hpr1.HousePos, hpr2.Point, hpr2.HousePos));
                }
            }            
        }

        foreach (var reception in receptions)
        {
            Console.WriteLine(reception);
        }
        return receptions;
    }
    
    
}