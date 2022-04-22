// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Creators;
using E4C.Models.Domain;
using E4C.domain.shared.specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using domain.shared;
using E4C.core.api;
using E4C.Core.Shared.Domain;
using E4C.Shared.ReqResp;
using E4C.Shared.References;

namespace E4CTest.Creators
{

    [TestClass]
    public class TestIntRangeCreator
    {
        [TestMethod]
        public void TestHappyFlow()
        {
            IIntRangeCreator _intRangeCreator = new IntRangeCreator();
            var _texts = new string[] { "1", "2", "3" };
            bool succes = _intRangeCreator.CreateIntRange(_texts, out int[] _numValues);
            Assert.IsTrue(succes);
            Assert.AreEqual(1, _numValues[0]);
            Assert.AreEqual(2, _numValues[1]);
            Assert.AreEqual(3, _numValues[2]);
        }

        [TestMethod]
        public void TestNoElements()
        {
            IIntRangeCreator _intRangeCreator = new IntRangeCreator();
            var _texts = Array.Empty<string>();
            bool succes = _intRangeCreator.CreateIntRange(_texts, out int[] _numValues);
            Assert.IsTrue(succes);
            Assert.AreEqual(0, _numValues.Length);
        }

        [TestMethod]
        public void TestOnlyZeros()
        {
            IIntRangeCreator _intRangeCreator = new IntRangeCreator();
            var _texts = new string[] { "0", "0", "0" };
            bool succes = _intRangeCreator.CreateIntRange(_texts, out int[] _numValues);
            Assert.IsTrue(succes);
            Assert.AreEqual(0, _numValues[0]);
            Assert.AreEqual(0, _numValues[1]);
            Assert.AreEqual(0, _numValues[2]);
        }

        [TestMethod]
        public void TestOneElement()
        {
            IIntRangeCreator _intRangeCreator = new IntRangeCreator();
            var _texts = new string[] { "1" };
            bool succes = _intRangeCreator.CreateIntRange(_texts, out int[] _numValues);
            Assert.IsTrue(succes);
            Assert.AreEqual(1, _numValues[0]);
        }

        [TestMethod]
        public void TestMultipleElements()
        {
            IIntRangeCreator _intRangeCreator = new IntRangeCreator();
            var _texts = new string[] { "1", "10", "100" };
            bool succes = _intRangeCreator.CreateIntRange(_texts, out int[] _numValues);
            Assert.IsTrue(succes);
            Assert.AreEqual(1, _numValues[0]);
            Assert.AreEqual(10, _numValues[1]);
            Assert.AreEqual(100, _numValues[2]);
        }

        [TestMethod]
        public void TestNegative()
        {
            IIntRangeCreator _intRangeCreator = new IntRangeCreator();
            var _texts = new string[] { "-1", "-2" };
            bool succes = _intRangeCreator.CreateIntRange(_texts, out int[] _numValues);
            Assert.IsTrue(succes);
            Assert.AreEqual(-1, _numValues[0]);
            Assert.AreEqual(-2, _numValues[1]);
        }

        [TestMethod]
        public void TestError()
        {
            IIntRangeCreator _intRangeCreator = new IntRangeCreator();
            var _texts = new string[] { "a", "b", "c" };
            bool succes = _intRangeCreator.CreateIntRange(_texts, out _);
            Assert.IsFalse(succes);
        }

    }

    [TestClass]
    public class TestLocationFactory
    {
        private readonly string _defaultLocationName = "Enschede, The Netherlands";
        private readonly string[] _defaultLongTexts = new string[] { "6", "54", "30" };
        private readonly string[] _defaultLatTexts = new string[] { "52", "13", "10" };
#pragma warning disable IDE0052 // Remove unread private members. Initialisations are used in several mocked instances.
        private int[] _defaultLongValues = new int[] { 6, 54, 30 };
        private int[] _defaultLatValues = new int[] { 52, 13, 10 };
#pragma warning restore IDE0052 // Remove unread private members

