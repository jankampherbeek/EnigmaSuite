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