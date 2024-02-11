// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Calc;

/// <summary>Definitons for flags.</summary>
public interface ISeFlags
{
    /// <summary>Define flags for a given CelPointsRequest.</summary>
    /// <param name="coordinateSystem"/>
    /// <param name="observerPosition"/>
    /// <param name="zodiacType"/>
    /// <returns>Combined value for flags.</returns>
    public int DefineFlags(CoordinateSystems coordinateSystem, ObserverPositions observerPosition, ZodiacTypes zodiacType);
}

/// <inheritdoc/>
public sealed class SeFlags : ISeFlags
{
    /// <inheritdoc/>
    public int DefineFlags(CoordinateSystems coordinateSystem, ObserverPositions observerPosition, ZodiacTypes zodiacType)
    {
        int flags = 2 + 256;              // use SE + speed
        if (coordinateSystem == CoordinateSystems.Equatorial) flags += 2048;          // use equatorial positions
        if (observerPosition == ObserverPositions.TopoCentric) flags += (32 * 1024); // use topocentric position (apply parallax)
        // TODO backlog fix flags for heliocentric positions
        //case ObserverPositions.HelioCentric:
        //    flags += 8;             // use heliocentric positions
        //    break;
        
        if (zodiacType == ZodiacTypes.Sidereal && coordinateSystem == CoordinateSystems.Ecliptical)
        {
            flags += (64 * 1024);   // use sidereal zodiac
        }
        return flags;
    }
}