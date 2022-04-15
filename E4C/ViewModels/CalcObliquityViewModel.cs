// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.calc.seph.secalculations;
using E4C.core.astron.obliquity;
using E4C.core.shared.domain;
using E4C.Models.Astron;
using E4C.Models.Creators;
using E4C.Models.Domain;
using E4C.shared.references;
using System;
using System.Collections.Generic;

namespace E4C.ViewModels
{
    public class CalcObliquityViewModel
    {
        readonly private ICalendarSpecifications _calendarSpecifications;
        readonly private IYearCountSpecifications _yearCountSpecifications;
        readonly private ICalendarCalc _calCalc;
        readonly private IDateTimeValidations _dateTimeValidations;
        readonly private IObliquityCalc _obliquityNutationCalc;
        public List<CalendarDetails> CalendarItems { get; }
        public List<YearCountDetails> YearCountItems { get; }
        public string[] InputDate { get; set; }
        public Calendars InputCalendar { get; set; }
        public YearCounts InputYearCount { get; set; }
        public bool UseTrueObliquity { get; set; }


        public CalcObliquityViewModel(ICalendarCalc calCalc, IObliquityCalc oblCalc, IDateTimeValidations dateTimeValidations, ICalendarSpecifications calendarSpecifications, IYearCountSpecifications yearCountSpecifications)
        {
            _calendarSpecifications = calendarSpecifications;
            _yearCountSpecifications = yearCountSpecifications;
            _calCalc = calCalc;
            _dateTimeValidations = dateTimeValidations;
            _obliquityNutationCalc = oblCalc;
            UseTrueObliquity = true;
            InputDate = new string[3];
            CalendarItems = new List<CalendarDetails>();
            foreach (Calendars calendar in Enum.GetValues(typeof(Calendars)))
            {
                CalendarItems.Add(_calendarSpecifications.DetailsForCalendar(calendar));
            }
            YearCountItems = new List<YearCountDetails>();
            foreach (YearCounts yearCount in Enum.GetValues(typeof(YearCounts)))
            {
                YearCountItems.Add(_yearCountSpecifications.DetailsForYearCount(yearCount));
            }
        }

        public List<int> ValidateInput()
        {
            List<int> _dateErrors = _dateTimeValidations.ValidateDate(InputDate, InputCalendar, InputYearCount);
            List<int> _allErrors = new();
            _allErrors.AddRange(_dateErrors);
            return _allErrors;
        }

        public string CalculateObliquity()
        {
            int _year = int.Parse(InputDate[0]);
            int _month = int.Parse(InputDate[1]);
            int _day = int.Parse(InputDate[2]);
            // Convert to astronomical yearcount if original yearcount is BCE.
            if (InputYearCount == YearCounts.BCE)
            {
                _year = -(Math.Abs(_year) + 1);
            }
            SimpleDateTime _dateTime = new(_year, _month, _day, 0.0, InputCalendar);
            ResultForDouble _julianDayResult = _calCalc.CalculateJd(_dateTime);
            if (!_julianDayResult.NoErrors)
            {
                throw new Exception("Error calculating jdnr..........");
            }

            double _resultObliquity = _obliquityNutationCalc.CalculateObliquity(_julianDayResult.ReturnValue, UseTrueObliquity);
            return _resultObliquity.ToString();
        }
    }
}
