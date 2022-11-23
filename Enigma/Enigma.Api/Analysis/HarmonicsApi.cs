// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Work.Analysis.Harmonics;
using Enigma.Domain.Charts;

namespace Enigma.Api.Analysis;

/// <inheritdoc/>
public class HarmonicsApi : IHarmonicsApi
{

    private readonly IHarmonicsHandler _handler;

    public HarmonicsApi(IHarmonicsHandler handler)
    {
        _handler= handler;
    }

    /// <inheritdoc/>
    public List<double> Harmonics(CalculatedChart chart, double harmonicNumber)
    {
        Guard.Against.Null(chart);
        Guard.Against.NegativeOrZero(harmonicNumber-1); // harmonic number must be at least 1
        return _handler.RetrieveHarmonicPositions(chart, harmonicNumber);
    }
}