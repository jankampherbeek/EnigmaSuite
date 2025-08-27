// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestBlaPositionsFactory
{
    private BlaPositionsFactory _factory;

    [SetUp]
    public void Setup()
    {
        _factory = new BlaPositionsFactory();
    }

    [Test]
    public void TestCreateBlaPositions_AriesSign()
    {
        // Test Aries (0° - 29.999°)
        var result = _factory.CreateBlaPositions(ChartPoints.Sun, 0.0, 1);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result.Sign, Is.EqualTo(1)); // Aries
            Assert.That(result.Decan, Is.EqualTo(1)); // Mars decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Moon, 15.0, 2);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Moon));
            Assert.That(result.Sign, Is.EqualTo(1)); // Aries
            Assert.That(result.Decan, Is.EqualTo(2)); // Sun decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Mercury, 29.999, 3);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Mercury));
            Assert.That(result.Sign, Is.EqualTo(1)); // Aries
            Assert.That(result.Decan, Is.EqualTo(3)); // Venus decan
        });
    }

    [Test]
    public void TestCreateBlaPositions_TaurusSign()
    {
        // Test Taurus (30° - 59.999°)
        var result = _factory.CreateBlaPositions(ChartPoints.Venus, 30.0, 4);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Venus));
            Assert.That(result.Sign, Is.EqualTo(2)); // Taurus
            Assert.That(result.Decan, Is.EqualTo(4)); // Mercury decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Mars, 45.0, 5);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Mars));
            Assert.That(result.Sign, Is.EqualTo(2)); // Taurus
            Assert.That(result.Decan, Is.EqualTo(5)); // Moon decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Jupiter, 59.999, 6);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Jupiter));
            Assert.That(result.Sign, Is.EqualTo(2)); // Taurus
            Assert.That(result.Decan, Is.EqualTo(6)); // Saturn decan
        });
    }

    [Test]
    public void TestCreateBlaPositions_GeminiSign()
    {
        // Test Gemini (60° - 89.999°)
        var result = _factory.CreateBlaPositions(ChartPoints.Saturn, 60.0, 7);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Saturn));
            Assert.That(result.Sign, Is.EqualTo(3)); // Gemini
            Assert.That(result.Decan, Is.EqualTo(7)); // Jupiter decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Uranus, 75.0, 8);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Uranus));
            Assert.That(result.Sign, Is.EqualTo(3)); // Gemini
            Assert.That(result.Decan, Is.EqualTo(1)); // Mars decan (wrapped around)
        });
    }

    [Test]
    public void TestCreateBlaPositions_CancerSign()
    {
        // Test Cancer (90° - 119.999°)
        var result = _factory.CreateBlaPositions(ChartPoints.Neptune, 90.0, 9);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Neptune));
            Assert.That(result.Sign, Is.EqualTo(4)); // Cancer
            Assert.That(result.Decan, Is.EqualTo(3)); // Venus decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Pluto, 105.0, 10);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Pluto));
            Assert.That(result.Sign, Is.EqualTo(4)); // Cancer
            Assert.That(result.Decan, Is.EqualTo(4)); // Mercury decan
        });
    }

    [Test]
    public void TestCreateBlaPositions_LeoSign()
    {
        // Test Leo (120° - 149.999°)
        var result = _factory.CreateBlaPositions(ChartPoints.Sun, 120.0, 11);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result.Sign, Is.EqualTo(5)); // Leo
            Assert.That(result.Decan, Is.EqualTo(6)); // Saturn decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Moon, 135.0, 12);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Moon));
            Assert.That(result.Sign, Is.EqualTo(5)); // Leo
            Assert.That(result.Decan, Is.EqualTo(7)); // Jupiter decan
        });
    }

    [Test]
    public void TestCreateBlaPositions_VirgoSign()
    {
        // Test Virgo (150° - 179.999°)
        var result = _factory.CreateBlaPositions(ChartPoints.Mercury, 150.0, 1);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Mercury));
            Assert.That(result.Sign, Is.EqualTo(6)); // Virgo
            Assert.That(result.Decan, Is.EqualTo(2)); // Sun decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Venus, 165.0, 2);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Venus));
            Assert.That(result.Sign, Is.EqualTo(6)); // Virgo
            Assert.That(result.Decan, Is.EqualTo(3)); // Venus decan
        });
    }

    [Test]
    public void TestCreateBlaPositions_LibraSign()
    {
        // Test Libra (180° - 209.999°)
        var result = _factory.CreateBlaPositions(ChartPoints.Mars, 180.0, 3);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Mars));
            Assert.That(result.Sign, Is.EqualTo(7)); // Libra
            Assert.That(result.Decan, Is.EqualTo(5)); // Moon decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Jupiter, 195.0, 4);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Jupiter));
            Assert.That(result.Sign, Is.EqualTo(7)); // Libra
            Assert.That(result.Decan, Is.EqualTo(6)); // Saturn decan
        });
    }

    [Test]
    public void TestCreateBlaPositions_ScorpioSign()
    {
        // Test Scorpio (210° - 239.999°)
        var result = _factory.CreateBlaPositions(ChartPoints.Saturn, 210.0, 5);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Saturn));
            Assert.That(result.Sign, Is.EqualTo(8)); // Scorpio
            Assert.That(result.Decan, Is.EqualTo(1)); // Mars decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Uranus, 225.0, 6);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Uranus));
            Assert.That(result.Sign, Is.EqualTo(8)); // Scorpio
            Assert.That(result.Decan, Is.EqualTo(2)); // Sun decan
        });
    }

    [Test]
    public void TestCreateBlaPositions_SagittariusSign()
    {
        // Test Sagittarius (240° - 269.999°)
        var result = _factory.CreateBlaPositions(ChartPoints.Neptune, 240.0, 7);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Neptune));
            Assert.That(result.Sign, Is.EqualTo(9)); // Sagittarius
            Assert.That(result.Decan, Is.EqualTo(4)); // Mercury decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Pluto, 255.0, 8);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Pluto));
            Assert.That(result.Sign, Is.EqualTo(9)); // Sagittarius
            Assert.That(result.Decan, Is.EqualTo(5)); // Moon decan
        });
    }

    [Test]
    public void TestCreateBlaPositions_CapricornSign()
    {
        // Test Capricorn (270° - 299.999°)
        var result = _factory.CreateBlaPositions(ChartPoints.Sun, 270.0, 9);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result.Sign, Is.EqualTo(10)); // Capricorn
            Assert.That(result.Decan, Is.EqualTo(7)); // Jupiter decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Moon, 285.0, 10);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Moon));
            Assert.That(result.Sign, Is.EqualTo(10)); // Capricorn
            Assert.That(result.Decan, Is.EqualTo(1)); // Mars decan (wrapped around)
        });
    }

    [Test]
    public void TestCreateBlaPositions_AquariusSign()
    {
        // Test Aquarius (300° - 329.999°)
        var result = _factory.CreateBlaPositions(ChartPoints.Mercury, 300.0, 11);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Mercury));
            Assert.That(result.Sign, Is.EqualTo(11)); // Aquarius
            Assert.That(result.Decan, Is.EqualTo(3)); // Venus decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Venus, 315.0, 12);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Venus));
            Assert.That(result.Sign, Is.EqualTo(11)); // Aquarius
            Assert.That(result.Decan, Is.EqualTo(4)); // Mercury decan
        });
    }

    [Test]
    public void TestCreateBlaPositions_PiscesSign()
    {
        // Test Pisces (330° - 359.999°)
        var result = _factory.CreateBlaPositions(ChartPoints.Mars, 330.0, 1);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Mars));
            Assert.That(result.Sign, Is.EqualTo(12)); // Pisces
            Assert.That(result.Decan, Is.EqualTo(6)); // Saturn decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Jupiter, 345.0, 2);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Jupiter));
            Assert.That(result.Sign, Is.EqualTo(12)); // Pisces
            Assert.That(result.Decan, Is.EqualTo(7)); // Jupiter decan
        });

        result = _factory.CreateBlaPositions(ChartPoints.Saturn, 359.999, 3);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Saturn));
            Assert.That(result.Sign, Is.EqualTo(12)); // Pisces
            Assert.That(result.Decan, Is.EqualTo(1)); // Mars decan (wrapped around)
        });
    }


    [Test]
    public void TestCreateBlaPositions_AllChartPoints()
    {
        // Test that all chart points work correctly
        var chartPoints = new[]
        {
            ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury, ChartPoints.Venus,
            ChartPoints.Mars, ChartPoints.Jupiter, ChartPoints.Saturn, ChartPoints.Uranus,
            ChartPoints.Neptune, ChartPoints.Pluto, ChartPoints.Ascendant, ChartPoints.Mc,
            ChartPoints.Vertex, ChartPoints.EastPoint
        };

        foreach (var point in chartPoints)
        {
            var result = _factory.CreateBlaPositions(point, 45.0, 5);
            Assert.Multiple(() =>
            {
                Assert.That(result.Point, Is.EqualTo(point));
                Assert.That(result.Sign, Is.EqualTo(2)); // Taurus at 45°
                Assert.That(result.Decan, Is.EqualTo(5)); // Moon decan
            });
        }
    }

    [Test]
    public void TestCreateBlaPositions_EmptyDataSet()
    {
        // Test behavior with empty data set - relevant for adaptive DataGrid
        // This ensures the factory handles edge cases properly
        var result = _factory.CreateBlaPositions(ChartPoints.Sun, 0.0, 1);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result.Sign, Is.EqualTo(1)); // Aries
            Assert.That(result.Decan, Is.EqualTo(1)); // Mars decan
        });
    }

    [Test]
    public void TestCreateBlaPositions_MinimalDataSet()
    {
        // Test with minimal data set - one position
        // This reflects the adaptive DataGrid behavior with few rows
        var result = _factory.CreateBlaPositions(ChartPoints.Moon, 180.0, 7);
        Assert.Multiple(() =>
        {
            Assert.That(result.Point, Is.EqualTo(ChartPoints.Moon));
            Assert.That(result.Sign, Is.EqualTo(7)); // Libra
            Assert.That(result.Decan, Is.EqualTo(5)); // Moon decan
        });
    }

    [Test]
    public void TestCreateBlaPositions_LargeDataSet()
    {
        // Test with multiple positions to simulate larger DataGrid content
        // This tests the factory's ability to handle varying amounts of data
        var positions = new[]
        {
            (ChartPoints.Sun, 0.0, 1, 1, 1),    // Aries, Mars decan, House 1
            (ChartPoints.Moon, 30.0, 2, 4, 2),  // Taurus, Mercury decan, House 2
            (ChartPoints.Mercury, 60.0, 3, 7, 3), // Gemini, Jupiter decan, House 3
            (ChartPoints.Venus, 90.0, 4, 3, 4),   // Cancer, Venus decan, House 4
            (ChartPoints.Mars, 120.0, 5, 6, 5),   // Leo, Saturn decan, House 5
            (ChartPoints.Jupiter, 150.0, 6, 2, 6), // Virgo, Sun decan, House 6
            (ChartPoints.Saturn, 180.0, 7, 5, 7),  // Libra, Moon decan, House 7
            (ChartPoints.Uranus, 210.0, 8, 1, 8),  // Scorpio, Mars decan, House 8
            (ChartPoints.Neptune, 240.0, 9, 4, 9), // Sagittarius, Mercury decan, House 9
            (ChartPoints.Pluto, 270.0, 10, 7, 10)   // Capricorn, Jupiter decan, House 10
        };

        foreach (var (point, longitude, expectedSign, expectedDecan, house) in positions)
        {
            var result = _factory.CreateBlaPositions(point, longitude, house);
            Assert.Multiple(() =>
            {
                Assert.That(result.Point, Is.EqualTo(point));
                Assert.That(result.Sign, Is.EqualTo(expectedSign));
                Assert.That(result.Decan, Is.EqualTo(expectedDecan));
            });
        }
    }

    [Test]
    public void TestCreateBlaPositions_BoundaryValues()
    {
        // Test boundary values to ensure robust behavior for adaptive UI
        var boundaryTests = new[]
        {
            (0.0, 1, 1, 1),      // Start of Aries
            (29.999, 1, 3, 2),   // End of Aries
            (30.0, 2, 4, 3),     // Start of Taurus
            (59.999, 2, 6, 4),   // End of Taurus
            (330.0, 12, 6, 5),   // Start of Pisces
            (359.999, 12, 1, 6)  // End of Pisces
        };

        foreach (var (longitude, expectedSign, expectedDecan, house) in boundaryTests)
        {
            var result = _factory.CreateBlaPositions(ChartPoints.Sun, longitude, house);
            Assert.Multiple(() =>
            {
                Assert.That(result.Sign, Is.EqualTo(expectedSign));
                Assert.That(result.Decan, Is.EqualTo(expectedDecan));
            });
        }
    }

    [Test]
    public void TestCreateBlaPositions_ConsistentResults()
    {
        // Test that the factory produces consistent results for the same input
        // This is important for the adaptive DataGrid to work reliably
        var testCases = new[]
        {
            (ChartPoints.Sun, 45.0, 1),
            (ChartPoints.Moon, 135.0, 2),
            (ChartPoints.Mercury, 225.0, 3),
            (ChartPoints.Venus, 315.0, 4)
        };

        foreach (var (point, longitude, house) in testCases)
        {
            var result1 = _factory.CreateBlaPositions(point, longitude, house);
            var result2 = _factory.CreateBlaPositions(point, longitude, house);
            
            Assert.Multiple(() =>
            {
                Assert.That(result1.Point, Is.EqualTo(result2.Point));
                Assert.That(result1.Sign, Is.EqualTo(result2.Sign));
                Assert.That(result1.Decan, Is.EqualTo(result2.Decan));
            });
        }
    }
}
