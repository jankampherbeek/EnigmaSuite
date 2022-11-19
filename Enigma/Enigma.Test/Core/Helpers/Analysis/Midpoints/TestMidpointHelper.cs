// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis.Midpoints;
using Enigma.Core.Helpers.Analysis.Midpoints;
using Enigma.Core.Helpers.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.AstronCalculations;

namespace Enigma.Test.Core.Helpers.Analysis.Midpoints;

[TestFixture]
public class TestMidpointHelper
{
    private IMidpointsHelper _midpointsHelper = new MidpointsHelper();
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestConstructEffectiveMidpointHappyFlow()
    {
        AnalysisPoint point1 = new(PointGroups.ZodiacalPoints, 1, 22.0, "a");
        AnalysisPoint point2 = new(PointGroups.ZodiacalPoints, 2, 44.0, "b");
        double expectedMidpoint = 33.0;
        EffectiveMidpoint actualMidpoint = _midpointsHelper.ConstructEffectiveMidpointInDial(point1, point2, 1.0);    
        Assert.That(actualMidpoint.Position, Is.EqualTo(expectedMidpoint).Within(_delta));
    }

    [Test]
    public void TestConstructEffectiveMidpointLargeDifference()
    {
        AnalysisPoint point1 = new(PointGroups.ZodiacalPoints, 1, 2.0, "a");
        AnalysisPoint point2 = new(PointGroups.ZodiacalPoints, 2, 352.0, "b");
        double expectedMidpoint = 357.0;
        EffectiveMidpoint actualMidpoint = _midpointsHelper.ConstructEffectiveMidpointInDial(point1, point2, 1.0);
        Assert.That(actualMidpoint.Position, Is.EqualTo(expectedMidpoint).Within(_delta));
    }

    [Test]
    public void TestMeasureMidpointDeviationDial360HappyFlow()
    {
        double division = 1.0;
        double posMidpoint = 10.0;
        double posCelPoint = 11.0;
        double expectedDeviation = 1.0;
        double actualDeviation = _midpointsHelper.MeasureMidpointDeviation(division, posMidpoint, posCelPoint);
        Assert.That(actualDeviation, Is.EqualTo(expectedDeviation).Within(_delta));
    }

    [Test]
    public void TestMeasureMidpointDeviationDial360LargeDifference()
    {
        double division = 1.0;
        double posMidpoint = 10.0;
        double posCelPoint = 348.0;
        double expectedDeviation = 22.0;
        double actualDeviation = _midpointsHelper.MeasureMidpointDeviation(division, posMidpoint, posCelPoint);
        Assert.That(actualDeviation, Is.EqualTo(expectedDeviation).Within(_delta));
    }
/*
    [Test]
    public void TestMeasureMidpointDeviationDial90HappyFlow()
    {
        double division = 0.25;
        double posMidpoint = 10.0;
        double posCelPoint = 101.0;
        double expectedDeviation = 1.0;
        double actualDeviation = _midpointsHelper.MeasureMidpointDeviation(division, posMidpoint, posCelPoint);
        Assert.That(actualDeviation, Is.EqualTo(expectedDeviation).Within(_delta));
    }

    [Test]
    public void TestMeasureMidpointDeviationDial90LargeDifference()
    {
        double division = 0.25;
        double posMidpoint = 10.0;
        double posCelPoint = 281.0;
        double expectedDeviation = 1.0;
        double actualDeviation = _midpointsHelper.MeasureMidpointDeviation(division, posMidpoint, posCelPoint);
        Assert.That(actualDeviation, Is.EqualTo(expectedDeviation).Within(_delta));
    }

    [Test]
    public void TestMeasureMidpointDeviationDial45HappyFlow()
    {
        double division = 0.125;
        double posMidpoint = 10.0;
        double posCelPoint = 56.0;
        double expectedDeviation = 1.0;
        double actualDeviation = _midpointsHelper.MeasureMidpointDeviation(division, posMidpoint, posCelPoint);
        Assert.That(actualDeviation, Is.EqualTo(expectedDeviation).Within(_delta));
    }

    [Test]
    public void TestMeasureMidpointDeviationDial45LargeDifference()
    {
        double division = 0.125;
        double posMidpoint = 10.0;
        double posCelPoint = 326.0;
        double expectedDeviation = 1.0;
        double actualDeviation = _midpointsHelper.MeasureMidpointDeviation(division, posMidpoint, posCelPoint);
        Assert.That(actualDeviation, Is.EqualTo(expectedDeviation).Within(_delta));
    }
*/
    [Test]
    public void TestCreateMidpoints4Dial360Degrees()
    {
        List<EffectiveMidpoint> midpoints360 = CreateMidpoints();
        List<EffectiveMidpoint> midpointsConverted = _midpointsHelper.CreateMidpoints4Dial(1.0, midpoints360);
        Assert.Multiple(() =>
        {
            Assert.That(midpointsConverted.Count, Is.EqualTo(6));
            Assert.That(midpointsConverted[0].Point1.ItemId, Is.EqualTo(0));
            Assert.That(midpointsConverted[0].Point2.ItemId, Is.EqualTo(1));
            Assert.That(midpointsConverted[0].Position, Is.EqualTo(100.0).Within(_delta));
            Assert.That(midpointsConverted[1].Position, Is.EqualTo(342.0).Within(_delta));
            Assert.That(midpointsConverted[2].Position, Is.EqualTo(32.0).Within(_delta));
            Assert.That(midpointsConverted[3].Position, Is.EqualTo(240.0).Within(_delta));
            Assert.That(midpointsConverted[4].Position, Is.EqualTo(110.0).Within(_delta));
            Assert.That(midpointsConverted[5].Position, Is.EqualTo(352.0).Within(_delta));
        });
    }

