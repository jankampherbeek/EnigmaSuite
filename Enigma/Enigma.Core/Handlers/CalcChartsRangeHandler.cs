// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;

namespace Enigma.Core.Handlers;

/// <inheritdoc/>
public sealed class CalcChartsRangeHandler : ICalcChartsRangeHandler
{
    private readonly IChartAllPositionsHandler _chartAllPositionsHandler;
    private readonly IJulDayHandler _julDayHandler;

    public CalcChartsRangeHandler(IChartAllPositionsHandler chartAllPositionsHandler, IJulDayHandler julDayHandler)
    {
        _chartAllPositionsHandler = chartAllPositionsHandler;
        _julDayHandler = julDayHandler;
    }

    /// <inheritdoc/>
    public List<FullChartForResearchItem> CalculateRange(ChartsRangeRequest request)
    {
        CalculationPreferences preferences = request.Preferences;
        List<DataForCalculationOfRange> calcData = request.CalcData;

        return (from calcDataItem in calcData 
            let jdUt = _julDayHandler.CalcJulDay(calcDataItem.DateTime).JulDayUt 
            let celPointsRequest = new CelPointsRequest(jdUt, calcDataItem.Location, preferences) 
            let chartPositions = _chartAllPositionsHandler.CalcFullChart(celPointsRequest) 
            select new FullChartForResearchItem(calcDataItem.Id, chartPositions)).ToList();
    }

}