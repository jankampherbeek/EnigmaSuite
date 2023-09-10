// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using Enigma.Domain.References;

namespace Enigma.Core.Analysis;

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
        var allPoints = from point in chart.Positions           // TODO 0.6 remove restriction for Vertex and Easpoint as new glyphs are available
                        where (point.Key.GetDetails().PointCat == PointCats.Common) || (point.Key.GetDetails().PointCat == PointCats.Angle && point.Key != ChartPoints.Vertex && point.Key != ChartPoints.EastPoint)
                        select point;
        List<double> originalPositions = allPoints.Select(item => item.Value.Ecliptical.MainPosSpeed.Position).ToList();
        return _calculator.CalculateHarmonics(originalPositions, harmonicNumber);
    }

    /// <inheritdoc/>
    public Dictionary<ChartPoints, double> RetrieveHarmonicPositions(List<PositionedPoint> posPoints, double harmonicNumber)
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