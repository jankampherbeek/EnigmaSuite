// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.References;

namespace Enigma.Core.Calc.CelestialPoints.Helpers;

/// <inheritdoc/>
public sealed class SeFlags : ISeFlags
{
    /// <inheritdoc/>
    public int DefineFlags(CoordinateSystems coordinateSystem, ObserverPositions observerPosition, ZodiacTypes zodiacType)
    {
        int flags = 2 + 256;              // use SE + speed
        if (coordinateSystem == CoordinateSystems.Equatorial) flags += 2048;          // use equatorial positions
        if (observerPosition == ObserverPositions.TopoCentric) flags += (32 * 1024); // use topocentric position (apply parallax)
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