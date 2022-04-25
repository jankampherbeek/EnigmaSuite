// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.domain.shared.specifications;
using E4C.Shared.References;
using E4C.Ui.Charts;
using System.Collections.Generic;

namespace E4C.Models.Domain
{



    /// <summary>
    /// Wrapper for a calculated double with error information.
    /// </summary>
    public record ResultForDouble
    {
        public readonly double ReturnValue;
        public readonly bool NoErrors;
        public readonly string ErrorText = "";

        public ResultForDouble(double returnValue, bool noErrors, string errorText = "")
        {
            ReturnValue = returnValue;
            NoErrors = noErrors;
            ErrorText = errorText;
        }
    }

    /// <summary>
    /// Wrapper for a date with error information.
    /// </summary>
    public record ValidatedDate
    {
        public readonly int Year;
        public readonly int Month;
        public readonly int Day;
        public readonly Calendars Calendar;
        public readonly List<int> ErrorCodes;

        public ValidatedDate(int year, int month, int day, Calendars calendar, List<int> errorCodes)
        {
            Year = year;
            Month = month;
            Day = day;
            Calendar = calendar;
            ErrorCodes = errorCodes;
        }
    }

    /// <summary>
    /// Wrapper for time with error information. Time should always be in UT.
    /// </summary>
    public record ValidatedUniversalTime
    {
        public readonly int Hour;
        public readonly int Minute;
        public readonly int Second;
        public readonly List<int> ErrorCodes;

        public ValidatedUniversalTime(int hour, int minute, int second, List<int> errorCodes)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
            ErrorCodes = errorCodes;
        }

    }


    /// <summary>
    /// Metadata for a chart.
    /// </summary>
    public record MetaData
    {
        public readonly string Name;
        public readonly string Description;
        public readonly string Source;
        public readonly ChartCategories ChartCategory;
        public readonly RoddenRatings RoddenRating;

        /// <summary>
        /// Constructor for record MetaData.
        /// </summary>
        /// <param name="name">Name for the chart.</param>
        /// <param name="description">A descriptive text, possibly an empty string.</param>
        /// <param name="source">An indication for the source, possibly an empty string.</param>
        /// <param name="chartCategory">The category for teh chart, from enum ChartCategories.</param>
        /// <param name="roddenRating">The Rodden Rating, from the enum RoddenRatings.</param>
        public MetaData(string name, string description, string source, ChartCategories chartCategory, RoddenRatings roddenRating)
        {
            Name = name;
            Description = description;
            Source = source;
            ChartCategory = chartCategory;
            RoddenRating = roddenRating;
        }
    }


    /// <summary>
    /// Record for a full definition of a data.
    /// </summary>
    /// <remarks>Assumes an astronomical year count.</remarks>
    public record FullDate
    {
        public readonly int[] YearMonthDay;
        public readonly Calendars Calendar;
        public readonly string DateFullText;

        /// <summary>
        /// Constructor for FullDate.
        /// </summary>
        /// <param name="yearMonthDay">Texts for year, month and day, in that sequence.</param>
        /// <param name="calendar">Instane of enu Calendars.</param>
        /// <param name="dateFullText">Text for the date, includes texts between [] that needs to be replaced with texts from Rosetta.</param>
        public FullDate(int[] yearMonthDay, Calendars calendar, string dateFullText)
        {
            YearMonthDay = yearMonthDay;
            Calendar = calendar;
            DateFullText = dateFullText;
        }
    }

    /// <summary>
    /// Record for a full definition of a time.
    /// </summary>
    public record FullTime
    {
        public readonly int[] HourMinuteSecond;
        public readonly double Ut;
        public readonly int CorrectionForDay;
        public readonly string TimeFullText;

        /// <summary>
        /// Constructor for FullTime.
        /// </summary>
        /// <param name="hourMinuteSecond">Texts for hour, minute and second in that sequence.</param>
        /// <param name="ut">Value of Universal Time, using 24 hour notation.</param>
        /// <param name="correctionForDay">Correction for day, due to time overflow. Poswsible values -1, 0, +1.</param>
        /// <param name="timeFullText">Text for the time, includes texts between [] that needs to be replaced with texts from Rosetta.</param>
        public FullTime(int[] hourMinuteSecond, double ut, int correctionForDay, string timeFullText)
        {
            HourMinuteSecond = hourMinuteSecond;
            Ut = ut;
            CorrectionForDay = correctionForDay;
            TimeFullText = timeFullText;
        }

    }

    /// <summary>
    /// Date/time related data.
    /// </summary>
    public class FullDateTime
    {
        public readonly string DateText;
        public readonly string TimeText;
        public readonly double JulianDayForEt;

        /// <summary>
        /// Constructor for FullDateTime, using predefined values.
        /// </summary>
        /// <param name="dateText">Textual presentation for the date.</param>
        /// <param name="timeText">Textual presentation for the time.</param>
        /// <param name="julianDayForEt">Julian Day for ephemeris time.</param>
        public FullDateTime(string dateText, string timeText, double julianDayForEt)
        {
            DateText = dateText;
            TimeText = timeText;
            JulianDayForEt = julianDayForEt;
        }
    }

    /// <summary>
    /// Data for a chart.
    /// </summary>
    /// <remarks>
    /// Data required for calculations and data to be shown to the user. Does not contain the astronomical positions.
    /// </remarks>
    public record ChartData
    {
        public readonly int Id;
        public int TempId { get; set; }
        public readonly MetaData ChartMetaData;
        public readonly Location ChartLocation;
        public readonly FullDateTime ChartDateTime;

        /// <summary>
        /// Constructor for record ChartData.
        /// </summary>
        /// <param name="id">Unique id that also serves as a primary key in the database.</param>
        /// <param name="tempId">Temporary id, unique within the set of charts that are currently avaiable to the user.</param>
        /// <param name="metaData">Metadata for this chart.</param>
        /// <param name="location">Location related data.</param>
        /// <param name="fullDateTime">Date/time related data.</param>
        public ChartData(int id, int tempId, MetaData metaData, Location location, FullDateTime fullDateTime)
        {
            Id = id;
            TempId = tempId;
            ChartMetaData = metaData;
            ChartLocation = location;
            ChartDateTime = fullDateTime;
        }
    }








}
