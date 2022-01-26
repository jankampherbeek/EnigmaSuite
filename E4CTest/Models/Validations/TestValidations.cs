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
            string year = "1953";
            string month = "1";
            string day = "29";
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian))).Returns(true);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateDate(year, month, day, Calendars.Gregorian, YearCounts.CE);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestDateNonNumerics()
        {
            string year = "1953";
            string month = "Januar";
            string day = "29";
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian))).Returns(true);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateDate(year, month, day, Calendars.Gregorian, YearCounts.CE);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_DATE, errors[0]);
        }

        [TestMethod]
        public void TestDateTwoElements()
        {
            string year = "1953";
            string month = "1";
            string day = "";
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian))).Returns(true);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateDate(year, month, day, Calendars.Gregorian, YearCounts.CE);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_DATE, errors[0]);
        }

        [TestMethod]
        public void TestDateLeapYear()
        {
            string year = "2024";
            string month = "2";
            string day = "29";
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(2024, 2, 29, 0.0, Calendars.Gregorian))).Returns(true);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateDate(year, month, day, Calendars.Gregorian, YearCounts.CE);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestDateNoLeapYear()
        {
            string year = "2023";
            string month = "2";
            string day = "29";
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(2023, 2, 29, 0.0, Calendars.Gregorian))).Returns(false);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateDate(year, month, day, Calendars.Gregorian, YearCounts.CE);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_DATE, errors[0]);
        }

        [TestMethod]
        public void TestTimeHappyFlow()
        {
            string hour = "10";
            string minute = "30";
            string second = "0";
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(hour, minute, second);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestTimeDefaultSeconds()
        {
            string hour = "10";
            string minute = "30";
            string second = "";
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(hour, minute, second);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestNonNumeric()
        {
            string hour = "10";
            string minute = "30";
            string second = "ab";
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(hour, minute, second);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_TIME, errors[0]);
        }

        [TestMethod]
        public void TestRangeTooLarge()
        {
            string hour = "24";
            string minute = "0";
            string second = "0";
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(hour, minute, second);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_TIME, errors[0]);
        }

        [TestMethod]
        public void TestRangeTooSmall()
        {
            string hour = "-1";
            string minute = "0";
            string second = "0";
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(hour, minute, second);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_TIME, errors[0]);
        }

        [TestMethod]
        public void TestElementsOutOfRange()
        {
            string hour = "10";
            string minute = "60";
            string second = "0";
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(hour, minute, second);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_TIME, errors[0]);
        }

    }

}