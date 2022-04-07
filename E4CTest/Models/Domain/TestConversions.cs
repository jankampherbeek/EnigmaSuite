// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using E4C.Models.Astron;
using E4C.Models.Domain;
using E4C.domain.shared.references;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using E4C.calc.util;

namespace ConversionsTest
{
    [TestClass]
    public class TestSexagesimalConversions
    {
        readonly private ISexagesimalConversions conversions = new SexagesimalConversions();
        readonly private double delta = 0.00000001;

        [TestMethod]
        public void TestInputGeoLatToDoubleHappyFlow()
        {
            string[] inputLat = new string[] { "52", "13", "0" };
            Directions4GeoLat direction = Directions4GeoLat.North;
            double expected = 52.2166666667;
            Assert.AreEqual(expected, conversions.InputGeoLatToDouble(inputLat, direction), delta);
        }

        [TestMethod]
        public void TestInputGeoLatToDoubleWest()
        {
            string[] inputLat = new string[] { "52", "13", "0" };
            Directions4GeoLat direction = Directions4GeoLat.South;
            double expected = -52.2166666667;
            Assert.AreEqual(expected, conversions.InputGeoLatToDouble(inputLat, direction), delta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInputGeoLatToDoubleError()
        {
            string[] inputLat = new string[] { "xx", "13", "0" };
            Directions4GeoLat direction = Directions4GeoLat.North;
            _ = conversions.InputGeoLatToDouble(inputLat, direction);
        }

        [TestMethod]
        public void TestInputGeoLongToDoubleHappyFlow()
        {
            string[] inputLong = new string[] { "12", "15", "20" };
            Directions4GeoLong direction = Directions4GeoLong.East;
            double expected = 12.25555555556;
            Assert.AreEqual(expected, conversions.InputGeoLongToDouble(inputLong, direction), delta);
        }

        [TestMethod]
        public void TestInputGeoLongToDoubleWest()
        {
            string[] inputLong = new string[] { "12", "15", "20" };
            Directions4GeoLong direction = Directions4GeoLong.West;
            double expected = -12.25555555556;
            Assert.AreEqual(expected, conversions.InputGeoLongToDouble(inputLong, direction), delta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInputGeoLongToDoubleError()
        {
            string[] inputLong = new string[] { "12", "x", "20" };
            Directions4GeoLong direction = Directions4GeoLong.West;
            _ = conversions.InputGeoLongToDouble(inputLong, direction);
        }

        [TestMethod]
        public void TestInputTimeToDoubleHoursHappyFlow()
        {
            string[] inputTime = new string[] { "8", "37", "30" };
            double expected = 8.625;
            Assert.AreEqual(expected, conversions.InputTimeToDoubleHours(inputTime), delta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInputTimeToDoubleHoursError()
        {
            string[] inputTime = new string[] { "8", "xx", "30" };
            _ = conversions.InputTimeToDoubleHours(inputTime);
        }
    }

    [TestClass]
    public class TestDateConversions
    {
        readonly private double delta = 0.00000001;

        [TestMethod]
        public void TestInputDateToJdNrHappyFlow()
        {
            string[] inputDate = new string[] { "1953", "1", "29" };
            var calendar = Calendars.Gregorian;
            var yearCount = YearCounts.CE;
            double jdnr = 2434406.5;
            var mock = new Mock<ICalendarCalc>();
            var jdResult = new ResultForDouble(jdnr, true);
            mock.Setup(p => p.CalculateJd(new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian))).Returns(jdResult);
            DateConversions conversions = new(mock.Object);
            Assert.AreEqual(jdnr, conversions.InputDateToJdNr(inputDate, calendar, yearCount), delta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInputDateToJdNrError()
        {
            string[] inputDate = new string[] { "1953", "Jan", "29" };
            var calendar = Calendars.Gregorian;
            var yearCount = YearCounts.CE;
            double jdnr = 2434406.5;
            var mock = new Mock<ICalendarCalc>();
            var jdResult = new ResultForDouble(jdnr, true);
            mock.Setup(p => p.CalculateJd(new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian))).Returns(jdResult);
            DateConversions conversions = new(mock.Object);
            double dummyValue = conversions.InputDateToJdNr(inputDate, calendar, yearCount);
        }

        [TestMethod]
        public void TestInputDateToDecimalsHappyFlow()
        {
            string[] inputDate = new string[] { "1953", "1", "29" };
            var mock = new Mock<ICalendarCalc>();
            DateConversions conversions = new(mock.Object);
            int[] dateResult = conversions.InputDateToDecimals(inputDate);
            Assert.AreEqual(1953, dateResult[0]);
            Assert.AreEqual(1, dateResult[1]);
            Assert.AreEqual(29, dateResult[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInputDateToDecimalsError()
        {
            string[] inputDate = new string[] { "xx", "1", "29" };
            var mock = new Mock<ICalendarCalc>();
            DateConversions conversions = new(mock.Object);
            _ = conversions.InputDateToDecimals(inputDate);
        }

    }

    [TestClass]
    public class TestRangeUtilNoChange
    {
        private double _delta = 0.00000001;

        [TestMethod]
        public void TestNoChange()
        {
            double testValue = 12.0;
            double expectedValue = 12.0;
            double lowerLimit = 0.0;
            double upperLimit = 360.0;
            Assert.AreEqual(expectedValue, RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), _delta);
        }

        [TestMethod]
        public void TestTooLow()
        {
            double testValue = -10.0;
            double expectedValue = 350.0;
            double lowerLimit = 0.0;
            double upperLimit = 360.0;
            Assert.AreEqual(expectedValue, RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), _delta);
        }

        [TestMethod]
        public void TestTooHigh()
        {
            double testValue = 410.0;
            double expectedValue = 50.0;
            double lowerLimit = 0.0;
            double upperLimit = 360.0;
            Assert.AreEqual(expectedValue, RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), _delta);
        }

        [TestMethod]
        public void TestOnLowerLimit()
        {
            double testValue = 0.0;
            double expectedValue = 0.0;
            double lowerLimit = 0.0;
            double upperLimit = 360.0;
            Assert.AreEqual(expectedValue, RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), _delta);
        }

        [TestMethod]
        public void TestOnUpperLimit()
        {
            double testValue = 360.0;
            double expectedValue = 0.0;
            double lowerLimit = 0.0;
            double upperLimit = 360.0;
            Assert.AreEqual(expectedValue, RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), _delta);
        }

        [TestMethod]
        public void TestPositiveNegativeRangeNoChange()
        {
            double testValue = -45.0;
            double expectedValue = -45.0;
            double lowerLimit = -90.0;
            double upperLimit = 90.0;
            Assert.AreEqual(expectedValue, RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), _delta);
        }

        [TestMethod]
        public void TestPositiveNegativeRangeTooLow()
        {
            double testValue = -100.0;
            double expectedValue = 80.0;
            double lowerLimit = -90.0;
            double upperLimit = 90.0;
            Assert.AreEqual(expectedValue, RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), _delta);
        }

        [TestMethod]
        public void TestPositiveNegativeRangeTooHigh()
        {
            double testValue = 100.0;
            double expectedValue = -80.0;
            double lowerLimit = -90.0;
            double upperLimit = 90.0;
            Assert.AreEqual(expectedValue, RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), _delta);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWrongSequenceLOwerUpper()
        {
            double testValue = 100.0;
            double expectedValue = -80.0;
            double lowerLimit = 0.0;
            double upperLimit = 360.0;
            _ = RangeUtil.ValueToRange(testValue, upperLimit, lowerLimit);
        }




    }

    [TestClass]
    public class TestDegreeRadianUtil
    {
        double _delta = 0.00000001;

        [TestMethod]
        public void TestDegrees2Radians()
        {
            double degrees = 100.0;
            double expectedRadians = 1.74532925199;
            Assert.AreEqual(expectedRadians, MathExtra.DegToRad(degrees), _delta);
        }

        [TestMethod]
        public void TestRadians2Degrees()
        {
            double radians = 2.0;
            double expectedDegrees = 114.591559026;
            Assert.AreEqual(expectedDegrees, MathExtra.RadToDeg(radians), _delta);
        }
    }
}