// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Astron;
using E4C.Models.Domain;
using E4C.domain.shared.specifications;
using System;
using System.Collections.Generic;

namespace E4C.Models.Creators
{

    /// <summary>
    /// Conversions from an array of string to an array with integers.
    /// </summary>
    public interface IIntRangeCreator
    {
        /// <summary>
        /// Convert and validate an array of strings to an array of integers.
        /// </summary>
        /// <param name="texts">An array with texts.</param>
        /// <param name="numValues">If no error occurred an array with integer values that are converted from the contents of texts.</param>
        /// <returns>True if no error occurred, otherwise false.</returns>
        public bool CreateIntRange(string[] texts, out int[] numValues);
    }

    /// <summary>
    /// Factory for a record Location.
    /// </summary>
    public interface ILocationFactory
    {
        /// <summary>
        /// Validate input and create a record Location.
        /// </summary>
        /// <param name="locationName">Name for the location. An empty string will be ignored.</param>
        /// <param name="longTexts">Texts with values for longitude: degrees, minutes, seconds.</param>
        /// <param name="latTexts">Texts with values for latitude: degrees, minutes, seconds.</param>
        /// <param name="longIsEast">True for eastern longitude, false for west.</param>
        /// <param name="latIsNorth">True for northern latitude, false for south.</param>
        /// <param name="location">The constructed record of Location.</param>
        /// <param name="errorCodes">Errorcodes, if any.</param>
        /// <returns>True if no error occurred, otherwise false.</returns>
        public bool CreateLocation(string locationName, string[] longTexts, string[] latTexts, bool longIsEast, bool latIsNorth, out Location location, out List<int> errorCodes);
    }

    /// <summary>
    /// Factory for a full data.
    /// </summary>
    public interface IDateFactory
    {
        /// <summary>
        /// Validate input and create a record FullDate.
        /// </summary>
        /// <param name="dateTexts">Texts with subsequently values for year, month and day.</param>
        /// <param name="calendar">The calendar that is used (Gregorian or Julian).</param>
        /// <param name="yearCount">The year count, this will be converted to an astronomical year count.</param>
        /// <param name="fullDate">The resulting record FullDate.</param>
        /// <param name="errorCodes">Errorcodes, if any.</param> 
        /// <returns>True if no error occurred, otherwise false.</returns>
        public bool CreateDate(string[] dateTexts, Calendars calendar, YearCounts yearCount, out FullDate fullDate, out List<int> errorCodes);
    }


    /// <summary>
    /// Factory for FullTime.
    /// </summary>
    public interface ITimeFactory
    {
        /// <summary>
        /// Validate input and create a record FullTime.
        /// </summary>
        /// <param name="timeTexts">Texts with subsequently values for hour, minute and second.</param>
        /// <param name="timezone">The timezone that is used.</param>
        /// <param name="offsetLmtTexts">If TimeZone is LMT this parameter contains texts for the time offset: hours, minutes and seconds in that sequence. Ignore if TimeZone != LMT.</param>
        /// <param name="offSetPlus">True is offset for LMT is positieve (east), otherwise false. Ignore if TimeZone != LMT.</param>
        /// <param name="fullTime">Resulting record FullTime.</param>
        /// <param name="errorCodes">Errorcodes, if any.</param>
        /// <returns>True if no error was found, otherwise false.</returns>
        public bool CreateTime(string[] timeTexts, TimeZones timezone, string[] offsetLmtTexts, bool offSetPlus, out FullTime fullTime, out List<int> errorCodes);
    }

    /// <summary>
    /// Factory for FullDateTime.
    /// </summary>
    public interface IDateTimeFactory
    {
        /// <summary>
        /// Creates record FullDateTime and validates incoming parameters.
        /// </summary>
        /// <param name="fullDate">Instance of FullDate.</param>
        /// <param name="fullTime">Instance of FullTime.</param>
        /// <param name="fullDateTime">Resulting record FullDateTime.</param>
        /// <param name="errorCodes">Errorcodes, if any.</param>
        /// <returns>True if no error was found, otherwise false.</returns>
        public bool CreateDateTime(FullDate fullDate, FullTime fullTime, out FullDateTime fullDateTime, out List<int> errorCodes);
    }

    /// <summary>
    /// Validations for the input of date and time.
    /// </summary>
    public interface IDateTimeValidations
    {
        // TODO remove
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


    public class IntRangeCreator : IIntRangeCreator
    {
        public bool CreateIntRange(string[] texts, out int[] numValues)
        {
            int count = texts.Length;
            numValues = new int[count];
            bool success = true;
            for (int i = 0; i < count; i++)
            {
                success = success && (int.TryParse(texts[i], out numValues[i]));
            }
            return success;
        }

    }

    public class LocationFactory : ILocationFactory
    {
        private readonly IIntRangeCreator _intRangeCreator;
        private int[] _longValues = Array.Empty<int>();
        private int[] _latValues = Array.Empty<int>();
        private bool _success = true;

        public LocationFactory(IIntRangeCreator intRangeCreator)
        {
            _intRangeCreator = intRangeCreator;
        }

