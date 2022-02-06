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
        readonly private ICalendarCalc _calendarCalc;

        public DateTimeValidations(ICalendarCalc calendarCalc)
        {
            _calendarCalc = calendarCalc ?? throw new ArgumentNullException(nameof(calendarCalc));
        }

        public List<int> ValidateDate(string year, string month, string day, Calendars calendar, YearCounts yearCount)
        {
            List<int> _errorCodes = new();
            int _yearValue, _monthValue, _dayValue;
            try
            {
                _yearValue = int.Parse(year);
                _monthValue = int.Parse(month);
                _dayValue = int.Parse(day);
            }
            catch (Exception ex)
            {
                // todo log exception
                _errorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
                return _errorCodes;
            }
            // Convert to astronomical yearcount if original yearcount is BCE.
            if (yearCount == YearCounts.BCE)
            {
                _yearValue = -(Math.Abs(_yearValue) + 1);
            }
            SimpleDateTime _simpleDateTime = new(_yearValue, _monthValue, _dayValue, 0.0, calendar);
            if (!_calendarCalc.ValidDateAndtime(_simpleDateTime))
            {
                _errorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
            };
            return _errorCodes;
        }

        public List<int> ValidateTime(string hour, string minute, string second)
        {
            List<int> _errorCodes = new();
            int _hourValue, _minuteValue, _secondValue;
            string _secondText = String.IsNullOrWhiteSpace(second) ? "0" : second;
            try
            {
                _hourValue = int.Parse(hour);
                _minuteValue = int.Parse(minute);
                _secondValue = int.Parse(_secondText);
            }
            catch (Exception ex)
            {
                // todo log exception
                _errorCodes.Add(ErrorCodes.ERR_INVALID_TIME);
                return _errorCodes;
            }
            if (_hourValue < 0 || _hourValue > 23 || _minuteValue < 0 || _minuteValue > 59 || _secondValue < 0 || _secondValue > 59)
            {
                _errorCodes.Add(ErrorCodes.ERR_INVALID_TIME);
            }
            return _errorCodes;
        }

    }
}
