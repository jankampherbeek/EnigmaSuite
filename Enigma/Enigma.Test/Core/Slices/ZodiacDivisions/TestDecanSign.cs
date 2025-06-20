// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.ZodiacDivisions;

namespace Enigma.Test.Core.Slices.ZodiacDivisions;

[TestFixture]
public class TestDecanSign
{
    private DecanSign _decanSign = null!;

    [SetUp]
    public void SetUp()
    {
        _decanSign = new DecanSign();
    }

    [Test]
    public void TestAriesDecans()
    {
        Assert.Multiple(() =>
        {
            // Aries 1st decan (0-9.999°): Aries (0)
            Assert.That(_decanSign.IndexForDecanSign(0.0), Is.EqualTo(0), "0° should return Aries 1st decan (0)");
            Assert.That(_decanSign.IndexForDecanSign(5.0), Is.EqualTo(0), "5° should return Aries 1st decan (0)");
            Assert.That(_decanSign.IndexForDecanSign(9.999), Is.EqualTo(0), "9.999° should return Aries 1st decan (0)");
            
            // Aries 2nd decan (10-19.999°): Leo (4)
            Assert.That(_decanSign.IndexForDecanSign(10.0), Is.EqualTo(4), "10° should return Aries 2nd decan (Leo = 4)");
            Assert.That(_decanSign.IndexForDecanSign(15.0), Is.EqualTo(4), "15° should return Aries 2nd decan (Leo = 4)");
            Assert.That(_decanSign.IndexForDecanSign(19.999), Is.EqualTo(4), "19.999° should return Aries 2nd decan (Leo = 4)");
            
            // Aries 3rd decan (20-29.999°): Sagittarius (8)
            Assert.That(_decanSign.IndexForDecanSign(20.0), Is.EqualTo(8), "20° should return Aries 3rd decan (Sagittarius = 8)");
            Assert.That(_decanSign.IndexForDecanSign(25.0), Is.EqualTo(8), "25° should return Aries 3rd decan (Sagittarius = 8)");
            Assert.That(_decanSign.IndexForDecanSign(29.999), Is.EqualTo(8), "29.999° should return Aries 3rd decan (Sagittarius = 8)");
        });
    }

    [Test]
    public void TestTaurusDecans()
    {
        Assert.Multiple(() =>
        {
            // Taurus 1st decan (30-39.999°): Taurus (1)
            Assert.That(_decanSign.IndexForDecanSign(30.0), Is.EqualTo(1), "30° should return Taurus 1st decan (1)");
            Assert.That(_decanSign.IndexForDecanSign(35.0), Is.EqualTo(1), "35° should return Taurus 1st decan (1)");
            Assert.That(_decanSign.IndexForDecanSign(39.999), Is.EqualTo(1), "39.999° should return Taurus 1st decan (1)");
            
            // Taurus 2nd decan (40-49.999°): Virgo (5)
            Assert.That(_decanSign.IndexForDecanSign(40.0), Is.EqualTo(5), "40° should return Taurus 2nd decan (Virgo = 5)");
            Assert.That(_decanSign.IndexForDecanSign(45.0), Is.EqualTo(5), "45° should return Taurus 2nd decan (Virgo = 5)");
            Assert.That(_decanSign.IndexForDecanSign(49.999), Is.EqualTo(5), "49.999° should return Taurus 2nd decan (Virgo = 5)");
            
            // Taurus 3rd decan (50-59.999°): Capricorn (9)
            Assert.That(_decanSign.IndexForDecanSign(50.0), Is.EqualTo(9), "50° should return Taurus 3rd decan (Capricorn = 9)");
            Assert.That(_decanSign.IndexForDecanSign(55.0), Is.EqualTo(9), "55° should return Taurus 3rd decan (Capricorn = 9)");
            Assert.That(_decanSign.IndexForDecanSign(59.999), Is.EqualTo(9), "59.999° should return Taurus 3rd decan (Capricorn = 9)");
        });
    }

