// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Facades.Se.Interfaces;
using Enigma.Core.Handlers.Calc.Mundane.Helpers;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Moq;

namespace Enigma.Test.Core.Handlers.Calc.Mundane;

[TestFixture]
public class TestHousesCalc
{
    private const double GeoLat = 52.0;
    private const double GeoLon = 6.53;
    private const double Obliquity = 23.447;
    private const char HouseSystemId = 'p';
    private const int Flags = 0;
    private Location? _location;
    private readonly double[] _cusps = { 0.0, 10.0, 40.0, 70.0, 100.0, 130.0, 160.0, 190.0, 220.0, 250.0, 280.0, 310.0, 340.0 };
    private readonly double[] _otherPoints = { 10.0, 280.0, 281.0, 192.0, 12.0 };
    private double[][]? _calculatedResults;

    [SetUp]
    public void SetU()
    {
        _location = new Location("LocationName", GeoLon, GeoLat);
        _calculatedResults = PerformTestCalculateHouses();
    }


    [Test]
    public void TestCusps()
    {
        Assert.That(_calculatedResults, Is.Not.Null);
        Assert.That(_calculatedResults![0], Is.EqualTo(_cusps));
    }

    [Test]
    public void TestOtherPoints()
    {
        Assert.That(_calculatedResults, Is.Not.Null);
        Assert.That(_calculatedResults![1], Is.EqualTo(_otherPoints));
    }


    private double[][] PerformTestCalculateHouses()
    {
        const double jd = 12345.678;
        IHousesCalc calc = CreateHousesCalc();
        return calc.CalculateHouses(jd, Obliquity, _location!, HouseSystemId, Flags);
    }

    private IHousesCalc CreateHousesCalc()
    {
        const char houseSystemId = 'p';
        const int flags = 0;
        const double jd = 12345.678;
        double[][] positions = { _cusps, _otherPoints };
        var mockFacade = new Mock<IHousesFacade>();
        mockFacade.Setup(p => p.RetrieveHouses(jd, flags, GeoLat, GeoLon, houseSystemId)).Returns(positions);
        HousesCalc calc = new(mockFacade.Object);
        return calc;
    }
}
