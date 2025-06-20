// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.ZodiacDivisions;

namespace Enigma.Test.Core.Slices.ZodiacDivisions;

[TestFixture]
public class TestDecanPlanets
{
    private DecanPlanets _decanPlanets = null!;

    [SetUp]
    public void SetUp()
    {
        _decanPlanets = new DecanPlanets();
    }

    [Test]
    public void TestFirstCycleDecans()
    {
        Assert.Multiple(() =>
        {
            // First cycle (0-69.999°): planets[0-6] = {0, 2, 3, 1, 4, 5, 6}
            // Decan 0 (0-9.999°): Sun (0)
            Assert.That(_decanPlanets.IndexForDecanPlanet(0.0), Is.EqualTo(0), "0° should return Sun (0)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(5.0), Is.EqualTo(0), "5° should return Sun (0)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(9.999), Is.EqualTo(0), "9.999° should return Sun (0)");
            
            // Decan 1 (10-19.999°): Mercury (2)
            Assert.That(_decanPlanets.IndexForDecanPlanet(10.0), Is.EqualTo(2), "10° should return Mercury (2)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(15.0), Is.EqualTo(2), "15° should return Mercury (2)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(19.999), Is.EqualTo(2), "19.999° should return Mercury (2)");
            
            // Decan 2 (20-29.999°): Venus (3)
            Assert.That(_decanPlanets.IndexForDecanPlanet(20.0), Is.EqualTo(3), "20° should return Venus (3)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(25.0), Is.EqualTo(3), "25° should return Venus (3)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(29.999), Is.EqualTo(3), "29.999° should return Venus (3)");
            
            // Decan 3 (30-39.999°): Moon (1)
            Assert.That(_decanPlanets.IndexForDecanPlanet(30.0), Is.EqualTo(1), "30° should return Moon (1)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(35.0), Is.EqualTo(1), "35° should return Moon (1)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(39.999), Is.EqualTo(1), "39.999° should return Moon (1)");
            
            // Decan 4 (40-49.999°): Mars (4)
            Assert.That(_decanPlanets.IndexForDecanPlanet(40.0), Is.EqualTo(4), "40° should return Mars (4)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(45.0), Is.EqualTo(4), "45° should return Mars (4)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(49.999), Is.EqualTo(4), "49.999° should return Mars (4)");
            
            // Decan 5 (50-59.999°): Jupiter (5)
            Assert.That(_decanPlanets.IndexForDecanPlanet(50.0), Is.EqualTo(5), "50° should return Jupiter (5)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(55.0), Is.EqualTo(5), "55° should return Jupiter (5)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(59.999), Is.EqualTo(5), "59.999° should return Jupiter (5)");
            
            // Decan 6 (60-69.999°): Saturn (6)
            Assert.That(_decanPlanets.IndexForDecanPlanet(60.0), Is.EqualTo(6), "60° should return Saturn (6)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(65.0), Is.EqualTo(6), "65° should return Saturn (6)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(69.999), Is.EqualTo(6), "69.999° should return Saturn (6)");
        });
    }

