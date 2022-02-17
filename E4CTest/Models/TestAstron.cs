// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using E4C.Models.Domain;
using E4C.Models.Astron;
using E4C.Models.SeFacade;

namespace E4CTest.Models
{
    [TestClass]
    public class TestPositionSolSysPointCalc
    {
        // Tests are performed with mocked positions for Mars, using a jd of 2123456.5 and a location  52°00'00" North, 6°00'00" East.

        [TestMethod]
        public void TestCalculateSolSysPointEcliptical()
        {
            double _delta = 0.00000001;
            CalculatedFullSolSysPointPosition _result = CalculateFullSolSysPointPos();
            Assert.AreEqual(100.0, _result.EclipticalPosition[0], _delta);
            Assert.AreEqual(-2.0, _result.EclipticalPosition[1], _delta);
            Assert.AreEqual(0.5, _result.EclipticalPosition[2], _delta);
            Assert.AreEqual(-0.1, _result.EclipticalPosition[3], _delta);
        }

        [TestMethod]
        public void TestCalculateSolSysPointEquatorial()
        {
            double _delta = 0.00000001;
            CalculatedFullSolSysPointPosition _result = CalculateFullSolSysPointPos();
            Assert.AreEqual(99.0, _result.EquatorialPosition[0], _delta);
            Assert.AreEqual(-1.0, _result.EquatorialPosition[1], _delta);
            Assert.AreEqual(0.51, _result.EquatorialPosition[2], _delta);
            Assert.AreEqual(-0.09, _result.EquatorialPosition[3], _delta);
        }

        [TestMethod]
        public void TestCalculateSolSysPointDistance()
        {
            double _delta = 0.00000001;
            CalculatedFullSolSysPointPosition _result = CalculateFullSolSysPointPos();
            Assert.AreEqual(3.3, _result.Distance[0], _delta);
            Assert.AreEqual(0.003, _result.Distance[1], _delta);
        }

        [TestMethod]
        public void TestCalculateSolSysPointHorizontal()
        {
            double _delta = 0.00000001;
            CalculatedFullSolSysPointPosition _result = CalculateFullSolSysPointPos();
            Assert.AreEqual(66.6, _result.HorizontalPosition[0], _delta);
            Assert.AreEqual(45.0, _result.HorizontalPosition[1], _delta);
 //           Assert.AreEqual(45.1, _result.HorizontalPosition[2], _delta);
        }

        private CalculatedFullSolSysPointPosition CalculateFullSolSysPointPos()
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
            _mockHorCoordCalc.Setup(p => p.CalculateHorizontalCoordinates(_julianDayUt, new double[] { 52.0, 6.0, 0.0 }, new double[] { 100.0, -2.0, 3.3 })).
                Returns(new double[] { 66.6, 45.0, 45.1 });
            var _mockSolSysPointSpecs = new Mock<ISolarSystemPointSpecifications>();
            _mockSolSysPointSpecs.Setup(p => p.DetailsForPoint(SolarSystemPoints.Mars)).
                Returns(new SolarSystemPointDetails(SolarSystemPoints.Mars, SolSysPointCats.Classic, Constants.SE_MARS, true, true, "solSysPointMars"));
            PositionSolSysPointCalc _calc = new(_mockCelPointCalc.Object, _mockHorCoordCalc.Object, _mockSolSysPointSpecs.Object);
            return _calc.CalculateSolSysPoint(SolarSystemPoints.Mars, _julianDayUt, _location, _flagsEcliptical, _flagsEquatorial);
        }
    }
}