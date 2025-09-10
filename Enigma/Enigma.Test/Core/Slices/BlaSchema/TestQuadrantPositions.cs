// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestQuadrantPositions
{
    [Test]
    public void TestDefineQuadrants_HappyFlow()
    {
        var chart = CreateTestChartWithNormalQuadrants();
        var result = QuadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[1], Is.EqualTo(2), "Quadrant 1 (ASC to IC) should have 2 planets");
            Assert.That(result[2], Is.EqualTo(1), "Quadrant 2 (IC to DESC) should have 1 planet");
            Assert.That(result[3], Is.EqualTo(1), "Quadrant 3 (DESC to MC) should have 1 planet");
            Assert.That(result[4], Is.EqualTo(1), "Quadrant 4 (MC to ASC) should have 1 planet");
        });
    }

    [Test]
    public void TestDefineQuadrants_NullChart()
    {
        Assert.That(() => QuadrantPositions.DefineQuadrants(null!), 
            Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestDefineQuadrants_EmptyChart()
    {
        var chart = CreateEmptyChart();
        var result = QuadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[1], Is.EqualTo(0));
            Assert.That(result[2], Is.EqualTo(0));
            Assert.That(result[3], Is.EqualTo(0));
            Assert.That(result[4], Is.EqualTo(0));
        });
    }

    [Test]
    public void TestDefineQuadrants_NoAngles()
    {
        var chart = CreateChartWithNoAngles();
        var result = QuadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[1], Is.EqualTo(0));
            Assert.That(result[2], Is.EqualTo(0));
            Assert.That(result[3], Is.EqualTo(0));
            Assert.That(result[4], Is.EqualTo(0));
        });
    }

    [Test]
    public void TestDefineQuadrants_MissingAscendant()
    {
        var chart = CreateChartWithMissingAscendant();
        var result = QuadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[1], Is.EqualTo(0));
            Assert.That(result[2], Is.EqualTo(0));
            Assert.That(result[3], Is.EqualTo(0));
            Assert.That(result[4], Is.EqualTo(0));
        });
    }

    [Test]
    public void TestDefineQuadrants_MissingMC()
    {
        var chart = CreateChartWithMissingMC();
        var result = QuadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[1], Is.EqualTo(0));
            Assert.That(result[2], Is.EqualTo(0));
            Assert.That(result[3], Is.EqualTo(0));
            Assert.That(result[4], Is.EqualTo(0));
        });
    }

    [Test]
    public void TestDefineQuadrants_QuadrantBoundaryConditions()
    {
        var chart = CreateChartWithBoundaryConditions();
        var result = QuadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[1], Is.EqualTo(1), "Planet exactly at ASC should be in quadrant 1");
            Assert.That(result[2], Is.EqualTo(1), "Planet exactly at IC should be in quadrant 2");
            Assert.That(result[3], Is.EqualTo(1), "Planet exactly at DESC should be in quadrant 3");
            Assert.That(result[4], Is.EqualTo(1), "Planet exactly at MC should be in quadrant 4");
        });
    }

    [Test]
    public void TestDefineQuadrants_LongitudeOverflow()
    {
        var chart = CreateChartWithLongitudeOverflow();
        var result = QuadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[1], Is.EqualTo(1), "Planet at 359° should be in quadrant 1");
            Assert.That(result[2], Is.EqualTo(0));
            Assert.That(result[3], Is.EqualTo(0));
            Assert.That(result[4], Is.EqualTo(1), "Planet at 1° should be in quadrant 4");
        });
    }

    [Test]
    public void TestDefineQuadrants_AllQuadrants()
    {
        var chart = CreateChartWithAllQuadrants();
        var result = QuadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[1], Is.EqualTo(2), "Quadrant 1 should have 2 planets");
            Assert.That(result[2], Is.EqualTo(2), "Quadrant 2 should have 2 planets");
            Assert.That(result[3], Is.EqualTo(2), "Quadrant 3 should have 2 planets");
            Assert.That(result[4], Is.EqualTo(2), "Quadrant 4 should have 2 planets");
        });
    }

    [Test]
    public void TestDefineQuadrants_OnlyAngles()
    {
        var chart = CreateChartWithOnlyAngles();
        var result = QuadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[1], Is.EqualTo(0));
            Assert.That(result[2], Is.EqualTo(0));
            Assert.That(result[3], Is.EqualTo(0));
            Assert.That(result[4], Is.EqualTo(0));
        });
    }

    [Test]
    public void TestDefineQuadrants_CounterclockwiseOrdering()
    {
        // Test that quadrants are correctly ordered counterclockwise:
        // Quadrant 1: ASC to IC (counterclockwise)
        // Quadrant 2: IC to DESC (counterclockwise)  
        // Quadrant 3: DESC to MC (counterclockwise)
        // Quadrant 4: MC to ASC (counterclockwise)
        var chart = CreateChartForCounterclockwiseTest();
        var result = QuadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            // Sun at 15° (between ASC 0° and IC 90°) should be in quadrant 1
            Assert.That(result[1], Is.EqualTo(1), "Sun at 15° should be in quadrant 1");
            // Moon at 105° (between IC 90° and DESC 180°) should be in quadrant 2
            Assert.That(result[2], Is.EqualTo(1), "Moon at 105° should be in quadrant 2");
            // Mercury at 195° (between DESC 180° and MC 270°) should be in quadrant 3
            Assert.That(result[3], Is.EqualTo(1), "Mercury at 195° should be in quadrant 3");
            // Venus at 285° (between MC 270° and ASC 0°/360°) should be in quadrant 4
            Assert.That(result[4], Is.EqualTo(1), "Venus at 285° should be in quadrant 4");
        });
    }

    [Test]
    public void TestDefineQuadrants_EdgeCaseAtZero()
    {
        var chart = CreateChartWithZeroLongitude();
        var result = QuadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[1], Is.EqualTo(1), "Planet at 0° should be in quadrant 1");
            Assert.That(result[2], Is.EqualTo(0));
            Assert.That(result[3], Is.EqualTo(0));
            Assert.That(result[4], Is.EqualTo(0));
        });
    }

    [Test]
    public void TestDefineQuadrants_LargeDataset()
    {
        var chart = CreateChartWithLargeDataset();
        var result = QuadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            var totalCount = result.Values.Sum();
            Assert.That(totalCount, Is.EqualTo(20), "Total count should be 20");
            
            // Verify all counts are non-negative
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value, Is.GreaterThanOrEqualTo(0), $"Quadrant {kvp.Key} should have non-negative count");
            }
        });
    }

    #region Helper Methods

    private static ChartLongitudes CreateTestChartWithNormalQuadrants()
    {
        var points = new Dictionary<ChartPoints, double>
        {
            // Angles
            { ChartPoints.Ascendant, 0.0 },    // ASC at 0°
            { ChartPoints.Mc, 90.0 },          // MC at 90°
            
            // Planets in different quadrants
            { ChartPoints.Sun, 15.0 },         // Quadrant 1 (ASC to IC)
            { ChartPoints.Moon, 45.0 },        // Quadrant 1 (ASC to IC)
            { ChartPoints.Mercury, 135.0 },    // Quadrant 2 (IC to DESC)
            { ChartPoints.Venus, 225.0 },      // Quadrant 3 (DESC to MC)
            { ChartPoints.Mars, 315.0 }        // Quadrant 4 (MC to ASC)
        };
        
        var cusps = new Dictionary<int, double>();
        return new ChartLongitudes(points, cusps);
    }

    private static ChartLongitudes CreateEmptyChart()
    {
        var points = new Dictionary<ChartPoints, double>();
        var cusps = new Dictionary<int, double>();
        return new ChartLongitudes(points, cusps);
    }

    private static ChartLongitudes CreateChartWithNoAngles()
    {
        var points = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 15.0 },
            { ChartPoints.Moon, 45.0 }
        };
        var cusps = new Dictionary<int, double>();
        return new ChartLongitudes(points, cusps);
    }

    private static ChartLongitudes CreateChartWithMissingAscendant()
    {
        var points = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Mc, 90.0 },
            { ChartPoints.Sun, 15.0 }
        };
        var cusps = new Dictionary<int, double>();
        return new ChartLongitudes(points, cusps);
    }

    private static ChartLongitudes CreateChartWithMissingMC()
    {
        var points = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Ascendant, 0.0 },
            { ChartPoints.Sun, 15.0 }
        };
        var cusps = new Dictionary<int, double>();
        return new ChartLongitudes(points, cusps);
    }

    private static ChartLongitudes CreateChartWithBoundaryConditions()
    {
        var points = new Dictionary<ChartPoints, double>
        {
            // Angles
            { ChartPoints.Ascendant, 0.0 },    // ASC at 0°
            { ChartPoints.Mc, 90.0 },          // MC at 90°
            
            // Planets exactly at quadrant boundaries
            { ChartPoints.Sun, 0.0 },          // Exactly at ASC
            { ChartPoints.Moon, 90.0 },        // Exactly at IC
            { ChartPoints.Mercury, 180.0 },    // Exactly at DESC
            { ChartPoints.Venus, 270.0 }       // Exactly at MC
        };
        
        var cusps = new Dictionary<int, double>();
        return new ChartLongitudes(points, cusps);
    }

    private static ChartLongitudes CreateChartWithLongitudeOverflow()
    {
        var points = new Dictionary<ChartPoints, double>
        {
            // Angles
            { ChartPoints.Ascendant, 0.0 },    // ASC at 0°
            { ChartPoints.Mc, 90.0 },          // MC at 90°
            
            // Planets near longitude overflow
            { ChartPoints.Sun, 359.0 },        // Just before 0°
            { ChartPoints.Moon, 1.0 }          // Just after 0°
        };
        
        var cusps = new Dictionary<int, double>();
        return new ChartLongitudes(points, cusps);
    }

    private static ChartLongitudes CreateChartWithAllQuadrants()
    {
        var points = new Dictionary<ChartPoints, double>
        {
            // Angles
            { ChartPoints.Ascendant, 0.0 },    // ASC at 0°
            { ChartPoints.Mc, 90.0 },          // MC at 90°
            
            // Two planets in each quadrant
            { ChartPoints.Sun, 15.0 },         // Quadrant 1
            { ChartPoints.Moon, 45.0 },        // Quadrant 1
            { ChartPoints.Mercury, 105.0 },    // Quadrant 2
            { ChartPoints.Venus, 135.0 },      // Quadrant 2
            { ChartPoints.Mars, 195.0 },       // Quadrant 3
            { ChartPoints.Jupiter, 225.0 },    // Quadrant 3
            { ChartPoints.Saturn, 285.0 },     // Quadrant 4
            { ChartPoints.Uranus, 315.0 }      // Quadrant 4
        };
        
        var cusps = new Dictionary<int, double>();
        return new ChartLongitudes(points, cusps);
    }

    private static ChartLongitudes CreateChartWithOnlyAngles()
    {
        var points = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Ascendant, 0.0 },
            { ChartPoints.Mc, 90.0 }
        };
        var cusps = new Dictionary<int, double>();
        return new ChartLongitudes(points, cusps);
    }

    private static ChartLongitudes CreateChartForCounterclockwiseTest()
    {
        var points = new Dictionary<ChartPoints, double>
        {
            // Angles
            { ChartPoints.Ascendant, 0.0 },    // ASC at 0°
            { ChartPoints.Mc, 90.0 },          // MC at 90°
            
            // Planets positioned to test counterclockwise ordering
            { ChartPoints.Sun, 15.0 },         // Quadrant 1 (ASC to IC)
            { ChartPoints.Moon, 105.0 },       // Quadrant 2 (IC to DESC)
            { ChartPoints.Mercury, 195.0 },    // Quadrant 3 (DESC to MC)
            { ChartPoints.Venus, 285.0 }       // Quadrant 4 (MC to ASC)
        };
        
        var cusps = new Dictionary<int, double>();
        return new ChartLongitudes(points, cusps);
    }

    private static ChartLongitudes CreateChartWithZeroLongitude()
    {
        var points = new Dictionary<ChartPoints, double>
        {
            // Angles
            { ChartPoints.Ascendant, 0.0 },    // ASC at 0°
            { ChartPoints.Mc, 90.0 },          // MC at 90°
            
            // Planet at zero longitude
            { ChartPoints.Sun, 0.0 }           // Exactly at 0°
        };
        
        var cusps = new Dictionary<int, double>();
        return new ChartLongitudes(points, cusps);
    }

    private static ChartLongitudes CreateChartWithLargeDataset()
    {
        var points = new Dictionary<ChartPoints, double>();
        
        // Add angles
        points[ChartPoints.Ascendant] = 0.0;
        points[ChartPoints.Mc] = 90.0;
        
        // Add 20 planets distributed across quadrants
        var planets = new[]
        {
            ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury, ChartPoints.Venus, ChartPoints.Mars,
            ChartPoints.Jupiter, ChartPoints.Saturn, ChartPoints.Uranus, ChartPoints.Neptune, ChartPoints.Pluto,
            ChartPoints.NorthNode, ChartPoints.TrueNode, ChartPoints.Chiron, ChartPoints.Pholus, ChartPoints.Ceres,
            ChartPoints.Pallas, ChartPoints.Juno, ChartPoints.Vesta, ChartPoints.Eris, ChartPoints.Nessus
        };
        
        var longitudes = new[]
        {
            15.0, 45.0, 105.0, 135.0, 195.0, 225.0, 285.0, 315.0, 30.0, 60.0,
            120.0, 150.0, 210.0, 240.0, 300.0, 330.0, 20.0, 50.0, 110.0, 140.0
        };
        
        for (int i = 0; i < planets.Length; i++)
        {
            points[planets[i]] = longitudes[i];
        }
        
        var cusps = new Dictionary<int, double>();
        return new ChartLongitudes(points, cusps);
    }


    #endregion
}
