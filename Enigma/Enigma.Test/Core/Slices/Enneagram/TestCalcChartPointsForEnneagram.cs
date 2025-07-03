// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.Enneagram;
using Enigma.Domain.References;
using FakeItEasy;

namespace Enigma.Test.Core.Slices.Enneagram;

[TestFixture]
public class TestCalcChartPointsForEnneagram
{
    private CalcChartPointsForEnneagram _calcChartPoints = null!;
    private const double VALID_JD = 2459580.5; // 2022-01-01

    [SetUp]
    public void SetUp()
    {
        _calcChartPoints = new CalcChartPointsForEnneagram();
    }

    [Test]
    public void CalcChartPoints_ValidParameters_ReturnsExpectedResult()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury };

        // Act
        var result = _calcChartPoints.CalcChartPoints(points, VALID_JD);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3));
            
            // Check that all requested points are present
            var resultPoints = result.Select(kvp => kvp.Key).ToList();
            Assert.That(resultPoints, Is.EquivalentTo(points));
            
            // Check that all positions are valid longitude values (0-360)
            foreach (var position in result)
            {
                Assert.That(position.Value, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(position.Value, Is.LessThan(360.0));
            }
        });
    }

    [Test]
    public void CalcChartPoints_SinglePoint_ReturnsExpectedResult()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Venus };

        // Act
        var result = _calcChartPoints.CalcChartPoints(points, VALID_JD);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Venus));
            Assert.That(result[0].Value, Is.GreaterThanOrEqualTo(0.0));
            Assert.That(result[0].Value, Is.LessThan(360.0));
        });
    }

    [Test]
    public void CalcChartPoints_MultiplePoints_ReturnsExpectedResult()
    {
        // Arrange
        var points = new List<ChartPoints> 
        { 
            ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury, ChartPoints.Venus,
            ChartPoints.Mars, ChartPoints.Jupiter, ChartPoints.Saturn, ChartPoints.Uranus,
            ChartPoints.Neptune, ChartPoints.Pluto
        };

        // Act
        var result = _calcChartPoints.CalcChartPoints(points, VALID_JD);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(10));
            
            // Check that all requested points are present
            var resultPoints = result.Select(kvp => kvp.Key).ToList();
            Assert.That(resultPoints, Is.EquivalentTo(points));
            
            // Check that all positions are valid longitude values
            foreach (var position in result)
            {
                Assert.That(position.Value, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(position.Value, Is.LessThan(360.0));
            }
        });
    }

    [Test]
    public void CalcChartPoints_DifferentDates_ReturnsDifferentPositions()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Sun };
        var dates = new[] 
        { 
            2459580.5, // 2022-01-01
            2459581.5, // 2022-01-02
            2459582.5  // 2022-01-03
        };

        // Act & Assert
        var previousPosition = -1.0;
        foreach (var date in dates)
        {
            var result = _calcChartPoints.CalcChartPoints(points, date);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Sun));
                Assert.That(result[0].Value, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(result[0].Value, Is.LessThan(360.0));
            });

            // Sun should move between consecutive days
            if (previousPosition >= 0)
            {
                var difference = Math.Abs(result[0].Value - previousPosition);
                // Account for crossing 0/360 boundary
                if (difference > 180) difference = 360 - difference;
                Assert.That(difference, Is.GreaterThan(0.5), "Sun should move between consecutive days");
            }
            previousPosition = result[0].Value;
        }
    }


    [Test]
    public void CalcChartPoints_NullPoints_ThrowsArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => 
            _calcChartPoints.CalcChartPoints(null!, VALID_JD));
        Assert.That(exception.ParamName, Is.EqualTo("points"));
    }

    [Test]
    public void CalcChartPoints_EmptyPointsList_ThrowsArgumentException()
    {
        // Arrange
        var points = new List<ChartPoints>();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _calcChartPoints.CalcChartPoints(points, VALID_JD));
        Assert.That(exception.ParamName, Is.EqualTo("points"));
        Assert.That(exception.Message, Does.Contain("cannot be empty"));
    }

    [Test]
    public void CalcChartPoints_InvalidJulianDay_ThrowsArgumentException()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Sun };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _calcChartPoints.CalcChartPoints(points, double.NaN));
        Assert.That(exception.ParamName, Is.EqualTo("julianDay"));
        Assert.That(exception.Message, Does.Contain("must be a finite number"));
    }

    [Test]
    public void CalcChartPoints_InfinityJulianDay_ThrowsArgumentException()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Sun };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _calcChartPoints.CalcChartPoints(points, double.PositiveInfinity));
        Assert.That(exception.ParamName, Is.EqualTo("julianDay"));
        Assert.That(exception.Message, Does.Contain("must be a finite number"));
    }

    [Test]
    public void CalcChartPoints_ServiceReusability()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Sun };
        var date = VALID_JD;

        // Act - Use the same service instance multiple times
        var result1 = _calcChartPoints.CalcChartPoints(points, date);
        var result2 = _calcChartPoints.CalcChartPoints(points, date);
        var result3 = _calcChartPoints.CalcChartPoints(points, date);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result1, Is.Not.Null);
            Assert.That(result2, Is.Not.Null);
            Assert.That(result3, Is.Not.Null);
            Assert.That(result1, Has.Count.EqualTo(1));
            Assert.That(result2, Has.Count.EqualTo(1));
            Assert.That(result3, Has.Count.EqualTo(1));
            
            // All results should be for the same point and date, so positions should be identical
            Assert.That(result1[0].Key, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result2[0].Key, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result3[0].Key, Is.EqualTo(ChartPoints.Sun));
            
            Assert.That(result1[0].Value, Is.EqualTo(result2[0].Value));
            Assert.That(result2[0].Value, Is.EqualTo(result3[0].Value));
        });
    }

    [Test]
    public void CalcChartPoints_RealisticAstronomicalValues()
    {
        // Arrange - Test with a known astronomical event
        var points = new List<ChartPoints> { ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury };
        var date = 2459580.5; // 2022-01-01
        
        // Act
        var result = _calcChartPoints.CalcChartPoints(points, date);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3));
            
            // Sun should be near Capricorn (around 280-290 degrees) in early January
            var sunPosition = result.First(kvp => kvp.Key == ChartPoints.Sun).Value;
            Assert.That(sunPosition, Is.GreaterThan(270.0));
            Assert.That(sunPosition, Is.LessThan(300.0));
            
            // All positions should be valid longitude values
            foreach (var position in result)
            {
                Assert.That(position.Value, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(position.Value, Is.LessThan(360.0));
            }
        });
    }
} 