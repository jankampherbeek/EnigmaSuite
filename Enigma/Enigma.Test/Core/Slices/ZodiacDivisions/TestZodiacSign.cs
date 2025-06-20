// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.ZodiacDivisions;

namespace Enigma.Test.Core.Slices.ZodiacDivisions;

[TestFixture]
public class TestZodiacSign
{
    private ZodiacSign _zodiacSign = null!;

    [SetUp]
    public void SetUp()
    {
        _zodiacSign = new ZodiacSign();
    }

    [Test]
    public void TestAriesBoundaries()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_zodiacSign.IndexForSign(0.0), Is.EqualTo(0), "0° should return Aries (0)");
            Assert.That(_zodiacSign.IndexForSign(15.0), Is.EqualTo(0), "15° should return Aries (0)");
            Assert.That(_zodiacSign.IndexForSign(29.999), Is.EqualTo(0), "29.999° should return Aries (0)");
        });
    }

    [Test]
    public void TestTaurusBoundaries()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_zodiacSign.IndexForSign(30.0), Is.EqualTo(1), "30° should return Taurus (1)");
            Assert.That(_zodiacSign.IndexForSign(45.0), Is.EqualTo(1), "45° should return Taurus (1)");
            Assert.That(_zodiacSign.IndexForSign(59.999), Is.EqualTo(1), "59.999° should return Taurus (1)");
        });
    }

    [Test]
    public void TestGeminiBoundaries()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_zodiacSign.IndexForSign(60.0), Is.EqualTo(2), "60° should return Gemini (2)");
            Assert.That(_zodiacSign.IndexForSign(75.0), Is.EqualTo(2), "75° should return Gemini (2)");
            Assert.That(_zodiacSign.IndexForSign(89.999), Is.EqualTo(2), "89.999° should return Gemini (2)");
        });
    }

    [Test]
    public void TestCancerBoundaries()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_zodiacSign.IndexForSign(90.0), Is.EqualTo(3), "90° should return Cancer (3)");
            Assert.That(_zodiacSign.IndexForSign(105.0), Is.EqualTo(3), "105° should return Cancer (3)");
            Assert.That(_zodiacSign.IndexForSign(119.999), Is.EqualTo(3), "119.999° should return Cancer (3)");
        });
    }

    [Test]
    public void TestLeoBoundaries()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_zodiacSign.IndexForSign(120.0), Is.EqualTo(4), "120° should return Leo (4)");
            Assert.That(_zodiacSign.IndexForSign(135.0), Is.EqualTo(4), "135° should return Leo (4)");
            Assert.That(_zodiacSign.IndexForSign(149.999), Is.EqualTo(4), "149.999° should return Leo (4)");
        });
    }

    [Test]
    public void TestVirgoBoundaries()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_zodiacSign.IndexForSign(150.0), Is.EqualTo(5), "150° should return Virgo (5)");
            Assert.That(_zodiacSign.IndexForSign(165.0), Is.EqualTo(5), "165° should return Virgo (5)");
            Assert.That(_zodiacSign.IndexForSign(179.999), Is.EqualTo(5), "179.999° should return Virgo (5)");
        });
    }

    [Test]
    public void TestLibraBoundaries()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_zodiacSign.IndexForSign(180.0), Is.EqualTo(6), "180° should return Libra (6)");
            Assert.That(_zodiacSign.IndexForSign(195.0), Is.EqualTo(6), "195° should return Libra (6)");
            Assert.That(_zodiacSign.IndexForSign(209.999), Is.EqualTo(6), "209.999° should return Libra (6)");
        });
    }

    [Test]
    public void TestScorpioBoundaries()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_zodiacSign.IndexForSign(210.0), Is.EqualTo(7), "210° should return Scorpio (7)");
            Assert.That(_zodiacSign.IndexForSign(225.0), Is.EqualTo(7), "225° should return Scorpio (7)");
            Assert.That(_zodiacSign.IndexForSign(239.999), Is.EqualTo(7), "239.999° should return Scorpio (7)");
        });
    }

    [Test]
    public void TestSagittariusBoundaries()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_zodiacSign.IndexForSign(240.0), Is.EqualTo(8), "240° should return Sagittarius (8)");
            Assert.That(_zodiacSign.IndexForSign(255.0), Is.EqualTo(8), "255° should return Sagittarius (8)");
            Assert.That(_zodiacSign.IndexForSign(269.999), Is.EqualTo(8), "269.999° should return Sagittarius (8)");
        });
    }

    [Test]
    public void TestCapricornBoundaries()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_zodiacSign.IndexForSign(270.0), Is.EqualTo(9), "270° should return Capricorn (9)");
            Assert.That(_zodiacSign.IndexForSign(285.0), Is.EqualTo(9), "285° should return Capricorn (9)");
            Assert.That(_zodiacSign.IndexForSign(299.999), Is.EqualTo(9), "299.999° should return Capricorn (9)");
        });
    }

    [Test]
    public void TestAquariusBoundaries()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_zodiacSign.IndexForSign(300.0), Is.EqualTo(10), "300° should return Aquarius (10)");
            Assert.That(_zodiacSign.IndexForSign(315.0), Is.EqualTo(10), "315° should return Aquarius (10)");
            Assert.That(_zodiacSign.IndexForSign(329.999), Is.EqualTo(10), "329.999° should return Aquarius (10)");
        });
    }

    [Test]
    public void TestPiscesBoundaries()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_zodiacSign.IndexForSign(330.0), Is.EqualTo(11), "330° should return Pisces (11)");
            Assert.That(_zodiacSign.IndexForSign(345.0), Is.EqualTo(11), "345° should return Pisces (11)");
            Assert.That(_zodiacSign.IndexForSign(359.999), Is.EqualTo(11), "359.999° should return Pisces (11)");
        });
    }

    [Test]
    public void TestInvalidLongitudes()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_zodiacSign.IndexForSign(-0.1), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(_zodiacSign.IndexForSign(-1.0), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(_zodiacSign.IndexForSign(-360.0), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(_zodiacSign.IndexForSign(360.0), Is.EqualTo(-1), "360° should return -1");
            Assert.That(_zodiacSign.IndexForSign(360.1), Is.EqualTo(-1), "Longitude > 360° should return -1");
            Assert.That(_zodiacSign.IndexForSign(720.0), Is.EqualTo(-1), "Longitude > 360° should return -1");
        });
    }

    [Test]
    public void TestBoundaryValues()
    {
        Assert.Multiple(() =>
        {
            // Test exact boundary values
            Assert.That(_zodiacSign.IndexForSign(0.0), Is.EqualTo(0), "Exact 0° should return Aries (0)");
            Assert.That(_zodiacSign.IndexForSign(30.0), Is.EqualTo(1), "Exact 30° should return Taurus (1)");
            Assert.That(_zodiacSign.IndexForSign(60.0), Is.EqualTo(2), "Exact 60° should return Gemini (2)");
            Assert.That(_zodiacSign.IndexForSign(90.0), Is.EqualTo(3), "Exact 90° should return Cancer (3)");
            Assert.That(_zodiacSign.IndexForSign(120.0), Is.EqualTo(4), "Exact 120° should return Leo (4)");
            Assert.That(_zodiacSign.IndexForSign(150.0), Is.EqualTo(5), "Exact 150° should return Virgo (5)");
            Assert.That(_zodiacSign.IndexForSign(180.0), Is.EqualTo(6), "Exact 180° should return Libra (6)");
            Assert.That(_zodiacSign.IndexForSign(210.0), Is.EqualTo(7), "Exact 210° should return Scorpio (7)");
            Assert.That(_zodiacSign.IndexForSign(240.0), Is.EqualTo(8), "Exact 240° should return Sagittarius (8)");
            Assert.That(_zodiacSign.IndexForSign(270.0), Is.EqualTo(9), "Exact 270° should return Capricorn (9)");
            Assert.That(_zodiacSign.IndexForSign(300.0), Is.EqualTo(10), "Exact 300° should return Aquarius (10)");
            Assert.That(_zodiacSign.IndexForSign(330.0), Is.EqualTo(11), "Exact 330° should return Pisces (11)");
        });
    }
} 