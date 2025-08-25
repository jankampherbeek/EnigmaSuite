// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestInterceptedClamped
{
    private InterceptedClamped _interceptedClamped;

    [SetUp]
    public void Setup()
    {
        _interceptedClamped = new InterceptedClamped();
    }

    [Test]
    public void TestDefineInterceptedSigns_NoInterceptedSigns()
    {
        // Create test chart where all signs appear on cusps
        // Each cusp has a different sign
        var chart = CreateTestChartWithAllSignsOnCusps();
        
        var result = _interceptedClamped.DefineInterceptedSigns(chart);
        
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestDefineInterceptedSigns_SomeInterceptedSigns()
    {
        // Create test chart where some signs are missing from cusps
        // Signs 3, 7, and 11 are not on any cusp
        var chart = CreateTestChartWithSomeInterceptedSigns();
        
        var result = _interceptedClamped.DefineInterceptedSigns(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result, Contains.Item(3)); // Gemini
            Assert.That(result, Contains.Item(7)); // Libra
            Assert.That(result, Contains.Item(11)); // Aquarius
        });
    }

    [Test]
    public void TestDefineInterceptedSigns_AllSignsIntercepted()
    {
        // Create test chart where only one sign appears on all cusps
        // All other signs are intercepted
        var chart = CreateTestChartWithAllSignsIntercepted();
        
        var result = _interceptedClamped.DefineInterceptedSigns(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(11)); // All signs except the one on cusps
            Assert.That(result, Does.Not.Contain(1)); // Aries is on all cusps
            Assert.That(result, Contains.Item(2)); // Taurus is intercepted
            Assert.That(result, Contains.Item(3)); // Gemini is intercepted
            Assert.That(result, Contains.Item(4)); // Cancer is intercepted
            Assert.That(result, Contains.Item(5)); // Leo is intercepted
            Assert.That(result, Contains.Item(6)); // Virgo is intercepted
            Assert.That(result, Contains.Item(7)); // Libra is intercepted
            Assert.That(result, Contains.Item(8)); // Scorpio is intercepted
            Assert.That(result, Contains.Item(9)); // Sagittarius is intercepted
            Assert.That(result, Contains.Item(10)); // Capricorn is intercepted
            Assert.That(result, Contains.Item(11)); // Aquarius is intercepted
            Assert.That(result, Contains.Item(12)); // Pisces is intercepted
        });
    }

    [Test]
    public void TestDefineInterceptedSigns_RealisticChart()
    {
        // Create test chart with realistic house cusps where some signs are intercepted
        var chart = CreateRealisticChartWithInterceptedSigns();
        
        var result = _interceptedClamped.DefineInterceptedSigns(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(2)); // Virgo (6) and Pisces (12) are intercepted
            Assert.That(result, Does.Contain(6)); // Virgo is intercepted
            Assert.That(result, Does.Contain(12)); // Pisces is intercepted
        });
    }

    [Test]
    public void TestDefineInterceptedSigns_EmptyChart()
    {
        // Test with empty chart
        var chart = CreateEmptyChart();
        
        var result = _interceptedClamped.DefineInterceptedSigns(chart);
        
        Assert.That(result, Has.Count.EqualTo(12)); // All signs are intercepted when no cusps exist
    }

    [Test]
    public void TestDefineInterceptedSigns_AllSignsOnCusps()
    {
        // Test where all signs appear on cusps (no intercepted signs)
        var chart = CreateTestChartWithAllSignsOnCusps();
        
        var result = _interceptedClamped.DefineInterceptedSigns(chart);
        
        Assert.That(result, Has.Count.EqualTo(0)); // No signs are intercepted
    }

    [Test]
    public void TestDefineClampedHouses_NoClampedHouses()
    {
        // Create test chart where each house has a different sign on its cusp
        // No houses are clamped
        var chart = CreateTestChartWithAllSignsOnCusps();
        
        var result = _interceptedClamped.DefineClampedHouses(chart);
        
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestDefineClampedHouses_SomeClampedHouses()
    {
        // Create test chart where some houses are clamped
        // Houses 2, 5, and 8 are clamped (same sign as next cusp)
        var chart = CreateTestChartWithSomeClampedHouses();
        
        var result = _interceptedClamped.DefineClampedHouses(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result, Contains.Item(2)); // House 2 is clamped
            Assert.That(result, Contains.Item(5)); // House 5 is clamped
            Assert.That(result, Contains.Item(8)); // House 8 is clamped
        });
    }

    [Test]
    public void TestDefineClampedHouses_AllHousesClamped()
    {
        // Create test chart where all houses are clamped
        // All cusps have the same sign
        var chart = CreateTestChartWithAllSignsIntercepted();
        
        var result = _interceptedClamped.DefineClampedHouses(chart);
        
        Assert.That(result, Has.Count.EqualTo(12)); // All houses are clamped
    }

    [Test]
    public void TestDefineClampedHouses_RealisticChart()
    {
        // Create test chart with realistic house cusps where some houses are clamped
        var chart = CreateRealisticChartWithClampedHouses();
        
        var result = _interceptedClamped.DefineClampedHouses(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(2)); // Houses 3 and 7 are clamped
            Assert.That(result, Contains.Item(3)); // House 3 is clamped (Gemini on both cusps)
            Assert.That(result, Contains.Item(7)); // House 7 is clamped (Libra on both cusps)
        });
    }

    [Test]
    public void TestDefineClampedHouses_EmptyChart()
    {
        // Test with empty chart
        var chart = CreateEmptyChart();
        
        var result = _interceptedClamped.DefineClampedHouses(chart);
        
        Assert.That(result, Is.Empty); // No houses are clamped when no cusps exist
    }

    [Test]
    public void TestDefineClampedHouses_OverflowBoundary()
    {
        // Test with chart where house 12 is clamped (sign continues from cusp 12 to cusp 1)
        var chart = CreateTestChartWithOverflowClampedHouse();
        
        var result = _interceptedClamped.DefineClampedHouses(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result, Contains.Item(12)); // House 12 is clamped (Pisces continues to cusp 1)
        });
    }

    private static CalculatedChart CreateTestChartWithAllSignsOnCusps()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Create a chart where all 12 signs appear on cusps
            { ChartPoints.Cusp1, CreateFullPointPos(15.0) },   // Aries (1)
            { ChartPoints.Cusp2, CreateFullPointPos(45.0) },   // Taurus (2)
            { ChartPoints.Cusp3, CreateFullPointPos(75.0) },   // Gemini (3)
            { ChartPoints.Cusp4, CreateFullPointPos(105.0) },  // Cancer (4)
            { ChartPoints.Cusp5, CreateFullPointPos(135.0) },  // Leo (5)
            { ChartPoints.Cusp6, CreateFullPointPos(165.0) },  // Virgo (6)
            { ChartPoints.Cusp7, CreateFullPointPos(195.0) },  // Libra (7)
            { ChartPoints.Cusp8, CreateFullPointPos(225.0) },  // Scorpio (8)
            { ChartPoints.Cusp9, CreateFullPointPos(255.0) },  // Sagittarius (9)
            { ChartPoints.Cusp10, CreateFullPointPos(285.0) }, // Capricorn (10)
            { ChartPoints.Cusp11, CreateFullPointPos(315.0) }, // Aquarius (11)
            { ChartPoints.Cusp12, CreateFullPointPos(345.0) }, // Pisces (12)
        };
        
        var chartData = CreateTestChartData();
        return new CalculatedChart(positions, chartData, 23.44);
    }

    private static CalculatedChart CreateTestChartWithSomeInterceptedSigns()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Only 9 cusps with specific signs, leaving 3 signs intercepted
            { ChartPoints.Cusp1, CreateFullPointPos(0.0) },    // Aries (1)
            { ChartPoints.Cusp2, CreateFullPointPos(30.0) },   // Taurus (2)
            { ChartPoints.Cusp3, CreateFullPointPos(90.0) },   // Cancer (4) - Note: Gemini (3) is missing
            { ChartPoints.Cusp4, CreateFullPointPos(120.0) },  // Leo (5)
            { ChartPoints.Cusp5, CreateFullPointPos(150.0) },  // Virgo (6)
            { ChartPoints.Cusp6, CreateFullPointPos(210.0) },  // Scorpio (8) - Note: Libra (7) is missing
            { ChartPoints.Cusp7, CreateFullPointPos(240.0) },  // Sagittarius (9)
            { ChartPoints.Cusp8, CreateFullPointPos(270.0) },  // Capricorn (10)
            { ChartPoints.Cusp9, CreateFullPointPos(330.0) }   // Pisces (12) - Note: Aquarius (11) is missing
        };
        
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateTestChartWithAllSignsIntercepted()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // All cusps have the same sign (Aries = 1)
            { ChartPoints.Cusp1, CreateFullPointPos(0.0) },    // Aries (1)
            { ChartPoints.Cusp2, CreateFullPointPos(5.0) },    // Aries (1)
            { ChartPoints.Cusp3, CreateFullPointPos(10.0) },   // Aries (1)
            { ChartPoints.Cusp4, CreateFullPointPos(15.0) },   // Aries (1)
            { ChartPoints.Cusp5, CreateFullPointPos(20.0) },   // Aries (1)
            { ChartPoints.Cusp6, CreateFullPointPos(25.0) },   // Aries (1)
            { ChartPoints.Cusp7, CreateFullPointPos(2.0) },    // Aries (1)
            { ChartPoints.Cusp8, CreateFullPointPos(7.0) },    // Aries (1)
            { ChartPoints.Cusp9, CreateFullPointPos(12.0) },   // Aries (1)
            { ChartPoints.Cusp10, CreateFullPointPos(17.0) },  // Aries (1)
            { ChartPoints.Cusp11, CreateFullPointPos(22.0) },  // Aries (1)
            { ChartPoints.Cusp12, CreateFullPointPos(27.0) }   // Aries (1)
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
            // Only celestial bodies, no cusps
            { ChartPoints.Sun, CreateFullPointPos(30.0) },
            { ChartPoints.Moon, CreateFullPointPos(150.0) },
            { ChartPoints.Mercury, CreateFullPointPos(240.0) }
        };
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateRealisticChartWithInterceptedSigns()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Realistic house cusps where Virgo (6) and Pisces (12) are intercepted
            // No cusps fall in Virgo (150-180°) or Pisces (330-360°)
            { ChartPoints.Cusp1, CreateFullPointPos(15.0) },   // Aries (1)
            { ChartPoints.Cusp2, CreateFullPointPos(45.0) },   // Taurus (2)
            { ChartPoints.Cusp3, CreateFullPointPos(75.0) },   // Gemini (3)
            { ChartPoints.Cusp4, CreateFullPointPos(105.0) },  // Cancer (4)
            { ChartPoints.Cusp5, CreateFullPointPos(135.0) },  // Leo (5)
            { ChartPoints.Cusp6, CreateFullPointPos(185.0) },  // Libra (7) - Note: Virgo (6) is intercepted
            { ChartPoints.Cusp7, CreateFullPointPos(215.0) },  // Scorpio (8)
            { ChartPoints.Cusp8, CreateFullPointPos(245.0) },  // Sagittarius (9)
            { ChartPoints.Cusp9, CreateFullPointPos(275.0) },  // Capricorn (10)
            { ChartPoints.Cusp10, CreateFullPointPos(305.0) }, // Aquarius (11)
            { ChartPoints.Cusp11, CreateFullPointPos(325.0) }, // Aquarius (11) - Note: Pisces (12) is intercepted
            { ChartPoints.Cusp12, CreateFullPointPos(25.0) },   // Aries (1) - Note: Pisces (12) is intercepted
        };
        
        var chartData = CreateTestChartData();
        return new CalculatedChart(positions, chartData, 23.44);
    }

    private static CalculatedChart CreateRealisticChartWithClampedHouses()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Realistic house cusps where Houses 3 and 7 are clamped
            // House 3: Gemini (3) on both Cusp3 and Cusp4
            // House 7: Libra (7) on both Cusp7 and Cusp8
            { ChartPoints.Cusp1, CreateFullPointPos(15.0) },   // Aries (1)
            { ChartPoints.Cusp2, CreateFullPointPos(45.0) },   // Taurus (2)
            { ChartPoints.Cusp3, CreateFullPointPos(75.0) },   // Gemini (3)
            { ChartPoints.Cusp4, CreateFullPointPos(85.0) },   // Gemini (3) - House 3 is clamped
            { ChartPoints.Cusp5, CreateFullPointPos(135.0) },  // Leo (5)
            { ChartPoints.Cusp6, CreateFullPointPos(165.0) },  // Virgo (6)
            { ChartPoints.Cusp7, CreateFullPointPos(195.0) },  // Libra (7)
            { ChartPoints.Cusp8, CreateFullPointPos(205.0) },  // Libra (7) - House 7 is clamped
            { ChartPoints.Cusp9, CreateFullPointPos(255.0) },  // Sagittarius (9)
            { ChartPoints.Cusp10, CreateFullPointPos(285.0) }, // Capricorn (10)
            { ChartPoints.Cusp11, CreateFullPointPos(315.0) }, // Aquarius (11)
            { ChartPoints.Cusp12, CreateFullPointPos(345.0) }, // Pisces (12)
        };
        
        var chartData = CreateTestChartData();
        return new CalculatedChart(positions, chartData, 23.44);
    }

    private static CalculatedChart CreateTestChartWithOverflowClampedHouse()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Chart where house 12 is clamped (Pisces continues from cusp 12 to cusp 1)
            { ChartPoints.Cusp1, CreateFullPointPos(355.0) },  // Pisces (12) - House 12 is clamped
            { ChartPoints.Cusp2, CreateFullPointPos(45.0) },   // Taurus (2)
            { ChartPoints.Cusp3, CreateFullPointPos(75.0) },   // Gemini (3)
            { ChartPoints.Cusp4, CreateFullPointPos(105.0) },  // Cancer (4)
            { ChartPoints.Cusp5, CreateFullPointPos(135.0) },  // Leo (5)
            { ChartPoints.Cusp6, CreateFullPointPos(165.0) },  // Virgo (6)
            { ChartPoints.Cusp7, CreateFullPointPos(195.0) },  // Libra (7)
            { ChartPoints.Cusp8, CreateFullPointPos(225.0) },  // Scorpio (8)
            { ChartPoints.Cusp9, CreateFullPointPos(255.0) },  // Sagittarius (9)
            { ChartPoints.Cusp10, CreateFullPointPos(285.0) }, // Capricorn (10)
            { ChartPoints.Cusp11, CreateFullPointPos(315.0) }, // Aquarius (11)
            { ChartPoints.Cusp12, CreateFullPointPos(345.0) }, // Pisces (12)
        };
        
        var chartData = CreateTestChartData();
        return new CalculatedChart(positions, chartData, 23.44);
    }

    private static CalculatedChart CreateTestChartWithSomeClampedHouses()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Chart where houses 2, 5, and 8 are clamped
            // House 2: Taurus (2) on both Cusp2 and Cusp3
            // House 5: Leo (5) on both Cusp5 and Cusp6  
            // House 8: Scorpio (8) on both Cusp8 and Cusp9
            { ChartPoints.Cusp1, CreateFullPointPos(15.0) },   // Aries (1)
            { ChartPoints.Cusp2, CreateFullPointPos(45.0) },   // Taurus (2)
            { ChartPoints.Cusp3, CreateFullPointPos(55.0) },   // Taurus (2) - House 2 is clamped
            { ChartPoints.Cusp4, CreateFullPointPos(105.0) },  // Cancer (4)
            { ChartPoints.Cusp5, CreateFullPointPos(135.0) },  // Leo (5)
            { ChartPoints.Cusp6, CreateFullPointPos(145.0) },  // Leo (5) - House 5 is clamped
            { ChartPoints.Cusp7, CreateFullPointPos(195.0) },  // Libra (7)
            { ChartPoints.Cusp8, CreateFullPointPos(225.0) },  // Scorpio (8)
            { ChartPoints.Cusp9, CreateFullPointPos(235.0) },  // Scorpio (8) - House 8 is clamped
            { ChartPoints.Cusp10, CreateFullPointPos(285.0) }, // Capricorn (10)
            { ChartPoints.Cusp11, CreateFullPointPos(315.0) }, // Aquarius (11)
            { ChartPoints.Cusp12, CreateFullPointPos(345.0) }, // Pisces (12)
        };
        
        var chartData = CreateTestChartData();
        return new CalculatedChart(positions, chartData, 23.44);
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
