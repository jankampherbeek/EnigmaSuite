// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Slices;
using Enigma.Core.Slices.ZodiacDivisions;

namespace Enigma.Test.Integration;

[TestFixture]
public class IntegrationTestZodiacDivisionsService
{
    private readonly ZodiacDivisionsService _zodiacDivisionsService;

    public IntegrationTestZodiacDivisionsService()
    {
        _zodiacDivisionsService = new ZodiacDivisionsService();
    }

    [Test]
    [TestCase(0.0, ZodiacDivisionMethods.Signs, 0)] // Aries
    [TestCase(30.0, ZodiacDivisionMethods.Signs, 1)] // Taurus
    [TestCase(60.0, ZodiacDivisionMethods.Signs, 2)] // Gemini
    [TestCase(90.0, ZodiacDivisionMethods.Signs, 3)] // Cancer
    [TestCase(120.0, ZodiacDivisionMethods.Signs, 4)] // Leo
    [TestCase(150.0, ZodiacDivisionMethods.Signs, 5)] // Virgo
    [TestCase(180.0, ZodiacDivisionMethods.Signs, 6)] // Libra
    [TestCase(210.0, ZodiacDivisionMethods.Signs, 7)] // Scorpio
    [TestCase(240.0, ZodiacDivisionMethods.Signs, 8)] // Sagittarius
    [TestCase(270.0, ZodiacDivisionMethods.Signs, 9)] // Capricorn
    [TestCase(300.0, ZodiacDivisionMethods.Signs, 10)] // Aquarius
    [TestCase(330.0, ZodiacDivisionMethods.Signs, 11)] // Pisces
    [TestCase(359.999, ZodiacDivisionMethods.Signs, 11)] // End of Pisces
    public void TestSignsMethod(double longitude, ZodiacDivisionMethods method, int expectedIndex)
    {
        var result = _zodiacDivisionsService.FindIndexForDivision(longitude, method);
        Assert.That(result, Is.EqualTo(expectedIndex), 
            $"{longitude}° should return index {expectedIndex} for {method}");
    }

    [Test]
    [TestCase(0.0, ZodiacDivisionMethods.DecansPlanet, 4)] // Mars
    [TestCase(10.0, ZodiacDivisionMethods.DecansPlanet, 0)] // Sun
    [TestCase(20.0, ZodiacDivisionMethods.DecansPlanet, 3)] // Venus
    [TestCase(30.0, ZodiacDivisionMethods.DecansPlanet, 2)] // Mercury
    [TestCase(40.0, ZodiacDivisionMethods.DecansPlanet, 1)] // Moon
    [TestCase(50.0, ZodiacDivisionMethods.DecansPlanet, 6)] // Saturn
    [TestCase(60.0, ZodiacDivisionMethods.DecansPlanet, 5)] // Jupiter
    [TestCase(70.0, ZodiacDivisionMethods.DecansPlanet, 4)] // Mars (second cycle)
    [TestCase(180.0, ZodiacDivisionMethods.DecansPlanet, 1)] // Moon (third cycle)
    public void TestDecansPlanetMethod(double longitude, ZodiacDivisionMethods method, int expectedIndex)
    {
        var result = _zodiacDivisionsService.FindIndexForDivision(longitude, method);
        Assert.That(result, Is.EqualTo(expectedIndex), 
            $"{longitude}° should return index {expectedIndex} for {method}");
    }

    [Test]
    [TestCase(0.0, ZodiacDivisionMethods.DecansSign, 0)] // Aries first decan
    [TestCase(10.0, ZodiacDivisionMethods.DecansSign, 4)] // Leo (Aries second decan)
    [TestCase(20.0, ZodiacDivisionMethods.DecansSign, 8)] // Sagittarius (Aries third decan)
    [TestCase(30.0, ZodiacDivisionMethods.DecansSign, 1)] // Taurus first decan
    [TestCase(40.0, ZodiacDivisionMethods.DecansSign, 5)] // Virgo (Taurus second decan)
    [TestCase(50.0, ZodiacDivisionMethods.DecansSign, 9)] // Capricorn (Taurus third decan)
    [TestCase(180.0, ZodiacDivisionMethods.DecansSign, 6)] // Libra first decan
    public void TestDecansSignMethod(double longitude, ZodiacDivisionMethods method, int expectedIndex)
    {
        var result = _zodiacDivisionsService.FindIndexForDivision(longitude, method);
        Assert.That(result, Is.EqualTo(expectedIndex), 
            $"{longitude}° should return index {expectedIndex} for {method}");
    }

    

