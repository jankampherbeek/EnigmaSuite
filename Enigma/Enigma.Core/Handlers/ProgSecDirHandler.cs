// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Core.Handlers;

/// <inheritdoc/>
public sealed class ProgSecDirHandler: ICalcSecDirHandler
{
    private readonly IProgRealPointCalc _progRealPointCalc;

    public ProgSecDirHandler(IProgRealPointCalc progRealPointCalc)
    {
        _progRealPointCalc = progRealPointCalc;
    }
    
    /// <inheritdoc/>
    public ProgRealPointsResponse CalculateSecDir(SecDirEventRequest request)
    {
        double secJd = DefineJdForKey(request.JdRadix, request.JdEvent);
        return _progRealPointCalc.CalculateTransits(request.Ayanamsha, request.ObserverPos, request.Location,
            secJd, request.ConfigSecDir.ProgPoints);
    }

    private double DefineJdForKey(double jdRadix, double jdEvent)
    {
        double lengthInDays = jdEvent - jdRadix;
        return jdRadix + lengthInDays / EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
    }
    
}