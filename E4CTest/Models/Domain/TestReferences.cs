// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
using E4C.domain.shared.references;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace E4CTest.be.domain
{
    [TestClass]
    public class TestSolSysPointCatSpecifications
    {

        [TestMethod]
        public void TestRetrievingDetails()
        {
            SolSysPointCats solSysPointCat = SolSysPointCats.Modern;
            ISolSysPointCatSpecifications specifications = new SolSysPointCatSpecifications();
            SolSysPointCatDetails details = specifications.DetailsForCategory(solSysPointCat);
            Assert.IsNotNull(details);
            Assert.AreEqual(solSysPointCat, details.Category);
            Assert.AreEqual("enumSolSysPointCatModern", details.TextId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            ISolSysPointCatSpecifications specifications = new SolSysPointCatSpecifications();
            foreach (SolSysPointCats category in Enum.GetValues(typeof(SolSysPointCats)))
            {
                SolSysPointCatDetails details = specifications.DetailsForCategory(category);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.TextId.Length > 0);
            }
        }
    }

    [TestClass]
    public class TestSolarSystemPointSpecifications
    {
        [TestMethod]
        public void TestRetrievingDetails()
        {
            SolarSystemPoints point = SolarSystemPoints.Neptune;
            ISolarSystemPointSpecifications specifications = new SolarSystemPointSpecifications();
            SolarSystemPointDetails details = specifications.DetailsForPoint(point);
            Assert.IsNotNull(details);
            Assert.AreEqual(point, details.SolarSystemPoint);
            Assert.AreEqual(SolSysPointCats.Modern, details.SolSysPointCat);
            Assert.AreEqual(Constants.SE_NEPTUNE, details.SeId);
            Assert.IsTrue(details.UseForHeliocentric);
            Assert.IsTrue(details.UseForGeocentric);
            Assert.AreEqual("neptune", details.TextId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            ISolarSystemPointSpecifications specifications = new SolarSystemPointSpecifications();
            foreach (SolarSystemPoints point in Enum.GetValues(typeof(SolarSystemPoints)))
            {
                SolarSystemPointDetails details = specifications.DetailsForPoint(point);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.TextId.Length > 0);
            }
        }
    }

    [TestClass]
    public class TestCoordinateSystemSpecifications
    {
        [TestMethod]
        public void TestRetrievingDetails()
        {
            CoordinateSystems system = CoordinateSystems.Equatorial;
            ICoordinateSystemSpecifications specifications = new CoordinateSystemSpecifications();
            CoordinateSystemDetails details = specifications.DetailsForCoordinateSystem(system);
            Assert.IsNotNull(details);
            Assert.AreEqual(system, details.CoordinateSystem);
            Assert.AreEqual(Constants.SEFLG_EQUATORIAL, details.ValueForFlag);
            Assert.AreEqual("coordinateSysEquatorial", details.TextId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            ICoordinateSystemSpecifications specifications = new CoordinateSystemSpecifications();
            foreach (CoordinateSystems system in Enum.GetValues(typeof(CoordinateSystems)))
            {
                CoordinateSystemDetails details = specifications.DetailsForCoordinateSystem(system);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.TextId.Length > 0);
            }
        }
    }

    [TestClass]
    public class TestObserverPositionSpecifications
    {
        [TestMethod]
        public void TestRetrievingDetails()
        {
            ObserverPositions position = ObserverPositions.TopoCentric;
            IObserverPositionSpecifications specifications = new ObserverPositionSpecifications();
            ObserverPositionDetails details = specifications.DetailsForObserverPosition(position);
            Assert.IsNotNull(details);
            Assert.AreEqual(position, details.ObserverPosition);
            Assert.AreEqual(Constants.SEFLG_TOPOCTR, details.ValueForFlag);
            Assert.AreEqual("observerPosTopoCentric", details.TextId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            IObserverPositionSpecifications specifications = new ObserverPositionSpecifications();
            foreach (ObserverPositions position in Enum.GetValues(typeof(ObserverPositions)))
            {
                ObserverPositionDetails details = specifications.DetailsForObserverPosition(position);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.TextId.Length > 0);
            }
        }
    }

    [TestClass]
    public class TestZodiacTypeSpecifications
    {
        [TestMethod]
        public void TestRetrievingDetails()
        {
            ZodiacTypes zodiacType = ZodiacTypes.Sidereal;
            IZodiacTypeSpecifications specifications = new ZodiacTypeSpecifications();
            ZodiacTypeDetails details = specifications.DetailsForZodiacType(zodiacType);
            Assert.IsNotNull(details);
            Assert.AreEqual(zodiacType, details.ZodiacType);
            Assert.AreEqual(Constants.SEFLG_SIDEREAL, details.ValueForFlag);
            Assert.AreEqual("zodiacTypeSidereal", details.TextId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            IZodiacTypeSpecifications specifications = new ZodiacTypeSpecifications();
            foreach (ZodiacTypes zodiacType in Enum.GetValues(typeof(ZodiacTypes)))
            {
                ZodiacTypeDetails details = specifications.DetailsForZodiacType(zodiacType);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.TextId.Length > 0);
            }
        }
    }

  

    [TestClass]
    public class TestAyanamshaSpecifications
    {
        [TestMethod]
        public void TestRetrievingDetails()
        {
            Ayanamshas ayanamsha = Ayanamshas.Huber;
            IAyanamshaSpecifications specifications = new AyanamshaSpecifications();
            AyanamshaDetails details = specifications.DetailsForAyanamsha(ayanamsha);
            Assert.IsNotNull(details);
            Assert.AreEqual(Ayanamshas.Huber, details.Ayanamsha);
            Assert.AreEqual(12, details.SeId);
            Assert.AreEqual("ayanamshaHuber", details.TextId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            IAyanamshaSpecifications specifications = new AyanamshaSpecifications();
            foreach (Ayanamshas system in Enum.GetValues(typeof(Ayanamshas)))
            {
                AyanamshaDetails details = specifications.DetailsForAyanamsha(system);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.TextId.Length > 0);
            }
        }
    }

    [TestClass]
    public class TestCalendarSpecifications
    {
        [TestMethod]
        public void TestRetrievingDetails()
        {
            Calendars calendar = Calendars.Julian;
            ICalendarSpecifications specifications = new CalendarSpecifications();
            CalendarDetails details = specifications.DetailsForCalendar(calendar);
            Assert.IsNotNull(details);
            Assert.AreEqual(calendar, details.Calendar);
            Assert.AreEqual("ref.enum.calendar.julian", details.TextId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            ICalendarSpecifications specifications = new CalendarSpecifications();
            foreach (Calendars calendar in Enum.GetValues(typeof(Calendars)))
            {
                CalendarDetails details = specifications.DetailsForCalendar(calendar);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.TextId.Length > 0);
            }
        }

        [TestMethod]
        public void TestRetrievingWithIndex()
        {
            ICalendarSpecifications specifications = new CalendarSpecifications();
            int calendarIndex = 1;
            Calendars calendar = specifications.CalendarForIndex(calendarIndex);
            Assert.AreEqual(Calendars.Julian, calendar);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrievingWithWrongIndex()
        {
            ICalendarSpecifications specifications = new CalendarSpecifications();
            int calendarIndex = 333;
            _ = specifications.CalendarForIndex(calendarIndex);
        }

    }

    [TestClass]
    public class TestYearCounts
    {
        [TestMethod]
        public void TestRetrievingDetails()
        {
            YearCounts yearCount = YearCounts.BCE;
            IYearCountSpecifications specifications = new YearCountSpecifications();
            YearCountDetails details = specifications.DetailsForYearCount(yearCount);
            Assert.IsNotNull(details);
            Assert.AreEqual(yearCount, details.YearCount);
            Assert.AreEqual("ref.enum.yearcount.bce", details.TextId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            IYearCountSpecifications specifications = new YearCountSpecifications();
            foreach (YearCounts yearCount in Enum.GetValues(typeof(YearCounts)))
            {
                YearCountDetails details = specifications.DetailsForYearCount(yearCount);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.TextId.Length > 0);
            }
        }

        [TestMethod]
        public void TestRetrievingWithIndex()
        {
            IYearCountSpecifications specifications = new YearCountSpecifications();
            int yearCountIndex = 2;
            YearCounts yearCount = specifications.YearCountForIndex(yearCountIndex);
            Assert.AreEqual(YearCounts.Astronomical, yearCount);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrievingWithWrongIndex()
        {
            IYearCountSpecifications specifications = new YearCountSpecifications();
            int yearCountIndex = 44;
            _ = specifications.YearCountForIndex(yearCountIndex);
        }
    }



    [TestClass]
    public class TestChartCategories
    {
        [TestMethod]
        public void TestRetrievingDetails()
        {
            ChartCategories chartCategory = ChartCategories.Election;
            IChartCategorySpecifications specifications = new ChartCategorySpecifications();
            ChartCategoryDetails details = specifications.DetailsForCategory(chartCategory);
            Assert.IsNotNull(details);
            Assert.AreEqual(chartCategory, details.Category);
            Assert.AreEqual("ref.enum.chartcategories.election", details.TextId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            IChartCategorySpecifications specifications = new ChartCategorySpecifications();

            foreach (ChartCategories chartCategory in Enum.GetValues(typeof(ChartCategories)))
            {
                ChartCategoryDetails details = specifications.DetailsForCategory(chartCategory);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.TextId.Length > 0);
            }
        }

        [TestMethod]
        public void TestRetrievingWithIndex()
        {
            IChartCategorySpecifications specifications = new ChartCategorySpecifications();
            int chartCategoryIndex = 3;
            ChartCategories chartCategory = specifications.ChartCategoryForIndex(chartCategoryIndex);
            Assert.AreEqual(ChartCategories.Event, chartCategory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrievingWithWrongIndex()
        {
            IChartCategorySpecifications specifications = new ChartCategorySpecifications();
            int chartCategoryIndex = 500;
            _ = specifications.ChartCategoryForIndex(chartCategoryIndex);
        }

    }

    [TestClass]
    public class TestRoddenRatings
    {
        [TestMethod]
        public void TestRetrievingDetails()
        {
            RoddenRatings roddenRating = RoddenRatings.C;
            IRoddenRatingSpecifications specifications = new RoddenRatingSpecifications();
            RoddenRatingDetails details = specifications.DetailsForRating(roddenRating);
            Assert.IsNotNull(details);
            Assert.AreEqual(roddenRating, details.Rating);
            Assert.AreEqual("ref.enum.roddenrating.c", details.TextId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            IRoddenRatingSpecifications specifications = new RoddenRatingSpecifications();

            foreach (RoddenRatings roddenRating in Enum.GetValues(typeof(RoddenRatings)))
            {
                RoddenRatingDetails details = specifications.DetailsForRating(roddenRating);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.TextId.Length > 0);
            }
        }

        [TestMethod]
        public void TestRetrievingWithIndex()
        {
            IRoddenRatingSpecifications specifications = new RoddenRatingSpecifications();
            int roddenRatingIndex = 2;
            RoddenRatings roddenRating = specifications.RoddenRatingForIndex(roddenRatingIndex);
            Assert.AreEqual(RoddenRatings.A, roddenRating);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrievingWithWrongIndex()
        {
            IRoddenRatingSpecifications specifications = new RoddenRatingSpecifications();
            int roddenRatingIndex = 1000;
            _ = specifications.RoddenRatingForIndex(roddenRatingIndex);
        }

    }



    [TestClass]
    public class TestTimeZones
    {
        [TestMethod]
        public void TestRetrievingDetails()
        {
            double delta = 0.00000001;
            TimeZones timeZone = TimeZones.AZOT;
            ITimeZoneSpecifications specifications = new TimeZoneSpecifications();
            TimeZoneDetails details = specifications.DetailsForTimeZone(timeZone);
            Assert.IsNotNull(details);
            Assert.AreEqual(timeZone, details.TimeZone);
            Assert.AreEqual(-1.0, details.OffsetFromUt, delta);
            Assert.AreEqual("ref.enum.timezone.azot", details.TextId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            IRoddenRatingSpecifications specifications = new RoddenRatingSpecifications();

            foreach (RoddenRatings roddenRating in Enum.GetValues(typeof(RoddenRatings)))
            {
                RoddenRatingDetails details = specifications.DetailsForRating(roddenRating);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.TextId.Length > 0);
            }
        }

        [TestMethod]
        public void TestRetrievingWithIndex()
        {
            ITimeZoneSpecifications specifications = new TimeZoneSpecifications();
            int timeZoneIndex = 29;
            TimeZones timeZone = specifications.TimeZoneForIndex(timeZoneIndex);
            Assert.AreEqual(TimeZones.BRT, timeZone);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrievingWithWrongIndex()
        {
            ITimeZoneSpecifications specifications = new TimeZoneSpecifications();
            int timeZoneIndex = -100;
            _ = specifications.TimeZoneForIndex(timeZoneIndex);
        }

    }



}