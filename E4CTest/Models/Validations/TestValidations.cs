// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Astron;
using E4C.Models.Domain;
using E4C.Models.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace E4CTest
{
    [TestClass]
    public class TestDateTimeValidations
    {

        [TestMethod]
        public void TestDateHappyFlow()
        {
            string[] inputDate = new string[] {"1953", "1", "29" }; 
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian))).Returns(true);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateDate(inputDate, Calendars.Gregorian, YearCounts.CE);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestDateNonNumerics()
        {
            string[] inputDate = new string[] { "1953", "Januar", "29" };
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian))).Returns(true);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateDate(inputDate, Calendars.Gregorian, YearCounts.CE);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_DATE, errors[0]);
        }

        [TestMethod]
        public void TestDateTwoElements()
        {

            string[] inputDate = new string[] { "1953", "1", "" };

            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian))).Returns(true);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateDate(inputDate, Calendars.Gregorian, YearCounts.CE);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_DATE, errors[0]);
        }

        [TestMethod]
        public void TestDateLeapYear()
        {
            string[] inputDate = new string[] { "2024", "2", "29" };
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(2024, 2, 29, 0.0, Calendars.Gregorian))).Returns(true);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateDate(inputDate, Calendars.Gregorian, YearCounts.CE);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestDateNoLeapYear()
        {
            string[] inputDate = new string[] { "2023", "2", "29" };
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(2023, 2, 29, 0.0, Calendars.Gregorian))).Returns(false);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateDate(inputDate, Calendars.Gregorian, YearCounts.CE);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_DATE, errors[0]);
        }

        [TestMethod]
        public void TestTimeHappyFlow()
        {
            string[] inputTime = new string[] { "10", "30", "0"};
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(inputTime);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestTimeDefaultSeconds()
        {
            string[] inputTime = new string[] { "10", "30", "" };
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(inputTime);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestNonNumeric()
        {
            string[] inputTime = new string[] { "10", "30", "ab" };
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(inputTime);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_TIME, errors[0]);
        }

        [TestMethod]
        public void TestRangeTooLarge()
        {
            string[] inputTime = new string[] { "24", "0", "0" };
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(inputTime);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_TIME, errors[0]);
        }

        [TestMethod]
        public void TestRangeTooSmall()
        { 
            string[] inputTime = new string[] { "-1", "0", "0" };
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(inputTime);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_TIME, errors[0]);
        }

        [TestMethod]
        public void TestElementsOutOfRange()
        {
            string[] inputTime = new string[] { "10", "60", "0" };
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(inputTime);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_TIME, errors[0]);
        }

    }

    [TestClass]
    public class TestLocationValidations
    {
        [TestMethod]
        public void TestLongitudeHappyFlow()
        {
            string[] geoLongInput = { "6", "54", "0"};
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLongitude(geoLongInput);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestLongitudeNonNumerics()
        {
            string[] geoLongInput = { "6", "abc", "0" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLongitude(geoLongInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLON, errors[0]);
        }

        [TestMethod]
        public void TestLongitudeSecondsEmpty()
        {
            string[] geoLongInput = { "6", "54", "" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLongitude(geoLongInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLON, errors[0]);
        }

        [TestMethod]
        public void TestLongitudeDegreesTooSmall()
        {
            string[] geoLongInput = { "-1", "54", "0" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLongitude(geoLongInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLON, errors[0]);
        }

        [TestMethod]
        public void TestLongitudeDegreesTooLarge()
        {
            string[] geoLongInput = { "200", "54", "0" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLongitude(geoLongInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLON, errors[0]);
        }

        [TestMethod]
        public void TestLongitudeMinutesTooSmall()
        {
            string[] geoLongInput = { "6", "-54", "0" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLongitude(geoLongInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLON, errors[0]);
        }

        [TestMethod]
        public void TestLongitudeMinutesTooLarge()
        {
            string[] geoLongInput = { "6", "154", "0" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLongitude(geoLongInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLON, errors[0]);
        }

        [TestMethod]
        public void TestLongitudeSecondsTooSmall()
        {
            string[] geoLongInput = { "6", "54", "-10" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLongitude(geoLongInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLON, errors[0]);
        }

        [TestMethod]
        public void TestLongitudeSecondsTooLarge()
        {
            string[] geoLongInput = { "6", "54", "60" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLongitude(geoLongInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLON, errors[0]);
        }

        [TestMethod]
        public void TestLongitudeMaxValues()
        {
            string[] geoLongInput = { "180", "0", "0" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLongitude(geoLongInput);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestLongitudeMinValues()
        {
            string[] geoLongInput = { "0", "0", "0" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLongitude(geoLongInput);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestLongitudeMaxPlusOneSecond()
        {
            string[] geoLongInput = { "180", "0", "1" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLongitude(geoLongInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLON, errors[0]);
        }

        [TestMethod]
        public void TestLatitudeHappyFlow()
        {
            string[] geoLatInput = { "52", "13", "10" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLatitude(geoLatInput);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestLatitudeNonNumerics()
        {
            string[] geoLatInput = { "x", "13", "10" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLatitude(geoLatInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLAT, errors[0]);
        }

        [TestMethod]
        public void TestLatitudeSecondsEmpty()
        {
            string[] geoLatInput = { "52", "13", "" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLatitude(geoLatInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLAT, errors[0]);
        }

        [TestMethod]
        public void TestLatitudeDegreesTooSmall()
        {
            string[] geoLatInput = { "-1", "13", "10" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLatitude(geoLatInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLAT, errors[0]);
        }

        [TestMethod]
        public void TestLatitudeDegreesTooLarge()
        {
            string[] geoLatInput = { "100", "10", "10" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLatitude(geoLatInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLAT, errors[0]);
        }

        [TestMethod]
        public void TestLatitudeMinutesTooSmall()
        {
            string[] geoLatInput = { "52", "-13", "0" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLatitude(geoLatInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLAT, errors[0]);
        }

        [TestMethod]
        public void TestLatitudeMinutesTooLarge()
        {
            string[] geoLatInput = { "52", "60", "0" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLatitude(geoLatInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLAT, errors[0]);
        }

        [TestMethod]
        public void TestLatitudeSecondsTooSmall()
        {
            string[] geoLatInput = { "52", "13", "-1" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLatitude(geoLatInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLAT, errors[0]);
        }

        [TestMethod]
        public void TestLatitudeSecondsTooLarge()
        {
            string[] geoLatInput = { "52", "13", "60" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLatitude(geoLatInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLAT, errors[0]);
        }

        [TestMethod]
        public void TestLatitudeMaxValues()
        {
            string[] geoLatInput = { "89", "59", "59" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLatitude(geoLatInput);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestLatitudeMaxValuesPlusOneSecond()
        {
            string[] geoLatInput = { "90", "0", "0" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLatitude(geoLatInput);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLAT, errors[0]);
        }

        [TestMethod]
        public void TestLatitudeMinValues()
        {
            string[] geoLatInput = { "0", "0", "0" };
            LocationValidations locationValidations = new();
            List<int> errors = locationValidations.ValidateGeoLatitude(geoLatInput);
            Assert.AreEqual(0, errors.Count);
        }


    }

}