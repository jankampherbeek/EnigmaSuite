// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.Enneagram;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Slices.Enneagram;

[TestFixture]
public class TestEnneagramCalc
{
    private EnneagramCalc _enneagramCalc = null!;
    private List<EnneagramData> _signsData = null!;
    private List<EnneagramData> _housesData = null!;
    private List<KeyValuePair<ChartPoints, double>> _chartPoints = null!;
    private double[] _houses = null!;

    [SetUp]
    public void SetUp()
    {
        _enneagramCalc = new EnneagramCalc();
        
        // Create test data for signs (planets in different signs)
        _signsData = new List<EnneagramData>
        {
            // Sun in Aries (sign 1) - factors for Enneagram types 1-9
            new EnneagramData((int)ChartPoints.Sun, 1, new double[] { 1.5, 2.0, 1.0, 1.2, 0.8, 1.1, 1.3, 0.9, 1.4 }),
            // Moon in Taurus (sign 2)
            new EnneagramData((int)ChartPoints.Moon, 2, new double[] { 1.1, 1.8, 1.2, 0.9, 1.5, 1.0, 1.4, 1.1, 0.8 }),
            // Mercury in Gemini (sign 3)
            new EnneagramData((int)ChartPoints.Mercury, 3, new double[] { 1.3, 1.0, 1.6, 1.1, 0.9, 1.2, 1.0, 1.3, 1.1 }),
            // Venus in Cancer (sign 4)
            new EnneagramData((int)ChartPoints.Venus, 4, new double[] { 0.9, 1.2, 1.1, 1.7, 1.0, 1.3, 1.1, 0.8, 1.2 }),
            // Mars in Leo (sign 5)
            new EnneagramData((int)ChartPoints.Mars, 5, new double[] { 1.2, 1.1, 0.9, 1.0, 1.8, 1.1, 1.2, 1.0, 1.3 }),
            // Jupiter in Virgo (sign 6)
            new EnneagramData((int)ChartPoints.Jupiter, 6, new double[] { 1.0, 1.3, 1.2, 1.1, 1.0, 1.9, 1.1, 1.2, 1.0 }),
            // Saturn in Libra (sign 7)
            new EnneagramData((int)ChartPoints.Saturn, 7, new double[] { 1.4, 1.0, 1.1, 1.2, 1.1, 1.0, 1.8, 1.1, 1.2 }),
            // Uranus in Scorpio (sign 8)
            new EnneagramData((int)ChartPoints.Uranus, 8, new double[] { 1.1, 1.2, 1.0, 1.1, 1.2, 1.1, 1.0, 1.9, 1.1 }),
            // Neptune in Sagittarius (sign 9)
            new EnneagramData((int)ChartPoints.Neptune, 9, new double[] { 1.2, 1.1, 1.3, 1.0, 1.1, 1.2, 1.1, 1.0, 1.8 }),
            // Pluto in Capricorn (sign 10)
            new EnneagramData((int)ChartPoints.Pluto, 10, new double[] { 1.0, 1.1, 1.2, 1.3, 1.1, 1.0, 1.2, 1.1, 1.0 }),
            // Ascendant in Aries (sign 1) - ID 1001
            new EnneagramData(1001, 1, new double[] { 1.6, 1.0, 1.1, 1.2, 1.0, 1.1, 1.0, 1.1, 1.0 }),
            // MC in Cancer (sign 4) - ID 1002
            new EnneagramData(1002, 4, new double[] { 1.0, 1.1, 1.0, 1.8, 1.1, 1.0, 1.1, 1.0, 1.1 })
        };
        
        // Create test data for houses (cusps in different signs)
        _housesData = new List<EnneagramData>
        {
            // Cusp 1 in Aries (sign 1) - ID 2001
            new EnneagramData(2001, 1, new double[] { 1.7, 1.0, 1.1, 1.0, 1.1, 1.0, 1.1, 1.0, 1.1 }),
            // Cusp 2 in Taurus (sign 2) - ID 2002
            new EnneagramData(2002, 2, new double[] { 1.0, 1.9, 1.1, 1.0, 1.1, 1.0, 1.1, 1.0, 1.1 }),
            // Cusp 3 in Gemini (sign 3) - ID 2003
            new EnneagramData(2003, 3, new double[] { 1.1, 1.0, 1.8, 1.1, 1.0, 1.1, 1.0, 1.1, 1.0 }),
            // Cusp 4 in Cancer (sign 4) - ID 2004
            new EnneagramData(2004, 4, new double[] { 1.0, 1.1, 1.0, 1.9, 1.1, 1.0, 1.1, 1.0, 1.1 }),
            // Cusp 5 in Leo (sign 5) - ID 2005
            new EnneagramData(2005, 5, new double[] { 1.1, 1.0, 1.1, 1.0, 1.8, 1.1, 1.0, 1.1, 1.0 }),
            // Cusp 6 in Virgo (sign 6) - ID 2006
            new EnneagramData(2006, 6, new double[] { 1.0, 1.1, 1.0, 1.1, 1.0, 1.9, 1.1, 1.0, 1.1 }),
            // Cusp 7 in Libra (sign 7) - ID 2007
            new EnneagramData(2007, 7, new double[] { 1.1, 1.0, 1.1, 1.0, 1.1, 1.0, 1.8, 1.1, 1.0 }),
            // Cusp 8 in Scorpio (sign 8) - ID 2008
            new EnneagramData(2008, 8, new double[] { 1.0, 1.1, 1.0, 1.1, 1.0, 1.1, 1.0, 1.9, 1.1 }),
            // Cusp 9 in Sagittarius (sign 9) - ID 2009
            new EnneagramData(2009, 9, new double[] { 1.1, 1.0, 1.1, 1.0, 1.1, 1.0, 1.1, 1.0, 1.8 }),
            // Cusp 10 in Capricorn (sign 10) - ID 2010
            new EnneagramData(2010, 10, new double[] { 1.0, 1.1, 1.0, 1.1, 1.0, 1.1, 1.0, 1.1, 1.0 }),
            // Cusp 11 in Aquarius (sign 11) - ID 2011
            new EnneagramData(2011, 11, new double[] { 1.1, 1.0, 1.1, 1.0, 1.1, 1.0, 1.1, 1.0, 1.1 }),
            // Cusp 12 in Pisces (sign 12) - ID 2012
            new EnneagramData(2012, 12, new double[] { 1.0, 1.1, 1.0, 1.1, 1.0, 1.1, 1.0, 1.1, 1.0 })
        };
        
        // Create test chart points with longitudes that correspond to the signs above
        _chartPoints = new List<KeyValuePair<ChartPoints, double>>
        {
            new(ChartPoints.Sun, 15.0),      // Aries (0-30°)
            new(ChartPoints.Moon, 45.0),     // Taurus (30-60°)
            new(ChartPoints.Mercury, 75.0),  // Gemini (60-90°)
            new(ChartPoints.Venus, 105.0),   // Cancer (90-120°)
            new(ChartPoints.Mars, 135.0),    // Leo (120-150°)
            new(ChartPoints.Jupiter, 165.0), // Virgo (150-180°)
            new(ChartPoints.Saturn, 195.0),  // Libra (180-210°)
            new(ChartPoints.Uranus, 225.0),  // Scorpio (210-240°)
            new(ChartPoints.Neptune, 255.0), // Sagittarius (240-270°)
            new(ChartPoints.Pluto, 285.0)    // Capricorn (270-300°)
        };
        
        // Create test houses array (13 elements, index 0 ignored)
        _houses = new double[]
        {
            0.0,    // Index 0 - ignored
            15.0,   // Index 1 - Ascendant in Aries
            45.0,   // Index 2 - Cusp 2 in Taurus
            75.0,   // Index 3 - Cusp 3 in Gemini
            105.0,  // Index 4 - Cusp 4 in Cancer
            135.0,  // Index 5 - Cusp 5 in Leo
            165.0,  // Index 6 - Cusp 6 in Virgo
            195.0,  // Index 7 - Cusp 7 in Libra
            225.0,  // Index 8 - Cusp 8 in Scorpio
            255.0,  // Index 9 - Cusp 9 in Sagittarius
            285.0,  // Index 10 - MC in Capricorn
            315.0,  // Index 11 - Cusp 11 in Aquarius
            345.0   // Index 12 - Cusp 12 in Pisces
        };
    }

