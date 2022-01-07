// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Runtime.InteropServices;
using E4C.be.model;

namespace E4C.be.sefacade
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
            int cal = (dateTime.gregorian) ? 1 : 0;
            return ext_swe_julday(dateTime.year, dateTime.month, dateTime.day, dateTime.ut, cal);
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
    }

        public class SePosCelPointFacade : ISePosCelPointFacade
        {
            public double[] PosCelPointFromSe(double julianDay, int seCelPointId, int flags)
            {
                string resultValue = "";
                double[] positions = new double[6];
                int returnFlag = ext_swe_calc_ut(julianDay, seCelPointId, flags, positions, resultValue);
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