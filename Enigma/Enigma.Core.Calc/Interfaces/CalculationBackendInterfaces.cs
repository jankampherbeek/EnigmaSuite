// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.CalcDomain;
using Enigma.Core.Calc.ReqResp;
using Enigma.Core.Calc.Util;
using Enigma.Domain.CalcVars;
using Enigma.Domain.DateTime;
using Enigma.Domain.Locational;
using Enigma.Domain.Positional;

namespace Enigma.Core.Calc.Interfaces;

/// <summary>Handle calculation for a full chart with all positions.</summary>
public interface IChartAllPositionsHandler
{
    public ChartAllPositionsResponse CalcFullChart(ChartAllPositionsRequest request);
}


/// <summary>Convert ecliptical longitude and latitude to equatorial right ascension and declination.</summary>
public interface ICoordinateConversionCalc
{
    public EquatorialCoordinates PerformConversion(EclipticCoordinates eclCoord, double obliquity);

}


/// <summary>Handles the conversion from ecliptical to equatorial coordinates.</summary>
public interface ICoordinateConversionHandler
{
    public CoordinateConversionResponse HandleConversion(CoordinateConversionRequest request);
}

public interface ICheckDateTimeHandler
{
    public CheckDateTimeResponse CheckDateTime(CheckDateTimeRequest request);
}

/// <summary>Calculations for Julian Day.</summary>
public interface ICheckDateTimeValidator
{
    public bool ValidateDateTime(SimpleDateTime dateTime);
}

/// <summary>Calculations for Julian Day.</summary>
public interface IDateTimeCalc
{
    public SimpleDateTime CalcDateTime(double julDay, Calendars calendar);
}

public interface IDateTimeHandler
{
    public DateTimeResponse CalcDateTime(DateTimeRequest request);
}

/// <summary>Calculations for Julian Day.</summary>
public interface IJulDayCalc
{
    /// <summary>Calculate Julian Day for Universal Time.</summary>
    /// <param name="dateTime"/>
    /// <returns>Calculated JD for UT.</returns>
    public double CalcJulDayUt(SimpleDateTime dateTime);

    /// <summary>Calculate Delta T</summary>
    /// <param name="juldayUt">Julian Day for UT.</param>
    /// <returns>The value for delta t in seconds and fractions of seconds.</returns>
    public double CalcDeltaT(double juldayUt);
}

public interface IJulDayHandler
{
    public JulianDayResponse CalcJulDay(JulianDayRequest request);
}

public interface IHorizontalCalc
{
    public double[] CalculateHorizontal(double jdUt, Location location, EclipticCoordinates eclipticCoordinates, int flags);
}

public interface IHorizontalHandler
{
    public HorizontalResponse CalcHorizontal(HorizontalRequest request);
}

/// <summary>Calculations for houses and other mundane positions.</summary>
public interface IHousesCalc
{
    /// <summary>Calculate longitude for houses and other mundane positions.</summary>
    /// <param name="julianDayUt">Julian Day for UT.</param>
    /// <param name="obliquity"/>
    /// <param name="location"/>
    /// <param name="houseSystemId">Id for a housesystem as used by the SE.</param>
    /// <param name="flags"/>
    /// <returns>The calculated positions for the houses and other mundane points.</returns>
    public double[][] CalculateHouses(double julianDayUt, double obliquity, Location location, char houseSystemId, int flags);
}


public interface IHousesHandler
{
    public FullHousesPosResponse CalcHouses(FullHousesPosRequest request);
}


/// <summary>Calculator for oblique longitudes (School of Ram).</summary>
public interface IObliqueLongitudeCalculator
{
    /// <summary>Perform calculations to obtain oblique longitudes.</summary>
    /// <param name="request">Specifications for the calculation.</param>
    /// <returns>Solar System Points with the oblique longitude.</returns>
    public List<NamedEclipticLongitude> CalcObliqueLongitudes(ObliqueLongitudeRequest request);
}


