// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;

namespace E4C.Models.Domain
{

    /// <summary>
    /// Representation for a date and time, including calendar.
    /// </summary>
    /// <remarks>
    /// For ut (Universal Time) add the time in 0..23 hours and a decimal fraction for the total of minutes and seconds.
    /// </remarks>
    public record SimpleDateTime
    {
        public readonly int Year, Month, Day;
        public readonly double Ut;
        public readonly Calendars Calendar;

        public SimpleDateTime(int year, int month, int day, double ut, Calendars calendar)
        {
            Year = year;
            Month = month;
            Day = day;
            Ut = ut;
            Calendar = calendar;
        }
    }

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
    /// Combination of position and speed (for a solar system point).
    /// </summary>
    public record PosSpeed
    {
        public readonly double Position;
        public readonly double Speed;

        public PosSpeed(double position, double speed)
        {
            Position = position;
            Speed = speed;
        }
    }

    /// <summary>
    /// Position, speed and distance in a coordinatesystem for point in the Solar system.
    /// </summary>
    public record SolSysPointPosSpeeds
    {
        public readonly PosSpeed MainPosSpeed;
        public readonly PosSpeed DeviationPosSpeed;
        public readonly PosSpeed DistancePosSpeed;

        public SolSysPointPosSpeeds(double[] values)
        {
            if (values.Length != 6) throw new ArgumentException("Wrong numer of values for SolSysPointSpeeds.");
            MainPosSpeed = new PosSpeed(values[0], values[1]);
            DeviationPosSpeed = new PosSpeed(values[2], values[3]);
            DistancePosSpeed = new PosSpeed(values[4], values[5]);
        }

        public SolSysPointPosSpeeds(PosSpeed mainPosSpeed, PosSpeed deviationPosSpeed, PosSpeed distancePosSpeed)
        {
            MainPosSpeed = mainPosSpeed;
            DeviationPosSpeed = deviationPosSpeed;
            DistancePosSpeed = distancePosSpeed;
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
    /// Location related data.
    /// </summary>
    public record Location
    {
        public readonly string LocationFullName;
        public readonly double GeoLong;
        public readonly double GeoLat;

        /// <summary>
        /// Constructor for record Location
        /// </summary>
        /// <param name="locationFullName">Name and sexagesimal coordinatevalues for a location.</param>
        /// <param name="geoLong">Value for geographic longitude.</param>
        /// <param name="geoLat">Value for geographic latitude.</param>
        public Location(string locationFullName, double geoLong, double geoLat)
        {
            LocationFullName = locationFullName;
            GeoLong = geoLong;
            GeoLat = geoLat;
        }
    }

    /// <summary>
    /// Date/time related data.
    /// </summary>
    public record FullDateTime
    {
        public readonly string DateText;
        public readonly string TimeText;
        public readonly double JulianDayForEt;
        public readonly SimpleDateTime DateTime;

        /// <summary>
        /// Constructor for record FullDateTime.
        /// </summary>
        /// <param name="dateText">Textual presentation for the date.</param>
        /// <param name="timeText">Textual presentation for the time.</param>
        /// <param name="julianDayForEt">Julian Day for ephemeris time.</param>
        /// <param name="dateTime">Instance of SimpleDateTime with values for date and time.</param>
        public FullDateTime(string dateText, string timeText, double julianDayForEt, SimpleDateTime dateTime)
        {
            DateText = dateText;
            TimeText = timeText;
            JulianDayForEt = julianDayForEt;
            DateTime = dateTime;
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

    /// <summary>
    /// Combines the flags for the Swiss Ephemeris to a single value.
    /// </summary>
    public class SeFlags
    {
        public static int DefineFlags(CoordinateSystems coordinateSystem, ObserverPositions observerPosition, ZodiacTypes zodiacType)
        {
            // Always use Swiss Ephemeris files and always calculate speed.
            int _flags = 0 | Constants.SEFLG_SWIEPH | Constants.SEFLG_SPEED;
            if (coordinateSystem == CoordinateSystems.Equatorial) _flags |= Constants.SEFLG_EQUATORIAL;
            if (observerPosition == ObserverPositions.HelioCentric) _flags |= Constants.SEFLG_HELCTR;
            if (observerPosition == ObserverPositions.TopoCentric) _flags |= Constants.SEFLG_TOPOCTR;
            if (zodiacType == ZodiacTypes.Sidereal) _flags |= Constants.SEFLG_SIDEREAL;
            return _flags;
        }
    }


}
