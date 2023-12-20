// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Core.Handlers;

/// <inheritdoc/>
public sealed class ProgTransitsHandler: ICalcTransitsHandler
{
    private readonly IProgRealPointCalc _progRealPointCalc;
    
    public ProgTransitsHandler(IProgRealPointCalc progRealPointCalc)
    {
        _progRealPointCalc = progRealPointCalc;
    }

    /// <inheritdoc/>
    public ProgRealPointsResponse CalculateTransits(TransitsEventRequest request)
    {
        return _progRealPointCalc.CalculateTransits(request.Ayanamsha, request.ObserverPos, request.Location,
            request.JulianDayUt, request.ConfigTransits.ProgPoints);
    }
    
}