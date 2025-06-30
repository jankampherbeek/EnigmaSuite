// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.ZodiacDivisions;

namespace Enigma.Test.Core.Slices.ZodiacDivisions;

[TestFixture]
public class TestDecanPlanets
{
    [Test]
    public void TestFirstCycleDecans()
    {
        Assert.Multiple(() =>
        {
            // First cycle (0-69.999°)
            // Decan 0 (0-9.999°): Mars (4)
            Assert.That(DecanPlanets.IndexForDecanPlanet(0.0), Is.EqualTo(4), "0° should return Mars (4)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(5.0), Is.EqualTo(4), "5° should return Mars (4)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(9.999), Is.EqualTo(4), "9.999° should return Mars (4)");
            
            // Decan 1 (10-19.999°): Sun (0)
            Assert.That(DecanPlanets.IndexForDecanPlanet(10.0), Is.EqualTo(0), "10° should return Sun (0)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(15.0), Is.EqualTo(0), "15° should return Sun (0)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(19.999), Is.EqualTo(0), "19.999° should return Sun (0)");
            
            // Decan 2 (20-29.999°): Venus (3)
            Assert.That(DecanPlanets.IndexForDecanPlanet(20.0), Is.EqualTo(3), "20° should return Venus (3)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(25.0), Is.EqualTo(3), "25° should return Venus (3)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(29.999), Is.EqualTo(3), "29.999° should return Venus (3)");
            
            // Decan 3 (30-39.999°): Mercury (2)
            Assert.That(DecanPlanets.IndexForDecanPlanet(30.0), Is.EqualTo(2), "30° should return Mercury (2)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(35.0), Is.EqualTo(2), "35° should return Mercury (2)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(39.999), Is.EqualTo(2), "39.999° should return Mercury (2)");
            
            // Decan 4 (40-49.999°): Moon (1)
            Assert.That(DecanPlanets.IndexForDecanPlanet(40.0), Is.EqualTo(1), "40° should return Moon (1)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(45.0), Is.EqualTo(1), "45° should return Moon (1)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(49.999), Is.EqualTo(1), "49.999° should return Moon (1)");
            
            // Decan 5 (50-59.999°): Saturn (6)
            Assert.That(DecanPlanets.IndexForDecanPlanet(50.0), Is.EqualTo(6), "50° should return Saturn (6)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(55.0), Is.EqualTo(6), "55° should return Saturn (6)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(59.999), Is.EqualTo(6), "59.999° should return Saturn (6)");
            
            // Decan 6 (60-69.999°): Jupiter (5)
            Assert.That(DecanPlanets.IndexForDecanPlanet(60.0), Is.EqualTo(5), "60° should return Jupiter (5)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(65.0), Is.EqualTo(5), "65° should return Jupiter (5)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(69.999), Is.EqualTo(5), "69.999° should return Jupiter (5)");
        });
    }

