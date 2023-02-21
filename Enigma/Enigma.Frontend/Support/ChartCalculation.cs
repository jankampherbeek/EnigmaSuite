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

namespace Enigma.Frontend.Ui.Support;

/// <inheritdoc/>
public sealed class ChartCalculation: IChartCalculation
{
    private static readonly AstroConfig _config = CurrentConfig.Instance.GetConfig();
    private readonly IChartAllPositionsApi _chartAllPositionsApi;


    public ChartCalculation(IChartAllPositionsApi chartAllPositionsApi)
    {
        _chartAllPositionsApi = chartAllPositionsApi;
    }


    /// <inheritdoc/>
    public CalculatedChart CalculateChart(ChartData chartData)
    {
        CelPointsRequest celPointsRequest = new(chartData.FullDateTime.JulianDayForEt, chartData.Location, RetrieveCalculationPreferences());
        Dictionary<ChartPoints, FullPointPos> calculatedChartPositions = _chartAllPositionsApi.GetChart(celPointsRequest);
        return new CalculatedChart(calculatedChartPositions, chartData);
    }



    /// <summary>
    /// Retrieve calculation preferences from active modus. Currently uses hardcoded values.
    /// </summary>
    /// TODO: add CoordinateSystem to the configuration ???? Or use it without config but only as ad hoc setting?.
    private static CalculationPreferences RetrieveCalculationPreferences()
    {
        List<ChartPoints> celPoints = new();
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs> spec in _config.ChartPoints)
        {
            if (spec.Value.IsUsed)
            {
                celPoints.Add(spec.Key);
            }
        }
        return new CalculationPreferences(celPoints, _config.ZodiacType, _config.Ayanamsha, CoordinateSystems.Ecliptical, _config.ObserverPosition, _config.ProjectionType, _config.HouseSystem);
    }
}