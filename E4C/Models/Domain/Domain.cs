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
    /// Horizontal coordinates.
    /// </summary>
    public record HorizontalPos
    {
        public readonly double Azimuth;
        public readonly double Altitude;

        /// <summary>
        /// Constructor for Horizontal Coordinates.
        /// </summary>
        /// <param name="azimuth">Azimuth.</param>
        /// <param name="altitude">Altitude.</param>
        public HorizontalPos(double azimuth, double altitude)
        {
            Azimuth = azimuth;
            Altitude = altitude;
        }
    }

    /// <summary>
    /// Full position for  a cusp or other mundane point.
    /// </summary>
    public record CuspFullPos
    {
        public readonly double Longitude;
        public readonly double RightAscension;
        public readonly double Declination;
        public readonly HorizontalPos AzimuthAltitude;

        /// <summary>
        /// Constructor for the full position of a cusp/mundane point.
        /// </summary>
        /// <param name="longitude">Longitude.</param>
        /// <param name="rightAscension">Right ascension.</param>
        /// <param name="declination">Declination.</param>
        /// <param name="azimuthAltitude">Horizontal coordinates.</param>
        public CuspFullPos(double longitude, double rightAscension, double declination, HorizontalPos azimuthAltitude)
        {
            Longitude = longitude;
            RightAscension = rightAscension;
            Declination = declination;
            AzimuthAltitude = azimuthAltitude;
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
    public class Location
    {
        public readonly string LocationFullName;
        public readonly double GeoLong;
        public readonly double GeoLat;
        public readonly Directions4GeoLong DirLong;
        public readonly Directions4GeoLat DirLat;

        /// <summary>
        /// Constructor for Location.
        /// </summary>
        /// <param name="locationFullName">Name and sexagesimal coordinatevalues for a location. Directions are defined between [] and need to be replaced with texts from Rosetta.</param>
        /// <param name="geoLong">Value for geographic longitude.</param>
        /// <param name="geoLat">Value for geographic latitude.</param>
        public Location(string locationFullName, double geoLong, double geoLat)
        {
            LocationFullName = locationFullName;
            GeoLong = geoLong;
            GeoLat = geoLat;
            DirLong = GeoLong >= 0.0 ? Directions4GeoLong.East : Directions4GeoLong.West;
            DirLat = GeoLat >= 0.0 ? Directions4GeoLat.North : Directions4GeoLat.South;
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
    /// Data for a request to calculate a full chart.
    /// </summary>
    public record FullChartRequest
    {
        public readonly double JulianDayUt;
        public readonly Location ChartLocation;
        public readonly List<SolarSystemPoints> SolarSystemPoints;
        public readonly HouseSystems HouseSystem;
        public readonly ZodiacTypes ZodiacType;
        public readonly Ayanamshas Ayanamsha;
        public readonly ObserverPositions ObserverPosition;
        public readonly ProjectionTypes ProjectionType;

        /// <summary>
        /// Constructor for the record FullChartRequest.
        /// </summary>
        /// <param name="julianDayUt">Julian day for universal time.</param>
        /// <param name="location">Location (only latitude and longitude are used).</param>
        /// <param name="solarSystemPoints">List with the Solar System Points to calculate.</param>
        /// <param name="houseSystem">The preferred house system.</param>
        /// <param name="zodiacType">The zodiac type: tropical or sidereal.</param>
        /// <param name="ayanamsha">The ayanamsha to be applied.</param>
        /// <param name="observerPosition">Observer position (geocentric, topocentric, heliocentric).</param>
        /// <param name="projectionType">Projection type (standard or oblique longitude).</param>
        public FullChartRequest(double julianDayUt, Location location, List<SolarSystemPoints> solarSystemPoints, HouseSystems houseSystem,
            ZodiacTypes zodiacType, Ayanamshas ayanamsha, ObserverPositions observerPosition, ProjectionTypes projectionType)
        {
            JulianDayUt = julianDayUt;
            ChartLocation = location;
            SolarSystemPoints = solarSystemPoints;
            HouseSystem = houseSystem;
            ZodiacType = zodiacType;
            Ayanamsha = ayanamsha;
            ObserverPosition = observerPosition;
            ProjectionType = projectionType;
        }
    }

    /// <summary>
    /// Results of calculation for mundane positions (cusps, asc. mc, vertex, eastpoint).
    /// </summary>
    public record MundanePositions
    {
        public readonly List<CuspFullPos> Cusps;
        public readonly CuspFullPos Mc;
        public readonly CuspFullPos Ascendant;
        public readonly CuspFullPos Vertex;
        public readonly CuspFullPos EastPoint;

        /// <summary>
        /// Constructor for record CalculatedMundanePositions.
        /// </summary>
        /// <param name="cusps">List with full positions for cusps, in the sequence 1 ..n. </param>
        /// <param name="mc">Full position for the Mc.</param>
        /// <param name="ascendant">Full position for the ascendant.</param>
        /// <param name="vertex">Full position for the vertex.</param>
        /// <param name="eastpoint">Full position for the eastpoint.</param>
        public MundanePositions(List<CuspFullPos> cusps, CuspFullPos mc, CuspFullPos ascendant, CuspFullPos vertex, CuspFullPos eastpoint)
        {
            Cusps = cusps;
            Mc = mc;
            Ascendant = ascendant;
            Vertex = vertex;
            EastPoint = eastpoint;
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
    /// Complete calcualtion results for a full chart.
    /// </summary>
    public record FullChartResponse
    {
        public readonly List<FullSolSysPointPos> SolarSystemPointPositions;
        public readonly MundanePositions MundanePositions;

        /// <summary>
        /// Constructor for record FullChartResponse.
        /// </summary>
        /// <param name="solarSystemPointPositions">List with calcualted positions for Solar System Points.</param>
        /// <param name="mundanePositions">Calcualted mundane positions.</param>
        public FullChartResponse(List<FullSolSysPointPos> solarSystemPointPositions, MundanePositions mundanePositions)
        {
            SolarSystemPointPositions = solarSystemPointPositions;
            MundanePositions = mundanePositions;
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
