// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Api.Analysis;


/// <summary>Api for finding aspects between progressive and radix positions.</summary>
public interface IProgAspectsApi
{
    /// <summary>Find aspects.</summary>
    /// <param name="request">Definitions for radix pints, progressive oints, aspects and orb.</param>
    /// <returns>Resulting aspects and result code.</returns>
    public ProgAspectsResponse FindProgAspects(ProgAspectsRequest request);
}

/// <inheritdoc/>
public class ProgAspectsApi(IProgAspectsHandler handler) : IProgAspectsApi
{
    /// <inheritdoc/>
    public ProgAspectsResponse FindProgAspects(ProgAspectsRequest request)
    {
        return handler.FindProgAspects(request);
    }
}