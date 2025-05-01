// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Analysis;
using Enigma.Domain.Dtos;
using Serilog;

namespace Enigma.Api.Analysis;


/// <summary>Api for the calculation of harmonics.</summary>
public interface IHarmonicsApi
{
    /// <summary>Calculate harmonic positions using the given harmonic number.</summary>
    /// <param name="chart">Calculated chart.</param>
    /// <param name="harmonicNumber">Number for the harmonic, a fractional number is possible.</param>
    /// <returns>List with harmonic positions, the celestial points in the same sequence as in the calculated chart,
    /// followed by Mc, Asc, Vertex and Eastpoint (in that sequence).</returns>
    public List<double> Harmonics(CalculatedChart chart, double harmonicNumber);
}

/// <inheritdoc/>
public sealed class HarmonicsApi(IHarmonicsHandler handler) : IHarmonicsApi
{
    /// <inheritdoc/>
    public List<double> Harmonics(CalculatedChart chart, double harmonicNumber)
    {
        Guard.Against.Null(chart);
        Guard.Against.NegativeOrZero(harmonicNumber - 1); // harmonic number must be at least 1
        Log.Information("HarmonicsApi: Harmonics nr. {HarmNr} for chart : {ChartName} ", harmonicNumber, 
            chart.InputtedChartData.MetaData.Name);
        return handler.RetrieveHarmonicPositions(chart, harmonicNumber);
    }
}