﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;

namespace Enigma.Core.Interfaces;

/// <summary>Handle calculation for a full chart with all positions.</summary>
public interface IChartAllPositionsHandler
{
    public Dictionary<ChartPoints, FullPointPos> CalcFullChart(CelPointsRequest request);
}


/// <summary>Start calculations for oblique longitude.</summary>
/// <remarks>Oblique longitude is the 'True place' according to the School of Ram.</remarks>
public interface IObliqueLongitudeHandler
{
    /// <summary>Calculate oblique longitude for one point.</summary>
    /// <param name="request"></param>
    /// <returns>Calculated positions in oblique longitude.</returns>
    public List<NamedEclipticLongitude> CalcObliqueLongitude(ObliqueLongitudeRequest request);
}


/// <summary>
/// Handler for the calculation of one or more celestial points.
/// </summary>
public interface ICelPointsHandler
{
    public Dictionary<ChartPoints, FullPointPos> CalcCommonPoints(double jdUt, double obliquity, double ayanamshaOffset, double armc, Location location, CalculationPreferences prefs);
}

/// <summary>Handler for the calculation of  range of charts for research purposes.</summary>
public interface ICalcChartsRangeHandler
{
    /// <summary>Calculate a range of charts.</summary>
    /// <param name="request">Request with the data and the settings.</param>
    /// <returns>The calculated result.</returns>
    public List<FullChartForResearchItem> CalculateRange(ChartsRangeRequest request);
}


/// <summary>
/// Calculate heliocentric rectangular positions for celestial points that are not supported by the CommonSE.
/// </summary>
public interface ICalcHelioPos
{
    /// <param name="factorT">Fraction of century, mostly simply called 'T'.</param>
    /// <param name="orbitDefinition">Orbital elements to calculate the position.</param>
    /// <returns>Calculated rectangualr coördinates.</returns>
    public RectAngCoordinates CalcEclipticPosition(double factorT, OrbitDefinition orbitDefinition);
}


/// <summary>Calculations for celestial points.</summary>
public interface ICelPointSeCalc
{
    /// <summary>Calculate a single celestial point.</summary>
    /// <param name="celPoint">The celestial point that will be calculated.</param>
    /// <param name="jdnr">The Julian day number.</param>
    /// <param name="location">Location with coordinates.</param>
    /// <param name="flags">Flags that contain the settings for ecliptic or equatorial based calculations.</param>
    /// <returns>Array with position and speed for mainposition, deviation and distance, in that sequence. Typically: longitude, latitude, distance or right ascension, declination and distance.</returns>
    public PosSpeed[] CalculateCelPoint(ChartPoints celPoint, double jdnr, Location location, int flags);
}

/// <summary>
/// Calculate geocentric ecliptical position for celestial points that are not supported by the CommonSE.
/// </summary>
public interface ICelPointsElementsCalc
{
    /// <param name="planet">Currently supported hypothetical planets: Persephone, Hermes, Demeter (School of Ram).</param>
    /// <param name="jdUt">Julian day for UT.</param>
    /// <param name="observerPosition">Geocentric/topcentric or heliocentric. No difference for topocentric because ofd the large distance of the planets involved.</param>
    /// <returns>Array with longitude, latitude and distance in that sequence.</returns>
    public double[] Calculate(ChartPoints planet, double jdUt, ObserverPositions observerPosition);
}

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

/// <summary>Calculate positions for specific zodiac points.</summary>
public interface IZodiacPointsCalc
{
    public Dictionary<ChartPoints, FullPointPos> CalculateAllZodiacalPoints(CalculationPreferences prefs, double jdUt, double obliquity, Location location);
}

/// <summary>Factory for the construction of an instance of FullPointPos.</summary>
public interface IFullPointPosFactory
{
    /// <summary>Constract instance of FullPointPos.</summary>
    /// <param name="eclipticPosSpeed">Eclitpical vlaues: longitude, latitude, distance.</param>
    /// <param name="equatorialPosSpeed">Equatorial values: right ascension, declination, distance.</param>
    /// <param name="horCoord">Horizontal coordinates: azimuth and altitude.</param>
    /// <returns>Instance of FullPointPos.</returns>
    public FullPointPos CreateFullPointPos(PosSpeed[] eclipticPosSpeed, PosSpeed[] equatorialPosSpeed, HorizontalCoordinates horCoord);
}

/// <summary>Calculator for Hellenistic lots.</summary>
public interface ILotsCalculator
{
    public Dictionary<ChartPoints, FullPointPos> CalculateAllLots(Dictionary<ChartPoints, FullPointPos> commonPositions, Dictionary<ChartPoints, FullPointPos> mundanePositions, CalculationPreferences prefs,
       double jdUt, double obliquity, Location location);
}


/// <summary>Support supported period for ChartPoints.</summary>
public interface IPeriodSupportChecker
{
    /// <summary>Checks if the calculation of a ChartPoint for a specific date is supported.</summary>
    /// <param name="chartPoint">The chart point to check.</param>
    /// <param name="jdnr">The julian day number for the given date.</param>
    /// <returns>True if the ChartPoint can be calculated for the given date, otherwise false.</returns>
    public bool IsSupported(ChartPoints chartPoint, double jdnr);
}