// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Api;


/// <summary>API for the calculation of symbolic directions for a given event.</summary>
public interface IProgSymDirEventApi
{
    /// <summary>Calculate symbolic directions.</summary>
    /// <param name="request">Request with date, time and settings.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalcSymDir(SymDirEventRequest request);
}

/// <inheritdoc/>
public sealed class ProgSymDirEventApi: IProgSymDirEventApi
{
    private readonly ICalcSymDirHandler _calcSymDirHandler;

    public ProgSymDirEventApi(ICalcSymDirHandler calcSymDirHandler)
    {
        _calcSymDirHandler = calcSymDirHandler;
    }
    
    /// <inheritdoc/>
    public ProgRealPointsResponse CalcSymDir(SymDirEventRequest request)
    {
        return _calcSymDirHandler.CalculateSymDir(request);
    }
    
    
}