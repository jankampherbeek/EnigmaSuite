// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.Specials;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Calc.CelestialPoints;

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
        List<FullChartForResearchItem> fullChartForResearchItems = new();

        CalculationPreferences preferences = request.Preferences;
        List<DataForCalculationOfRange> calcData = request.CalcData;

        foreach (var calcDataItem in calcData)
        {
            double jdUt = _julDayHandler.CalcJulDay(calcDataItem.DateTime).JulDayUt;
            CelPointsRequest celPointsRequest = new(jdUt, calcDataItem.Location, preferences);
            Dictionary<ChartPoints, FullPointPos> chartPositions = _chartAllPositionsHandler.CalcFullChart(celPointsRequest);
            fullChartForResearchItems.Add(new FullChartForResearchItem(calcDataItem.Id, chartPositions));
        }
        return fullChartForResearchItems;
    }

}