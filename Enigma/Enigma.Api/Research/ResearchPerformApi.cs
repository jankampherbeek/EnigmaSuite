// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Research;
using Serilog;

namespace Engima.Api.Research;


/// <inheritdoc/>
public class ResearchPerformApi : IResearchPerformApi
{

    private readonly IResearchPerformHandler _researchPerformHandler;


    public ResearchPerformApi(IResearchPerformHandler researchPerformHandler)
    {
        _researchPerformHandler = researchPerformHandler;
    }


    /// <inheritdoc/>
    public MethodResponse PerformTest(MethodPerformRequest request)
    {
        Guard.Against.Null(request);
        Log.Information("ResearchPerformApi: PerformTest: " + request.Method);
        return _researchPerformHandler.HandleTestPeformance(request);
    }
}