    [Test]
    [TestCase(0.0, ZodiacDivisionMethods.DodecatsOriginal, 0)] // Aries first subportion
    [TestCase(2.5, ZodiacDivisionMethods.DodecatsOriginal, 1)] // Taurus (Aries second subportion)
    [TestCase(5.0, ZodiacDivisionMethods.DodecatsOriginal, 2)] // Gemini (Aries third subportion)
    [TestCase(7.5, ZodiacDivisionMethods.DodecatsOriginal, 3)] // Cancer (Aries fourth subportion)
    [TestCase(30.0, ZodiacDivisionMethods.DodecatsOriginal, 1)] // Taurus first subportion
    [TestCase(32.5, ZodiacDivisionMethods.DodecatsOriginal, 2)] // Gemini (Taurus second subportion)
    [TestCase(180.0, ZodiacDivisionMethods.DodecatsOriginal, 6)] // Libra first subportion
    public void TestDodecatsOriginalMethod(double longitude, ZodiacDivisionMethods method, int expectedIndex)
    {
        var result = _zodiacDivisionsService.FindIndexForDivision(longitude, method);
        Assert.That(result, Is.EqualTo(expectedIndex), 
            $"{longitude}° should return index {expectedIndex} for {method}");
    }

    [Test]
    [TestCase(0.0, ZodiacDivisionMethods.DodecatsPaulus, 0)] // 0° × 13 = 0 → 0 ÷ 30 = 0 (Aries)
    [TestCase(30.0, ZodiacDivisionMethods.DodecatsPaulus, 1)] // 30° × 13 = 390 → 390 - 360 = 30 → 30 ÷ 30 = 1 (Taurus)
    [TestCase(45.0, ZodiacDivisionMethods.DodecatsPaulus, 7)] // 45° × 13 = 585 → 585 - 360 = 225 → 225 ÷ 30 = 7 (Libra)
    [TestCase(180.0, ZodiacDivisionMethods.DodecatsPaulus, 6)] // 180° × 13 = 2340 → 2340 - 6×360 = 180 → 180 ÷ 30 = 6 (Libra)
    public void TestDodecatsPaulusMethod(double longitude, ZodiacDivisionMethods method, int expectedIndex)
    {
        var result = _zodiacDivisionsService.FindIndexForDivision(longitude, method);
        Assert.That(result, Is.EqualTo(expectedIndex), 
            $"{longitude}° should return index {expectedIndex} for {method}");
    }

    [Test]
    [TestCase(0.0, ZodiacDivisionMethods.BoundsEgyptian, 5)] // Jupiter in Aries (Egyptian)
    [TestCase(6.0, ZodiacDivisionMethods.BoundsEgyptian, 3)] // Venus in Aries (Egyptian)
    [TestCase(12.0, ZodiacDivisionMethods.BoundsEgyptian, 2)] // Mercury in Aries (Egyptian)
    [TestCase(20.0, ZodiacDivisionMethods.BoundsEgyptian, 4)] // Mars in Aries (Egyptian)
    [TestCase(25.0, ZodiacDivisionMethods.BoundsEgyptian, 6)] // Saturn in Aries (Egyptian)
    [TestCase(180.0, ZodiacDivisionMethods.BoundsEgyptian, 6)] // Saturn in Libra (Egyptian)
    public void TestBoundsEgyptianMethod(double longitude, ZodiacDivisionMethods method, int expectedIndex)
    {
        var result = _zodiacDivisionsService.FindIndexForDivision(longitude, method);
        Assert.That(result, Is.EqualTo(expectedIndex), 
            $"{longitude}° should return index {expectedIndex} for {method}");
    }

