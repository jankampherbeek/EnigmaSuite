// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Api.Analysis;

/// <summary>API for the calculation of transits for a given event.</summary>
public interface IProgTransitsEventApi
{
    /// <summary>Calculate transits.</summary>
    /// <param name="request">Request with date, time and settings.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalcTransits(TransitsEventRequest request);
}

/// <inheritdoc/>
public class ProgTransitsEventApi(ICalcTransitsHandler handler) : IProgTransitsEventApi
{
    public ProgRealPointsResponse CalcTransits(TransitsEventRequest request)
    {
        return handler.CalculateTransits(request);
    }
}