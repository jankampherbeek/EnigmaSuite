﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Serilog;

namespace Enigma.Api;

/// <inheritdoc/>
public sealed class HarmonicsApi : IHarmonicsApi
{

    private readonly IHarmonicsHandler _handler;

    public HarmonicsApi(IHarmonicsHandler handler)
    {
        _handler = handler;
    }

    /// <inheritdoc/>
    public List<double> Harmonics(CalculatedChart chart, double harmonicNumber)
    {
        Guard.Against.Null(chart);
        Guard.Against.NegativeOrZero(harmonicNumber - 1); // harmonic number must be at least 1
        Log.Information("HarmonicsApi: Harmonics nr. {HarmNr} for chart : {ChartName} ", harmonicNumber, 
            chart.InputtedChartData.MetaData.Name);
        return _handler.RetrieveHarmonicPositions(chart, harmonicNumber);
    }
}