    [Test]
    public void TestSecondCycleDecans()
    {
        Assert.Multiple(() =>
        {
            // Second cycle (70-139.999°)
            // Decan 7 (70-79.999°): Mars (4)
            Assert.That(DecanPlanets.IndexForDecanPlanet(70.0), Is.EqualTo(4), "70° should return Mars (4)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(75.0), Is.EqualTo(4), "75° should return Mars (4)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(79.999), Is.EqualTo(4), "79.999° should return Mars (4)");
            
            // Decan 8 (80-89.999°): Sun (0)
            Assert.That(DecanPlanets.IndexForDecanPlanet(80.0), Is.EqualTo(0), "80° should return Sun (0)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(85.0), Is.EqualTo(0), "85° should return Sun (0)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(89.999), Is.EqualTo(0), "89.999° should return Sun (0)");
            
            // Decan 9 (90-99.999°): Venus (3)
            Assert.That(DecanPlanets.IndexForDecanPlanet(90.0), Is.EqualTo(3), "90° should return Venus (3)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(95.0), Is.EqualTo(3), "95° should return Venus (3)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(99.999), Is.EqualTo(3), "99.999° should return Venus (3)");
            
            // Decan 10 (100-109.999°): Mercury (2)
            Assert.That(DecanPlanets.IndexForDecanPlanet(100.0), Is.EqualTo(2), "100° should return Mercury (2)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(105.0), Is.EqualTo(2), "105° should return Mercury (2)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(109.999), Is.EqualTo(2), "109.999° should return Mercury (2)");
            
            // Decan 11 (110-119.999°): Moon (1)
            Assert.That(DecanPlanets.IndexForDecanPlanet(110.0), Is.EqualTo(1), "110° should return Moon (1)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(115.0), Is.EqualTo(1), "115° should return Moon (1)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(119.999), Is.EqualTo(1), "119.999° should return Moon (1)");
            
            // Decan 12 (120-129.999°): Saturn (6)
            Assert.That(DecanPlanets.IndexForDecanPlanet(120.0), Is.EqualTo(6), "120° should return Saturn (6)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(125.0), Is.EqualTo(6), "125° should return Saturn (6)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(129.999), Is.EqualTo(6), "129.999° should return Saturn (6)");
            
            // Decan 13 (130-139.999°): Jupiter (5)
            Assert.That(DecanPlanets.IndexForDecanPlanet(130.0), Is.EqualTo(5), "130° should return Jupiter (5)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(135.0), Is.EqualTo(5), "135° should return Jupiter (5)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(139.999), Is.EqualTo(5), "139.999° should return Jupiter (5)");
        });
    }

    [Test]
    public void TestThirdCycleDecans()
    {
        Assert.Multiple(() =>
        {
            // Third cycle (140-209.999°): planets[14-20] = {4, 0, 3, 2, 1, 6, 5} (repeats)
            // Decan 14 (140-149.999°): Mars (4)
            Assert.That(DecanPlanets.IndexForDecanPlanet(140.0), Is.EqualTo(4), "140° should return Mars (4)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(145.0), Is.EqualTo(4), "145° should return Mars (4)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(149.999), Is.EqualTo(4), "149.999° should return Mars (4)");
            
            // Decan 15 (150-159.999°): Sun (0)
            Assert.That(DecanPlanets.IndexForDecanPlanet(150.0), Is.EqualTo(0), "150° should return Sun (0)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(155.0), Is.EqualTo(0), "155° should return Sun (0)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(159.999), Is.EqualTo(0), "159.999° should return Sun (0)");
            
            // Decan 16 (160-169.999°): Venus (3)
            Assert.That(DecanPlanets.IndexForDecanPlanet(160.0), Is.EqualTo(3), "160° should return Venus (3)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(165.0), Is.EqualTo(3), "165° should return Venus (3)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(169.999), Is.EqualTo(3), "169.999° should return Venus (3)");
            
            // Decan 17 (170-179.999°): Mercury (2)
            Assert.That(DecanPlanets.IndexForDecanPlanet(170.0), Is.EqualTo(2), "170° should return Mercury (2)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(175.0), Is.EqualTo(2), "175° should return Mercury (2)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(179.999), Is.EqualTo(2), "179.999° should return Mercury (2)");
            
            // Decan 18 (180-189.999°): Moon (1)
            Assert.That(DecanPlanets.IndexForDecanPlanet(180.0), Is.EqualTo(1), "180° should return Moon (1)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(185.0), Is.EqualTo(1), "185° should return Moon (1)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(189.999), Is.EqualTo(1), "189.999° should return Moon (1)");
            
            // Decan 19 (190-199.999°): Saturn (6)
            Assert.That(DecanPlanets.IndexForDecanPlanet(190.0), Is.EqualTo(6), "190° should return Saturn (6)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(195.0), Is.EqualTo(6), "195° should return Saturn (6)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(199.999), Is.EqualTo(6), "199.999° should return Saturn (6)");
            
            // Decan 20 (200-209.999°): Jupiter (5)
            Assert.That(DecanPlanets.IndexForDecanPlanet(200.0), Is.EqualTo(5), "200° should return Jupiter (5)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(205.0), Is.EqualTo(5), "205° should return Jupiter (5)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(209.999), Is.EqualTo(5), "209.999° should return Jupiter (5)");
        });
    }

