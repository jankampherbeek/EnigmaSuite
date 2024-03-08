// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Facades.Se;
using FakeItEasy;

namespace Enigma.Test.Core.Calc;

[TestFixture]
public class TestHousesCalc
{
    private const double GEO_LAT = 52.0;
    private const double GEO_LON = 6.53;
    private const double OBLIQUITY = 23.447;
    private const char HOUSE_SYSTEM_ID = 'p';
    private const int FLAGS = 0;
    private Location? _location;
    private readonly double[] _cusps = { 0.0, 10.0, 40.0, 70.0, 100.0, 130.0, 160.0, 190.0, 220.0, 250.0, 280.0, 310.0, 340.0 };
    private readonly double[] _otherPoints = { 10.0, 280.0, 281.0, 192.0, 12.0 };
    private double[][]? _calculatedResults;

    [SetUp]
    public void SetU()
    {
        _location = new Location("LocationName", GEO_LON, GEO_LAT);
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
        return calc.CalculateHouses(jd, OBLIQUITY, _location!, HOUSE_SYSTEM_ID, FLAGS);
    }

    private IHousesCalc CreateHousesCalc()
    {
        const char houseSystemId = 'p';
        const int flags = 0;
        const double jd = 12345.678;
        double[][] positions = { _cusps, _otherPoints };
        var facadeFake = A.Fake<IHousesFacade>();
        A.CallTo(() => facadeFake.RetrieveHouses(jd, flags, GEO_LAT, GEO_LON, houseSystemId)).Returns(positions);
        HousesCalc calc = new(facadeFake);
        return calc;
    }
}
