// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Api;


/// <summary>Api for the analysis of aspects.</summary>
public interface IParallelsApi
{
    /// <summary>Aspects for celestial points.</summary>
    public IEnumerable<DefinedParallel> ParallelsForCelPoints(ParallelRequest request);

}

/// <inheritdoc/>
public sealed class ParallelsApi : IParallelsApi
{


    private readonly IParallelsHandler _parallelsHandler;

    public ParallelsApi(IParallelsHandler parallelsHandler)
    {
        _parallelsHandler = parallelsHandler;
    }


    /// <inheritdoc/>
    public IEnumerable<DefinedParallel> ParallelsForCelPoints(ParallelRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.CalcChart);
        Log.Information("AspectsApi: AspectsForChartPoints for chart {Name}", 
            request.CalcChart.InputtedChartData.MetaData.Name);
        return _parallelsHandler.ParallelsForChartPoints(request);
    }
}