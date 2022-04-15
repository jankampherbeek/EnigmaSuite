// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using E4C.core.facades;
using E4C.Models.Domain;
using E4C.core.facades;
using System;
using E4C.core.shared.domain;
using E4C.shared.references;

namespace E4C.Models.Astron
{
    /// <summary>
    /// Calculations for date and time functionality.
    /// </summary>
    public interface ICalendarCalc
    {


        /// <summary>
        /// Calculate date and time (ut) from a given Julian Day Number.
        /// </summary>
        /// <param name="julianDayNumber">Vale of the Julian Day Number.</param>
        /// <param name="calendar">Gregorian or Julian calendar.</param>
        /// <returns>Calculated date and timne.</returns>
        public SimpleDateTime CalculateDateTimeFromJd(double julianDayNumber, Calendars calendar);

        /// <summary>
        /// Checks date and time for correctness. Date should fit in the calendar used, taking leapyears into account. Time should be between 0.0 (inclusive) and 24.0 (exclusive).
        /// </summary>
        /// <param name="dateTime">Instance of SimpleDateTime to check</param>
        /// <returns>true if date and time are both correct, otherwise false.</returns>
        public bool ValidDateAndtime(SimpleDateTime dateTime);

    }




    public class CalendarCalc : ICalendarCalc
    {

        private readonly IDateConversionFacade _dateConversionFacade;
        private readonly IJulDayFacade _julDayFacade;
        private readonly IRevJulFacade _revJulFacade;


        public CalendarCalc(IDateConversionFacade dateConversionFacade, IJulDayFacade julDayFacade, IRevJulFacade revJulFacade)
        {
            _dateConversionFacade = dateConversionFacade;
            _julDayFacade = julDayFacade;
            _revJulFacade = revJulFacade;
        }


        public SimpleDateTime CalculateDateTimeFromJd(double julianDayNumber, Calendars calendar)
        {
            SimpleDateTime _result;
            try
            {
                _result = _revJulFacade.DateTimeFromJd(julianDayNumber, calendar);
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
            return _dateConversionFacade.DateTimeIsValid(dateTime);
        }
    }





}