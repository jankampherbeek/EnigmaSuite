// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.ZodiacDivisions;

namespace Enigma.Test.Core.Slices.ZodiacDivisions;

[TestFixture]
public class TestDodecatOriginal
{
    [Test]
    public void TestAriesDodecatemoria()
    {
        Assert.Multiple(() =>
        {
            // Aries (0-29.999°): 12 subportions of 2.5° each
            // Subportion 0 (0-2.499°): 0 + 0 = 0
            Assert.That(DodecatOriginal.IndexForDodecat(0.0), Is.EqualTo(0), "0° should return 0");
            Assert.That(DodecatOriginal.IndexForDodecat(1.0), Is.EqualTo(0), "1° should return 0");
            Assert.That(DodecatOriginal.IndexForDodecat(2.499), Is.EqualTo(0), "2.499° should return 0");
            
            // Subportion 1 (2.5-4.999°): 0 + 1 = 1
            Assert.That(DodecatOriginal.IndexForDodecat(2.5), Is.EqualTo(1), "2.5° should return 1");
            Assert.That(DodecatOriginal.IndexForDodecat(3.0), Is.EqualTo(1), "3° should return 1");
            Assert.That(DodecatOriginal.IndexForDodecat(4.999), Is.EqualTo(1), "4.999° should return 1");
            
            // Subportion 2 (5-7.499°): 0 + 2 = 2
            Assert.That(DodecatOriginal.IndexForDodecat(5.0), Is.EqualTo(2), "5° should return 2");
            Assert.That(DodecatOriginal.IndexForDodecat(6.0), Is.EqualTo(2), "6° should return 2");
            Assert.That(DodecatOriginal.IndexForDodecat(7.499), Is.EqualTo(2), "7.499° should return 2");
            
            // Subportion 3 (7.5-9.999°): 0 + 3 = 3
            Assert.That(DodecatOriginal.IndexForDodecat(7.5), Is.EqualTo(3), "7.5° should return 3");
            Assert.That(DodecatOriginal.IndexForDodecat(8.0), Is.EqualTo(3), "8° should return 3");
            Assert.That(DodecatOriginal.IndexForDodecat(9.999), Is.EqualTo(3), "9.999° should return 3");
            
            // Subportion 4 (10-12.499°): 0 + 4 = 4
            Assert.That(DodecatOriginal.IndexForDodecat(10.0), Is.EqualTo(4), "10° should return 4");
            Assert.That(DodecatOriginal.IndexForDodecat(11.0), Is.EqualTo(4), "11° should return 4");
            Assert.That(DodecatOriginal.IndexForDodecat(12.499), Is.EqualTo(4), "12.499° should return 4");
            
            // Subportion 5 (12.5-14.999°): 0 + 5 = 5
            Assert.That(DodecatOriginal.IndexForDodecat(12.5), Is.EqualTo(5), "12.5° should return 5");
            Assert.That(DodecatOriginal.IndexForDodecat(13.0), Is.EqualTo(5), "13° should return 5");
            Assert.That(DodecatOriginal.IndexForDodecat(14.999), Is.EqualTo(5), "14.999° should return 5");
            
            // Subportion 6 (15-17.499°): 0 + 6 = 6
            Assert.That(DodecatOriginal.IndexForDodecat(15.0), Is.EqualTo(6), "15° should return 6");
            Assert.That(DodecatOriginal.IndexForDodecat(16.0), Is.EqualTo(6), "16° should return 6");
            Assert.That(DodecatOriginal.IndexForDodecat(17.499), Is.EqualTo(6), "17.499° should return 6");
            
            // Subportion 7 (17.5-19.999°): 0 + 7 = 7
            Assert.That(DodecatOriginal.IndexForDodecat(17.5), Is.EqualTo(7), "17.5° should return 7");
            Assert.That(DodecatOriginal.IndexForDodecat(18.0), Is.EqualTo(7), "18° should return 7");
            Assert.That(DodecatOriginal.IndexForDodecat(19.999), Is.EqualTo(7), "19.999° should return 7");
            
            // Subportion 8 (20-22.499°): 0 + 8 = 8
            Assert.That(DodecatOriginal.IndexForDodecat(20.0), Is.EqualTo(8), "20° should return 8");
            Assert.That(DodecatOriginal.IndexForDodecat(21.0), Is.EqualTo(8), "21° should return 8");
            Assert.That(DodecatOriginal.IndexForDodecat(22.499), Is.EqualTo(8), "22.499° should return 8");
            
            // Subportion 9 (22.5-24.999°): 0 + 9 = 9
            Assert.That(DodecatOriginal.IndexForDodecat(22.5), Is.EqualTo(9), "22.5° should return 9");
            Assert.That(DodecatOriginal.IndexForDodecat(23.0), Is.EqualTo(9), "23° should return 9");
            Assert.That(DodecatOriginal.IndexForDodecat(24.999), Is.EqualTo(9), "24.999° should return 9");
            
            // Subportion 10 (25-27.499°): 0 + 10 = 10
            Assert.That(DodecatOriginal.IndexForDodecat(25.0), Is.EqualTo(10), "25° should return 10");
            Assert.That(DodecatOriginal.IndexForDodecat(26.0), Is.EqualTo(10), "26° should return 10");
            Assert.That(DodecatOriginal.IndexForDodecat(27.499), Is.EqualTo(10), "27.499° should return 10");
            
            // Subportion 11 (27.5-29.999°): 0 + 11 = 11
            Assert.That(DodecatOriginal.IndexForDodecat(27.5), Is.EqualTo(11), "27.5° should return 11");
            Assert.That(DodecatOriginal.IndexForDodecat(28.0), Is.EqualTo(11), "28° should return 11");
            Assert.That(DodecatOriginal.IndexForDodecat(29.999), Is.EqualTo(11), "29.999° should return 11");
        });
    }

