// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestSignCounts
{
    [Test]
    public void TestCountPointsInSigns_EmptyList()
    {
        // Test with empty list
        var positions = new List<BlaPositions>();
        
        var result = SignCounts.CountPointsInSigns(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            for (int i = 1; i <= 12; i++)
            {
                Assert.That(result[i], Is.EqualTo(0), $"Sign {i} should have count 0");
            }
        });
    }

    [Test]
    public void TestCountPointsInSigns_SinglePosition()
    {
        // Test with single position in Aries (sign 1)
        var positions = new List<BlaPositions>
        {
            new(ChartPoints.Sun, 15.0, 1, 2, 1) // Sun in Aries, 2nd decan, house 1
        };
        
        var result = SignCounts.CountPointsInSigns(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            Assert.That(result[1], Is.EqualTo(1), "Aries should have count 1");
            for (int i = 2; i <= 12; i++)
            {
                Assert.That(result[i], Is.EqualTo(0), $"Sign {i} should have count 0");
            }
        });
    }

    [Test]
    public void TestCountPointsInSigns_MultiplePositions()
    {
        // Test with multiple positions in different signs
        var positions = new List<BlaPositions>
        {
            new(ChartPoints.Sun, 15.0, 1, 2, 1),    // Sun in Aries
            new(ChartPoints.Moon, 45.0, 2, 2, 2),   // Moon in Taurus
            new(ChartPoints.Mercury, 75.0, 3, 1, 3), // Mercury in Gemini
            new(ChartPoints.Venus, 105.0, 4, 1, 4)  // Venus in Cancer
        };
        
        var result = SignCounts.CountPointsInSigns(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            Assert.That(result[1], Is.EqualTo(1), "Aries should have count 1");
            Assert.That(result[2], Is.EqualTo(1), "Taurus should have count 1");
            Assert.That(result[3], Is.EqualTo(1), "Gemini should have count 1");
            Assert.That(result[4], Is.EqualTo(1), "Cancer should have count 1");
            for (int i = 5; i <= 12; i++)
            {
                Assert.That(result[i], Is.EqualTo(0), $"Sign {i} should have count 0");
            }
        });
    }

    [Test]
    public void TestCountPointsInSigns_DuplicateSigns()
    {
        // Test with multiple positions in the same sign
        var positions = new List<BlaPositions>
        {
            new(ChartPoints.Sun, 15.0, 1, 2, 1),     // Sun in Aries
            new(ChartPoints.Mars, 25.0, 1, 3, 1),    // Mars in Aries
            new(ChartPoints.Mercury, 5.0, 1, 1, 1),  // Mercury in Aries
            new(ChartPoints.Moon, 45.0, 2, 2, 2),    // Moon in Taurus
            new(ChartPoints.Venus, 55.0, 2, 3, 2)    // Venus in Taurus
        };
        
        var result = SignCounts.CountPointsInSigns(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            Assert.That(result[1], Is.EqualTo(3), "Aries should have count 3");
            Assert.That(result[2], Is.EqualTo(2), "Taurus should have count 2");
            for (int i = 3; i <= 12; i++)
            {
                Assert.That(result[i], Is.EqualTo(0), $"Sign {i} should have count 0");
            }
        });
    }

    [Test]
    public void TestCountPointsInSigns_AllSigns()
    {
        // Test with positions in all 12 signs
        var positions = new List<BlaPositions>
        {
            new(ChartPoints.Sun, 15.0, 1, 2, 1),      // Aries
            new(ChartPoints.Moon, 45.0, 2, 2, 2),     // Taurus
            new(ChartPoints.Mercury, 75.0, 3, 1, 3),  // Gemini
            new(ChartPoints.Venus, 105.0, 4, 1, 4),   // Cancer
            new(ChartPoints.Mars, 135.0, 5, 1, 5),    // Leo
            new(ChartPoints.Jupiter, 165.0, 6, 1, 6), // Virgo
            new(ChartPoints.Saturn, 195.0, 7, 1, 7),  // Libra
            new(ChartPoints.Uranus, 225.0, 8, 1, 8),  // Scorpio
            new(ChartPoints.Neptune, 255.0, 9, 1, 9), // Sagittarius
            new(ChartPoints.Pluto, 285.0, 10, 1, 10), // Capricorn
            new(ChartPoints.Ascendant, 315.0, 11, 1, 11), // Aquarius
            new(ChartPoints.Mc, 345.0, 12, 1, 12)     // Pisces
        };
        
        var result = SignCounts.CountPointsInSigns(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            for (int i = 1; i <= 12; i++)
            {
                Assert.That(result[i], Is.EqualTo(1), $"Sign {i} should have count 1");
            }
        });
    }

    [Test]
    public void TestCountPointsInSigns_BoundaryConditions()
    {
        // Test with positions at sign boundaries
        var positions = new List<BlaPositions>
        {
            new(ChartPoints.Sun, 0.0, 1, 1, 1),       // Start of Aries
            new(ChartPoints.Moon, 29.999, 1, 3, 1),   // End of Aries
            new(ChartPoints.Mercury, 30.0, 2, 1, 2),  // Start of Taurus
            new(ChartPoints.Venus, 59.999, 2, 3, 2),  // End of Taurus
            new(ChartPoints.Mars, 330.0, 12, 1, 12),  // Start of Pisces
            new(ChartPoints.Jupiter, 359.999, 12, 3, 12) // End of Pisces
        };
        
        var result = SignCounts.CountPointsInSigns(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            Assert.That(result[1], Is.EqualTo(2), "Aries should have count 2");
            Assert.That(result[2], Is.EqualTo(2), "Taurus should have count 2");
            Assert.That(result[12], Is.EqualTo(2), "Pisces should have count 2");
            for (int i = 3; i <= 11; i++)
            {
                Assert.That(result[i], Is.EqualTo(0), $"Sign {i} should have count 0");
            }
        });
    }

    [Test]
    public void TestCountPointsInSigns_LargeDataset()
    {
        // Test with a large dataset to ensure performance and correctness
        var positions = new List<BlaPositions>();
        var random = new Random(42); // Fixed seed for reproducible tests
        
        // Add 100 random positions
        for (int i = 0; i < 100; i++)
        {
            var sign = random.Next(1, 13); // 1-12
            var longitude = random.NextDouble() * 30.0 + (sign - 1) * 30.0;
            var decan = random.Next(1, 8); // 1-7
            var house = random.Next(1, 13); // 1-12
            var point = (ChartPoints)(i % 20 + 1); // Cycle through chart points
            
            positions.Add(new BlaPositions(point, longitude, sign, decan, house));
        }
        
        var result = SignCounts.CountPointsInSigns(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            var totalCount = result.Values.Sum();
            Assert.That(totalCount, Is.EqualTo(100), "Total count should be 100");
            
            // Verify all counts are non-negative
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value, Is.GreaterThanOrEqualTo(0), $"Sign {kvp.Key} should have non-negative count");
            }
        });
    }

    [Test]
    public void TestCountPointsInSigns_NullList()
    {
        // Test with null list - should throw ArgumentNullException
        Assert.That(() => SignCounts.CountPointsInSigns(null!), 
            Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestCountPointsInSigns_AllSameSign()
    {
        // Test with all positions in the same sign (Leo)
        var positions = new List<BlaPositions>
        {
            new(ChartPoints.Sun, 135.0, 5, 1, 1),     // Sun in Leo
            new(ChartPoints.Moon, 145.0, 5, 2, 2),    // Moon in Leo
            new(ChartPoints.Mercury, 155.0, 5, 3, 3), // Mercury in Leo
            new(ChartPoints.Venus, 165.0, 5, 1, 4),   // Venus in Leo
            new(ChartPoints.Mars, 175.0, 5, 2, 5),    // Mars in Leo
            new(ChartPoints.Jupiter, 185.0, 5, 3, 6)  // Jupiter in Leo
        };
        
        var result = SignCounts.CountPointsInSigns(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            Assert.That(result[5], Is.EqualTo(6), "Leo should have count 6");
            for (int i = 1; i <= 12; i++)
            {
                if (i != 5)
                {
                    Assert.That(result[i], Is.EqualTo(0), $"Sign {i} should have count 0");
                }
            }
        });
    }

    [Test]
    public void TestCountPointsInSigns_AlternatingSigns()
    {
        // Test with positions alternating between two signs
        var positions = new List<BlaPositions>
        {
            new(ChartPoints.Sun, 15.0, 1, 1, 1),      // Aries
            new(ChartPoints.Moon, 45.0, 2, 1, 2),     // Taurus
            new(ChartPoints.Mercury, 25.0, 1, 2, 3),  // Aries
            new(ChartPoints.Venus, 55.0, 2, 2, 4),    // Taurus
            new(ChartPoints.Mars, 5.0, 1, 3, 5),      // Aries
            new(ChartPoints.Jupiter, 35.0, 2, 3, 6)   // Taurus
        };
        
        var result = SignCounts.CountPointsInSigns(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            Assert.That(result[1], Is.EqualTo(3), "Aries should have count 3");
            Assert.That(result[2], Is.EqualTo(3), "Taurus should have count 3");
            for (int i = 3; i <= 12; i++)
            {
                Assert.That(result[i], Is.EqualTo(0), $"Sign {i} should have count 0");
            }
        });
    }

    [Test]
    public void TestCountPointsInSigns_VerifySignMapping()
    {
        // Test to verify the correct mapping of longitudes to signs
        var positions = new List<BlaPositions>
        {
            new(ChartPoints.Sun, 0.0, 1, 1, 1),       // 0° = Aries
            new(ChartPoints.Moon, 30.0, 2, 1, 2),     // 30° = Taurus
            new(ChartPoints.Mercury, 60.0, 3, 1, 3),  // 60° = Gemini
            new(ChartPoints.Venus, 90.0, 4, 1, 4),    // 90° = Cancer
            new(ChartPoints.Mars, 120.0, 5, 1, 5),    // 120° = Leo
            new(ChartPoints.Jupiter, 150.0, 6, 1, 6), // 150° = Virgo
            new(ChartPoints.Saturn, 180.0, 7, 1, 7),  // 180° = Libra
            new(ChartPoints.Uranus, 210.0, 8, 1, 8),  // 210° = Scorpio
            new(ChartPoints.Neptune, 240.0, 9, 1, 9), // 240° = Sagittarius
            new(ChartPoints.Pluto, 270.0, 10, 1, 10), // 270° = Capricorn
            new(ChartPoints.Ascendant, 300.0, 11, 1, 11), // 300° = Aquarius
            new(ChartPoints.Mc, 330.0, 12, 1, 12)     // 330° = Pisces
        };
        
        var result = SignCounts.CountPointsInSigns(positions);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            for (int i = 1; i <= 12; i++)
            {
                Assert.That(result[i], Is.EqualTo(1), $"Sign {i} should have count 1");
            }
        });
    }
}
