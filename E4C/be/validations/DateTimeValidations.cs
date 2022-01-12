// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.be.astron;
using E4C.be.domain;
using System;

namespace E4C.be.validations
{
    /// <summary>
    /// Validations for the input of date and time.
    /// </summary>
    public interface IDateTimeValidations
    {
        /// <summary>
        /// Check the validity of input for a date. 
        /// </summary>
        /// <param name="DateText">Text in the format yyyy/mm/dd.</param>
        /// <param name="IsGregorian">True for Gregorian calendar, false for Julian calendar.</param>
        /// <returns>Instance of ValidatedDate with values for the date and information about the validation results.</returns>
        public ValidatedDate ConstructAndValidateDate(string DateText, bool IsGregorian);

        /// <summary>
        /// Check the validity of input for a time.
        /// </summary>
        /// <param name="TimeText">Text in the format hh:mm:ss, the part for seconds is optional.</param>
        /// <returns>Instance of ValidatedTime with values for the time and information about the validation results.</returns>
        public ValidatedUniversalTime ConstructAndValidateTime(string TimeText);
    }


    public class DateTimeValidations : IDateTimeValidations
    {
        private string _dateText;
        private string _timeText;
        private bool _isGregorian;
        readonly private ICalendarCalc _calendarCalc;

        public DateTimeValidations(ICalendarCalc calendarCalc)
        {
            _calendarCalc = calendarCalc ?? throw new ArgumentNullException(nameof(calendarCalc));
        }

        public ValidatedDate ConstructAndValidateDate(string dateText, bool isGregorian)
        {
            _dateText = dateText ?? throw new ArgumentNullException(nameof(dateText));
            _isGregorian = isGregorian;
            int year, month, day;

            string[] dateItems = _dateText.Split('/');
            string ErrorText = "";
            bool NoErrors;
            if (dateItems.Length != 3)
            {
                NoErrors = false;
                ErrorText = "Wrong format for DateText.";
                return new ValidatedDate(0, 0, 0, true, NoErrors, ErrorText);
            }
            try
            {
                year = Int32.Parse(dateItems[0]);
                month = Int32.Parse(dateItems[1]);
                day = Int32.Parse(dateItems[2]);
            }
            catch (Exception ex)
            {
                NoErrors = false;
                ErrorText = "Error converting DateText to numerics: " + ex.Message;
                return new ValidatedDate(0, 0, 0, true, NoErrors, ErrorText);
            }
            SimpleDateTime simpleDateTime = new(year, month, day, 0.0, _isGregorian);
            NoErrors = _calendarCalc.ValidDateAndtime(simpleDateTime);
            if (!NoErrors)
            {
                ErrorText = "Incorrect date.";
            }
            return new ValidatedDate(year, month, day, isGregorian, NoErrors, ErrorText);

        }

        public ValidatedUniversalTime ConstructAndValidateTime(string timeText)
        {
            _timeText = timeText ?? throw new ArgumentNullException(nameof(timeText));
            bool NoErrors = true;
            int hour, minute, second;
            string ErrorText = "";
            string[] timeItems = _timeText.Split(':');
            if (timeItems.Length < 2 || timeItems.Length > 3)
            {
                NoErrors = false;
                ErrorText = "Wrong format for TimeText.";
                return new ValidatedUniversalTime(0, 0, 0, NoErrors, ErrorText);
            }
            try
            {
                hour = Int32.Parse(timeItems[0]);
                minute = Int32.Parse(timeItems[1]);
                if (timeItems.Length == 3) second = Int32.Parse(timeItems[2]);
                else second = 0;
            }
            catch (Exception ex)
            {
                NoErrors = false;
                ErrorText = "Error converting TimeText to numerics: " + ex.Message;
                return new ValidatedUniversalTime(0, 0, 0, NoErrors, ErrorText);
            }
            if (hour < 0 || hour > 23 || minute < 0 || minute > 59 || second < 0 || second > 59)
            {
                NoErrors = false;
                ErrorText = "One or more values out of range.";
                return new ValidatedUniversalTime(0, 0, 0, NoErrors, ErrorText);
            }
            return new ValidatedUniversalTime(hour, minute, second, NoErrors, ErrorText);
        }

    }
}