    [Test]
    public void TestGeminiDecans()
    {
        Assert.Multiple(() =>
        {
            // Gemini 1st decan (60-69.999°): Gemini (2)
            Assert.That(_decanSign.IndexForDecanSign(60.0), Is.EqualTo(2), "60° should return Gemini 1st decan (2)");
            Assert.That(_decanSign.IndexForDecanSign(65.0), Is.EqualTo(2), "65° should return Gemini 1st decan (2)");
            Assert.That(_decanSign.IndexForDecanSign(69.999), Is.EqualTo(2), "69.999° should return Gemini 1st decan (2)");
            
            // Gemini 2nd decan (70-79.999°): Libra (6)
            Assert.That(_decanSign.IndexForDecanSign(70.0), Is.EqualTo(6), "70° should return Gemini 2nd decan (Libra = 6)");
            Assert.That(_decanSign.IndexForDecanSign(75.0), Is.EqualTo(6), "75° should return Gemini 2nd decan (Libra = 6)");
            Assert.That(_decanSign.IndexForDecanSign(79.999), Is.EqualTo(6), "79.999° should return Gemini 2nd decan (Libra = 6)");
            
            // Gemini 3rd decan (80-89.999°): Aquarius (10)
            Assert.That(_decanSign.IndexForDecanSign(80.0), Is.EqualTo(10), "80° should return Gemini 3rd decan (Aquarius = 10)");
            Assert.That(_decanSign.IndexForDecanSign(85.0), Is.EqualTo(10), "85° should return Gemini 3rd decan (Aquarius = 10)");
            Assert.That(_decanSign.IndexForDecanSign(89.999), Is.EqualTo(10), "89.999° should return Gemini 3rd decan (Aquarius = 10)");
        });
    }

    [Test]
    public void TestCancerDecans()
    {
        Assert.Multiple(() =>
        {
            // Cancer 1st decan (90-99.999°): Cancer (3)
            Assert.That(_decanSign.IndexForDecanSign(90.0), Is.EqualTo(3), "90° should return Cancer 1st decan (3)");
            Assert.That(_decanSign.IndexForDecanSign(95.0), Is.EqualTo(3), "95° should return Cancer 1st decan (3)");
            Assert.That(_decanSign.IndexForDecanSign(99.999), Is.EqualTo(3), "99.999° should return Cancer 1st decan (3)");
            
            // Cancer 2nd decan (100-109.999°): Scorpio (7)
            Assert.That(_decanSign.IndexForDecanSign(100.0), Is.EqualTo(7), "100° should return Cancer 2nd decan (Scorpio = 7)");
            Assert.That(_decanSign.IndexForDecanSign(105.0), Is.EqualTo(7), "105° should return Cancer 2nd decan (Scorpio = 7)");
            Assert.That(_decanSign.IndexForDecanSign(109.999), Is.EqualTo(7), "109.999° should return Cancer 2nd decan (Scorpio = 7)");
            
            // Cancer 3rd decan (110-119.999°): Pisces (11)
            Assert.That(_decanSign.IndexForDecanSign(110.0), Is.EqualTo(11), "110° should return Cancer 3rd decan (Pisces = 11)");
            Assert.That(_decanSign.IndexForDecanSign(115.0), Is.EqualTo(11), "115° should return Cancer 3rd decan (Pisces = 11)");
            Assert.That(_decanSign.IndexForDecanSign(119.999), Is.EqualTo(11), "119.999° should return Cancer 3rd decan (Pisces = 11)");
        });
    }

