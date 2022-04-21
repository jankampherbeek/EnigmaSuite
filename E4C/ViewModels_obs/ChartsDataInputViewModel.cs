// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Creators;
using E4C.Models.Domain;
using E4C.Models.UiHelpers;
using E4C.domain.shared.specifications;
using System;
using System.Collections.Generic;
using E4C.shared.references;

namespace E4C.ViewModels
{
    public class ChartsDataInputViewModel
    {
        readonly private IChartCategorySpecifications _chartCategorySpecifications;
        readonly private IRoddenRatingSpecifications _roddenRatingSpecifications;
        readonly private ICalendarSpecifications _calendarSpecifications;
        readonly private IYearCountSpecifications _yearCountSpecifications;
        readonly private ITimeZoneSpecifications _timeZoneSpecifications;
        readonly private IChartsStock _chartsStock;
        readonly private ILocationFactory _locationFactory;
        readonly private IDateFactory _dateFactory;
        readonly private ITimeFactory _timeFactory;
        readonly private IDateTimeFactory _dateTimeFactory;

        private FullDateTime? _fullDateTime;
        private Location? _location;

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
        public string[] InputLmtOffset { get; set; }
        public bool InputRbEastSelected { get; set; }
        public bool InputRbNorthSelected { get; set; }
        public bool InputRbLmtPlusSelected { get; set; }
        public bool InputCbDstSelected { get; set; }
        public int ChartCategoryIndex { get; set; }
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
            IChartsStock chartsStock,
            ILocationFactory locationFactory,
            IDateFactory dateFactory,
            ITimeFactory timeFactory,
            IDateTimeFactory dateTimeFactory)
        {
            _chartCategorySpecifications = chartCategorySpecifications;
            _roddenRatingSpecifications = roddenRatingSpecifications;
            _calendarSpecifications = calendarSpecifications;
            _yearCountSpecifications = yearCountSpecifications;
            _timeZoneSpecifications = timeZoneSpecifications;
            _chartsStock = chartsStock;
            _locationFactory = locationFactory;
            _dateFactory = dateFactory;
            _timeFactory = timeFactory;
            _dateTimeFactory = dateTimeFactory;

            ChartCategoryItems = new List<ChartCategoryDetails>();
            RoddenRatingItems = new List<RoddenRatingDetails>();
            CalendarItems = new List<CalendarDetails>();
            YearCountItems = new List<YearCountDetails>();
            TimeZoneItems = new List<TimeZoneDetails>();
            InputDate = new string[3];
            InputTime = new string[3];
            InputGeoLong = new string[3];
            InputGeoLat = new string[3];
            InputLmtOffset = new string[3];
            InputDescription = "";
            InputName = "";
            InputLocation = "";
            InputSource = "";


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
            foreach (TimeZones timeZone in Enum.GetValues(typeof(TimeZones)))
            {
                TimeZoneItems.Add(_timeZoneSpecifications.DetailsForTimeZone(timeZone));
            }
        }

        public List<int> ValidateInput()
        {
            bool dateOk = _dateFactory.CreateDate(InputDate, InputCalendar, InputYearCount, out FullDate fullDate, out List<int> dateErrorCodes);
            bool timeOk = _timeFactory.CreateTime(InputTime, InputTimeZone, InputLmtOffset, InputRbLmtPlusSelected, out FullTime fullTime, out List<int> timeErrorCodes);


            bool dateTimeOk = false;
            List<int> dateTimeErrorCodes = new();
            if (dateOk && timeOk)
            {
                dateTimeOk = _dateTimeFactory.CreateDateTime(fullDate, fullTime, out _fullDateTime, out dateTimeErrorCodes);
            }
            bool locationOk = _locationFactory.CreateLocation(InputLocation, InputGeoLong, InputGeoLat, InputRbEastSelected, InputRbNorthSelected, out _location, out List<int> locationErrorCodes);
            List<int> _allErrors = new();
            _allErrors.AddRange(dateErrorCodes);
            _allErrors.AddRange(timeErrorCodes);
            _allErrors.AddRange(dateTimeErrorCodes);
            _allErrors.AddRange(locationErrorCodes);
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

        public void SignalNewChartInputCompleted()
        {
            MetaData _metaData = new(InputName, InputDescription, InputSource, InputChartCategories, InputRoddenRating);

            // todo define tempId
            ChartData _chartData = new(-1, -1, _metaData, _location, _fullDateTime);

            // add ChartData to _chartsStock
            // construct Request
            // perform calculation
            // create form with chartdata
        }

    }
}


