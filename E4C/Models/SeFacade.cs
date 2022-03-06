// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
using System;
using System.Runtime.InteropServices;

namespace E4C.Models.SeFacade
{
    /// <summary>
    /// Facade for date/time functionality in the Swiss Ephemeris.
    /// </summary>
    /// <remarks>
    /// Enables accessing the SE dll. Passes any result without checking, exceptions are automatically propagated. 
    /// </remarks>
    public interface ISeDateTimeFacade
    {
        /// <summary>
        /// Retrieve Julian Day number from Swiss Ephemeris.
        /// </summary>
        /// <param name="dateTime">Date, time and calendar.</param>
        /// <returns>The calculated and validated Julian Day number.</returns>
        public double JdFromSe(SimpleDateTime dateTime);

        /// <summary>
        /// Retrieve date and time (UT) from a given Julian Day Number.
        /// </summary>
        /// <param name="julianDayNumber">Value for JUlian Day Number.</param>
        /// <param name="calendar">Gregorian or Julian calendar.</param>
        /// <returns>An instance of SimpleDateTime.</returns>
        public SimpleDateTime DateTimeFromJd(double julianDayNumber, Calendars calendar);

        /// <summary>
        /// Checks if a date and time are valid.
        /// </summary>
        /// <param name="dateTime">Instance of SimpleDateTime.</param>
        /// <returns>True if date is a valid date and 0.0 <= ut < 24.0.</returns>
        public bool DateTimeIsValid(SimpleDateTime dateTime);

    }

    /// <summary>
    /// Facade for the calculation of the positions of celestial points (planets, nodes etc.)
    /// </summary> 
    public interface ISePosCelPointFacade
    {
        /// <summary>
        /// Retrieve positions for a celestial point.
        /// </summary>
        /// <param name="julianDay">Julian day calculated for UT.</param>
        /// <param name="seCelPointId">Identifier for the celestial point.</param>
        /// <param name="flags">Combined value for flags to define the desired calculation.</param>
        /// <returns>Array with 6 positions: longitude, latitude, distance, longitude speed, latitude speed and distance speed.</returns>
        public double[] PosCelPointFromSe(double julianDay, int seCelPointId, int flags);
    }

    /// <summary>
    /// Calculation for horizontal coordinates: azimuth and altitude.
    /// </summary>
    public interface IHorizontalCoordinatesFacade
    {
        /// <summary>
        /// 
        /// Calculate azimuth and altitude.
        /// </summary>
        /// <remarks>
        /// Assumes zero for atmospheric pressure and temperature.
        /// </remarks>
        /// <param name="JulianDayUt">Julian day in universal time.</param>
        /// <param name="geoGraphicCoordinates">Geographic coordinates: gepgraphic longitude, geographic latitude and height (meters), in that sequence.</param>
        /// <param name="eclipticCoordinates">Ecliptic coordinates: longitude, latitude and distance, in that sequence.</param>
        /// <param name="flags">Value for flags that contain settings.</param>
        /// <returns>Instance of HorizontalPos with azimuth and true altitude.</returns>
        public HorizontalPos CalculateHorizontalCoordinates(double JulianDayUt, double[] geoGraphicCoordinates, double[] eclipticCoordinates, int flags);
    }

    /// <summary>
    /// Facade for the conversion between ecliptic and equatorial coordinates.
    /// </summary>
    public interface ICoordinateConversionFacade
    {
        /// <summary>
        /// Convert ecliptic to equaotrial coordinates.
        /// </summary>
        /// <param name="eclipticCoordinates">Array with subsequently longitudea and latitude.</param>
        /// <param name="obliquity">Obliquity.</param>
        /// <returns>Array with subsequently right ascension and declination.</returns>
        public double[] EclipticToEquatorial(double[] eclipticCoordinates, double obliquity);
    }

