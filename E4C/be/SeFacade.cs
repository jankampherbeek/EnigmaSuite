// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.be.domain;
using System;
using System.Runtime.InteropServices;

namespace E4C.be.sefacade
{

    public class SeInitializer
    {

        /// <summary>
        /// Set location for Swiss Ephemeris files.
        /// </summary>
        /// <param name="path">Location, relative to the program.</param>
        public static void setEphePath(String path)
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
        /// <param name="JulianDayNumber">Value for JUlian Day Number.</param>
        /// <param name="calendar">Gregorian or Julian calendar.</param>
        /// <returns>An instance of SimpleDateTime.</returns>
        public SimpleDateTime DateTimeFromJd(double JulianDayNumber, Calendars calendar);

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
            int cal = (dateTime.calendar == Calendars.Gregorian) ? 1 : 0;
            return ext_swe_julday(dateTime.year, dateTime.month, dateTime.day, dateTime.ut, cal);
        }

        public SimpleDateTime DateTimeFromJd(double JulianDayNumber, Calendars calendar)
        {
            int calId = (calendar == Calendars.Gregorian) ? 1 : 0;
            int year = 0;
            int month = 0;
            int day = 0;
            double ut = 0.0;
            try
            {
                double result = ext_swe_revjul(JulianDayNumber, calId, ref year, ref month, ref day, ref ut);

            }
            catch (Exception e)
            {
                // todo throw new exception
                // todo handle exception, write to log-file
                Console.WriteLine("Error to log in SeDAteTimeFacade.DateTimeFromJd: " + e.Message);
                return new SimpleDateTime(0, 0, 0, 0.0, Calendars.Gregorian);
            }
            return new SimpleDateTime(year, month, day, ut, calendar);
        }

        public bool DateTimeIsValid(SimpleDateTime dateTime)
        {
            double JulianDay = 0.0;
            char calendar = dateTime.calendar == Calendars.Gregorian ? 'g' : 'j';
            int result = ext_swe_date_conversion(dateTime.year, dateTime.month, dateTime.day, dateTime.ut, calendar, ref JulianDay);
            return (result == 0) && (0.0 <= dateTime.ut) && (dateTime.ut < 24.0);
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

    


            string resultValue = "";
            double[] positions = new double[6];
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
         



            int returnFlag = ext_swe_calc_ut(julianDay, seCelPointId, flags, positions, resultValue);
            if (returnFlag < 0) Console.WriteLine("Error to log in SePosCelPointFacade.PosCelPointFromSe. ReturnFlag : " + returnFlag.ToString());
            // TODO check value of returnflag, if < 0 throw exception
            return positions;
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