// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Analysis.Interfaces;
using Enigma.Core.Work.Analysis.Midpoints;
using Enigma.Domain.Analysis;
using Enigma.Domain.AstronCalculations;

namespace Enigma.Test.Core.Work.Analysis.Midpoints;

[TestFixture]
public class TestBaseMidpointsCreator
{
    private readonly IBaseMidpointsCreator _baseMidpointsCreator = new BaseMidpointsCreator();
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestCreateBaseMidpointsHappyFlow()
    {
        List<AnalysisPoint> analysisPoints = CreateAnalysisPoints();
        List<BaseMidpoint> baseMidpoints = _baseMidpointsCreator.CreateBaseMidpoints(analysisPoints);
        Assert.Multiple(() =>
        {
            Assert.That(baseMidpoints[0].Position, Is.EqualTo(100.0).Within(_delta));
            Assert.That(baseMidpoints[1].Position, Is.EqualTo(342.0).Within(_delta));
            Assert.That(baseMidpoints[2].Position, Is.EqualTo(32.0).Within(_delta));
            Assert.That(baseMidpoints[3].Position, Is.EqualTo(240.0).Within(_delta));
            Assert.That(baseMidpoints[4].Position, Is.EqualTo(110.0).Within(_delta));
            Assert.That(baseMidpoints[5].Position, Is.EqualTo(352.0).Within(_delta));
        });
    }

    [Test]
    public void TestCreateBaseMidpointsEmptyList()
    {
        List<AnalysisPoint> analysisPoints = new();
        List<BaseMidpoint> baseMidpoints = _baseMidpointsCreator.CreateBaseMidpoints(analysisPoints);
        Assert.That(baseMidpoints, Is.Empty);
    }

    [Test]
    public void TestConvertBaseMidpointsToDial360Degrees()
    {
        List<BaseMidpoint> baseMidpoints = CreateBaseMidpoints();
        List<BaseMidpoint> midpointsInDial = _baseMidpointsCreator.ConvertBaseMidpointsToDial(baseMidpoints, 360.0);
        Assert.Multiple(() =>
        {
            Assert.That(midpointsInDial[0].Position, Is.EqualTo(100.0).Within(_delta));
            Assert.That(midpointsInDial[1].Position, Is.EqualTo(342.0).Within(_delta));
            Assert.That(midpointsInDial[2].Position, Is.EqualTo(32.0).Within(_delta));
            Assert.That(midpointsInDial[3].Position, Is.EqualTo(240.0).Within(_delta));
            Assert.That(midpointsInDial[4].Position, Is.EqualTo(110.0).Within(_delta));
            Assert.That(midpointsInDial[5].Position, Is.EqualTo(352.0).Within(_delta));
        });
    }

    [Test]
    public void TestConvertBaseMidpointsToDial90Degrees()
    {
        List<BaseMidpoint> baseMidpoints = CreateBaseMidpoints();
        List<BaseMidpoint> midpointsInDial = _baseMidpointsCreator.ConvertBaseMidpointsToDial(baseMidpoints, 90.0);
        Assert.Multiple(() =>
        {
            Assert.That(midpointsInDial[0].Position, Is.EqualTo(10.0).Within(_delta));
            Assert.That(midpointsInDial[1].Position, Is.EqualTo(72.0).Within(_delta));
            Assert.That(midpointsInDial[2].Position, Is.EqualTo(32.0).Within(_delta));
            Assert.That(midpointsInDial[3].Position, Is.EqualTo(60.0).Within(_delta));
            Assert.That(midpointsInDial[4].Position, Is.EqualTo(20.0).Within(_delta));
            Assert.That(midpointsInDial[5].Position, Is.EqualTo(82.0).Within(_delta));
        });
    }

    [Test]
    public void TestConvertBaseMidpointsToDial45Degrees()
    {
        List<BaseMidpoint> baseMidpoints = CreateBaseMidpoints();
        List<BaseMidpoint> midpointsInDial = _baseMidpointsCreator.ConvertBaseMidpointsToDial(baseMidpoints, 45.0);
        Assert.Multiple(() =>
        {
            Assert.That(midpointsInDial[0].Position, Is.EqualTo(10.0).Within(_delta));
            Assert.That(midpointsInDial[1].Position, Is.EqualTo(27.0).Within(_delta));
            Assert.That(midpointsInDial[2].Position, Is.EqualTo(32.0).Within(_delta));
            Assert.That(midpointsInDial[3].Position, Is.EqualTo(15.0).Within(_delta));
            Assert.That(midpointsInDial[4].Position, Is.EqualTo(20.0).Within(_delta));
            Assert.That(midpointsInDial[5].Position, Is.EqualTo(37.0).Within(_delta));
        });
    }


    private static List<BaseMidpoint> CreateBaseMidpoints()
    {
        List<BaseMidpoint> baseMidpoints = new();
        List<AnalysisPoint> analysisPoints = CreateAnalysisPoints();
        baseMidpoints.Add(new BaseMidpoint(analysisPoints[0], analysisPoints[1], 100.0));     // sun and moon
        baseMidpoints.Add(new BaseMidpoint(analysisPoints[0], analysisPoints[2], 342.0));     // sun and mars
        baseMidpoints.Add(new BaseMidpoint(analysisPoints[0], analysisPoints[1], 32.0));     // sun and jupiter
        baseMidpoints.Add(new BaseMidpoint(analysisPoints[1], analysisPoints[2], 240.0));     // moon and mars
        baseMidpoints.Add(new BaseMidpoint(analysisPoints[1], analysisPoints[3], 110.0));     // moon and jupiter
        baseMidpoints.Add(new BaseMidpoint(analysisPoints[2], analysisPoints[3], 352.0));     // mars and jupiter
        return baseMidpoints;
    }


    private static List<AnalysisPoint> CreateAnalysisPoints()
    {
        PointGroups pointGroup = PointGroups.ZodiacalPoints;
        AnalysisPoint sun = new(pointGroup, 0, 22.0, "a");
        AnalysisPoint moon = new(pointGroup, 1, 178.0, "b");
        AnalysisPoint mars = new(pointGroup, 5, 302.0, "c");
        AnalysisPoint jupiter = new(pointGroup, 6, 42.0, "d");
        List<AnalysisPoint> analysisPoints = new() { sun, moon, mars, jupiter };
        return analysisPoints;
    }
}

