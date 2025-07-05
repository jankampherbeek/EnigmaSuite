// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.Enneagram;
using Enigma.Domain.References;
using NUnit.Framework;

namespace Enigma.Test.Core.Slices.Enneagram;

[TestFixture]
public class TestEnneagramDetails
{
    private EnneagramDetails _enneagramDetails = null!;
    private List<EnneagramData> _signsData = null!;
    private List<EnneagramData> _housesData = null!;
    private List<KeyValuePair<ChartPoints, double>> _chartPoints = null!;
    private double[] _houses = null!;

    [SetUp]
    public void Setup()
    {
        _enneagramDetails = new EnneagramDetails();

        // Create test signs data
        _signsData =
        [
            new EnneagramData((int)ChartPoints.Sun, 1, [1.5, 2.0, 1.0, 1.2, 0.8, 1.1, 1.3, 0.9, 1.4]),
            // Moon in Taurus (sign 2)
            new EnneagramData((int)ChartPoints.Moon, 2, [1.1, 1.8, 1.2, 0.9, 1.5, 1.0, 1.4, 1.1, 0.8]),
            // Mercury in Gemini (sign 3)
            new EnneagramData((int)ChartPoints.Mercury, 3,
                [1.3, 1.0, 1.6, 1.1, 0.9, 1.2, 1.0, 1.3, 1.1]),
            // Venus in Cancer (sign 4)
            new EnneagramData((int)ChartPoints.Venus, 4, [0.9, 1.2, 1.1, 1.7, 1.0, 1.3, 1.1, 0.8, 1.2]),
            // Mars in Leo (sign 5)
            new EnneagramData((int)ChartPoints.Mars, 5, [1.2, 1.1, 0.9, 1.0, 1.8, 1.1, 1.2, 1.0, 1.3]),
            // Jupiter in Virgo (sign 6)
            new EnneagramData((int)ChartPoints.Jupiter, 6,
                [1.0, 1.3, 1.2, 1.1, 1.0, 1.9, 1.1, 1.2, 1.0]),
            // Saturn in Libra (sign 7)
            new EnneagramData((int)ChartPoints.Saturn, 7, [1.4, 1.0, 1.1, 1.2, 1.1, 1.0, 1.8, 1.1, 1.2]),
            // Uranus in Scorpio (sign 8)
            new EnneagramData((int)ChartPoints.Uranus, 8, [1.1, 1.2, 1.0, 1.1, 1.2, 1.1, 1.0, 1.9, 1.1]),
            // Neptune in Sagittarius (sign 9)
            new EnneagramData((int)ChartPoints.Neptune, 9,
                [1.2, 1.1, 1.3, 1.0, 1.1, 1.2, 1.1, 1.0, 1.8]),
            // Pluto in Capricorn (sign 10)
            new EnneagramData((int)ChartPoints.Pluto, 10, [1.0, 1.1, 1.2, 1.3, 1.1, 1.0, 1.2, 1.1, 1.0]),
            // Ascendant in Aries (sign 1) - ID 1001
            new EnneagramData(1001, 1, [1.6, 1.0, 1.1, 1.2, 1.0, 1.1, 1.0, 1.1, 1.0]),
            // MC in Capricorn (sign 10) - ID 1002
            new EnneagramData(1002, 10, [1.0, 1.1, 1.0, 1.8, 1.1, 1.0, 1.1, 1.0, 1.1])
        ];

        // Create test houses data
        _housesData =
        [
            new EnneagramData(2001, 1, [1.7, 1.0, 1.1, 1.2, 1.0, 1.1, 1.0, 1.1, 1.0]),
            // Cusp 2 in Taurus (sign 2)
            new EnneagramData(2002, 2, [1.0, 1.9, 1.1, 1.0, 1.2, 1.0, 1.1, 1.0, 1.1]),
            // Cusp 3 in Gemini (sign 3)
            new EnneagramData(2003, 3, [1.1, 1.0, 1.7, 1.1, 1.0, 1.2, 1.0, 1.1, 1.0]),
            // Cusp 4 in Cancer (sign 4)
            new EnneagramData(2004, 4, [1.0, 1.1, 1.0, 1.9, 1.1, 1.0, 1.2, 1.0, 1.1]),
            // Cusp 5 in Leo (sign 5)
            new EnneagramData(2005, 5, [1.2, 1.0, 1.1, 1.0, 1.8, 1.1, 1.0, 1.1, 1.0]),
            // Cusp 6 in Virgo (sign 6)
            new EnneagramData(2006, 6, [1.0, 1.2, 1.0, 1.1, 1.0, 1.9, 1.1, 1.0, 1.1]),
            // Cusp 7 in Libra (sign 7)
            new EnneagramData(2007, 7, [1.1, 1.0, 1.2, 1.0, 1.1, 1.0, 1.8, 1.1, 1.0]),
            // Cusp 8 in Scorpio (sign 8)
            new EnneagramData(2008, 8, [1.0, 1.1, 1.0, 1.2, 1.0, 1.1, 1.0, 1.9, 1.1]),
            // Cusp 9 in Sagittarius (sign 9)
            new EnneagramData(2009, 9, [1.2, 1.0, 1.1, 1.0, 1.1, 1.0, 1.1, 1.0, 1.8]),
            // Cusp 10 in Capricorn (sign 10)
            new EnneagramData(2010, 10, [1.0, 1.2, 1.0, 1.1, 1.0, 1.1, 1.0, 1.1, 1.0]),
            // Cusp 11 in Aquarius (sign 11)
            new EnneagramData(2011, 11, [1.1, 1.0, 1.2, 1.0, 1.1, 1.0, 1.1, 1.0, 1.1]),
            // Cusp 12 in Pisces (sign 12)
            new EnneagramData(2012, 12, [1.0, 1.1, 1.0, 1.1, 1.0, 1.1, 1.0, 1.1, 1.0])
        ];

        // Create test chart points with longitudes that correspond to the signs above
        _chartPoints =
        [
            new(ChartPoints.Sun, 15.0), // Aries (0-30°)
            new(ChartPoints.Moon, 45.0), // Taurus (30-60°)
            new(ChartPoints.Mercury, 75.0), // Gemini (60-90°)
            new(ChartPoints.Venus, 105.0), // Cancer (90-120°)
            new(ChartPoints.Mars, 135.0), // Leo (120-150°)
            new(ChartPoints.Jupiter, 165.0), // Virgo (150-180°)
            new(ChartPoints.Saturn, 195.0), // Libra (180-210°)
            new(ChartPoints.Uranus, 225.0), // Scorpio (210-240°)
            new(ChartPoints.Neptune, 255.0), // Sagittarius (240-270°)
            new(ChartPoints.Pluto, 285.0)
        ];

        // Create test houses array (13 elements, index 0 ignored)
        _houses =
        [
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
        ];
    }