    [Test]
    public void TestTaurusDodecatemoria()
    {
        Assert.Multiple(() =>
        {
            // Taurus (30-59.999°): 12 subportions of 2.5° each
            // Subportion 0 (30-32.499°): 1 + 0 = 1
            Assert.That(DodecatOriginal.IndexForDodecat(30.0), Is.EqualTo(1), "30° should return 1");
            Assert.That(DodecatOriginal.IndexForDodecat(31.0), Is.EqualTo(1), "31° should return 1");
            Assert.That(DodecatOriginal.IndexForDodecat(32.499), Is.EqualTo(1), "32.499° should return 1");
            
            // Subportion 1 (32.5-34.999°): 1 + 1 = 2
            Assert.That(DodecatOriginal.IndexForDodecat(32.5), Is.EqualTo(2), "32.5° should return 2");
            Assert.That(DodecatOriginal.IndexForDodecat(33.0), Is.EqualTo(2), "33° should return 2");
            Assert.That(DodecatOriginal.IndexForDodecat(34.999), Is.EqualTo(2), "34.999° should return 2");
            
            // Subportion 11 (57.5-59.999°): 1 + 11 = 12 → 0 (after subtracting 12)
            Assert.That(DodecatOriginal.IndexForDodecat(57.5), Is.EqualTo(0), "57.5° should return 0");
            Assert.That(DodecatOriginal.IndexForDodecat(58.0), Is.EqualTo(0), "58° should return 0");
            Assert.That(DodecatOriginal.IndexForDodecat(59.999), Is.EqualTo(0), "59.999° should return 0");
        });
    }

