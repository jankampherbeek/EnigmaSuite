// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Domain.CalcChartsRange;
using Serilog;

namespace Enigma.Api.Calc.CalcChartsRangeApi;

/// <inheritdoc/>
public class CalcChartsRangeApi: ICalcChartsRangeApi
{
    private readonly ICalcChartsRangeHandler _handler;

    public CalcChartsRangeApi(ICalcChartsRangeHandler handler)
    {
        _handler = handler;
    }

    /// <inheritdoc/>
    public List<FullChartForResearchItem> CalculateRange(ChartsRangeRequest request)
    {
        Guard.Against.Null(request);
        Log.Information("CalcChartsRangeApi: CalculateRange");
        return _handler.CalculateRange(request);
    }
} 