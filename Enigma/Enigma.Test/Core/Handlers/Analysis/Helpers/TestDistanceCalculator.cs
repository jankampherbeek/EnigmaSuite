﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Analysis.Helpers;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Points;

namespace Enigma.Test.Core.Handlers.Analysis.Helpers;

[TestFixture]
public class TestDistanceCalculator
{
    private readonly double _delta = 0.00000001;
    private readonly IDistanceCalculator _distanceCalculator = new DistanceCalculator();

    [Test]
    public void TestFindShortestDistances()
    {
        List<PositionedPoint> posPoints = CreatePositionedPoints();
        List<DistanceBetween2Points> allDistances = _distanceCalculator.FindShortestDistances(posPoints);
        Assert.Multiple(() =>
        {
            Assert.That(allDistances, Has.Count.EqualTo(3));
            Assert.That(allDistances[0].Point1.Point, Is.EqualTo(ChartPoints.Sun));
            Assert.That(allDistances[0].Point2.Point, Is.EqualTo(ChartPoints.Moon));
            Assert.That(allDistances[2].Point2.Point, Is.EqualTo(ChartPoints.Jupiter));
            Assert.That(allDistances[0].Distance, Is.EqualTo(20.0).Within(_delta));         // Sun Moon
            Assert.That(allDistances[1].Distance, Is.EqualTo(160.5).Within(_delta));        // Sun Jupiter
            Assert.That(allDistances[2].Distance, Is.EqualTo(179.5).Within(_delta));        // Moon Jupiter
        });
    }

    [Test]
    public void TestFindShortestDistancesBetweenPointsAndCusps()
    {
        List<PositionedPoint> posPoints = CreatePositionedPoints();
        List<PositionedPoint> cusps = CreateCusps();
        List<DistanceBetween2Points> allDistances = _distanceCalculator.FindShortestDistanceBetweenPointsAndCusps(posPoints, cusps);

        Assert.Multiple(() =>
        {
            Assert.That(allDistances, Has.Count.EqualTo(9));
            Assert.That(allDistances[0].Point1.Point, Is.EqualTo(ChartPoints.Sun));
            Assert.That(allDistances[1].Point2.Point, Is.EqualTo(ChartPoints.Cusp2));
            Assert.That(allDistances[0].Distance, Is.EqualTo(10.0).Within(_delta));         // Sun Cusp1
            Assert.That(allDistances[4].Distance, Is.EqualTo(60.0).Within(_delta));        // Moon Cusp2
            Assert.That(allDistances[8].Distance, Is.EqualTo(89.5).Within(_delta));        // Jupiter Cusp3
        });
    }



    private static List<PositionedPoint> CreatePositionedPoints()
    {
        List<PositionedPoint> allPoints = new()
        {
            new PositionedPoint(ChartPoints.Sun, 10.0),
            new PositionedPoint(ChartPoints.Moon, 350.0),
            new PositionedPoint(ChartPoints.Jupiter, 170.5)
        };
        return allPoints;
    }

    private static List<PositionedPoint> CreateCusps()
    {
        List<PositionedPoint> allPoints = new()
        {
            new PositionedPoint(ChartPoints.Cusp1, 20.0),
            new PositionedPoint(ChartPoints.Cusp2, 50.0),
            new PositionedPoint(ChartPoints.Cusp3, 81.0)
        };
        return allPoints;
    }
}