    [Test]
    public void TestInvalidLongitudes()
    {
        Assert.Multiple(() =>
        {
            Assert.That(DodecatOriginal.IndexForDodecat(-0.1), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(DodecatOriginal.IndexForDodecat(-1.0), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(DodecatOriginal.IndexForDodecat(-360.0), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(DodecatOriginal.IndexForDodecat(360.0), Is.EqualTo(-1), "360° should return -1");
            Assert.That(DodecatOriginal.IndexForDodecat(360.1), Is.EqualTo(-1), "Longitude > 360° should return -1");
            Assert.That(DodecatOriginal.IndexForDodecat(720.0), Is.EqualTo(-1), "Longitude > 360° should return -1");
        });
    }

    [Test]
    public void TestBoundaryValues()
    {
        Assert.Multiple(() =>
        {
            // Test exact boundary values for each sign (every 30 degrees)
            Assert.That(DodecatOriginal.IndexForDodecat(0.0), Is.EqualTo(0), "0° should return 0 (Aries start)");
            Assert.That(DodecatOriginal.IndexForDodecat(30.0), Is.EqualTo(1), "30° should return 1 (Taurus start)");
            Assert.That(DodecatOriginal.IndexForDodecat(60.0), Is.EqualTo(2), "60° should return 2 (Gemini start)");
            Assert.That(DodecatOriginal.IndexForDodecat(90.0), Is.EqualTo(3), "90° should return 3 (Cancer start)");
            Assert.That(DodecatOriginal.IndexForDodecat(120.0), Is.EqualTo(4), "120° should return 4 (Leo start)");
            Assert.That(DodecatOriginal.IndexForDodecat(150.0), Is.EqualTo(5), "150° should return 5 (Virgo start)");
            Assert.That(DodecatOriginal.IndexForDodecat(180.0), Is.EqualTo(6), "180° should return 6 (Libra start)");
            Assert.That(DodecatOriginal.IndexForDodecat(210.0), Is.EqualTo(7), "210° should return 7 (Scorpio start)");
            Assert.That(DodecatOriginal.IndexForDodecat(240.0), Is.EqualTo(8), "240° should return 8 (Sagittarius start)");
            Assert.That(DodecatOriginal.IndexForDodecat(270.0), Is.EqualTo(9), "270° should return 9 (Capricorn start)");
            Assert.That(DodecatOriginal.IndexForDodecat(300.0), Is.EqualTo(10), "300° should return 10 (Aquarius start)");
            Assert.That(DodecatOriginal.IndexForDodecat(330.0), Is.EqualTo(11), "330° should return 11 (Pisces start)");
        });
    }

    [Test]
    public void TestSubportionBoundaries()
    {
        Assert.Multiple(() =>
        {
            // Test subportion boundaries within Aries (0-29.999°)
            Assert.That(DodecatOriginal.IndexForDodecat(0.0), Is.EqualTo(0), "0° should return 0");
            Assert.That(DodecatOriginal.IndexForDodecat(2.5), Is.EqualTo(1), "2.5° should return 1");
            Assert.That(DodecatOriginal.IndexForDodecat(5.0), Is.EqualTo(2), "5° should return 2");
            Assert.That(DodecatOriginal.IndexForDodecat(7.5), Is.EqualTo(3), "7.5° should return 3");
            Assert.That(DodecatOriginal.IndexForDodecat(10.0), Is.EqualTo(4), "10° should return 4");
            Assert.That(DodecatOriginal.IndexForDodecat(12.5), Is.EqualTo(5), "12.5° should return 5");
            Assert.That(DodecatOriginal.IndexForDodecat(15.0), Is.EqualTo(6), "15° should return 6");
            Assert.That(DodecatOriginal.IndexForDodecat(17.5), Is.EqualTo(7), "17.5° should return 7");
            Assert.That(DodecatOriginal.IndexForDodecat(20.0), Is.EqualTo(8), "20° should return 8");
            Assert.That(DodecatOriginal.IndexForDodecat(22.5), Is.EqualTo(9), "22.5° should return 9");
            Assert.That(DodecatOriginal.IndexForDodecat(25.0), Is.EqualTo(10), "25° should return 10");
            Assert.That(DodecatOriginal.IndexForDodecat(27.5), Is.EqualTo(11), "27.5° should return 11");
        });
    }

    [Test]
    public void TestDodecatemoriaSequence()
    {
        Assert.Multiple(() =>
        {
            // Test the complete dodecatemoria sequence across the zodiac
            // This tests the modulo operation when indices exceed 11
            
            // Aries (0-29.999°): 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11
            Assert.That(DodecatOriginal.IndexForDodecat(0.0), Is.EqualTo(0), "0°: 0");
            Assert.That(DodecatOriginal.IndexForDodecat(2.5), Is.EqualTo(1), "2.5°: 1");
            Assert.That(DodecatOriginal.IndexForDodecat(5.0), Is.EqualTo(2), "5°: 2");
            Assert.That(DodecatOriginal.IndexForDodecat(7.5), Is.EqualTo(3), "7.5°: 3");
            Assert.That(DodecatOriginal.IndexForDodecat(10.0), Is.EqualTo(4), "10°: 4");
            Assert.That(DodecatOriginal.IndexForDodecat(12.5), Is.EqualTo(5), "12.5°: 5");
            Assert.That(DodecatOriginal.IndexForDodecat(15.0), Is.EqualTo(6), "15°: 6");
            Assert.That(DodecatOriginal.IndexForDodecat(17.5), Is.EqualTo(7), "17.5°: 7");
            Assert.That(DodecatOriginal.IndexForDodecat(20.0), Is.EqualTo(8), "20°: 8");
            Assert.That(DodecatOriginal.IndexForDodecat(22.5), Is.EqualTo(9), "22.5°: 9");
            Assert.That(DodecatOriginal.IndexForDodecat(25.0), Is.EqualTo(10), "25°: 10");
            Assert.That(DodecatOriginal.IndexForDodecat(27.5), Is.EqualTo(11), "27.5°: 11");
            
            // Taurus (30-59.999°): 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0
            Assert.That(DodecatOriginal.IndexForDodecat(30.0), Is.EqualTo(1), "30°: 1");
            Assert.That(DodecatOriginal.IndexForDodecat(32.5), Is.EqualTo(2), "32.5°: 2");
            Assert.That(DodecatOriginal.IndexForDodecat(35.0), Is.EqualTo(3), "35°: 3");
            Assert.That(DodecatOriginal.IndexForDodecat(37.5), Is.EqualTo(4), "37.5°: 4");
            Assert.That(DodecatOriginal.IndexForDodecat(40.0), Is.EqualTo(5), "40°: 5");
            Assert.That(DodecatOriginal.IndexForDodecat(42.5), Is.EqualTo(6), "42.5°: 6");
            Assert.That(DodecatOriginal.IndexForDodecat(45.0), Is.EqualTo(7), "45°: 7");
            Assert.That(DodecatOriginal.IndexForDodecat(47.5), Is.EqualTo(8), "47.5°: 8");
            Assert.That(DodecatOriginal.IndexForDodecat(50.0), Is.EqualTo(9), "50°: 9");
            Assert.That(DodecatOriginal.IndexForDodecat(52.5), Is.EqualTo(10), "52.5°: 10");
            Assert.That(DodecatOriginal.IndexForDodecat(55.0), Is.EqualTo(11), "55°: 11");
            Assert.That(DodecatOriginal.IndexForDodecat(57.5), Is.EqualTo(0), "57.5°: 0 (modulo 12)");
        });
    }
}