    [Test]
    [TestCase(0.0, ZodiacDivisionMethods.BoundsPtolemy, 5)] // Jupiter in Aries (Ptolemy)
    [TestCase(6.0, ZodiacDivisionMethods.BoundsPtolemy, 3)] // Venus in Aries (Ptolemy)
    [TestCase(14.0, ZodiacDivisionMethods.BoundsPtolemy, 2)] // Mercury in Aries (Ptolemy)
    [TestCase(21.0, ZodiacDivisionMethods.BoundsPtolemy, 4)] // Mars in Aries (Ptolemy)
    [TestCase(26.0, ZodiacDivisionMethods.BoundsPtolemy, 6)] // Saturn in Aries (Ptolemy)
    [TestCase(180.0, ZodiacDivisionMethods.BoundsPtolemy, 6)] // Saturn in Libra (Ptolemy)
    public void TestBoundsPtolemyMethod(double longitude, ZodiacDivisionMethods method, int expectedIndex)
    {
        var result = _zodiacDivisionsService.FindIndexForDivision(longitude, method);
        Assert.That(result, Is.EqualTo(expectedIndex), 
            $"{longitude}° should return index {expectedIndex} for {method}");
    }

    [Test]
    [TestCase(-0.1, ZodiacDivisionMethods.Signs)]
    [TestCase(-1.0, ZodiacDivisionMethods.DecansPlanet)]
    [TestCase(-360.0, ZodiacDivisionMethods.DecansSign)]
    [TestCase(360.0, ZodiacDivisionMethods.DodecatsOriginal)]
    [TestCase(360.1, ZodiacDivisionMethods.DodecatsPaulus)]
    [TestCase(720.0, ZodiacDivisionMethods.BoundsEgyptian)]
    public void TestInvalidLongitudes(double longitude, ZodiacDivisionMethods method)
    {
        Assert.Throws<ArgumentException>(() => 
            _zodiacDivisionsService.FindIndexForDivision(longitude, method),
            $"Longitude {longitude} should throw ArgumentException for {method}");
    }

    [Test]
    public void TestBoundaryValues()
    {
        Assert.Multiple(() =>
        {
            // Test exact boundary values
            Assert.That(_zodiacDivisionsService.FindIndexForDivision(0.0, ZodiacDivisionMethods.Signs), Is.EqualTo(0), "0° should return Aries (0)");
            Assert.That(_zodiacDivisionsService.FindIndexForDivision(359.999, ZodiacDivisionMethods.Signs), Is.EqualTo(11), "359.999° should return Pisces (11)");
            
            // Test middle values
            Assert.That(_zodiacDivisionsService.FindIndexForDivision(180.0, ZodiacDivisionMethods.Signs), Is.EqualTo(6), "180° should return Libra (6)");
            Assert.That(_zodiacDivisionsService.FindIndexForDivision(90.0, ZodiacDivisionMethods.Signs), Is.EqualTo(3), "90° should return Cancer (3)");
            Assert.That(_zodiacDivisionsService.FindIndexForDivision(270.0, ZodiacDivisionMethods.Signs), Is.EqualTo(9), "270° should return Capricorn (9)");
        });
    }