    [Test]
    public void TestSecondCycleDecans()
    {
        Assert.Multiple(() =>
        {
            // Second cycle (70-139.999°): planets[7-13] = {0, 2, 3, 1, 4, 5, 6} (repeats)
            // Decan 7 (70-79.999°): Sun (0)
            Assert.That(_decanPlanets.IndexForDecanPlanet(70.0), Is.EqualTo(0), "70° should return Sun (0)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(75.0), Is.EqualTo(0), "75° should return Sun (0)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(79.999), Is.EqualTo(0), "79.999° should return Sun (0)");
            
            // Decan 8 (80-89.999°): Mercury (2)
            Assert.That(_decanPlanets.IndexForDecanPlanet(80.0), Is.EqualTo(2), "80° should return Mercury (2)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(85.0), Is.EqualTo(2), "85° should return Mercury (2)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(89.999), Is.EqualTo(2), "89.999° should return Mercury (2)");
            
            // Decan 9 (90-99.999°): Venus (3)
            Assert.That(_decanPlanets.IndexForDecanPlanet(90.0), Is.EqualTo(3), "90° should return Venus (3)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(95.0), Is.EqualTo(3), "95° should return Venus (3)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(99.999), Is.EqualTo(3), "99.999° should return Venus (3)");
            
            // Decan 10 (100-109.999°): Moon (1)
            Assert.That(_decanPlanets.IndexForDecanPlanet(100.0), Is.EqualTo(1), "100° should return Moon (1)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(105.0), Is.EqualTo(1), "105° should return Moon (1)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(109.999), Is.EqualTo(1), "109.999° should return Moon (1)");
            
            // Decan 11 (110-119.999°): Mars (4)
            Assert.That(_decanPlanets.IndexForDecanPlanet(110.0), Is.EqualTo(4), "110° should return Mars (4)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(115.0), Is.EqualTo(4), "115° should return Mars (4)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(119.999), Is.EqualTo(4), "119.999° should return Mars (4)");
            
            // Decan 12 (120-129.999°): Jupiter (5)
            Assert.That(_decanPlanets.IndexForDecanPlanet(120.0), Is.EqualTo(5), "120° should return Jupiter (5)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(125.0), Is.EqualTo(5), "125° should return Jupiter (5)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(129.999), Is.EqualTo(5), "129.999° should return Jupiter (5)");
            
            // Decan 13 (130-139.999°): Saturn (6)
            Assert.That(_decanPlanets.IndexForDecanPlanet(130.0), Is.EqualTo(6), "130° should return Saturn (6)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(135.0), Is.EqualTo(6), "135° should return Saturn (6)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(139.999), Is.EqualTo(6), "139.999° should return Saturn (6)");
        });
    }

    [Test]
    public void TestThirdCycleDecans()
    {
        Assert.Multiple(() =>
        {
            // Third cycle (140-209.999°): planets[14-20] = {0, 2, 3, 1, 4, 5, 6} (repeats)
            // Decan 14 (140-149.999°): Sun (0)
            Assert.That(_decanPlanets.IndexForDecanPlanet(140.0), Is.EqualTo(0), "140° should return Sun (0)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(145.0), Is.EqualTo(0), "145° should return Sun (0)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(149.999), Is.EqualTo(0), "149.999° should return Sun (0)");
            
            // Decan 15 (150-159.999°): Mercury (2)
            Assert.That(_decanPlanets.IndexForDecanPlanet(150.0), Is.EqualTo(2), "150° should return Mercury (2)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(155.0), Is.EqualTo(2), "155° should return Mercury (2)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(159.999), Is.EqualTo(2), "159.999° should return Mercury (2)");
            
            // Decan 16 (160-169.999°): Venus (3)
            Assert.That(_decanPlanets.IndexForDecanPlanet(160.0), Is.EqualTo(3), "160° should return Venus (3)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(165.0), Is.EqualTo(3), "165° should return Venus (3)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(169.999), Is.EqualTo(3), "169.999° should return Venus (3)");
            
            // Decan 17 (170-179.999°): Moon (1)
            Assert.That(_decanPlanets.IndexForDecanPlanet(170.0), Is.EqualTo(1), "170° should return Moon (1)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(175.0), Is.EqualTo(1), "175° should return Moon (1)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(179.999), Is.EqualTo(1), "179.999° should return Moon (1)");
            
            // Decan 18 (180-189.999°): Mars (4)
            Assert.That(_decanPlanets.IndexForDecanPlanet(180.0), Is.EqualTo(4), "180° should return Mars (4)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(185.0), Is.EqualTo(4), "185° should return Mars (4)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(189.999), Is.EqualTo(4), "189.999° should return Mars (4)");
            
            // Decan 19 (190-199.999°): Jupiter (5)
            Assert.That(_decanPlanets.IndexForDecanPlanet(190.0), Is.EqualTo(5), "190° should return Jupiter (5)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(195.0), Is.EqualTo(5), "195° should return Jupiter (5)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(199.999), Is.EqualTo(5), "199.999° should return Jupiter (5)");
            
            // Decan 20 (200-209.999°): Saturn (6)
            Assert.That(_decanPlanets.IndexForDecanPlanet(200.0), Is.EqualTo(6), "200° should return Saturn (6)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(205.0), Is.EqualTo(6), "205° should return Saturn (6)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(209.999), Is.EqualTo(6), "209.999° should return Saturn (6)");
        });
    }

