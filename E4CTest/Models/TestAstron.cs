// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Astron;
using E4C.Models.Domain;
using E4C.Models.SeFacade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace E4CTest.Models
{
    [TestClass]
    public class TestFlagDefinitions
    {
        // relevant values:
        // SEFLG_SWIEPH       2L              // use SWISSEPH ephemeris, default
        // SEFLG_HELCTR       8L              // return heliocentric position
        // SEFLG_SPEED       256L            // high precision speed (analyt. comp.)
        // SEFLG_EQUATORIAL   2048L           // equatorial positions are wanted
        // SEFLG_TOPOCTR      (32*1024L)      // topocentric positions
        // SEFLG_SIDEREAL     (64*1024L)      // sidereal positions

        // Flag is always 2L || 256L
        // Variables: heliocentric, topocentric, sidereal

        [TestMethod]
        public void TestDefineFlagsDefault()
        {
            FullChartRequest _request = CreateRequest(false, false, false);
            Assert.AreEqual(258, new FlagDefinitions().DefineFlags(_request));
        }

        [TestMethod]
        public void TestDefineFlagsHeliocentric()
        {
            FullChartRequest _request = CreateRequest(true, false, false);
            Assert.AreEqual(266, new FlagDefinitions().DefineFlags(_request));
        }

        [TestMethod]
        public void TestDefineFlagsTopocentric()
        {
            FullChartRequest _request = CreateRequest(false, true, false);
            Assert.AreEqual(32 * 1024 + 258, new FlagDefinitions().DefineFlags(_request));
        }

        [TestMethod]
        public void TestDefineFlagsSidereal()
        {
            FullChartRequest _request = CreateRequest(false, false, true);
            Assert.AreEqual(64 * 1024 + 258, new FlagDefinitions().DefineFlags(_request));
        }

        [TestMethod]
        public void TestDefineFlagsCombi()
        {
            FullChartRequest _request = CreateRequest(false, true, true);
            Assert.AreEqual(96 * 1024 + 258, new FlagDefinitions().DefineFlags(_request));
        }

        [TestMethod]
        public void TestAddEquatorial()
        {
            int eclipticFlags = 258;
            int equatorialFlags = new FlagDefinitions().AddEquatorial(eclipticFlags);
            Assert.AreEqual(2306, equatorialFlags);
        }

        private FullChartRequest CreateRequest(bool helioCentric, bool topoCentric, bool sidereal)
        {
            double _jdUt = 123456.789;
            var _location = new Location("", 50.0, 10.0);
            var _solSysPoints = new List<SolarSystemPoints>();
            var _houseSystem = HouseSystems.Campanus;
            var _zodiactType = sidereal ? ZodiacTypes.Sidereal : ZodiacTypes.Tropical;
            var _ayanamsha = Ayanamshas.Fagan;
            var _observerPosition = ObserverPositions.GeoCentric;
            if (helioCentric) _observerPosition = ObserverPositions.HelioCentric;
            if (topoCentric) _observerPosition = ObserverPositions.TopoCentric;
            var _projectionType = ProjectionTypes.twoDimensional;
            return new FullChartRequest(_jdUt, _location, _solSysPoints, _houseSystem, _zodiactType, _ayanamsha, _observerPosition, _projectionType);
        }

    }


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
    public class TestCalculatedMundanePositions
    {
        // testvalues used:
        // cusp/point     longitude right ascension  declination
        // 1               10.0      11.0             1.0
        // 2               40.0      41.0             2.0
        // 3               70.0      71.0             3.0
        // 4              100.0     101.0             2.5
        // 5              130.0     131.0             1.5
        // 6              160.0     161.0             0.5
        // 7              190.0     191.0            -0.5
        // 8              220.0     221.0            -1.5
        // 9              250.0     251.0            -2.5
        // 10             280.0     281.0            -3.0
        // 11             310.0     311.0            -2.0
        // 12             340.0     341.0            -1.0
        // ascendant       10.0      11.0             1.0
        // MC             280.0     281.0            -3.0
        // armc           285.0       0.0             0.0
        // vertex         192.0     193.0            -0.4
        // eastpoint       12.0      13.0             1.2

        readonly double _delta = 0.00000001;

        [TestMethod]
        public void TestCusps()
        {
            MundanePositions _result = CalculateMundanePositions();
            // Cusp 1
            Assert.AreEqual(10.0, _result.Cusps[0].Longitude, _delta);
            Assert.AreEqual(11.0, _result.Cusps[0].RightAscension, _delta);
            Assert.AreEqual(1.0, _result.Cusps[0].Declination, _delta);
            // Cusp 9
            Assert.AreEqual(250.0, _result.Cusps[8].Longitude, _delta);
            Assert.AreEqual(251.0, _result.Cusps[8].RightAscension, _delta);
            Assert.AreEqual(-2.5, _result.Cusps[8].Declination, _delta);
        }


        [TestMethod]
        public void TestAscendant()
        {
            MundanePositions _result = CalculateMundanePositions();
            Assert.AreEqual(280.0, _result.Ascendant.Longitude, _delta);
            Assert.AreEqual(281.0, _result.Ascendant.RightAscension, _delta);
            Assert.AreEqual(-3.0, _result.Ascendant.Declination, _delta);
        }

        [TestMethod]
        public void TestMc()
        {
            MundanePositions _result = CalculateMundanePositions();
            Assert.AreEqual(10.0, _result.Mc.Longitude, _delta);
            Assert.AreEqual(11.0, _result.Mc.RightAscension, _delta);
            Assert.AreEqual(1.0, _result.Mc.Declination, _delta);
        }

        [TestMethod]
        public void TestVertex()
        {
            MundanePositions _result = CalculateMundanePositions();
            Assert.AreEqual(192.0, _result.Vertex.Longitude, _delta);
            Assert.AreEqual(193.0, _result.Vertex.RightAscension, _delta);
            Assert.AreEqual(-0.4, _result.Vertex.Declination, _delta);
        }

        [TestMethod]
        public void TestEastPoint()
        {
            MundanePositions _result = CalculateMundanePositions();
            Assert.AreEqual(12.0, _result.EastPoint.Longitude, _delta);
            Assert.AreEqual(13.0, _result.EastPoint.RightAscension, _delta);
            Assert.AreEqual(1.2, _result.EastPoint.Declination, _delta);
        }

        private MundanePositions CalculateMundanePositions()
        {
            double _jdUt = 2123456.5;
            double _obliquity = 23.447;
            var _location = new Location("", 52.0, 6.0);
            char _houseSysId = 'B';   // Alcabitius
            var _houseSystem = HouseSystems.Alcabitius;
            int _flags = 0;
            var _cusps = new double[] { 0.0, 10.0, 40.0, 70.0, 100.0, 130.0, 160.0, 190.0, 220.0, 250.0, 280.0, 310.0, 340.0 };
            var _otherPoints = new double[] { 10.0, 280.0, 281.0, 192.0, 12.0 };
            var _geoCoord = new double[] { 52.0, 6.0, 1.0 };
            var _facadeResult = new double[][] { _cusps, _otherPoints };
            var _mockPosHousesFacade = new Mock<ISePosHousesFacade>();
            _mockPosHousesFacade.Setup(p => p.PosHousesFromSe(_jdUt, _flags, _location.GeoLat, _location.GeoLong, _houseSysId)).Returns(_facadeResult);
            var _mockCoordinateConversionFacade = new Mock<ICoordinateConversionFacade>();
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 10.0, 0.0 }, _obliquity)).Returns(new double[] { 11.0, 1.0 });       // cusp 1 and ascendant
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 40.0, 0.0 }, _obliquity)).Returns(new double[] { 41.0, 2.0 });     // cusp 2 
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 70.0, 0.0 }, _obliquity)).Returns(new double[] { 71.0, 3.0 });      // cusp 3
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 100.0, 0.0 }, _obliquity)).Returns(new double[] { 101.0, 2.5 });    // cusp 4
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 130.0, 0.0 }, _obliquity)).Returns(new double[] { 131.0, 1.5 });    // cusp 5
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 160.0, 0.0 }, _obliquity)).Returns(new double[] { 161.0, 0.5 });    // cusp 6
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 190.0, 0.0 }, _obliquity)).Returns(new double[] { 191.0, -0.5 });   // cusp 7
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 220.0, 0.0 }, _obliquity)).Returns(new double[] { 221.0, -1.5 });   // cusp 8
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 250.0, 0.0 }, _obliquity)).Returns(new double[] { 251.0, -2.5 });   // cusp 9
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 280.0, 0.0 }, _obliquity)).Returns(new double[] { 281.0, -3.0 });   // cusp 10 and MC
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 310.0, 0.0 }, _obliquity)).Returns(new double[] { 311.0, -2.0 });   // cusp 11
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 340.0, 0.0 }, _obliquity)).Returns(new double[] { 341.0, -1.0 });   // cusp 12
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 285.0, 0.0 }, _obliquity)).Returns(new double[] { 0.0, 0.0 });      // armc, ignored
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 192.0, 0.0 }, _obliquity)).Returns(new double[] { 193.0, -0.4 });   // vertex
            _mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 12.0, 0.0 }, _obliquity)).Returns(new double[] { 13.0, 1.2 });      // eastpoint
            var _mockHorizontalCoordinatesFacade = new Mock<IHorizontalCoordinatesFacade>();
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 10.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(29.0, 0.0));         // cusp 1 and ascendant
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 40.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(59.0, -10.0));       // cusp 2
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 70.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(89.0, -20.0));       // cusp 3
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 100.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(119.0, -30.0));     // cusp 4
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 130.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(149.0, -22.0));     // cusp 5
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 160.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(179.0, -12.0));     // cusp 6
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 190.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(209.0, 0.0));       // cusp 7
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 220.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(239.0, 10.0));      // cusp 8
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 250.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(269.0, 20.0));      // cusp 9
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 280.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(299.0, 30.0));      // cusp 10 and MC
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 310.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(319.0, 23.0));      // cusp 11
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 340.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(349, 33.0));        // cusp 12
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 285.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(0.0, 0.0));         // armc, ignored
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 192.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(311.0, 2.0));       // vertex
            _mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(_jdUt, _geoCoord, new double[] { 12.0, 0.0, 0.0 }, _flags)).Returns(new HorizontalPos(31.0, -8.0));        // eastpoint



            var _mockHouseSystemSpecifications = new Mock<IHouseSystemSpecifications>();
            _mockHouseSystemSpecifications.Setup(p => p.DetailsForHouseSystem(HouseSystems.Alcabitius)).
                Returns(new HouseSystemDetails(_houseSystem, true, 'B', 12, true, true, "houseSystemAlcabitius"));
            PositionsMundane positions = new(_mockPosHousesFacade.Object, _mockCoordinateConversionFacade.Object, _mockHorizontalCoordinatesFacade.Object, _mockHouseSystemSpecifications.Object);
            return positions.CalculateAllMundanePositions(_jdUt, _obliquity, _flags, _location, _houseSystem);
        }

    }


}