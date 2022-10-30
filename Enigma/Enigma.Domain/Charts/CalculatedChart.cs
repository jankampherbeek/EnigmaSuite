// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;



namespace Enigma.Domain.Charts;

/// <summary>Inputted data and calculation results for a chart.</summary>
public record CalculatedChart
{
    public readonly List<FullSolSysPointPos> SolSysPointPositions;
    public readonly FullHousesPositions FullHousePositions;
    public readonly ChartData InputtedChartData;


    public CalculatedChart(List<FullSolSysPointPos> solSysPointPositions, FullHousesPositions fullHousePositions, ChartData inputtedChartData)
    {
        SolSysPointPositions = solSysPointPositions;
        FullHousePositions = fullHousePositions;
        InputtedChartData = inputtedChartData;
    }

}