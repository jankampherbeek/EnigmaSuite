// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Points;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestHouseCounts
{
    [Test]
    public void TestCountPosInHouses_EmptyList()
    {
        // Test with empty list of positions
        var positions = new List<BlaPositions>();
        
        var result = HouseCounts.CountPosInHouses(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            for (int i = 1; i <= 12; i++)
            {
                Assert.That(result[i], Is.EqualTo(0), $"House {i} should have count 0");
            }
        });
    }

    [Test]
    public void TestCountPosInHouses_SinglePosition()
    {
        // Test with single position in house 5
        var positions = new List<BlaPositions>
        {
            new BlaPositions(ChartPoints.Sun, 120.0, 4, 2, 5)
        };
        
        var result = HouseCounts.CountPosInHouses(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            Assert.That(result[5], Is.EqualTo(1), "House 5 should have count 1");
            
            // All other houses should have count 0
            for (int i = 1; i <= 12; i++)
            {
                if (i != 5)
                {
                    Assert.That(result[i], Is.EqualTo(0), $"House {i} should have count 0");
                }
            }
        });
    }

    [Test]
    public void TestCountPosInHouses_MultiplePositionsInDifferentHouses()
    {
        // Test with multiple positions in different houses
        var positions = new List<BlaPositions>
        {
            new BlaPositions(ChartPoints.Sun, 45.0, 2, 1, 3),
            new BlaPositions(ChartPoints.Moon, 120.0, 4, 2, 5),
            new BlaPositions(ChartPoints.Mercury, 200.0, 7, 1, 8),
            new BlaPositions(ChartPoints.Venus, 300.0, 10, 2, 11)
        };
        
        var result = HouseCounts.CountPosInHouses(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            Assert.That(result[3], Is.EqualTo(1), "House 3 should have count 1 (Sun)");
            Assert.That(result[5], Is.EqualTo(1), "House 5 should have count 1 (Moon)");
            Assert.That(result[8], Is.EqualTo(1), "House 8 should have count 1 (Mercury)");
            Assert.That(result[11], Is.EqualTo(1), "House 11 should have count 1 (Venus)");
            
            // All other houses should have count 0
            for (int i = 1; i <= 12; i++)
            {
                if (i != 3 && i != 5 && i != 8 && i != 11)
                {
                    Assert.That(result[i], Is.EqualTo(0), $"House {i} should have count 0");
                }
            }
        });
    }

    [Test]
    public void TestCountPosInHouses_MultiplePositionsInSameHouse()
    {
        // Test with multiple positions in the same house
        var positions = new List<BlaPositions>
        {
            new BlaPositions(ChartPoints.Sun, 45.0, 2, 1, 3),
            new BlaPositions(ChartPoints.Moon, 50.0, 2, 2, 3),
            new BlaPositions(ChartPoints.Mercury, 55.0, 2, 3, 3),
            new BlaPositions(ChartPoints.Venus, 120.0, 4, 2, 5)
        };
        
        var result = HouseCounts.CountPosInHouses(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            Assert.That(result[3], Is.EqualTo(3), "House 3 should have count 3 (Sun, Moon, Mercury)");
            Assert.That(result[5], Is.EqualTo(1), "House 5 should have count 1 (Venus)");
            
            // All other houses should have count 0
            for (int i = 1; i <= 12; i++)
            {
                if (i != 3 && i != 5)
                {
                    Assert.That(result[i], Is.EqualTo(0), $"House {i} should have count 0");
                }
            }
        });
    }

    [Test]
    public void TestCountPosInHouses_AllHousesOccupied()
    {
        // Test with positions in all 12 houses
        var positions = new List<BlaPositions>();
        
        // Create one position for each house
        for (int house = 1; house <= 12; house++)
        {
            var longitude = (house - 1) * 30.0; // Distribute evenly around the zodiac
            var sign = ((house - 1) % 12) + 1;
            var decan = ((house - 1) % 3) + 1;
            
            positions.Add(new BlaPositions(ChartPoints.Sun, longitude, sign, decan, house));
        }
        
        var result = HouseCounts.CountPosInHouses(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            for (int i = 1; i <= 12; i++)
            {
                Assert.That(result[i], Is.EqualTo(1), $"House {i} should have count 1");
            }
        });
    }

    [Test]
    public void TestCountPosInHouses_EdgeCases()
    {
        // Test with edge cases - houses 1 and 12
        var positions = new List<BlaPositions>
        {
            new BlaPositions(ChartPoints.Ascendant, 0.0, 1, 1, 1),
            new BlaPositions(ChartPoints.Mc, 350.0, 12, 3, 12),
            new BlaPositions(ChartPoints.Sun, 180.0, 7, 1, 7)
        };
        
        var result = HouseCounts.CountPosInHouses(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            Assert.That(result[1], Is.EqualTo(1), "House 1 should have count 1 (Ascendant)");
            Assert.That(result[7], Is.EqualTo(1), "House 7 should have count 1 (Sun)");
            Assert.That(result[12], Is.EqualTo(1), "House 12 should have count 1 (MC)");
            
            // All other houses should have count 0
            for (int i = 1; i <= 12; i++)
            {
                if (i != 1 && i != 7 && i != 12)
                {
                    Assert.That(result[i], Is.EqualTo(0), $"House {i} should have count 0");
                }
            }
        });
    }

    [Test]
    public void TestCountPosInHouses_NullInput()
    {
        // Test that null input throws ArgumentNullException
        Assert.Throws<ArgumentNullException>(() => HouseCounts.CountPosInHouses(null!));
    }

    [Test]
    public void TestCountPosInHouses_LargeNumberOfPositions()
    {
        // Test with a large number of positions to ensure performance
        var positions = new List<BlaPositions>();
        var random = new Random(42); // Fixed seed for reproducible tests
        
        // Create 100 random positions
        for (int i = 0; i < 100; i++)
        {
            var house = random.Next(1, 13); // Random house 1-12
            var longitude = random.NextDouble() * 360.0;
            var sign = random.Next(1, 13);
            var decan = random.Next(1, 4);
            
            positions.Add(new BlaPositions(ChartPoints.Sun, longitude, sign, decan, house));
        }
        
        var result = HouseCounts.CountPosInHouses(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            
            // Verify that the sum of all house counts equals the total number of positions
            var totalCount = result.Values.Sum();
            Assert.That(totalCount, Is.EqualTo(100), "Total count should equal number of input positions");
            
            // Verify that no house has a negative count
            for (int i = 1; i <= 12; i++)
            {
                Assert.That(result[i], Is.GreaterThanOrEqualTo(0), $"House {i} should not have negative count");
            }
        });
    }

    [Test]
    public void TestCountPosInHouses_DifferentChartPoints()
    {
        // Test with different chart points to ensure the method works with all point types
        var positions = new List<BlaPositions>
        {
            new BlaPositions(ChartPoints.Sun, 45.0, 2, 1, 3),
            new BlaPositions(ChartPoints.Moon, 120.0, 4, 2, 5),
            new BlaPositions(ChartPoints.Mercury, 200.0, 7, 1, 8),
            new BlaPositions(ChartPoints.Venus, 300.0, 10, 2, 11),
            new BlaPositions(ChartPoints.Mars, 60.0, 3, 1, 4),
            new BlaPositions(ChartPoints.Jupiter, 150.0, 6, 1, 6),
            new BlaPositions(ChartPoints.Saturn, 240.0, 9, 1, 9),
            new BlaPositions(ChartPoints.Uranus, 330.0, 12, 1, 12)
        };
        
        var result = HouseCounts.CountPosInHouses(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            Assert.That(result[3], Is.EqualTo(1), "House 3 should have count 1 (Sun)");
            Assert.That(result[4], Is.EqualTo(1), "House 4 should have count 1 (Mars)");
            Assert.That(result[5], Is.EqualTo(1), "House 5 should have count 1 (Moon)");
            Assert.That(result[6], Is.EqualTo(1), "House 6 should have count 1 (Jupiter)");
            Assert.That(result[8], Is.EqualTo(1), "House 8 should have count 1 (Mercury)");
            Assert.That(result[9], Is.EqualTo(1), "House 9 should have count 1 (Saturn)");
            Assert.That(result[11], Is.EqualTo(1), "House 11 should have count 1 (Venus)");
            Assert.That(result[12], Is.EqualTo(1), "House 12 should have count 1 (Uranus)");
            
            // Houses 1, 2, 7, 10 should have count 0
            Assert.That(result[1], Is.EqualTo(0), "House 1 should have count 0");
            Assert.That(result[2], Is.EqualTo(0), "House 2 should have count 0");
            Assert.That(result[7], Is.EqualTo(0), "House 7 should have count 0");
            Assert.That(result[10], Is.EqualTo(0), "House 10 should have count 0");
        });
    }
}