        private readonly double _defaultLongitude = 6.9083333333333;
        private readonly double _defaultLatitude = 52.2194444444444;
        private readonly bool _longEast = true;
        private readonly bool _latNorth = true;
        private readonly double _delta = 0.00000001;

        [TestMethod]
        public void TestFullName()
        {
            var _location = LocationFromStandardFactory();
            string expected = "Enschede, The Netherlands 006" + Constants.DEGREE_SIGN + "54" + Constants.MINUTE_SIGN + "30" + Constants.SECOND_SIGN + " [E] " +
                "52" + Constants.DEGREE_SIGN + "13" + Constants.MINUTE_SIGN + "10" + Constants.SECOND_SIGN + " [N]";
            Assert.AreEqual(expected, _location.LocationFullName);
        }

        [TestMethod]
        public void TestFullNameWithEmptyLocationName()
        {
            var _mock = new Mock<IIntRangeCreator>();
            _mock.Setup(p => p.CreateIntRange(_defaultLongTexts, out _defaultLongValues)).Returns(true);
            _mock.Setup(p => p.CreateIntRange(_defaultLatTexts, out _defaultLatValues)).Returns(true);
            LocationFactory _locationFactory = new(_mock.Object);
            string expected = "006" + Constants.DEGREE_SIGN + "54" + Constants.MINUTE_SIGN + "30" + Constants.SECOND_SIGN + " [E] " +
                "52" + Constants.DEGREE_SIGN + "13" + Constants.MINUTE_SIGN + "10" + Constants.SECOND_SIGN + " [N]";
            bool success = _locationFactory.CreateLocation("", _defaultLongTexts, _defaultLatTexts, _longEast, _latNorth, out Location location, out List<int> errorCodes);
            Assert.IsTrue(success);
            Assert.AreEqual(expected, location.LocationFullName);
        }

        [TestMethod]
        public void TestGeoLong()
        {
            var _location = LocationFromStandardFactory();
            Assert.AreEqual(_defaultLongitude, _location.GeoLong, _delta);
        }


        [TestMethod]
        public void TestGeoLongSyntaxError()
        {
            string[] _errorLongTexts = new string[] { "a", "b", "c" };
            var _mock = new Mock<IIntRangeCreator>();
            _mock.Setup(p => p.CreateIntRange(_errorLongTexts, out _defaultLongValues)).Returns(false);
            _mock.Setup(p => p.CreateIntRange(_defaultLatTexts, out _defaultLatValues)).Returns(true);
            LocationFactory _locationFactory = new(_mock.Object);
            bool success = _locationFactory.CreateLocation("Enschede", _defaultLongTexts, _defaultLatTexts, _longEast, _latNorth, out Location location, out List<int> errorCodes);
            Assert.IsFalse(success);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLON, errorCodes[0]);
        }

