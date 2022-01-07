// Astron

using System.Runtime.InteropServices;
using E4C.be.model;
using E4C.be.sefacade;

namespace E4C.be.astron
{
    /// <summary>
    /// Calculations for date and time functionality.
    /// </summary>
    public interface ICalendarCalc
    {
        /// <summary>
        /// Calculate Julian Day number.
        /// </summary>
        /// <param name="dateTime">Date, time and calendar.</param>
        /// <returns>The calculated and validated Julian Day number.</returns>
        public ResultForDouble CalculateJd(SimpleDateTime dateTime);
    }

    /// <summary>
    /// Calculations for obliquity and/or nutation.
    /// </summary>
    public interface IObliquityNutationCalc
    {
        /// <summary>
        /// Calculate obliquity.
        /// </summary>
        /// <param name="JulianDayUt">Julian Day for UT.</param>
        /// <param name="useTrueObliquity">True for true obliquity, flase for mean obliquity.</param>
        /// <returns>The calculated and validated obliquity.</returns>
        public ResultForDouble CalculateObliquity(double julianDayUt, bool useTrueObliquity);
    }


    public class CalendarCalc : ICalendarCalc
    {

        private readonly ISeDateTimeFacade dateTimeFacade;


        public CalendarCalc(ISeDateTimeFacade dateTimeFacade)
        {
            this.dateTimeFacade = dateTimeFacade;
        }


        public ResultForDouble CalculateJd(SimpleDateTime dateTime)
        {
            ResultForDouble result;
            try
            {
                double jdNr = dateTimeFacade.JdFromSe(dateTime);
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

    public class ObliquityNutationCalc: IObliquityNutationCalc
    {
        const int SE_ECL_NUT = -1;   // TODO move to separate class that contains constants for the SE 

        private readonly ISePosCelPointFacade posCelPointFacade;

        public ObliquityNutationCalc(ISePosCelPointFacade celPointFacade)
        {
            this.posCelPointFacade = celPointFacade;
        }

        public ResultForDouble CalculateObliquity(double julianDayUt, bool useTrueObliquity)
        {
            ResultForDouble result;
            try
            {
                int celPointId = SE_ECL_NUT;
                int flags = 0;   // todo define flags
                double[] positions = posCelPointFacade.PosCelPointFromSe(julianDayUt, celPointId, flags);
                double resultingPosition = useTrueObliquity ? positions[1] : positions[0];
                result = new ResultForDouble(resultingPosition, true);
            } 
            catch (System.Exception e)   // todo replace with specific exception for SE
            {
                result = new ResultForDouble(0.0, false, "Exception: " + e.Message);
                // TODO log exception
            }
            return result;
        }
    }

}