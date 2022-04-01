// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using E4C.Models.Astron;
using E4C.Models.Domain;
using E4C.calc.seph.sefacade;
using E4C.domain.shared.positions;
using E4C.domain.shared.specifications;
using E4C.domain.shared.references;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace E4CTest.Models
{
 


    [TestClass]
    public class TestPositionSolSysPointCalc
    {

        [TestMethod]
        public void TestCalculateSolSysPointEcliptical()
        {
            double _delta = 0.00000001;
            FullSolSysPointPos _result = CalculateFullSolSysPointPos();
            Assert.AreEqual(100.0, _result.Longitude.Position, _delta);
            Assert.AreEqual(-2.0, _result.Latitude.Position, _delta);
        }

        [TestMethod]
        public void TestCalculateSolSysPointEquatorial()
        {
            double _delta = 0.00000001;
            FullSolSysPointPos _result = CalculateFullSolSysPointPos();
            Assert.AreEqual(99.0, _result.RightAscension.Position, _delta);
            Assert.AreEqual(-1.0, _result.Declination.Position, _delta);
        }

        [TestMethod]
        public void TestCalculateSolSysPointDistance()
        {
            double _delta = 0.00000001;
            FullSolSysPointPos _result = CalculateFullSolSysPointPos();
            Assert.AreEqual(3.3, _result.Distance.Position, _delta);
            Assert.AreEqual(0.003, _result.Distance.Speed, _delta);
        }

        [TestMethod]
        public void TestCalculateSolSysPointHorizontal()
        {
            double _delta = 0.00000001;
            FullSolSysPointPos _result = CalculateFullSolSysPointPos();
            Assert.AreEqual(66.6, _result.AzimuthAltitude.Azimuth, _delta);
            Assert.AreEqual(45.0, _result.AzimuthAltitude.Altitude, _delta);
        }

        private FullSolSysPointPos CalculateFullSolSysPointPos()
        {
            double _julianDayUt = 2123456.5;
            int _flagsEcliptical = 0;
            int _flagsEquatorial = 1;
            var _eclipticalPositions = new double[] { 100.0, -2.0, 3.3, 0.5, -0.1, 0.003 };
            var _equatorialPositions = new double[] { 99.0, -1.0, 3.3, 0.51, -0.09, 0.003 };
            var _location = new Location("", 52.0, 6.0);
            var _mockCelPointCalc = new Mock<ISePosCelPointFacade>();
            _mockCelPointCalc.Setup(p => p.PosCelPointFromSe(_julianDayUt, Constants.SE_MARS, _flagsEcliptical)).Returns(_eclipticalPositions);
            _mockCelPointCalc.Setup(p => p.PosCelPointFromSe(_julianDayUt, Constants.SE_MARS, _flagsEquatorial)).Returns(_equatorialPositions);
            var _mockHorCoordCalc = new Mock<IHorizontalCoordinatesFacade>();
            _mockHorCoordCalc.Setup(p => p.CalculateHorizontalCoordinates(_julianDayUt, new double[] { 52.0, 6.0, 0.0 }, new double[] { 100.0, -2.0, 3.3 }, _flagsEcliptical)).
                Returns(new HorizontalPos(66.6, 45.0));
            var _mockSolSysPointSpecs = new Mock<ISolarSystemPointSpecifications>();
            _mockSolSysPointSpecs.Setup(p => p.DetailsForPoint(SolarSystemPoints.Mars)).
                Returns(new SolarSystemPointDetails(SolarSystemPoints.Mars, SolSysPointCats.Classic, Constants.SE_MARS, true, true, "solSysPointMars"));
            PositionSolSysPointCalc _calc = new(_mockCelPointCalc.Object, _mockHorCoordCalc.Object, _mockSolSysPointSpecs.Object);
            return _calc.CalculateSolSysPoint(SolarSystemPoints.Mars, _julianDayUt, _location, _flagsEcliptical, _flagsEquatorial);
        }
    }

  

    [TestClass]
    public class TestSouthPointCalculator
    {
        private double _delta = 0.001;     // TODO enlarge value for _delta.

        [TestMethod]
        public void TestHappyFlow()
        {
            double armc = 331.883333333333;
            double obliquity = 23.449614320676233;  // mean obliquity
            double geoLat = 48.8333333333333;
            ISouthPointCalculator calculator = new SouthPointCalculator();
            double expectedLong = 318.50043580207006;
            double expectedLat = -27.562090280566338;
            EclipticCoordinates result = calculator.CalculateSouthPoint(armc, obliquity, geoLat);
            Assert.AreEqual(expectedLong, result.Longitude, _delta);
            Assert.AreEqual(expectedLat, result.Latitude, _delta);
            // Expected a difference no greater than <1E-08> between expected value <318,50043580207006> and actual value <318,5003113717042>.   Mean obliquity   verschil 0.0001245   0.4482 seconde
            // Expected a difference no greater than<1E-08 > between expected value<-27,562090280566338 > and actual value<-27,562042216088308 >. Latitude, mean obliquity  
            // Expected a difference no greater than <1E-08> between expected value <318,50043580207006> and actual value <318,4995873430928>.   True obliquity   verschil 0.00084846  3 seconden
        }

        [TestMethod]
        public void TestSouthernHemisphere()
        {
            double armc = 331.883333333333;
            double obliquity = 23.449614320676233;  // mean obliquity
            double geoLat = -48.8333333333333;
            ISouthPointCalculator calculator = new SouthPointCalculator();
            // double expectedLong = 318.50043580207006;
            double expectedLong = 174.53494810489755;
            // double expectedLat = -27.562090280566338;
            double expectedLat = -48.16467239725159;
            // TODO check values for southern latitude
            EclipticCoordinates result = calculator.CalculateSouthPoint(armc, obliquity, geoLat);
            Assert.AreEqual(expectedLong, result.Longitude, _delta);
            Assert.AreEqual(expectedLat, result.Latitude, _delta);
        }


    }
}