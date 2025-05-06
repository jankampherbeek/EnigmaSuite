// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Api.Analysis;


/// <summary>API for the calculation of secondary directions for a given event.</summary>
public interface IProgSecDirEventApi
{
    /// <summary>Calculate secondary directions.</summary>
    /// <param name="request">Request with date, time and settings.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalcSecDir(SecDirEventRequest request);
}

/// <inheritdoc/>
public sealed class ProgSecDirEventApi(ICalcSecDirHandler handler) : IProgSecDirEventApi
{
    /// <inheritdoc/>
    public ProgRealPointsResponse CalcSecDir(SecDirEventRequest request)
    {
        return handler.CalculateSecDir(request);
    }
}