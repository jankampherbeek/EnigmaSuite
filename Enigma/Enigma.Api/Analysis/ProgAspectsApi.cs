// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Core.Interfaces;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Api.Analysis;

/// <inheritdoc/>
public class ProgAspectsApi: IProgAspectsApi
{
    private readonly IProgAspectsHandler _handler;

    public ProgAspectsApi(IProgAspectsHandler handler)
    {
        _handler = handler;
    }
    
    /// <inheritdoc/>
    public ProgAspectsResponse FindProgAspects(ProgAspectsRequest request)
    {
        return _handler.FindProgAspects(request);
    }
}