    [Test]
    public void TestFourthCycleDecans()
    {
        Assert.Multiple(() =>
        {
            // Fourth cycle (210-279.999°): planets[21-27] = {0, 2, 3, 1, 4, 5, 6} (repeats)
            // Decan 21 (210-219.999°): Sun (0)
            Assert.That(_decanPlanets.IndexForDecanPlanet(210.0), Is.EqualTo(0), "210° should return Sun (0)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(215.0), Is.EqualTo(0), "215° should return Sun (0)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(219.999), Is.EqualTo(0), "219.999° should return Sun (0)");
            
            // Decan 22 (220-229.999°): Mercury (2)
            Assert.That(_decanPlanets.IndexForDecanPlanet(220.0), Is.EqualTo(2), "220° should return Mercury (2)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(225.0), Is.EqualTo(2), "225° should return Mercury (2)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(229.999), Is.EqualTo(2), "229.999° should return Mercury (2)");
            
            // Decan 23 (230-239.999°): Venus (3)
            Assert.That(_decanPlanets.IndexForDecanPlanet(230.0), Is.EqualTo(3), "230° should return Venus (3)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(235.0), Is.EqualTo(3), "235° should return Venus (3)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(239.999), Is.EqualTo(3), "239.999° should return Venus (3)");
            
            // Decan 24 (240-249.999°): Moon (1)
            Assert.That(_decanPlanets.IndexForDecanPlanet(240.0), Is.EqualTo(1), "240° should return Moon (1)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(245.0), Is.EqualTo(1), "245° should return Moon (1)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(249.999), Is.EqualTo(1), "249.999° should return Moon (1)");
            
            // Decan 25 (250-259.999°): Mars (4)
            Assert.That(_decanPlanets.IndexForDecanPlanet(250.0), Is.EqualTo(4), "250° should return Mars (4)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(255.0), Is.EqualTo(4), "255° should return Mars (4)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(259.999), Is.EqualTo(4), "259.999° should return Mars (4)");
            
            // Decan 26 (260-269.999°): Jupiter (5)
            Assert.That(_decanPlanets.IndexForDecanPlanet(260.0), Is.EqualTo(5), "260° should return Jupiter (5)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(265.0), Is.EqualTo(5), "265° should return Jupiter (5)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(269.999), Is.EqualTo(5), "269.999° should return Jupiter (5)");
            
            // Decan 27 (270-279.999°): Saturn (6)
            Assert.That(_decanPlanets.IndexForDecanPlanet(270.0), Is.EqualTo(6), "270° should return Saturn (6)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(275.0), Is.EqualTo(6), "275° should return Saturn (6)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(279.999), Is.EqualTo(6), "279.999° should return Saturn (6)");
        });
    }