    /// <summary>
    /// Facade for the calculation of mundane points (housecusps, vertex etc.).
    /// </summary>
    public interface ISePosHousesFacade
    {
        /// <summary>
        /// Retrieve positions for house cusps and other mundane points.
        /// </summary>
        /// <param name="jdUt">Julian Day for UT.</param>
        /// <param name="flags">0 for tropical, 0 or SEFLG_SIDEREAL for sidereal.</param>
        /// <param name="geoLat">Geographic latitude.</param>
        /// <param name="geoLon">Geographic longitude.</param>
        /// <param name="houseSystem">Indication for the house system within the Swiss Ephemeris.</param>
        /// <returns>A two dimensional array. The first array contains the cusps, starting from position 1 (position 0 is empty) and ordered by number. 
        /// The length is 13 (for systems with 12 cusps) or 37 (for Gauquelin houses, which have 36 cusps).
        /// The second array contains 10 positions with the following content:
        /// 0: = Ascendant, 1: MC, 2: ARMC, 3: Vertex, 4: equatorial ascendant( East point), 5: co-ascendant (Koch), 6: co-ascendant (Munkasey), 7: polar ascendant (Munkasey). 
        /// Positions 8 and 9 are empty.
        /// </returns>
        public double[][] PosHousesFromSe(double jdUt, int flags, double geoLat, double geoLon, char houseSystem);
    }


    public class SeDateTimeFacade : ISeDateTimeFacade
    {

        public double JdFromSe(SimpleDateTime dateTime)
        {
            int _cal = (dateTime.Calendar == Calendars.Gregorian) ? 1 : 0;
            return ext_swe_julday(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Ut, _cal);
        }

        public SimpleDateTime DateTimeFromJd(double julianDayNumber, Calendars calendar)
        {
            int _calId = (calendar == Calendars.Gregorian) ? 1 : 0;
            int _year = 0;
            int _month = 0;
            int _day = 0;
            double _ut = 0.0;
            try
            {
                double result = ext_swe_revjul(julianDayNumber, _calId, ref _year, ref _month, ref _day, ref _ut);

            }
            catch (Exception e)
            {
                // todo throw new exception
                // todo handle exception, write to log-file
                Console.WriteLine("Error to log in SeDAteTimeFacade.DateTimeFromJd: " + e.Message);
                return new SimpleDateTime(0, 0, 0, 0.0, Calendars.Gregorian);
            }
            return new SimpleDateTime(_year, _month, _day, _ut, calendar);
        }

        public bool DateTimeIsValid(SimpleDateTime dateTime)
        {
            double _julianDay = 0.0;
            char _calendar = dateTime.Calendar == Calendars.Gregorian ? 'g' : 'j';
            int _result = ext_swe_date_conversion(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Ut, _calendar, ref _julianDay);
            return (_result == 0) && (0.0 <= dateTime.Ut) && (dateTime.Ut < 24.0);
        }