    [Test]
    public void CalcEnneagramStrengths_ValidData_ReturnsExpectedResults()
    {
        // Act
        var result = _enneagramDetails.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, _houses, true, false);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));

            // Should have entries for each chart point that has matching data
            var sunEntry = result.FirstOrDefault(r => r.Point == ChartPoints.Sun && r.InSigns);
            Assert.That(sunEntry, Is.Not.Null, "Should have Sun entry");
            Assert.That(sunEntry.PositionIndex, Is.EqualTo(1), "Sun should be in sign 1 (Aries)");
            Assert.That(sunEntry.Factors, Has.Length.EqualTo(9), "Should have 9 factors");

            var moonEntry = result.FirstOrDefault(r => r.Point == ChartPoints.Moon && r.InSigns);
            Assert.That(moonEntry, Is.Not.Null, "Should have Moon entry");
            Assert.That(moonEntry.PositionIndex, Is.EqualTo(2), "Moon should be in sign 2 (Taurus)");

            // Should have entries for Ascendant and MC
            var ascendantEntry = result.FirstOrDefault(r => r.Point == ChartPoints.Ascendant && r.InSigns);
            Assert.That(ascendantEntry, Is.Not.Null, "Should have Ascendant entry");
            Assert.That(ascendantEntry.PositionIndex, Is.EqualTo(1), "Ascendant should be in sign 1 (Aries)");

            var mcEntry = result.FirstOrDefault(r => r.Point == ChartPoints.Mc && r.InSigns);
            Assert.That(mcEntry, Is.Not.Null, "Should have MC entry");
            Assert.That(mcEntry.PositionIndex, Is.EqualTo(10), "MC should be in sign 10 (Capricorn)");

            // Should not have entries for cusps (cusps are boundaries, not chart points)
            var cusp1Entry = result.FirstOrDefault(r => r.Point == ChartPoints.Cusp1);
            Assert.That(cusp1Entry, Is.Null, "Should not have Cusp 1 entry (cusps are boundaries)");

            var cusp2Entry = result.FirstOrDefault(r => r.Point == ChartPoints.Cusp2);
            Assert.That(cusp2Entry, Is.Null, "Should not have Cusp 2 entry (cusps are boundaries)");
        });
    }

    [Test]
    public void CalcEnneagramStrengths_TimeNotKnown_IgnoresHousesAndAngles()
    {
        // Act
        var result = _enneagramDetails.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, _houses, false, false);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));

            // Should only have entries for chart points (signs data)
            var signsEntries = result.Where(r => r.InSigns).ToList();
            var housesEntries = result.Where(r => !r.InSigns).ToList();

            Assert.That(signsEntries, Has.Count.GreaterThan(0), "Should have signs entries");
            Assert.That(housesEntries, Has.Count.EqualTo(0), "Should not have houses entries when time not known");

            // Should not have Ascendant or MC entries when time is not known
            var ascendantEntry = result.FirstOrDefault(r => r.Point == ChartPoints.Ascendant);
            Assert.That(ascendantEntry, Is.Null, "Should not have Ascendant entry when time not known");

            var mcEntry = result.FirstOrDefault(r => r.Point == ChartPoints.Mc);
            Assert.That(mcEntry, Is.Null, "Should not have MC entry when time not known");
        });
    }

    [Test]
    public void CalcEnneagramStrengths_PlutoDouble_CountsPlutoFactorsTwice()
    {
        // Act
        var resultWithoutDouble = _enneagramDetails.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, _houses, true, false);
        var resultWithDouble = _enneagramDetails.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, _houses, true, true);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resultWithoutDouble, Is.Not.Null);
            Assert.That(resultWithDouble, Is.Not.Null);

            var plutoEntriesWithoutDouble = resultWithoutDouble.Where(r => r.Point == ChartPoints.Pluto).ToList();
            var plutoEntriesWithDouble = resultWithDouble.Where(r => r.Point == ChartPoints.Pluto).ToList();

            Assert.That(plutoEntriesWithoutDouble, Has.Count.EqualTo(1), "Should have one Pluto entry without double");
            Assert.That(plutoEntriesWithDouble, Has.Count.EqualTo(2), "Should have two Pluto entries with double");

            // Both entries should have the same factors
            Assert.That(plutoEntriesWithDouble[0].Factors, Is.EqualTo(plutoEntriesWithDouble[1].Factors));
        });
    }

    [Test]
    public void CalcEnneagramStrengths_NoMatchingData_ReturnsEmptyList()
    {
        // Arrange
        var emptySignsData = new List<EnneagramData>();
        var emptyHousesData = new List<EnneagramData>();

        // Act
        var result = _enneagramDetails.CalcEnneagramStrengths(emptySignsData, emptyHousesData, _chartPoints, _houses, true, false);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(0));
    }

    [Test]
    public void CalcEnneagramStrengths_NullInputs_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Multiple(() =>
        {
            var exception1 = Assert.Throws<ArgumentNullException>(() => 
                _enneagramDetails.CalcEnneagramStrengths(null!, _housesData, _chartPoints, _houses, true, false));
            Assert.That(exception1.ParamName, Is.EqualTo("signsData"));
            
            var exception2 = Assert.Throws<ArgumentNullException>(() => 
                _enneagramDetails.CalcEnneagramStrengths(_signsData, null!, _chartPoints, _houses, true, false));
            Assert.That(exception2.ParamName, Is.EqualTo("housesData"));
            
            var exception3 = Assert.Throws<ArgumentNullException>(() => 
                _enneagramDetails.CalcEnneagramStrengths(_signsData, _housesData, null!, _houses, true, false));
            Assert.That(exception3.ParamName, Is.EqualTo("chartPoints"));
            
            var exception4 = Assert.Throws<ArgumentNullException>(() => 
                _enneagramDetails.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, null!, true, false));
            Assert.That(exception4.ParamName, Is.EqualTo("houses"));
        });
    }

    [Test]
    public void CalcEnneagramStrengths_InvalidHousesArray_ThrowsArgumentException()
    {
        // Arrange
        var invalidHouses = new double[10]; // Less than 13 elements

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _enneagramDetails.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, invalidHouses, true, false));
        
        Assert.That(exception.ParamName, Is.EqualTo("houses"));
        Assert.That(exception.Message, Does.Contain("must have at least 13 elements"));
    }

    [Test]
    public void CalcEnneagramStrengths_EmptyHousesArray_ThrowsArgumentException()
    {
        // Arrange
        var emptyHouses = new double[0];

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _enneagramDetails.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, emptyHouses, true, false));
        
        Assert.That(exception.ParamName, Is.EqualTo("houses"));
        Assert.That(exception.Message, Does.Contain("must have at least 13 elements"));
    }

    [Test]
    public void CalcEnneagramStrengths_ExactlyThirteenHousesArray_DoesNotThrowException()
    {
        // Arrange
        var exactlyThirteenHouses = new double[13]; // Exactly 13 elements

        // Act & Assert
        Assert.DoesNotThrow(() => 
            _enneagramDetails.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, exactlyThirteenHouses, true, false));
    }

    [Test]
    public void CalcEnneagramStrengths_EmptyChartPointsList_ReturnsEmptyList()
    {
        // Arrange
        var emptyChartPoints = new List<KeyValuePair<ChartPoints, double>>();

        // Act
        var result = _enneagramDetails.CalcEnneagramStrengths(_signsData, _housesData, emptyChartPoints, _houses, true, false);

        // Assert
        Assert.That(result, Is.Not.Null);
        // Should only have Ascendant and MC entries when time is known, even with empty chart points
        Assert.That(result, Has.Count.EqualTo(2));
        
        // Verify we have Ascendant and MC entries
        var ascendantEntry = result.FirstOrDefault(r => r.Point == ChartPoints.Ascendant);
        var mcEntry = result.FirstOrDefault(r => r.Point == ChartPoints.Mc);
        
        Assert.That(ascendantEntry, Is.Not.Null, "Should have Ascendant entry when time is known");
        Assert.That(mcEntry, Is.Not.Null, "Should have MC entry when time is known");
    }

    [Test]
    public void CalcEnneagramStrengths_FactorsFormat_Verification()
    {
        // Act
        var result = _enneagramDetails.CalcEnneagramStrengths(_signsData, _housesData, _chartPoints, _houses, true, false);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));

            // All entries should have exactly 9 factors
            foreach (var entry in result)
            {
                Assert.That(entry.Factors, Is.Not.Null, $"Factors for {entry.Point} should not be null");
                Assert.That(entry.Factors, Has.Length.EqualTo(9), $"Factors for {entry.Point} should have 9 elements");
                
                // All factors should be positive
                foreach (var factor in entry.Factors)
                {
                    Assert.That(factor, Is.GreaterThan(0.0), $"Factor for {entry.Point} should be positive");
                }
            }
        });
    }
} 