    [Test]
    public void TestLeoDecans()
    {
        Assert.Multiple(() =>
        {
            // Leo 1st decan (120-129.999°): Leo (4)
            Assert.That(_decanSign.IndexForDecanSign(120.0), Is.EqualTo(4), "120° should return Leo 1st decan (4)");
            Assert.That(_decanSign.IndexForDecanSign(125.0), Is.EqualTo(4), "125° should return Leo 1st decan (4)");
            Assert.That(_decanSign.IndexForDecanSign(129.999), Is.EqualTo(4), "129.999° should return Leo 1st decan (4)");
            
            // Leo 2nd decan (130-139.999°): Sagittarius (8)
            Assert.That(_decanSign.IndexForDecanSign(130.0), Is.EqualTo(8), "130° should return Leo 2nd decan (Sagittarius = 8)");
            Assert.That(_decanSign.IndexForDecanSign(135.0), Is.EqualTo(8), "135° should return Leo 2nd decan (Sagittarius = 8)");
            Assert.That(_decanSign.IndexForDecanSign(139.999), Is.EqualTo(8), "139.999° should return Leo 2nd decan (Sagittarius = 8)");
            
            // Leo 3rd decan (140-149.999°): Aries (0) - wraps around
            Assert.That(_decanSign.IndexForDecanSign(140.0), Is.EqualTo(0), "140° should return Leo 3rd decan (Aries = 0, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(145.0), Is.EqualTo(0), "145° should return Leo 3rd decan (Aries = 0, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(149.999), Is.EqualTo(0), "149.999° should return Leo 3rd decan (Aries = 0, wrapped)");
        });
    }

    [Test]
    public void TestVirgoDecans()
    {
        Assert.Multiple(() =>
        {
            // Virgo 1st decan (150-159.999°): Virgo (5)
            Assert.That(_decanSign.IndexForDecanSign(150.0), Is.EqualTo(5), "150° should return Virgo 1st decan (5)");
            Assert.That(_decanSign.IndexForDecanSign(155.0), Is.EqualTo(5), "155° should return Virgo 1st decan (5)");
            Assert.That(_decanSign.IndexForDecanSign(159.999), Is.EqualTo(5), "159.999° should return Virgo 1st decan (5)");
            
            // Virgo 2nd decan (160-169.999°): Capricorn (9)
            Assert.That(_decanSign.IndexForDecanSign(160.0), Is.EqualTo(9), "160° should return Virgo 2nd decan (Capricorn = 9)");
            Assert.That(_decanSign.IndexForDecanSign(165.0), Is.EqualTo(9), "165° should return Virgo 2nd decan (Capricorn = 9)");
            Assert.That(_decanSign.IndexForDecanSign(169.999), Is.EqualTo(9), "169.999° should return Virgo 2nd decan (Capricorn = 9)");
            
            // Virgo 3rd decan (170-179.999°): Taurus (1) - wraps around
            Assert.That(_decanSign.IndexForDecanSign(170.0), Is.EqualTo(1), "170° should return Virgo 3rd decan (Taurus = 1, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(175.0), Is.EqualTo(1), "175° should return Virgo 3rd decan (Taurus = 1, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(179.999), Is.EqualTo(1), "179.999° should return Virgo 3rd decan (Taurus = 1, wrapped)");
        });
    }

    [Test]
    public void TestLibraDecans()
    {
        Assert.Multiple(() =>
        {
            // Libra 1st decan (180-189.999°): Libra (6)
            Assert.That(_decanSign.IndexForDecanSign(180.0), Is.EqualTo(6), "180° should return Libra 1st decan (6)");
            Assert.That(_decanSign.IndexForDecanSign(185.0), Is.EqualTo(6), "185° should return Libra 1st decan (6)");
            Assert.That(_decanSign.IndexForDecanSign(189.999), Is.EqualTo(6), "189.999° should return Libra 1st decan (6)");
            
            // Libra 2nd decan (190-199.999°): Aquarius (10)
            Assert.That(_decanSign.IndexForDecanSign(190.0), Is.EqualTo(10), "190° should return Libra 2nd decan (Aquarius = 10)");
            Assert.That(_decanSign.IndexForDecanSign(195.0), Is.EqualTo(10), "195° should return Libra 2nd decan (Aquarius = 10)");
            Assert.That(_decanSign.IndexForDecanSign(199.999), Is.EqualTo(10), "199.999° should return Libra 2nd decan (Aquarius = 10)");
            
            // Libra 3rd decan (200-209.999°): Gemini (2) - wraps around
            Assert.That(_decanSign.IndexForDecanSign(200.0), Is.EqualTo(2), "200° should return Libra 3rd decan (Gemini = 2, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(205.0), Is.EqualTo(2), "205° should return Libra 3rd decan (Gemini = 2, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(209.999), Is.EqualTo(2), "209.999° should return Libra 3rd decan (Gemini = 2, wrapped)");
        });
    }

    [Test]
    public void TestScorpioDecans()
    {
        Assert.Multiple(() =>
        {
            // Scorpio 1st decan (210-219.999°): Scorpio (7)
            Assert.That(_decanSign.IndexForDecanSign(210.0), Is.EqualTo(7), "210° should return Scorpio 1st decan (7)");
            Assert.That(_decanSign.IndexForDecanSign(215.0), Is.EqualTo(7), "215° should return Scorpio 1st decan (7)");
            Assert.That(_decanSign.IndexForDecanSign(219.999), Is.EqualTo(7), "219.999° should return Scorpio 1st decan (7)");
            
            // Scorpio 2nd decan (220-229.999°): Pisces (11)
            Assert.That(_decanSign.IndexForDecanSign(220.0), Is.EqualTo(11), "220° should return Scorpio 2nd decan (Pisces = 11)");
            Assert.That(_decanSign.IndexForDecanSign(225.0), Is.EqualTo(11), "225° should return Scorpio 2nd decan (Pisces = 11)");
            Assert.That(_decanSign.IndexForDecanSign(229.999), Is.EqualTo(11), "229.999° should return Scorpio 2nd decan (Pisces = 11)");
            
            // Scorpio 3rd decan (230-239.999°): Cancer (3) - wraps around
            Assert.That(_decanSign.IndexForDecanSign(230.0), Is.EqualTo(3), "230° should return Scorpio 3rd decan (Cancer = 3, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(235.0), Is.EqualTo(3), "235° should return Scorpio 3rd decan (Cancer = 3, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(239.999), Is.EqualTo(3), "239.999° should return Scorpio 3rd decan (Cancer = 3, wrapped)");
        });
    }

    [Test]
    public void TestSagittariusDecans()
    {
        Assert.Multiple(() =>
        {
            // Sagittarius 1st decan (240-249.999°): Sagittarius (8)
            Assert.That(_decanSign.IndexForDecanSign(240.0), Is.EqualTo(8), "240° should return Sagittarius 1st decan (8)");
            Assert.That(_decanSign.IndexForDecanSign(245.0), Is.EqualTo(8), "245° should return Sagittarius 1st decan (8)");
            Assert.That(_decanSign.IndexForDecanSign(249.999), Is.EqualTo(8), "249.999° should return Sagittarius 1st decan (8)");
            
            // Sagittarius 2nd decan (250-259.999°): Aries (0) - wraps around
            Assert.That(_decanSign.IndexForDecanSign(250.0), Is.EqualTo(0), "250° should return Sagittarius 2nd decan (Aries = 0, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(255.0), Is.EqualTo(0), "255° should return Sagittarius 2nd decan (Aries = 0, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(259.999), Is.EqualTo(0), "259.999° should return Sagittarius 2nd decan (Aries = 0, wrapped)");
            
            // Sagittarius 3rd decan (260-269.999°): Leo (4) - wraps around
            Assert.That(_decanSign.IndexForDecanSign(260.0), Is.EqualTo(4), "260° should return Sagittarius 3rd decan (Leo = 4, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(265.0), Is.EqualTo(4), "265° should return Sagittarius 3rd decan (Leo = 4, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(269.999), Is.EqualTo(4), "269.999° should return Sagittarius 3rd decan (Leo = 4, wrapped)");
        });
    }

    [Test]
    public void TestCapricornDecans()
    {
        Assert.Multiple(() =>
        {
            // Capricorn 1st decan (270-279.999°): Capricorn (9)
            Assert.That(_decanSign.IndexForDecanSign(270.0), Is.EqualTo(9), "270° should return Capricorn 1st decan (9)");
            Assert.That(_decanSign.IndexForDecanSign(275.0), Is.EqualTo(9), "275° should return Capricorn 1st decan (9)");
            Assert.That(_decanSign.IndexForDecanSign(279.999), Is.EqualTo(9), "279.999° should return Capricorn 1st decan (9)");
            
            // Capricorn 2nd decan (280-289.999°): Taurus (1) - wraps around
            Assert.That(_decanSign.IndexForDecanSign(280.0), Is.EqualTo(1), "280° should return Capricorn 2nd decan (Taurus = 1, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(285.0), Is.EqualTo(1), "285° should return Capricorn 2nd decan (Taurus = 1, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(289.999), Is.EqualTo(1), "289.999° should return Capricorn 2nd decan (Taurus = 1, wrapped)");
            
            // Capricorn 3rd decan (290-299.999°): Virgo (5) - wraps around
            Assert.That(_decanSign.IndexForDecanSign(290.0), Is.EqualTo(5), "290° should return Capricorn 3rd decan (Virgo = 5, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(295.0), Is.EqualTo(5), "295° should return Capricorn 3rd decan (Virgo = 5, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(299.999), Is.EqualTo(5), "299.999° should return Capricorn 3rd decan (Virgo = 5, wrapped)");
        });
    }

    [Test]
    public void TestAquariusDecans()
    {
        Assert.Multiple(() =>
        {
            // Aquarius 1st decan (300-309.999°): Aquarius (10)
            Assert.That(_decanSign.IndexForDecanSign(300.0), Is.EqualTo(10), "300° should return Aquarius 1st decan (10)");
            Assert.That(_decanSign.IndexForDecanSign(305.0), Is.EqualTo(10), "305° should return Aquarius 1st decan (10)");
            Assert.That(_decanSign.IndexForDecanSign(309.999), Is.EqualTo(10), "309.999° should return Aquarius 1st decan (10)");
            
            // Aquarius 2nd decan (310-319.999°): Gemini (2) - wraps around
            Assert.That(_decanSign.IndexForDecanSign(310.0), Is.EqualTo(2), "310° should return Aquarius 2nd decan (Gemini = 2, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(315.0), Is.EqualTo(2), "315° should return Aquarius 2nd decan (Gemini = 2, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(319.999), Is.EqualTo(2), "319.999° should return Aquarius 2nd decan (Gemini = 2, wrapped)");
            
            // Aquarius 3rd decan (320-329.999°): Libra (6) - wraps around
            Assert.That(_decanSign.IndexForDecanSign(320.0), Is.EqualTo(6), "320° should return Aquarius 3rd decan (Libra = 6, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(325.0), Is.EqualTo(6), "325° should return Aquarius 3rd decan (Libra = 6, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(329.999), Is.EqualTo(6), "329.999° should return Aquarius 3rd decan (Libra = 6, wrapped)");
        });
    }

    [Test]
    public void TestPiscesDecans()
    {
        Assert.Multiple(() =>
        {
            // Pisces 1st decan (330-339.999°): Pisces (11)
            Assert.That(_decanSign.IndexForDecanSign(330.0), Is.EqualTo(11), "330° should return Pisces 1st decan (11)");
            Assert.That(_decanSign.IndexForDecanSign(335.0), Is.EqualTo(11), "335° should return Pisces 1st decan (11)");
            Assert.That(_decanSign.IndexForDecanSign(339.999), Is.EqualTo(11), "339.999° should return Pisces 1st decan (11)");
            
            // Pisces 2nd decan (340-349.999°): Cancer (3) - wraps around
            Assert.That(_decanSign.IndexForDecanSign(340.0), Is.EqualTo(3), "340° should return Pisces 2nd decan (Cancer = 3, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(345.0), Is.EqualTo(3), "345° should return Pisces 2nd decan (Cancer = 3, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(349.999), Is.EqualTo(3), "349.999° should return Pisces 2nd decan (Cancer = 3, wrapped)");
            
            // Pisces 3rd decan (350-359.999°): Scorpio (7) - wraps around
            Assert.That(_decanSign.IndexForDecanSign(350.0), Is.EqualTo(7), "350° should return Pisces 3rd decan (Scorpio = 7, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(355.0), Is.EqualTo(7), "355° should return Pisces 3rd decan (Scorpio = 7, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(359.999), Is.EqualTo(7), "359.999° should return Pisces 3rd decan (Scorpio = 7, wrapped)");
        });
    }

    [Test]
    public void TestInvalidLongitudes()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_decanSign.IndexForDecanSign(-0.1), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(_decanSign.IndexForDecanSign(-1.0), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(_decanSign.IndexForDecanSign(-360.0), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(_decanSign.IndexForDecanSign(360.0), Is.EqualTo(-1), "360° should return -1");
            Assert.That(_decanSign.IndexForDecanSign(360.1), Is.EqualTo(-1), "Longitude > 360° should return -1");
            Assert.That(_decanSign.IndexForDecanSign(720.0), Is.EqualTo(-1), "Longitude > 360° should return -1");
        });
    }

    [Test]
    public void TestBoundaryValues()
    {
        Assert.Multiple(() =>
        {
            // Test exact boundary values for each decan
            Assert.That(_decanSign.IndexForDecanSign(0.0), Is.EqualTo(0), "0° should return Aries 1st decan (0)");
            Assert.That(_decanSign.IndexForDecanSign(10.0), Is.EqualTo(4), "10° should return Aries 2nd decan (Leo = 4)");
            Assert.That(_decanSign.IndexForDecanSign(20.0), Is.EqualTo(8), "20° should return Aries 3rd decan (Sagittarius = 8)");
            Assert.That(_decanSign.IndexForDecanSign(30.0), Is.EqualTo(1), "30° should return Taurus 1st decan (1)");
            Assert.That(_decanSign.IndexForDecanSign(40.0), Is.EqualTo(5), "40° should return Taurus 2nd decan (Virgo = 5)");
            Assert.That(_decanSign.IndexForDecanSign(50.0), Is.EqualTo(9), "50° should return Taurus 3rd decan (Capricorn = 9)");
            Assert.That(_decanSign.IndexForDecanSign(60.0), Is.EqualTo(2), "60° should return Gemini 1st decan (2)");
            Assert.That(_decanSign.IndexForDecanSign(70.0), Is.EqualTo(6), "70° should return Gemini 2nd decan (Libra = 6)");
            Assert.That(_decanSign.IndexForDecanSign(80.0), Is.EqualTo(10), "80° should return Gemini 3rd decan (Aquarius = 10)");
            Assert.That(_decanSign.IndexForDecanSign(90.0), Is.EqualTo(3), "90° should return Cancer 1st decan (3)");
            Assert.That(_decanSign.IndexForDecanSign(100.0), Is.EqualTo(7), "100° should return Cancer 2nd decan (Scorpio = 7)");
            Assert.That(_decanSign.IndexForDecanSign(110.0), Is.EqualTo(11), "110° should return Cancer 3rd decan (Pisces = 11)");
            Assert.That(_decanSign.IndexForDecanSign(120.0), Is.EqualTo(4), "120° should return Leo 1st decan (4)");
            Assert.That(_decanSign.IndexForDecanSign(130.0), Is.EqualTo(8), "130° should return Leo 2nd decan (Sagittarius = 8)");
            Assert.That(_decanSign.IndexForDecanSign(140.0), Is.EqualTo(0), "140° should return Leo 3rd decan (Aries = 0, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(150.0), Is.EqualTo(5), "150° should return Virgo 1st decan (5)");
            Assert.That(_decanSign.IndexForDecanSign(160.0), Is.EqualTo(9), "160° should return Virgo 2nd decan (Capricorn = 9)");
            Assert.That(_decanSign.IndexForDecanSign(170.0), Is.EqualTo(1), "170° should return Virgo 3rd decan (Taurus = 1, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(180.0), Is.EqualTo(6), "180° should return Libra 1st decan (6)");
            Assert.That(_decanSign.IndexForDecanSign(190.0), Is.EqualTo(10), "190° should return Libra 2nd decan (Aquarius = 10)");
            Assert.That(_decanSign.IndexForDecanSign(200.0), Is.EqualTo(2), "200° should return Libra 3rd decan (Gemini = 2, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(210.0), Is.EqualTo(7), "210° should return Scorpio 1st decan (7)");
            Assert.That(_decanSign.IndexForDecanSign(220.0), Is.EqualTo(11), "220° should return Scorpio 2nd decan (Pisces = 11)");
            Assert.That(_decanSign.IndexForDecanSign(230.0), Is.EqualTo(3), "230° should return Scorpio 3rd decan (Cancer = 3, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(240.0), Is.EqualTo(8), "240° should return Sagittarius 1st decan (8)");
            Assert.That(_decanSign.IndexForDecanSign(250.0), Is.EqualTo(0), "250° should return Sagittarius 2nd decan (Aries = 0, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(260.0), Is.EqualTo(4), "260° should return Sagittarius 3rd decan (Leo = 4, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(270.0), Is.EqualTo(9), "270° should return Capricorn 1st decan (9)");
            Assert.That(_decanSign.IndexForDecanSign(280.0), Is.EqualTo(1), "280° should return Capricorn 2nd decan (Taurus = 1, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(290.0), Is.EqualTo(5), "290° should return Capricorn 3rd decan (Virgo = 5, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(300.0), Is.EqualTo(10), "300° should return Aquarius 1st decan (10)");
            Assert.That(_decanSign.IndexForDecanSign(310.0), Is.EqualTo(2), "310° should return Aquarius 2nd decan (Gemini = 2, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(320.0), Is.EqualTo(6), "320° should return Aquarius 3rd decan (Libra = 6, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(330.0), Is.EqualTo(11), "330° should return Pisces 1st decan (11)");
            Assert.That(_decanSign.IndexForDecanSign(340.0), Is.EqualTo(3), "340° should return Pisces 2nd decan (Cancer = 3, wrapped)");
            Assert.That(_decanSign.IndexForDecanSign(350.0), Is.EqualTo(7), "350° should return Pisces 3rd decan (Scorpio = 7, wrapped)");
        });
    }
} 