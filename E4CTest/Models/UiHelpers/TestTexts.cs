// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
using E4C.Models.UiHelpers;
using E4C.Views.ViewHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace E4CTest
{
    [TestClass]
    public class TestTextAssembler
    {

        [TestMethod]
        public void TestCreateLocationFullTextHappyFlow()
        {
            ITextAssembler _textAssembler = CreateMockedTextAssembler();
            string _locationName = "Enschede, The Netherlands";
            int[] _geoLongValues = new int[] { 6, 54, 0 };
            int[] _geoLatValues = new int[] { 52, 13, 0 };
            var _dirLong = Directions4GeoLong.East;
            var _dirLat = Directions4GeoLat.North;
            string _expected = "Enschede, The Netherlands, 6°54'00\" East, 52°13'00\" North.";
            string _result = _textAssembler.CreateLocationFullText(_locationName, _geoLongValues, _geoLatValues, _dirLong, _dirLat);
            Assert.AreEqual(_expected, _result);
        }

        [TestMethod]
        public void TestCreateLocationFullTextNoPlaceName()
        {
            ITextAssembler _textAssembler = CreateMockedTextAssembler();
            string _locationName = "";
            var _geoLongValues = new int[] { 6, 54, 0 };
            var _geoLatValues = new int[] { 52, 13, 0 };
            var _dirLong = Directions4GeoLong.East;
            var _dirLat = Directions4GeoLat.North;
            string _expected = "No name for location, 6°54'00\" East, 52°13'00\" North.";
            string _result = _textAssembler.CreateLocationFullText(_locationName, _geoLongValues, _geoLatValues, _dirLong, _dirLat);
            Assert.AreEqual(_expected, _result);
        }

        [TestMethod]
        public void TestCreateDateFullTextHappyFlow()
        {
            ITextAssembler _textAssembler = CreateMockedTextAssembler();
            var _dateValues = new int[] { 1953, 1, 29 };
            var _calendar = Calendars.Gregorian;
            var _yearCount = YearCounts.CE;
            string _expected = "January 29, 1953. Gregorian, CE.";
            string _result = _textAssembler.CreateDayFullText(_dateValues, _calendar, _yearCount);
            Assert.AreEqual(_expected, _result);
        }

        [TestMethod]
        public void TestCreateDateFullTextJulian()
        {
            ITextAssembler _textAssembler = CreateMockedTextAssembler();
            var _dateValues = new int[] { 953, 1, 29 };
            var _calendar = Calendars.Julian;
            var _yearCount = YearCounts.CE;
            string _expected = "January 29, 953. Julian, CE.";
            string _result = _textAssembler.CreateDayFullText(_dateValues, _calendar, _yearCount);
            Assert.AreEqual(_expected, _result);
        }

        [TestMethod]
        public void TestCreateDateFullTextNegativeAstronomicalYearCount()
        {
            ITextAssembler _textAssembler = CreateMockedTextAssembler();
            var _dateValues = new int[] { -953, 1, 29 };
            var _calendar = Calendars.Julian;
            var _yearCount = YearCounts.Astronomical;
            string _expected = "January 29, -953. Julian, Astronomical.";
            string _result = _textAssembler.CreateDayFullText(_dateValues, _calendar, _yearCount);
            Assert.AreEqual(_expected, _result);
        }

        [TestMethod]
        public void TestCreateDateFullTextBCE()
        {
            ITextAssembler _textAssembler = CreateMockedTextAssembler();
            var _dateValues = new int[] { 953, 1, 29 };
            var _calendar = Calendars.Julian;
            var _yearCount = YearCounts.BCE;
            string _expected = "January 29, 953. Julian, BCE.";
            string _result = _textAssembler.CreateDayFullText(_dateValues, _calendar, _yearCount);
            Assert.AreEqual(_expected, _result);
        }

        [TestMethod]
        public void TestCreateTimeFullTextHappyFlow()
        {
            ITextAssembler _textAssembler = CreateMockedTextAssembler();
            var timeValues = new int[] { 8, 37, 30 };
            bool dst = false;
            var _timezone = TimeZones.CET;
            string _expected = "8:37:30, +01:00: CET/Central European Time, no DST.";
            var _result = _textAssembler.CreateTimeFullText(timeValues, _timezone, dst);
            Assert.AreEqual(_expected, _result);
        }

        [TestMethod]
        public void TestCreateTimeFullTextWithDst()
        {
            ITextAssembler _textAssembler = CreateMockedTextAssembler();
            var timeValues = new int[] { 8, 37, 30 };
            bool dst = true;
            var _timezone = TimeZones.CET;
            string _expected = "8:37:30, +01:00: CET/Central European Time, DST applied.";
            var _result = _textAssembler.CreateTimeFullText(timeValues, _timezone, dst);
            Assert.AreEqual(_expected, _result);
        }

        [TestMethod]
        public void TestCreateTimeFullTextTimeZoneLmt()
        {
            ITextAssembler _textAssembler = CreateMockedTextAssembler();
            var timeValues = new int[] { 8, 37, 30 };
            bool dst = false;
            var lmtLongitudeValues = new int[] { 6, 54, 0 };
            var lmtLongDir = Directions4GeoLong.East;
            var _timezone = TimeZones.LMT;
            string _expected = "8:37:30, LMT: Local Mean Time for 6°54'00\" East, no DST.";
            var _result = _textAssembler.CreateTimeFullText(timeValues, _timezone, dst, lmtLongitudeValues, lmtLongDir);
            Assert.AreEqual(_expected, _result);
        }


        private ITextAssembler CreateMockedTextAssembler()
        {
            var mockTimeZoneSpec = new Mock<ITimeZoneSpecifications>();
            mockTimeZoneSpec.Setup(p => p.DetailsForTimeZone(TimeZones.CET)).Returns(new TimeZoneDetails(TimeZones.CET, 1.0, "ref.enum.timezone.cet"));
            mockTimeZoneSpec.Setup(p => p.DetailsForTimeZone(TimeZones.LMT)).Returns(new TimeZoneDetails(TimeZones.LMT, 0.0, "ref.enum.timezone.lmt"));
            var mockRosetta = new Mock<IRosetta>();
            mockRosetta.Setup(p => p.TextForId("common.location.dirgeolat.north")).Returns("North");
            mockRosetta.Setup(p => p.TextForId("common.location.dirgeolong.east")).Returns("East");
            mockRosetta.Setup(p => p.TextForId("common.location.noname")).Returns("No name for location");
            mockRosetta.Setup(p => p.TextForId("common.date.month.jan")).Returns("January");
            mockRosetta.Setup(p => p.TextForId("ref.enum.yearcount.astronomical")).Returns("Astronomical");
            mockRosetta.Setup(p => p.TextForId("ref.enum.yearcount.bce")).Returns("BCE");
            mockRosetta.Setup(p => p.TextForId("ref.enum.yearcount.ce")).Returns("CE");
            mockRosetta.Setup(p => p.TextForId("ref.enum.calendar.gregorian")).Returns("Gregorian");
            mockRosetta.Setup(p => p.TextForId("ref.enum.calendar.julian")).Returns("Julian");
            mockRosetta.Setup(p => p.TextForId("ref.enum.timezone.cet")).Returns("+01:00: CET/Central European Time");
            mockRosetta.Setup(p => p.TextForId("ref.enum.timezone.lmt")).Returns("LMT: Local Mean Time");
            mockRosetta.Setup(p => p.TextForId("common.time.dst.notused")).Returns("no DST");
            mockRosetta.Setup(p => p.TextForId("common.time.dst.used")).Returns("DST applied");
            mockRosetta.Setup(p => p.TextForId("common.for")).Returns("for");
            TextAssembler _createdTextAssembler = new(mockRosetta.Object, mockTimeZoneSpec.Object);
            return _createdTextAssembler;
        }



    }
}