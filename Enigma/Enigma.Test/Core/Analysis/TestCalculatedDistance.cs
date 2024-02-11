// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Analysis;

[TestFixture]
public class TestCalculatedDistance
{
    private const double Delta = 0.00000001;
    private readonly ICalculatedDistance _calculatedDistance = new CalculatedDistance();
    
    [Test]
    public void TestShortedDistanceForPointsHappyFLow()
    {
        const double pos1 = 10.0;
        const double pos2 = 150.0;
        const double expected = 140.0;
        double result = _calculatedDistance.ShortestDistance(pos1, pos2);
        Assert.That(result, Is.EqualTo(expected).Within(Delta));
    }
    
    [Test]
    public void TestShortedDistanceForPointsZeroInBetween()
    {
        const double pos1 = 10.0;
        const double pos2 = 350.0;
        const double expected = 20.0;
        double result = _calculatedDistance.ShortestDistance(pos1, pos2);
        Assert.That(result, Is.EqualTo(expected).Within(Delta));
    }
    
    [Test]
    public void TestShortedDistanceForPointsInputNegative()
    {
        const double pos1 = -10.0;
        const double pos2 = 350.0;
        _ = Assert.Throws<ArgumentException>(() => _calculatedDistance.ShortestDistance(pos1, pos2));
    }
    
    [Test]
    public void TestShortedDistanceForPointsInputTooLarge()
    {
        const double pos1 = 10.0;
        const double pos2 = 450.0;
        _ = Assert.Throws<ArgumentException>(() => _calculatedDistance.ShortestDistance(pos1, pos2));
    }
    
      [Test]
    public void TestFindShortestDistances()
    {
        List<PositionedPoint> posPoints = CreatePositionedPoints();
        List<DistanceBetween2Points> allDistances = _calculatedDistance.ShortestDistances(posPoints);
        Assert.Multiple(() =>
        {
            Assert.That(allDistances, Has.Count.EqualTo(3));
            Assert.That(allDistances[0].Point1.Point, Is.EqualTo(ChartPoints.Sun));
            Assert.That(allDistances[0].Point2.Point, Is.EqualTo(ChartPoints.Moon));
            Assert.That(allDistances[2].Point2.Point, Is.EqualTo(ChartPoints.Jupiter));
            Assert.That(allDistances[0].Distance, Is.EqualTo(20.0).Within(Delta));         // Sun Moon
            Assert.That(allDistances[1].Distance, Is.EqualTo(160.5).Within(Delta));        // Sun Jupiter
            Assert.That(allDistances[2].Distance, Is.EqualTo(179.5).Within(Delta));        // Moon Jupiter
        });
    }

    [Test]
    public void TestFindShortestDistancesBetweenPointsAndCusps()
    {
        List<PositionedPoint> posPoints = CreatePositionedPoints();
        List<PositionedPoint> cusps = CreateCusps();
        List<DistanceBetween2Points> allDistances = _calculatedDistance.ShortestDistanceBetweenPointsAndCusps(posPoints, cusps);

        Assert.Multiple(() =>
        {
            Assert.That(allDistances, Has.Count.EqualTo(9));
            Assert.That(allDistances[0].Point1.Point, Is.EqualTo(ChartPoints.Sun));
            Assert.That(allDistances[1].Point2.Point, Is.EqualTo(ChartPoints.Cusp2));
            Assert.That(allDistances[0].Distance, Is.EqualTo(10.0).Within(Delta));         // Sun Cusp1
            Assert.That(allDistances[4].Distance, Is.EqualTo(60.0).Within(Delta));        // Moon Cusp2
            Assert.That(allDistances[8].Distance, Is.EqualTo(89.5).Within(Delta));        // Jupiter Cusp3
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