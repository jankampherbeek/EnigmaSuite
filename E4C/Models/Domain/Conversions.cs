// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Astron;
using E4C.domain.shared.references;
using System;


namespace E4C.Models.Domain
{

    public interface ISexagesimalConversions
    {
        /// <summary>
        /// Convert input values for geographic longitude to a double indicating the longitude.
        /// </summary>
        /// <param name="inputLong">String array with degrees, minutes and seconds, in that sequence.</param>
        /// <param name="direction">North results in a positive value, south in a negative value.</param>
        /// <returns>A double representing the value. Positive for north, negative for south.</returns>
        public double InputGeoLongToDouble(string[] inputLong, Directions4GeoLong direction);

        /// <summary>
        /// Convert input values for geographic latitude to a double indicating the latitude.
        /// </summary>
        /// <param name="inputLat">String array with degrees, minutes and seconds, in that sequence.</param>
        /// <param name="direction">East results in a positive value, west in a negative value.</param>
        /// <returns>A double representing the value. Positive for east, negative for west.</returns>
        public double InputGeoLatToDouble(string[] inputLat, Directions4GeoLat direction);

        /// <summary>
        /// Convert input values for time to a double with the hour and fraction.
        /// </summary>
        /// <param name="inputTime">String array with hours, minnutes and seconds, in that sequence.</param>
        /// <returns>A double representing the hour and fractions of the hour.</returns>
        public double InputTimeToDoubleHours(string[] inputTime);
    }

    public interface IDateConversions
    {
        /// <summary>
        /// Convert inputvalues for a date to a Julian Day number. 
        /// </summary>
        /// <param name="inputDate">String array with year, month and day, in that sequence.</param>
        /// <param name="calendar">Value from enum Calendars.</param>
        /// <param name="yearCount">Value from enum YearCounts.</param>
        /// <returns>The Julian day umber for UT 0:00:00 of the given date.</returns>
        public double InputDateToJdNr(string[] inputDate, Calendars calendar, YearCounts yearCount);

        /// <summary>
        /// Convert string array for year, month and day to an int array. 
        /// </summary>
        /// <param name="inputDate">String array with year, month and day, in that sequence.</param>
        /// <returns>Int array with year, month and day, in that sequence.</returns>
        public int[] InputDateToDecimals(string[] inputDate);

    }

    public class SexagesimalConversions : ISexagesimalConversions
    {
        public double InputGeoLatToDouble(string[] inputLat, Directions4GeoLat direction)
        {
            return SexagesimalToDouble(inputLat) * (int)direction;
        }

        public double InputGeoLongToDouble(string[] inputLong, Directions4GeoLong direction)
        {
            return SexagesimalToDouble(inputLong) * (int)direction;
        }

        public double InputTimeToDoubleHours(string[] inputTime)
        {
            return SexagesimalToDouble(inputTime);
        }

        private static double SexagesimalToDouble(string[] texts)
        {
            try
            {
                double value1 = double.Parse(texts[0]);
                double value2 = double.Parse(texts[1]);
                double value3 = double.Parse(texts[2]);
                return value1 + value2 / Constants.MINUTES_PER_HOUR_DEGREE + value3 / Constants.SECONDS_PER_HOUR_DEGREE;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error converting to decimal geoLong, using values : " + texts.ToString() + ". Original exception message : " + e.Message);
            }
        }
    }

    public class DateConversions : IDateConversions
    {
        readonly private ICalendarCalc _calendarCalc;


        public DateConversions(ICalendarCalc calendarCalc)
        {
            _calendarCalc = calendarCalc;
        }

        public int[] InputDateToDecimals(string[] inputDate)
        {
            try
            {
                int value1 = int.Parse(inputDate[0]);
                int value2 = int.Parse(inputDate[1]);
                int value3 = int.Parse(inputDate[2]);
                return new int[] { value1, value2, value3 };
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error converting strings for a date to decimal, using values : " + inputDate.ToString() + ". Original exception message : " + e.Message);
            }
        }

        public double InputDateToJdNr(string[] inputDate, Calendars calendar, YearCounts yearCount)
        {
            int[] _dateValues = InputDateToDecimals(inputDate);
            double _ut = 0.0;
            if (yearCount == YearCounts.BCE)
            {
                _dateValues[0]++;
            }
            SimpleDateTime _simpleDateTime = new(_dateValues[0], _dateValues[1], _dateValues[2], _ut, calendar);

            ResultForDouble _jdResult = _calendarCalc.CalculateJd(_simpleDateTime);
            if (_jdResult.NoErrors)
            {
                return _jdResult.ReturnValue;
            }
            throw new ArgumentException("Error calculating JD while converting inputdate to Jdnr. Using values : " + _simpleDateTime.ToString() + " and Calendar : " + calendar);
        }
    }

    public static class RangeUtil
    {
        public static double ValueToRange(double testValue, double lowerLimit, double upperLimit)
        {
            if ((upperLimit <= lowerLimit))
            {
                throw new ArgumentException("UpperRange: " + upperLimit + " <+ lowerLimit: " + lowerLimit);
            }
            return ForceToRange(testValue, lowerLimit, upperLimit);
        }

        private static double ForceToRange(double testValue, double lowerLimit, double upperLimit)
        {
            double rangeSize = upperLimit - lowerLimit;
            double checkedForLowerLimit = ForceLowerLimit(lowerLimit, rangeSize, testValue);
            return ForceUpperLimit(upperLimit, rangeSize, checkedForLowerLimit);
        }

        private static double ForceLowerLimit(double lowerLimit, double rangeSize, double toCheck)
        {
            double checkedForUpperLimit = toCheck;
            while (checkedForUpperLimit < lowerLimit)
            {
                checkedForUpperLimit += rangeSize;
            }
            return checkedForUpperLimit;
        }

        private static double ForceUpperLimit(double upperLimit, double rangeSize, double toCheck)
        {
            double checkedForLowerLimit = toCheck;
            while (checkedForLowerLimit >= upperLimit)
            {
                checkedForLowerLimit -= rangeSize;
            }
            return checkedForLowerLimit;
        }
    }

    public static class DegreeRadianUtil
    {
        public static double RadToDeg(double radians)
        {
            return (180 / Math.PI) * radians;
        }

        public static double DegToRad(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }
    }


}



