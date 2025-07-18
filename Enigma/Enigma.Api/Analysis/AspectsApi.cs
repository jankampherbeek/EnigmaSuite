// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Analysis;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Api.Analysis;


/// <summary>Api for the analysis of aspects.</summary>
public interface IAspectsApi
{
    /// <summary>Aspects for celestial points.</summary>
    public IEnumerable<DefinedAspect> AspectsForCelPoints(AspectRequest request);

}

/// <inheritdoc/>
public sealed class AspectsApi(IAspectsHandler aspectHandler) : IAspectsApi
{
    /// <inheritdoc/>
    public IEnumerable<DefinedAspect> AspectsForCelPoints(AspectRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.CalcChart);
        return aspectHandler.AspectsForChartPoints(request);
    }
}