    [Test]
    public void TestFourthCycleDecans()
    {
        Assert.Multiple(() =>
        {
            // Fourth cycle (210-279.999°): planets[21-27] = {4, 0, 3, 2, 1, 6, 5} (repeats)
            // Decan 21 (210-219.999°): Mars (4)
            Assert.That(DecanPlanets.IndexForDecanPlanet(210.0), Is.EqualTo(4), "210° should return Mars (4)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(215.0), Is.EqualTo(4), "215° should return Mars (4)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(219.999), Is.EqualTo(4), "219.999° should return Mars (4)");
            
            // Decan 22 (220-229.999°): Sun (0)
            Assert.That(DecanPlanets.IndexForDecanPlanet(220.0), Is.EqualTo(0), "220° should return Sun (0)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(225.0), Is.EqualTo(0), "225° should return Sun (0)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(229.999), Is.EqualTo(0), "229.999° should return Sun (0)");
            
            // Decan 23 (230-239.999°): Venus (3)
            Assert.That(DecanPlanets.IndexForDecanPlanet(230.0), Is.EqualTo(3), "230° should return Venus (3)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(235.0), Is.EqualTo(3), "235° should return Venus (3)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(239.999), Is.EqualTo(3), "239.999° should return Venus (3)");
            
            // Decan 24 (240-249.999°): Mercury (2)
            Assert.That(DecanPlanets.IndexForDecanPlanet(240.0), Is.EqualTo(2), "240° should return Mercury (2)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(245.0), Is.EqualTo(2), "245° should return Mercury (2)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(249.999), Is.EqualTo(2), "249.999° should return Mercury (2)");
            
            // Decan 25 (250-259.999°): Moon (1)
            Assert.That(DecanPlanets.IndexForDecanPlanet(250.0), Is.EqualTo(1), "250° should return Moon (1)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(255.0), Is.EqualTo(1), "255° should return Moon (1)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(259.999), Is.EqualTo(1), "259.999° should return Moon (1)");
            
            // Decan 26 (260-269.999°): Saturn (6)
            Assert.That(DecanPlanets.IndexForDecanPlanet(260.0), Is.EqualTo(6), "260° should return Saturn (6)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(265.0), Is.EqualTo(6), "265° should return Saturn (6)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(269.999), Is.EqualTo(6), "269.999° should return Saturn (6)");
            
            // Decan 27 (270-279.999°): Jupiter (5)
            Assert.That(DecanPlanets.IndexForDecanPlanet(270.0), Is.EqualTo(5), "270° should return Jupiter (5)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(275.0), Is.EqualTo(5), "275° should return Jupiter (5)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(279.999), Is.EqualTo(5), "279.999° should return Jupiter (5)");
        });
    }

