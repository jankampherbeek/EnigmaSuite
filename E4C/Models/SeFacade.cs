// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
using System;
using System.Runtime.InteropServices;

namespace E4C.Models.SeFacade
{

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
        /// Retrive date and time (UT) from a given Julian Day Number.
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
            double[] _positions = new double[6];
            /*
            // temporary benchmark
            // initialize ephemeris


            SeInitializer.setEphePath(".//se");

            double jd = 1500000.0;
            string StartTime = DateTime.Now.ToString("h:mm:ss tt");
            for (int i = 0; i < 1000000; i++)
            {
                jd = jd + 1.0;
                ext_swe_calc_ut(jd, 1, 2, positions, resultValue);
            }
            string EndTime = DateTime.Now.ToString("h:mm:ss tt");


            // end temporar4y benchmark
            */




            int _returnFlag = ext_swe_calc_ut(julianDay, seCelPointId, flags, _positions, _resultValue);
            if (_returnFlag < 0) Console.WriteLine("Error to log in SePosCelPointFacade.PosCelPointFromSe. ReturnFlag : " + _returnFlag.ToString());
            // TODO check value of returnflag, if < 0 throw exception
            return _positions;
        }

        /// <summary>
        /// Access dll to retrieve position for celestial point.
        /// </summary>
        /// <param name="tjd">Julian day for UT</param>
        /// <param name="ipl">Identifier for the celestial point.</param>
        /// <param name="iflag">Combined values for flags.</param>
        /// <param name="xx">The resulting positions.</param>
        /// <param name="serr">Error text, if any.</param>
        /// <returns>An indication if the calculation was succesfull.</returns>
        // TODO check returnvalue,  >=0 --> succesfull 
        [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_calc_ut")]
        private extern static int ext_swe_calc_ut(double tjd, int ipl, long iflag, double[] xx, string serr);
    }





}