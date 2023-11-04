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