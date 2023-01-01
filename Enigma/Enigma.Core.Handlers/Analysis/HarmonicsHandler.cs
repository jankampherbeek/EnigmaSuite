// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Work.Analysis.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;

namespace Enigma.Core.Handlers.Analysis;

/// <inheritdoc/>
public class HarmonicsHandler : IHarmonicsHandler
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
        foreach (var celPoint in chart.CelPointPositions)
        {
            originalPositions.Add(celPoint.GeneralPointPos.Longitude.Position);
        }
        List<CuspFullPos> housePositions = new() { chart.FullHousePositions.Mc, chart.FullHousePositions.Ascendant, chart.FullHousePositions.Vertex, chart.FullHousePositions.EastPoint };
        foreach (var housePosition in housePositions)
        {
            originalPositions.Add(housePosition.Longitude);
        }
        return _calculator.CalculateHarmonics(originalPositions, harmonicNumber);
    }

}