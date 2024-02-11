// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Serilog;

namespace Enigma.Api;

/// <summary>Api for the analysis of midpoints.</summary>
public interface IMidpointsApi
{
    /// <summary>Return all base midpoints.</summary>
    /// <param name="chart">Calculated chart.</param>
    /// <returns>Midpoints in 360 degree dial, regardless of being occupied.</returns>
    public IEnumerable<BaseMidpoint> AllMidpoints(CalculatedChart chart);

    /// <summary>Return all occupied midpoints for a specific dial.</summary>
    /// <param name="chart">Chart with positions.</param>
    /// <param name="dialSize">Size of dial in degrees.</param>
    /// <param name="orb">Base orb from configuration.</param>
    /// <returns>All occupied midpoints.</returns>
    public IEnumerable<OccupiedMidpoint> OccupiedMidpoints(CalculatedChart chart, double dialSize, double orb);
}

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