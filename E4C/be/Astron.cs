// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.be.domain;
using E4C.be.sefacade;
using System;

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

        /// <summary>
        /// Calculate date and time (ut) from a given Julian Day Number.
        /// </summary>
        /// <param name="JulianDayNumber">Vale of the Julian Day Number.</param>
        /// <param name="calendar">Gregorian or Julian calendar.</param>
        /// <returns>Calculated date and timne.</returns>
        public SimpleDateTime CalculateDateTimeFromJd(double JulianDayNumber, Calendars calendar);

        /// <summary>
        /// Checks date and time for correctness. Date should fit in the calendar used, taking leapyears into account. Time should be >= 0.0 and < 24.0.
        /// </summary>
        /// <param name="dateTime">Instance of SimpleDateTime to check</param>
        /// <returns>true if date and time are both correct, otherwise false.</returns>
        public bool ValidDateAndtime(SimpleDateTime dateTime);

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
        /// <param name="useTrueObliquity">True for true obliquity, false for mean obliquity.</param>
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

        public SimpleDateTime CalculateDateTimeFromJd(double JulianDayNumber, Calendars calendar)
        {
            SimpleDateTime Result;
            try
            {
                Result = dateTimeFacade.DateTimeFromJd(JulianDayNumber, calendar);
            }
            catch (System.Exception e)
            {
                // todo handle exception, write to log-file
                Console.WriteLine("Error to log in CalendarCalc.CalculateDateTimeFromJd: " + e.Message);
                Result = new SimpleDateTime(0, 0, 0, 0.0, calendar);
            }
            return Result;
        }

        public bool ValidDateAndtime(SimpleDateTime dateTime)
        {
            return dateTimeFacade.DateTimeIsValid(dateTime);
        }
    }

    public class ObliquityNutationCalc : IObliquityNutationCalc
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
                // todo handle exception, write to log-file
                Console.WriteLine("Error to log in CalendarCalc.CalculateObliquity: " + e.Message);
            }
            return result;
        }
    }

}