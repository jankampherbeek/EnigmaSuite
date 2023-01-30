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
    public CountOfPartsResponse PerformPartsCountTest(GeneralResearchRequest request)
    {
        Guard.Against.Null(request);
        Log.Information("ResearchPerformApi: PerformPartsCountTest(): " + request.Method);
        return _researchMethodHandler.HandleTestForPartsMethod(request);
    }

    /// <inheritdoc/>
    public CountOfAspectsResponse PerformAspectCount(GeneralResearchRequest request)
    {
        Guard.Against.Null(request);
        Log.Information("ResearchPerformApi: PerformAspectCount(): " + request.Method);
        return _researchMethodHandler.HandleTestForAspectsMethod(request);
    }

    /// <inheritdoc/>w
    public CountOfUnaspectedResponse PerformUnaspectedCount(GeneralResearchRequest request)
    {
        Guard.Against.Null(request);
        Log.Information("ResearchPerformApi: PerformUnaspectedCount(): " + request.Method);
        return _researchMethodHandler.HandleTestForUnaspectedMethod(request);
    }

    /// <inheritdoc/>
    public CountOfOccupiedMidpointsResponse PerformOccupiedMidpointsCount(CountMidpointsPerformRequest request)
    {
        Guard.Against.Null(request);
        Log.Information("ResearchPerformApi: PerformOccupiedMidpointsCount(): " + request.Method);
        return _researchMethodHandler.HandleTestForOccupiedMidpoints(request);   
    }

}