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
    private QuadrantPositions _quadrantPositions;

    [SetUp]
    public void Setup()
    {
        _quadrantPositions = new QuadrantPositions();
    }

    [Test]
    public void TestDefineQuadrants_StandardChart()
    {
        // Create test chart with realistic angle positions
        // Ascendant at 15°, MC at 105° (IC at 285°, Descendant at 195°)
        var chart = CreateTestChartWithStandardAngles();
        
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(3)); // Sun, Moon, Mercury
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(1)); // Sun at 30° should be in Quadrant 1 (15° - 285°)
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(4)); // Moon at 150° should be in Quadrant 4 (105° - 15°)
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(3)); // Mercury at 240° should be in Quadrant 3 (195° - 105°)
        });
    }

    [Test]
    public void TestDefineQuadrants_OverflowChart()
    {
        // Create test chart with angles that overflow across 0°
        // Ascendant at 350°, MC at 80° (IC at 260°, Descendant at 170°)
        var chart = CreateTestChartWithOverflowAngles();
        
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(3)); // Sun, Moon, Mercury
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(2)); // Sun at 340° should be in Quadrant 2 (260° - 350°)
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(1)); // Moon at 10° should be in Quadrant 1 (350° - 260°)
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(4)); // Mercury at 120° should be in Quadrant 4 (80° - 350°)
        });
    }

    [Test]
    public void TestDefineQuadrants_ExactAnglePositions()
    {
        // Test chart points positioned exactly on angles
        var chart = CreateTestChartWithExactAnglePositions();
        
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(2)); // Only non-angle points
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(1)); // Sun at 15° (Ascendant) should be in Quadrant 1
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(4)); // Moon at 105° (MC) should be in Quadrant 4
        });
    }

    [Test]
    public void TestDefineQuadrants_EmptyChart()
    {
        // Test with empty chart
        var chart = CreateEmptyChart();
        
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestDefineQuadrants_NoAngles()
    {
        // Test chart with only celestial bodies, no angles
        var chart = CreateChartWithNoAngles();
        
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestDefineQuadrants_BoundaryConditions()
    {
        // Test chart points at quadrant boundaries
        var chart = CreateTestChartWithBoundaryConditions();
        
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(1)); // Sun at 15° (start of Quadrant 1)
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(1)); // Moon at 104.999° (still in Quadrant 1)
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(4)); // Mercury at 105° (start of Quadrant 4)
            Assert.That(result[ChartPoints.Venus], Is.EqualTo(4)); // Venus at 194.999° (in Quadrant 4)
        });
    }

    [Test]
    public void TestDefineQuadrants_OverflowBoundaryConditions()
    {
        // Test chart points at overflow quadrant boundaries
        var chart = CreateTestChartWithOverflowBoundaryConditions();
        
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(2)); // Sun at 340° (in Quadrant 2)
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(2)); // Moon at 349.999° (in Quadrant 2)
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(1)); // Mercury at 350° (start of Quadrant 1)
            Assert.That(result[ChartPoints.Venus], Is.EqualTo(1)); // Venus at 79.999° (still in Quadrant 1)
        });
    }

    [Test]
    public void TestDefineQuadrants_DebugStandardChart()
    {
        // Create test chart with realistic angle positions
        // Ascendant at 15°, MC at 105° (IC at 285°, Descendant at 195°)
        var chart = CreateTestChartWithStandardAngles();
        
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        // Let's just check that we get some results
        Assert.That(result, Has.Count.GreaterThan(0));
    }

    private static CalculatedChart CreateTestChartWithStandardAngles()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Angles with realistic positions
            { ChartPoints.Ascendant, CreateFullPointPos(15.0) },   // Quadrant 1: 15° - 285° (Ascendant to IC)
            { ChartPoints.Mc, CreateFullPointPos(105.0) },         // Quadrant 4: 105° - 15° (MC to Ascendant)
                                                                    // Quadrant 2: 285° - 195° (IC to Descendant)
                                                                    // Quadrant 3: 195° - 105° (Descendant to MC)
            
            // Celestial bodies
            { ChartPoints.Sun, CreateFullPointPos(30.0) },         // Should be in Quadrant 1
            { ChartPoints.Moon, CreateFullPointPos(150.0) },       // Should be in Quadrant 4
            { ChartPoints.Mercury, CreateFullPointPos(240.0) }     // Should be in Quadrant 3
        };
        
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateTestChartWithOverflowAngles()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Angles with overflow across 0°
            { ChartPoints.Ascendant, CreateFullPointPos(350.0) },  // Quadrant 1: 350° - 260° (Ascendant to IC)
            { ChartPoints.Mc, CreateFullPointPos(80.0) },          // Quadrant 4: 80° - 350° (MC to Ascendant)
                                                                    // Quadrant 2: 260° - 170° (IC to Descendant)
                                                                    // Quadrant 3: 170° - 80° (Descendant to MC)
            
            // Celestial bodies
            { ChartPoints.Sun, CreateFullPointPos(340.0) },        // Should be in Quadrant 1
            { ChartPoints.Moon, CreateFullPointPos(10.0) },        // Should be in Quadrant 1
            { ChartPoints.Mercury, CreateFullPointPos(120.0) }     // Should be in Quadrant 4
        };
        
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateTestChartWithExactAnglePositions()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Angles
            { ChartPoints.Ascendant, CreateFullPointPos(15.0) },   // Quadrant 1: 15° - 285° (Ascendant to IC)
            { ChartPoints.Mc, CreateFullPointPos(105.0) },         // Quadrant 4: 105° - 15° (MC to Ascendant)
            
            // Celestial bodies at exact angle positions
            { ChartPoints.Sun, CreateFullPointPos(15.0) },         // Exactly on Ascendant
            { ChartPoints.Moon, CreateFullPointPos(105.0) }        // Exactly on MC
        };
        
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateEmptyChart()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>();
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateChartWithNoAngles()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            { ChartPoints.Sun, CreateFullPointPos(30.0) },
            { ChartPoints.Moon, CreateFullPointPos(150.0) }
        };
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateTestChartWithBoundaryConditions()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Angles
            { ChartPoints.Ascendant, CreateFullPointPos(15.0) },   // Quadrant 1: 15° - 285° (Ascendant to IC)
            { ChartPoints.Mc, CreateFullPointPos(105.0) },         // Quadrant 4: 105° - 15° (MC to Ascendant)
            
            // Celestial bodies at boundaries
            { ChartPoints.Sun, CreateFullPointPos(15.0) },         // Start of Quadrant 1
            { ChartPoints.Moon, CreateFullPointPos(104.999) },     // Still in Quadrant 1
            { ChartPoints.Mercury, CreateFullPointPos(105.0) },    // Start of Quadrant 4
            { ChartPoints.Venus, CreateFullPointPos(194.999) }     // In Quadrant 3
        };
        
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateTestChartWithOverflowBoundaryConditions()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Angles with overflow
            { ChartPoints.Ascendant, CreateFullPointPos(350.0) },  // Quadrant 1: 350° - 260° (Ascendant to IC)
            { ChartPoints.Mc, CreateFullPointPos(80.0) },          // Quadrant 4: 80° - 350° (MC to Ascendant)
                                                                    // Quadrant 2: 260° - 170° (IC to Descendant)
                                                                    // Quadrant 3: 170° - 80° (Descendant to MC)
            
            // Celestial bodies at overflow boundaries
            { ChartPoints.Sun, CreateFullPointPos(340.0) },        // In Quadrant 1
            { ChartPoints.Moon, CreateFullPointPos(349.999) },     // In Quadrant 1
            { ChartPoints.Mercury, CreateFullPointPos(350.0) },    // Start of Quadrant 1
            { ChartPoints.Venus, CreateFullPointPos(79.999) }      // Still in Quadrant 1
        };
        
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static FullPointPos CreateFullPointPos(double longitude)
    {
        PosSpeed psDistance = new(longitude, 0.0);
        PosSpeed psLongitude = new(longitude, 0.0);
        PosSpeed psLatitude = new(0.0, 0.0);
        PosSpeed psRightAscension = new(0.0, 0.0);
        PosSpeed psDeclination = new(0.0, 0.0);
        PosSpeed psAzimuth = new(0.0, 0.0);
        PosSpeed psAltitude = new(0.0, 0.0);
        PointPosSpeeds ppsEcliptical = new(psLongitude, psLatitude, psDistance);
        PointPosSpeeds ppsEquatorial = new(psRightAscension, psDeclination, psDistance);
        PointPosSpeeds ppsHorizontal = new(psAzimuth, psAltitude, psDistance);
        return new FullPointPos(ppsEcliptical, ppsEquatorial, ppsHorizontal);
    }

    private static ChartData CreateTestChartData()
    {
        // Create minimal test chart data
        var location = new Location("Test", 0.0, 0.0);
        var metaData = new MetaData("Test Chart", "", "", "", 1, 1);
        var fullDateTime = new FullDateTime("2025/01/01", "12.00", 2460100.0);
        return new ChartData(1, metaData, location, fullDateTime);
    }
}
