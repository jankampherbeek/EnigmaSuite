// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.RequestResponse;

namespace Enigma.Api.Analysis;


public class MidpointsApi : IMidpointsApi
{

    private readonly IMidpointsHandler _midpointsHandler;

    public MidpointsApi(IMidpointsHandler midpointsHandler)
    {
        _midpointsHandler = midpointsHandler;
    }

    public MidpointResponse AllMidpoints(MidpointRequest request)
    {
        Guard.Against.Null(request);
        return _midpointsHandler.RetrieveMidpoints(request);
    }



}