    [Test]
    public void TestFifthCycleDecans()
    {
        Assert.Multiple(() =>
        {
            // Fifth cycle (280-349.999°): planets[28-34] = {0, 2, 3, 1, 4, 5, 6} (repeats)
            // Decan 28 (280-289.999°): Sun (0)
            Assert.That(_decanPlanets.IndexForDecanPlanet(280.0), Is.EqualTo(0), "280° should return Sun (0)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(285.0), Is.EqualTo(0), "285° should return Sun (0)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(289.999), Is.EqualTo(0), "289.999° should return Sun (0)");
            
            // Decan 29 (290-299.999°): Mercury (2)
            Assert.That(_decanPlanets.IndexForDecanPlanet(290.0), Is.EqualTo(2), "290° should return Mercury (2)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(295.0), Is.EqualTo(2), "295° should return Mercury (2)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(299.999), Is.EqualTo(2), "299.999° should return Mercury (2)");
            
            // Decan 30 (300-309.999°): Venus (3)
            Assert.That(_decanPlanets.IndexForDecanPlanet(300.0), Is.EqualTo(3), "300° should return Venus (3)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(305.0), Is.EqualTo(3), "305° should return Venus (3)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(309.999), Is.EqualTo(3), "309.999° should return Venus (3)");
            
            // Decan 31 (310-319.999°): Moon (1)
            Assert.That(_decanPlanets.IndexForDecanPlanet(310.0), Is.EqualTo(1), "310° should return Moon (1)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(315.0), Is.EqualTo(1), "315° should return Moon (1)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(319.999), Is.EqualTo(1), "319.999° should return Moon (1)");
            
            // Decan 32 (320-329.999°): Mars (4)
            Assert.That(_decanPlanets.IndexForDecanPlanet(320.0), Is.EqualTo(4), "320° should return Mars (4)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(325.0), Is.EqualTo(4), "325° should return Mars (4)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(329.999), Is.EqualTo(4), "329.999° should return Mars (4)");
            
            // Decan 33 (330-339.999°): Jupiter (5)
            Assert.That(_decanPlanets.IndexForDecanPlanet(330.0), Is.EqualTo(5), "330° should return Jupiter (5)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(335.0), Is.EqualTo(5), "335° should return Jupiter (5)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(339.999), Is.EqualTo(5), "339.999° should return Jupiter (5)");
            
            // Decan 34 (340-349.999°): Saturn (6)
            Assert.That(_decanPlanets.IndexForDecanPlanet(340.0), Is.EqualTo(6), "340° should return Saturn (6)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(345.0), Is.EqualTo(6), "345° should return Saturn (6)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(349.999), Is.EqualTo(6), "349.999° should return Saturn (6)");
        });
    }

    [Test]
    public void TestFinalDecan()
    {
        Assert.Multiple(() =>
        {
            // Final decan (350-359.999°): planets[35] = {0} (Sun)
            // Decan 35 (350-359.999°): Sun (0)
            Assert.That(_decanPlanets.IndexForDecanPlanet(350.0), Is.EqualTo(0), "350° should return Sun (0)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(355.0), Is.EqualTo(0), "355° should return Sun (0)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(359.999), Is.EqualTo(0), "359.999° should return Sun (0)");
        });
    }

