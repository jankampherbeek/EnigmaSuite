// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Schema for dispositors
/// </summary>
public static class Dispositors
{
    /// <summary>
    /// Create dispositors
    /// </summary>
    /// <param name="chart">The positions in the chart</param>
    /// <returns>The lines with dispositors</returns>
    public static List<BlaDispositorLine> CreateDispositors(ChartLongitudes chart, 
        Dictionary<int,int> SignCounts, 
        Dictionary<int,int> HouseCounts,
        Dictionary<int,int> SignsOnHouseCusps,
        Dictionary<ChartPoints,int> PlanetsInSign,
        Dictionary<ChartPoints,int> PlanetsInHouses,
        Dictionary<ChartPoints,int> PlanetsInDecanates,
        bool useDecanates)
    {  
        
        // preparation
        
        var dispositors = new List<BlaDispositorLine>();
        var decanateCounts = new Dictionary<int, int>
        {
            {1,0},
            {2,0},
            {3,0},
            {4,0},
            {5,0},
            {6,0},
            {7,0},
        };
        foreach (var planetDec in PlanetsInDecanates)
        {
            var decanate = planetDec.Value;
            decanateCounts[decanate]++;       
        }

        var decansWithFactors = new Dictionary<int, List<ChartPoints>>()
        {
            { 1, [] },
            { 2, [] },
            { 3, [] },
            { 4, [] },
            { 5, [] },
            { 6, [] },
            { 7, [] }
        };

        foreach (var factor in PlanetsInDecanates)
        {
            decansWithFactors[factor.Value].Add(factor.Key);      
        }
        
        foreach (var rulerPair in RulerPairsForDispositors())
        {
            var mainRuler = rulerPair.MainRuler;
            var subRuler = rulerPair.SubRuler;
            
            // rulers in signs
            
            var (signMainRuler, signSubRuler) = FindSignsRuledBy(mainRuler);;
            var signMainRulerCount = SignCounts[signMainRuler];
            var signSubRulerCount = SignCounts[signSubRuler];
            // define the sum of the results above  --> directSignCount
            var directSignCount = signMainRulerCount + signSubRulerCount;
            
            // for mainRuler and subRuler, define points in the signs they rule
            //      for each point, if it is a ruler and not the same as the mainRuler or subRuler
            //          define the sign these points rule and count the number of points in these signs --> indirectSignCount
            var indirectSignCount = 0;
            foreach (var point in PlanetsInSign)
            {
                if (point.Key == mainRuler || point.Key == subRuler || !IsRuler(point.Key)) continue;
                if (point.Value == signMainRuler || point.Value == signSubRuler)
                {
                    var signs = FindSignsRuledBy(point.Key);
                    indirectSignCount += SignCounts[signs.Item1];
                    indirectSignCount += SignCounts[signs.Item2];
                }
            }
            var totalSignCount = directSignCount + indirectSignCount;
            
            // rulers in decanates

            var directDecanateCount = 0;
  //          var indirectDecanateCount = 0;
            
            if (useDecanates)
            {
                foreach (var decanateRuler in BlaDomain.DecanateRulers())
                {
                    var decanate = decanateRuler.Decan;
                    if (decanateRuler.Ruler == mainRuler || decanateRuler.Ruler == subRuler)
                    {
                        foreach (var df in decansWithFactors)
                        {
                            if (df.Key != decanate) continue;
                            directDecanateCount = df.Value.Count;
                        }
                    }
                }
            }
            
            
            // rulers in houses
            
            // find number of points in houses ruled by mainRuler and subRuler  --> directHouseCount
            var directHouseCount = 0;
            var housesRuledByMainRuler = FindCuspsRuledBy(mainRuler, SignsOnHouseCusps);
            var housesRuledBySubRuler = FindCuspsRuledBy(subRuler, SignsOnHouseCusps);
            
            // only count for mainRuler to prevent double counting
            foreach (var house in housesRuledByMainRuler)
            {
                var nrOfPoints = HouseCounts[house];
                directHouseCount += nrOfPoints;   
            }

            
            
            // for mainRuler and subRuler, define points in the houses they rule
            //      for each point, if it is a ruler and not the same as the mainRuler or subRuler
            //          define the house these points rule and count the number of points in these houses --> indirectHouseCount
            var indirectHouseCount = 0;
    
            var pointsInHousesForIndirectRulership = new List<ChartPoints>();
            foreach (var house in housesRuledByMainRuler)
            {
                foreach (var point in PlanetsInHouses)
                {
                    if (point.Key == mainRuler || point.Key == subRuler || !IsRuler(point.Key)) continue;
                    if (point.Value == house)
                    {
                        pointsInHousesForIndirectRulership.Add(point.Key);
                    }
                }
            }
            // remove points from same rulerPair
            var uniquePoints = new List<ChartPoints>();
            foreach (var pair in BlaDomain.RulerPairs())
            {
                var mRuler = pair.MainRuler;
                var sRuler = pair.SubRuler;
                var handledPairs = new List<RulerPair>();
                foreach (var point in pointsInHousesForIndirectRulership)
                {
                    if (point != mRuler && point != sRuler) continue;
                    if (handledPairs.Contains(pair)) continue;      // do not use both points from same ruler pair
                    handledPairs.Add(pair);
                    if (uniquePoints.Contains(point)) continue;
                    uniquePoints.Add(point);
                }
            }
            
            foreach (var point in uniquePoints)
            {
                var house = FindCuspsRuledBy(point, SignsOnHouseCusps);
                foreach (var h in house)
                {
                    indirectHouseCount += HouseCounts[h];   
                }
            }
            var totalHouseCount = directHouseCount + indirectHouseCount;
            var totalCount = totalSignCount + totalHouseCount + directDecanateCount;
             
            dispositors.Add(new BlaDispositorLine(mainRuler, subRuler, signMainRulerCount, signSubRulerCount, 
                directSignCount, indirectSignCount, totalSignCount, directHouseCount, indirectHouseCount, totalHouseCount, 
                directDecanateCount, totalCount));
        }
        return dispositors;
        
    }

