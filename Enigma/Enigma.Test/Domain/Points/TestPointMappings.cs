// Jan Kampherbeek, (c) 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;

namespace Enigma.Test.Domain.Points;

/// <summary>Tests for the class PointMappings.</summary>
/// <remarks>Actually this is a minimal integrationtest, as it combines tests for PointMappings and for PointDefinitions.</remarks>
[TestFixture]
public class TestPointMappings
{

    private IPointMappings _mappings;

    [SetUp]
    public void SetUp()
    {
        _mappings = new PointMappings(new PointDefinitions());
    }


    [Test]
    public void TestIndexForCelPoint()              // offset for celestial points is 0
    {
        int expectedIndex = 9;
        CelPoints celPoint = CelPoints.Neptune;
        int resultingIndex = _mappings.IndexForCelPoint(celPoint);
        Assert.That(expectedIndex, Is.EqualTo(resultingIndex));
    }

    [Test]
    public void TestIndexForZodiacPoint()           // offset for zodiac points is 1000
    {
        int expectedIndex = 1001;
        ZodiacPoints zodiacPoint = ZodiacPoints.ZeroCancer;
        int resultingIndex = _mappings.IndexForZodiacPoint(zodiacPoint);
        Assert.That(expectedIndex, Is.EqualTo(resultingIndex));
    }

    [Test]
    public void TestIndexForArabicPoint()           // offset for arabic points is 2000
    {
        int expectedIndex = 2000;
        ArabicPoints arabicPoint = ArabicPoints.FortunaSect;
        int resultingIndex = _mappings.IndexForArabicPoint(arabicPoint);
        Assert.That(expectedIndex, Is.EqualTo(resultingIndex));
    }

    [Test]
    public void TestIndexForMundanePoint()           // offset for mundane points is 3000
    {
        int expectedIndex = 3002;
        MundanePoints mundanePoint = MundanePoints.EastPoint;
        int resultingIndex = _mappings.IndexForMundanePoint(mundanePoint);
        Assert.That(expectedIndex, Is.EqualTo(resultingIndex));
    }

    [Test]
    public void TestIndexForCusp()                  // offset for cusps is 4000
    {
        int expectedIndex = 4006;
        int cuspNr = 7;
        int resultingIndex = _mappings.IndexForCusp(cuspNr);
        Assert.That(expectedIndex, Is.EqualTo(resultingIndex));
    }

    [Test]
    public void TestGeneralPointForIndexHappyFlow()
    {
        int index = 41;
        string expectedPointName = "Hygieia";
        GeneralPoint resultingPoint = _mappings.GeneralPointForIndex(index);
        Assert.That(expectedPointName, Is.EqualTo(resultingPoint.Name));
    }

    [Test]
    public void TestGeneralPointForIndexMundanePoint()
    {
        int index = 3001;
        string expectedPointName = "MC";
        GeneralPoint resultingPoint = _mappings.GeneralPointForIndex(index);
        Assert.That(expectedPointName, Is.EqualTo(resultingPoint.Name));
    }

    [Test]
    public void TestGeneralPointForIndexNotExistingPoint()
    {
        int index = 9999;
        Assert.That(() => _ = _mappings.GeneralPointForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestPointTypeForIndex()
    {
        int index = 2100;
        PointTypes expectedPointType = PointTypes.ArabicPoint;
        PointTypes resultingPointType = _mappings.PointTypeForIndex(index);
        Assert.That(resultingPointType, Is.EqualTo(expectedPointType));
    }

    [Test]
    public void TestCelPointForIndexHappyFlow()
    {
        int index = 5;
        CelPoints expectedPoint = CelPoints.Mars;
        CelPoints resultingPoint = _mappings.CelPointForIndex(index);
        Assert.That(resultingPoint, Is.EqualTo(expectedPoint));
    }

    [Test]
    public void TestCelPointForIndexOutOfRange()
    {
        int index = 2020;
        Assert.That(() => _ = _mappings.CelPointForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestZodiacPointForIndexHappyFlow()
    {
        int index = 1000;
        ZodiacPoints expectedPoint = ZodiacPoints.ZeroAries;
        ZodiacPoints resultingPoint = _mappings.ZodiacPointForIndex(index);
        Assert.That(resultingPoint, Is.EqualTo(expectedPoint));
    }

    [Test]
    public void TestZodiacPointForIndexOutOfRange()
    {
        int index = 2000;
        Assert.That(() => _ = _mappings.ZodiacPointForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestArabicPointForIndexHappyFlow()
    {
        int index = 2001;
        ArabicPoints expectedPoint = ArabicPoints.FortunaNoSect;
        ArabicPoints resultingPoint = _mappings.ArabicPointForIndex(index);
        Assert.That(resultingPoint, Is.EqualTo(expectedPoint));
    }

    [Test]
    public void TestArabicPointForIndexOutOfRange()
    {
        int index = 3001;
        Assert.That(() => _ = _mappings.ArabicPointForIndex(index), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestMundanePointForIndexHappyFlow()
    {
        int index = 3002;
        MundanePoints expectedPoint = MundanePoints.EastPoint;
        MundanePoints resultingPoint = _mappings.MundanePointForIndex(index);
        Assert.That(resultingPoint, Is.EqualTo(expectedPoint));
    }

    [Test]
    public void TestMundanePointForIndexOutOfRange()
    {
        int index = 1002;
        Assert.That(() => _ = _mappings.MundanePointForIndex(index), Throws.TypeOf<ArgumentException>());
    }






}