// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using NUnit.Framework;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestHouseRulers
{
    private HouseRulers _houseRulers = null!;

    [SetUp]
    public void Setup()
    {
        _houseRulers = new HouseRulers();
    }

    [Test]
    public void TestDefineHouseRulers_AllSignsOnCusps()
    {
        // Create test chart where each house has a different sign on its cusp
        // No intercepted signs
        var chart = CreateTestChartWithAllSignsOnCusps();
        
        var result = _houseRulers.DefineHouseRulers(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            
            // Each house should have exactly one ruler pair (from the sign on the cusp)
            for (int i = 1; i <= 12; i++)
            {
                Assert.That(result[i], Has.Count.EqualTo(1), $"House {i} should have exactly one ruler pair");
            }
            
            // Verify specific rulers based on BlaDomain.SignRulers
            // House 1: Aries (1) -> Mars, Pluto
            Assert.That(result[1][0].Ruler, Is.EqualTo(ChartPoints.Mars));
            Assert.That(result[1][0].SubRuler, Is.EqualTo(ChartPoints.Pluto));
            
            // House 2: Taurus (2) -> Venus, PersephoneCarteret
            Assert.That(result[2][0].Ruler, Is.EqualTo(ChartPoints.Venus));
            Assert.That(result[2][0].SubRuler, Is.EqualTo(ChartPoints.PersephoneCarteret));
            
            // House 3: Gemini (3) -> Mercury, VulcanusCarteret
            Assert.That(result[3][0].Ruler, Is.EqualTo(ChartPoints.Mercury));
            Assert.That(result[3][0].SubRuler, Is.EqualTo(ChartPoints.VulcanusCarteret));
        });
    }

    [Test]
    public void TestDefineHouseRulers_WithInterceptedSigns()
    {
        // Create test chart with intercepted signs
        // Houses 6 and 12 have intercepted signs
        var chart = CreateTestChartWithInterceptedSigns();
        
        var result = _houseRulers.DefineHouseRulers(chart);
        

        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            
            // House 6: Leo (5) on cusp + Virgo (6) intercepted
            // Should have 2 ruler pairs: Leo rulers + Virgo rulers
            Assert.That(result[6], Has.Count.EqualTo(2), "House 6 should have 2 ruler pairs (Leo + Virgo)");
            
            // Verify Leo rulers (sign 5)
            var leoRulers = result[6].FirstOrDefault(r => r.Ruler == ChartPoints.Sun && r.SubRuler == ChartPoints.ApogeeMean);
            Assert.That(leoRulers, Is.Not.Null, "House 6 should have Leo rulers");
            
            // Verify Virgo rulers (sign 6)
            var virgoRulers = result[6].FirstOrDefault(r => r.Ruler == ChartPoints.Mercury && r.SubRuler == ChartPoints.VulcanusCarteret);
            Assert.That(virgoRulers, Is.Not.Null, "House 6 should have Virgo rulers");
            
            // House 11: Aquarius (11) on cusp + Pisces (12) intercepted
            // Should have 2 ruler pairs: Aquarius rulers + Pisces rulers
            Assert.That(result[11], Has.Count.EqualTo(2), "House 11 should have 2 ruler pairs (Aquarius + Pisces)");
            
            // Verify Aquarius rulers (sign 11)
            var aquariusRulers = result[11].FirstOrDefault(r => r.Ruler == ChartPoints.Moon && r.SubRuler == ChartPoints.Priapus);
            Assert.That(aquariusRulers, Is.Not.Null, "House 11 should have Aquarius rulers");
            
            // Verify Pisces rulers (sign 12)
            var piscesRulers = result[11].FirstOrDefault(r => r.Ruler == ChartPoints.Jupiter && r.SubRuler == ChartPoints.Neptune);
            Assert.That(piscesRulers, Is.Not.Null, "House 11 should have Pisces rulers");
        });
    }

    [Test]
    public void TestDefineHouseRulers_DebugInterceptedSigns()
    {
        // Create test chart with intercepted signs
        var chart = CreateTestChartWithInterceptedSigns();
        
        var result = _houseRulers.DefineHouseRulers(chart);
        
        // Just assert that we have some rulers
        Assert.That(result[6].Count, Is.GreaterThan(0));
        Assert.That(result[12].Count, Is.GreaterThan(0));
    }

    [Test]
    public void TestDefineHouseRulers_EmptyChart()
    {
        // Test with empty chart
        var chart = CreateEmptyChart();
        
        var result = _houseRulers.DefineHouseRulers(chart);
        
        Assert.That(result, Has.Count.EqualTo(12));
        
        // All houses should have empty ruler lists
        for (int i = 1; i <= 12; i++)
        {
            Assert.That(result[i], Is.Empty, $"House {i} should have no rulers when no cusps exist");
        }
    }

    [Test]
    public void TestDefineHouseRulers_ClampedHouses()
    {
        // Create test chart with clamped houses (same sign on consecutive cusps)
        var chart = CreateTestChartWithClampedHouses();
        
        var result = _houseRulers.DefineHouseRulers(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            
            // House 3: Gemini (3) on both Cusp3 and Cusp4
            // Should have only Gemini rulers (no intercepted signs)
            Assert.That(result[3], Has.Count.EqualTo(1), "House 3 should have only Gemini rulers");
            Assert.That(result[3][0].Ruler, Is.EqualTo(ChartPoints.Mercury));
            Assert.That(result[3][0].SubRuler, Is.EqualTo(ChartPoints.VulcanusCarteret));
            
            // House 7: Libra (7) on both Cusp7 and Cusp8
            // Should have only Libra rulers (no intercepted signs)
            Assert.That(result[7], Has.Count.EqualTo(1), "House 7 should have only Libra rulers");
            Assert.That(result[7][0].Ruler, Is.EqualTo(ChartPoints.Venus));
            Assert.That(result[7][0].SubRuler, Is.EqualTo(ChartPoints.PersephoneCarteret));
        });
    }

    [Test]
    public void TestDefineHouseRulers_OverflowBoundary()
    {
        // Test with chart where house 12 is clamped (Pisces continues from cusp 12 to cusp 1)
        var chart = CreateTestChartWithOverflowClampedHouse();
        
        var result = _houseRulers.DefineHouseRulers(chart);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(12));
            
            // House 12: Pisces (12) on both Cusp12 and Cusp1
            // Should have only Pisces rulers (no intercepted signs)
            Assert.That(result[12], Has.Count.EqualTo(1), "House 12 should have only Pisces rulers");
            Assert.That(result[12][0].Ruler, Is.EqualTo(ChartPoints.Jupiter));
            Assert.That(result[12][0].SubRuler, Is.EqualTo(ChartPoints.Neptune));
        });
    }

    private static CalculatedChart CreateTestChartWithAllSignsOnCusps()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
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
            { ChartPoints.Cusp12, CreateFullPointPos(345.0) }  // Pisces (12)
        };
        
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateTestChartWithInterceptedSigns()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>
        {
            // Realistic house cusps where Virgo (6) and Pisces (12) are intercepted
            // House 6: Leo (5) on cusp, Virgo (6) is intercepted
            // House 12: Aquarius (11) on cusp, Pisces (12) is intercepted
            { ChartPoints.Cusp1, CreateFullPointPos(15.0) },   // Aries (1)
            { ChartPoints.Cusp2, CreateFullPointPos(45.0) },   // Taurus (2)
            { ChartPoints.Cusp3, CreateFullPointPos(75.0) },   // Gemini (3)
            { ChartPoints.Cusp4, CreateFullPointPos(105.0) },  // Cancer (4)
            { ChartPoints.Cusp5, CreateFullPointPos(135.0) },  // Leo (5)
            { ChartPoints.Cusp6, CreateFullPointPos(145.0) },  // Leo (5) - House 6 starts with Leo
            { ChartPoints.Cusp7, CreateFullPointPos(205.0) },  // Libra (7) - House 6 ends with Libra, so Virgo (6) is intercepted
            { ChartPoints.Cusp8, CreateFullPointPos(245.0) },  // Sagittarius (9)
            { ChartPoints.Cusp9, CreateFullPointPos(275.0) },  // Capricorn (10)
            { ChartPoints.Cusp10, CreateFullPointPos(305.0) }, // Aquarius (11)
            { ChartPoints.Cusp11, CreateFullPointPos(325.0) }, // Aquarius (11) - House 11 starts with Aquarius
            { ChartPoints.Cusp12, CreateFullPointPos(25.0) }   // Aries (1) - House 12 starts with Aries, ends with Aries, so Pisces (12) is intercepted
        };
        
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateTestChartWithClampedHouses()
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
            { ChartPoints.Cusp12, CreateFullPointPos(345.0) }  // Pisces (12)
        };
        
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
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
            { ChartPoints.Cusp12, CreateFullPointPos(345.0) }  // Pisces (12)
        };
        
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static CalculatedChart CreateEmptyChart()
    {
        var positions = new Dictionary<ChartPoints, FullPointPos>();
        return new CalculatedChart(positions, CreateTestChartData(), 23.44);
    }

    private static ChartData CreateTestChartData()
    {
        // Create minimal test chart data
        var location = new Location("Test", 0.0, 0.0);
        var metaData = new MetaData("Test Chart", "", "", "", 1, 1);
        var fullDateTime = new FullDateTime("2025/01/01", "12.00", 2460100.0);
        return new ChartData(1, metaData, location, fullDateTime);
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
}
