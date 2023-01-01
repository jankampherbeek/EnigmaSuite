// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Work.Calc.Util;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Points;

namespace Enigma.Core.Work.Calc.Interfaces;

/// <summary>
/// Calculate heliocentric rectangular positions for celestial points that are not supported by the SE.
/// </summary>
public interface ICalcHelioPos
{
    /// <param name="factorT">Fraction of century, mostly simply called 'T'.</param>
    /// <param name="orbitDefinition">Orbital elements to calculate the position.</param>
    /// <returns>Calculated rectangualr coördinates.</returns>
    public RectAngCoordinates CalcEclipticPosition(double factorT, OrbitDefinition orbitDefinition);
}


/// <summary>Calculations for celestial points.</summary>
public interface ICelPointSECalc
{
    /// <summary>Calculate a single celestial point.</summary>
    /// <param name="celPoint">The celestial point that will be calculated.</param>
    /// <param name="jdnr">The Julian day number.</param>
    /// <param name="location">Location with coordinates.</param>
    /// <param name="flags">Flags that contain the settings for ecliptic or equatorial based calculations.</param>
    /// <returns>Array with position and speed for mainposition, deviation and distance, in that sequence. Typically: longitude, latitude, distance or right ascension, declination and distance.</returns>
    public PosSpeed[] CalculateCelPoint(CelPoints celPoint, double jdnr, Location location, int flags);
}

/// <summary>
/// Calculate geocentric ecliptical position for celestial points that are not supported by the SE.
/// </summary>
public interface ICelPointsElementsCalc
{
    /// <param name="planet">Currently supported hypothetical planets: Persephone, Hermes, Demeter (School of Ram).</param>
    /// <param name="jdUt">Julian day for UT.</param>
    /// <returns>Array with longitude, latitude and distance in that sequence.</returns>
    public double[] Calculate(CelPoints planet, double jdUt);
}