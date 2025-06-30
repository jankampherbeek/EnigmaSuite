// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.ZodiacDivisions;

namespace Enigma.Test.Core.Slices.ZodiacDivisions;

[TestFixture]
public class TestZodiacDivisionsOrchestrator
{
    private ZodiacDivisionsOrchestrator _orchestrator;

    [SetUp]
    public void Setup()
    {
        _orchestrator = new ZodiacDivisionsOrchestrator();
    }

    [Test]
    public void TestSignsMethod()
    {
        Assert.Multiple(() =>
        {
            // Test Aries (0-29.999°)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(0.0, ZodiacDivisionMethods.Signs)), Is.EqualTo(0), "0° should return Aries (0)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(15.0, ZodiacDivisionMethods.Signs)), Is.EqualTo(0), "15° should return Aries (0)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(29.999, ZodiacDivisionMethods.Signs)), Is.EqualTo(0), "29.999° should return Aries (0)");
            
            // Test Taurus (30-59.999°)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(30.0, ZodiacDivisionMethods.Signs)), Is.EqualTo(1), "30° should return Taurus (1)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(45.0, ZodiacDivisionMethods.Signs)), Is.EqualTo(1), "45° should return Taurus (1)");
            
            // Test Pisces (330-359.999°)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(330.0, ZodiacDivisionMethods.Signs)), Is.EqualTo(11), "330° should return Pisces (11)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(359.999, ZodiacDivisionMethods.Signs)), Is.EqualTo(11), "359.999° should return Pisces (11)");
        });
    }

    [Test]
    public void TestDecansPlanetMethod()
    {
        Assert.Multiple(() =>
        {
            // Test first decan (0-9.999°) - should return Mars (4)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(0.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(4), "0° should return Mars (4)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(5.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(4), "5° should return Mars (4)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(9.999, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(4), "9.999° should return Mars (4)");
            
            // Test second decan (10-19.999°) - should return Sun (0)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(10.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(0), "10° should return Sun (0)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(15.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(0), "15° should return Sun (0)");
            
            // Test third decan (20-29.999°) - should return Venus (3)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(20.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(3), "20° should return Venus (3)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(25.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(3), "25° should return Venus (3)");
            
            // Test fourth decan (30-39.999°) - should return Mercury (2)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(30.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(2), "30° should return Mercury (2)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(35.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(2), "35° should return Mercury (2)");
            
            // Test fifth decan (40-49.999°) - should return Moon (1)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(40.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(1), "40° should return Moon (1)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(45.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(1), "45° should return Moon (1)");
            
            // Test sixth decan (50-59.999°) - should return Saturn (6)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(50.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(6), "50° should return Saturn (6)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(55.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(6), "55° should return Saturn (6)");
            
            // Test seventh decan (60-69.999°) - should return Jupiter (5)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(60.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(5), "60° should return Jupiter (5)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(65.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(5), "65° should return Jupiter (5)");
        });
    }

    [Test]
    public void TestDecansSignMethod()
    {
        Assert.Multiple(() =>
        {
            // Test Aries first decan (0-9.999°) - should return Aries (0)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(0.0, ZodiacDivisionMethods.DecansSign)), Is.EqualTo(0), "0° should return Aries (0)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(5.0, ZodiacDivisionMethods.DecansSign)), Is.EqualTo(0), "5° should return Aries (0)");
            
            // Test Aries second decan (10-19.999°) - should return Leo (4)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(10.0, ZodiacDivisionMethods.DecansSign)), Is.EqualTo(4), "10° should return Leo (4)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(15.0, ZodiacDivisionMethods.DecansSign)), Is.EqualTo(4), "15° should return Leo (4)");
            
            // Test Aries third decan (20-29.999°) - should return Sagittarius (8)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(20.0, ZodiacDivisionMethods.DecansSign)), Is.EqualTo(8), "20° should return Sagittarius (8)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(25.0, ZodiacDivisionMethods.DecansSign)), Is.EqualTo(8), "25° should return Sagittarius (8)");
        });
    }

    [Test]
    public void TestDodecatsOriginalMethod()
    {
        Assert.Multiple(() =>
        {
            // Test Aries first subportion (0-2.499°) - should return Aries (0)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(0.0, ZodiacDivisionMethods.DodecatsOriginal)), Is.EqualTo(0), "0° should return Aries (0)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(1.0, ZodiacDivisionMethods.DodecatsOriginal)), Is.EqualTo(0), "1° should return Aries (0)");
            
            // Test Aries second subportion (2.5-4.999°) - should return Taurus (1)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(2.5, ZodiacDivisionMethods.DodecatsOriginal)), Is.EqualTo(1), "2.5° should return Taurus (1)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(4.0, ZodiacDivisionMethods.DodecatsOriginal)), Is.EqualTo(1), "4° should return Taurus (1)");
            
            // Test Aries third subportion (5.0-7.499°) - should return Gemini (2)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(5.0, ZodiacDivisionMethods.DodecatsOriginal)), Is.EqualTo(2), "5° should return Gemini (2)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(7.0, ZodiacDivisionMethods.DodecatsOriginal)), Is.EqualTo(2), "7° should return Gemini (2)");
        });
    }

    [Test]
    public void TestDodecatsPaulusMethod()
    {
        Assert.Multiple(() =>
        {
            // Test 0° × 13 = 0 → 0 ÷ 30 = 0 (Aries)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(0.0, ZodiacDivisionMethods.DodecatsPaulus)), Is.EqualTo(0), "0° should return Aries (0)");
            
            // Test 30° × 13 = 390 → 390 - 360 = 30 → 30 ÷ 30 = 1 (Taurus)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(30.0, ZodiacDivisionMethods.DodecatsPaulus)), Is.EqualTo(1), "30° should return Taurus (1)");
            
            // Test 45° × 13 = 585 → 585 - 360 = 225 → 225 ÷ 30 = 7 (Libra)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(45.0, ZodiacDivisionMethods.DodecatsPaulus)), Is.EqualTo(7), "45° should return Libra (7)");
        });
    }

