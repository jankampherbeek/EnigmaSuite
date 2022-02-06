using E4C.Models.Astron;
using E4C.Models.Domain;
using E4C.Models.Validations;
using System;
using System.Collections.Generic;

namespace E4C.ViewModels
{


    public class CalcJdViewModel
    {
        readonly private ICalendarSpecifications _calendarSpecifications;
        readonly private IYearCountSpecifications _yearCountSpecifications;
        readonly private ICalendarCalc _calCalc;
        readonly private IDateTimeValidations _dateTimeValidations;
        public List<CalendarDetails> CalendarItems { get; }
        public List<YearCountDetails> YearCountItems { get; }
        public string InputYear { get; set; }
        public string InputMonth { get; set; }
        public string InputDay { get; set; }
        public string InputHour { get; set; }
        public string InputMinute { get; set; }
        public string InputSecond { get; set; }
        public Calendars InputCalendar { get; set; }
        public YearCounts InputYearCount { get; set; }

        public CalcJdViewModel(ICalendarCalc calCalc, IDateTimeValidations dateTimeValidations, ICalendarSpecifications calendarSpecifications, IYearCountSpecifications yearCountSpecifications)
        {
            _calendarSpecifications = calendarSpecifications;
            _yearCountSpecifications = yearCountSpecifications;
            _calCalc = calCalc;
            _dateTimeValidations = dateTimeValidations;
            CalendarItems = new List<CalendarDetails>();
            foreach (Calendars calendar in Enum.GetValues(typeof(Calendars)))
            {
                CalendarItems.Add(calendarSpecifications.DetailsForCalendar(calendar));
            }
            YearCountItems = new List<YearCountDetails>();
            foreach (YearCounts yearCount in Enum.GetValues(typeof(YearCounts)))
            {
                YearCountItems.Add(yearCountSpecifications.DetailsForYearCount(yearCount));
            }
        }

        public List<int> ValidateInput()
        {
            List<int> _dateErrors = _dateTimeValidations.ValidateDate(InputYear, InputMonth, InputDay, InputCalendar, InputYearCount);
            List<int> _timeErrors = _dateTimeValidations.ValidateTime(InputHour, InputMinute, InputSecond);
            List<int> _allErrors = new();
            _allErrors.AddRange(_dateErrors);
            _allErrors.AddRange(_timeErrors);
            return _allErrors;
        }

        public string CalculateJd()
        {
            string _checkedSecond = String.IsNullOrWhiteSpace(InputSecond) ? "0" : InputSecond;
            double _fractionalTime = int.Parse(InputHour) + (int.Parse(InputMinute) / 60.0) + (int.Parse(_checkedSecond) / 3600.0);
            int _year = int.Parse(InputYear);
            int _month = int.Parse(InputMonth);
            int _day = int.Parse(InputDay);
            // Convert to astronomical yearcount if original yearcount is BCE.
            if (InputYearCount == YearCounts.BCE)
            {
                _year = -(Math.Abs(_year) + 1);
            }
            SimpleDateTime _dateTime = new(_year, _month, _day, _fractionalTime, InputCalendar);
            ResultForDouble _resultJd = _calCalc.CalculateJd(_dateTime);
            if (_resultJd.NoErrors)
            {
                return _resultJd.ReturnValue.ToString();
            }
            else
            {
                // todo log error
                throw new Exception("Error when calculating JD");
            }
        }

    }
}