    [Test]
    public void TestAllMethodsForSameLongitude()
    {
        const double longitude = 45.0;
        
        Assert.Multiple(() =>
        {
            Assert.That(_zodiacDivisionsService.FindIndexForDivision(longitude, ZodiacDivisionMethods.Signs), Is.EqualTo(1), "45° should return Taurus (1) for Signs");
            Assert.That(_zodiacDivisionsService.FindIndexForDivision(longitude, ZodiacDivisionMethods.DecansPlanet), Is.EqualTo(1), "45° should return Moon (1) for DecansPlanet");
            Assert.That(_zodiacDivisionsService.FindIndexForDivision(longitude, ZodiacDivisionMethods.DecansSign), Is.EqualTo(5), "45° should return Virgo (5) for DecansSign");
            Assert.That(_zodiacDivisionsService.FindIndexForDivision(longitude, ZodiacDivisionMethods.DodecatsOriginal), Is.EqualTo(7), "45° should return Libra (7) for DodecatsOriginal");
            Assert.That(_zodiacDivisionsService.FindIndexForDivision(longitude, ZodiacDivisionMethods.DodecatsPaulus), Is.EqualTo(7), "45° should return Libra (7) for DodecatsPaulus");
            Assert.That(_zodiacDivisionsService.FindIndexForDivision(longitude, ZodiacDivisionMethods.BoundsEgyptian), Is.EqualTo(5), "45° should return Jupiter (5) for BoundsEgyptian");
            Assert.That(_zodiacDivisionsService.FindIndexForDivision(longitude, ZodiacDivisionMethods.BoundsPtolemy), Is.EqualTo(5), "45° should return Jupiter (5) for BoundsPtolemy");
        });
    }

    
    //  Mercury 2, Venus 3, Mars 4, Jupiter 5, Saturn 6
    [Test]
    [TestCase(309.0, 3)]        // Venus
    [TestCase(121.75, 5)]       // Jupiter
    [TestCase(305.9, 2)]        // Mercury
    [TestCase(356.0, 4)]        // Mars
    [TestCase(352.6, 4)]        // Mars
    [TestCase(41.95, 2)]        // Mercury
    [TestCase(207.25, 3)]       // Venus
    [TestCase(105.55, 2)]       // Mercury
    [TestCase(203.85, 3)]       // Venus
    [TestCase(142.35, 2)]       // Mercury
    [TestCase(286.3, 3)]        // Venus
    [TestCase(154.2, 2)]        // Mercury
    [TestCase(150.75, 2)]       // Mercury
    [TestCase(127.35, 3)]       // Venus
    [TestCase(314.73, 5)]       // Jupiter
    [TestCase(251.1, 5 )]       // Jupiter
    [TestCase(163.3, 3)]        // Venus
    
    public void TestRangeOfLongitudesForEgyptianBounds(double longitude, int expectedIndex)
    {
        var method = ZodiacDivisionMethods.BoundsEgyptian;
        var result = _zodiacDivisionsService.FindIndexForDivision(longitude, method);
        Assert.That(result, Is.EqualTo(expectedIndex), 
            $"{longitude}° should return index {expectedIndex} for {method}");
    }
    
    //  Mercury 2, Venus 3, Mars 4, Jupiter 5, Saturn 6
    [Test]
    [TestCase(309.0, 2)]        // Mercury 
    [TestCase(121.75, 5)]       // Jupiter 
    [TestCase(305.9, 6)]        // Saturn 
    [TestCase(356.0, 6)]        // Saturn
    [TestCase(352.6, 4)]        // Mars 
    [TestCase(41.95, 2)]        // Mercury
    [TestCase(207.25, 4)]       // Mars
    [TestCase(105.55, 2)]       // Mercury
    [TestCase(203.85, 5)]       // Jupiter
    [TestCase(142.35, 3)]       // Venus 
    [TestCase(286.3, 5)]        // Jupiter
    [TestCase(154.2, 2)]        // Mercury 
    [TestCase(150.75, 2)]       // Mercury
    [TestCase(127.35, 2)]       // Mercury
    [TestCase(314.73, 3)]       // Venus
    [TestCase(251.1, 3)]        // Venus
    [TestCase(163.3, 5)]        // Jupiter
    
    public void TestRangeOfLongitudesForPtolemyBounds(double longitude, int expectedIndex)
    {
        var method = ZodiacDivisionMethods.BoundsPtolemy;
        var result = _zodiacDivisionsService.FindIndexForDivision(longitude, method);
        Assert.That(result, Is.EqualTo(expectedIndex), 
            $"{longitude}° should return index {expectedIndex} for {method}");
    }
    
