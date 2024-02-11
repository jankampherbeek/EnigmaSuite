// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Serilog;

namespace Enigma.Api;

/// <summary>API for the calculation of a range of charts.</summary>
public interface ICalcChartsRangeApi
{
    /// <summary>Calculate range of charts.</summary>
    /// <param name="request">Request with data and settings.</param>
    /// <returns>Calculated charts.</returns>
    public List<FullChartForResearchItem> CalculateRange(ChartsRangeRequest request);
}

/// <inheritdoc/>
public sealed class CalcChartsRangeApi : ICalcChartsRangeApi
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