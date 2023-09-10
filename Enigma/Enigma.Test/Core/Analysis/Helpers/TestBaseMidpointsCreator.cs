// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Analysis.Helpers;
using Enigma.Core.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Points;

namespace Enigma.Test.Core.Analysis.Helpers;

[TestFixture]
public class TestBaseMidpointsCreator
{
    private readonly IBaseMidpointsCreator _baseMidpointsCreator = new BaseMidpointsCreator();
    private const double Delta = 0.00000001;

    [Test]
    public void TestCreateBaseMidpointsHappyFlow()
    {
        List<PositionedPoint> positionedPoints = CreatePositionedPoints();
        List<BaseMidpoint> baseMidpoints = _baseMidpointsCreator.CreateBaseMidpoints(positionedPoints);
        Assert.Multiple(() =>
        {
            Assert.That(baseMidpoints[0].Position, Is.EqualTo(100.0).Within(Delta));
            Assert.That(baseMidpoints[1].Position, Is.EqualTo(342.0).Within(Delta));
            Assert.That(baseMidpoints[2].Position, Is.EqualTo(32.0).Within(Delta));
            Assert.That(baseMidpoints[3].Position, Is.EqualTo(240.0).Within(Delta));
            Assert.That(baseMidpoints[4].Position, Is.EqualTo(110.0).Within(Delta));
            Assert.That(baseMidpoints[5].Position, Is.EqualTo(352.0).Within(Delta));
        });
    }

    [Test]
    public void TestCreateBaseMidpointsEmptyList()
    {
        List<PositionedPoint> positionedPoints = new();
        List<BaseMidpoint> baseMidpoints = _baseMidpointsCreator.CreateBaseMidpoints(positionedPoints);
        Assert.That(baseMidpoints, Is.Empty);
    }

    [Test]
    public void TestConvertBaseMidpointsToDial360Degrees()
    {
        List<BaseMidpoint> baseMidpoints = CreateBaseMidpoints();
        List<BaseMidpoint> midpointsInDial = _baseMidpointsCreator.ConvertBaseMidpointsToDial(baseMidpoints, 360.0);
        Assert.Multiple(() =>
        {
            Assert.That(midpointsInDial[0].Position, Is.EqualTo(100.0).Within(Delta));
            Assert.That(midpointsInDial[1].Position, Is.EqualTo(342.0).Within(Delta));
            Assert.That(midpointsInDial[2].Position, Is.EqualTo(32.0).Within(Delta));
            Assert.That(midpointsInDial[3].Position, Is.EqualTo(240.0).Within(Delta));
            Assert.That(midpointsInDial[4].Position, Is.EqualTo(110.0).Within(Delta));
            Assert.That(midpointsInDial[5].Position, Is.EqualTo(352.0).Within(Delta));
        });
    }

    [Test]
    public void TestConvertBaseMidpointsToDial90Degrees()
    {
        List<BaseMidpoint> baseMidpoints = CreateBaseMidpoints();
        List<BaseMidpoint> midpointsInDial = _baseMidpointsCreator.ConvertBaseMidpointsToDial(baseMidpoints, 90.0);
        Assert.Multiple(() =>
        {
            Assert.That(midpointsInDial[0].Position, Is.EqualTo(10.0).Within(Delta));
            Assert.That(midpointsInDial[1].Position, Is.EqualTo(72.0).Within(Delta));
            Assert.That(midpointsInDial[2].Position, Is.EqualTo(32.0).Within(Delta));
            Assert.That(midpointsInDial[3].Position, Is.EqualTo(60.0).Within(Delta));
            Assert.That(midpointsInDial[4].Position, Is.EqualTo(20.0).Within(Delta));
            Assert.That(midpointsInDial[5].Position, Is.EqualTo(82.0).Within(Delta));
        });
    }

    [Test]
    public void TestConvertBaseMidpointsToDial45Degrees()
    {
        List<BaseMidpoint> baseMidpoints = CreateBaseMidpoints();
        List<BaseMidpoint> midpointsInDial = _baseMidpointsCreator.ConvertBaseMidpointsToDial(baseMidpoints, 45.0);
        Assert.Multiple(() =>
        {
            Assert.That(midpointsInDial[0].Position, Is.EqualTo(10.0).Within(Delta));
            Assert.That(midpointsInDial[1].Position, Is.EqualTo(27.0).Within(Delta));
            Assert.That(midpointsInDial[2].Position, Is.EqualTo(32.0).Within(Delta));
            Assert.That(midpointsInDial[3].Position, Is.EqualTo(15.0).Within(Delta));
            Assert.That(midpointsInDial[4].Position, Is.EqualTo(20.0).Within(Delta));
            Assert.That(midpointsInDial[5].Position, Is.EqualTo(37.0).Within(Delta));
        });
    }


    private static List<BaseMidpoint> CreateBaseMidpoints()
    {
        List<BaseMidpoint> baseMidpoints = new();
        List<PositionedPoint> analysisPoints = CreatePositionedPoints();
        baseMidpoints.Add(new BaseMidpoint(analysisPoints[0], analysisPoints[1], 100.0));     // sun and moon
        baseMidpoints.Add(new BaseMidpoint(analysisPoints[0], analysisPoints[2], 342.0));     // sun and mars
        baseMidpoints.Add(new BaseMidpoint(analysisPoints[0], analysisPoints[1], 32.0));     // sun and jupiter
        baseMidpoints.Add(new BaseMidpoint(analysisPoints[1], analysisPoints[2], 240.0));     // moon and mars
        baseMidpoints.Add(new BaseMidpoint(analysisPoints[1], analysisPoints[3], 110.0));     // moon and jupiter
        baseMidpoints.Add(new BaseMidpoint(analysisPoints[2], analysisPoints[3], 352.0));     // mars and jupiter
        return baseMidpoints;
    }


    private static List<PositionedPoint> CreatePositionedPoints()
    {
        PositionedPoint sun = new(ChartPoints.Sun, 22.0);
        PositionedPoint moon = new(ChartPoints.Moon, 178.0);
        PositionedPoint mars = new(ChartPoints.Mars, 302.0);
        PositionedPoint jupiter = new(ChartPoints.Jupiter, 42.0);
        List<PositionedPoint> positionedPoints = new() { sun, moon, mars, jupiter };
        return positionedPoints;
    }
}

