// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Research;
using Serilog;

namespace Enigma.Api.Research;


/// <inheritdoc/>
public class ResearchPerformApi : IResearchPerformApi
{

    private readonly IResearchMethodHandler _researchPerformHandler;


    public ResearchPerformApi(IResearchMethodHandler researchPerformHandler)
    {
        _researchPerformHandler = researchPerformHandler;
    }


    /// <inheritdoc/>
    public CountOfPartsResponse PerformTest(GeneralCountRequest request)
    {
        Guard.Against.Null(request);
        Log.Information("ResearchPerformApi: PerformTest: " + request.Method);
        return _researchPerformHandler.HandleTestMethod(request);
    }
}