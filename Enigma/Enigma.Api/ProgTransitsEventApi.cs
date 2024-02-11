// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Api;

/// <summary>API for the calculation of transits for a given event.</summary>
public interface IProgTransitsEventApi
{
    /// <summary>Calculate transits.</summary>
    /// <param name="request">Request with date, time and settings.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalcTransits(TransitsEventRequest request);
}

/// <inheritdoc/>
public class ProgTransitsEventApi: IProgTransitsEventApi
{

    private readonly ICalcTransitsHandler _handler;

    public ProgTransitsEventApi(ICalcTransitsHandler handler)
    {
        _handler = handler;
    }
    public ProgRealPointsResponse CalcTransits(TransitsEventRequest request)
    {
        return _handler.CalculateTransits(request);
    }
}