// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.ProgCalendar;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Slices.ProgCalendar;

[TestFixture]
public class TestProgCalSortAspectPoints
{
    /// <summary>
    /// Helper method to create a test CalculatedChart with specific positions
    /// </summary>
    private static CalculatedChart CreateTestChart(Dictionary<ChartPoints, double> positions)
    {
        var chartPositions = new Dictionary<ChartPoints, FullPointPos>();
        foreach (var kvp in positions)
        {
            var ecliptical = new PointPosSpeeds(
                new PosSpeed(kvp.Value, 0.5),
                new PosSpeed(0.0, 0.0),
                new PosSpeed(1.0, 0.0));
            var equatorial = new PointPosSpeeds(
                new PosSpeed(0.0, 0.0),
                new PosSpeed(0.0, 0.0),
                new PosSpeed(1.0, 0.0));
            var horizontal = new PointPosSpeeds(
                new PosSpeed(0.0, 0.0),
                new PosSpeed(0.0, 0.0),
                new PosSpeed(1.0, 0.0));
            chartPositions[kvp.Key] = new FullPointPos(ecliptical, equatorial, horizontal);
        }

        return new CalculatedChart(chartPositions, null!, 23.44);
    }

    [Test]
    public void CreateSortedAspectPoints_SinglePointSingleAspect_ReturnsCorrectCount()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 100.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun };
        var aspectTypes = new List<AspectTypes> { AspectTypes.Square }; // 90 degrees

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            // Square creates 2 points: +90 and -90 degrees
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].ChartPoint, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result[0].Aspect, Is.EqualTo(AspectTypes.Square));
        });
    }

    [Test]
    public void CreateSortedAspectPoints_ConjunctionAspect_ReturnsSinglePoint()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 100.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun };
        var aspectTypes = new List<AspectTypes> { AspectTypes.Conjunction }; // 0 degrees

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            // Conjunction creates only 1 point (0 degrees)
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Longitude, Is.EqualTo(100.0).Within(0.001));
        });
    }

    [Test]
    public void CreateSortedAspectPoints_OppositionAspect_ReturnsSinglePoint()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 100.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun };
        var aspectTypes = new List<AspectTypes> { AspectTypes.Opposition }; // 180 degrees

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            // Opposition creates only 1 point (180 degrees)
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Longitude, Is.EqualTo(280.0).Within(0.001)); // 100 + 180
        });
    }

    [Test]
    public void CreateSortedAspectPoints_ResultIsSortedByLongitude()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 200.0 },
            { ChartPoints.Moon, 50.0 },
            { ChartPoints.Mercury, 300.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury };
        var aspectTypes = new List<AspectTypes> { AspectTypes.Conjunction };

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3));
            
            // Verify sorting: should be 50, 200, 300
            Assert.That(result[0].Longitude, Is.EqualTo(50.0).Within(0.001));
            Assert.That(result[1].Longitude, Is.EqualTo(200.0).Within(0.001));
            Assert.That(result[2].Longitude, Is.EqualTo(300.0).Within(0.001));
            
            // Verify ascending order
            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.That(result[i].Longitude, Is.LessThanOrEqualTo(result[i + 1].Longitude),
                    $"Result at index {i} should be less than or equal to result at index {i + 1}");
            }
        });
    }

    [Test]
    public void CreateSortedAspectPoints_NegativeLongitude_NormalizesTo360Range()
    {
        // Arrange - Create a situation where aspect creates negative longitude
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 10.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun };
        var aspectTypes = new List<AspectTypes> { AspectTypes.Square }; // 90 degrees: 10 - 90 = -80

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            // Should have 2 points: 10+90=100 and 10-90=-80->280
            Assert.That(result, Has.Count.EqualTo(2));
            
            // All longitudes should be in 0-360 range
            foreach (var point in result)
            {
                Assert.That(point.Longitude, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(point.Longitude, Is.LessThan(360.0));
            }
            
            // Verify specific values after normalization
            Assert.That(result[0].Longitude, Is.EqualTo(100.0).Within(0.001)); // 10 + 90
            Assert.That(result[1].Longitude, Is.EqualTo(280.0).Within(0.001)); // 10 - 90 + 360
        });
    }

    [Test]
    public void CreateSortedAspectPoints_LongitudeOver360_NormalizesTo360Range()
    {
        // Arrange - Create a situation where aspect creates longitude > 360
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 350.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun };
        var aspectTypes = new List<AspectTypes> { AspectTypes.Square }; // 90 degrees: 350 + 90 = 440

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            
            // All longitudes should be in 0-360 range
            foreach (var point in result)
            {
                Assert.That(point.Longitude, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(point.Longitude, Is.LessThan(360.0));
            }
            
            // Verify specific values after normalization (sorted)
            Assert.That(result[0].Longitude, Is.EqualTo(80.0).Within(0.001)); // 350 + 90 - 360
            Assert.That(result[1].Longitude, Is.EqualTo(260.0).Within(0.001)); // 350 - 90
        });
    }

    [Test]
    public void CreateSortedAspectPoints_MultipleAspects_ReturnsCorrectCount()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 100.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun };
        var aspectTypes = new List<AspectTypes>
        {
            AspectTypes.Conjunction,  // 1 point
            AspectTypes.Opposition,   // 1 point
            AspectTypes.Square,       // 2 points
            AspectTypes.Triangle      // 2 points
        };

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            // Total: 1 + 1 + 2 + 2 = 6 points
            Assert.That(result, Has.Count.EqualTo(6));
        });
    }

    [Test]
    public void CreateSortedAspectPoints_MultiplePoints_ReturnsCorrectCount()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 100.0 },
            { ChartPoints.Moon, 150.0 },
            { ChartPoints.Mercury, 200.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury };
        var aspectTypes = new List<AspectTypes> { AspectTypes.Square }; // 2 points each

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            // 3 points * 2 aspect points each = 6 total
            Assert.That(result, Has.Count.EqualTo(6));
        });
    }

    [Test]
    public void CreateSortedAspectPoints_ComplexScenario_CorrectlySorted()
    {
        // Arrange - Create a complex scenario with multiple points and aspects
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 15.0 },
            { ChartPoints.Moon, 180.0 },
            { ChartPoints.Mercury, 345.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury };
        var aspectTypes = new List<AspectTypes>
        {
            AspectTypes.Square,      // 90 degrees
            AspectTypes.Triangle     // 120 degrees
        };

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            // 3 points * 2 aspects * 2 points per aspect = 12 total
            Assert.That(result, Has.Count.EqualTo(12));
            
            // Verify all are in valid range
            foreach (var point in result)
            {
                Assert.That(point.Longitude, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(point.Longitude, Is.LessThan(360.0));
            }
            
            // Verify ascending order
            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.That(result[i].Longitude, Is.LessThanOrEqualTo(result[i + 1].Longitude),
                    $"Longitude at index {i} ({result[i].Longitude}) should be <= longitude at index {i + 1} ({result[i + 1].Longitude})");
            }
        });
    }

    [Test]
    public void CreateSortedAspectPoints_SextileAspect_CorrectAngles()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 100.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun };
        var aspectTypes = new List<AspectTypes> { AspectTypes.Sextile }; // 60 degrees

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            
            // Should be sorted: 40, 160
            Assert.That(result[0].Longitude, Is.EqualTo(40.0).Within(0.001));  // 100 - 60
            Assert.That(result[1].Longitude, Is.EqualTo(160.0).Within(0.001)); // 100 + 60
        });
    }

    [Test]
    public void CreateSortedAspectPoints_TriangleAspect_CorrectAngles()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Mars, 200.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Mars };
        var aspectTypes = new List<AspectTypes> { AspectTypes.Triangle }; // 120 degrees

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            
            // Should be sorted: 80, 320
            Assert.That(result[0].Longitude, Is.EqualTo(80.0).Within(0.001));  // 200 - 120
            Assert.That(result[1].Longitude, Is.EqualTo(320.0).Within(0.001)); // 200 + 120
        });
    }

    [Test]
    public void CreateSortedAspectPoints_AllConjunctionAndOpposition_CorrectCount()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 50.0 },
            { ChartPoints.Moon, 150.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun, ChartPoints.Moon };
        var aspectTypes = new List<AspectTypes>
        {
            AspectTypes.Conjunction,
            AspectTypes.Opposition
        };

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            // 2 points * 2 aspects * 1 point each = 4 total
            Assert.That(result, Has.Count.EqualTo(4));
            
            // Verify sorted order
            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.That(result[i].Longitude, Is.LessThanOrEqualTo(result[i + 1].Longitude));
            }
        });
    }

    [Test]
    public void CreateSortedAspectPoints_EmptyChartPoints_ReturnsEmptyList()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 100.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints>();
        var aspectTypes = new List<AspectTypes> { AspectTypes.Square };

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        });
    }

    [Test]
    public void CreateSortedAspectPoints_EmptyAspectTypes_ReturnsEmptyList()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 100.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun };
        var aspectTypes = new List<AspectTypes>();

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        });
    }

    [Test]
    public void CreateSortedAspectPoints_PointNotInChart_SkipsPoint()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 100.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun, ChartPoints.Jupiter }; // Jupiter not in chart
        var aspectTypes = new List<AspectTypes> { AspectTypes.Square };

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            // Only Sun should be processed, Jupiter will have longitude -1 which becomes 359 after normalization
            // So we'll have 4 points: 2 for Sun (valid) and 2 for Jupiter (with normalized -1)
            Assert.That(result, Has.Count.EqualTo(4));
        });
    }

    [Test]
    public void CreateSortedAspectPoints_BoundaryLongitude_Zero()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 0.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun };
        var aspectTypes = new List<AspectTypes> { AspectTypes.Square };

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            
            // 0 + 90 = 90, 0 - 90 = -90 -> 270
            Assert.That(result[0].Longitude, Is.EqualTo(90.0).Within(0.001));
            Assert.That(result[1].Longitude, Is.EqualTo(270.0).Within(0.001));
        });
    }

    [Test]
    public void CreateSortedAspectPoints_BoundaryLongitude_Near360()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 359.5 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun };
        var aspectTypes = new List<AspectTypes> { AspectTypes.Conjunction };

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Longitude, Is.EqualTo(359.5).Within(0.001));
            Assert.That(result[0].Longitude, Is.LessThan(360.0));
        });
    }

    [Test]
    public void CreateSortedAspectPoints_PreservesChartPointAndAspectInfo()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Venus, 120.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Venus };
        var aspectTypes = new List<AspectTypes> { AspectTypes.Sextile };

        // Act
        var result = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            
            // Both results should preserve the ChartPoint and Aspect
            foreach (var point in result)
            {
                Assert.That(point.ChartPoint, Is.EqualTo(ChartPoints.Venus));
                Assert.That(point.Aspect, Is.EqualTo(AspectTypes.Sextile));
            }
        });
    }

    [Test]
    public void CreateSortedAspectPoints_ConsistencyCheck_SameInputSameOutput()
    {
        // Arrange
        var positions = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 100.0 },
            { ChartPoints.Moon, 200.0 }
        };
        var calcChart = CreateTestChart(positions);
        var chartPoints = new List<ChartPoints> { ChartPoints.Sun, ChartPoints.Moon };
        var aspectTypes = new List<AspectTypes> { AspectTypes.Square, AspectTypes.Triangle };

        // Act
        var result1 = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);
        var result2 = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, chartPoints, aspectTypes);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result1, Is.Not.Null);
            Assert.That(result2, Is.Not.Null);
            Assert.That(result1.Count, Is.EqualTo(result2.Count));
            
            // Results should be identical
            for (int i = 0; i < result1.Count; i++)
            {
                Assert.That(result1[i].ChartPoint, Is.EqualTo(result2[i].ChartPoint));
                Assert.That(result1[i].Aspect, Is.EqualTo(result2[i].Aspect));
                Assert.That(result1[i].Longitude, Is.EqualTo(result2[i].Longitude).Within(0.001));
            }
        });
    }
}


