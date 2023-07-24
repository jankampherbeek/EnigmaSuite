// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Points;
using Serilog;

namespace Enigma.Api.Calc;

/// <inheritdoc/>
public sealed class ChartAllPositionsApi : IChartAllPositionsApi
{
    private readonly IChartAllPositionsHandler _handler;

    /// <param name="handler">Handler for the calculation of the chart.</param>
    public ChartAllPositionsApi(IChartAllPositionsHandler handler) => _handler = handler;


    /// <inheritdoc/>
    public Dictionary<ChartPoints, FullPointPos> GetChart(CelPointsRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.Location);
        Guard.Against.Null(request.CalculationPreferences);
        Guard.Against.Null(request.CalculationPreferences.ActualProjectionType);
        Guard.Against.Null(request.CalculationPreferences.ActualAyanamsha);
        Guard.Against.Null(request.CalculationPreferences.ActualObserverPosition);
        Guard.Against.Null(request.CalculationPreferences.ActualHouseSystem);
        Guard.Against.Null(request.CalculationPreferences.ActualChartPoints);
        Guard.Against.Null(request.CalculationPreferences.ActualZodiacType);

        Log.Information("ChartAllPositionsApi.GetChart()");
        return _handler.CalcFullChart(request);
    }

}