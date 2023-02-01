// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Research;
using Serilog;

namespace Enigma.Api.Research;


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
        Log.Information("ResearchPerformApi: PerformResearch(): " + request.Method);
        return _researchMethodHandler.HandleResearch(request);
    }


}