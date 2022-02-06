// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
using E4C.Models.SeFacade;
using System;

namespace E4C.Models.Astron
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
        /// <param name="julianDayNumber">Vale of the Julian Day Number.</param>
        /// <param name="calendar">Gregorian or Julian calendar.</param>
        /// <returns>Calculated date and timne.</returns>
        public SimpleDateTime CalculateDateTimeFromJd(double julianDayNumber, Calendars calendar);

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

        private readonly ISeDateTimeFacade _dateTimeFacade;


        public CalendarCalc(ISeDateTimeFacade dateTimeFacade)
        {
            _dateTimeFacade = dateTimeFacade;
        }


        public ResultForDouble CalculateJd(SimpleDateTime dateTime)
        {
            ResultForDouble _result;
            try
            {
                double _jdNr = _dateTimeFacade.JdFromSe(dateTime);
                _result = new ResultForDouble(_jdNr, true);
            }
            catch (System.Exception e)
            {
                _result = new ResultForDouble(0.0, false, "Exception: " + e.Message);
                // TODO log exception
            }
            return _result;
        }

        public SimpleDateTime CalculateDateTimeFromJd(double julianDayNumber, Calendars calendar)
        {
            SimpleDateTime _result;
            try
            {
                _result = _dateTimeFacade.DateTimeFromJd(julianDayNumber, calendar);
            }
            catch (Exception e)
            {
                // todo handle exception, write to log-file
                Console.WriteLine("Error to log in CalendarCalc.CalculateDateTimeFromJd: " + e.Message);
                _result = new SimpleDateTime(0, 0, 0, 0.0, calendar);
            }
            return _result;
        }

        public bool ValidDateAndtime(SimpleDateTime dateTime)
        {
            return _dateTimeFacade.DateTimeIsValid(dateTime);
        }
    }

    public class ObliquityNutationCalc : IObliquityNutationCalc
    {
        const int SE_ECL_NUT = -1;   // TODO move to separate class that contains constants for the SE 

        private readonly ISePosCelPointFacade _posCelPointFacade;

        public ObliquityNutationCalc(ISePosCelPointFacade celPointFacade)
        {
            _posCelPointFacade = celPointFacade;
        }

        public ResultForDouble CalculateObliquity(double julianDayUt, bool useTrueObliquity)
        {
            ResultForDouble _result;
            try
            {
                int _celPointId = SE_ECL_NUT;
                int _flags = 0;   // todo define flags
                double[] _positions = _posCelPointFacade.PosCelPointFromSe(julianDayUt, _celPointId, _flags);
                double _resultingPosition = useTrueObliquity ? _positions[1] : _positions[0];
                _result = new ResultForDouble(_resultingPosition, true);
            }
            catch (System.Exception e)   // todo replace with specific exception for SE
            {
                _result = new ResultForDouble(0.0, false, "Exception: " + e.Message);
                // todo handle exception, write to log-file
                Console.WriteLine("Error to log in CalendarCalc.CalculateObliquity: " + e.Message);
            }

            return _result;
        }
    }


    public class PositionSolSysPointCalc
    {
        private readonly ISePosCelPointFacade _posCelPointFacade;

        public PositionSolSysPointCalc(ISePosCelPointFacade posCelPointFacade)
        {
            _posCelPointFacade = posCelPointFacade;
        }


        public void CalculateSolSysPoint(int pointId, double jdnr, int flags)
        {
            double[] _positions = _posCelPointFacade.PosCelPointFromSe(jdnr, pointId, flags);

        }
    }

}