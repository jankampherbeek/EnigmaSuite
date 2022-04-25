// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.core.api;
using E4C.Core.Shared.Domain;
using E4C.Models.Creators;
using E4C.Shared.References;
using E4C.Shared.ReqResp;
using E4C.Ui.Shared;
using System;
using System.Collections.Generic;

namespace E4C.ViewModels
{


    public class CalcJdViewModel
    {
        readonly private ICalendarSpecifications _calendarSpecifications;
        readonly private IYearCountSpecifications _yearCountSpecifications;
        readonly private IDateTimeValidations _dateTimeValidations;
        readonly private IDateTimeApi _dateTimeApi;


        public List<CalendarDetails> CalendarItems { get; }
        public List<YearCountDetails> YearCountItems { get; }
        public string[] InputDate { get; set; }
        public string[] InputTime { get; set; }
        public Calendars InputCalendar { get; set; }
        public YearCounts InputYearCount { get; set; }

        public CalcJdViewModel(IDateTimeValidations dateTimeValidations, ICalendarSpecifications calendarSpecifications, IYearCountSpecifications yearCountSpecifications, IDateTimeApi dateTimeApi)
        {
            _calendarSpecifications = calendarSpecifications;
            _yearCountSpecifications = yearCountSpecifications;
            _dateTimeValidations = dateTimeValidations;
            _dateTimeApi = dateTimeApi;
            InputDate = new string[3];
            InputTime = new string[3];
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
            List<int> _timeErrors = _dateTimeValidations.ValidateTime(InputTime);
            List<int> _allErrors = new();
            _allErrors.AddRange(_dateErrors);
            _allErrors.AddRange(_timeErrors);
            return _allErrors;
        }

        public string CalculateJd()
        {
            string _checkedSecond = String.IsNullOrWhiteSpace(InputTime[2]) ? "0" : InputTime[2];
            double _fractionalTime = int.Parse(InputTime[0]) + (int.Parse(InputTime[1]) / 60.0) + (int.Parse(_checkedSecond) / 3600.0);
            int _year = int.Parse(InputDate[0]);
            int _month = int.Parse(InputDate[1]);
            int _day = int.Parse(InputDate[2]);
            // Convert to astronomical yearcount if original yearcount is BCE.
            if (InputYearCount == YearCounts.BCE)
            {
                _year = -(Math.Abs(_year) + 1);
            }
            SimpleDateTime _dateTime = new(_year, _month, _day, _fractionalTime, InputCalendar);

            JulianDayResponse julDayResponse = _dateTimeApi.getJulianDay(new JulianDayRequest(_dateTime, true));

            if (julDayResponse.Success)
            {
                return julDayResponse.JulDay.ToString();
            }
            else
            {
                // todo log error
                throw new Exception("Error when calculating JD: " + julDayResponse.ErrorText);
            }

        }

    }
}
