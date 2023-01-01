// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Points;

namespace Enigma.Domain.Charts;

/// <summary>Inputted data and calculation results for a chart.</summary>
public record CalculatedChart
{
    public readonly List<FullCelPointPos> CelPointPositions;
    public readonly FullHousesPositions FullHousePositions;
    public readonly ChartData InputtedChartData;


    public CalculatedChart(List<FullCelPointPos> celPointPositions, FullHousesPositions fullHousePositions, ChartData inputtedChartData)
    {
        CelPointPositions = celPointPositions;
        FullHousePositions = fullHousePositions;
        InputtedChartData = inputtedChartData;
    }

}