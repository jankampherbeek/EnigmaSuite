// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
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
    public void TestDefineQuadrants_EmptyChart()
    {
        // Test with empty chart (no positions)
        var chart = CreateTestChart(new Dictionary<ChartPoints, FullPointPos>());
        
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            for (int i = 1; i <= 4; i++)
            {
                Assert.That(result[i], Is.EqualTo(0), $"Quadrant {i} should have count 0");
            }
        });
    }

    [Test]
    public void TestDefineQuadrants_StandardChart()
    {
        // Test with standard chart: Ascendant at 15°, MC at 105° (IC at 285°, Descendant at 195°)
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Angles
            { ChartPoints.Ascendant, CreateFullPointPos(15.0) },   // Quadrant 1 starts at 15°
            { ChartPoints.Mc, CreateFullPointPos(105.0) },         // Quadrant 4 starts at 105°
            
            // Common points in different quadrants
            { ChartPoints.Sun, CreateFullPointPos(45.0) },         // Quadrant 1 (15°-105°)
            { ChartPoints.Moon, CreateFullPointPos(135.0) },       // Quadrant 2 (105°-195°)
            { ChartPoints.Mercury, CreateFullPointPos(225.0) },    // Quadrant 3 (195°-285°)
            { ChartPoints.Venus, CreateFullPointPos(315.0) }       // Quadrant 4 (285°-15°)
        };
        
        var chart = CreateTestChart(positions);
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[1], Is.EqualTo(1), "Quadrant 1 should have count 1 (Sun)");
            Assert.That(result[2], Is.EqualTo(1), "Quadrant 2 should have count 1 (Moon)");
            Assert.That(result[3], Is.EqualTo(1), "Quadrant 3 should have count 1 (Mercury)");
            Assert.That(result[4], Is.EqualTo(1), "Quadrant 4 should have count 1 (Venus)");
        });
    }


    [Test]
    public void TestDefineQuadrants_CrossZeroDegree()
    {
        // Test with quadrants that cross the 0° boundary
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Angles - Ascendant at 330°, MC at 60° (creates quadrants crossing 0°)
            { ChartPoints.Ascendant, CreateFullPointPos(330.0) },  // Quadrant 1 starts at 330°
            { ChartPoints.Mc, CreateFullPointPos(260.0) },          // Quadrant 4 starts at 260°
            // Quadrant 1 starts at 330.0, 2 starts at 80.0, 3 starts at 150.0, 4 starts at 330.0  
            
            
            // Points in different quadrants
            { ChartPoints.Sun, CreateFullPointPos(350.0) },        // Quadrant 1
            { ChartPoints.Moon, CreateFullPointPos(10.0) },        // Quadrant 1
            { ChartPoints.Mercury, CreateFullPointPos(120.0) },    // Quadrant 2
            { ChartPoints.Venus, CreateFullPointPos(240.0) },      // Quadrant 3
            { ChartPoints.Mars, CreateFullPointPos(300.0) }        // Quadrant 4
        };
        
        var chart = CreateTestChart(positions);
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[1], Is.EqualTo(2), "Quadrant 1 should have count 2 (Sun, Moon)");
            Assert.That(result[2], Is.EqualTo(1), "Quadrant 2 should have count 1 (Mercury)");
            Assert.That(result[3], Is.EqualTo(1), "Quadrant 3 should have count 1 (Venus)");
            Assert.That(result[4], Is.EqualTo(1), "Quadrant 4 should have count 1 (Mars)");
        });
    }

    [Test]
    public void TestDefineQuadrants_MissingAngles()
    {
        // Test with missing angle points (should return all zeros)
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Only common points, no angles
            { ChartPoints.Sun, CreateFullPointPos(45.0) },
            { ChartPoints.Moon, CreateFullPointPos(135.0) },
            { ChartPoints.Mercury, CreateFullPointPos(225.0) },
            { ChartPoints.Venus, CreateFullPointPos(315.0) }
        };
        
        var chart = CreateTestChart(positions);
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            for (int i = 1; i <= 4; i++)
            {
                Assert.That(result[i], Is.EqualTo(0), $"Quadrant {i} should have count 0");
            }
        });
    }

    [Test]
    public void TestDefineQuadrants_MissingOneAngle()
    {
        // Test with only Ascendant but no MC
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Only Ascendant, no MC
            { ChartPoints.Ascendant, CreateFullPointPos(15.0) },
            
            // Common points
            { ChartPoints.Sun, CreateFullPointPos(45.0) },
            { ChartPoints.Moon, CreateFullPointPos(135.0) }
        };
        
        var chart = CreateTestChart(positions);
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            for (int i = 1; i <= 4; i++)
            {
                Assert.That(result[i], Is.EqualTo(0), $"Quadrant {i} should have count 0");
            }
        });
    }

    [Test]
    public void TestDefineQuadrants_OnlyAngles()
    {
        // Test with only angle points, no common points
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            { ChartPoints.Ascendant, CreateFullPointPos(15.0) },
            { ChartPoints.Mc, CreateFullPointPos(105.0) }
        };
        
        var chart = CreateTestChart(positions);
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            for (int i = 1; i <= 4; i++)
            {
                Assert.That(result[i], Is.EqualTo(0), $"Quadrant {i} should have count 0");
            }
        });
    }

    [Test]
    public void TestDefineQuadrants_LargeDataset()
    {
        // Test with a large dataset to ensure performance and correctness
        var positions = new Dictionary<ChartPoints, FullPointPos>();
        var random = new Random(42); // Fixed seed for reproducible tests
        
        // Add angles
        positions.Add(ChartPoints.Ascendant, CreateFullPointPos(0.0));
        positions.Add(ChartPoints.Mc, CreateFullPointPos(90.0));
        
        // Add 50 random common points
        var commonPoints = new List<ChartPoints>
        {
            ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury, ChartPoints.Venus, ChartPoints.Earth,
            ChartPoints.Mars, ChartPoints.Jupiter, ChartPoints.Saturn, ChartPoints.Uranus, ChartPoints.Neptune,
            ChartPoints.Pluto, ChartPoints.NorthNode, ChartPoints.TrueNode, ChartPoints.Chiron,
            ChartPoints.PersephoneRam, ChartPoints.HermesRam, ChartPoints.DemeterRam,
            ChartPoints.CupidoUra, ChartPoints.HadesUra, ChartPoints.ZeusUra, ChartPoints.KronosUra,
            ChartPoints.ApollonUra, ChartPoints.AdmetosUra, ChartPoints.VulcanusUra, ChartPoints.PoseidonUra,
            ChartPoints.Eris, ChartPoints.Pholus, ChartPoints.Ceres, ChartPoints.Pallas, ChartPoints.Juno,
            ChartPoints.Vesta, ChartPoints.Isis, ChartPoints.Nessus, ChartPoints.Huya, ChartPoints.Varuna,
            ChartPoints.Ixion, ChartPoints.Quaoar, ChartPoints.Haumea, ChartPoints.Orcus, ChartPoints.Makemake,
            ChartPoints.Sedna, ChartPoints.Hygieia, ChartPoints.Astraea, ChartPoints.ApogeeMean,
            ChartPoints.ApogeeCorrected, ChartPoints.ApogeeInterpolated, ChartPoints.PersephoneCarteret,
            ChartPoints.VulcanusCarteret, ChartPoints.PerigeeInterpolated, ChartPoints.Priapus,
            ChartPoints.PriapusCorrected, ChartPoints.Dragon, ChartPoints.Beast, ChartPoints.SouthNode,
            ChartPoints.BlackSun, ChartPoints.Diamond
        };
        
        for (int i = 0; i < 50; i++)
        {
            var longitude = random.NextDouble() * 360.0;
            var point = commonPoints[i % commonPoints.Count]; // Cycle through available common points
            positions.Add(point, CreateFullPointPos(longitude));
        }
        
        var chart = CreateTestChart(positions);
        var result = _quadrantPositions.DefineQuadrants(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            var totalCount = result.Values.Sum();
            Assert.That(totalCount, Is.EqualTo(50), "Total count should be 50");
            
            // Verify all counts are non-negative
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value, Is.GreaterThanOrEqualTo(0), $"Quadrant {kvp.Key} should have non-negative count");
            }
        });
    }

    [Test]
    public void TestDefineQuadrants_NullChart()
    {
        // Test with null chart - should throw ArgumentNullException
        Assert.That(() => _quadrantPositions.DefineQuadrants(null!), 
            Throws.TypeOf<ArgumentNullException>());
    }

    // Helper methods
    private static CalculatedChart CreateTestChart(Dictionary<ChartPoints, FullPointPos> positions)
    {
        var chartData = CreateTestChartData();
        return new CalculatedChart(positions, chartData, 23.5);
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
