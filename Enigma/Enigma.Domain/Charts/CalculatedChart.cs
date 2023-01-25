// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;

namespace Enigma.Domain.Charts;


/// <summary>Inputted data and calculation results for a chart.</summary>
/// <param name="Positions">Full positions for chart points.</param>
/// <param name="InputtedChartData">Originally inputted data.</param>
public record CalculatedChart(CalculatedChartPositions Positions, ChartData InputtedChartData);

