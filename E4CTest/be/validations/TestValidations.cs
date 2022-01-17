// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.be.astron;
using E4C.be.domain;
using E4C.be.validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace E4CTest
{
    [TestClass]
    public class TestDateTimeValidations
    {

        [TestMethod]
        public void TestDateHappyFlow()
        {
            string dateText = "1953/1/29";
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian))).Returns(true);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            ValidatedDate validatedDate = dateTimeValidations.ConstructAndValidateDate(dateText, Calendars.Gregorian);
            Assert.IsTrue(validatedDate.noErrors);
            Assert.AreEqual("", validatedDate.errorText);
            Assert.AreEqual(1953, validatedDate.year);
            Assert.AreEqual(1, validatedDate.month);
            Assert.AreEqual(29, validatedDate.day);
        }

        [TestMethod]
        public void TestDateWrongSeparator()
        {
            string dateText = "1953.1.29";
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian))).Returns(true);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            ValidatedDate validatedDate = dateTimeValidations.ConstructAndValidateDate(dateText, Calendars.Gregorian);
            Assert.IsFalse(validatedDate.noErrors);
            Assert.AreNotEqual("", validatedDate.errorText);
            Assert.AreEqual(0, validatedDate.year);
            Assert.AreEqual(0, validatedDate.month);
            Assert.AreEqual(0, validatedDate.day);
        }

        [TestMethod]
        public void TestDateNonNumerics()
        {
            string dateText = "1953/jan/29";
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian))).Returns(true);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            ValidatedDate validatedDate = dateTimeValidations.ConstructAndValidateDate(dateText, Calendars.Gregorian);
            Assert.IsFalse(validatedDate.noErrors);
        }

        [TestMethod]
        public void TestDateTwoElements()
        {
            string dateText = "1953/1";
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(1953, 1, 29, 0.0, Calendars.Gregorian))).Returns(true);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            ValidatedDate validatedDate = dateTimeValidations.ConstructAndValidateDate(dateText, Calendars.Gregorian);
            Assert.IsFalse(validatedDate.noErrors);
        }

        [TestMethod]
        public void TestDateLeapYear()
        {
            string dateText = "2024/2/29";
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(2024, 2, 29, 0.0, Calendars.Gregorian))).Returns(true);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            ValidatedDate validatedDate = dateTimeValidations.ConstructAndValidateDate(dateText, Calendars.Gregorian);
            Assert.IsTrue(validatedDate.noErrors);
            Assert.AreEqual("", validatedDate.errorText);
            Assert.AreEqual(2024, validatedDate.year);
            Assert.AreEqual(2, validatedDate.month);
            Assert.AreEqual(29, validatedDate.day);
        }

        [TestMethod]
        public void TestDateNoLeapYear()
        {
            string dateText = "2023/2/29";
            var mock = new Mock<ICalendarCalc>();
            mock.Setup(p => p.ValidDateAndtime(new SimpleDateTime(2023, 2, 29, 0.0, Calendars.Gregorian))).Returns(false);
            DateTimeValidations dateTimeValidations = new(mock.Object);
            ValidatedDate validatedDate = dateTimeValidations.ConstructAndValidateDate(dateText, Calendars.Gregorian);
            Assert.IsFalse(validatedDate.noErrors);
        }

        [TestMethod]
        public void TestTimeHappyFlow()
        {
            string timeText = "10:30:00";
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            ValidatedUniversalTime validatedTime = dateTimeValidations.ConstructAndValidateTime(timeText);
            Assert.IsTrue(validatedTime.noErrors);
            Assert.AreEqual(10, validatedTime.hour);
            Assert.AreEqual(30, validatedTime.minute);
            Assert.AreEqual(0, validatedTime.second);
        }

        [TestMethod]
        public void TestTimeDefaultSeconds()
        {
            string timeText = "10:30";
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            ValidatedUniversalTime validatedTime = dateTimeValidations.ConstructAndValidateTime(timeText);
            Assert.IsTrue(validatedTime.noErrors);
            Assert.AreEqual(10, validatedTime.hour);
            Assert.AreEqual(30, validatedTime.minute);
            Assert.AreEqual(0, validatedTime.second);
        }

        [TestMethod]
        public void TestTimeWrongSeparator()
        {
            string timeText = "10.30.00";
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            ValidatedUniversalTime validatedTime = dateTimeValidations.ConstructAndValidateTime(timeText);
            Assert.IsFalse(validatedTime.noErrors);
            Assert.AreEqual(0, validatedTime.hour);
            Assert.AreEqual(0, validatedTime.minute);
            Assert.AreEqual(0, validatedTime.second);
        }

        [TestMethod]
        public void TestNonNumeric()
        {
            string timeText = "10:ab:00";
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            ValidatedUniversalTime validatedTime = dateTimeValidations.ConstructAndValidateTime(timeText);
            Assert.IsFalse(validatedTime.noErrors);
            Assert.AreEqual(0, validatedTime.hour);
            Assert.AreEqual(0, validatedTime.minute);
            Assert.AreEqual(0, validatedTime.second);
        }

        [TestMethod]
        public void TestRangeTooLarge()
        {
            string timeText = "24:00:00";
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            ValidatedUniversalTime validatedTime = dateTimeValidations.ConstructAndValidateTime(timeText);
            Assert.IsFalse(validatedTime.noErrors);
            Assert.AreEqual(0, validatedTime.hour);
            Assert.AreEqual(0, validatedTime.minute);
            Assert.AreEqual(0, validatedTime.second);
        }

        [TestMethod]
        public void TestRangeTooSmall()
        {
            string timeText = "-1:00:00";
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            ValidatedUniversalTime validatedTime = dateTimeValidations.ConstructAndValidateTime(timeText);
            Assert.IsFalse(validatedTime.noErrors);
            Assert.AreEqual(0, validatedTime.hour);
            Assert.AreEqual(0, validatedTime.minute);
            Assert.AreEqual(0, validatedTime.second);
        }

        [TestMethod]
        public void TestElementsOutOfRange()
        {
            string timeText = "10:60:00";
            var mock = new Mock<ICalendarCalc>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            ValidatedUniversalTime validatedTime = dateTimeValidations.ConstructAndValidateTime(timeText);
            Assert.IsFalse(validatedTime.noErrors);
            Assert.AreEqual(0, validatedTime.hour);
            Assert.AreEqual(0, validatedTime.minute);
            Assert.AreEqual(0, validatedTime.second);
        }


    }


}