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
        Dictionary<ChartPoints,int> PlanetsInHouses)
    {  //chart, signCounts, houseCounts, signsOnCusps, planetsInSign, planetsInHouses
        var dispositors = new List<BlaDispositorLine>();

        foreach (var rulerPair in BlaDomain.RulerPairs())
        {
            // find mainRuler and subRuler
            var mainRuler = rulerPair.MainRuler;
            var subRuler = rulerPair.SubRuler;
            var (signMainRuler, signSubRuler) = FindSignsRuledBy(mainRuler);;
            // define number of point in signs ruled by mainRuler and subRuler
            var signMainRulerCount = SignCounts[signMainRuler];
            var subRulerCount = SignCounts[signSubRuler];
            // define the sum of the results above  --> directSignCount
            var directSignCount = signMainRulerCount + subRulerCount;
            // for mainRuler and subRuler, define points in the signs they rule
            //      for each point, if it is a ruler and not the same as the mainRuler or subRuler
            //          define the sign these points rule and count the number of points in these signs --> indirectSignCount
            var indirectSignCount = 0;
            foreach (var point in PlanetsInSign)
            {
                if (point.Key == mainRuler || point.Key == subRuler || !IsRuler(point.Key)) continue;
                if (point.Value == signMainRuler || point.Value == signSubRuler)
                {
                    indirectSignCount += SignCounts[point.Value];
                }
            }
            // define the total of directSignCount and indirectSignCount --> totalSignCount
            var totalSignCount = directSignCount + indirectSignCount;
            
            // find number of points in houses ruled by mainRuler and subRuler  --> directHouseCount
            var directHouseCount = 0;

            
            
            // for mainRuler and subRuler, define points in the houses they rule
            //      for each point, if it is a ruler and not the same as the mainRuler or subRuler
            //          define the house these points rule and count the numer of points in these houses --> indirectHouseCount
            var indirectHouseCount = 0;
            var houseRulers = RulerPairsForHouses(SignsOnHouseCusps);
            foreach (var houseRuler in houseRulers)
            {
                if (houseRuler.MainRuler == mainRuler || houseRuler.SubRuler == mainRuler ||
                    houseRuler.MainRuler == subRuler || houseRuler.SubRuler == subRuler)
                {
                    

                    
                    
                    
                }
            }
            // define the total of directHouseCount and indirectHouseCount --> totalHouseCount
            // define the total of directSignCount and directHouseCount --> totalCount
            // add the line to the list
        }
        
        
        return dispositors;
        
    }

    private static bool IsRuler(ChartPoints chartPoint)
    {
        return chartPoint == ChartPoints.Sun || 
               chartPoint == ChartPoints.Moon || 
               chartPoint == ChartPoints.Mercury || 
               chartPoint == ChartPoints.Venus || 
               chartPoint == ChartPoints.Mars || 
               chartPoint == ChartPoints.Jupiter || 
               chartPoint == ChartPoints.Neptune || 
               chartPoint == ChartPoints.Pluto || 
               chartPoint == ChartPoints.ApogeeMean || 
               chartPoint == ChartPoints.Priapus || 
               chartPoint == ChartPoints.PersephoneCarteret || 
               chartPoint == ChartPoints.VulcanusCarteret;
        
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