// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Handlers;

/// <summary>Handler for harmonics.</summary>
public interface IHarmonicsHandler
{
    /// <summary>Define the harmonics for all positions in CalculatedChart.</summary>
    /// <param name="chart">Chart with all positions.</param>
    /// <param name="harmonicNumber">The harmonic number, this can also be a fractional number.</param>
    /// <returns>The calculated harmonic positions, all celestial points followed by Mc, Asc, Vertex, Eastpoint in that sequence.</returns>
    public List<double> RetrieveHarmonicPositions(CalculatedChart chart, double harmonicNumber);

    /// <summary>Define the harmonics a list of PositionedPoint.</summary>
    /// <param name="posPoints">The points to calculate.</param>
    /// <param name="harmonicNumber">The multiplication factor for the harmonic.</param>
    /// <returns>The calculated results.</returns>
    public Dictionary<ChartPoints, double> RetrieveHarmonicPositions(List<PositionedPoint> posPoints, double harmonicNumber);
}

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
        var allPoints = from point in chart.Positions           // TODO remove restriction for Vertex and Eastpoint as new glyphs are available
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