// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.CalcChartsRange;
using Enigma.Domain.RequestResponse;

namespace Engima.Core.Handlers.Calc.CelestialPoints;

/// <inheritdoc/>
public class CalcChartsRangeHandler: ICalcChartsRangeHandler
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
        List<FullChartForResearchItem> fullChartForResearchItems = new();

        CalculationPreferences preferences = request.Preferences;
        List<DataForCalculationOfRange> calcData = request.CalcData;

        foreach (var calcDataItem in calcData)
        {
            JulianDayRequest jdRequest = new(calcDataItem.DateTime);
            double jdUt = _julDayHandler.CalcJulDay(jdRequest).JulDayUt;
            CelPointsRequest celPointsRequest = new(jdUt, calcDataItem.Location, preferences);
            ChartAllPositionsRequest chartAllPositionsRequest = new(celPointsRequest, preferences.ActualHouseSystem);
            ChartAllPositionsResponse response = _chartAllPositionsHandler.CalcFullChart(chartAllPositionsRequest);
            fullChartForResearchItems.Add(new FullChartForResearchItem(calcDataItem.Id, response.CelPointPositions, response.MundanePositions));
        }
        return fullChartForResearchItems;
    }

}