        /// <summary>
        /// Access dll to retrieve Julian Day number.
        /// </summary>
        /// <param name="year">The astronomical year.</param>
        /// <param name="month">The month, counting from 1..12.</param>
        /// <param name="day">The number of the day.</param>
        /// <param name="hour">The hour: integer part and fraction.</param>
        /// <param name="gregflag">Type of calendar: Gregorian = 1, Julian = 0.</param>
        /// <returns>The calculated Julian Day Number.</returns>
        [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_julday")]
        private extern static double ext_swe_julday(int year, int month, int day, double hour, int gregflag);

        [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_revjul")]
        private extern static double ext_swe_revjul(double tjd, int gregflag, ref int year, ref int month, ref int day, ref double hour);

        [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_date_conversion")]
        private extern static int ext_swe_date_conversion(int year, int month, int day, double time, char calendar, ref double julianday);

    }

    public class SePosCelPointFacade : ISePosCelPointFacade
    {
        public double[] PosCelPointFromSe(double julianDay, int seCelPointId, int flags)
        {
            string _resultValue = "";
            var _positions = new double[6];

            int _returnFlag = ext_swe_calc_ut(julianDay, seCelPointId, flags, _positions, _resultValue);
            if (_returnFlag < 0) Console.WriteLine("Error to log in SePosCelPointFacade.PosCelPointFromSe. ReturnFlag : " + _returnFlag.ToString());
            // TODO check value of returnflag, if < 0 throw exception
            return _positions;
        }

        /// <summary>
        /// Access dll to retrieve position for celestial point.
        /// </summary>
        /// <param name="tjd">Julian day for UT.</param>
        /// <param name="ipl">Identifier for the celestial point.</param>
        /// <param name="iflag">Combined values for flags.</param>
        /// <param name="xx">The resulting positions.</param>
        /// <param name="serr">Error text, if any.</param>
        /// <returns>An indication if the calculation was succesfull.</returns>
        // TODO check returnvalue,  >=0 --> succesfull 
        [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_calc_ut")]
        private extern static int ext_swe_calc_ut(double tjd, int ipl, long iflag, double[] xx, string serr);
    }

    public class HorizontalCoordinatesFacade : IHorizontalCoordinatesFacade
    {
        public HorizontalPos CalculateHorizontalCoordinates(double JulianDayUt, double[] geoGraphicCoordinates, double[] eclipticCoordinates, int flags)
        {
            double[] horizontalCoordinates = new double[3];
            int result = ext_swe_azalt(JulianDayUt, flags, geoGraphicCoordinates, 0, 0, eclipticCoordinates, horizontalCoordinates);
            // TODO check result
            return new HorizontalPos(horizontalCoordinates[0], horizontalCoordinates[1]);
        }

        /// <summary>
        /// Access dll to retrieve horizontal positions.
        /// </summary>
        /// <param name="tjd">Julian day for UT.</param>
        /// <param name="iflag">Flag: always SE_ECL2HOR = 0.</param>
        /// <param name="geoCoordinates">Geographic longitude, altitude and height above sea (ignored for real altitude).</param>
        /// <param name="atPress">Atmospheric pressure in mbar, ignored for true altitude.</param>
        /// <param name="atTemp">Atmospheric temperature in degrees Celsius, ignored for true altitude.</param>
        /// <param name="eclipticCoordinates">Ecliptic longitude, latitude and distance.</param>
        /// <param name="horizontalCoordinates">Resulting values for azimuth, true altitude and apparent altitude.</param>
        /// <returns>An indication if the calculation was succesfull.</returns>
        // TODO check returnvalue,  >=0 --> succesfull  
        [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_azalt")]
        private extern static int ext_swe_azalt(double tjd, long iflag, double[] geoCoordinates, double atPress, double atTemp, double[] eclipticCoordinates, double[] horizontalCoordinates);
    }

    public class CoordinateConversionFacade : ICoordinateConversionFacade
    {
        public double[] EclipticToEquatorial(double[] eclipticCoordinates, double obliquity)
        {
            double _negativeObliquity = -(Math.Abs(obliquity));
            double[] _allEclipticCoordinates = new double[] { eclipticCoordinates[0], eclipticCoordinates[1], 1.0 }; // 1.0 is placeholder for distance.
            double[] _equatorialResults = new double[3];
            int result = ext_swe_cotrans(_allEclipticCoordinates, _equatorialResults, _negativeObliquity);
            /// todo check return value
            return _equatorialResults;
        }

        [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_cotrans")]
        private extern static int ext_swe_cotrans(double[] allEclipticCoordinates, double[] equatorialResults, double negativeObliquity);
    }

    public class SePosHousesFacade : ISePosHousesFacade
    {
        public double[][] PosHousesFromSe(double jdUt, int flags, double geoLat, double geoLon, char houseSystem)
        {
            int _nrOfCusps = houseSystem == 'G' ? 37 : 13;
            double[] _cusps = new double[_nrOfCusps];
            double[] _mundanePoints = new double[6];
            int _returnFlag = ext_swe_houses_ex(jdUt, flags, geoLat, geoLon, houseSystem, _cusps, _mundanePoints);
            // todo check returnflag
            double[][] result = { _cusps, _mundanePoints };
            return result;

        }
        [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_houses_ex")]
        private extern static int ext_swe_houses_ex(double tjdut, int flags, double geolat, double geolon, char hsys, double[] hcusp0, double[] ascmc0);

    }

    public class SeInitializer
    {

        /// <summary>
        /// Set location for Swiss Ephemeris files.
        /// </summary>
        /// <param name="path">Location, relative to the program.</param>
        public static void SetEphePath(String path)
        {
            ext_swe_set_ephe_path(path);
        }
        [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_set_ephe_path")]
        private extern static void ext_swe_set_ephe_path(String path);


    }



}