    [Test]
    public void TestInvalidLongitudes()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_decanPlanets.IndexForDecanPlanet(-0.1), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(_decanPlanets.IndexForDecanPlanet(-1.0), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(_decanPlanets.IndexForDecanPlanet(-360.0), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(_decanPlanets.IndexForDecanPlanet(360.0), Is.EqualTo(-1), "360° should return -1");
            Assert.That(_decanPlanets.IndexForDecanPlanet(360.1), Is.EqualTo(-1), "Longitude > 360° should return -1");
            Assert.That(_decanPlanets.IndexForDecanPlanet(720.0), Is.EqualTo(-1), "Longitude > 360° should return -1");
        });
    }

    [Test]
    public void TestBoundaryValues()
    {
        Assert.Multiple(() =>
        {
            // Test exact boundary values for each decan (every 10 degrees)
            Assert.That(_decanPlanets.IndexForDecanPlanet(0.0), Is.EqualTo(0), "0° should return Sun (0)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(10.0), Is.EqualTo(2), "10° should return Mercury (2)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(20.0), Is.EqualTo(3), "20° should return Venus (3)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(30.0), Is.EqualTo(1), "30° should return Moon (1)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(40.0), Is.EqualTo(4), "40° should return Mars (4)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(50.0), Is.EqualTo(5), "50° should return Jupiter (5)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(60.0), Is.EqualTo(6), "60° should return Saturn (6)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(70.0), Is.EqualTo(0), "70° should return Sun (0) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(80.0), Is.EqualTo(2), "80° should return Mercury (2) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(90.0), Is.EqualTo(3), "90° should return Venus (3) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(100.0), Is.EqualTo(1), "100° should return Moon (1) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(110.0), Is.EqualTo(4), "110° should return Mars (4) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(120.0), Is.EqualTo(5), "120° should return Jupiter (5) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(130.0), Is.EqualTo(6), "130° should return Saturn (6) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(140.0), Is.EqualTo(0), "140° should return Sun (0) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(150.0), Is.EqualTo(2), "150° should return Mercury (2) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(160.0), Is.EqualTo(3), "160° should return Venus (3) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(170.0), Is.EqualTo(1), "170° should return Moon (1) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(180.0), Is.EqualTo(4), "180° should return Mars (4) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(190.0), Is.EqualTo(5), "190° should return Jupiter (5) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(200.0), Is.EqualTo(6), "200° should return Saturn (6) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(210.0), Is.EqualTo(0), "210° should return Sun (0) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(220.0), Is.EqualTo(2), "220° should return Mercury (2) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(230.0), Is.EqualTo(3), "230° should return Venus (3) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(240.0), Is.EqualTo(1), "240° should return Moon (1) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(250.0), Is.EqualTo(4), "250° should return Mars (4) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(260.0), Is.EqualTo(5), "260° should return Jupiter (5) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(270.0), Is.EqualTo(6), "270° should return Saturn (6) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(280.0), Is.EqualTo(0), "280° should return Sun (0) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(290.0), Is.EqualTo(2), "290° should return Mercury (2) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(300.0), Is.EqualTo(3), "300° should return Venus (3) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(310.0), Is.EqualTo(1), "310° should return Moon (1) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(320.0), Is.EqualTo(4), "320° should return Mars (4) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(330.0), Is.EqualTo(5), "330° should return Jupiter (5) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(340.0), Is.EqualTo(6), "340° should return Saturn (6) - cycle repeats");
            Assert.That(_decanPlanets.IndexForDecanPlanet(350.0), Is.EqualTo(0), "350° should return Sun (0) - final decan");
        });
    }

    [Test]
    public void TestPlanetSequencePattern()
    {
        Assert.Multiple(() =>
        {
            // Test that the planet sequence follows the pattern: Sun, Mercury, Venus, Moon, Mars, Jupiter, Saturn
            // This tests the modulo operation with the planets array {0, 2, 3, 1, 4, 5, 6}
            
            // First complete cycle (0-69.999°)
            Assert.That(_decanPlanets.IndexForDecanPlanet(0.0), Is.EqualTo(0), "Decan 0: Sun");
            Assert.That(_decanPlanets.IndexForDecanPlanet(10.0), Is.EqualTo(2), "Decan 1: Mercury");
            Assert.That(_decanPlanets.IndexForDecanPlanet(20.0), Is.EqualTo(3), "Decan 2: Venus");
            Assert.That(_decanPlanets.IndexForDecanPlanet(30.0), Is.EqualTo(1), "Decan 3: Moon");
            Assert.That(_decanPlanets.IndexForDecanPlanet(40.0), Is.EqualTo(4), "Decan 4: Mars");
            Assert.That(_decanPlanets.IndexForDecanPlanet(50.0), Is.EqualTo(5), "Decan 5: Jupiter");
            Assert.That(_decanPlanets.IndexForDecanPlanet(60.0), Is.EqualTo(6), "Decan 6: Saturn");
            
            // Second cycle should repeat the same pattern
            Assert.That(_decanPlanets.IndexForDecanPlanet(70.0), Is.EqualTo(0), "Decan 7: Sun (repeats)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(80.0), Is.EqualTo(2), "Decan 8: Mercury (repeats)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(90.0), Is.EqualTo(3), "Decan 9: Venus (repeats)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(100.0), Is.EqualTo(1), "Decan 10: Moon (repeats)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(110.0), Is.EqualTo(4), "Decan 11: Mars (repeats)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(120.0), Is.EqualTo(5), "Decan 12: Jupiter (repeats)");
            Assert.That(_decanPlanets.IndexForDecanPlanet(130.0), Is.EqualTo(6), "Decan 13: Saturn (repeats)");
        });
    }
} 