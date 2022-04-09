// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.calc.seph.secalculations;
using E4C.calc.seph.sefacade;
using E4C.domain.shared.references;
using E4C.domain.shared.positions;
using E4C.domain.shared.specifications;
using Moq;
using NUnit.Framework;

namespace E4CTest.testsrc.calc.seph.secalculations;

[TestFixture]
public class TestMundanePositionsCalc
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

    [Test]
    public void TestCusp1()
    {
        FullMundanePositions result = CalculateMundanePositions();
        Assert.AreEqual(10.0, result.Cusps[0].Longitude, _delta);
        Assert.AreEqual(11.0, result.Cusps[0].RightAscension, _delta);
        Assert.AreEqual(1.0, result.Cusps[0].Declination, _delta);
    }

    [Test]
    public void TestCusp9()
    {
        FullMundanePositions result = CalculateMundanePositions();
        Assert.AreEqual(250.0, result.Cusps[8].Longitude, _delta);
        Assert.AreEqual(251.0, result.Cusps[8].RightAscension, _delta);
        Assert.AreEqual(-2.5, result.Cusps[8].Declination, _delta);
    }

    [Test]
    public void TestCusp12()
    {
        FullMundanePositions result = CalculateMundanePositions();
        Assert.AreEqual(340.0, result.Cusps[11].Longitude, _delta);
        Assert.AreEqual(341.0, result.Cusps[11].RightAscension, _delta);
        Assert.AreEqual(-1.0, result.Cusps[11].Declination, _delta);
    }

    [Test]
    public void TestAscendant()
    {
        FullMundanePositions result = CalculateMundanePositions();
        Assert.AreEqual(280.0, result.Ascendant.Longitude, _delta);
        Assert.AreEqual(281.0, result.Ascendant.RightAscension, _delta);
        Assert.AreEqual(-3.0, result.Ascendant.Declination, _delta);
    }

    [Test]
    public void TestMc()
    {
        FullMundanePositions result = CalculateMundanePositions();
        Assert.AreEqual(10.0, result.Mc.Longitude, _delta);
        Assert.AreEqual(11.0, result.Mc.RightAscension, _delta);
        Assert.AreEqual(1.0, result.Mc.Declination, _delta);
    }

    [Test]
    public void TestVertex()
    {
        FullMundanePositions result = CalculateMundanePositions();
        Assert.AreEqual(192.0, result.Vertex.Longitude, _delta);
        Assert.AreEqual(193.0, result.Vertex.RightAscension, _delta);
        Assert.AreEqual(-0.4, result.Vertex.Declination, _delta);
    }

    [Test]
    public void TestEastPoint()
    {
        FullMundanePositions result = CalculateMundanePositions();
        Assert.AreEqual(12.0, result.EastPoint.Longitude, _delta);
        Assert.AreEqual(13.0, result.EastPoint.RightAscension, _delta);
        Assert.AreEqual(1.2, result.EastPoint.Declination, _delta);
    }

    private FullMundanePositions CalculateMundanePositions()
    {
        double jdUt = 2123456.5;
        double obliquity = 23.447;
        var location = new Location("", 52.0, 6.0);
        char houseSysId = 'B';   // Alcabitius
        var houseSystem = HouseSystems.Alcabitius;
        int flags = 0;
        var cusps = new double[] { 0.0, 10.0, 40.0, 70.0, 100.0, 130.0, 160.0, 190.0, 220.0, 250.0, 280.0, 310.0, 340.0 };
        var otherPoints = new double[] { 10.0, 280.0, 281.0, 192.0, 12.0 };
        var geoCoord = new double[] { 52.0, 6.0, 1.0 };
        var facadeResult = new double[][] { cusps, otherPoints };
        var mockPosHousesFacade = new Mock<ISePosHousesFacade>();
        mockPosHousesFacade.Setup(p => p.PosHousesFromSe(jdUt, flags, location.GeoLat, location.GeoLong, houseSysId)).Returns(facadeResult);

        var mockCoordinateConversionFacade = new Mock<ICoordinateConversionFacade>();
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 10.0, 0.0 }, obliquity)).Returns(new double[] { 11.0, 1.0 });      // cusp 1 and ascendant
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 40.0, 0.0 }, obliquity)).Returns(new double[] { 41.0, 2.0 });      // cusp 2 
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 70.0, 0.0 }, obliquity)).Returns(new double[] { 71.0, 3.0 });      // cusp 3
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 100.0, 0.0 }, obliquity)).Returns(new double[] { 101.0, 2.5 });    // cusp 4
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 130.0, 0.0 }, obliquity)).Returns(new double[] { 131.0, 1.5 });    // cusp 5
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 160.0, 0.0 }, obliquity)).Returns(new double[] { 161.0, 0.5 });    // cusp 6
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 190.0, 0.0 }, obliquity)).Returns(new double[] { 191.0, -0.5 });   // cusp 7
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 220.0, 0.0 }, obliquity)).Returns(new double[] { 221.0, -1.5 });   // cusp 8
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 250.0, 0.0 }, obliquity)).Returns(new double[] { 251.0, -2.5 });   // cusp 9
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 280.0, 0.0 }, obliquity)).Returns(new double[] { 281.0, -3.0 });   // cusp 10 and MC
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 310.0, 0.0 }, obliquity)).Returns(new double[] { 311.0, -2.0 });   // cusp 11
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 340.0, 0.0 }, obliquity)).Returns(new double[] { 341.0, -1.0 });   // cusp 12
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 285.0, 0.0 }, obliquity)).Returns(new double[] { 0.0, 0.0 });      // armc, ignored
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 192.0, 0.0 }, obliquity)).Returns(new double[] { 193.0, -0.4 });   // vertex
        mockCoordinateConversionFacade.Setup(p => p.EclipticToEquatorial(new double[] { 12.0, 0.0 }, obliquity)).Returns(new double[] { 13.0, 1.2 });      // eastpoint

        var mockHorizontalCoordinatesFacade = new Mock<IHorizontalCoordinatesFacade>();
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 10.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(29.0, 0.0));         // cusp 1 and ascendant
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 40.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(59.0, -10.0));       // cusp 2
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 70.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(89.0, -20.0));       // cusp 3
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 100.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(119.0, -30.0));     // cusp 4
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 130.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(149.0, -22.0));     // cusp 5
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 160.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(179.0, -12.0));     // cusp 6
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 190.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(209.0, 0.0));       // cusp 7
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 220.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(239.0, 10.0));      // cusp 8
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 250.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(269.0, 20.0));      // cusp 9
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 280.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(299.0, 30.0));      // cusp 10 and MC
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 310.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(319.0, 23.0));      // cusp 11
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 340.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(349, 33.0));        // cusp 12
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 285.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(0.0, 0.0));         // armc, ignored
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 192.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(311.0, 2.0));       // vertex
        mockHorizontalCoordinatesFacade.Setup(p => p.CalculateHorizontalCoordinates(jdUt, geoCoord, new double[] { 12.0, 0.0, 0.0 }, flags)).Returns(new HorizontalPos(31.0, -8.0));        // eastpoint

        var mockHouseSystemSpecifications = new Mock<IHouseSystemSpecs>();
        mockHouseSystemSpecifications.Setup(p => p.DetailsForHouseSystem(HouseSystems.Alcabitius)).
            Returns(new HouseSystemDetails(houseSystem, true, 'B', 12, true, true, "houseSystemAlcabitius"));
        MundanePositionsCalculator positions = new(mockPosHousesFacade.Object, mockCoordinateConversionFacade.Object, mockHorizontalCoordinatesFacade.Object, mockHouseSystemSpecifications.Object);
        return positions.CalculateAllMundanePositions(jdUt, obliquity, flags, location, houseSystem);
    }

}
