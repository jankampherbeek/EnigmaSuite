// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

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
            if (chartPoint.GetDetails().PointCat == PointCats.Common ||
                chartPoint.GetDetails().PointCat == PointCats.Angle)
            {
                var longitude = value.Ecliptical.MainPosSpeed.Position;
                var house = housePos.FindSingleHousePosition(chart, longitude);
                var blaPositions = blaPositionsFactory.CreateBlaPositions(chartPoint, longitude, house);
                signsDecans.Add(blaPositions);  
            }
        }

        var houseCounts = housePos.DefineHousePositions(chart);
        var quadrantCounts = quadrantPos.DefineQuadrants(chart);
        var interceptedSigns = interceptedClamped.DefineInterceptedSigns(chart);
        var clampedHouses = interceptedClamped.DefineClampedHouses(chart);
        var allHouseRulers = houseRulers.DefineHouseRulers(chart);
        
        return new ChartDetails(signsDecans, interceptedSigns, clampedHouses, houseCounts,  quadrantCounts, allHouseRulers);

    }
    
}