    [Test]
    public void TestFifthCycleDecans()
    {
        Assert.Multiple(() =>
        {
            // Fifth cycle (280-349.999°): planets[28-34] = {4, 0, 3, 2, 1, 6, 5} (repeats)
            // Decan 28 (280-289.999°): Mars (4)
            Assert.That(DecanPlanets.IndexForDecanPlanet(280.0), Is.EqualTo(4), "280° should return Mars (4)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(285.0), Is.EqualTo(4), "285° should return Mars (4)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(289.999), Is.EqualTo(4), "289.999° should return Mars (4)");
            
            // Decan 29 (290-299.999°): Sun (0)
            Assert.That(DecanPlanets.IndexForDecanPlanet(290.0), Is.EqualTo(0), "290° should return Sun (0)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(295.0), Is.EqualTo(0), "295° should return Sun (0)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(299.999), Is.EqualTo(0), "299.999° should return Sun (0)");
            
            // Decan 30 (300-309.999°): Venus (3)
            Assert.That(DecanPlanets.IndexForDecanPlanet(300.0), Is.EqualTo(3), "300° should return Venus (3)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(305.0), Is.EqualTo(3), "305° should return Venus (3)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(309.999), Is.EqualTo(3), "309.999° should return Venus (3)");
            
            // Decan 31 (310-319.999°): Mercury (2)
            Assert.That(DecanPlanets.IndexForDecanPlanet(310.0), Is.EqualTo(2), "310° should return Mercury (2)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(315.0), Is.EqualTo(2), "315° should return Mercury (2)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(319.999), Is.EqualTo(2), "319.999° should return Mercury (2)");
            
            // Decan 32 (320-329.999°): Moon (1)
            Assert.That(DecanPlanets.IndexForDecanPlanet(320.0), Is.EqualTo(1), "320° should return Moon (1)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(325.0), Is.EqualTo(1), "325° should return Moon (1)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(329.999), Is.EqualTo(1), "329.999° should return Moon (1)");
            
            // Decan 33 (330-339.999°): Saturn (6)
            Assert.That(DecanPlanets.IndexForDecanPlanet(330.0), Is.EqualTo(6), "330° should return Saturn (6)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(335.0), Is.EqualTo(6), "335° should return Saturn (6)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(339.999), Is.EqualTo(6), "339.999° should return Saturn (6)");
            
            // Decan 34 (340-349.999°): Jupiter (5)
            Assert.That(DecanPlanets.IndexForDecanPlanet(340.0), Is.EqualTo(5), "340° should return Jupiter (5)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(345.0), Is.EqualTo(5), "345° should return Jupiter (5)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(349.999), Is.EqualTo(5), "349.999° should return Jupiter (5)");
        });
    }

    [Test]
    public void TestFinalDecan()
    {
        Assert.Multiple(() =>
        {
            // Final decan (350-359.999°): planets[35] = {4} (Mars)
            // Decan 35 (350-359.999°): Mars (4)
            Assert.That(DecanPlanets.IndexForDecanPlanet(350.0), Is.EqualTo(4), "350° should return Mars (4)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(355.0), Is.EqualTo(4), "355° should return Mars (4)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(359.999), Is.EqualTo(4), "359.999° should return Mars (4)");
        });
    }

