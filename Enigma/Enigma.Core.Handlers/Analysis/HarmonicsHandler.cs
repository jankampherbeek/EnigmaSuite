// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
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
        
        
        foreach (KeyValuePair<ChartPoints, FullPointPos> celPoint in chart.Positions.CommonPoints)
        {
            originalPositions.Add(celPoint.Value.Ecliptical.MainPosSpeed.Position);
        }
        foreach (KeyValuePair<ChartPoints, FullPointPos> housePosition in chart.Positions.Angles)
        {
            originalPositions.Add(housePosition.Value.Ecliptical.MainPosSpeed.Position);
        }
        return _calculator.CalculateHarmonics(originalPositions, harmonicNumber);
    }

    /// <inheritdoc/>
    public Dictionary<ChartPoints, double> RetrieveHarmonicPositions(List<PositionedPoint> posPoints, double harmonicNumber)   // todo 0.1 test RetrieveHarmonicPositions.
    {
        Dictionary<ChartPoints, double> harmonicPositions = new();
        foreach (PositionedPoint posPoint in posPoints)
        {
            double harmonic = _calculator.CalculateHarmonic(posPoint.Position, harmonicNumber);
            harmonicPositions.Add(posPoint.Point, harmonic);
        }
        return harmonicPositions;
    }


}