    [Test]
    public void CalcEnneagramStrengths_ValidData_ReturnsExpectedResults()
    {
        // Act
        var result = _enneagramCalc.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, _houses, true, false);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // Check that all Enneagram types 1-9 are present
            for (int i = 1; i <= 9; i++)
            {
                Assert.That(result.Any(kvp => kvp.Key == i), $"Enneagram type {i} should be present");
            }
            
            // Check that all strengths are positive (multiplication of factors)
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value, Is.GreaterThan(0.0), $"Strength for type {kvp.Key} should be positive");
            }
        });
    }

    [Test]
    public void CalcEnneagramStrengths_TimeNotKnown_IgnoresHousesAndAngles()
    {
        // Act
        var result = _enneagramCalc.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, _houses, false, false);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // All strengths should be positive but likely smaller than with houses included
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value, Is.GreaterThan(0.0));
            }
        });
    }

    [Test]
    public void CalcEnneagramStrengths_PlutoDouble_CountsPlutoFactorsTwice()
    {
        // Act
        var resultWithoutDouble = _enneagramCalc.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, _houses, true, false);
        var resultWithDouble = _enneagramCalc.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, _houses, true, true);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resultWithoutDouble, Is.Not.Null);
            Assert.That(resultWithDouble, Is.Not.Null);
            Assert.That(resultWithoutDouble, Has.Count.EqualTo(9));
            Assert.That(resultWithDouble, Has.Count.EqualTo(9));
            
            // With plutoDouble=true, strengths should be higher (or equal if Pluto factors are 1.0)
            for (int i = 0; i < 9; i++)
            {
                Assert.That(resultWithDouble[i].Value, Is.GreaterThanOrEqualTo(resultWithoutDouble[i].Value));
            }
        });
    }

    [Test]
    public void CalcEnneagramStrengths_NullSignsData_ThrowsArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => 
            _enneagramCalc.CalcEnneagramStrengths(null!, _housesData, _chartPoints, _houses, true, false));
        Assert.That(exception.ParamName, Is.EqualTo("signsData"));
    }

    [Test]
    public void CalcEnneagramStrengths_NullHousesData_ThrowsArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => 
            _enneagramCalc.CalcEnneagramStrengths(_signsData, null!, _chartPoints, _houses, true, false));
        Assert.That(exception.ParamName, Is.EqualTo("housesData"));
    }

    [Test]
    public void CalcEnneagramStrengths_NullChartPoints_ThrowsArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => 
            _enneagramCalc.CalcEnneagramStrengths(_signsData, _housesData, null!, _houses, true, false));
        Assert.That(exception.ParamName, Is.EqualTo("chartPoints"));
    }

    [Test]
    public void CalcEnneagramStrengths_NullHouses_ThrowsArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => 
            _enneagramCalc.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, null!, true, false));
        Assert.That(exception.ParamName, Is.EqualTo("houses"));
    }

    [Test]
    public void CalcEnneagramStrengths_HousesArrayTooSmall_ThrowsArgumentException()
    {
        // Arrange
        var smallHouses = new double[10]; // Less than 13

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _enneagramCalc.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, smallHouses, true, false));
        Assert.That(exception.ParamName, Is.EqualTo("houses"));
        Assert.That(exception.Message, Does.Contain("at least 13 elements"));
    }

    [Test]
    public void CalcEnneagramStrengths_EmptyChartPoints_ReturnsDefaultStrengths()
    {
        // Arrange
        var emptyChartPoints = new List<KeyValuePair<ChartPoints, double>>();

        // Act
        var result = _enneagramCalc.CalcEnneagramStrengths(_signsData, _housesData, emptyChartPoints, _houses, true, false);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // All strengths should be positive (houses data is still processed when timeIsKnown is true)
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value, Is.GreaterThan(0.0));
            }
        });
    }

    [Test]
    public void CalcEnneagramStrengths_EmptyChartPointsTimeNotKnown_ReturnsDefaultStrengths()
    {
        // Arrange
        var emptyChartPoints = new List<KeyValuePair<ChartPoints, double>>();

        // Act
        var result = _enneagramCalc.CalcEnneagramStrengths(_signsData, _housesData, emptyChartPoints, _houses, false, false);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // All strengths should be 1.0 (default multiplication value) when no chart points and time not known
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value, Is.EqualTo(1.0));
            }
        });
    }

    [Test]
    public void CalcEnneagramStrengths_NoMatchingData_ReturnsDefaultStrengths()
    {
        // Arrange
        var emptySignsData = new List<EnneagramData>();
        var emptyHousesData = new List<EnneagramData>();

        // Act
        var result = _enneagramCalc.CalcEnneagramStrengths(emptySignsData, emptyHousesData, _chartPoints, _houses, true, false);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // All strengths should be 1.0 (default multiplication value) if no matching data
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value, Is.EqualTo(1.0));
            }
        });
    }

    [Test]
    public void CalcEnneagramStrengths_NoMatchingDataTimeNotKnown_ReturnsDefaultStrengths()
    {
        // Arrange
        var emptySignsData = new List<EnneagramData>();
        var emptyHousesData = new List<EnneagramData>();

        // Act
        var result = _enneagramCalc.CalcEnneagramStrengths(emptySignsData, emptyHousesData, _chartPoints, _houses, false, false);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // All strengths should be 1.0 (default multiplication value) if no matching data and time not known
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value, Is.EqualTo(1.0));
            }
        });
    }

    [Test]
    public void CalcEnneagramStrengths_SignCalculation_BoundaryValues()
    {
        // Arrange - Test boundary values for sign calculation
        var boundaryChartPoints = new List<KeyValuePair<ChartPoints, double>>
        {
            new(ChartPoints.Sun, 0.0),       // Should be sign 1 (Aries)
            new(ChartPoints.Moon, 29.9),     // Should be sign 1 (Aries)
            new(ChartPoints.Mercury, 30.0),  // Should be sign 2 (Taurus)
            new(ChartPoints.Venus, 59.9),    // Should be sign 2 (Taurus)
            new(ChartPoints.Mars, 330.0),    // Should be sign 12 (Pisces)
            new(ChartPoints.Jupiter, 359.9), // Should be sign 12 (Pisces)
            new(ChartPoints.Saturn, 360.0),  // Should be sign 1 (Aries)
            new(ChartPoints.Uranus, -30.0),  // Should be sign 12 (Pisces)
            new(ChartPoints.Neptune, -0.1),  // Should be sign 12 (Pisces)
        };

        // Create data for all these signs
        var boundarySignsData = new List<EnneagramData>
        {
            new EnneagramData((int)ChartPoints.Sun, 1, new double[] { 2.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 }),
            new EnneagramData((int)ChartPoints.Moon, 1, new double[] { 2.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 }),
            new EnneagramData((int)ChartPoints.Mercury, 2, new double[] { 1.0, 2.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 }),
            new EnneagramData((int)ChartPoints.Venus, 2, new double[] { 1.0, 2.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 }),
            new EnneagramData((int)ChartPoints.Mars, 12, new double[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0 }),
            new EnneagramData((int)ChartPoints.Jupiter, 12, new double[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0 }),
            new EnneagramData((int)ChartPoints.Saturn, 1, new double[] { 2.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 }),
            new EnneagramData((int)ChartPoints.Uranus, 12, new double[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0 }),
            new EnneagramData((int)ChartPoints.Neptune, 12, new double[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0 })
        };

        // Act
        var result = _enneagramCalc.CalcEnneagramStrengths(boundarySignsData, _housesData, boundaryChartPoints, _houses, false, false);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // Enneagram type 1 should have higher strength (multiple planets in Aries)
            Assert.That(result.First(kvp => kvp.Key == 1).Value, Is.GreaterThan(1.0));
            
            // Enneagram type 2 should have higher strength (planets in Taurus)
            Assert.That(result.First(kvp => kvp.Key == 2).Value, Is.GreaterThan(1.0));
            
            // Enneagram type 9 should have higher strength (planets in Pisces)
            Assert.That(result.First(kvp => kvp.Key == 9).Value, Is.GreaterThan(1.0));
        });
    }

    [Test]
    public void CalcEnneagramStrengths_ConsistencyCheck_SameInputSameOutput()
    {
        // Act
        var result1 = _enneagramCalc.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, _houses, true, false);
        var result2 = _enneagramCalc.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, _houses, true, false);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result1, Is.Not.Null);
            Assert.That(result2, Is.Not.Null);
            Assert.That(result1, Has.Count.EqualTo(9));
            Assert.That(result2, Has.Count.EqualTo(9));
            
            // Results should be identical
            for (int i = 0; i < 9; i++)
            {
                Assert.That(result1[i].Key, Is.EqualTo(result2[i].Key));
                Assert.That(result1[i].Value, Is.EqualTo(result2[i].Value));
            }
        });
    }

    [Test]
    public void CalcEnneagramStrengths_MultiplicationLogic_Verification()
    {
        // Arrange - Create simple test data with known factors
        var simpleSignsData = new List<EnneagramData>
        {
            new EnneagramData((int)ChartPoints.Sun, 1, new double[] { 2.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 }),
            new EnneagramData((int)ChartPoints.Moon, 1, new double[] { 3.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 })
        };
        
        var simpleChartPoints = new List<KeyValuePair<ChartPoints, double>>
        {
            new(ChartPoints.Sun, 15.0),  // Aries
            new(ChartPoints.Moon, 5.0)   // Aries
        };

        // Act
        var result = _enneagramCalc.CalcEnneagramStrengths(simpleSignsData, _housesData, simpleChartPoints, _houses, false, false);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // Enneagram type 1 should be 2.0 * 3.0 = 6.0
            var type1Strength = result.First(kvp => kvp.Key == 1).Value;
            Assert.That(type1Strength, Is.EqualTo(6.0));
            
            // Other types should be 1.0 (default)
            for (int i = 2; i <= 9; i++)
            {
                var strength = result.First(kvp => kvp.Key == i).Value;
                Assert.That(strength, Is.EqualTo(1.0));
            }
        });
    }
} 