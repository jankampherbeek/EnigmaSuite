// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;
using Enigma.Api;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Frontend.Ui.Support;

/// <inheritdoc/>
public sealed class ChartCalculation : IChartCalculation
{
    private readonly IChartAllPositionsApi _chartAllPositionsApi;
    private readonly IConfigPreferencesConverter _configPrefsConverter;


    public ChartCalculation(IChartAllPositionsApi chartAllPositionsApi, IConfigPreferencesConverter configPrefsConverter)
    {
        _chartAllPositionsApi = chartAllPositionsApi;
        _configPrefsConverter = configPrefsConverter;
    }


    /// <inheritdoc/>
    public CalculatedChart CalculateChart(ChartData chartData)
    {
        CelPointsRequest celPointsRequest = new(chartData.FullDateTime.JulianDayForEt, chartData.Location, 
            _configPrefsConverter.RetrieveCalculationPreferences());
        Log.Information("ChartCalculation.CalculateChart(): retrieving calculated chart from ChartAllpositionsApi");
        Dictionary<ChartPoints, FullPointPos> calculatedChartPositions = _chartAllPositionsApi.GetChart(celPointsRequest);
        return new CalculatedChart(calculatedChartPositions, chartData);
    }

}