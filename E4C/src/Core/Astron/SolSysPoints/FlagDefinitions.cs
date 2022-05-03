// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Shared.Constants;
using E4C.Shared.References;

namespace E4C.Core.Astron.SolSysPoints;

/// <summary>Definitons for flags.</summary>
public interface IFlagDefinitions
{
    /// <summary>Define flags for a given SolSysPointsRequest.</summary>
    /// <param name="coordinateSystem"/>
    /// <param name="observerPosition"/>
    /// <param name="zodiacType"/>
    /// <returns>Combined value for flags.</returns>
    public int DefineFlags(CoordinateSystems coordinateSystem, ObserverPositions observerPosition, ZodiacTypes zodiacType);
}



/// <inheritdoc/>
public class FlagDefinitions : IFlagDefinitions
{
    /// <inheritdoc/>
    public int DefineFlags(CoordinateSystems coordinateSystem, ObserverPositions observerPosition, ZodiacTypes zodiacType)
    {
        int _flags = Constants.SEFLG_SWIEPH | Constants.SEFLG_SPEED;
        if (coordinateSystem == CoordinateSystems.Equatorial) _flags |= Constants.SEFLG_EQUATORIAL;
        if (observerPosition == ObserverPositions.HelioCentric) _flags |= Constants.SEFLG_HELCTR;
        if (observerPosition == ObserverPositions.TopoCentric) _flags |= Constants.SEFLG_TOPOCTR;
        if (zodiacType == ZodiacTypes.Sidereal) _flags |= Constants.SEFLG_SIDEREAL;
        return _flags;
    }
}

