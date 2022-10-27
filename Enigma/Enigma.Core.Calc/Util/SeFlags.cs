// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.CalcVars;
using Enigma.Domain.Constants;

namespace Enigma.Core.Calc.Util;

/// <summary>Definitons for flags.</summary>
public interface ISeFlags
{
    /// <summary>Define flags for a given SolSysPointsRequest.</summary>
    /// <param name="coordinateSystem"/>
    /// <param name="observerPosition"/>
    /// <param name="zodiacType"/>
    /// <returns>Combined value for flags.</returns>
    public int DefineFlags(CoordinateSystems coordinateSystem, ObserverPositions observerPosition, ZodiacTypes zodiacType);
}



/// <inheritdoc/>
public class SeFlags : ISeFlags
{
    /// <inheritdoc/>
    public int DefineFlags(CoordinateSystems coordinateSystem, ObserverPositions observerPosition, ZodiacTypes zodiacType)
    {
        int _flags = EnigmaConstants.SEFLG_SWIEPH | EnigmaConstants.SEFLG_SPEED;
        if (coordinateSystem == CoordinateSystems.Equatorial) _flags |= EnigmaConstants.SEFLG_EQUATORIAL;
        if (observerPosition == ObserverPositions.HelioCentric) _flags |= EnigmaConstants.SEFLG_HELCTR;
        if (observerPosition == ObserverPositions.TopoCentric) _flags |= EnigmaConstants.SEFLG_TOPOCTR;
        if (zodiacType == ZodiacTypes.Sidereal) _flags |= EnigmaConstants.SEFLG_SIDEREAL;
        return _flags;
    }
}