    [Test]
    public void TestCreateMidpoints4Dial90Degrees()
    {
        List<EffectiveMidpoint> midpoints360 = CreateMidpoints();
        List<EffectiveMidpoint> midpointsConverted = _midpointsHelper.CreateMidpoints4Dial(0.25, midpoints360);
        Assert.Multiple(() =>
        {
            Assert.That(midpointsConverted.Count, Is.EqualTo(6));
            Assert.That(midpointsConverted[0].Point1.ItemId, Is.EqualTo(0));
            Assert.That(midpointsConverted[0].Point2.ItemId, Is.EqualTo(1));
            Assert.That(midpointsConverted[0].Position, Is.EqualTo(10.0).Within(_delta));
            Assert.That(midpointsConverted[1].Position, Is.EqualTo(72.0).Within(_delta));
            Assert.That(midpointsConverted[2].Position, Is.EqualTo(32.0).Within(_delta));
            Assert.That(midpointsConverted[3].Position, Is.EqualTo(60.0).Within(_delta));
            Assert.That(midpointsConverted[4].Position, Is.EqualTo(20.0).Within(_delta));
            Assert.That(midpointsConverted[5].Position, Is.EqualTo(82.0).Within(_delta));
        });
    }

    [Test]
    public void TestCreateMidpoints4Dial45Degrees()
    {
        List<EffectiveMidpoint> midpoints360 = CreateMidpoints();
        List<EffectiveMidpoint> midpointsConverted = _midpointsHelper.CreateMidpoints4Dial(0.125, midpoints360);
        Assert.Multiple(() =>
        {
            Assert.That(midpointsConverted.Count, Is.EqualTo(6));
            Assert.That(midpointsConverted[0].Point1.ItemId, Is.EqualTo(0));
            Assert.That(midpointsConverted[0].Point2.ItemId, Is.EqualTo(1));
            Assert.That(midpointsConverted[0].Position, Is.EqualTo(10.0).Within(_delta));
            Assert.That(midpointsConverted[1].Position, Is.EqualTo(27.0).Within(_delta));
            Assert.That(midpointsConverted[2].Position, Is.EqualTo(32.0).Within(_delta));
            Assert.That(midpointsConverted[3].Position, Is.EqualTo(15.0).Within(_delta));
            Assert.That(midpointsConverted[4].Position, Is.EqualTo(20.0).Within(_delta));
            Assert.That(midpointsConverted[5].Position, Is.EqualTo(37.0).Within(_delta));
        });
    }



    private static List<EffectiveMidpoint> CreateMidpoints()
    {
        List<EffectiveMidpoint> midpoints = new();
        PointGroups pointGroup = PointGroups.ZodiacalPoints;
        AnalysisPoint sun = new(pointGroup, 0, 22.0, "a");
        AnalysisPoint moon = new(pointGroup, 1, 178.0, "b");
        AnalysisPoint mars = new(pointGroup, 5, 302.0, "c");
        AnalysisPoint jupiter = new(pointGroup, 6, 42.0, "d");

        midpoints.Add(new EffectiveMidpoint(sun, moon, 100.0, 1.0));
        midpoints.Add(new EffectiveMidpoint(sun, mars, 342.0, 1.0));
        midpoints.Add(new EffectiveMidpoint(sun, jupiter, 32.0, 1.0));
        midpoints.Add(new EffectiveMidpoint(moon, mars, 240.0, 1.0));
        midpoints.Add(new EffectiveMidpoint(moon, jupiter, 110.0, 1.0));
        midpoints.Add(new EffectiveMidpoint(mars, jupiter, 352.0, 1.0));
        return midpoints;
    }

}