        [TestMethod]
        public void TestGeoLongInvalid()
        {
            string[] _errorLongTexts = new string[] { "200", "10", "20" };
            var _mock = new Mock<IIntRangeCreator>();
            _mock.Setup(p => p.CreateIntRange(_errorLongTexts, out _defaultLongValues)).Returns(true);
            _mock.Setup(p => p.CreateIntRange(_defaultLatTexts, out _defaultLatValues)).Returns(true);
            LocationFactory _locationFactory = new(_mock.Object);
            bool success = _locationFactory.CreateLocation("Enschede", _defaultLongTexts, _defaultLatTexts, _longEast, _latNorth, out Location location, out List<int> errorCodes);
            Assert.IsFalse(success);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLON, errorCodes[0]);
        }



        [TestMethod]
        public void TestGeoLat()
        {
            var _location = LocationFromStandardFactory();
            Assert.AreEqual(_defaultLatitude, _location.GeoLat, _delta);
        }

        [TestMethod]
        public void TestGeoLatSyntaxError()
        {
            string[] _errorLatTexts = new string[] { "a", "b", "c" };
            var _mock = new Mock<IIntRangeCreator>();
            _mock.Setup(p => p.CreateIntRange(_defaultLongTexts, out _defaultLongValues)).Returns(true);
            _mock.Setup(p => p.CreateIntRange(_errorLatTexts, out _defaultLatValues)).Returns(false);
            LocationFactory _locationFactory = new(_mock.Object);
            bool success = _locationFactory.CreateLocation("Enschede", _defaultLongTexts, _defaultLatTexts, _longEast, _latNorth, out Location location, out List<int> errorCodes);
            Assert.IsFalse(success);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLAT, errorCodes[0]);
        }

        [TestMethod]
        public void TestGeoLatInvalid()
        {
            string[] _errorLatTexts = new string[] { "99", "12", "24" };
            var _mock = new Mock<IIntRangeCreator>();
            _mock.Setup(p => p.CreateIntRange(_defaultLongTexts, out _defaultLongValues)).Returns(true);
            _mock.Setup(p => p.CreateIntRange(_errorLatTexts, out _defaultLatValues)).Returns(true);
            LocationFactory _locationFactory = new(_mock.Object);
            bool success = _locationFactory.CreateLocation("Enschede", _defaultLongTexts, _defaultLatTexts, _longEast, _latNorth, out Location location, out List<int> errorCodes);
            Assert.IsFalse(success);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_GEOLAT, errorCodes[0]);
        }

        private Location LocationFromStandardFactory()
        {
            var _mock = new Mock<IIntRangeCreator>();
            _mock.Setup(p => p.CreateIntRange(_defaultLongTexts, out _defaultLongValues)).Returns(true);
            _mock.Setup(p => p.CreateIntRange(_defaultLatTexts, out _defaultLatValues)).Returns(true);
            LocationFactory _locationFactory = new(_mock.Object);
            _locationFactory.CreateLocation(_defaultLocationName, _defaultLongTexts, _defaultLatTexts, _longEast, _latNorth, out Location location, out List<int> errorCodes);
            return location;
        }


    }

    [TestClass]
    public class TestDateFactory
    {
        private readonly string[] _defaultDateTexts = new string[] { "2022", "3", "1" };
        private readonly Calendars _defaultCalendar = Calendars.Gregorian;
        private readonly YearCounts _defaultYearCount = YearCounts.CE;


        [TestMethod]
        public void TestDate()
        {
            IDateFactory _dateFactory = CreateDateFactory();
            bool _success = _dateFactory.CreateDate(_defaultDateTexts, _defaultCalendar, _defaultYearCount, out FullDate fullDate, out List<int> errorCodes);
            Assert.IsTrue(_success);
            Assert.AreEqual(0, errorCodes.Count);
            Assert.AreEqual(2022, fullDate.YearMonthDay[0]);
            Assert.AreEqual(3, fullDate.YearMonthDay[1]);
            Assert.AreEqual(1, fullDate.YearMonthDay[2]);
        }

        [TestMethod]
        public void TestFullText()
        {
            IDateFactory _dateFactory = CreateDateFactory();
            bool _success = _dateFactory.CreateDate(_defaultDateTexts, _defaultCalendar, _defaultYearCount, out FullDate fullDate, out List<int> errorCodes);
            string expected = "[month:mar] 2022, 1 [g]";
            Assert.IsTrue(_success);
            Assert.AreEqual(0, errorCodes.Count);
            Assert.AreEqual(expected, fullDate.DateFullText);
        }

        [TestMethod]
        public void TestYearCountBCE()
        {
            var _yearCount = YearCounts.BCE;
            var _calendar = Calendars.Julian;
            var _dateTexts = new string[] { "100", "1", "2" };
            var _dateValues = new int[] { 100, 1, 2 };
            var _mockIntRangeCreator = new Mock<IIntRangeCreator>();
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_dateTexts, out _dateValues)).Returns(true);
            var _mockDateTimeApi = new Mock<IDateTimeApi>();
            var _simpleDateTime = new SimpleDateTime(-99, 1, 2, 0.0, _calendar);
            var _checkDateTimeRequest = new CheckDateTimeRequest(_simpleDateTime);
            _mockDateTimeApi.Setup(p => p.checkDateTime(_checkDateTimeRequest)).Returns(new CheckDateTimeResponse(true, true, ""));


            DateFactory _dateFactory = new(_mockIntRangeCreator.Object, _mockDateTimeApi.Object);
            bool _success = _dateFactory.CreateDate(_dateTexts, _calendar, _yearCount, out FullDate fullDate, out List<int> errorCodes);
            Assert.IsTrue(_success);
            Assert.AreEqual(-99, fullDate.YearMonthDay[0]);
            Assert.AreEqual(1, fullDate.YearMonthDay[1]);
            Assert.AreEqual(2, fullDate.YearMonthDay[2]);
            string expected = "[month:jan] -0099, 2 [j]";
            Assert.AreEqual(expected, fullDate.DateFullText);
        }

        private IDateFactory CreateDateFactory()
        {
            var _mockIntRangeCreator = new Mock<IIntRangeCreator>();
            int[] _defaultDateValues = new int[] { 2022, 3, 1 };
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_defaultDateTexts, out _defaultDateValues)).Returns(true);
            var _mockDateTimeApi = new Mock<IDateTimeApi>();
            var _simpleDateTime = new SimpleDateTime(2022, 3, 1, 0.0, Calendars.Gregorian);
            _mockDateTimeApi.Setup(p => p.checkDateTime(new CheckDateTimeRequest(_simpleDateTime))).Returns(new CheckDateTimeResponse(true, true, ""));
            DateFactory _dateFactory = new(_mockIntRangeCreator.Object, _mockDateTimeApi.Object);
            return _dateFactory;
        }

    }

    [TestClass]
    public class TestTimeFactory
    {

        private string[] _defaultTimeTexts = new string[] { "22", "9", "30" };
#pragma warning disable IDE0052 // Remove unread private members. Initialisations are used in several mocked instances.
        private int[] _defaultTimeValues = new int[] { 22, 9, 30 };
        private int[] _defaultOffsetValues = new int[] { 0, 0, 0 };
#pragma warning restore IDE0052 // Remove unread private members
        private readonly string[] _defaultOffsetTexts = new string[] { "0", "0", "0" };
        private readonly bool _defaultOffsetPlus = true;
        private readonly TimeZones _defaultZone = TimeZones.CET;
        private readonly double _delta = 0.00000001;

        [TestMethod]
        public void TestTime()
        {

            _defaultTimeValues = new int[] { 22, 9, 30 };
            _defaultTimeTexts = new string[] { "22", "9", "30" };
            ITimeFactory _timeFactory = CreateTimeFactory();
            bool _success = _timeFactory.CreateTime(_defaultTimeTexts, _defaultZone, _defaultOffsetTexts, _defaultOffsetPlus, out FullTime fullTime, out List<int> _errorCodes);
            Assert.IsTrue(_success);
            Assert.AreEqual(0, _errorCodes.Count);
            Assert.AreEqual(22, fullTime.HourMinuteSecond[0]);
            Assert.AreEqual(9, fullTime.HourMinuteSecond[1]);
            Assert.AreEqual(30, fullTime.HourMinuteSecond[2]);
        }

        [TestMethod]
        public void TestUt()
        {
            double _expectedUt = 21.1583333333333;
            ITimeFactory _timeFactory = CreateTimeFactory();
            _ = _timeFactory.CreateTime(_defaultTimeTexts, _defaultZone, _defaultOffsetTexts, _defaultOffsetPlus, out FullTime fullTime, out List<int> _);
            Assert.AreEqual(_expectedUt, fullTime.Ut, _delta);
        }

        [TestMethod]
        public void TestLmt()
        {
            double _expectedUt = 20.65833333333;
            var _lmtOffsetTexts = new string[] { "1", "30", "0" };
            var _lmtOffsetValues = new int[] { 1, 30, 0 };
            var _lmtZone = TimeZones.LMT;
            var _mockIntRangeCreator = new Mock<IIntRangeCreator>();
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_defaultTimeTexts, out _defaultTimeValues)).Returns(true);
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_lmtOffsetTexts, out _lmtOffsetValues)).Returns(true);
            var _mockTimeZoneSpecifications = new Mock<ITimeZoneSpecifications>();
            _mockTimeZoneSpecifications.Setup(p => p.DetailsForTimeZone(_lmtZone)).Returns(new TimeZoneDetails(_lmtZone, 0.0, "ref.enum.timezone.lmt"));
            TimeFactory _timeFactory = new(_mockIntRangeCreator.Object, _mockTimeZoneSpecifications.Object);
            _ = _timeFactory.CreateTime(_defaultTimeTexts, _lmtZone, _lmtOffsetTexts, _defaultOffsetPlus, out FullTime fullTime, out List<int> errorCodes);
            Assert.AreEqual(_expectedUt, fullTime.Ut, _delta);
        }

        [TestMethod]
        public void TestFullText()
        {
            string _expectedFullText = "22:09:30 [ref.enum.timezone.cet]";
            ITimeFactory _timeFactory = CreateTimeFactory();
            _ = _timeFactory.CreateTime(_defaultTimeTexts, _defaultZone, _defaultOffsetTexts, _defaultOffsetPlus, out FullTime fullTime, out List<int> _);
            Assert.AreEqual(_expectedFullText, fullTime.TimeFullText);
        }

        [TestMethod]
        public void TestLmtFullText()
        {
            string _expectedFullText = "22:09:30 [ref.enum.timezone.lmt] +01:30:00";

            var _lmtOffsetTexts = new string[] { "1", "30", "0" };
            var _lmtOffsetValues = new int[] { 1, 30, 0 };
            var _lmtZone = TimeZones.LMT;
            var _mockIntRangeCreator = new Mock<IIntRangeCreator>();
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_defaultTimeTexts, out _defaultTimeValues)).Returns(true);
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_lmtOffsetTexts, out _lmtOffsetValues)).Returns(true);
            var _mockTimeZoneSpecifications = new Mock<ITimeZoneSpecifications>();
            _mockTimeZoneSpecifications.Setup(p => p.DetailsForTimeZone(_lmtZone)).Returns(new TimeZoneDetails(_lmtZone, 0.0, "ref.enum.timezone.lmt"));
            TimeFactory _timeFactory = new(_mockIntRangeCreator.Object, _mockTimeZoneSpecifications.Object);
            _ = _timeFactory.CreateTime(_defaultTimeTexts, _lmtZone, _lmtOffsetTexts, _defaultOffsetPlus, out FullTime fullTime, out List<int> _);
            Assert.AreEqual(_expectedFullText, fullTime.TimeFullText);
        }

        [TestMethod]
        public void TestError()
        {
            var _errorTimeTexts = new string[] { "a", "b", "c" };
            var _errorTimeValues = System.Array.Empty<int>();
            var _mockIntRangeCreator = new Mock<IIntRangeCreator>();
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_defaultTimeTexts, out _errorTimeValues)).Returns(false);
            var _mockTimeZoneSpecifications = new Mock<ITimeZoneSpecifications>();
            _mockTimeZoneSpecifications.Setup(p => p.DetailsForTimeZone(_defaultZone)).Returns(new TimeZoneDetails(_defaultZone, 1.0, "ref.enum.timezone.cet"));
            TimeFactory _timeFactory = new(_mockIntRangeCreator.Object, _mockTimeZoneSpecifications.Object);
            bool _success = _timeFactory.CreateTime(_errorTimeTexts, _defaultZone, _defaultOffsetTexts, _defaultOffsetPlus, out FullTime fullTime, out List<int> errorCodes);
            Assert.IsFalse(_success);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_TIME, errorCodes[0]);
        }

        [TestMethod]
        public void TestErrorInvalidOffset()
        {
            var _lmtTimeTexts = new string[] { "a", "b", "c" };
            var _errorTimeValues = Array.Empty<int>();
            var _lmtZone = TimeZones.LMT;
            var _mockIntRangeCreator = new Mock<IIntRangeCreator>();
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_defaultTimeTexts, out _errorTimeValues)).Returns(true);
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_lmtTimeTexts, out _errorTimeValues)).Returns(false);
            var _mockTimeZoneSpecifications = new Mock<ITimeZoneSpecifications>();
            _mockTimeZoneSpecifications.Setup(p => p.DetailsForTimeZone(_lmtZone)).Returns(new TimeZoneDetails(_lmtZone, 0.0, "ref.enum.timezone.lmt"));
            TimeFactory _timeFactory = new(_mockIntRangeCreator.Object, _mockTimeZoneSpecifications.Object);
            bool _success = _timeFactory.CreateTime(_defaultTimeTexts, _lmtZone, _defaultOffsetTexts, _defaultOffsetPlus, out FullTime fullTime, out List<int> errorCodes);
            Assert.IsFalse(_success);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_OFFSET, errorCodes[0]);
        }

        [TestMethod]
        public void TestDayCorrectionForUtDownSwitch()
        {
            var _timeTexts = new string[] { "2", "30", "0" };
            var _timeValues = new int[] { 2, 30, 0 };
            var _timeZone = TimeZones.PKT;   // offset 5.0 
            var _mockIntRangeCreator = new Mock<IIntRangeCreator>();
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_timeTexts, out _timeValues)).Returns(true);
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_defaultOffsetTexts, out _defaultOffsetValues)).Returns(true);
            var _mockTimeZoneSpecifications = new Mock<ITimeZoneSpecifications>();
            _mockTimeZoneSpecifications.Setup(p => p.DetailsForTimeZone(_timeZone)).Returns(new TimeZoneDetails(_timeZone, 5.0, "ref.enum.timezone.pkt"));
            TimeFactory _timeFactory = new(_mockIntRangeCreator.Object, _mockTimeZoneSpecifications.Object);
            _ = _timeFactory.CreateTime(_timeTexts, _timeZone, _defaultOffsetTexts, _defaultOffsetPlus, out FullTime fullTime, out List<int> errorCodes);
            Assert.AreEqual(-1, fullTime.CorrectionForDay);
            Assert.AreEqual(21.5, fullTime.Ut, _delta);
        }

        [TestMethod]
        public void TestDayCorrectionForUtUpSwitch()
        {
            var _timeTexts = new string[] { "22", "30", "0" };
            var _timeValues = new int[] { 22, 30, 0 };
            //timeZone, -4.0, "ref.enum.timezone.ast
            var _timeZone = TimeZones.AST;   // offset -4.0
            var _mockIntRangeCreator = new Mock<IIntRangeCreator>();
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_timeTexts, out _timeValues)).Returns(true);
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_defaultOffsetTexts, out _defaultOffsetValues)).Returns(true);
            var _mockTimeZoneSpecifications = new Mock<ITimeZoneSpecifications>();
            _mockTimeZoneSpecifications.Setup(p => p.DetailsForTimeZone(_timeZone)).Returns(new TimeZoneDetails(_timeZone, -4.0, "ref.enum.timezone.ast"));
            TimeFactory _timeFactory = new(_mockIntRangeCreator.Object, _mockTimeZoneSpecifications.Object);
            _ = _timeFactory.CreateTime(_timeTexts, _timeZone, _defaultOffsetTexts, _defaultOffsetPlus, out FullTime fullTime, out List<int> errorCodes);
            Assert.AreEqual(1, fullTime.CorrectionForDay);
            Assert.AreEqual(2.5, fullTime.Ut, _delta);
        }


        private ITimeFactory CreateTimeFactory()
        {
            var _mockIntRangeCreator = new Mock<IIntRangeCreator>();
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_defaultTimeTexts, out _defaultTimeValues)).Returns(true);
            _mockIntRangeCreator.Setup(p => p.CreateIntRange(_defaultOffsetTexts, out _defaultOffsetValues)).Returns(true);
            var _mockTimeZoneSpecifications = new Mock<ITimeZoneSpecifications>();
            _mockTimeZoneSpecifications.Setup(p => p.DetailsForTimeZone(_defaultZone)).Returns(new TimeZoneDetails(_defaultZone, 1.0, "ref.enum.timezone.cet"));
            TimeFactory _timeFactory = new(_mockIntRangeCreator.Object, _mockTimeZoneSpecifications.Object);
            return _timeFactory;
        }

    }

    [TestClass]
    public class TestDateTimeFactory
    {
        private readonly double delta = 0.00000001;
        private readonly double _baseJd = 2000000.0;
        private readonly FullDate _fullDate = new(new int[] { 2022, 3, 6 }, Calendars.Gregorian, "FullTextForDate");
        private readonly FullTime _fullTime = new(new int[] { 10, 0, 0 }, 0.0, 0, "FullTextForTime");

        [TestMethod]
        public void TestDateTime()
        {
            IDateTimeFactory factory = CreateDateTimeFactory();
            bool success = factory.CreateDateTime(_fullDate, _fullTime, out FullDateTime _, out List<int> errorCodes);
            Assert.IsTrue(success);
            Assert.AreEqual(0, errorCodes.Count);
        }

        [TestMethod]
        public void TestJulianDay()
        {
            IDateTimeFactory factory = CreateDateTimeFactory();
            _ = factory.CreateDateTime(_fullDate, _fullTime, out FullDateTime _fullDateTime, out List<int> _);
            Assert.AreEqual(_baseJd, _fullDateTime.JulianDayForEt, delta);
        }

        [TestMethod]
        public void TestFullDateText()
        {
            IDateTimeFactory factory = CreateDateTimeFactory();
            _ = factory.CreateDateTime(_fullDate, _fullTime, out FullDateTime _fullDateTime, out List<int> _);
            string expected = "FullTextForDate";
            Assert.AreEqual(expected, _fullDateTime.DateText);
        }

        [TestMethod]
        public void TestFullTimeText()
        {
            IDateTimeFactory factory = CreateDateTimeFactory();
            _ = factory.CreateDateTime(_fullDate, _fullTime, out FullDateTime _fullDateTime, out List<int> _);
            string expected = "FullTextForTime";
            Assert.AreEqual(expected, _fullDateTime.TimeText);
        }

        [TestMethod]
        public void TestUt()
        {
            var mockDateTimeApi = new Mock<IDateTimeApi>();
            SimpleDateTime _dateTime = new(2022, 3, 6, 10.0, Calendars.Gregorian);
            JulianDayRequest julDayRequest = new (_dateTime, true);
            mockDateTimeApi.Setup(p => p.getJulianDay(julDayRequest)).Returns(new JulianDayResponse(_baseJd + (10.0 / 24.0), true, ""));
            IDateTimeFactory factory = new DateTimeFactory(mockDateTimeApi.Object);
            FullTime _fullTimeUt = new(new int[] { 10, 0, 0 }, 10.0, 0, "FullTextForTime");
            _ = factory.CreateDateTime(_fullDate, _fullTimeUt, out FullDateTime _fullDateTime, out List<int> _);
            double expectedJd = _baseJd + (10.0 / 24.0);
            Assert.AreEqual(expectedJd, _fullDateTime.JulianDayForEt, delta);
        }


        [TestMethod]
        public void TestUnderflow()
        {
            IDateTimeFactory factory = CreateDateTimeFactory();
            FullTime _fullTimeUnderflow = new(new int[] { 0, 0, 0 }, 0.0, 1, "FullTextForTime");
            _ = factory.CreateDateTime(_fullDate, _fullTimeUnderflow, out FullDateTime _fullDateTime, out List<int> _);
            double expectedJd = _baseJd + 1;
            Assert.AreEqual(expectedJd, _fullDateTime.JulianDayForEt, delta);
        }

        [TestMethod]
        public void TestOverflow()
        {
            IDateTimeFactory factory = CreateDateTimeFactory();
            FullTime _fullTimeOverflow = new(new int[] { 0, 0, 0 }, 0.0, -1, "FullTextForTime");
            _ = factory.CreateDateTime(_fullDate, _fullTimeOverflow, out FullDateTime _fullDateTime, out List<int> _);
            double expectedJd = _baseJd - 1;
            Assert.AreEqual(expectedJd, _fullDateTime.JulianDayForEt, delta);
        }

        [TestMethod]
        public void TestUtUnderflow()
        {
            IDateTimeFactory factory = CreateDateTimeFactory();
            FullTime _fullTimeUnderflow = new(new int[] { 10, 0, 0 }, 0.0, 1, "FullTextForTime");
            _ = factory.CreateDateTime(_fullDate, _fullTimeUnderflow, out FullDateTime _fullDateTime, out List<int> _);
            double expectedJd = _baseJd + 1;
            Assert.AreEqual(expectedJd, _fullDateTime.JulianDayForEt, delta);
        }

        private IDateTimeFactory CreateDateTimeFactory()
        {
            var mockDateTimeApi = new Mock<IDateTimeApi>();
            SimpleDateTime _dateTime = new(2022, 3, 6, 0.0, Calendars.Gregorian);
            JulianDayRequest julDayRequest = new(_dateTime, true);
            mockDateTimeApi.Setup(p => p.getJulianDay(julDayRequest)).Returns(new JulianDayResponse(_baseJd, true, ""));
            return new DateTimeFactory(mockDateTimeApi.Object);
        }

    }


    [TestClass]
    public class TestDateTimeValidations
    {
        /*
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
        */

        [TestMethod]
        public void TestTimeHappyFlow()
        {
            string[] inputTime = new string[] { "10", "30", "0" };
            var mock = new Mock<IDateTimeApi>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(inputTime);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestTimeDefaultSeconds()
        {
            string[] inputTime = new string[] { "10", "30", "" };
            var mock = new Mock<IDateTimeApi>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(inputTime);
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void TestNonNumeric()
        {
            string[] inputTime = new string[] { "10", "30", "ab" };
            var mock = new Mock<IDateTimeApi>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(inputTime);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_TIME, errors[0]);
        }

        [TestMethod]
        public void TestRangeTooLarge()
        {
            string[] inputTime = new string[] { "24", "0", "0" };
            var mock = new Mock<IDateTimeApi>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(inputTime);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_TIME, errors[0]);
        }

        [TestMethod]
        public void TestRangeTooSmall()
        {
            string[] inputTime = new string[] { "-1", "0", "0" };
            var mock = new Mock<IDateTimeApi>();
            DateTimeValidations dateTimeValidations = new(mock.Object);
            List<int> errors = dateTimeValidations.ValidateTime(inputTime);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(ErrorCodes.ERR_INVALID_TIME, errors[0]);
        }

        [TestMethod]
        public void TestElementsOutOfRange()
        {
            string[] inputTime = new string[] { "10", "60", "0" };
            var mock = new Mock<IDateTimeApi>();
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
            string[] geoLongInput = { "6", "54", "0" };
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