// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Analysis;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Api.Analysis;


/// <summary>Api for the analysis of aspects.</summary>
public interface IParallelsApi
{
    /// <summary>Aspects for celestial points.</summary>
    public IEnumerable<DefinedParallel> ParallelsForCelPoints(ParallelRequest request);

}

/// <inheritdoc/>
public sealed class ParallelsApi(IParallelsHandler parallelsHandler) : IParallelsApi
{
    /// <inheritdoc/>
    public IEnumerable<DefinedParallel> ParallelsForCelPoints(ParallelRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.CalcChart);
        Log.Information("AspectsApi: AspectsForChartPoints for chart {Name}", 
            request.CalcChart.InputtedChartData.MetaData.Name);
        return parallelsHandler.ParallelsForChartPoints(request);
    }
}