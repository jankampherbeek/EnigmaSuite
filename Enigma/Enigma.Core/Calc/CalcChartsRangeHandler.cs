// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;

namespace Enigma.Core.Calc;

/// <summary>Handler for the calculation of  range of charts for research purposes.</summary>
public interface ICalcChartsRangeHandler
{
    /// <summary>Calculate a range of charts.</summary>
    /// <param name="request">Request with the data and the settings.</param>
    /// <returns>The calculated result.</returns>
    public List<FullChartForResearchItem> CalculateRange(ChartsRangeRequest request);
}


/// <inheritdoc/>
public sealed class CalcChartsRangeHandler(
    IChartAllPositionsHandler chartAllPositionsHandler,
    IJulDayHandler julDayHandler)
    : ICalcChartsRangeHandler
{
    /// <inheritdoc/>
    public List<FullChartForResearchItem> CalculateRange(ChartsRangeRequest request)
    {
        var preferences = request.Preferences;
        var calcData = request.CalcData;

        return (from calcDataItem in calcData 
            let jdUt = julDayHandler.CalcJulDay(calcDataItem.DateTime).JulDayUt 
            let celPointsRequest = new CelPointsRequest(jdUt, calcDataItem.Location, preferences) 
            let chartPositions = chartAllPositionsHandler.CalcFullChart(celPointsRequest) 
            select new FullChartForResearchItem(calcDataItem.Id, chartPositions)).ToList();
    }

}