// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Astron.Houses;
using E4C.Core.Facades;
using E4C.Shared.Domain;
using E4C.Shared.References;
using Moq;
using NUnit.Framework;


namespace E4CTest.core.astron.houses;

[TestFixture]
public class TestHousesCalc
{
    private readonly double _geoLat = 52.0;
    private readonly double _geoLon = 6.53;
    private readonly double _obliquity = 23.447;
    private readonly char _houseSystemId = 'p';
    private readonly int _flags = 0;
    private Location? _location;
    private readonly double[] _cusps = new double[] { 0.0, 10.0, 40.0, 70.0, 100.0, 130.0, 160.0, 190.0, 220.0, 250.0, 280.0, 310.0, 340.0 };
    private readonly double[] _otherPoints = new double[] { 10.0, 280.0, 281.0, 192.0, 12.0 };
    private double[][]? _calculatedResults;

    [SetUp]
    public void SetU()
    {
        _location = new Location("LocationName", _geoLon, _geoLat);
        _calculatedResults = PerformTestCalculateHouses();
    }


    [Test]
    public void TestCusps()
    {
        Assert.NotNull(_calculatedResults);
        Assert.AreEqual(_cusps, _calculatedResults[0]);
    }

    [Test]
    public void TestOtherPoints()
    {
        Assert.NotNull(_calculatedResults);
        Assert.AreEqual(_otherPoints, _calculatedResults[1]);
    }


    private double[][] PerformTestCalculateHouses()
    {
        double jd = 12345.678;
        IHousesCalc calc = CreateHousesCalc();
        return calc.CalculateHouses(jd, _obliquity, _location, _houseSystemId, _flags);
    }

    private IHousesCalc CreateHousesCalc()
    {
        char houseSystemId = 'p';
        int flags = 0;
        double jd = 12345.678;
        double[][] positions = { _cusps, _otherPoints };
        var mockFacade = new Mock<IHousesFacade>();
        mockFacade.Setup(p => p.RetrieveHouses(jd, flags, _geoLat, _geoLon, houseSystemId)).Returns(positions);
        var mockSpecs = new Mock<IHouseSystemSpecs>();
        mockSpecs.Setup(p => p.DetailsForHouseSystem(HouseSystems.Placidus)).Returns(new HouseSystemDetails(HouseSystems.Placidus, true, _houseSystemId, 12, true, true, "textId"));
        HousesCalc calc = new(mockFacade.Object, mockSpecs.Object);
        return calc;
    }
}
