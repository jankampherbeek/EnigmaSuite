// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Details for a chart that are relevant for the BLA schema calculations
/// </summary>
public record ChartDetails(List<BlaPositions> SignsDecans, 
    int[] InterceptedSigns, 
    int[] ClampedHouses,
    Dictionary<ChartPoints, int> Houses,
    Dictionary<ChartPoints, int> QuadrantCounts,
    Dictionary<int, List<RulerPair>> HouseRulers);

/// <summary>
/// Combination of ruler and subruler
/// </summary>
/// <param name="Ruler">ChartPoint for the ruler</param>
/// <param name="SubRuler">ChartPoint for the subruler</param>
public record RulerPair(ChartPoints Ruler, ChartPoints SubRuler);

/// <summary>
/// Factory for ChartDetails
/// </summary>
public class ChartDetailsFactory(BlaPositionsFactory blaPositionsFactory, HousePositions housePos, QuadrantPositions quadrantPos)
{
    /// <summary>
    /// Create ChartDetails
    /// </summary>
    /// <param name="chart">A recalculated chart which should include the required chartpoints, independent of the configuration.</param>
    /// <returns>Populated instance of ChartDetails</returns>
    public ChartDetails CreateChartDetails(CalculatedChart chart)
    {
        // calculate positions in signs and decans
        List<BlaPositions> signsDecans = [];
        foreach (var (chartPoint, value) in chart.Positions)
        {
            var longitude = value.Ecliptical.MainPosSpeed.Position;
            var blaPositions = blaPositionsFactory.CreateBlaPositions(chartPoint, longitude);
            signsDecans.Add(blaPositions);
        }

        var houseCounts = housePos.DefineHousePositions(chart);
        var quadrantCounts = quadrantPos.DefineQuadrants(chart);
        
        
        
        int[] interceptedSigns = [];
        int[] clampedHouses = [];
        
        
        // define main ruler and sub ruler for each house
        var houseRulers = new Dictionary<int, List<RulerPair>>();
        
        
        // create a hardcoded list of rulers and subrulers
        // use the longitude of the cusp to define the ruler for the house
        // add rulers for any intercepted sign in the house

        
        // define mundane ruler and sub ruler for each house
        // create a hardcoded list of mundane rulers and subrulers    


        return new ChartDetails(signsDecans, interceptedSigns, clampedHouses, houseCounts,  quadrantCounts, houseRulers);

    }
    
   
    
    
    
 
    
    
}