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
