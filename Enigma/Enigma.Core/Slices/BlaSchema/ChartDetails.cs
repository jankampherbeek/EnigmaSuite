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
    List<int> InterceptedSigns, 
    List<int> ClampedHouses,
    Dictionary<ChartPoints, int> Houses,
    Dictionary<ChartPoints, int> QuadrantCounts,
    Dictionary<int, List<RulerPair>> HouseRulers);



/// <summary>
/// Factory for ChartDetails
/// </summary>
public class ChartDetailsFactory(BlaPositionsFactory blaPositionsFactory, 
    HousePositions housePos, 
    QuadrantPositions quadrantPos,
    InterceptedClamped interceptedClamped,
    HouseRulers houseRulers)
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
        var interceptedSigns = interceptedClamped.DefineInterceptedSigns(chart);
        var clampedHouses = interceptedClamped.DefineClampedHouses(chart);
        var allHouseRulers = houseRulers.DefineHouseRulers(chart);
        
        return new ChartDetails(signsDecans, interceptedSigns, clampedHouses, houseCounts,  quadrantCounts, allHouseRulers);

    }
    
}