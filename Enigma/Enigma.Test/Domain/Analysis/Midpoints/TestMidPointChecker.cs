// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis.Midpoints;
using Enigma.Domain.Analysis;
using Enigma.Frontend.Charts;
using Enigma.Frontend;
using Microsoft.Extensions.DependencyInjection;
using Enigma.Core.Analysis.Dto;
using Enigma.Domain.CalcVars;
using Enigma.Core.Analysis.Services;
using Enigma.Core.Analysis.Aspects;
using Moq;
using Castle.Components.DictionaryAdapter.Xml;

namespace Enigma.Test.Domain.Midpoints;

[TestFixture]
public class TextMidpointChecker
{
    private IMidpointChecker _midpointChecker;
    private readonly double _delta = 0.00000001;

    [SetUp]
    public void SetUp() {
        var mockOrbConstructor = new Mock<IMidpointOrbConstructor>();
        var mockMidpointSpecs = new Mock<IMidpointSpecifications>();
        _midpointChecker = new MidpointChecker(mockOrbConstructor.Object, mockMidpointSpecs.Object);
    }
   

    [Test]
    public void TestFindMidpointsHappyFlow()
    {
        double pos1 = 100.0;
        double pos2 = 200.0;
        double expected = 150.0;

        List<AnalysisPoint> points = new();
        points.Add(new AnalysisPoint(PointGroups.SolarSystemPoints, 0, pos1));
        points.Add(new AnalysisPoint(PointGroups.SolarSystemPoints, 1, pos2));
        List<EffectiveMidpoint> effectiveMidpoints = _midpointChecker.FindMidpoints(points);
        Assert.That(effectiveMidpoints, Is.Not.Null);
        Assert.That(effectiveMidpoints.Count, Is.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(effectiveMidpoints[0].Position, Is.EqualTo(expected).Within(_delta));
            Assert.That(effectiveMidpoints[0].Point1.ItemId, Is.EqualTo(0));
            Assert.That(effectiveMidpoints[0].Point2.ItemId, Is.EqualTo(1));
        });
    }

    [Test]
    public void TestFindMidpointsLargestFirst()
    {
        double pos1 = 100.0;
        double pos2 = 10.0;
        double expected = 55.0;

        List<AnalysisPoint> points = new();
        points.Add(new AnalysisPoint(PointGroups.SolarSystemPoints, 3, pos1));
        points.Add(new AnalysisPoint(PointGroups.SolarSystemPoints, 9, pos2));
        List<EffectiveMidpoint> effectiveMidpoints = _midpointChecker.FindMidpoints(points);
        Assert.That(effectiveMidpoints, Is.Not.Null);
        Assert.That(effectiveMidpoints.Count, Is.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(effectiveMidpoints[0].Position, Is.EqualTo(expected).Within(_delta));
            Assert.That(effectiveMidpoints[0].Point1.ItemId, Is.EqualTo(3));
            Assert.That(effectiveMidpoints[0].Point2.ItemId, Is.EqualTo(9));
        });
    }

    [Test]
    public void TestFindMidpointsLargeArc()
    {
        double pos1 = 10.0;
        double pos2 = 340.0;
        double expected = 355.0;

        List<AnalysisPoint> points = new();
        points.Add(new AnalysisPoint(PointGroups.SolarSystemPoints, 2, pos1));
        points.Add(new AnalysisPoint(PointGroups.SolarSystemPoints, 4, pos2));
        List<EffectiveMidpoint> effectiveMidpoints = _midpointChecker.FindMidpoints(points);

        Assert.That(effectiveMidpoints[0].Position, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestFindMidpointsArcWithZero()
    {
        double pos1 = 300.0;
        double pos2 = 0.0;
        double expected = 330.0;

        List<AnalysisPoint> points = new();
        points.Add(new AnalysisPoint(PointGroups.SolarSystemPoints, 2, pos1));
        points.Add(new AnalysisPoint(PointGroups.SolarSystemPoints, 4, pos2));
        List<EffectiveMidpoint> effectiveMidpoints = _midpointChecker.FindMidpoints(points);

        Assert.That(effectiveMidpoints[0].Position, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestFindMidpointsAlmostHalfCircle()
    {
        double pos1 = 10.0;
        double pos2 = 189.0;
        double expected = 99.5;

        List<AnalysisPoint> points = new();
        points.Add(new AnalysisPoint(PointGroups.SolarSystemPoints, 2, pos1));
        points.Add(new AnalysisPoint(PointGroups.SolarSystemPoints, 4, pos2));
        List<EffectiveMidpoint> effectiveMidpoints = _midpointChecker.FindMidpoints(points);

        Assert.That(effectiveMidpoints[0].Position, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestFindMidpointsLargerThanHalfCircle()
    {
        double pos1 = 10.0;
        double pos2 = 191.0;
        double expected = 280.5;

        List<AnalysisPoint> points = new();
        points.Add(new AnalysisPoint(PointGroups.SolarSystemPoints, 2, pos1));
        points.Add(new AnalysisPoint(PointGroups.SolarSystemPoints, 4, pos2));
        List<EffectiveMidpoint> effectiveMidpoints = _midpointChecker.FindMidpoints(points);

        Assert.That(effectiveMidpoints[0].Position, Is.EqualTo(expected).Within(_delta));
    }




}