    private static bool IsRuler(ChartPoints chartPoint)
    {
        return chartPoint is ChartPoints.Sun or ChartPoints.Moon or ChartPoints.Mercury or ChartPoints.Venus 
            or ChartPoints.Mars or ChartPoints.Jupiter or ChartPoints.Neptune or ChartPoints.Pluto 
            or ChartPoints.ApogeeMean or ChartPoints.Priapus or ChartPoints.PersephoneCarteret or ChartPoints.VulcanusCarteret;
        
    }
    

    private static List<RulerPair> RulerPairsForDispositors()
    {
        var rulerPairs = new List<RulerPair>
        {
            new(1,ChartPoints.Mars, ChartPoints.Pluto),
            new(2,ChartPoints.Venus, ChartPoints.PersephoneCarteret),
            new(3,ChartPoints.Mercury, ChartPoints.VulcanusCarteret),
            new(4,ChartPoints.Moon, ChartPoints.Priapus),
            new(5, ChartPoints.Sun, ChartPoints.ApogeeMean),
            new(9, ChartPoints.Jupiter, ChartPoints.Neptune)
        };
        return rulerPairs;
    }

    private static List<RulerPair> RulerPairsForHouses(Dictionary<int, int> SignsOnHouseCusps)
    {
        var rulerPairsForHouses = new List<RulerPair>();
        for (var i = 1; i <= 12; i++)
        {
            foreach (var soCup in SignsOnHouseCusps)
            {
                var sign = soCup.Key;
                var house = soCup.Value;
                foreach (var rulerPair in BlaDomain.RulerPairs())
                {
                    if (rulerPair.SignIndex == sign)
                    {
                        rulerPairsForHouses.Add(new RulerPair(sign, rulerPair.MainRuler, rulerPair.SubRuler ));
                    }
                }
            }
        }
        return rulerPairsForHouses;
    }

    // Find the cusps ruled by a ChartPoint, either as main ruler or as sub ruler
    private static List<int> FindCuspsRuledBy(ChartPoints ruler, Dictionary<int, int> signsOnCusps)
    {
        var (signMainRuler, signSubRuler) = FindSignsRuledBy(ruler);
        var cusps = new List<int>();
        foreach (var (house, sign) in signsOnCusps)
        {
            if (sign == signMainRuler || sign == signSubRuler)
            {
                cusps.Add(house);
            }
        }
        return cusps;
    }
    
    
    private static (int, int) FindSignsRuledBy(ChartPoints ruler)
    {
        var signMain = 0;
        var signSub = 0;
        foreach (var rulers in BlaDomain.RulerPairs())
        {
            if (ruler == rulers.MainRuler) signMain = rulers.SignIndex;
            if (ruler == rulers.SubRuler) signSub = rulers.SignIndex;
        }
        return (signMain, signSub);
    }
    
}