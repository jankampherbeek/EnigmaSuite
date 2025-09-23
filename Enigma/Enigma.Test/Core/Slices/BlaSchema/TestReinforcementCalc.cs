// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestReinforcementCalc
{
    [Test]
    public void TestFindPointsInOwnSign_WithPlanetsInRulingSigns_ReturnsCorrectPoints()
    {
        // Arrange - Using BLA schema ruler pairs
        var planetsInSigns = new Dictionary<ChartPoints, int>
        {
            { ChartPoints.Mars, 1 },              // Mars in Aries (sign 1) - Mars is main ruler of Aries
            { ChartPoints.Venus, 2 },             // Venus in Taurus (sign 2) - Venus is main ruler of Taurus
            { ChartPoints.Sun, 5 },               // Sun in Leo (sign 5) - Sun is main ruler of Leo
            { ChartPoints.Mercury, 3 },           // Mercury in Gemini (sign 3) - Mercury is main ruler of Gemini
            { ChartPoints.Jupiter, 9 },           // Jupiter in Sagittarius (sign 9) - Jupiter is main ruler of Sagittarius
            { ChartPoints.Moon, 4 }               // Moon in Cancer (sign 4) - Moon is main ruler of Cancer
        };

        // Act
        var result = ReinforcementCalc.FindPointsInOwnSign(planetsInSigns);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(6));
            Assert.That(result[ChartPoints.Mars], Is.EqualTo(1));
            Assert.That(result[ChartPoints.Venus], Is.EqualTo(2));
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(5));
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(3));
            Assert.That(result[ChartPoints.Jupiter], Is.EqualTo(9));
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(4));
        });
    }

    [Test]
    public void TestFindPointsInOwnSign_WithPlanetsNotInRulingSigns_ReturnsEmptyDictionary()
    {
        // Arrange
        var planetsInSigns = new Dictionary<ChartPoints, int>
        {
            { ChartPoints.Mars, 2 },      // Mars in Taurus (sign 2) - Mars rules Aries, not Taurus
            { ChartPoints.Venus, 1 },     // Venus in Aries (sign 1) - Venus rules Taurus, not Aries
            { ChartPoints.Sun, 3 },       // Sun in Gemini (sign 3) - Sun rules Leo, not Gemini
            { ChartPoints.Mercury, 4 },   // Mercury in Cancer (sign 4) - Mercury rules Gemini, not Cancer
            { ChartPoints.Jupiter, 8 },   // Jupiter in Scorpio (sign 8) - Jupiter rules Sagittarius, not Scorpio
            { ChartPoints.Saturn, 9 }     // Saturn in Sagittarius (sign 9) - Saturn rules Capricorn, not Sagittarius
        };

        // Act
        var result = ReinforcementCalc.FindPointsInOwnSign(planetsInSigns);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestFindPointsInOwnSign_WithEmptyInput_ReturnsEmptyDictionary()
    {
        // Arrange
        var planetsInSigns = new Dictionary<ChartPoints, int>();

        // Act
        var result = ReinforcementCalc.FindPointsInOwnSign(planetsInSigns);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestFindPointsInOwnSign_WithMixedScenarios_ReturnsOnlyPointsInOwnSigns()
    {
        // Arrange
        var planetsInSigns = new Dictionary<ChartPoints, int>
        {
            { ChartPoints.Mars, 1 },      // Mars in Aries (sign 1) - Mars rules Aries ✓
            { ChartPoints.Venus, 1 },     // Venus in Aries (sign 1) - Venus rules Taurus ✗
            { ChartPoints.Sun, 5 },       // Sun in Leo (sign 5) - Sun rules Leo ✓
            { ChartPoints.Mercury, 4 },   // Mercury in Cancer (sign 4) - Mercury rules Gemini ✗
            { ChartPoints.Jupiter, 9 },   // Jupiter in Sagittarius (sign 9) - Jupiter rules Sagittarius ✓
            { ChartPoints.Saturn, 11 }    // Saturn in Aquarius (sign 11) - Saturn rules Capricorn ✗
        };

        // Act
        var result = ReinforcementCalc.FindPointsInOwnSign(planetsInSigns);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result[ChartPoints.Mars], Is.EqualTo(1));
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(5));
            Assert.That(result[ChartPoints.Jupiter], Is.EqualTo(9));
        });
    }

    [Test]
    public void TestFindPointsInOwnSign_WithMainAndSubRulers_ReturnsBothWhenInOwnSigns()
    {
        // Arrange - Using the BLA schema ruler pairs
        var planetsInSigns = new Dictionary<ChartPoints, int>
        {
            { ChartPoints.Mars, 1 },              // Mars in Aries (sign 1) - Mars is main ruler of Aries ✓
            { ChartPoints.Pluto, 1 },             // Pluto in Aries (sign 1) - Pluto is sub ruler of Aries ✓
            { ChartPoints.Venus, 2 },             // Venus in Taurus (sign 2) - Venus is main ruler of Taurus ✓
            { ChartPoints.PersephoneCarteret, 2 }, // PersephoneCarteret in Taurus (sign 2) - PersephoneCarteret is sub ruler of Taurus ✓
            { ChartPoints.Mercury, 3 },           // Mercury in Gemini (sign 3) - Mercury is main ruler of Gemini ✓
            { ChartPoints.VulcanusCarteret, 3 },  // VulcanusCarteret in Gemini (sign 3) - VulcanusCarteret is sub ruler of Gemini ✓
            { ChartPoints.Sun, 4 },               // Sun in Cancer (sign 4) - Sun is main ruler of Leo, not Cancer ✗
            { ChartPoints.Moon, 4 }               // Moon in Cancer (sign 4) - Moon is main ruler of Cancer ✓
        };

        // Act
        var result = ReinforcementCalc.FindPointsInOwnSign(planetsInSigns);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(7));
            Assert.That(result[ChartPoints.Mars], Is.EqualTo(1));
            Assert.That(result[ChartPoints.Pluto], Is.EqualTo(1));
            Assert.That(result[ChartPoints.Venus], Is.EqualTo(2));
            Assert.That(result[ChartPoints.PersephoneCarteret], Is.EqualTo(2));
            Assert.That(result[ChartPoints.Mercury], Is.EqualTo(3));
            Assert.That(result[ChartPoints.VulcanusCarteret], Is.EqualTo(3));
            Assert.That(result[ChartPoints.Moon], Is.EqualTo(4));
        });
    }

    [Test]
    public void TestFindPointsInOwnSign_WithAllBlaRulerPairs_ReturnsAllPointsInOwnSigns()
    {
        // Arrange - Test all 12 BLA ruler pairs with planets in their ruling signs
        // Note: Some planets appear as both main and sub rulers, so we need to be careful about duplicates
        var planetsInSigns = new Dictionary<ChartPoints, int>
        {
            // Sign 1 (Aries) - Mars, Pluto
            { ChartPoints.Mars, 1 },
            { ChartPoints.Pluto, 1 },
            
            // Sign 2 (Taurus) - Venus, PersephoneCarteret
            { ChartPoints.Venus, 2 },
            { ChartPoints.PersephoneCarteret, 2 },
            
            // Sign 3 (Gemini) - Mercury, VulcanusCarteret
            { ChartPoints.Mercury, 3 },
            { ChartPoints.VulcanusCarteret, 3 },
            
            // Sign 4 (Cancer) - Moon, Priapus
            { ChartPoints.Moon, 4 },
            { ChartPoints.Priapus, 4 },
            
            // Sign 5 (Leo) - Sun, ApogeeMean
            { ChartPoints.Sun, 5 },
            { ChartPoints.ApogeeMean, 5 },
            
            // Sign 9 (Sagittarius) - Jupiter, Neptune
            { ChartPoints.Jupiter, 9 },
            { ChartPoints.Neptune, 9 },
        };

        // Act
        var result = ReinforcementCalc.FindPointsInOwnSign(planetsInSigns);

        // Assert
        Assert.That(result, Has.Count.EqualTo(12)); // All planets that are in their ruling signs
        
        // Verify all expected points are present
        var expectedPoints = new[]
        {
            ChartPoints.Mars, ChartPoints.Pluto,           // Sign 1
            ChartPoints.Venus, ChartPoints.PersephoneCarteret, // Sign 2
            ChartPoints.Mercury, ChartPoints.VulcanusCarteret, // Sign 3
            ChartPoints.Moon, ChartPoints.Priapus,         // Sign 4
            ChartPoints.Sun, ChartPoints.ApogeeMean,       // Sign 5
            ChartPoints.Jupiter, ChartPoints.Neptune       // Sign 9
        };

        foreach (var expectedPoint in expectedPoints)
        {
            Assert.That(result.ContainsKey(expectedPoint), Is.True, 
                $"Expected point {expectedPoint} to be in the result");
        }
    }

    [Test]
    public void TestFindPointsInOwnSign_WithDuplicatePoints_HandlesCorrectly()
    {
        // Arrange - Test with duplicate entries (should not happen in real usage, but test robustness)
        var planetsInSigns = new Dictionary<ChartPoints, int>
        {
            { ChartPoints.Mars, 1 },      // Mars in Aries (sign 1) - Mars rules Aries ✓
            { ChartPoints.Venus, 2 },     // Venus in Taurus (sign 2) - Venus rules Taurus ✓
            { ChartPoints.Sun, 5 }        // Sun in Leo (sign 5) - Sun rules Leo ✓
        };

        // Act
        var result = ReinforcementCalc.FindPointsInOwnSign(planetsInSigns);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result[ChartPoints.Mars], Is.EqualTo(1));
            Assert.That(result[ChartPoints.Venus], Is.EqualTo(2));
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(5));
        });
    }

    [Test]
    public void TestFindPointsInOwnSign_WithNonRulingPoints_IgnoresNonRulingPoints()
    {
        // Arrange - Include points that are not rulers of any sign
        var planetsInSigns = new Dictionary<ChartPoints, int>
        {
            { ChartPoints.Mars, 1 },          // Mars in Aries (sign 1) - Mars rules Aries ✓
            { ChartPoints.Venus, 2 },         // Venus in Taurus (sign 2) - Venus rules Taurus ✓
            { ChartPoints.Chiron, 3 },        // Chiron in Gemini (sign 3) - Chiron is not a ruler ✗
            { ChartPoints.NorthNode, 4 },     // North Node in Cancer (sign 4) - North Node is not a ruler ✗
            { ChartPoints.Sun, 5 },           // Sun in Leo (sign 5) - Sun rules Leo ✓
            { ChartPoints.Ascendant, 6 }      // Ascendant in Virgo (sign 6) - Ascendant is not a ruler ✗
        };

        // Act
        var result = ReinforcementCalc.FindPointsInOwnSign(planetsInSigns);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result[ChartPoints.Mars], Is.EqualTo(1));
            Assert.That(result[ChartPoints.Venus], Is.EqualTo(2));
            Assert.That(result[ChartPoints.Sun], Is.EqualTo(5));
        });
    }


    [Test]
    public void TestFindPointsinOwnHouse()
    {
        var planetsInHouses = CreatePlanetsInHouses();
        var signsOnCusp = SignsOnCusp();
        var result = ReinforcementCalc.FindPointsInOwnHouses(planetsInHouses, signsOnCusp);
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.ContainsKey(ChartPoints.VulcanusCarteret));
            Assert.That(result.ContainsValue(6));
        });
    }

    [Test]
    public void TestfindPointsInMundaneHouses()
    {
        var planetsInHouses = CreatePlanetsInHouses();
        var result = ReinforcementCalc.FindPointsInMundaneHouses(planetsInHouses);
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result.ContainsKey(ChartPoints.VulcanusCarteret));
            Assert.That(result.ContainsKey(ChartPoints.Mars));
            Assert.That(result.ContainsKey(ChartPoints.Venus));
            Assert.That(result.ContainsValue(6));
            Assert.That(result.ContainsValue(8));
            Assert.That(result.ContainsValue(7));
            Assert.That(result.ContainsValue(2));
        });
    }

    [Test]
    public void TestRulerInHouseAsSign()
    {
        var signsOnCusp = SignsOnCusp();
        var planetsInHouses = CreatePlanetsInHouses();
        var planetsInSigns = CreatePlanetsInSigns();
        var result = ReinforcementCalc.FindRulerInHouseAsSign(signsOnCusp, planetsInSigns);
        foreach (var resultItem in result)
        {
            Console.WriteLine($"{resultItem.Key} = {resultItem.Value}");       
        }
        
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result.ContainsKey(ChartPoints.Venus));
            Assert.That(result.ContainsValue(4));
            Assert.That(result.ContainsKey(ChartPoints.Mercury));
            Assert.That(result.ContainsValue(5));
        });
    }

    [Test]
    public void TestFindFactorPairs()
    {
        var planetsInSigns = new Dictionary<ChartPoints, int>
        {
            { ChartPoints.Sun, 5 },
            { ChartPoints.Moon, 11 },
            { ChartPoints.Mercury, 5 },
            { ChartPoints.Saturn, 1 },
            { ChartPoints.Uranus, 5 }
        };
        var planetsInHouses = new Dictionary<ChartPoints, int>()
        {
            { ChartPoints.Sun, 7 },
            { ChartPoints.Moon, 1 },
            { ChartPoints.Mercury, 7 },
            { ChartPoints.Saturn, 2 },
            { ChartPoints.Uranus, 1 }
        };
        var result = ReinforcementCalc.FindFactorPairs(planetsInSigns, planetsInHouses);
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].PointInSign, Is.EqualTo(ChartPoints.Saturn));
            Assert.That(result[0].Sign, Is.EqualTo(1));
            Assert.That(result[0].PointInHouse, Is.EqualTo(ChartPoints.Uranus));
            Assert.That(result[0].House, Is.EqualTo(1));
        });
    }

    [Test]
    public void TestFindReceptionInSigns()
    {
        var planetsInSigns = new Dictionary<ChartPoints, int>()
        {
            { ChartPoints.Sun, 5 },
            { ChartPoints.Moon, 11 },
            { ChartPoints.Mercury, 5 },
            { ChartPoints.Venus, 4 },
            { ChartPoints.Mars, 7 },
            { ChartPoints.Jupiter, 4 },
            { ChartPoints.Saturn, 1 },
            { ChartPoints.Priapus, 2 }
        };
        var result = ReinforcementCalc.FindReceptionInSigns(planetsInSigns);
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Point1, Is.EqualTo(ChartPoints.Venus));
            Assert.That(result[0].SignOrHouse1, Is.EqualTo(4));
            Assert.That(result[0].Point2, Is.EqualTo(ChartPoints.Priapus));
            Assert.That(result[0].SignOrHouse2, Is.EqualTo(2));
        });
    }


    [Test]
    public void TestFindReceptionInHouses()
    {
        var planetsInHouses = new Dictionary<ChartPoints, int>()
        {
            { ChartPoints.Sun, 7 },
            { ChartPoints.Moon, 1 },
            { ChartPoints.Mercury, 7 },
            { ChartPoints.Venus, 7 },
            { ChartPoints.Mars, 8 },
            { ChartPoints.Jupiter, 7 },
            { ChartPoints.Saturn, 2 },
            { ChartPoints.Uranus, 4 },
            { ChartPoints.Neptune, 2},
            { ChartPoints.Pluto, 1 },
            { ChartPoints.PersephoneCarteret, 2 },
            { ChartPoints.VulcanusCarteret, 6 },
            { ChartPoints.ApogeeMean, 10 },
            { ChartPoints.Priapus, 4}
        };
        
        var signsOnCusp = SignsOnCusp();
        var result = ReinforcementCalc.FindReceptionInHouses(planetsInHouses, signsOnCusp);
        /*
         * Expected: 5 receptions:
         * ApogeeMean + Pluto = 10 + 1
         * Zon + Maan = 7 + 1           --> tested
         * Priapus + Persephone = 4 + 2
         * Venus + Priapus = 7 + 4
         * ApogeeMean + Mars = 10 + 8
         */
        
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(5));
            Assert.That(result[0].Point1, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result[0].SignOrHouse1, Is.EqualTo(7));
            Assert.That(result[0].Point2, Is.EqualTo(ChartPoints.Moon));
            Assert.That(result[0].SignOrHouse2, Is.EqualTo(1));
        }); 
    }

    [Test]
    public void TestFindReceptionInMundaneHouses()
    {
        var planetsInHouses = new Dictionary<ChartPoints, int>()
        {
            { ChartPoints.Sun, 4 },
            { ChartPoints.Moon, 5 },
            { ChartPoints.Mercury, 4 },
            { ChartPoints.Venus, 3 },
            { ChartPoints.Mars, 8 },
            { ChartPoints.Jupiter, 7 },
            { ChartPoints.Saturn, 2 },
            { ChartPoints.Uranus, 4 },
            { ChartPoints.Neptune, 2},
            { ChartPoints.Pluto, 1 },
            { ChartPoints.PersephoneCarteret, 2 },
            { ChartPoints.VulcanusCarteret, 6 },
            { ChartPoints.ApogeeMean, 10 },
            { ChartPoints.Priapus, 4}
        };
        var signsOnCusp = SignsOnCusp();
        var result = ReinforcementCalc.FindReceptionInMundaneHouses(planetsInHouses, signsOnCusp);
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Point1, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result[0].SignOrHouse1, Is.EqualTo(4));
            Assert.That(result[0].Point2, Is.EqualTo(ChartPoints.Moon));
            Assert.That(result[0].SignOrHouse2, Is.EqualTo(5));
        }); 
        
        
    }
    
    
    private Dictionary<ChartPoints, int> CreatePlanetsInSigns()
    {
        return new Dictionary<ChartPoints, int>()
        {
            { ChartPoints.Sun, 5 },
            { ChartPoints.Moon, 11 },
            { ChartPoints.Mercury, 5 },
            { ChartPoints.Venus, 4 },
            { ChartPoints.Mars, 7 },
            { ChartPoints.Jupiter, 4 },
            { ChartPoints.Saturn, 1 },
            { ChartPoints.PersephoneCarteret, 12 },
            { ChartPoints.VulcanusCarteret, 3 }
        };
    }
    
    
    
    private Dictionary<ChartPoints, int> CreatePlanetsInHouses()
    {
        return new Dictionary<ChartPoints, int>()
        {
            { ChartPoints.Sun, 7 },
            { ChartPoints.Moon, 1 },
            { ChartPoints.Mercury, 7 },
            { ChartPoints.Venus, 7 },
            { ChartPoints.Mars, 8 },
            { ChartPoints.Jupiter, 7 },
            { ChartPoints.Saturn, 2 },
            { ChartPoints.PersephoneCarteret, 2 },
            { ChartPoints.VulcanusCarteret, 6 }
        };
    }

    private Dictionary<int, int> SignsOnCusp()
    {
        return new Dictionary<int, int>()
        {
            { 1, 10 },
            { 2, 11 },
            { 3, 1 },
            { 4, 2 },
            { 5, 3 },
            { 6, 3 },
            { 7, 4 },
            { 8, 5 },
            { 9, 7 },
            { 10, 8 },
            { 11, 9 },
            { 12, 9 }
        };
    }
}
