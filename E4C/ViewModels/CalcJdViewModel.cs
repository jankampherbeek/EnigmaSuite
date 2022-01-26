using E4C.Models.Astron;
using E4C.Models.Domain;
using E4C.Models.Validations;
using System;
using System.Collections.Generic;

namespace E4C.ViewModels
{


    public class CalcJdViewModel
    {
        readonly private ICalendarSpecifications calendarSpecifications;
        readonly private IYearCountSpecifications yearCountSpecifications;
        readonly private ICalendarCalc calCalc;
        readonly private IDateTimeValidations dateTimeValidations;
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
            this.calendarSpecifications = calendarSpecifications;
            this.yearCountSpecifications = yearCountSpecifications;
            this.calCalc = calCalc;
            this.dateTimeValidations = dateTimeValidations;
            CalendarItems = new List<CalendarDetails>();
            foreach (Calendars calendar in Enum.GetValues(typeof(Calendars)))
            {
                CalendarItems.Add(calendarSpecifications.DetailsForCalendar(calendar));
            }
            YearCountItems= new List<YearCountDetails>();
            foreach (YearCounts yearCount in Enum.GetValues(typeof(YearCounts)))
            {
                YearCountItems.Add(yearCountSpecifications.DetailsForYearCount(yearCount));
            }
        }

        public List<int> ValidateInput()
        {
            List<int> dateErrors = dateTimeValidations.ValidateDate(InputYear, InputMonth, InputDay, InputCalendar, InputYearCount);
            List<int> timeErrors = dateTimeValidations.ValidateTime(InputHour, InputMinute, InputSecond);
            List<int> allErrors = new();
            allErrors.AddRange(dateErrors);
            allErrors.AddRange(timeErrors);
            return allErrors;
        }

        public string CalculateJd()
        {
            string checkedSecond = String.IsNullOrWhiteSpace(InputSecond) ? "0" : InputSecond;
            double fractionalTime = Int32.Parse(InputHour) + (Int32.Parse(InputMinute) / 60.0) + (Int32.Parse(checkedSecond) / 3600.0);
            int year = Int32.Parse(InputYear);
            int month = Int32.Parse(InputMonth);
            int day = Int32.Parse(InputDay);
            // Convert to astronomical yearcount if original yearcount is BCE.
            if (InputYearCount == YearCounts.BCE)
            {
                year = -(Math.Abs(year) + 1);
            }
            SimpleDateTime dateTime = new(year, month, day, fractionalTime, InputCalendar);
            ResultForDouble resultJd = calCalc.CalculateJd(dateTime);
            if (resultJd.noErrors)
            {
                return resultJd.returnValue.ToString();
            }
            else
            {
                // todo log error
                throw new Exception("Error when calculating JD");
            }
        }

    }
}
