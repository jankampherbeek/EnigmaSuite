// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Core.Interfaces;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Api;

/// <inheritdoc/>
public class ProgPrimDirApi: IProgPrimDirApi
{

    private readonly IProgPrimDirHandler _handler;


    public ProgPrimDirApi(IProgPrimDirHandler handler)
    {
        _handler = handler;
    }
    
    /// <inheritdoc/>
    public ProgPrimDirResponse CalcPrimDir(ProgPrimDirRequest request)
    {
        return _handler.CalculatePrimDir(request);
    }
    
    
}