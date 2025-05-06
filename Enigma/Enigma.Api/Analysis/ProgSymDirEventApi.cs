// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Api.Analysis;


/// <summary>API for the calculation of symbolic directions for a given event.</summary>
public interface IProgSymDirEventApi
{
    /// <summary>Calculate symbolic directions.</summary>
    /// <param name="request">Request with date, time and settings.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalcSymDir(SymDirEventRequest request);
}

/// <inheritdoc/>
public sealed class ProgSymDirEventApi(ICalcSymDirHandler calcSymDirHandler) : IProgSymDirEventApi
{
    /// <inheritdoc/>
    public ProgRealPointsResponse CalcSymDir(SymDirEventRequest request)
    {
        return calcSymDirHandler.CalculateSymDir(request);
    }
    
    
}