    //  0..11  Aries..Pisces
    [Test]
    [TestCase(309.0, 10)]        // Aquarius  
    [TestCase(121.75, 4)]        // Leo  
    [TestCase(305.9, 10)]        // Aquarius  
    [TestCase(356.0, 7)]         // Scorpio 
    [TestCase(352.6, 7)]         // Scorpio 
    [TestCase(41.95, 5)]         // Virgo 
    [TestCase(207.25, 2)]        // Gemini 
    [TestCase(105.55, 7)]        // Scorpio 
    [TestCase(203.85, 2)]        // Gemini  
    [TestCase(142.35, 0)]        // Aries  
    [TestCase(286.3, 1)]         // Taurus 
    [TestCase(154.2, 5)]         // Virgo  
    [TestCase(150.75, 5)]        // Virgo 
    [TestCase(127.35, 4)]        // Leo
    [TestCase(314.73, 2)]        // Gemini 
    [TestCase(251.1, 0)]         // Aries 
    [TestCase(163.3, 9)]         // Capricorn 
    
    
    
    
    // Sun 0, Moon 1, Mercury 2, Venus 3, Mars 4, Jupiter 5, Saturn 6
    // the first portion shold get the number 4, followed by 5, 6, 0, 2, 3, 1 etc.
    // Bouché-Leclercq p. 228: Mars, Sun, Venus, Mercury, Moon, Saturn, Jupiter etc
    // 4, 0, 3, 2, 1, 6, 5
    // Aries    4, 0, 3
    // Taurus   2, 1, 6
    // Gemini   5, 4, 0
    // Cancer   3, 2, 1
    // Leo      6, 5, 4
    // Virgo    0, 3, 2
    // Libra    1, 6, 5
    // Scorpio  4, 0, 3
    // Sagitt.  2, 1, 6
    // Capric.  5, 4, 0
    // Aquarius 3, 2, 1
    // Pisces   6, 5, 4
    
    
    public void TestRangeOfLongitudesForDecansSign(double longitude, int expectedIndex)
    {
        var method = ZodiacDivisionMethods.DecansSign;
        var result = _zodiacDivisionsService.FindIndexForDivision(longitude, method);
        Assert.That(result, Is.EqualTo(expectedIndex), 
            $"{longitude}° should return index {expectedIndex} for {method}");
    }
    
    [Test]
    [TestCase(309.0, 3)]        // Venus  
    [TestCase(121.75, 6)]       // Saturn  
    [TestCase(305.9, 3)]        // Venus   
    [TestCase(356.0, 4)]        // Mars 
    [TestCase(352.6, 4)]        // Mars 
    [TestCase(41.95, 1)]        // Moon
    [TestCase(207.25, 5)]       // Jupiter
    [TestCase(105.55, 2)]       // Mercury
    [TestCase(203.85, 5)]       // Jupiter  
    [TestCase(142.35, 4)]       // Mars  
    [TestCase(286.3, 4)]        // Mars 
    [TestCase(154.2, 0)]        // Sun 
    [TestCase(150.75, 0)]       // Sun 
    [TestCase(127.35, 6)]       // Saturn
    [TestCase(314.73, 2)]       // Mercury 
    [TestCase(251.1, 1)]        // Moon 
    [TestCase(163.3, 3)]        // Venus 
    
