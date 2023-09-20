// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Serilog;

namespace Enigma.Api.Analysis;


/// <inheritdoc/>
public sealed class MidpointsApi : IMidpointsApi
{

    private readonly IMidpointsHandler _midpointsHandler;

    public MidpointsApi(IMidpointsHandler midpointsHandler)
    {
        _midpointsHandler = midpointsHandler;
    }


    /// <inheritdoc/>
    public IEnumerable<BaseMidpoint> AllMidpoints(CalculatedChart chart)
    {
        Guard.Against.Null(chart);
        Log.Information("MidpointsApi: AllMidpoints for chart : {ChartName} ", chart.InputtedChartData.MetaData.Name);
        return _midpointsHandler.RetrieveBaseMidpoints(chart);
    }


    /// <inheritdoc/>
    public IEnumerable<OccupiedMidpoint> OccupiedMidpoints(CalculatedChart chart, double dialSize, double orb)
    {
        Guard.Against.Null(chart);
        Log.Information("MidpointsApi: OccupiedMidpointsFinder in dial size {DialSize} for chart : {ChartName} ", dialSize, chart.InputtedChartData.MetaData.Name);
        return _midpointsHandler.RetrieveOccupiedMidpoints(chart, dialSize, orb);
    }

}