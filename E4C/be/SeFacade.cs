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
        [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_julday")]
        private extern static double ext_swe_julday(int year, int month, int day, double hour, int gregflag);

        [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_revjul")]
        private extern static double ext_swe_revjul(double tjd, int gregflag, ref int year, ref int month, ref int day, ref double hour);
    }


}