        public bool CreateLocation(string locationName, string[] longTexts, string[] latTexts, bool longIsEast, bool latIsNorth, out Location location, out List<int> errorCodes)
        {
            errorCodes = new List<int>();
            string _locFullName = string.Empty;
            double _longValue = 0.0;
            double _latValue = 0.0;
            if (!_intRangeCreator.CreateIntRange(longTexts, out _longValues))
            {
                _success = false;
                errorCodes.Add(ErrorCodes.ERR_INVALID_GEOLON);
            }
            if (!_intRangeCreator.CreateIntRange(latTexts, out _latValues))
            {
                _success = false;
                errorCodes.Add(ErrorCodes.ERR_INVALID_GEOLAT);
            }
            if (_success)
            {
                _locFullName = ConstructLocationFullName(locationName, longIsEast, latIsNorth);
                _longValue = _longValues[0] + (double)_longValues[1] / Constants.MINUTES_PER_HOUR_DEGREE + (double)_longValues[2] / Constants.SECONDS_PER_HOUR_DEGREE;
                _latValue = _latValues[0] + (double)_latValues[1] / Constants.MINUTES_PER_HOUR_DEGREE + (double)_latValues[2] / Constants.SECONDS_PER_HOUR_DEGREE;
                if (!longIsEast) { _longValue = -_longValue; }
                if (!latIsNorth) { _latValue = -_latValue; }

                if (_longValues[0] > 180.0 || _longValues[0] < 0.0
                    || _longValues[1] < Constants.MINUTE_MIN || _longValues[1] > Constants.MINUTE_MAX
                    || _longValues[2] < Constants.SECOND_MIN || _longValues[2] > Constants.SECOND_MAX
                    || _longValue > 180.0 || _longValue < -180.0)
                {
                    _success = false;
                    errorCodes.Add(ErrorCodes.ERR_INVALID_GEOLON);
                }
                if (_latValues[0] >= 90.0 || _latValues[0] <= -90.0
                    || _latValues[1] < Constants.MINUTE_MIN || _latValues[1] > Constants.MINUTE_MAX
                    || _latValues[2] < Constants.SECOND_MIN || _latValues[2] > Constants.SECOND_MAX)
                {
                    _success = false;
                    errorCodes.Add(ErrorCodes.ERR_INVALID_GEOLAT);
                }
            }
            location = new Location(_locFullName, _longValue, _latValue);
            return _success;
        }

        private string ConstructLocationFullName(string locationName, bool longIsEast, bool latIsNorth)
        {
            string longDirection = longIsEast ? "E" : "W";
            string latDirection = latIsNorth ? "N" : "S";
            string dmsLongText = $"{_longValues[0]:D3}{Constants.DEGREE_SIGN}{_longValues[1]:D2}{Constants.MINUTE_SIGN}{_longValues[2]:D2}{Constants.SECOND_SIGN} [{longDirection}]";
            string dmsLatText = $"{_latValues[0]:D2}{Constants.DEGREE_SIGN}{_latValues[1]:D2}{Constants.MINUTE_SIGN}{_latValues[2]:D2}{Constants.SECOND_SIGN} [{latDirection}]";
            if (locationName == "")
            {
                return $"{dmsLongText} {dmsLatText}";
            }
            return $"{locationName} {dmsLongText} {dmsLatText}";
        }


    }

    public class DateFactory : IDateFactory
    {
        private readonly ICalendarCalc _calendarCalc;
        private readonly IIntRangeCreator _intRangeCreator;
        private int[] _dateValues = Array.Empty<int>();
        private bool _success = true;
        private readonly List<int> _errorCodes = new();

        public DateFactory(IIntRangeCreator intRangeCreator, ICalendarCalc calendarCalc)
        {
            _intRangeCreator = intRangeCreator;
            _calendarCalc = calendarCalc;
        }

        public bool CreateDate(string[] dateTexts, Calendars calendar, YearCounts yearCount, out FullDate fullDate, out List<int> errorCodes)
        {
            _success = _success && _intRangeCreator.CreateIntRange(dateTexts, out _dateValues);
            if (yearCount == YearCounts.BCE)
            {
                _dateValues[0] = -(_dateValues[0]) + 1;
            }
            _success = _success && _calendarCalc.ValidDateAndtime(new SimpleDateTime(_dateValues[0], _dateValues[1], _dateValues[2], 0.0, calendar));
            if (!_success)
            {
                _errorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
            }
            string _fullDateText = CreateFullDateText(calendar);
            fullDate = new FullDate(_dateValues, calendar, _fullDateText);
            errorCodes = _errorCodes;
            return _success;
        }

        private string CreateFullDateText(Calendars calendar)
        {
            string _yearText = $"{_dateValues[0]:D4}";
            if (_dateValues[0] > 9999 || _dateValues[0] < -9999)
            {
                _yearText = $"{_dateValues[0]:D5}";
            }
            string _monthText = GetPostFixIdForResourceBundle(_dateValues[1]);
            string _calendarText = calendar == Calendars.Gregorian ? "g" : "j";
            return $"[month:{_monthText}] {_yearText}, {_dateValues[2]} [{_calendarText}]";
        }

