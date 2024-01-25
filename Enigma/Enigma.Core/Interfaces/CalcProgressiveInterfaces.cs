// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Responses;

namespace Enigma.Core.Interfaces;


/// <summary>Timekey based on a fixed value.</summary>
public interface IFixedTimeKey
{
    /// <summary>Calculate days (including fraction) from a given arc and timekey.</summary>
    /// <param name="arc">Length of the arc.</param>
    /// <param name="keyLength">Length of the timekey.</param>
    /// <returns>The calculated days.</returns>
    public double DaysFromArc(double arc, double keyLength);

    /// <summary>Calculate arc from given numer of days and length of timekey.</summary>
    /// <param name="days">Number of days (including fraction).</param>
    /// <param name="keyLength">Length of timekey.</param>
    /// <returns>The calculated arc.</returns>
    public double ArcFromDays(double days, double keyLength);
}

/// <summary>Dynamic timekey, typically based on movement of the Sun.</summary>
public interface IPlacidusTimeKey
{
    /// <summary>Calculate arc based on the movement of the Sun in a given period.</summary>
    /// <param name="days">Number of days (including fraction) of the timepsan between radix and event.</param>
    /// <param name="jdRadix">Julian Day Number for the radix.</param>
    /// <param name="coordSys">Coordinate system. Should be ecliptic or equatorial.</param>
    /// <param name="observerPos">Observer position. SHould be geocentric or topocentric.</param>
    /// <param name="location">Location for birth.</param>
    /// <exception cref="ArgumentException">Is thrown for two conditions: observerpositions is not geocentric or topocentric, or Coordinate system is not ecliptic or equatorial.</exception>
    /// <returns>The calculated arc.</returns>
    public double ArcFromDays(double days, double jdRadix, CoordinateSystems coordSys, ObserverPositions observerPos, Location location);


    /// <summary>Calculate the days (including fraction) that are needed for the Sun to reach a position that equals
    /// the radix position plus a given arc.</summary>
    /// <param name="jdRadix">Julian Day Number for the readix.</param>
    /// <param name="arcInDegrees">The arc that needs to be added to the radix position of the Sun.</param>
    /// <param name="radixSun">Fully defined position of the Sun.</param>
    /// <param name="coordSys">Coordinate system. Should be ecliptic or equatorial.</param>
    /// <param name="observerPos">Observer position. SHould be geocentric or topocentric.</param>
    /// <param name="location">Location for birth.</param>
    /// <exception cref="ArgumentException">Is thrown for two conditions: observerpositions is not geocentric or
    /// topocentric, or Coordinate system is not ecliptic or equatorial.</exception>
    /// <returns>The calculated numer of days.</returns>
    public double DaysFromArc(double jdRadix, double arcInDegrees, FullPointPos radixSun, CoordinateSystems coordSys,
        ObserverPositions observerPos, Location location);
}



/// <summary>Calculation of solar arcs for progressive techniques.</summary>
public interface ISolarArcCalculator
{
    /// <summary>Calculate the solar arc for a startmoment and a given timespan.</summary>
    /// <param name="jdRadix">Julian Day Number for the start moment (typically the radix).</param>
    /// <param name="timespan">The timespan in days.</param>
    /// <param name="location">The location is im,portant if the flags indicate the use of parallax, otherwise it is ignored.</param>
    /// <param name="flags">Combined value for the flags toa ccess the Swiss Ephemjeris.</param>
    /// <returns>The calculated solar arc.</returns>
    public double CalcSolarArcForTimespan(double jdRadix, double timespan, Location location, int flags);
}


/// <summary>Calculator for progressive points based on real movements, e.g. transits and secondary directions.</summary>
public interface IProgRealPointCalc
{
    /// <summary>Calculates progressive positions.</summary>
    /// <param name="ayanamsha">Ayanamsha to use, 'None' for tropical calculations.</param>
    /// <param name="observerPos">Position of observer.</param>
    /// <param name="location">Location.</param>
    /// <param name="julianDayUt">Julian day number.</param>
    /// <param name="progPoints">Dictionary with supported points.</param>
    /// <returns></returns>
    public ProgRealPointsResponse CalculateTransits(Ayanamshas ayanamsha, ObserverPositions observerPos,
        Location location, double julianDayUt, Dictionary<ChartPoints, ProgPointConfigSpecs> progPoints);
}