// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Analysis;

/// <inheritdoc/>
public sealed class HarmonicsHandler : IHarmonicsHandler
{
    private readonly IHarmonicsCalculator _calculator;

    public HarmonicsHandler(IHarmonicsCalculator calculator)
    {
        _calculator = calculator;
    }

    /// <inheritdoc/>
    public List<double> RetrieveHarmonicPositions(CalculatedChart chart, double harmonicNumber)
    {
        List<double> originalPositions = new();
        foreach (var celPoint in chart.ChartPointPositions)
        {
            originalPositions.Add(celPoint.PointPos.Longitude.Position);
        }
        List<FullChartPointPos> housePositions = new() { chart.FullHousePositions.Mc, chart.FullHousePositions.Ascendant, chart.FullHousePositions.Vertex, chart.FullHousePositions.EastPoint };
        foreach (var housePosition in housePositions)
        {
            originalPositions.Add(housePosition.PointPos.Longitude.Position);
        }
        return _calculator.CalculateHarmonics(originalPositions, harmonicNumber);
    }

}