    public void TestRangeOfLongitudesForDecansPlanet(double longitude, int expectedIndex)
    {
        var method = ZodiacDivisionMethods.DecansPlanet;
        var result = _zodiacDivisionsService.FindIndexForDivision(longitude, method);
        Assert.That(result, Is.EqualTo(expectedIndex), 
            $"{longitude}° should return index {expectedIndex} for {method}");
    }
    
    
    [Test]
    [TestCase(309.0, 1)]        // Taurus   
    [TestCase(121.75, 4)]       // Leo   
    [TestCase(305.9, 0)]        // Aries    
    [TestCase(356.0, 9)]        // Capricorn  
    [TestCase(352.6, 8)]        // Sagittarius  
    [TestCase(41.95, 5)]        // Virgo 
    [TestCase(207.25, 4)]       // Leo 
    [TestCase(105.55, 9)]       // Capricorn 
    [TestCase(203.85, 3)]       // Cancer   
    [TestCase(142.35, 0)]       // Aries  
    [TestCase(286.3, 3)]        // Cancer  
    [TestCase(154.2, 6)]        // Libra  
    [TestCase(150.75, 5)]       // Virgo  
    [TestCase(127.35, 6)]       // Libra 
    [TestCase(314.73, 3)]       // Cancer  
    [TestCase(251.1, 0)]        // Aries  
    [TestCase(163.3, 10)]       // Aquarius  
    
    public void TestRangeOfLongitudesForDodecatsOriginal(double longitude, int expectedIndex)
    {
        var method = ZodiacDivisionMethods.DodecatsOriginal;
        var result = _zodiacDivisionsService.FindIndexForDivision(longitude, method);
        Assert.That(result, Is.EqualTo(expectedIndex), 
            $"{longitude}° should return index {expectedIndex} for {method}");
    }
    
    [Test]
    [TestCase(309.0, 1)]        // Taurus    
    [TestCase(121.75, 4)]       // Leo   
    [TestCase(305.9, 0)]        // Aries     
    [TestCase(356.0, 10)]       // Aquarius   
    [TestCase(352.6, 8)]        // Sagittarius   
    [TestCase(41.95, 6)]        // Libra  
    [TestCase(207.25, 5)]       // Virgo  
    [TestCase(105.55, 9)]       // Capricorn  
    [TestCase(203.85, 4)]       // Leo    
    [TestCase(142.35, 1)]       // Taurus   
    [TestCase(286.3, 4)]        // Leo   
    [TestCase(154.2, 6)]        // Libra   
    [TestCase(150.75, 5)]       // Virgo   
    [TestCase(127.35, 7)]       // Scorpio  
    [TestCase(314.73, 4)]       // Leo  
    [TestCase(251.1, 0)]        // Aries  
    [TestCase(163.3, 10)]       // Aquarius   
    
    public void TestRangeOfLongitudesForDodecatsPaulus(double longitude, int expectedIndex)
    {
        var method = ZodiacDivisionMethods.DodecatsPaulus;
        var result = _zodiacDivisionsService.FindIndexForDivision(longitude, method);
        Assert.That(result, Is.EqualTo(expectedIndex), 
            $"{longitude}° should return index {expectedIndex} for {method}");
    }
    
    [Test]
    [TestCase(309.0,10 )]     // Aquarius     
    [TestCase(121.75, 4)]     // Leo    
    [TestCase(305.9, 10)]     // Aquarius      
    [TestCase(356.0, 11)]     // Pisces    
    [TestCase(352.6, 11)]     // Pisces    
    [TestCase(41.95, 1)]      // Taurus   
    [TestCase(207.25, 6)]     // Libra   
    [TestCase(105.55, 3)]     // Cancer   
    [TestCase(203.85, 6)]     // Libra     
    [TestCase(142.35, 4)]     // Leo    
    [TestCase(286.3, 9)]      // Capricorn   
    [TestCase(154.2, 5)]      // Virgo    
    [TestCase(150.75, 5)]     // Virgo    
    [TestCase(127.35, 4)]     // Leo   
    [TestCase(314.73, 10)]    // Aquarius   
    [TestCase(251.1, 8)]      // Sagittarius   
    [TestCase(163.3, 5)]      // Virgo    
    
    public void TestRangeOfLongitudesForSigns(double longitude, int expectedIndex)
    {
        var method = ZodiacDivisionMethods.Signs;
        var result = _zodiacDivisionsService.FindIndexForDivision(longitude, method);
        Assert.That(result, Is.EqualTo(expectedIndex), 
            $"{longitude}° should return index {expectedIndex} for {method}");
    }
} 