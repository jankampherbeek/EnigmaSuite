// Enigma Astrology Research.
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
    public void TestShortestDistances()
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

}