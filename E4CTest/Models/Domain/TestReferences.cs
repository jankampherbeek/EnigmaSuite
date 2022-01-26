// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
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
            Assert.AreEqual(solSysPointCat, details.category);
            Assert.AreEqual("enumSolSysPointCatModern", details.textId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            ISolSysPointCatSpecifications specifications = new SolSysPointCatSpecifications();
            foreach (SolSysPointCats category in Enum.GetValues(typeof(SolSysPointCats)))
            {
                SolSysPointCatDetails details = specifications.DetailsForCategory(category);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.textId.Length > 0);
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
            Assert.AreEqual(point, details.solarSystemPoint);
            Assert.AreEqual(SolSysPointCats.Modern, details.solSysPointCat);
            Assert.AreEqual(Constants.SE_NEPTUNE, details.seId);
            Assert.IsTrue(details.useForHeliocentric);
            Assert.IsTrue(details.useForGeocentric);
            Assert.AreEqual("solSysPointNeptune", details.textId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            ISolarSystemPointSpecifications specifications = new SolarSystemPointSpecifications();
            foreach (SolarSystemPoints point in Enum.GetValues(typeof(SolarSystemPoints)))
            {
                SolarSystemPointDetails details = specifications.DetailsForPoint(point);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.textId.Length > 0);
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
            Assert.AreEqual(system, details.coordinateSystem);
            Assert.AreEqual(Constants.SEFLG_EQUATORIAL, details.valueForFlag);
            Assert.AreEqual("coordinateSysEquatorial", details.textId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            ICoordinateSystemSpecifications specifications = new CoordinateSystemSpecifications();
            foreach (CoordinateSystems system in Enum.GetValues(typeof(CoordinateSystems)))
            {
                CoordinateSystemDetails details = specifications.DetailsForCoordinateSystem(system);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.textId.Length > 0);
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
            Assert.AreEqual(position, details.observerPosition);
            Assert.AreEqual(Constants.SEFLG_TOPOCTR, details.valueForFlag);
            Assert.AreEqual("observerPosTopoCentric", details.textId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            IObserverPositionSpecifications specifications = new ObserverPositionSpecifications();
            foreach (ObserverPositions position in Enum.GetValues(typeof(ObserverPositions)))
            {
                ObserverPositionDetails details = specifications.DetailsForObserverPosition(position);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.textId.Length > 0);
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
            Assert.AreEqual(zodiacType, details.zodiacType);
            Assert.AreEqual(Constants.SEFLG_SIDEREAL, details.valueForFlag);
            Assert.AreEqual("zodiacTypeSidereal", details.textId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            IZodiacTypeSpecifications specifications = new ZodiacTypeSpecifications();
            foreach (ZodiacTypes zodiacType in Enum.GetValues(typeof(ZodiacTypes)))
            {
                ZodiacTypeDetails details = specifications.DetailsForZodiacType(zodiacType);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.textId.Length > 0);
            }
        }
    }

    [TestClass]
    public class TestHouseSystemSpecifications
    {
        [TestMethod]
        public void TestRetrievingDetails()
        {
            HouseSystems houseSystem = HouseSystems.Regiomontanus;
            IHouseSystemSpecifications specifications = new HouseSystemSpecifications();
            HouseSystemDetails details = specifications.DetailsForHouseSystem(houseSystem);
            Assert.IsNotNull(details);
            Assert.AreEqual(houseSystem, details.HouseSystem);
            Assert.AreEqual('R', details.SeId);
            Assert.AreEqual(12, details.NrOfCusps);
            Assert.IsTrue(details.CounterClockWise);
            Assert.IsTrue(details.QuadrantSystem);
            Assert.AreEqual("houseSystemRegiomontanus", details.TextId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            IHouseSystemSpecifications specifications = new HouseSystemSpecifications();
            foreach (HouseSystems system in Enum.GetValues(typeof(HouseSystems)))
            {
                HouseSystemDetails details = specifications.DetailsForHouseSystem(system);
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
            Assert.AreEqual(calendar, details.calendar);
            Assert.AreEqual("ref.enumcalendarjulian", details.textId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            ICalendarSpecifications specifications = new CalendarSpecifications();
            foreach (Calendars calendar in Enum.GetValues(typeof(Calendars)))
            {
                CalendarDetails details = specifications.DetailsForCalendar(calendar);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.textId.Length > 0);
            }
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
            Assert.AreEqual(yearCount, details.yearCount);
            Assert.AreEqual("ref.enumyearcountbce", details.textId);
        }

        [TestMethod]
        public void TestAvailabilityOfDetailsForAllEnums()
        {
            IYearCountSpecifications specifications = new YearCountSpecifications();
            foreach (YearCounts yearCount in Enum.GetValues(typeof(YearCounts)))
            {
                YearCountDetails details = specifications.DetailsForYearCount(yearCount);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.textId.Length > 0);
            }
        }
    }


}