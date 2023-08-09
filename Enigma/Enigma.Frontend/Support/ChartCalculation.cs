// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using System.Collections.Generic;
using System.Linq;

namespace Enigma.Frontend.Ui.Support;

/// <inheritdoc/>
public sealed class ChartCalculation : IChartCalculation
{
    private readonly IChartAllPositionsApi _chartAllPositionsApi;


    public ChartCalculation(IChartAllPositionsApi chartAllPositionsApi)
    {
        _chartAllPositionsApi = chartAllPositionsApi;
    }


    /// <inheritdoc/>
    public CalculatedChart CalculateChart(ChartData chartData)
    {
        CelPointsRequest celPointsRequest = new(chartData.FullDateTime.JulianDayForEt, chartData.Location, 
            RetrieveCalculationPreferences());
        Dictionary<ChartPoints, FullPointPos> calculatedChartPositions = _chartAllPositionsApi.GetChart(celPointsRequest);
        return new CalculatedChart(calculatedChartPositions, chartData);
    }



    /// <summary>
    /// Retrieve calculation preferences from active modus. 
    /// </summary>
    private static CalculationPreferences RetrieveCalculationPreferences()
    {
        AstroConfig config = CurrentConfig.Instance.GetConfig();
        List<ChartPoints> celPoints = (
            from spec in config.ChartPoints 
            where spec.Value.IsUsed 
            select spec.Key).ToList();
        return new CalculationPreferences(celPoints, config.ZodiacType, config.Ayanamsha, CoordinateSystems.Ecliptical, 
            config.ObserverPosition, config.ProjectionType, config.HouseSystem);
    }
}