// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Frontend.Ui.Support;

/// <summary>Calculation for a single chart.</summary>
public interface IChartCalculation
{

    /// <summary>Calculate the chart based on the current configuration.</summary>
    /// <param name="chartData">ChartData with all the required input.</param>
    /// <returns>The calculated chart.</returns>
    public CalculatedChart CalculateChart(ChartData chartData);
}

/// <inheritdoc/>
public sealed class ChartCalculation : IChartCalculation
{
    private readonly IChartAllPositionsApi _chartAllPositionsApi;
    private readonly IObliquityApi _obliquityApi;
    private readonly IConfigPreferencesConverter _configPrefsConverter;


    public ChartCalculation(IChartAllPositionsApi chartAllPositionsApi, IConfigPreferencesConverter configPrefsConverter,
        IObliquityApi obliquityApi)
    {
        _chartAllPositionsApi = chartAllPositionsApi;
        _obliquityApi = obliquityApi;
        _configPrefsConverter = configPrefsConverter;
    }


    /// <inheritdoc/>
    public CalculatedChart CalculateChart(ChartData chartData)
    {
        CelPointsRequest celPointsRequest = new(chartData.FullDateTime.JulianDayForEt, chartData.Location, 
            _configPrefsConverter.RetrieveCalculationPreferences());
        Log.Information("ChartCalculation.CalculateChart(): retrieving calculated chart from ChartAllpositionsApi");
        Dictionary<ChartPoints, FullPointPos> calculatedChartPositions = _chartAllPositionsApi.GetChart(celPointsRequest);
        ObliquityRequest obliquityRequest = new(chartData.FullDateTime.JulianDayForEt, true);
        double obliquity = _obliquityApi.GetObliquity(obliquityRequest); 
        return new CalculatedChart(calculatedChartPositions, chartData, obliquity);
    }

}