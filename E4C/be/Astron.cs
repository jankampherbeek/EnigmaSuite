// Astron

using System.Runtime.InteropServices;
using E4C.be.model;
using E4C.be.sefacade;

namespace E4C.be.astron
{
    interface ICalendarCalc
    {
        public ResultForDouble CalculateJd(SimpleDateTime dateTime);
    }


    /// <summary>
    /// Calculations for date and time functionality.
    /// </summary>
    public class CalendarCalc : ICalendarCalc
    {

        private readonly ISeDateTimeFacade dateTimeFacade;


        public CalendarCalc(ISeDateTimeFacade dateTimeFacade)
        {
            this.dateTimeFacade = dateTimeFacade;
        }

        /// <summary>
        /// Calculate Julian Day number.
        /// </summary>
        /// <param name="dateTime">Date, time and calendar.</param>
        /// <returns>The calculated and validated Julian Day number.</returns>
        public ResultForDouble CalculateJd(SimpleDateTime dateTime)
        {
            ResultForDouble result;
            double jdNr;
            try
            {
                jdNr = dateTimeFacade.JdFromSe(dateTime);
                result = new ResultForDouble(jdNr, true);
            }
            catch (System.Exception e)
            {
                result = new ResultForDouble(0.0, false, "Exception: " + e.Message);
                // TODO log exception
            }
            return result;
        }


    }

}