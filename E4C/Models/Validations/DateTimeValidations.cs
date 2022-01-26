// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Astron;
using E4C.Models.Domain;
using System;
using System.Collections.Generic;

namespace E4C.Models.Validations
{
    /// <summary>
    /// Validations for the input of date and time.
    /// </summary>
    public interface IDateTimeValidations
    {
        /// <summary>
        /// Check the validity of input for a date. 
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <param name="calendar">Gregorian or Julian calendar.</param>
        /// <param name="yearCount">Year count: BCE, CE or astronomical.</param>
        /// <returns>List with error codes.</returns>
        public List<int> ValidateDate(string year, string month, string day, Calendars calendar, YearCounts yearCount);

        /// <summary>
        /// Check the validity of input for a time.
        /// </summary>
        /// <param name="hour">The hour (0..23).</param>
        /// <param name="minute">The minute (0..59).</param>
        /// <param name="second">The seconds (0..59). Is set to 0 if empty.</param>
        /// <returns>List with error codes.</returns>
        public List<int> ValidateTime(string hour, string minute, string second);
    }


    public class DateTimeValidations : IDateTimeValidations
    {
        readonly private ICalendarCalc calendarCalc;

        public DateTimeValidations(ICalendarCalc calendarCalc)
        {
            this.calendarCalc = calendarCalc ?? throw new ArgumentNullException(nameof(calendarCalc));
        }

        public List<int> ValidateDate(string year, string month, string day, Calendars calendar, YearCounts yearCount)
        {
            List<int> errorCodes = new();
            int yearValue, monthValue, dayValue;
            try
            {
                yearValue = Int32.Parse(year);
                monthValue = Int32.Parse(month);
                dayValue = Int32.Parse(day);
            }
            catch (Exception ex)
            {
                // todo log exception
                errorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
                return errorCodes;
            }
            // Convert to astronomical yearcount if original yearcount is BCE.
            if (yearCount == YearCounts.BCE)
            {
                yearValue = -(Math.Abs(yearValue) + 1);
            }
            SimpleDateTime simpleDateTime = new(yearValue, monthValue, dayValue, 0.0, calendar);
            if (!calendarCalc.ValidDateAndtime(simpleDateTime))
            {
                errorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
            };
            return errorCodes;
        }

        public List<int> ValidateTime(string hour, string minute, string second)
        {
            List<int> errorCodes = new();
            int hourValue, minuteValue, secondValue;
            string secondText = String.IsNullOrWhiteSpace(second) ? "0" : second;
            try
            {
                hourValue = Int32.Parse(hour);
                minuteValue = Int32.Parse(minute);
                secondValue = Int32.Parse(secondText);
             }
            catch (Exception ex)
            {
                // todo log exception
                errorCodes.Add(ErrorCodes.ERR_INVALID_TIME);
                return errorCodes;
            }
            if (hourValue < 0 || hourValue > 23 || minuteValue < 0 || minuteValue > 59 || secondValue < 0 || secondValue > 59)
            {
                errorCodes.Add(ErrorCodes.ERR_INVALID_TIME);
            }
            return errorCodes;
        }

    }
}
