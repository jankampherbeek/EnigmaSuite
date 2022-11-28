// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Research;

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
    public ResearchResponse PerformTest(ResearchRequest request)
    {
        return _researchPerformHandler.HandleTestPeformance(request);
    }
}