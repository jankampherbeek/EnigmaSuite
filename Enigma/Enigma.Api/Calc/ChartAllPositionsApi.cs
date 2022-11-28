// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Domain.RequestResponse;

namespace Enigma.Api.Astron;




/// <inheritdoc/>
public class ChartAllPositionsApi : IChartAllPositionsApi
{
    private readonly IChartAllPositionsHandler _handler;

    /// <param name="handler">Handler for the calculation of the chart.</param>
    public ChartAllPositionsApi(IChartAllPositionsHandler handler) => _handler = handler;

    public ChartAllPositionsResponse GetChart(ChartAllPositionsRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.celPointsRequest);
        Guard.Against.Null(request.celPointsRequest.ChartLocation);
        Guard.Against.Null(request.celPointsRequest.ActualCalculationPreferences);
        Guard.Against.Null(request.celPointsRequest.ActualCalculationPreferences.ActualProjectionType);
        Guard.Against.Null(request.celPointsRequest.ActualCalculationPreferences.ActualAyanamsha);
        Guard.Against.Null(request.celPointsRequest.ActualCalculationPreferences.ActualObserverPosition);
        Guard.Against.Null(request.celPointsRequest.ActualCalculationPreferences.ActualHouseSystem);
        Guard.Against.Null(request.celPointsRequest.ActualCalculationPreferences.ActualCelPoints);
        Guard.Against.Null(request.celPointsRequest.ActualCalculationPreferences.ActualZodiacType);
        Guard.Against.Null(request.HouseSystem);

        return _handler.CalcFullChart(request);
    }

}