// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.Specials;

namespace Enigma.Core.Interfaces;

/// <summary>Handler for the calculation of obliquity of the earths axis.</summary>
public interface IObliquityHandler
{
    /// <summary>Start the calculation.</summary>
    /// <param name="obliquityRequest"></param>
    /// <returns></returns>
    public double CalcObliquity(ObliquityRequest obliquityRequest);
}

/// <summary>Calculations for obliquity of the earths axis.</summary>
public interface IObliquityCalc
{
    /// <summary>Calculate mean or true obliquity.</summary>
    /// <param name="julianDayUt">Julian Day for UT.</param>
    /// <param name="useTrueObliquity">True for true obliquity, false for mean obliquity.</param>
    /// <returns>The calculated obliquity.</returns>
    public double CalculateObliquity(double julianDayUt, bool useTrueObliquity);
}

/// <summary>Search for positions for  aspecific ChartPoint.</summary>
public interface IPositionFinder
{
    /// <summary>Search for a position for the Sun.</summary>
    /// <param name="posToFind">The position to find in longitude or in ra.</param>
    /// <param name="startJd">The Julian Day to start the search.</param>
    /// <param name="startInterval">The initial interval in days.</param>
    /// <param name="maxMargin">The max allowed margin.</param>
    /// <param name="coordSys">Coordinate system, should be ecliptical or equatorial.</param>
    /// <param name="observerPos">Observer position, should be geocentric or topocentric.</param>
    /// <param name="location">Location, only used for topocentric positions.</param>
    /// <exception cref="ArgumentException">Thrown when a wrong coordinate system is used.</exception>
    /// <returns>The first JD when the position will occur.</returns>
    public double FindJdForPositionSun(double posToFind, double startJd, double startInterval, double maxMargin, CoordinateSystems coordSys, ObserverPositions observerPos, Location location);
}