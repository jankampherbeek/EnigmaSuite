// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Api;


/// <summary>API for the calculation of secondary directions for a given event.</summary>
public interface IProgSecDirEventApi
{
    /// <summary>Calculate secondary directions.</summary>
    /// <param name="request">Request with date, time and settings.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalcSecDir(SecDirEventRequest request);
}

/// <inheritdoc/>
public sealed class ProgSecDirEventApi: IProgSecDirEventApi
{
    private readonly ICalcSecDirHandler _handler;

    public ProgSecDirEventApi(ICalcSecDirHandler handler)
    {
        _handler = handler;
    }

    /// <inheritdoc/>
    public ProgRealPointsResponse CalcSecDir(SecDirEventRequest request)
    {
        return _handler.CalculateSecDir(request);
    }
}