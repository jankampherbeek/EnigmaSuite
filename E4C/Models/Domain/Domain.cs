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
        public readonly int year, month, day;
        public readonly double ut;
        public readonly Calendars calendar;

        public SimpleDateTime(int year, int month, int day, double ut, Calendars calendar)
        {
            this.year = year;
            this.month = month;
            this.day = day;
            this.ut = ut;
            this.calendar = calendar;
        }
    }

    /// <summary>
    /// Wrapper for a calculated double with error information.
    /// </summary>
    public record ResultForDouble
    {
        public readonly double returnValue;
        public readonly bool noErrors;
        public readonly string errorText = "";

        public ResultForDouble(double returnValue, bool noErrors, string errorText = "")
        {
            this.returnValue = returnValue;
            this.noErrors = noErrors;
            this.errorText = errorText;
        }
    }

    /// <summary>
    /// Wrapper for a date with error information.
    /// </summary>
    public record ValidatedDate
    {
        public readonly int year;
        public readonly int month;
        public readonly int day;
        public readonly Calendars calendar;
        public readonly List<int> errorCodes;

        public ValidatedDate(int year, int month, int day, Calendars calendar, List<int> errorCodes)
        {
            this.year = year;
            this.month = month;
            this.day = day;
            this.calendar = calendar;
            this.errorCodes = errorCodes;
        }
    }

    /// <summary>
    /// Wrapper for time with error information. Time should always be in UT.
    /// </summary>
    public record ValidatedUniversalTime
    {
        public readonly int hour;
        public readonly int minute;
        public readonly int second;
        public readonly List<int> errorCodes;

        public ValidatedUniversalTime(int hour, int minute, int second, List<int> errorCodes)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
            this.errorCodes = errorCodes;
        }

    }

    /// <summary>
    /// Combination of position and speed (for a solar system point).
    /// </summary>
    public record PosSpeed
    {
        public readonly double position;
        public readonly double speed;

        public PosSpeed(double position, double speed)
        {
            this.position = position;
            this.speed = speed;
        }
    }

    /// <summary>
    /// Position, speed and distance in a coordinatesystem for point in the Solar system.
    /// </summary>
    public record SolSysPointPosSpeeds
    {
        public readonly PosSpeed mainPosSpeed;
        public readonly PosSpeed deviationPosSpeed;
        public readonly PosSpeed distancePosSpeed;

        public SolSysPointPosSpeeds(double[] values)
        {
            if (values.Length != 6) throw new ArgumentException("Wrong numer of values for SolSysPointSpeeds.");
            mainPosSpeed = new PosSpeed(values[0], values[1]);
            deviationPosSpeed = new PosSpeed(values[2], values[3]);
            distancePosSpeed = new PosSpeed(values[4], values[5]);
        }

        public SolSysPointPosSpeeds(PosSpeed mainPosSpeed, PosSpeed deviationPosSpeed, PosSpeed distancePosSpeed)
        {
            this.mainPosSpeed = mainPosSpeed;
            this.deviationPosSpeed = deviationPosSpeed;
            this.distancePosSpeed = distancePosSpeed;
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
            int flags = 0 | Constants.SEFLG_SWIEPH | Constants.SEFLG_SPEED;
            if (coordinateSystem == CoordinateSystems.Equatorial) flags |= Constants.SEFLG_EQUATORIAL;
            if (observerPosition == ObserverPositions.HelioCentric) flags |= Constants.SEFLG_HELCTR;
            if (observerPosition == ObserverPositions.TopoCentric) flags |= Constants.SEFLG_TOPOCTR;
            if (zodiacType == ZodiacTypes.Sidereal) flags |= Constants.SEFLG_SIDEREAL;
            return flags;
        }

    }
}
