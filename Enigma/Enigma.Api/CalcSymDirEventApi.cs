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
public sealed class CalcSymDirEventApi: ICalcSymDirEventApi
{
    private readonly ICalcSymDirHandler _calcSymDirHandler;

    public CalcSymDirEventApi(ICalcSymDirHandler calcSymDirHandler)
    {
        _calcSymDirHandler = calcSymDirHandler;
    }
    
    /// <inheritdoc/>
    public ProgRealPointsResponse CalcSymDir(SymDirEventRequest request)
    {
        return _calcSymDirHandler.CalculateSymDir(request);
    }
    
    
}