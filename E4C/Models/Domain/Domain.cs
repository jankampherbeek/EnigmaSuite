// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using E4C.domain.shared.positions;
using E4C.domain.shared.specifications;
using E4C.domain.shared.references;
using domain.shared;

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
    /// Ecliptic position.
    /// </summary>
    public record EclipticCoordinates
    {
        public readonly double Longitude;
        public readonly double Latitude;

        /// <summary>
        /// Constructor for record EclipticCoordinates.
        /// </summary>
        /// <param name="longitude">Ecliptic longitude.</param>
        /// <param name="latitude">Ecliptic latitude.</param>
        public EclipticCoordinates(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }

    /// <summary>
    /// Ecliptic position for a specific Solar system point.
    /// </summary>
    public record NamedEclipticCoordinates
    {
        public readonly SolarSystemPoints SolarSystemPoint;
        public readonly EclipticCoordinates EclipticCoordinates;

        /// <summary>
        /// Constrctor for record NamedEclipticCoordinates.
        /// </summary>
        /// <param name="solarSystemPoint">Instance of enum SolarSystemPoints.</param>
        /// <param name="eclipticCoordinate">Ecliptic position.</param>
        public NamedEclipticCoordinates(SolarSystemPoints solarSystemPoint, EclipticCoordinates eclipticCoordinate)
        {
            SolarSystemPoint = solarSystemPoint;
            EclipticCoordinates = eclipticCoordinate;
        }
    }

    /// <summary>
    /// Ecliptic longitude for a specific Solar system point.
    /// </summary>
    public record NamedEclipticLongitude
    {
        public readonly SolarSystemPoints SolarSystemPoint;
        public readonly double EclipticLongitude;

        /// <summary>
        /// Constructor for record NamedEdclipticLongitude.
        /// </summary>
        /// <param name="solarSystemPoint">Instance of enum SolarSystemPoints.</param>
        /// <param name="eclipticLongitude">Ecliptic longitude.</param>
        public NamedEclipticLongitude(SolarSystemPoints solarSystemPoint, double eclipticLongitude)
        {
            SolarSystemPoint = solarSystemPoint;
            EclipticLongitude = eclipticLongitude;
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



    /// <summary>
    /// Data for a request to calculate oblique longitudes.
    /// </summary>
    public record ObliqueLongitudeRequest
    {
        public readonly double Armc;
        public readonly double Obliquity;
        public readonly double GeoLat;
        public readonly List<NamedEclipticCoordinates> SolSysPointCoordinates;

        /// <summary>
        /// Constructor for the record ObliqueLongitudeRequest.
        /// </summary>
        /// <param name="armc">Right ascension of the MC (in degrees).</param>
        /// <param name="obliquity">True obliquity of the earths axis.</param>
        /// <param name="geoLat">Geographic latitude.</param>
        /// <param name="solSysPointCoordinates">Solar system for which to calculate the oblique longitude, incoluding their ecliptical coordinates.</param>
        public ObliqueLongitudeRequest(double armc, double obliquity, double geoLat, List<NamedEclipticCoordinates> solSysPointCoordinates)
        {
            Armc = armc;
            Obliquity = obliquity;
            GeoLat = geoLat;
            SolSysPointCoordinates = solSysPointCoordinates;
        }

    }

 



    /// <summary>
    /// Results of calculation for a single Solar System Point.
    /// </summary>
    public record FullSolSysPointPos
    {
        public readonly SolarSystemPoints SolarSystemPoint;
        public readonly PosSpeed Longitude;
        public readonly PosSpeed Latitude;
        public readonly PosSpeed RightAscension;
        public readonly PosSpeed Declination;
        public readonly PosSpeed Distance;
        public readonly HorizontalPos AzimuthAltitude;

        /// <summary>
        /// Constructor for a fully defined Solar system point.
        /// </summary>
        /// <param name="solarSystemPoint">Instance of the enum SolarSystemPoints.</param>
        /// <param name="longitude">Longitude in degrees.</param>
        /// <param name="latitude">Latitude in degrees.</param>
        /// <param name="rightAscension">Right ascension in degrees.</param>
        /// <param name="declination">Declination in degrees.</param>
        /// <param name="distance">distance in AU.</param>
        /// <param name="azimuthAltitude">Azimuth and altitude in degrees.</param>
        public FullSolSysPointPos(SolarSystemPoints solarSystemPoint, PosSpeed longitude, PosSpeed latitude, PosSpeed rightAscension,
            PosSpeed declination, PosSpeed distance, HorizontalPos azimuthAltitude)
        {
            SolarSystemPoint = solarSystemPoint;
            Longitude = longitude;
            Latitude = latitude;
            RightAscension = rightAscension;
            Declination = declination;
            Distance = distance;
            AzimuthAltitude = azimuthAltitude;
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
