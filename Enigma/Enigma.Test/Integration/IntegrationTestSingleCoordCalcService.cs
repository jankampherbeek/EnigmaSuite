// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Slices;
using Enigma.Core.Slices.SingleCoordinateCalc;
using Enigma.Domain.References;

namespace Enigma.Test.Integration;

[TestFixture]
public class IntegrationTestSingleCoordCalcService
{
    private SingleCoordCalcService _service = null!;
    private const double DELTA = 0.00000001;

    [SetUp]
    public void SetUp()
    {
        _service = new SingleCoordCalcService();
    }

    [Test]
    public void TestCalcPositions_Longitude_HappyFlow()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Sun, ChartPoints.Moon };
        var request = new SingleCoordCalcRequest(points, 2459580.5, Coordinates.Longitude); // 2022-01-01

        // Act
        var result = _service.CalcPositions(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result[1].Key, Is.EqualTo(ChartPoints.Moon));
            Assert.That(result[0].Value, Is.GreaterThanOrEqualTo(0.0));
            Assert.That(result[0].Value, Is.LessThan(360.0));
            Assert.That(result[1].Value, Is.GreaterThanOrEqualTo(0.0));
            Assert.That(result[1].Value, Is.LessThan(360.0));
        });
    }

    [Test]
    public void TestCalcPositions_Latitude_HappyFlow()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Mercury, ChartPoints.Venus };
        var request = new SingleCoordCalcRequest(points, 2459580.5, Coordinates.Latitude);

        // Act
        var result = _service.CalcPositions(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Mercury));
            Assert.That(result[1].Key, Is.EqualTo(ChartPoints.Venus));
            Assert.That(result[0].Value, Is.GreaterThanOrEqualTo(-90.0));
            Assert.That(result[0].Value, Is.LessThanOrEqualTo(90.0));
            Assert.That(result[1].Value, Is.GreaterThanOrEqualTo(-90.0));
            Assert.That(result[1].Value, Is.LessThanOrEqualTo(90.0));
        });
    }

    [Test]
    public void TestCalcPositions_RightAscension_HappyFlow()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Jupiter, ChartPoints.Saturn };
        var request = new SingleCoordCalcRequest(points, 2459580.5, Coordinates.RightAscension);

        // Act
        var result = _service.CalcPositions(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Jupiter));
            Assert.That(result[1].Key, Is.EqualTo(ChartPoints.Saturn));
            Assert.That(result[0].Value, Is.GreaterThanOrEqualTo(0.0));
            Assert.That(result[0].Value, Is.LessThan(360.0));
            Assert.That(result[1].Value, Is.GreaterThanOrEqualTo(0.0));
            Assert.That(result[1].Value, Is.LessThan(360.0));
        });
    }

    [Test]
    public void TestCalcPositions_Declination_HappyFlow()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Uranus, ChartPoints.Neptune };
        var request = new SingleCoordCalcRequest(points, 2459580.5, Coordinates.Declination);

        // Act
        var result = _service.CalcPositions(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Uranus));
            Assert.That(result[1].Key, Is.EqualTo(ChartPoints.Neptune));
            Assert.That(result[0].Value, Is.GreaterThanOrEqualTo(-90.0));
            Assert.That(result[0].Value, Is.LessThanOrEqualTo(90.0));
            Assert.That(result[1].Value, Is.GreaterThanOrEqualTo(-90.0));
            Assert.That(result[1].Value, Is.LessThanOrEqualTo(90.0));
        });
    }

    [Test]
    public void TestCalcPositions_Azimuth_HappyFlow()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Pluto, ChartPoints.TrueNode };
        var request = new SingleCoordCalcRequest(points, 2459580.5, Coordinates.Azimuth);

        // Act
        var result = _service.CalcPositions(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Pluto));
            Assert.That(result[1].Key, Is.EqualTo(ChartPoints.TrueNode));
            Assert.That(result[0].Value, Is.GreaterThanOrEqualTo(0.0));
            Assert.That(result[0].Value, Is.LessThan(360.0));
            Assert.That(result[1].Value, Is.GreaterThanOrEqualTo(0.0));
            Assert.That(result[1].Value, Is.LessThan(360.0));
        });
    }

    [Test]
    public void TestCalcPositions_Altitude_HappyFlow()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Mars };
        var request = new SingleCoordCalcRequest(points, 2459580.5, Coordinates.Altitude);

        // Act
        var result = _service.CalcPositions(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Mars));
            Assert.That(result[0].Value, Is.GreaterThanOrEqualTo(-90.0));
            Assert.That(result[0].Value, Is.LessThanOrEqualTo(90.0));
        });
    }

    [Test]
    public void TestCalcPositions_MultiplePoints()
    {
        // Arrange
        var points = new List<ChartPoints> 
        { 
            ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury, ChartPoints.Venus, 
            ChartPoints.Mars, ChartPoints.Jupiter, ChartPoints.Saturn 
        };
        var request = new SingleCoordCalcRequest(points, 2459580.5, Coordinates.Longitude);

        // Act
        var result = _service.CalcPositions(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(7));
            
            // Check that all points are present and have valid longitude values
            foreach (var position in result)
            {
                Assert.That(position.Value, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(position.Value, Is.LessThan(360.0));
            }
            
            // Check that all requested points are present
            var resultPoints = result.Select(kvp => kvp.Key).ToList();
            Assert.That(resultPoints, Is.EquivalentTo(points));
        });
    }

    [Test]
    public void TestCalcPositions_DifferentDates()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Sun };
        var dates = new[] 
        { 
            2459580.5, // 2022-01-01
            2459581.5, // 2022-01-02
            2459582.5, // 2022-01-03
            2459583.5  // 2022-01-04
        };

        // Act & Assert
        var previousPosition = -1.0;
        foreach (var date in dates)
        {
            var request = new SingleCoordCalcRequest(points, date, Coordinates.Longitude);
            var result = _service.CalcPositions(request);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Sun));
                Assert.That(result[0].Value, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(result[0].Value, Is.LessThan(360.0));
            });

            // Sun should move approximately 1 degree per day
            if (previousPosition >= 0)
            {
                var difference = Math.Abs(result[0].Value - previousPosition);
                // Account for crossing 0/360 boundary
                if (difference > 180) difference = 360 - difference;
                Assert.That(difference, Is.GreaterThan(0.5), "Sun should move between consecutive days");
                Assert.That(difference, Is.LessThan(2.0), "Sun should not move more than ~2 degrees per day");
            }
            previousPosition = result[0].Value;
        }
    }

    [Test]
    public void TestCalcPositions_AllCoordinateTypes()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Sun };
        var coordinates = Enum.GetValues<Coordinates>();

        // Act & Assert
        foreach (var coordinate in coordinates)
        {
            var request = new SingleCoordCalcRequest(points, 2459580.5, coordinate);
            var result = _service.CalcPositions(request);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Sun));
                
                // Validate coordinate-specific ranges
                switch (coordinate)
                {
                    case Coordinates.Longitude:
                    case Coordinates.RightAscension:
                    case Coordinates.Azimuth:
                        Assert.That(result[0].Value, Is.GreaterThanOrEqualTo(0.0));
                        Assert.That(result[0].Value, Is.LessThan(360.0));
                        break;
                    case Coordinates.Latitude:
                    case Coordinates.Declination:
                    case Coordinates.Altitude:
                        Assert.That(result[0].Value, Is.GreaterThanOrEqualTo(-90.0));
                        Assert.That(result[0].Value, Is.LessThanOrEqualTo(90.0));
                        break;
                }
            });
        }
    }

    [Test]
    public void TestCalcPositions_HistoricalDates()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Jupiter };
        var historicalDates = new[]
        {
            2440587.5, // 1969-07-20 (Moon landing)
            2415020.5, // 1900-01-01
            2299160.5, // 1582-10-15 (Gregorian calendar start)
            1721425.5  // 0001-01-01 (Year 1)
        };

        // Act & Assert
        foreach (var date in historicalDates)
        {
            var request = new SingleCoordCalcRequest(points, date, Coordinates.Longitude);
            var result = _service.CalcPositions(request);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Jupiter));
                Assert.That(result[0].Value, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(result[0].Value, Is.LessThan(360.0));
            });
        }
    }

    [Test]
    public void TestCalcPositions_FutureDates()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Saturn };
        var futureDates = new[]
        {
            2469807.5, // 2050-01-01
            2488073.5, // 2100-01-01
            2525008.5  // 2200-01-01
        };

        // Act & Assert
        foreach (var date in futureDates)
        {
            var request = new SingleCoordCalcRequest(points, date, Coordinates.Longitude);
            var result = _service.CalcPositions(request);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Saturn));
                Assert.That(result[0].Value, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(result[0].Value, Is.LessThan(360.0));
            });
        }
    }

    [Test]
    public void TestCalcPositions_PlanetaryPositions()
    {
        // Arrange
        var planets = new List<ChartPoints> 
        { 
            ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury, ChartPoints.Venus,
            ChartPoints.Mars, ChartPoints.Jupiter, ChartPoints.Saturn, ChartPoints.Uranus,
            ChartPoints.Neptune, ChartPoints.Pluto
        };
        var request = new SingleCoordCalcRequest(planets, 2459580.5, Coordinates.Longitude);

        // Act
        var result = _service.CalcPositions(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(10));
            
            // Check that all planets have valid positions
            foreach (var position in result)
            {
                Assert.That(position.Value, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(position.Value, Is.LessThan(360.0));
            }
            
            // Check that all requested planets are present
            var resultPlanets = result.Select(kvp => kvp.Key).ToList();
            Assert.That(resultPlanets, Is.EquivalentTo(planets));
        });
    }

   
    [Test]
    public void TestCalcPositions_CoordinateConsistency()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Sun };
        var date = 2459580.5;
        
        // Test that longitude and latitude are consistent (same coordinate system)
        var longitudeRequest = new SingleCoordCalcRequest(points, date, Coordinates.Longitude);
        var latitudeRequest = new SingleCoordCalcRequest(points, date, Coordinates.Latitude);

        // Act
        var longitudeResult = _service.CalcPositions(longitudeRequest);
        var latitudeResult = _service.CalcPositions(latitudeRequest);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(longitudeResult, Is.Not.Null);
            Assert.That(latitudeResult, Is.Not.Null);
            Assert.That(longitudeResult, Has.Count.EqualTo(1));
            Assert.That(latitudeResult, Has.Count.EqualTo(1));
            Assert.That(longitudeResult[0].Key, Is.EqualTo(ChartPoints.Sun));
            Assert.That(latitudeResult[0].Key, Is.EqualTo(ChartPoints.Sun));
            
            // Longitude should be in ecliptical coordinates (0-360)
            Assert.That(longitudeResult[0].Value, Is.GreaterThanOrEqualTo(0.0));
            Assert.That(longitudeResult[0].Value, Is.LessThan(360.0));
            
            // Latitude should be in ecliptical coordinates (-90 to 90)
            Assert.That(latitudeResult[0].Value, Is.GreaterThanOrEqualTo(-90.0));
            Assert.That(latitudeResult[0].Value, Is.LessThanOrEqualTo(90.0));
        });
    }

    [Test]
    public void TestCalcPositions_EquatorialConsistency()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Jupiter };
        var date = 2459580.5;
        
        // Test that right ascension and declination are consistent (same coordinate system)
        var raRequest = new SingleCoordCalcRequest(points, date, Coordinates.RightAscension);
        var decRequest = new SingleCoordCalcRequest(points, date, Coordinates.Declination);

        // Act
        var raResult = _service.CalcPositions(raRequest);
        var decResult = _service.CalcPositions(decRequest);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(raResult, Is.Not.Null);
            Assert.That(decResult, Is.Not.Null);
            Assert.That(raResult, Has.Count.EqualTo(1));
            Assert.That(decResult, Has.Count.EqualTo(1));
            Assert.That(raResult[0].Key, Is.EqualTo(ChartPoints.Jupiter));
            Assert.That(decResult[0].Key, Is.EqualTo(ChartPoints.Jupiter));
            
            // Right ascension should be in equatorial coordinates (0-360)
            Assert.That(raResult[0].Value, Is.GreaterThanOrEqualTo(0.0));
            Assert.That(raResult[0].Value, Is.LessThan(360.0));
            
            // Declination should be in equatorial coordinates (-90 to 90)
            Assert.That(decResult[0].Value, Is.GreaterThanOrEqualTo(-90.0));
            Assert.That(decResult[0].Value, Is.LessThanOrEqualTo(90.0));
        });
    }

    [Test]
    public void TestCalcPositions_HorizontalConsistency()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Mars };
        var date = 2459580.5;
        
        // Test that azimuth and altitude are consistent (same coordinate system)
        var azimuthRequest = new SingleCoordCalcRequest(points, date, Coordinates.Azimuth);
        var altitudeRequest = new SingleCoordCalcRequest(points, date, Coordinates.Altitude);

        // Act
        var azimuthResult = _service.CalcPositions(azimuthRequest);
        var altitudeResult = _service.CalcPositions(altitudeRequest);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(azimuthResult, Is.Not.Null);
            Assert.That(altitudeResult, Is.Not.Null);
            Assert.That(azimuthResult, Has.Count.EqualTo(1));
            Assert.That(altitudeResult, Has.Count.EqualTo(1));
            Assert.That(azimuthResult[0].Key, Is.EqualTo(ChartPoints.Mars));
            Assert.That(altitudeResult[0].Key, Is.EqualTo(ChartPoints.Mars));
            
            // Azimuth should be in horizontal coordinates (0-360)
            Assert.That(azimuthResult[0].Value, Is.GreaterThanOrEqualTo(0.0));
            Assert.That(azimuthResult[0].Value, Is.LessThan(360.0));
            
            // Altitude should be in horizontal coordinates (-90 to 90)
            Assert.That(altitudeResult[0].Value, Is.GreaterThanOrEqualTo(-90.0));
            Assert.That(altitudeResult[0].Value, Is.LessThanOrEqualTo(90.0));
        });
    }

    [Test]
    public void TestCalcPositions_NullRequest_ThrowsArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => 
            _service.CalcPositions(null!));
        Assert.That(exception!.ParamName, Is.EqualTo("request"));
    }

    [Test]
    public void TestCalcPositions_EmptyPointsList_ThrowsArgumentException()
    {
        // Arrange
        var points = new List<ChartPoints>();
        var request = new SingleCoordCalcRequest(points, 2459580.5, Coordinates.Longitude);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _service.CalcPositions(request));
        Assert.That(exception!.Message, Is.EqualTo("Points list cannot be empty (Parameter 'points')"));
        Assert.That(exception.ParamName, Is.EqualTo("points"));
    }

    [Test]
    public void TestCalcPositions_SinglePoint()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Venus };
        var request = new SingleCoordCalcRequest(points, 2459580.5, Coordinates.Longitude);

        // Act
        var result = _service.CalcPositions(request);

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
    public void TestCalcPositions_ServiceReusability()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Sun };
        var date = 2459580.5;

        // Act - Use the same service instance multiple times
        var request1 = new SingleCoordCalcRequest(points, date, Coordinates.Longitude);
        var result1 = _service.CalcPositions(request1);

        var request2 = new SingleCoordCalcRequest(points, date, Coordinates.Latitude);
        var result2 = _service.CalcPositions(request2);

        var request3 = new SingleCoordCalcRequest(points, date, Coordinates.RightAscension);
        var result3 = _service.CalcPositions(request3);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result1, Is.Not.Null);
            Assert.That(result2, Is.Not.Null);
            Assert.That(result3, Is.Not.Null);
            Assert.That(result1, Has.Count.EqualTo(1));
            Assert.That(result2, Has.Count.EqualTo(1));
            Assert.That(result3, Has.Count.EqualTo(1));
            
            // All results should be for the same point
            Assert.That(result1[0].Key, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result2[0].Key, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result3[0].Key, Is.EqualTo(ChartPoints.Sun));
        });
    }

    [Test]
    public void TestCalcPositions_RealisticAstronomicalValues()
    {
        // Arrange - Test with a known astronomical event
        var points = new List<ChartPoints> { ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury };
        var date = 2459580.5; // 2022-01-01
        
        // Act
        var longitudeRequest = new SingleCoordCalcRequest(points, date, Coordinates.Longitude);
        var latitudeRequest = new SingleCoordCalcRequest(points, date, Coordinates.Latitude);
        
        var longitudeResult = _service.CalcPositions(longitudeRequest);
        var latitudeResult = _service.CalcPositions(latitudeRequest);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(longitudeResult, Is.Not.Null);
            Assert.That(latitudeResult, Is.Not.Null);
            Assert.That(longitudeResult, Has.Count.EqualTo(3));
            Assert.That(latitudeResult, Has.Count.EqualTo(3));
            
            // Sun should be near Capricorn (around 280-290 degrees) in early January
            var sunLongitude = longitudeResult.First(kvp => kvp.Key == ChartPoints.Sun).Value;
            Assert.That(sunLongitude, Is.GreaterThan(270.0));
            Assert.That(sunLongitude, Is.LessThan(300.0));
            
            // Sun's latitude should be very close to 0 (ecliptic)
            var sunLatitude = latitudeResult.First(kvp => kvp.Key == ChartPoints.Sun).Value;
            Assert.That(sunLatitude, Is.EqualTo(0.0).Within(1.0));
            
            // Moon's latitude should be within reasonable bounds
            var moonLatitude = latitudeResult.First(kvp => kvp.Key == ChartPoints.Moon).Value;
            Assert.That(moonLatitude, Is.GreaterThanOrEqualTo(-6.0));
            Assert.That(moonLatitude, Is.LessThanOrEqualTo(6.0));
        });
    }
} 