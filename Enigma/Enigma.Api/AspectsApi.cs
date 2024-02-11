// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Api;


/// <summary>Api for the analysis of aspects.</summary>
public interface IAspectsApi
{
    /// <summary>Aspects for celestial points.</summary>
    public IEnumerable<DefinedAspect> AspectsForCelPoints(AspectRequest request);

}

/// <inheritdoc/>
public sealed class AspectsApi : IAspectsApi
{

    private readonly IAspectsHandler _aspectHandler;

    public AspectsApi(IAspectsHandler aspectHandler)
    {
        _aspectHandler = aspectHandler;
    }


    /// <inheritdoc/>
    public IEnumerable<DefinedAspect> AspectsForCelPoints(AspectRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.CalcChart);
        Log.Information("AspectsApi: AspectsForChartPoints for chart {Name}", 
            request.CalcChart.InputtedChartData.MetaData.Name);
        return _aspectHandler.AspectsForChartPoints(request);
    }
}

