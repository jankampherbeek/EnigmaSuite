// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestHousePositions
{
    private HousePositions _housePositions;

    [SetUp]
    public void Setup()
    {
        _housePositions = new HousePositions();
    }

    [Test]
    public void TestDefineHousePositions_NormalHouses()
    {
        // Create test chart with normal house cusps (no overflow)
        var chart = CreateTestChartWithNormalHouses();
        
        var result = _housePositions.DefineHousePositions(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(3)); // Sun, Moon, Mercury
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(1)); // Sun at 15° should be in House 1
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(2)); // Moon at 45° should be in House 2
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(3)); // Mercury at 75° should be in House 3
        });
    }

    [Test]
    public void TestDefineHousePositions_OverflowHouses()
    {
        // Create test chart with house cusps that overflow across 0°
        var chart = CreateTestChartWithOverflowHouses();
        
        var result = _housePositions.DefineHousePositions(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(3)); // Sun, Moon, Mercury
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(12)); // Sun at 350° should be in House 12
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(1)); // Moon at 10° should be in House 1
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(2)); // Mercury at 30° should be in House 2
        });
    }

    [Test]
    public void TestDefineHousePositions_ExactCuspPositions()
    {
        // Test chart points positioned exactly on cusps
        var chart = CreateTestChartWithExactCuspPositions();
        
        var result = _housePositions.DefineHousePositions(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(2)); // Only non-cusp points
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(1)); // Sun at 0° (Cusp 1) should be in House 1
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(2)); // Moon at 30° (Cusp 2) should be in House 2
        });
    }

    [Test]
    public void TestDefineHousePositions_EmptyChart()
    {
        // Test with empty chart
        var chart = CreateEmptyChart();
        
        var result = _housePositions.DefineHousePositions(chart);
        
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestDefineHousePositions_NoCusps()
    {
        // Test chart with only celestial bodies, no cusps
        var chart = CreateChartWithNoCusps();
        
        var result = _housePositions.DefineHousePositions(chart);
        
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestDefineHousePositions_BoundaryConditions()
    {
        // Test chart points at house boundaries
        var chart = CreateTestChartWithBoundaryConditions();
        
        var result = _housePositions.DefineHousePositions(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(1)); // Sun at 0° (start of House 1)
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(1)); // Moon at 29.999° (end of House 1)
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(2)); // Mercury at 30° (start of House 2)
            Assert.That(result[ChartPoints.Venus], Is.EqualTo(2)); // Venus at 59.999° (end of House 2)
        });
    }

    [Test]
    public void TestDefineHousePositions_OverflowBoundaryConditions()
    {
        // Test chart points at overflow house boundaries
        var chart = CreateTestChartWithOverflowBoundaryConditions();
        
        var result = _housePositions.DefineHousePositions(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(12)); // Sun at 350° (start of House 12)
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(12)); // Moon at 359.999° (end of House 12)
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(12)); // Mercury at 0° (still in House 12: 350° - 10°)
            Assert.That(result[ChartPoints.Venus], Is.EqualTo(12)); // Venus at 9.999° (still in House 12: 350° - 10°)
        });
    }

    private static CalculatedChart CreateTestChartWithNormalHouses()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // House cusps
            { ChartPoints.Cusp1, CreateFullPointPos(0.0) },   // House 1: 0° - 30°
            { ChartPoints.Cusp2, CreateFullPointPos(30.0) },  // House 2: 30° - 60°
            { ChartPoints.Cusp3, CreateFullPointPos(60.0) },  // House 3: 60° - 90°
            { ChartPoints.Cusp4, CreateFullPointPos(90.0) },  // House 4: 90° - 120°
            
            // Celestial bodies
            { ChartPoints.Sun, CreateFullPointPos(15.0) },      // Should be in House 1
            { ChartPoints.Moon, CreateFullPointPos(45.0) },     // Should be in House 2
            { ChartPoints.Mercury, CreateFullPointPos(75.0) }   // Should be in House 3
        };
        
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateTestChartWithOverflowHouses()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // House cusps with overflow across 0°
            { ChartPoints.Cusp1, CreateFullPointPos(10.0) },   // House 1: 10° - 30°
            { ChartPoints.Cusp2, CreateFullPointPos(30.0) },   // House 2: 30° - 60°
            { ChartPoints.Cusp3, CreateFullPointPos(60.0) },   // House 3: 60° - 90°
            { ChartPoints.Cusp12, CreateFullPointPos(350.0) }, // House 12: 350° - 10°
            
            // Celestial bodies
            { ChartPoints.Sun, CreateFullPointPos(350.0) },     // Should be in House 12
            { ChartPoints.Moon, CreateFullPointPos(10.0) },     // Should be in House 1
            { ChartPoints.Mercury, CreateFullPointPos(30.0) }   // Should be in House 2
        };
        
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateTestChartWithExactCuspPositions()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // House cusps
            { ChartPoints.Cusp1, CreateFullPointPos(0.0) },   // House 1: 0° - 30°
            { ChartPoints.Cusp2, CreateFullPointPos(30.0) },  // House 2: 30° - 60°
            
            // Celestial bodies at exact cusp positions
            { ChartPoints.Sun, CreateFullPointPos(0.0) },      // Exactly on Cusp 1
            { ChartPoints.Moon, CreateFullPointPos(30.0) }     // Exactly on Cusp 2
        };
        
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateEmptyChart()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>();
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateChartWithNoCusps()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            { ChartPoints.Sun, CreateFullPointPos(15.0) },
            { ChartPoints.Moon, CreateFullPointPos(45.0) }
        };
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateTestChartWithBoundaryConditions()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // House cusps
            { ChartPoints.Cusp1, CreateFullPointPos(0.0) },   // House 1: 0° - 30°
            { ChartPoints.Cusp2, CreateFullPointPos(30.0) },  // House 2: 30° - 60°
            { ChartPoints.Cusp3, CreateFullPointPos(60.0) },  // House 3: 60° - 90°
            
            // Celestial bodies at boundaries
            { ChartPoints.Sun, CreateFullPointPos(0.0) },      // Start of House 1
            { ChartPoints.Moon, CreateFullPointPos(29.999) },  // End of House 1
            { ChartPoints.Mercury, CreateFullPointPos(30.0) }, // Start of House 2
            { ChartPoints.Venus, CreateFullPointPos(59.999) }  // End of House 2
        };
        
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateTestChartWithOverflowBoundaryConditions()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // House cusps with overflow
            { ChartPoints.Cusp1, CreateFullPointPos(10.0) },   // House 1: 10° - 30°
            { ChartPoints.Cusp2, CreateFullPointPos(30.0) },   // House 2: 30° - 60°
            { ChartPoints.Cusp12, CreateFullPointPos(350.0) }, // House 12: 350° - 10°
            
            // Celestial bodies at overflow boundaries
            { ChartPoints.Sun, CreateFullPointPos(350.0) },     // Start of House 12
            { ChartPoints.Moon, CreateFullPointPos(359.999) },  // End of House 12
            { ChartPoints.Mercury, CreateFullPointPos(0.0) },   // Start of House 1
            { ChartPoints.Venus, CreateFullPointPos(9.999) }    // End of House 1
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
        var simpleDateTime = new SimpleDateTime(2025, 1, 1, 12.0, Calendars.Gregorian);
        var location = new Location("Test", 0.0, 0.0);
        var metaData = new MetaData("Test Chart", "", "", "", 1, 1);
        var fullDateTime = new FullDateTime("2025/01/01", "12.00", 2460100.0);
        return new ChartData(1, metaData, location, fullDateTime);
    }
}
