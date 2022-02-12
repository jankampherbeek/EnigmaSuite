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
        /// <param name="inputDate">Array with strings for year, month and day, in that sequence.</param>
        /// <param name="calendar">Gregorian or Julian calendar.</param>
        /// <param name="yearCount">Year count: BCE, CE or astronomical.</param>
        /// <returns>List with error codes.</returns>
        public List<int> ValidateDate(string[] inputDate, Calendars calendar, YearCounts yearCount);

        /// <summary>
        /// Check the validity of input for a time.
        /// </summary>
        /// <param name="inputTime">Array with strings for hour, minute and second in that sequence.</param>
        /// <returns>List with error codes.</returns>
        public List<int> ValidateTime(string[] inputTime);
    }


    public class DateTimeValidations : IDateTimeValidations
    {
        readonly private ICalendarCalc _calendarCalc;

        public DateTimeValidations(ICalendarCalc calendarCalc)
        {
            _calendarCalc = calendarCalc ?? throw new ArgumentNullException(nameof(calendarCalc));
        }

        public List<int> ValidateDate(string[] inputDate, Calendars calendar, YearCounts yearCount)
        {
            List<int> _errorCodes = new();
            int _yearValue, _monthValue, _dayValue;
            try
            {
                _yearValue = int.Parse(inputDate[0]);
                _monthValue = int.Parse(inputDate[1]);
                _dayValue = int.Parse(inputDate[2]);
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

        public List<int> ValidateTime(string[] inputTime)
        {
            List<int> _errorCodes = new();
            int _hourValue, _minuteValue, _secondValue;
            string _secondText = String.IsNullOrWhiteSpace(inputTime[2]) ? "0" : inputTime[2];
            try
            {
                _hourValue = int.Parse(inputTime[0]);
                _minuteValue = int.Parse(inputTime[1]);
                _secondValue = int.Parse(_secondText);
            }
            catch (Exception ex)
            {
                // todo log exception
                _errorCodes.Add(ErrorCodes.ERR_INVALID_TIME);
                return _errorCodes;
            }
            if (_hourValue < Constants.HOUR_MIN || _hourValue > Constants.HOUR_MAX 
                || _minuteValue < Constants.MINUTE_MIN || _minuteValue > Constants.MINUTE_MAX 
                || _secondValue < Constants.SECOND_MIN || _secondValue > Constants.SECOND_MAX)
            {
                _errorCodes.Add(ErrorCodes.ERR_INVALID_TIME);
            }
            return _errorCodes;
        }

    }
}
