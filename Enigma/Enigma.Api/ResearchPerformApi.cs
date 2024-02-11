// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Api;


/// <summary>Api for handling tests.</summary>
public interface IResearchPerformApi
{

    /// <summary>Perform a test.</summary>
    /// <param name="request">GeneralResearchRequest or one of its children.</param>
    /// <returns>MethodResponse or one of its children.</returns>
    public MethodResponse PerformResearch(GeneralResearchRequest request);
}

/// <inheritdoc/>
public sealed class ResearchPerformApi : IResearchPerformApi
{

    private readonly IResearchMethodHandler _researchMethodHandler;


    public ResearchPerformApi(IResearchMethodHandler researchPerformHandler)
    {
        _researchMethodHandler = researchPerformHandler;
    }

    /// <inheritdoc/>
    public MethodResponse PerformResearch(GeneralResearchRequest request)
    {
        Guard.Against.Null(request);
        Log.Information("ResearchPerformApi: PerformResearch(): {Method}", request.Method);
        return _researchMethodHandler.HandleResearch(request);
    }


}