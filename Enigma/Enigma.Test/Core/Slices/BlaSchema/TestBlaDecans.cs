// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestBlaDecans
{

    [Test]
    public void TestDefineDecans_BasicCalculation()
    {
        var longitudes = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 5.0 },    // Should be decan 1 (0-9.99°)
            { ChartPoints.Moon, 15.0 },  // Should be decan 2 (10-19.99°)
            { ChartPoints.Mercury, 25.0 }, // Should be decan 3 (20-29.99°)
            { ChartPoints.Venus, 35.0 },   // Should be decan 4 (30-39.99°)
            { ChartPoints.Mars, 45.0 },    // Should be decan 5 (40-49.99°)
            { ChartPoints.Jupiter, 55.0 }, // Should be decan 6 (50-59.99°)
            { ChartPoints.Saturn, 65.0 }   // Should be decan 7 (60-69.99°)
        };

        var result = BlaDecans.DefineDecans(longitudes);

        Assert.Multiple(() =>
        {
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(1));
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(2));
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(3));
            Assert.That(result[ChartPoints.Venus], Is.EqualTo(4));
            Assert.That(result[ChartPoints.Mars], Is.EqualTo(5));
            Assert.That(result[ChartPoints.Jupiter], Is.EqualTo(6));
            Assert.That(result[ChartPoints.Saturn], Is.EqualTo(7));
        });
    }

    [Test]
    public void TestDefineDecans_BoundaryValues()
    {
        var longitudes = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 0.0 },     // Should be decan 1
            { ChartPoints.Moon, 9.99 },   // Should be decan 1
            { ChartPoints.Mercury, 10.0 }, // Should be decan 2
            { ChartPoints.Venus, 19.99 },  // Should be decan 2
            { ChartPoints.Mars, 20.0 },    // Should be decan 3
            { ChartPoints.Jupiter, 29.99 }, // Should be decan 3
            { ChartPoints.Saturn, 30.0 },   // Should be decan 4
            { ChartPoints.Uranus, 39.99 },  // Should be decan 4
            { ChartPoints.Neptune, 40.0 },  // Should be decan 5
            { ChartPoints.Pluto, 49.99 },   // Should be decan 5
            { ChartPoints.VulcanusCarteret, 50.0 }, // Should be decan 6
            { ChartPoints.PersephoneCarteret, 59.99 }, // Should be decan 6
            { ChartPoints.FortunaNoSect, 60.0 }, // Should be decan 7
            { ChartPoints.NorthNode, 69.99 } // Should be decan 7
        };

        var result = BlaDecans.DefineDecans(longitudes);

        Assert.Multiple(() =>
        {
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(1));
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(1));
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(2));
            Assert.That(result[ChartPoints.Venus], Is.EqualTo(2));
            Assert.That(result[ChartPoints.Mars], Is.EqualTo(3));
            Assert.That(result[ChartPoints.Jupiter], Is.EqualTo(3));
            Assert.That(result[ChartPoints.Saturn], Is.EqualTo(4));
            Assert.That(result[ChartPoints.Uranus], Is.EqualTo(4));
            Assert.That(result[ChartPoints.Neptune], Is.EqualTo(5));
            Assert.That(result[ChartPoints.Pluto], Is.EqualTo(5));
            Assert.That(result[ChartPoints.VulcanusCarteret], Is.EqualTo(6));
            Assert.That(result[ChartPoints.PersephoneCarteret], Is.EqualTo(6));
            Assert.That(result[ChartPoints.FortunaNoSect], Is.EqualTo(7));
            Assert.That(result[ChartPoints.NorthNode], Is.EqualTo(7));
        });
    }

    [Test]
    public void TestDefineDecans_WrappingFrom7To1()
    {
        var longitudes = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 70.0 },    // Should be decan 1 (70/10 + 1 = 8, then 8-7 = 1)
            { ChartPoints.Moon, 80.0 },   // Should be decan 2 (80/10 + 1 = 9, then 9-7 = 2)
            { ChartPoints.Mercury, 90.0 }, // Should be decan 3 (90/10 + 1 = 10, then 10-7 = 3)
            { ChartPoints.Venus, 100.0 },  // Should be decan 4 (100/10 + 1 = 11, then 11-7 = 4)
            { ChartPoints.Mars, 110.0 },   // Should be decan 5 (110/10 + 1 = 12, then 12-7 = 5)
            { ChartPoints.Jupiter, 120.0 }, // Should be decan 6 (120/10 + 1 = 13, then 13-7 = 6)
            { ChartPoints.Saturn, 130.0 },  // Should be decan 7 (130/10 + 1 = 14, then 14-7 = 7)
            { ChartPoints.Uranus, 140.0 }   // Should be decan 1 (140/10 + 1 = 15, then 15-7 = 8, then 8-7 = 1)
        };

        var result = BlaDecans.DefineDecans(longitudes);

        Assert.Multiple(() =>
        {
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(1));
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(2));
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(3));
            Assert.That(result[ChartPoints.Venus], Is.EqualTo(4));
            Assert.That(result[ChartPoints.Mars], Is.EqualTo(5));
            Assert.That(result[ChartPoints.Jupiter], Is.EqualTo(6));
            Assert.That(result[ChartPoints.Saturn], Is.EqualTo(7));
            Assert.That(result[ChartPoints.Uranus], Is.EqualTo(1));
        });
    }

    [Test]
    public void TestDefineDecans_LargeValues()
    {
        var longitudes = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 350.0 },   // Should be decan 1 (350/10 + 1 = 36, then 36-7*5 = 1)
            { ChartPoints.Moon, 360.0 },  // Should be decan 1 (360/10 + 1 = 37, then 37-7*5 = 2, then 2-7 = -5, then -5+7 = 2)
            { ChartPoints.Mercury, 720.0 }, // Should be decan 1 (720/10 + 1 = 73, then 73-7*10 = 3)
            { ChartPoints.Venus, 1000.0 }   // Should be decan 1 (1000/10 + 1 = 101, then 101-7*14 = 3)
        };

        var result = BlaDecans.DefineDecans(longitudes);

        Assert.Multiple(() =>
        {
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(1));
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(2));
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(3));
            Assert.That(result[ChartPoints.Venus], Is.EqualTo(3));
        });
    }

    [Test]
    public void TestDefineDecans_NegativeValues()
    {
        var longitudes = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, -5.0 },    // truncate(-0.5) + 1 = 0 + 1 = 1
            { ChartPoints.Moon, -15.0 },  // truncate(-1.5) + 1 = -1 + 1 = 0
            { ChartPoints.Mercury, -25.0 } // truncate(-2.5) + 1 = -2 + 1 = -1
        };

        var result = BlaDecans.DefineDecans(longitudes);

        Assert.Multiple(() =>
        {
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(1));
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(0));
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(-1));
        });
    }

    [Test]
    public void TestDefineDecans_EmptyInput()
    {
        var longitudes = new Dictionary<ChartPoints, double>();

        var result = BlaDecans.DefineDecans(longitudes);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(0));
    }

    [Test]
    public void TestDefineDecans_SinglePoint()
    {
        var longitudes = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 25.5 }
        };

        var result = BlaDecans.DefineDecans(longitudes);

        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(3)); // 25.5/10 = 2.55, truncate = 2, +1 = 3
        });
    }

    [Test]
    public void TestDefineDecans_RealWorldExample()
    {
        // Test with realistic chart data similar to what's used in other tests
        var longitudes = new Dictionary<ChartPoints, double>
        {
            { ChartPoints.Sun, 309.1 },      // Should be decan 1 (309.1/10 + 1 = 31.91, then 31-7*4 = 3)
            { ChartPoints.Moon, 121.75 },    // Should be decan 3 (121.75/10 + 1 = 13.175, then 13-7 = 6)
            { ChartPoints.Mercury, 305.9 },  // Should be decan 1 (305.9/10 + 1 = 31.59, then 31-7*4 = 3)
            { ChartPoints.Venus, 356.0 },    // Should be decan 1 (356.0/10 + 1 = 36.6, then 36-7*5 = 1)
            { ChartPoints.Mars, 352.6 },     // Should be decan 1 (352.6/10 + 1 = 36.26, then 36-7*5 = 1)
            { ChartPoints.Jupiter, 31.95 },  // Should be decan 4 (31.95/10 + 1 = 4.195, then 4)
            { ChartPoints.Saturn, 207.25 },  // Should be decan 1 (207.25/10 + 1 = 21.725, then 21-7*3 = 0, then 0+7 = 7)
            { ChartPoints.Uranus, 105.55 },  // Should be decan 1 (105.55/10 + 1 = 11.555, then 11-7 = 4)
            { ChartPoints.Neptune, 203.85 }, // Should be decan 1 (203.85/10 + 1 = 21.385, then 21-7*3 = 0, then 0+7 = 7)
            { ChartPoints.Pluto, 142.3 }     // Should be decan 3 (142.3/10 + 1 = 15.23, then 15-7*2 = 1)
        };

        var result = BlaDecans.DefineDecans(longitudes);

        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(10));
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(3));
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(6));
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(3));
            Assert.That(result[ChartPoints.Venus], Is.EqualTo(1));
            Assert.That(result[ChartPoints.Mars], Is.EqualTo(1));
            Assert.That(result[ChartPoints.Jupiter], Is.EqualTo(4));
            Assert.That(result[ChartPoints.Saturn], Is.EqualTo(7));
            Assert.That(result[ChartPoints.Uranus], Is.EqualTo(4));
            Assert.That(result[ChartPoints.Neptune], Is.EqualTo(7));
            Assert.That(result[ChartPoints.Pluto], Is.EqualTo(1));
        });
    }
}