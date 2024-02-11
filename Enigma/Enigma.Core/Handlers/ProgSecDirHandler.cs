// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Constants;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Core.Handlers;


/// <summary>Handles calculation of secondary directions.</summary>
public interface ICalcSecDirHandler
{
    /// <summary>Handles calculation of secondary directions for a specific event.</summary>
    /// <param name="request">Request with config items, date/time etc.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalculateSecDir(SecDirEventRequest request);
}

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