public interface IObliqueLongitudeHandler
{
    public ObliqueLongitudeResponse CalcObliqueLongitude(ObliqueLongitudeRequest request);
}

/// <summary>Calculations for the south-point.</summary>
public interface ISouthPointCalculator
{
    /// <summary>Calculate longitude and latitude for the south-point.</summary>
    /// <param name="armc">Right ascension for the MC.</param>
    /// <param name="obliquity">Obliquity of the earths axis.</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <returns>An instance of EclipticCoodinates with values for longitude and latitude.</returns>
    public EclipticCoordinates CalculateSouthPoint(double armc, double obliquity, double geoLat);
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


public interface IObliquityHandler
{
    ObliquityResponse CalcObliquity(ObliquityRequest obliquityRequest);
}

/// <summary>
/// Calculation for horizontal coordinates: azimuth and altitude.
/// </summary>
public interface IAzAltFacade
{
    /// <summary>
    /// Calculate azimuth and altitude.
    /// </summary>
    /// <remarks>
    /// Assumes zero for atmospheric pressure and temperature. 
    /// </remarks>
    /// <param name="julianDayUt">Julian day in universal time.</param>
    /// <param name="geoGraphicCoordinates">Geographic coordinates: gepgraphic longitude, geographic latitude and height (meters), in that sequence.</param>
    /// <param name="eclipticCoordinates">Ecliptic coordinates: longitude, latitude and distance, in that sequence.</param>
    /// <param name="flags">Combined values that contain settings.</param>
    /// <returns>Array with azimuth and altitude in that seqwuence.</returns>
    public double[] RetrieveHorizontalCoordinates(double julianDayUt, double[] geoGraphicCoordinates, double[] eclipticCoordinates, int flags);
}


/// <summary>Facade for the calculation of the positions of celestial points (planets, nodes etc.).</summary> 
/// <remarks>Enables accessing the SE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface ICalcUtFacade
{
    /// <summary>Retrieve positions for a celestial point.</summary>
    /// <remarks>Calls the function ext_swe_calc_ut from the SE.</remarks>
    /// <param name="julianDay">Julian day calculated for UT.</param>
    /// <param name="seCelPointId">Identifier for the celestial point as used by the SE.</param>
    /// <param name="flags">Combined value for flags to define the desired calculation.</param>
    /// <returns>Array with 6 positions, subsequently: longitude, latitude, distance, longitude speed, latitude speed and distance speed.</returns>
    public double[] PosCelPointFromSe(double julianDay, int seCelPointId, int flags);
}

/// <summary>Facade for the conversion between ecliptic and equatorial coordinates.</summary>
/// <remarks>Enables accessing the SE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface ICoTransFacade
{
    /// <summary>Convert ecliptic to equatorial coordinates.</summary>
    /// <remarks>Calls the function ext_swe_cotrans from the SE.</remarks>/// 
    /// <param name="eclipticCoordinates">Array with subsequently longitude and latitude.</param>
    /// <param name="obliquity"/>
    /// <returns>Array with subsequently right ascension and declination.</returns>
    public double[] EclipticToEquatorial(double[] eclipticCoordinates, double obliquity);
}


/// <summary>Facade for date/time conversion functionality in the Swiss Ephemeris.</summary>
/// <remarks>Enables accessing the SE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface IDateConversionFacade
{
    /// <summary>Checks if a date and time are valid.</summary>
    /// <param name="dateTime"/>
    /// <returns>True if date is a valid date and ut between 0.0 (inclusive) and 24.0 (exclusive).</returns>
    public bool DateTimeIsValid(SimpleDateTime dateTime);
}

/// <summary>
/// Facade for the calculation of mundane points (housecusps, vertex etc.).
/// </summary>
public interface IHousesFacade
{
    /// <summary>
    /// Retrieve positions for house cusps and other mundane points.
    /// </summary>
    /// <param name="jdUt">Julian Day for UT.</param>
    /// <param name="flags">0 for tropical, 0 or SEFLG_SIDEREAL for sidereal (logical or).</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <param name="geoLon">Geographic longitude.</param>
    /// <param name="houseSystem">Indication for the house system within the Swiss Ephemeris.</param>
    /// <returns>A two dimensional array. The first array contains the cusps, starting from position 1 (position 0 is empty) and ordered by number. 
    /// The length is 13 (for systems with 12 cusps) or 37 (for Gauquelin houses (houseSystem 'G'), which have 36 cusps).
    /// The second array contains 10 positions with the following content:
    /// 0: = Ascendant, 1: MC, 2: ARMC, 3: Vertex, 4: equatorial ascendant( East point), 5: co-ascendant (Koch), 6: co-ascendant (Munkasey), 7: polar ascendant (Munkasey). 
    /// Positions 8 and 9 are empty.
    /// </returns>
    public double[][] RetrieveHouses(double jdUt, int flags, double geoLat, double geoLon, char houseSystem);
}


/// <summary>Facade for retrieving Julian Day number from date and time, using the Swiss Ephemeris.</summary>
/// <remarks>Enables accessing the SE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface IJulDayFacade
{
    /// <summary>Retrieve Julian Day number from Swiss Ephemeris.</summary>
    /// <param name="dateTime">Date, time and calendar.</param>
    /// <returns>The calculated Julian Day number.</returns>
    public double JdFromSe(SimpleDateTime dateTime);

    /// <summary>Retrieve value for Delta T.</summary>
    /// <param name="JulianDayUT">Value for Julian Day in UT.</param>
    /// <returns>The value for Delta T in seconds and fractions of seconds.</returns>
    public double DeltaTFromSe(double JulianDayUT);
}


/// <summary>Facade for retrieving date and time from a Julian Day number, using the Swiss Ephemeris.</summary>
/// <remarks>Enables accessing the SE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface IRevJulFacade
{

    /// <summary>Retrieve date and time (UT) from a given Julian Day Number.</summary>
    /// <param name="julianDayNumber"/>
    /// <param name="calendar">Gregorian or Julian calendar.</param>
    /// <returns>An instance of SimpleDateTime that reflects the Julian Day nr and the calendar.</returns>
    public SimpleDateTime DateTimeFromJd(double julianDayNumber, Calendars calendar);
}


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


/// <summary>Calculations for Solar System points.</summary>
public interface ISolSysPointSECalc
{
    /// <summary>Calculate a single Solar System point.</summary>
    /// <param name="solarSystemPoint">The Solar System point that will be calcualted.</param>
    /// <param name="jdnr">The Julian day number.</param>
    /// <param name="location">Location with coordinates.</param>
    /// <param name="flags">Flags that contain the settings for ecliptic or equatorial based calculations.</param>
    /// <returns>Array with position and speed for mainposition, deviation and distance, in that sequence. Typically: longitude, latitude, distance or right ascension, declination and distance.</returns>
    public PosSpeed[] CalculateSolSysPoint(SolarSystemPoints solarSystemPoint, double jdnr, Location location, int flags);
}

/// <summary>
/// Calculate geocentric ecliptical position for celestial points that are not supported by the SE.
/// </summary>
public interface ISolSysPointsElementsCalc
{
    /// <param name="planet">Currently supported hypothetical planets: Persephone, Hermes, Demeter (School of Ram).</param>
    /// <param name="jdUt">Julian day for UT.</param>
    /// <returns>Array with longitude, latitude and distance in that sequence.</returns>
    public double[] Calculate(SolarSystemPoints planet, double jdUt);
}


/// <summary>
/// Handler for the calculation of one or more solar system points.
/// </summary>
public interface ISolSysPointsHandler
{

    public SolSysPointsResponse CalcSolSysPoints(SolSysPointsRequest request);
}


