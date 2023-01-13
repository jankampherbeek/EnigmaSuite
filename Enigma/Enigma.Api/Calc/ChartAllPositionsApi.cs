// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Serilog;

namespace Enigma.Api.Astron;

/// <inheritdoc/>
public sealed class ChartAllPositionsApi : IChartAllPositionsApi
{
    private readonly IChartAllPositionsHandler _handler;

    /// <param name="handler">Handler for the calculation of the chart.</param>
    public ChartAllPositionsApi(IChartAllPositionsHandler handler) => _handler = handler;


    /// <inheritdoc/>
    public ChartAllPositionsResponse GetChart(ChartAllPositionsRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.CelPointRequest);
        Guard.Against.Null(request.CelPointRequest.Location);
        Guard.Against.Null(request.CelPointRequest.CalculationPreferences);
        Guard.Against.Null(request.CelPointRequest.CalculationPreferences.ActualProjectionType);
        Guard.Against.Null(request.CelPointRequest.CalculationPreferences.ActualAyanamsha);
        Guard.Against.Null(request.CelPointRequest.CalculationPreferences.ActualObserverPosition);
        Guard.Against.Null(request.CelPointRequest.CalculationPreferences.ActualHouseSystem);
        Guard.Against.Null(request.CelPointRequest.CalculationPreferences.ActualChartPoints);
        Guard.Against.Null(request.CelPointRequest.CalculationPreferences.ActualZodiacType);
        Guard.Against.Null(request.HouseSystem);

        Log.Information("ChartAllPositionsApi.GetChart()");
        return _handler.CalcFullChart(request);
    }

}