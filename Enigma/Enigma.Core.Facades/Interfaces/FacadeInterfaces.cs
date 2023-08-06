// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;

namespace Enigma.Facades.Interfaces;


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
    /// <param name="equCoordinates">Equatorial coordinates: ra, declination and distance, in that sequence.</param>
    /// <param name="flags">Combined values that contain settings.</param>
    /// <returns>Array with azimuth and altitude in that sequence.</returns>
    public double[] RetrieveHorizontalCoordinates(double julianDayUt, double[] geoGraphicCoordinates, double[] equCoordinates, int flags);
}


/// <summary>Facade for the calculation of the positions of celestial points (planets, nodes etc.).</summary> 
/// <remarks>Enables accessing the CommonSE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface ICalcUtFacade
{
    /// <summary>Retrieve positions for a celestial point.</summary>
    /// <remarks>Calls the function ext_swe_calc_ut from the CommonSE.</remarks>
    /// <param name="julianDay">Julian day calculated for UT.</param>
    /// <param name="seCelPointId">Identifier for the celestial point as used by the CommonSE.</param>
    /// <param name="flags">Combined value for flags to define the desired calculation.</param>
    /// <returns>Array with 6 positions, subsequently: longitude, latitude, distance, longitude speed, latitude speed and distance speed.</returns>
    public double[] PositionFromSe(double julianDay, int seCelPointId, int flags);
}

/// <summary>Facade for the conversion between ecliptic and equatorial coordinates.</summary>
/// <remarks>Enables accessing the CommonSE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface ICoTransFacade
{
    /// <summary>Convert ecliptic to equatorial coordinates.</summary>
    /// <remarks>Calls the function ext_swe_cotrans from the CommonSE.</remarks>/// 
    /// <param name="eclipticCoordinates">Array with subsequently longitude and latitude.</param>
    /// <param name="obliquity"/>
    /// <returns>Array with subsequently right ascension and declination.</returns>
    public double[] EclipticToEquatorial(double[] eclipticCoordinates, double obliquity);
}


/// <summary>Facade for date/time conversion functionality in the Swiss Ephemeris.</summary>
/// <remarks>Enables accessing the CommonSE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface IDateConversionFacade
{
    /// <summary>Checks if a date and time are valid.</summary>
    /// <param name="dateTime"/>
    /// <returns>True if date is a valid date and ut between 0.0 (inclusive) and 24.0 (exclusive).</returns>
    public bool DateTimeIsValid(SimpleDateTime dateTime);
}



/// <summary>Facade for retrieving Julian Day number from date and time, using the Swiss Ephemeris.</summary>
/// <remarks>Enables accessing the CommonSE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface IJulDayFacade
{
    /// <summary>Retrieve Julian Day number from Swiss Ephemeris.</summary>
    /// <param name="dateTime">Date, time and calendar.</param>
    /// <returns>The calculated Julian Day number.</returns>
    public double JdFromSe(SimpleDateTime dateTime);

    /// <summary>Retrieve value for Delta T.</summary>
    /// <param name="julianDayUt">Value for Julian Day in UT.</param>
    /// <returns>The value for Delta T in seconds and fractions of seconds.</returns>
    public double DeltaTFromSe(double julianDayUt);
}


/// <summary>Facade for retrieving date and time from a Julian Day number, using the Swiss Ephemeris.</summary>
/// <remarks>Enables accessing the CommonSE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface IRevJulFacade
{

    /// <summary>Retrieve date and time (UT) from a given Julian Day Number.</summary>
    /// <param name="julianDayNumber"/>
    /// <param name="calendar">Gregorian or Julian calendar.</param>
    /// <returns>An instance of SimpleDateTime that reflects the Julian Day nr and the calendar.</returns>
    public SimpleDateTime DateTimeFromJd(double julianDayNumber, Calendars calendar);
}


/// <summary>Facade for retrieving the value of hte current Ayanamsha offset.</summary>
/// <remarks>The SE must have been initialized to use a specific ayanamsha.</remarks>
public interface IAyanamshaFacade
{
    /// <summary>Calculate the Ayanamsa</summary>
    /// <remarks>Throws EnigmaException if an error occurs.</remarks>
    /// <param name="jdUt">Julian Day for UT.</param>
    /// <returns>The offseet for the ayanamsha.</returns>
    public double GetAyanamshaOffset(double jdUt);
}