    [Test]
    public void TestBoundsEgyptianMethod()
    {
        Assert.Multiple(() =>
        {
            // Test Aries terms: 0-6° Jupiter(5), 6-12° Venus(3), 12-20° Mercury(2), 20-25° Mars(4), 25-30° Saturn(6)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(0.0, ZodiacDivisionMethods.BoundsEgyptian)), Is.EqualTo(5), "0° should return Jupiter (5) in Aries (Egyptian)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(6.0, ZodiacDivisionMethods.BoundsEgyptian)), Is.EqualTo(3), "6° should return Venus (3) in Aries (Egyptian)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(12.0, ZodiacDivisionMethods.BoundsEgyptian)), Is.EqualTo(2), "12° should return Mercury (2) in Aries (Egyptian)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(20.0, ZodiacDivisionMethods.BoundsEgyptian)), Is.EqualTo(4), "20° should return Mars (4) in Aries (Egyptian)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(25.0, ZodiacDivisionMethods.BoundsEgyptian)), Is.EqualTo(6), "25° should return Saturn (6) in Aries (Egyptian)");
        });
    }

    [Test]
    public void TestBoundsPtolemyMethod()
    {
        Assert.Multiple(() =>
        {
            // Test Aries terms: 0-6° Jupiter(5), 6-14° Venus(3), 14-21° Mercury(2), 21-26° Mars(4), 26-30° Saturn(6)
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(0.0, ZodiacDivisionMethods.BoundsPtolemy)), Is.EqualTo(5), "0° should return Jupiter (5) in Aries (Ptolemy)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(6.0, ZodiacDivisionMethods.BoundsPtolemy)), Is.EqualTo(3), "6° should return Venus (3) in Aries (Ptolemy)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(14.0, ZodiacDivisionMethods.BoundsPtolemy)), Is.EqualTo(2), "14° should return Mercury (2) in Aries (Ptolemy)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(21.0, ZodiacDivisionMethods.BoundsPtolemy)), Is.EqualTo(4), "21° should return Mars (4) in Aries (Ptolemy)");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(26.0, ZodiacDivisionMethods.BoundsPtolemy)), Is.EqualTo(6), "26° should return Saturn (6) in Aries (Ptolemy)");
        });
    }

    [Test]
    public void TestInvalidLongitudes()
    {
        Assert.Multiple(() =>
        {
            // Test that invalid longitudes return -1 for all methods
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(-0.1, ZodiacDivisionMethods.Signs)), Is.EqualTo(-1), "Negative longitude should return -1 for Signs");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(360.0, ZodiacDivisionMethods.Signs)), Is.EqualTo(-1), "360° should return -1 for Signs");
            
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(-1.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(-1), "Negative longitude should return -1 for DecansPlanet");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(360.1, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(-1), "Longitude > 360° should return -1 for DecansPlanet");
            
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(-360.0, ZodiacDivisionMethods.DecansSign)), Is.EqualTo(-1), "Negative longitude should return -1 for DecansSign");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(720.0, ZodiacDivisionMethods.DecansSign)), Is.EqualTo(-1), "Longitude > 360° should return -1 for DecansSign");
        });
    }

    [Test]
    public void TestBoundaryValues()
    {
        Assert.Multiple(() =>
        {
            // Test exact boundary values for different methods
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(0.0, ZodiacDivisionMethods.Signs)), Is.EqualTo(0), "Exact 0° should return Aries (0) for Signs");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(30.0, ZodiacDivisionMethods.Signs)), Is.EqualTo(1), "Exact 30° should return Taurus (1) for Signs");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(359.999, ZodiacDivisionMethods.Signs)), Is.EqualTo(11), "359.999° should return Pisces (11) for Signs");
            
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(0.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(4), "Exact 0° should return Mars (4) for DecansPlanet");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(10.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(0), "Exact 10° should return Sun (0) for DecansPlanet");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(20.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(3), "Exact 20° should return Venus (3) for DecansPlanet");
        });
    }

    [Test]
    public void TestMidZodiacValues()
    {
        Assert.Multiple(() =>
        {
            // Test middle of the zodiac (180°) for different methods
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(180.0, ZodiacDivisionMethods.Signs)), Is.EqualTo(6), "180° should return Libra (6) for Signs");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(180.0, ZodiacDivisionMethods.DecansPlanet)), Is.EqualTo(1), "180° should return Moon (1) for DecansPlanet");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(180.0, ZodiacDivisionMethods.DecansSign)), Is.EqualTo(6), "180° should return Libra (6) for DecansSign");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(180.0, ZodiacDivisionMethods.DodecatsOriginal)), Is.EqualTo(6), "180° should return Libra (6) for DodecatsOriginal");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(180.0, ZodiacDivisionMethods.DodecatsPaulus)), Is.EqualTo(6), "180° should return Libra (6) for DodecatsPaulus");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(180.0, ZodiacDivisionMethods.BoundsEgyptian)), Is.EqualTo(6), "180° should return Saturn (6) for BoundsEgyptian");
            Assert.That(_orchestrator.Calculate(new ZodiacDivisionRequest(180.0, ZodiacDivisionMethods.BoundsPtolemy)), Is.EqualTo(6), "180° should return Saturn (6) for BoundsPtolemy");
        });
    }
} 