    [Test]
    public void TestInvalidLongitudes()
    {
        Assert.Multiple(() =>
        {
            Assert.That(DecanPlanets.IndexForDecanPlanet(-0.1), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(DecanPlanets.IndexForDecanPlanet(-1.0), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(DecanPlanets.IndexForDecanPlanet(-360.0), Is.EqualTo(-1), "Negative longitude should return -1");
            Assert.That(DecanPlanets.IndexForDecanPlanet(360.0), Is.EqualTo(-1), "360° should return -1");
            Assert.That(DecanPlanets.IndexForDecanPlanet(360.1), Is.EqualTo(-1), "Longitude > 360° should return -1");
            Assert.That(DecanPlanets.IndexForDecanPlanet(720.0), Is.EqualTo(-1), "Longitude > 360° should return -1");
        });
    }

    [Test]
    public void TestBoundaryValues()
    {
        Assert.Multiple(() =>
        {
            // Test exact boundary values for each decan (every 10 degrees)
            Assert.That(DecanPlanets.IndexForDecanPlanet(0.0), Is.EqualTo(4), "0° should return Mars (4)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(10.0), Is.EqualTo(0), "10° should return Sun (0)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(20.0), Is.EqualTo(3), "20° should return Venus (3)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(30.0), Is.EqualTo(2), "30° should return Mercury (2)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(40.0), Is.EqualTo(1), "40° should return Moon (1)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(50.0), Is.EqualTo(6), "50° should return Saturn (6)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(60.0), Is.EqualTo(5), "60° should return Jupiter (5)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(70.0), Is.EqualTo(4), "70° should return Mars (4) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(80.0), Is.EqualTo(0), "80° should return Sun (0) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(90.0), Is.EqualTo(3), "90° should return Venus (3) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(100.0), Is.EqualTo(2), "100° should return Mercury (2) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(110.0), Is.EqualTo(1), "110° should return Moon (1) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(120.0), Is.EqualTo(6), "120° should return Saturn (6) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(130.0), Is.EqualTo(5), "130° should return Jupiter (5) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(140.0), Is.EqualTo(4), "140° should return Mars (4) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(150.0), Is.EqualTo(0), "150° should return Sun (0) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(160.0), Is.EqualTo(3), "160° should return Venus (3) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(170.0), Is.EqualTo(2), "170° should return Mercury (2) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(180.0), Is.EqualTo(1), "180° should return Moon (1) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(190.0), Is.EqualTo(6), "190° should return Saturn (6) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(200.0), Is.EqualTo(5), "200° should return Jupiter (5) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(210.0), Is.EqualTo(4), "210° should return Mars (4) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(220.0), Is.EqualTo(0), "220° should return Sun (0) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(230.0), Is.EqualTo(3), "230° should return Venus (3) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(240.0), Is.EqualTo(2), "240° should return Mercury (2) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(250.0), Is.EqualTo(1), "250° should return Moon (1) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(260.0), Is.EqualTo(6), "260° should return Saturn (6) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(270.0), Is.EqualTo(5), "270° should return Jupiter (5) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(280.0), Is.EqualTo(4), "280° should return Mars (4) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(290.0), Is.EqualTo(0), "290° should return Sun (0) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(300.0), Is.EqualTo(3), "300° should return Venus (3) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(310.0), Is.EqualTo(2), "310° should return Mercury (2) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(320.0), Is.EqualTo(1), "320° should return Moon (1) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(330.0), Is.EqualTo(6), "330° should return Saturn (6) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(340.0), Is.EqualTo(5), "340° should return Jupiter (5) - cycle repeats");
            Assert.That(DecanPlanets.IndexForDecanPlanet(350.0), Is.EqualTo(4), "350° should return Mars (4) - final decan");
        });
    }

    [Test]
    public void TestPlanetSequencePattern()
    {
        Assert.Multiple(() =>
        {
            // Test that the planet sequence follows the pattern: Mars, Sun, Venus, Mercury, Moon, Saturn, Jupiter
            // This tests the modulo operation with the planets array {4, 0, 3, 2, 1, 6, 5}
            
            // First complete cycle (0-69.999°)
            Assert.That(DecanPlanets.IndexForDecanPlanet(0.0), Is.EqualTo(4), "Decan 0: Mars");
            Assert.That(DecanPlanets.IndexForDecanPlanet(10.0), Is.EqualTo(0), "Decan 1: Sun");
            Assert.That(DecanPlanets.IndexForDecanPlanet(20.0), Is.EqualTo(3), "Decan 2: Venus");
            Assert.That(DecanPlanets.IndexForDecanPlanet(30.0), Is.EqualTo(2), "Decan 3: Mercury");
            Assert.That(DecanPlanets.IndexForDecanPlanet(40.0), Is.EqualTo(1), "Decan 4: Moon");
            Assert.That(DecanPlanets.IndexForDecanPlanet(50.0), Is.EqualTo(6), "Decan 5: Saturn");
            Assert.That(DecanPlanets.IndexForDecanPlanet(60.0), Is.EqualTo(5), "Decan 6: Jupiter");
            
            // Second cycle should repeat the same pattern
            Assert.That(DecanPlanets.IndexForDecanPlanet(70.0), Is.EqualTo(4), "Decan 7: Mars (repeats)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(80.0), Is.EqualTo(0), "Decan 8: Sun (repeats)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(90.0), Is.EqualTo(3), "Decan 9: Venus (repeats)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(100.0), Is.EqualTo(2), "Decan 10: Mercury (repeats)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(110.0), Is.EqualTo(1), "Decan 11: Moon (repeats)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(120.0), Is.EqualTo(6), "Decan 12: Saturn (repeats)");
            Assert.That(DecanPlanets.IndexForDecanPlanet(130.0), Is.EqualTo(5), "Decan 13: Jupiter (repeats)");
        });
    }
} 