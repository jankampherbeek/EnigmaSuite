using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E4C.Models.Domain;
using E4C.Models.Validations;

namespace E4C.ViewModels
{
    public class ChartsDataInputViewModel
    {
        readonly private IChartCategorySpecifications _chartCategorySpecifications;
        readonly private IRoddenRatingSpecifications _roddenRatingSpecifications;
        readonly private ICalendarSpecifications _calendarSpecifications;
        readonly private IYearCountSpecifications _yearCountSpecifications;
        readonly private ITimeZoneSpecifications _timeZoneSpecifications;
        readonly private IDateTimeValidations _dateTimeValidations;
        readonly private ILocationValidations _locationValidations;

        public List<ChartCategoryDetails> ChartCategoryItems { get; }
        public List<RoddenRatingDetails> RoddenRatingItems { get; }
        public List<CalendarDetails> CalendarItems { get; }
        public List<YearCountDetails> YearCountItems { get; }   
        public List<TimeZoneDetails> TimeZoneItems { get; }
        public string InputName { get; set; }   
        public string InputLocation { get; set; }
        public string InputSource { get; set; }
        public string InputDescription { get; set; }
        public string[] InputDate { get; set; }
        public string[] InputTime { get; set; }
        public string[] InputGeoLong { get; set; }
        public string[] InputGeoLat { get; set; }
        public string[] InputLmtLong { get; set; }
        public bool? InputRbEastSelected { get; set; }
        public bool? InputRbNorthSelected { get; set; }
        public bool? InputRbLmtEastSelected { get; set; }
        public bool? InputCbDstSelected { get; set; }
        public int ChartCategoryIndex { get;  set; }
        public int RoddenRatingIndex { get; set; }
        public int CalendarIndex { get; set; }
        public int YearCountIndex { get; set; }
        public int TimeZoneIndex { get; set; }
        public Calendars InputCalendar { get; set; }
        public YearCounts InputYearCount { get; set; }
        public ChartCategories InputChartCategories { get; set; }
        public RoddenRatings InputRoddenRating { get; set; }
        public TimeZones InputTimeZone { get; set; }




        public ChartsDataInputViewModel(IChartCategorySpecifications chartCategorySpecifications, 
            IRoddenRatingSpecifications roddenRatingSpecifications,
            ICalendarSpecifications calendarSpecifications,
            IYearCountSpecifications yearCountSpecifications,
            ITimeZoneSpecifications timeZoneSpecifications,
            IDateTimeValidations dateTimeValidations,
            ILocationValidations locationValidations)
        {
            _chartCategorySpecifications = chartCategorySpecifications;
            _roddenRatingSpecifications = roddenRatingSpecifications;
            _calendarSpecifications = calendarSpecifications;
            _yearCountSpecifications = yearCountSpecifications;
            _timeZoneSpecifications = timeZoneSpecifications;
            _dateTimeValidations = dateTimeValidations;
            _locationValidations = locationValidations;

            ChartCategoryItems = new List<ChartCategoryDetails>();
            RoddenRatingItems = new List<RoddenRatingDetails>();
            CalendarItems = new List<CalendarDetails>();
            YearCountItems = new List<YearCountDetails>();
            TimeZoneItems = new List<TimeZoneDetails>();

            DefineReferenceItems();

        }

        public void DefineReferenceItems()
        {
            DefineChartCategoryItems();
            DefineRoddenRatingItems();
            DefineCalendarItems();
            DefineYearCounts();
            DefineTimeZones();
        }


        public void DefineChartCategoryItems()
        {
            foreach (ChartCategories category in Enum.GetValues(typeof(ChartCategories)))
            {
                ChartCategoryItems.Add(_chartCategorySpecifications.DetailsForCategory(category));
            }
        }

        public void DefineRoddenRatingItems()
        {
            foreach (RoddenRatings rating in Enum.GetValues(typeof(RoddenRatings)))
            {
                RoddenRatingItems.Add(_roddenRatingSpecifications.DetailsForRating(rating));
            }
        }

        public void DefineCalendarItems()
        {
            foreach (Calendars calendar in Enum.GetValues(typeof(Calendars)))
            {
                CalendarItems.Add(_calendarSpecifications.DetailsForCalendar(calendar));
            }
        }

        public void DefineYearCounts()
        {
            foreach (YearCounts yearCount in Enum.GetValues(typeof(YearCounts)))
            {
                YearCountItems.Add(_yearCountSpecifications.DetailsForYearCount(yearCount));    
            }
        }

        public void DefineTimeZones()
        {
            foreach(TimeZones timeZone in Enum.GetValues(typeof(TimeZones)))
            {
                TimeZoneItems.Add(_timeZoneSpecifications.DetailsForTimeZone(timeZone));
            }
        }

        public List<int> ValidateInput()
        {
            if (InputRbEastSelected == null || InputRbNorthSelected == null || InputRbLmtEastSelected == null || InputCbDstSelected == null)
            {
                string ErrorMsg = "Unexpected error in ChartsDataInputViewModel.ValidateInput(). At least one of the radiobuttons or checkbox is null. " +
                    "Received the values: InputRbEastSelected " + InputRbEastSelected + " , InputRbNorthSelected " + InputRbNorthSelected + " , InputRbLmtEastSelected " + InputRbLmtEastSelected + " , InputCbDstSelected " + InputCbDstSelected;
                throw new Exception(ErrorMsg);
            }
            List<int> _dateErrors = _dateTimeValidations.ValidateDate(InputDate, InputCalendar, InputYearCount);
            List<int> _timeErrors = _dateTimeValidations.ValidateTime(InputTime);
            List<int> _geoLongitudeErrors = _locationValidations.ValidateGeoLongitude(InputGeoLong);
            List<int> _geoLatitudeErrors = _locationValidations.ValidateGeoLatitude(InputGeoLat);
            List<int> _lmtLongitudeErrors = new();
            if (InputTimeZone == TimeZones.LMT)
            {
                _lmtLongitudeErrors = _locationValidations.ValidateGeoLongitude(InputLmtLong);
            }
            List<int> _allErrors = new();
            _allErrors.AddRange(_dateErrors);
            _allErrors.AddRange(_timeErrors);
            _allErrors.AddRange(_geoLongitudeErrors);
            _allErrors.AddRange(_geoLatitudeErrors);
            _allErrors.AddRange(_lmtLongitudeErrors);
            return _allErrors;
        }

        public void RetrieveComboBoxValues()
        {
            InputChartCategories = _chartCategorySpecifications.ChartCategoryForIndex(ChartCategoryIndex);
            InputRoddenRating = _roddenRatingSpecifications.RoddenRatingForIndex(RoddenRatingIndex);
            InputCalendar = _calendarSpecifications.CalendarForIndex(CalendarIndex);
            InputYearCount = _yearCountSpecifications.YearCountForIndex(YearCountIndex);
            InputTimeZone = _timeZoneSpecifications.TimeZoneForIndex(TimeZoneIndex);
            
        }

    }
}