        private static string GetPostFixIdForResourceBundle(int monthId)
        {
            string[] postFixes = new string[] { "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" };
            return postFixes[monthId - 1];
        }
    }

    public class TimeFactory : ITimeFactory
    {
        readonly private IIntRangeCreator _intRangeCreator;
        readonly private ITimeZoneSpecifications _timeZoneSpecifications;
        private int[] _timeValues = Array.Empty<int>();
        private int[] _lmtOffsetValues = Array.Empty<int>();
        private bool _success = true;
        private readonly List<int> _errorCodes = new();

        public TimeFactory(IIntRangeCreator intRangeCreator, ITimeZoneSpecifications timeZoneSpecifications)
        {
            _intRangeCreator = intRangeCreator;
            _timeZoneSpecifications = timeZoneSpecifications;
        }

        public bool CreateTime(string[] timeTexts, TimeZones timezone, string[] offsetLmtTexts, bool offSetPlus, out FullTime fullTime, out List<int> errorCodes)
        {
            double _ut = 0.0;
            double _offset;
            string _fullText = "";
            int _correctionForDay = 0;
            bool _timesOk = _intRangeCreator.CreateIntRange(timeTexts, out _timeValues);
            if (!_timesOk)
            {
                _success = false;
                _errorCodes.Add(ErrorCodes.ERR_INVALID_TIME);
            };
            bool _lmtOffsetOk = _intRangeCreator.CreateIntRange(offsetLmtTexts, out _lmtOffsetValues);
            if (!_lmtOffsetOk)
            {
                _success = false;
                _errorCodes.Add(ErrorCodes.ERR_INVALID_OFFSET);
            }
            if (_success)
            {
                _ut = _timeValues[0] + (double)_timeValues[1] / Constants.MINUTES_PER_HOUR_DEGREE + (double)_timeValues[2] / Constants.SECONDS_PER_HOUR_DEGREE;
                if (timezone == TimeZones.LMT)
                {
                    _offset = _lmtOffsetValues[0] + (double)_lmtOffsetValues[1] / Constants.MINUTES_PER_HOUR_DEGREE + (double)_lmtOffsetValues[2] / Constants.SECONDS_PER_HOUR_DEGREE;
                }
                else
                {
                    _offset = _timeZoneSpecifications.DetailsForTimeZone(timezone).OffsetFromUt;
                }
                _ut -= _offset;
                if (_ut < 0.0)
                {
                    _ut += 24.0;
                    _correctionForDay = -1;
                }
                else if (_ut >= 24.0)
                {
                    _ut -= 24.0;
                    _correctionForDay = 1;
                }
                _fullText = CreateFullText(timezone, offSetPlus);
            }
            fullTime = new FullTime(_timeValues, _ut, _correctionForDay, _fullText);
            errorCodes = _errorCodes;
            return _success;
        }

        private string CreateFullText(TimeZones timezone, bool offSetPlus)
        {
            string _timeZoneTextId = _timeZoneSpecifications.DetailsForTimeZone(timezone).TextId;
            string _fullText = $"{_timeValues[0]:d2}:{_timeValues[1]:d2}:{_timeValues[2]:d2} [{_timeZoneTextId}]";

            if (timezone == TimeZones.LMT)
            {
                string _plusMinus = offSetPlus ? "+" : "-";
                _fullText += $" {_plusMinus}{_lmtOffsetValues[0]:d2}:{_lmtOffsetValues[1]:d2}:{_lmtOffsetValues[2]:d2}";
            }
            return _fullText;
        }

    }

    public class DateTimeFactory : IDateTimeFactory
    {
        readonly private ICalendarCalc _calendarCalc;

        public DateTimeFactory(ICalendarCalc calendarCalc)
        {
            _calendarCalc = calendarCalc;
        }

        public bool CreateDateTime(FullDate fullDate, FullTime fullTime, out FullDateTime fullDateTime, out List<int> errorCodes)
        {
            bool succes = true;
            errorCodes = new List<int>();
            string dateText = fullDate.DateFullText;
            string timeText = fullTime.TimeFullText;
            SimpleDateTime simpleDateTime = new(fullDate.YearMonthDay[0], fullDate.YearMonthDay[1], fullDate.YearMonthDay[2], fullTime.Ut, fullDate.Calendar);
            ResultForDouble resultForJulianDay = _calendarCalc.CalculateJd(simpleDateTime);
            double baseForJulianDay = 0.0;
            if (resultForJulianDay.NoErrors)
            {
                baseForJulianDay = resultForJulianDay.ReturnValue;
            }
            else
            {
                errorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
                succes = false;
            }
            double jdnrEt = baseForJulianDay + fullTime.CorrectionForDay;
            fullDateTime = new FullDateTime(dateText, timeText, jdnrEt);
            return succes;
        }
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
            catch (Exception)
            {
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
            catch (Exception)
            {
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
