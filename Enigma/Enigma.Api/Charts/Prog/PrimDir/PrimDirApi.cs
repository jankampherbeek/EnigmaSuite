// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Charts.Prog.PrimDir;
using Enigma.Domain.Charts.Prog.PrimDir;

namespace Enigma.Api.Charts.Prog.PrimDir;

/// <summary>API for the calculation of primary directions.</summary>
public interface IPrimDirApi
{
    /// <summary>Calculate primary directions.</summary>
    /// <param name="request">Request.</param>
    /// <returns>Response with the calculated directions, indication of possible errors and a result text.</returns>
    public PrimDirResponse CalcPrimDir(PrimDirRequest request);
}

// ============================ Implementation ====================================================================

/// <inheritdoc/>
public class PrimDirApi(IProgPrimDirHandler handler) : IPrimDirApi
{
    private IProgPrimDirHandler _handler = handler;
    
    /// <inheritdoc/>
    public PrimDirResponse CalcPrimDir(PrimDirRequest request)
    {
        Guard.Against.Null(request);
        return _handler.HandleRequest(request);
    }
}