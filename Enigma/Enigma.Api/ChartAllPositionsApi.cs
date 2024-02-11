// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Api;


/// <summary>API for calculation of a fully defined chart.</summary>
public interface IChartAllPositionsApi
{
    /// <summary>Api call to calculate a full chart.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Response with all positions.</returns>
    public Dictionary<ChartPoints, FullPointPos> GetChart(CelPointsRequest request);
}

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