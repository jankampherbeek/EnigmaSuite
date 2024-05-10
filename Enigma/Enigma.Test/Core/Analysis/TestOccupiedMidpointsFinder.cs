// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Test.Core.Analysis;

[TestFixture]
public class TestOccupiedMidpointsFinder
{
    private const double DELTA = 0.0000001;
    private readonly IOccupiedMidpointsFinder _midpointsFinder = 
        App.ServiceProvider.GetRequiredService<IOccupiedMidpointsFinder>();
    
    [Test]
    public void TestCalculateOccupiedMidpointsInDeclinationForPositionedPointsHappyFlow()
    {
        List<PositionedPoint> posPoints = CreatePositionedPointsHappyFlow();
        const double orb = 1.0;
        List<OccupiedMidpoint>  result = _midpointsFinder.CalculateOccupiedMidpointsInDeclination(posPoints, orb);
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].OccupyingPoint.Point, Is.EqualTo(ChartPoints.Moon));
            Assert.That(result[0].Midpoint.Point1.Point, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result[0].Midpoint.Point2.Position, Is.EqualTo(21.0).Within(DELTA));
            Assert.That(result[0].Exactness, Is.EqualTo(75));
        });
    }
    
    [Test]
    public void TestCalculateOccupiedMidpointsInDeclinationForPositionedPointsNorthAndSouth()
    {
        List<PositionedPoint> posPoints = CreatePositionedPointsNorthAndSouth();
        const double orb = 1.0;
        List<OccupiedMidpoint>  result = _midpointsFinder.CalculateOccupiedMidpointsInDeclination(posPoints, orb);
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].OccupyingPoint.Point, Is.EqualTo(ChartPoints.Mercury));
            Assert.That(result[0].Midpoint.Point1.Point, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result[0].Midpoint.Point1.Position, Is.EqualTo(-10.0).Within(DELTA));
            Assert.That(result[0].Exactness, Is.EqualTo(80).Within(DELTA));
        });
    }


    private List<PositionedPoint> CreatePositionedPointsHappyFlow()
    {
        List<PositionedPoint> posPoints = new()
        {
            new PositionedPoint(ChartPoints.Sun, 10.5),
            new PositionedPoint(ChartPoints.Moon, 15.5),
            new PositionedPoint(ChartPoints.Mercury, 21.0)
        };
        return posPoints;
    }
    
    private List<PositionedPoint> CreatePositionedPointsNorthAndSouth()
    {
        List<PositionedPoint> posPoints = new()
        {
            new PositionedPoint(ChartPoints.Sun, -10.0),
            new PositionedPoint(ChartPoints.Moon, 20.0),
            new PositionedPoint(ChartPoints.Mercury, 5.2)
        };
        return posPoints;
    }
    
    
}