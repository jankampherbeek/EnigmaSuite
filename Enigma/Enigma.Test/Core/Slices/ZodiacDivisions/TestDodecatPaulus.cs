// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.ZodiacDivisions;

namespace Enigma.Test.Core.Slices.ZodiacDivisions;

[TestFixture]
public class TestDodecatPaulus
{
    [Test]
    public void TestInvalidLongitudes()
    {
        Assert.Multiple(() =>
        {
            Assert.That(DodecatPaulus.IndexForDodecat(-0.1), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(DodecatPaulus.IndexForDodecat(-1.0), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(DodecatPaulus.IndexForDodecat(-360.0), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(DodecatPaulus.IndexForDodecat(360.0), Is.EqualTo(-1), "360° should return -1");
            Assert.That(DodecatPaulus.IndexForDodecat(360.1), Is.EqualTo(-1), "Longitude > 360° should return -1");
            Assert.That(DodecatPaulus.IndexForDodecat(720.0), Is.EqualTo(-1), "Longitude > 360° should return -1");
        });
    }

    [Test]
    public void TestZeroLongitude()
    {
        // 0° × 13 = 0 → 0 ÷ 30 = 0 (Aries)
        Assert.That(DodecatPaulus.IndexForDodecat(0.0), Is.EqualTo(0), "0° should return 0 (Aries)");
    }

    [Test]
    public void TestAriesBoundaries()
    {
        Assert.Multiple(() =>
        {
            // Aries (0-29.999°): 13th harmonic calculations
            // 0° × 13 = 0 → 0 ÷ 30 = 0
            Assert.That(DodecatPaulus.IndexForDodecat(0.0), Is.EqualTo(0), "0° should return 0");
            
            // 1° × 13 = 13 → 13 ÷ 30 = 0
            Assert.That(DodecatPaulus.IndexForDodecat(1.0), Is.EqualTo(0), "1° should return 0");
            
            // 2° × 13 = 26 → 26 ÷ 30 = 0
            Assert.That(DodecatPaulus.IndexForDodecat(2.0), Is.EqualTo(0), "2° should return 0");
            
            // 23° × 13 = 299 → 299 ÷ 30 = 9
            Assert.That(DodecatPaulus.IndexForDodecat(23.0), Is.EqualTo(9), "23° should return 9");
            
            // 29.999° × 13 = 389.987 → 389.987 - 360 = 29.987 → 29.987 ÷ 30 = 0
            Assert.That(DodecatPaulus.IndexForDodecat(29.999), Is.EqualTo(0), "29.999° should return 0");
        });
    }

    [Test]
    public void TestTaurusBoundaries()
    {
        Assert.Multiple(() =>
        {
            // Taurus (30-59.999°): 13th harmonic calculations
            // 30° × 13 = 390 → 390 - 360 = 30 → 30 ÷ 30 = 1
            Assert.That(DodecatPaulus.IndexForDodecat(30.0), Is.EqualTo(1), "30° should return 1");
            
            // 31° × 13 = 403 → 403 - 360 = 43 → 43 ÷ 30 = 1
            Assert.That(DodecatPaulus.IndexForDodecat(31.0), Is.EqualTo(1), "31° should return 1");
            
            // 45° × 13 = 585 → 585 - 360 = 225 → 225 ÷ 30 = 7
            Assert.That(DodecatPaulus.IndexForDodecat(45.0), Is.EqualTo(7), "45° should return 7");
            
            // 59.999° × 13 = 779.987 → 779.987 - 360 = 419.987 → 419.987 - 360 = 59.987 → 59.987 ÷ 30 = 1
            Assert.That(DodecatPaulus.IndexForDodecat(59.999), Is.EqualTo(1), "59.999° should return 1");
        });
    }

    [Test]
    public void TestGeminiBoundaries()
    {
        Assert.Multiple(() =>
        {
            // Gemini (60-89.999°): 13th harmonic calculations
            // 60° × 13 = 780 → 780 - 360 = 420 → 420 - 360 = 60 → 60 ÷ 30 = 2
            Assert.That(DodecatPaulus.IndexForDodecat(60.0), Is.EqualTo(2), "60° should return 2");
            
            // 75° × 13 = 975 → 975 - 360 = 615 → 615 - 360 = 255 → 255 ÷ 30 = 8
            Assert.That(DodecatPaulus.IndexForDodecat(75.0), Is.EqualTo(8), "75° should return 8");
            
            // 89.999° × 13 = 1169.987 → 1169.987 - 360 = 809.987 → 809.987 - 360 = 449.987 → 449.987 - 360 = 89.987 → 89.987 ÷ 30 = 2
            Assert.That(DodecatPaulus.IndexForDodecat(89.999), Is.EqualTo(2), "89.999° should return 2");
        });
    }

    [Test]
    public void TestCancerBoundaries()
    {
        Assert.Multiple(() =>
        {
            // Cancer (90-119.999°): 13th harmonic calculations
            // 90° × 13 = 1170 → 1170 - 360 = 810 → 810 - 360 = 450 → 450 - 360 = 90 → 90 ÷ 30 = 3
            Assert.That(DodecatPaulus.IndexForDodecat(90.0), Is.EqualTo(3), "90° should return 3");
            
            // 105° × 13 = 1365 → 1365 - 360 = 1005 → 1005 - 360 = 645 → 645 - 360 = 285 → 285 ÷ 30 = 9
            Assert.That(DodecatPaulus.IndexForDodecat(105.0), Is.EqualTo(9), "105° should return 9");
            
            // 119.999° × 13 = 1559.987 → 1559.987 - 360×4 = 1559.987 - 1440 = 119.987 → 119.987 ÷ 30 = 3
            Assert.That(DodecatPaulus.IndexForDodecat(119.999), Is.EqualTo(3), "119.999° should return 3");
        });
    }

    [Test]
    public void TestLeoBoundaries()
    {
        Assert.Multiple(() =>
        {
            // Leo (120-149.999°): 13th harmonic calculations
            // 120° × 13 = 1560 → 1560 - 360×4 = 1560 - 1440 = 120 → 120 ÷ 30 = 4
            Assert.That(DodecatPaulus.IndexForDodecat(120.0), Is.EqualTo(4), "120° should return 4");
            
            // 135° × 13 = 1755 → 1755 - 360×4 = 1755 - 1440 = 315 → 315 ÷ 30 = 10
            Assert.That(DodecatPaulus.IndexForDodecat(135.0), Is.EqualTo(10), "135° should return 10");
            
            // 149.999° × 13 = 1949.987 → 1949.987 - 360×5 = 1949.987 - 1800 = 149.987 → 149.987 ÷ 30 = 4
            Assert.That(DodecatPaulus.IndexForDodecat(149.999), Is.EqualTo(4), "149.999° should return 4");
        });
    }

    [Test]
    public void TestVirgoBoundaries()
    {
        Assert.Multiple(() =>
        {
            // Virgo (150-179.999°): 13th harmonic calculations
            // 150° × 13 = 1950 → 1950 - 360×5 = 1950 - 1800 = 150 → 150 ÷ 30 = 5
            Assert.That(DodecatPaulus.IndexForDodecat(150.0), Is.EqualTo(5), "150° should return 5");
            
            // 165° × 13 = 2145 → 2145 - 360×5 = 2145 - 1800 = 345 → 345 ÷ 30 = 11
            Assert.That(DodecatPaulus.IndexForDodecat(165.0), Is.EqualTo(11), "165° should return 11");
            
            // 179.999° × 13 = 2339.987 → 2339.987 - 360×6 = 2339.987 - 2160 = 179.987 → 179.987 ÷ 30 = 5
            Assert.That(DodecatPaulus.IndexForDodecat(179.999), Is.EqualTo(5), "179.999° should return 5");
        });
    }

    [Test]
    public void TestLibraBoundaries()
    {
        Assert.Multiple(() =>
        {
            // Libra (180-209.999°): 13th harmonic calculations
            // 180° × 13 = 2340 → 2340 - 360×6 = 2340 - 2160 = 180 → 180 ÷ 30 = 6
            Assert.That(DodecatPaulus.IndexForDodecat(180.0), Is.EqualTo(6), "180° should return 6");
            
            // 195° × 13 = 2535 → 2535 - 360×7 = 2535 - 2520 = 15 → 15 ÷ 30 = 0
            Assert.That(DodecatPaulus.IndexForDodecat(195.0), Is.EqualTo(0), "195° should return 0");
            
            // 209.999° × 13 = 2729.987 → 2729.987 - 360×7 = 2729.987 - 2520 = 209.987 → 209.987 ÷ 30 = 6
            Assert.That(DodecatPaulus.IndexForDodecat(209.999), Is.EqualTo(6), "209.999° should return 6");
        });
    }

    [Test]
    public void TestScorpioBoundaries()
    {
        Assert.Multiple(() =>
        {
            // Scorpio (210-239.999°): 13th harmonic calculations
            // 210° × 13 = 2730 → 2730 - 360×7 = 2730 - 2520 = 210 → 210 ÷ 30 = 7
            Assert.That(DodecatPaulus.IndexForDodecat(210.0), Is.EqualTo(7), "210° should return 7");
            
            // 225° × 13 = 2925 → 2925 - 360×8 = 2925 - 2880 = 45 → 45 ÷ 30 = 1
            Assert.That(DodecatPaulus.IndexForDodecat(225.0), Is.EqualTo(1), "225° should return 1");
            
            // 239.999° × 13 = 3119.987 → 3119.987 - 360×8 = 3119.987 - 2880 = 239.987 → 239.987 ÷ 30 = 7
            Assert.That(DodecatPaulus.IndexForDodecat(239.999), Is.EqualTo(7), "239.999° should return 7");
        });
    }

    [Test]
    public void TestSagittariusBoundaries()
    {
        Assert.Multiple(() =>
        {
            // Sagittarius (240-269.999°): 13th harmonic calculations
            // 240° × 13 = 3120 → 3120 - 360×8 = 3120 - 2880 = 240 → 240 ÷ 30 = 8
            Assert.That(DodecatPaulus.IndexForDodecat(240.0), Is.EqualTo(8), "240° should return 8");
            
            // 255° × 13 = 3315 → 3315 - 360×9 = 3315 - 3240 = 75 → 75 ÷ 30 = 2
            Assert.That(DodecatPaulus.IndexForDodecat(255.0), Is.EqualTo(2), "255° should return 2");
            
            // 269.999° × 13 = 3509.987 → 3509.987 - 360×9 = 3509.987 - 3240 = 269.987 → 269.987 ÷ 30 = 8
            Assert.That(DodecatPaulus.IndexForDodecat(269.999), Is.EqualTo(8), "269.999° should return 8");
        });
    }

    [Test]
    public void TestCapricornBoundaries()
    {
        Assert.Multiple(() =>
        {
            // Capricorn (270-299.999°): 13th harmonic calculations
            // 270° × 13 = 3510 → 3510 - 360×9 = 3510 - 3240 = 270 → 270 ÷ 30 = 9
            Assert.That(DodecatPaulus.IndexForDodecat(270.0), Is.EqualTo(9), "270° should return 9");
            
            // 285° × 13 = 3705 → 3705 - 360×10 = 3705 - 3600 = 105 → 105 ÷ 30 = 3
            Assert.That(DodecatPaulus.IndexForDodecat(285.0), Is.EqualTo(3), "285° should return 3");
            
            // 299.999° × 13 = 3899.987 → 3899.987 - 360×10 = 3899.987 - 3600 = 299.987 → 299.987 ÷ 30 = 9
            Assert.That(DodecatPaulus.IndexForDodecat(299.999), Is.EqualTo(9), "299.999° should return 9");
        });
    }

    [Test]
    public void TestAquariusBoundaries()
    {
        Assert.Multiple(() =>
        {
            // Aquarius (300-329.999°): 13th harmonic calculations
            // 300° × 13 = 3900 → 3900 - 360×10 = 3900 - 3600 = 300 → 300 ÷ 30 = 10
            Assert.That(DodecatPaulus.IndexForDodecat(300.0), Is.EqualTo(10), "300° should return 10");
            
            // 315° × 13 = 4095 → 4095 - 360×11 = 4095 - 3960 = 135 → 135 ÷ 30 = 4
            Assert.That(DodecatPaulus.IndexForDodecat(315.0), Is.EqualTo(4), "315° should return 4");
            
            // 329.999° × 13 = 4289.987 → 4289.987 - 360×11 = 4289.987 - 3960 = 329.987 → 329.987 ÷ 30 = 10
            Assert.That(DodecatPaulus.IndexForDodecat(329.999), Is.EqualTo(10), "329.999° should return 10");
        });
    }

    [Test]
    public void TestPiscesBoundaries()
    {
        Assert.Multiple(() =>
        {
            // Pisces (330-359.999°): 13th harmonic calculations
            // 330° × 13 = 4290 → 4290 - 360×11 = 4290 - 3960 = 330 → 330 ÷ 30 = 11
            Assert.That(DodecatPaulus.IndexForDodecat(330.0), Is.EqualTo(11), "330° should return 11");
            
            // 345° × 13 = 4485 → 4485 - 360×12 = 4485 - 4320 = 165 → 165 ÷ 30 = 5
            Assert.That(DodecatPaulus.IndexForDodecat(345.0), Is.EqualTo(5), "345° should return 5");
            
            // 359.999° × 13 = 4679.987 → 4679.987 - 360×12 = 4679.987 - 4320 = 359.987 → 359.987 ÷ 30 = 11
            Assert.That(DodecatPaulus.IndexForDodecat(359.999), Is.EqualTo(11), "359.999° should return 11");
        });
    }

    [Test]
    public void TestBoundaryValues()
    {
        Assert.Multiple(() =>
        {
            // Test exact boundary values for each sign (every 30 degrees)
            Assert.That(DodecatPaulus.IndexForDodecat(0.0), Is.EqualTo(0), "0° should return 0 (Aries start)");
            Assert.That(DodecatPaulus.IndexForDodecat(30.0), Is.EqualTo(1), "30° should return 1 (Taurus start)");
            Assert.That(DodecatPaulus.IndexForDodecat(60.0), Is.EqualTo(2), "60° should return 2 (Gemini start)");
            Assert.That(DodecatPaulus.IndexForDodecat(90.0), Is.EqualTo(3), "90° should return 3 (Cancer start)");
            Assert.That(DodecatPaulus.IndexForDodecat(120.0), Is.EqualTo(4), "120° should return 4 (Leo start)");
            Assert.That(DodecatPaulus.IndexForDodecat(150.0), Is.EqualTo(5), "150° should return 5 (Virgo start)");
            Assert.That(DodecatPaulus.IndexForDodecat(180.0), Is.EqualTo(6), "180° should return 6 (Libra start)");
            Assert.That(DodecatPaulus.IndexForDodecat(210.0), Is.EqualTo(7), "210° should return 7 (Scorpio start)");
            Assert.That(DodecatPaulus.IndexForDodecat(240.0), Is.EqualTo(8), "240° should return 8 (Sagittarius start)");
            Assert.That(DodecatPaulus.IndexForDodecat(270.0), Is.EqualTo(9), "270° should return 9 (Capricorn start)");
            Assert.That(DodecatPaulus.IndexForDodecat(300.0), Is.EqualTo(10), "300° should return 10 (Aquarius start)");
            Assert.That(DodecatPaulus.IndexForDodecat(330.0), Is.EqualTo(11), "330° should return 11 (Pisces start)");
        });
    }

    [Test]
    public void TestThirteenthHarmonicPattern()
    {
        Assert.Multiple(() =>
        {
            // Test the 13th harmonic pattern across the zodiac
            // This demonstrates how the 13th harmonic creates a different pattern than the original method
            
            // First few degrees show the pattern
            Assert.That(DodecatPaulus.IndexForDodecat(0.0), Is.EqualTo(0), "0°: 0");
            Assert.That(DodecatPaulus.IndexForDodecat(1.0), Is.EqualTo(0), "1°: 0");
            Assert.That(DodecatPaulus.IndexForDodecat(2.0), Is.EqualTo(0), "2°: 0");
            Assert.That(DodecatPaulus.IndexForDodecat(3.0), Is.EqualTo(1), "3°: 1");
            Assert.That(DodecatPaulus.IndexForDodecat(4.0), Is.EqualTo(1), "4°: 1");
            Assert.That(DodecatPaulus.IndexForDodecat(5.0), Is.EqualTo(2), "5°: 2");
            Assert.That(DodecatPaulus.IndexForDodecat(6.0), Is.EqualTo(2), "6°: 2");
            Assert.That(DodecatPaulus.IndexForDodecat(7.0), Is.EqualTo(3), "7°: 3");
            Assert.That(DodecatPaulus.IndexForDodecat(8.0), Is.EqualTo(3), "8°: 3");
            Assert.That(DodecatPaulus.IndexForDodecat(9.0), Is.EqualTo(3), "9°: 3");
            Assert.That(DodecatPaulus.IndexForDodecat(10.0), Is.EqualTo(4), "10°: 4");
            Assert.That(DodecatPaulus.IndexForDodecat(11.0), Is.EqualTo(4), "11°: 4");
            Assert.That(DodecatPaulus.IndexForDodecat(12.0), Is.EqualTo(5), "12°: 5");
            Assert.That(DodecatPaulus.IndexForDodecat(13.0), Is.EqualTo(5), "13°: 5");
            Assert.That(DodecatPaulus.IndexForDodecat(14.0), Is.EqualTo(6), "14°: 6");
            Assert.That(DodecatPaulus.IndexForDodecat(15.0), Is.EqualTo(6), "15°: 6");
            Assert.That(DodecatPaulus.IndexForDodecat(16.0), Is.EqualTo(6), "16°: 6");
            Assert.That(DodecatPaulus.IndexForDodecat(17.0), Is.EqualTo(7), "17°: 7");
            Assert.That(DodecatPaulus.IndexForDodecat(18.0), Is.EqualTo(7), "18°: 7");
            Assert.That(DodecatPaulus.IndexForDodecat(19.0), Is.EqualTo(8), "19°: 8");
            Assert.That(DodecatPaulus.IndexForDodecat(20.0), Is.EqualTo(8), "20°: 8");
            Assert.That(DodecatPaulus.IndexForDodecat(21.0), Is.EqualTo(9), "21°: 9");
            Assert.That(DodecatPaulus.IndexForDodecat(22.0), Is.EqualTo(9), "22°: 9");
            Assert.That(DodecatPaulus.IndexForDodecat(23.0), Is.EqualTo(9), "23°: 9");
            Assert.That(DodecatPaulus.IndexForDodecat(24.0), Is.EqualTo(10), "24°: 10");
            Assert.That(DodecatPaulus.IndexForDodecat(25.0), Is.EqualTo(10), "25°: 10");
            Assert.That(DodecatPaulus.IndexForDodecat(26.0), Is.EqualTo(11), "26°: 11");
            Assert.That(DodecatPaulus.IndexForDodecat(27.0), Is.EqualTo(11), "27°: 11");
            Assert.That(DodecatPaulus.IndexForDodecat(28.0), Is.EqualTo(0), "28°: 11");
            Assert.That(DodecatPaulus.IndexForDodecat(29.0), Is.EqualTo(0), "29°: 0 (wraps around)");
        });
    }

    [Test]
    public void TestLargeMultiplications()
    {
        Assert.Multiple(() =>
        {
            // Test cases that require multiple 360° subtractions
            // 300° × 13 = 3900 → 3900 - 360×10 = 3900 - 3600 = 300 → 300 ÷ 30 = 10
            Assert.That(DodecatPaulus.IndexForDodecat(300.0), Is.EqualTo(10), "300° should return 10");
            
            // 330° × 13 = 4290 → 4290 - 360×11 = 4290 - 3960 = 330 → 330 ÷ 30 = 11
            Assert.That(DodecatPaulus.IndexForDodecat(330.0), Is.EqualTo(11), "330° should return 11");
            
            // 359.999° × 13 = 4679.987 → 4679.987 - 360×12 = 4679.987 - 4320 = 359.987 → 359.987 ÷ 30 = 11
            Assert.That(DodecatPaulus.IndexForDodecat(359.999), Is.EqualTo(11), "359.999° should return 11");
        });
    }
} 