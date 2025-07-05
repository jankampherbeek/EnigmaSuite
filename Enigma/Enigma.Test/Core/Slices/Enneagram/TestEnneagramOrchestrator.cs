// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.Enneagram;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Slices.Enneagram;

[TestFixture]
public class TestEnneagramOrchestrator
{
    private EnneagramOrchestrator _orchestrator = null!;
    private const double VALID_JD = 2459580.5; // 2022-01-01
    private const double VALID_LAT = 52.0;
    private const double VALID_LON = 6.53;

    [SetUp]
    public void SetUp()
    {
        _orchestrator = new EnneagramOrchestrator();
    }

    /// <summary>
    /// Helper method to create the default chart points list that was previously hardcoded
    /// </summary>
    /// <returns>List of default chart points</returns>
    private static List<ChartPoints> GetDefaultChartPoints()
    {
        return
        [
            ChartPoints.Sun,
            ChartPoints.Moon,
            ChartPoints.Mercury,
            ChartPoints.Venus,
            ChartPoints.Mars,
            ChartPoints.Jupiter,
            ChartPoints.Saturn,
            ChartPoints.Uranus,
            ChartPoints.Neptune,
            ChartPoints.Pluto,
            ChartPoints.Chiron,
            ChartPoints.TrueNode,
            ChartPoints.ApogeeMean
        ];
    }

    [Test]
    public void CalcEnneagramStrengths_ValidRequest_ReturnsExpectedResults()
    {
        // Arrange
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);

        // Act
        var result = _orchestrator.CalcEnneagramStrengths(request);

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
            
            // Check that all strengths are positive and have the expected format
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value, Is.Not.Null);
                Assert.That(kvp.Value, Has.Length.EqualTo(1));
                Assert.That(kvp.Value[0], Is.GreaterThan(0.0), $"Strength for type {kvp.Key} should be positive");
            }
        });
    }

    [Test]
    public void CalcEnneagramStrengths_DifferentDates_ReturnsDifferentResults()
    {
        // Arrange
        var dates = new[]
        {
            2459580.5, // 2022-01-01
            2459581.5, // 2022-01-02
            2459582.5  // 2022-01-03
        };

        var results = new List<List<KeyValuePair<int, double[]>>>();

        // Act
        foreach (var date in dates)
        {
            var request = new EnneagramRequest(date, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);
            var result = _orchestrator.CalcEnneagramStrengths(request);
            results.Add(result);
        }

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(results, Has.Count.EqualTo(3));
            
            // All results should have 9 Enneagram types
            foreach (var result in results)
            {
                Assert.That(result, Has.Count.EqualTo(9));
            }
            
            // Results should be different for different dates (planets move)
            var firstResult = results[0];
            var secondResult = results[1];
            var thirdResult = results[2];
            
            // At least some strengths should be different between dates
            bool hasDifferences = false;
            for (int i = 0; i < 9; i++)
            {
                if (Math.Abs(firstResult[i].Value[0] - secondResult[i].Value[0]) > 0.001 ||
                    Math.Abs(secondResult[i].Value[0] - thirdResult[i].Value[0]) > 0.001)
                {
                    hasDifferences = true;
                    break;
                }
            }
            Assert.That(hasDifferences, Is.True, "Strengths should differ between different dates");
        });
    }

    [Test]
    public void CalcEnneagramStrengths_DifferentLocations_ReturnsDifferentResults()
    {
        // Arrange
        var locations = new[]
        {
            (lat: 52.0, lon: 6.53),   // Netherlands
            (lat: 40.7128, lon: -74.0060), // New York
            (lat: 35.6762, lon: 139.6503), // Tokyo
            (lat: -33.8688, lon: 151.2093) // Sydney
        };

        var results = new List<List<KeyValuePair<int, double[]>>>();

        // Act
        foreach (var (lat, lon) in locations)
        {
            var request = new EnneagramRequest(VALID_JD, lon, lat, GetDefaultChartPoints(), true, false);
            var result = _orchestrator.CalcEnneagramStrengths(request);
            results.Add(result);
        }

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(results, Has.Count.EqualTo(4));
            
            // All results should have 9 Enneagram types
            foreach (var result in results)
            {
                Assert.That(result, Has.Count.EqualTo(9));
            }
            
            // Results should be different for different locations (houses change)
            var firstResult = results[0];
            var secondResult = results[1];
            
            // At least some strengths should be different between locations
            bool hasDifferences = false;
            for (int i = 0; i < 9; i++)
            {
                if (Math.Abs(firstResult[i].Value[0] - secondResult[i].Value[0]) > 0.001)
                {
                    hasDifferences = true;
                    break;
                }
            }
            Assert.That(hasDifferences, Is.True, "Strengths should differ between different locations");
        });
    }

    [Test]
    public void CalcEnneagramStrengths_NullRequest_ThrowsArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => 
            _orchestrator.CalcEnneagramStrengths(null!));
        Assert.That(exception.ParamName, Is.EqualTo("request"));
    }

    [Test]
    public void CalcEnneagramStrengths_InvalidJulianDay_ThrowsException()
    {
        // Arrange
        var request = new EnneagramRequest(double.NaN, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            _orchestrator.CalcEnneagramStrengths(request));
    }

    [Test]
    public void CalcEnneagramStrengths_InvalidLatitude_ThrowsException()
    {
        // Arrange
        var request = new EnneagramRequest(VALID_JD, VALID_LON, 91.0, GetDefaultChartPoints(), true, false); // Invalid latitude

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            _orchestrator.CalcEnneagramStrengths(request));
    }

    [Test]
    public void CalcEnneagramStrengths_InvalidLongitude_ThrowsException()
    {
        // Arrange
        var request = new EnneagramRequest(VALID_JD, 181.0, VALID_LAT, GetDefaultChartPoints(), true, false); // Invalid longitude

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            _orchestrator.CalcEnneagramStrengths(request));
    }

    [Test]
    public void CalcEnneagramStrengths_HistoricalDate_ReturnsExpectedResults()
    {
        // Arrange
        var historicalDate = 2440587.5; // 1969-07-20 (Moon landing)
        var request = new EnneagramRequest(historicalDate, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);

        // Act
        var result = _orchestrator.CalcEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // All strengths should be positive
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value[0], Is.GreaterThan(0.0));
            }
        });
    }

    [Test]
    public void CalcEnneagramStrengths_FutureDate_ReturnsExpectedResults()
    {
        // Arrange
        var futureDate = 2469807.5; // 2050-01-01
        var request = new EnneagramRequest(futureDate, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);

        // Act
        var result = _orchestrator.CalcEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // All strengths should be positive
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value[0], Is.GreaterThan(0.0));
            }
        });
    }

    [Test]
    public void CalcEnneagramStrengths_ConsistencyCheck_SameInputSameOutput()
    {
        // Arrange
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);

        // Act
        var result1 = _orchestrator.CalcEnneagramStrengths(request);
        var result2 = _orchestrator.CalcEnneagramStrengths(request);

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
                Assert.That(result1[i].Value[0], Is.EqualTo(result2[i].Value[0]));
            }
        });
    }

    [Test]
    public void CalcEnneagramStrengths_AllChartPointsIncluded_Verification()
    {
        // Arrange
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);

        // Act
        var result = _orchestrator.CalcEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // All strengths should be positive (indicating all chart points were processed)
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value[0], Is.GreaterThan(0.0));
            }
            
            // At least some strengths should be greater than 1.0 (indicating factors were found)
            bool hasFactors = false;
            foreach (var kvp in result)
            {
                if (kvp.Value[0] > 1.0)
                {
                    hasFactors = true;
                    break;
                }
            }
            Assert.That(hasFactors, Is.True, "At least some Enneagram types should have factors > 1.0");
        });
    }

    [Test]
    public void CalcEnneagramStrengths_ResultFormat_Verification()
    {
        // Arrange
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);

        // Act
        var result = _orchestrator.CalcEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // Check that all results have the correct format
            for (int i = 0; i < 9; i++)
            {
                var kvp = result[i];
                
                // Key should be 1-9
                Assert.That(kvp.Key, Is.GreaterThanOrEqualTo(1));
                Assert.That(kvp.Key, Is.LessThanOrEqualTo(9));
                
                // Value should be a non-null array with exactly one element
                Assert.That(kvp.Value, Is.Not.Null);
                Assert.That(kvp.Value, Has.Length.EqualTo(1));
                
                // The single value should be positive
                Assert.That(kvp.Value[0], Is.GreaterThan(0.0));
            }
            
            // Check that all Enneagram types 1-9 are present in order
            for (int i = 0; i < 9; i++)
            {
                Assert.That(result[i].Key, Is.EqualTo(i + 1));
            }
        });
    }

    [Test]
    public void CalcEnneagramStrengths_ServiceReusability()
    {
        // Arrange
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);

        // Act - Use the same orchestrator instance multiple times
        var result1 = _orchestrator.CalcEnneagramStrengths(request);
        var result2 = _orchestrator.CalcEnneagramStrengths(request);
        var result3 = _orchestrator.CalcEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result1, Is.Not.Null);
            Assert.That(result2, Is.Not.Null);
            Assert.That(result3, Is.Not.Null);
            Assert.That(result1, Has.Count.EqualTo(9));
            Assert.That(result2, Has.Count.EqualTo(9));
            Assert.That(result3, Has.Count.EqualTo(9));
            
            // All results should be identical
            for (int i = 0; i < 9; i++)
            {
                Assert.That(result1[i].Key, Is.EqualTo(result2[i].Key));
                Assert.That(result2[i].Key, Is.EqualTo(result3[i].Key));
                Assert.That(result1[i].Value[0], Is.EqualTo(result2[i].Value[0]));
                Assert.That(result2[i].Value[0], Is.EqualTo(result3[i].Value[0]));
            }
        });
    }

    [Test]
    public void CalcEnneagramStrengths_RealisticAstronomicalValues()
    {
        // Arrange - Test with a known astronomical event
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false); // 2022-01-01

        // Act
        var result = _orchestrator.CalcEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // All strengths should be positive and reasonable
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value[0], Is.GreaterThan(0.0));
                Assert.That(kvp.Value[0], Is.LessThan(1000000.0), "Strengths should be reasonable values");
            }
            
            // At least some strengths should be greater than 1.0
            bool hasSignificantStrengths = false;
            foreach (var kvp in result)
            {
                if (kvp.Value[0] > 1.0)
                {
                    hasSignificantStrengths = true;
                    break;
                }
            }
            Assert.That(hasSignificantStrengths, Is.True, "Some Enneagram types should have significant strengths");
        });
    }

    [Test]
    public void CalcEnneagramStrengths_TimeNotKnown_ReturnsDifferentResults()
    {
        // Arrange
        var requestWithTime = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);
        var requestWithoutTime = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), false, false);

        // Act
        var resultWithTime = _orchestrator.CalcEnneagramStrengths(requestWithTime);
        var resultWithoutTime = _orchestrator.CalcEnneagramStrengths(requestWithoutTime);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resultWithTime, Is.Not.Null);
            Assert.That(resultWithoutTime, Is.Not.Null);
            Assert.That(resultWithTime, Has.Count.EqualTo(9));
            Assert.That(resultWithoutTime, Has.Count.EqualTo(9));
            
            // Results should be different when time is not known (houses are ignored)
            bool hasDifferences = false;
            for (int i = 0; i < 9; i++)
            {
                if (Math.Abs(resultWithTime[i].Value[0] - resultWithoutTime[i].Value[0]) > 0.001)
                {
                    hasDifferences = true;
                    break;
                }
            }
            Assert.That(hasDifferences, Is.True, "Results should differ when time is not known");
        });
    }

    [Test]
    public void CalcEnneagramStrengths_PlutoDouble_ReturnsDifferentResults()
    {
        // Arrange
        var requestWithoutPlutoDouble = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);
        var requestWithPlutoDouble = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, true);

        // Act
        var resultWithoutPlutoDouble = _orchestrator.CalcEnneagramStrengths(requestWithoutPlutoDouble);
        var resultWithPlutoDouble = _orchestrator.CalcEnneagramStrengths(requestWithPlutoDouble);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resultWithoutPlutoDouble, Is.Not.Null);
            Assert.That(resultWithPlutoDouble, Is.Not.Null);
            Assert.That(resultWithoutPlutoDouble, Has.Count.EqualTo(9));
            Assert.That(resultWithPlutoDouble, Has.Count.EqualTo(9));
            
            // Results should be different when Pluto is doubled
            bool hasDifferences = false;
            for (int i = 0; i < 9; i++)
            {
                if (Math.Abs(resultWithoutPlutoDouble[i].Value[0] - resultWithPlutoDouble[i].Value[0]) > 0.001)
                {
                    hasDifferences = true;
                    break;
                }
            }
            Assert.That(hasDifferences, Is.True, "Results should differ when Pluto is doubled");
        });
    }

    [Test]
    public void CalcEnneagramStrengths_TimeNotKnownAndPlutoDouble_CombinedEffect()
    {
        // Arrange
        var requestNormal = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);
        var requestCombined = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), false, true);

        // Act
        var resultNormal = _orchestrator.CalcEnneagramStrengths(requestNormal);
        var resultCombined = _orchestrator.CalcEnneagramStrengths(requestCombined);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resultNormal, Is.Not.Null);
            Assert.That(resultCombined, Is.Not.Null);
            Assert.That(resultNormal, Has.Count.EqualTo(9));
            Assert.That(resultCombined, Has.Count.EqualTo(9));
            
            // Results should be different with combined effects
            bool hasDifferences = false;
            for (int i = 0; i < 9; i++)
            {
                if (Math.Abs(resultNormal[i].Value[0] - resultCombined[i].Value[0]) > 0.001)
                {
                    hasDifferences = true;
                    break;
                }
            }
            Assert.That(hasDifferences, Is.True, "Results should differ with combined effects");
        });
    }

    [Test]
    public void CalcEnneagramStrengths_PlutoNotInList_IsDoublePlutoIgnored()
    {
        // Arrange - Create a list without Pluto but with IsDoublePluto = true
        var chartPointsWithoutPluto = new List<ChartPoints>
        {
            ChartPoints.Sun,
            ChartPoints.Moon,
            ChartPoints.Mercury,
            ChartPoints.Venus,
            ChartPoints.Mars,
            ChartPoints.Jupiter,
            ChartPoints.Saturn,
            ChartPoints.Uranus,
            ChartPoints.Neptune,
            ChartPoints.Chiron,
            ChartPoints.TrueNode,
            ChartPoints.ApogeeMean
        };
        
        var requestWithoutPluto = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, chartPointsWithoutPluto, true, true);
        var requestWithPluto = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, true);

        // Act
        var resultWithoutPluto = _orchestrator.CalcEnneagramStrengths(requestWithoutPluto);
        var resultWithPluto = _orchestrator.CalcEnneagramStrengths(requestWithPluto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resultWithoutPluto, Is.Not.Null);
            Assert.That(resultWithPluto, Is.Not.Null);
            Assert.That(resultWithoutPluto, Has.Count.EqualTo(9));
            Assert.That(resultWithPluto, Has.Count.EqualTo(9));
            
            // Results should be different because Pluto is only doubled in the second case
            bool hasDifferences = false;
            for (int i = 0; i < 9; i++)
            {
                if (Math.Abs(resultWithoutPluto[i].Value[0] - resultWithPluto[i].Value[0]) > 0.001)
                {
                    hasDifferences = true;
                    break;
                }
            }
            Assert.That(hasDifferences, Is.True, "Results should differ when Pluto is only doubled in one case");
        });
    }

    [Test]
    public void CalcEnneagramStrengths_EmptyPointsList_ThrowsArgumentException()
    {
        // Arrange
        var emptyPointsList = new List<ChartPoints>();
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, emptyPointsList, true, false);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _orchestrator.CalcEnneagramStrengths(request));
        Assert.That(exception.Message, Does.Contain("Points list cannot be null or empty"));
    }

    [Test]
    public void CalcEnneagramStrengths_NullPointsList_ThrowsArgumentException()
    {
        // Arrange
        List<ChartPoints>? nullPointsList = null;
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, nullPointsList!, true, false);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _orchestrator.CalcEnneagramStrengths(request));
        Assert.That(exception.Message, Does.Contain("Points list cannot be null or empty"));
    }

    [Test]
    public void CalcEnneagramStrengths_DifferentChartPoints_ReturnsDifferentResults()
    {
        // Arrange - Test with different sets of chart points
        var minimalPoints = new List<ChartPoints>
        {
            ChartPoints.Sun,
            ChartPoints.Moon
        };
        
        var extendedPoints = new List<ChartPoints>
        {
            ChartPoints.Sun,
            ChartPoints.Moon,
            ChartPoints.Mercury,
            ChartPoints.Venus,
            ChartPoints.Mars,
            ChartPoints.Jupiter,
            ChartPoints.Saturn,
            ChartPoints.Uranus,
            ChartPoints.Neptune,
            ChartPoints.Pluto,
            ChartPoints.Chiron,
            ChartPoints.TrueNode,
            ChartPoints.ApogeeMean
        };
        
        var requestMinimal = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, minimalPoints, true, false);
        var requestExtended = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, extendedPoints, true, false);

        // Act
        var resultMinimal = _orchestrator.CalcEnneagramStrengths(requestMinimal);
        var resultExtended = _orchestrator.CalcEnneagramStrengths(requestExtended);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resultMinimal, Is.Not.Null);
            Assert.That(resultExtended, Is.Not.Null);
            Assert.That(resultMinimal, Has.Count.EqualTo(9));
            Assert.That(resultExtended, Has.Count.EqualTo(9));
            
            // Results should be different due to different chart points
            bool hasDifferences = false;
            for (int i = 0; i < 9; i++)
            {
                if (Math.Abs(resultMinimal[i].Value[0] - resultExtended[i].Value[0]) > 0.001)
                {
                    hasDifferences = true;
                    break;
                }
            }
            Assert.That(hasDifferences, Is.True, "Results should differ with different chart points");
        });
    }

    [Test]
    public void CalcEnneagramStrengths_OnlySun_ReturnsValidResults()
    {
        // Arrange - Test with only Sun
        var onlySun = new List<ChartPoints> { ChartPoints.Sun };
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, onlySun, true, false);

        // Act
        var result = _orchestrator.CalcEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // All strengths should be positive
            foreach (var kvp in result)
            {
                Assert.That(kvp.Value[0], Is.GreaterThan(0.0));
            }
        });
    }

    // ===== CalcEnneagramDetails Tests =====

    [Test]
    public void CalcEnneagramDetails_ValidRequest_ReturnsExpectedResults()
    {
        // Arrange
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);

        // Act
        var result = _orchestrator.CalcEnneagramDetails(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));
            
            // Should have entries for each chart point plus Ascendant and MC when houses are used
            var chartPointEntries = result.Where(r => r.InSigns).ToList();
            Assert.That(chartPointEntries, Has.Count.EqualTo(GetDefaultChartPoints().Count + 2));
            
            // All entries should have valid factors
            foreach (var entry in result)
            {
                Assert.That(entry.Factors, Is.Not.Null);
                Assert.That(entry.Factors, Has.Length.EqualTo(9));
                Assert.That(entry.PositionIndex, Is.GreaterThanOrEqualTo(1));
                Assert.That(entry.PositionIndex, Is.LessThanOrEqualTo(12));
            }
        });
    }

    [Test]
    public void CalcEnneagramDetails_DifferentDates_ReturnsDifferentResults()
    {
        // Arrange
        var dates = new[]
        {
            2459580.5, // 2022-01-01
            2459581.5, // 2022-01-02
            2459582.5  // 2022-01-03
        };

        var results = new List<List<EnneagramDetailsLine>>();

        // Act
        foreach (var date in dates)
        {
            var request = new EnneagramRequest(date, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);
            var result = _orchestrator.CalcEnneagramDetails(request);
            results.Add(result);
        }

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(results, Has.Count.EqualTo(3));
            
            // All results should have entries
            foreach (var result in results)
            {
                Assert.That(result, Has.Count.GreaterThan(0));
            }
            
            // Results should be different for different dates (planets move)
            var firstResult = results[0];
            var secondResult = results[1];
            
            // At least some factors should be different between dates
            bool hasDifferences = false;
            for (int i = 0; i < Math.Min(firstResult.Count, secondResult.Count); i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Math.Abs(firstResult[i].Factors[j] - secondResult[i].Factors[j]) > 0.001)
                    {
                        hasDifferences = true;
                        break;
                    }
                }
                if (hasDifferences) break;
            }
            Assert.That(hasDifferences, Is.True, "Factors should differ between different dates");
        });
    }

    [Test]
    public void CalcEnneagramDetails_DifferentLocations_ReturnsDifferentResults()
    {
        // Arrange
        var locations = new[]
        {
            (lat: 52.0, lon: 6.53),   // Netherlands
            (lat: 40.7128, lon: -74.0060), // New York
            (lat: 35.6762, lon: 139.6503), // Tokyo
            (lat: -33.8688, lon: 151.2093) // Sydney
        };

        var results = new List<List<EnneagramDetailsLine>>();

        // Act
        foreach (var (lat, lon) in locations)
        {
            var request = new EnneagramRequest(VALID_JD, lon, lat, GetDefaultChartPoints(), true, false);
            var result = _orchestrator.CalcEnneagramDetails(request);
            results.Add(result);
        }

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(results, Has.Count.EqualTo(4));
            
            // All results should have entries
            foreach (var result in results)
            {
                Assert.That(result, Has.Count.GreaterThan(0));
            }
            
            // Results should be different for different locations (houses change)
            var firstResult = results[0];
            var secondResult = results[1];
            
            // At least some factors should be different between locations
            bool hasDifferences = false;
            for (int i = 0; i < Math.Min(firstResult.Count, secondResult.Count); i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Math.Abs(firstResult[i].Factors[j] - secondResult[i].Factors[j]) > 0.001)
                    {
                        hasDifferences = true;
                        break;
                    }
                }
                if (hasDifferences) break;
            }
            Assert.That(hasDifferences, Is.True, "Factors should differ between different locations");
        });
    }

    [Test]
    public void CalcEnneagramDetails_NullRequest_ThrowsArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => 
            _orchestrator.CalcEnneagramDetails(null!));
        Assert.That(exception.ParamName, Is.EqualTo("request"));
    }

    [Test]
    public void CalcEnneagramDetails_EmptyPointsList_ThrowsArgumentException()
    {
        // Arrange
        var emptyPointsList = new List<ChartPoints>();
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, emptyPointsList, true, false);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _orchestrator.CalcEnneagramDetails(request));
        Assert.That(exception.Message, Does.Contain("Points list cannot be null or empty"));
    }

    [Test]
    public void CalcEnneagramDetails_NullPointsList_ThrowsArgumentException()
    {
        // Arrange
        List<ChartPoints>? nullPointsList = null;
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, nullPointsList!, true, false);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _orchestrator.CalcEnneagramDetails(request));
        Assert.That(exception.Message, Does.Contain("Points list cannot be null or empty"));
    }


    [Test]
    public void CalcEnneagramDetails_PlutoDouble_ReturnsDifferentResults()
    {
        // Arrange
        var requestWithoutPlutoDouble = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);
        var requestWithPlutoDouble = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, true);

        // Act
        var resultWithoutPlutoDouble = _orchestrator.CalcEnneagramDetails(requestWithoutPlutoDouble);
        var resultWithPlutoDouble = _orchestrator.CalcEnneagramDetails(requestWithPlutoDouble);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resultWithoutPlutoDouble, Is.Not.Null);
            Assert.That(resultWithPlutoDouble, Is.Not.Null);
            
            // Result with Pluto double should have more entries (Pluto appears twice)
            Assert.That(resultWithPlutoDouble.Count, Is.GreaterThan(resultWithoutPlutoDouble.Count));
            
            // Count Pluto entries
            var plutoEntriesWithoutDouble = resultWithoutPlutoDouble.Where(r => r.Point == ChartPoints.Pluto).ToList();
            var plutoEntriesWithDouble = resultWithPlutoDouble.Where(r => r.Point == ChartPoints.Pluto).ToList();
            Assert.That(plutoEntriesWithoutDouble.Count, Is.EqualTo(1));
            Assert.That(plutoEntriesWithDouble.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public void CalcEnneagramDetails_PlutoNotInList_IsDoublePlutoIgnored()
    {
        // Arrange - Create a list without Pluto but with IsDoublePluto = true
        var chartPointsWithoutPluto = new List<ChartPoints>
        {
            ChartPoints.Sun,
            ChartPoints.Moon,
            ChartPoints.Mercury,
            ChartPoints.Venus,
            ChartPoints.Mars,
            ChartPoints.Jupiter,
            ChartPoints.Saturn,
            ChartPoints.Uranus,
            ChartPoints.Neptune,
            ChartPoints.Chiron,
            ChartPoints.TrueNode,
            ChartPoints.ApogeeMean
        };
        
        var requestWithoutPluto = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, chartPointsWithoutPluto, true, true);
        var requestWithPluto = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, true);

        // Act
        var resultWithoutPluto = _orchestrator.CalcEnneagramDetails(requestWithoutPluto);
        var resultWithPluto = _orchestrator.CalcEnneagramDetails(requestWithPluto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resultWithoutPluto, Is.Not.Null);
            Assert.That(resultWithPluto, Is.Not.Null);
            
            // Result with Pluto should have more entries (Pluto appears twice)
            Assert.That(resultWithPluto.Count, Is.GreaterThan(resultWithoutPluto.Count));
            
            // Count Pluto entries
            var plutoEntriesWithoutPluto = resultWithoutPluto.Where(r => r.Point == ChartPoints.Pluto).ToList();
            var plutoEntriesWithPluto = resultWithPluto.Where(r => r.Point == ChartPoints.Pluto).ToList();
            Assert.That(plutoEntriesWithoutPluto.Count, Is.EqualTo(0));
            Assert.That(plutoEntriesWithPluto.Count, Is.EqualTo(2));
        });
    }

  
    [Test]
    public void CalcEnneagramDetails_OnlySun_ReturnsValidResults()
    {
        // Arrange - Test with only Sun
        var onlySun = new List<ChartPoints> { ChartPoints.Sun };
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, onlySun, true, false);

        // Act
        var result = _orchestrator.CalcEnneagramDetails(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));
            
            // Should have one entry for Sun
            var sunEntries = result.Where(r => r.Point == ChartPoints.Sun).ToList();
            Assert.That(sunEntries, Has.Count.EqualTo(1));
            
            // Should have entries for Ascendant and MC when time is known
            var ascendantEntries = result.Where(r => r.Point == ChartPoints.Ascendant).ToList();
            var mcEntries = result.Where(r => r.Point == ChartPoints.Mc).ToList();
            Assert.That(ascendantEntries, Has.Count.EqualTo(1));
            Assert.That(mcEntries, Has.Count.EqualTo(1));
            
            // All entries should have valid factors
            foreach (var entry in result)
            {
                Assert.That(entry.Factors, Is.Not.Null);
                Assert.That(entry.Factors, Has.Length.EqualTo(9));
            }
        });
    }

    [Test]
    public void CalcEnneagramDetails_ConsistencyCheck_SameInputSameOutput()
    {
        // Arrange
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);

        // Act
        var result1 = _orchestrator.CalcEnneagramDetails(request);
        var result2 = _orchestrator.CalcEnneagramDetails(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result1, Is.Not.Null);
            Assert.That(result2, Is.Not.Null);
            Assert.That(result1.Count, Is.EqualTo(result2.Count));
            
            // All entries should be identical
            for (int i = 0; i < result1.Count; i++)
            {
                Assert.That(result1[i].Point, Is.EqualTo(result2[i].Point));
                Assert.That(result1[i].PositionIndex, Is.EqualTo(result2[i].PositionIndex));
                Assert.That(result1[i].InSigns, Is.EqualTo(result2[i].InSigns));
                Assert.That(result1[i].Factors, Is.EqualTo(result2[i].Factors));
            }
        });
    }

    [Test]
    public void CalcEnneagramDetails_ResultFormat_Verification()
    {
        // Arrange
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, GetDefaultChartPoints(), true, false);

        // Act
        var result = _orchestrator.CalcEnneagramDetails(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));
            
            // Verify each entry has correct format
            foreach (var entry in result)
            {
                // Point should be valid (not an invalid enum value)
                Assert.That(Enum.IsDefined(typeof(ChartPoints), entry.Point), Is.True);
                
                // Position index should be 1-12
                Assert.That(entry.PositionIndex, Is.GreaterThanOrEqualTo(1));
                Assert.That(entry.PositionIndex, Is.LessThanOrEqualTo(12));
                
                // Factors should be valid
                Assert.That(entry.Factors, Is.Not.Null);
                Assert.That(entry.Factors, Has.Length.EqualTo(9));
                
                // All factors should be positive
                foreach (var factor in entry.Factors)
                {
                    Assert.That(factor, Is.GreaterThan(0.0));
                }
            }
            
            // Should have chart point entries (including Ascendant and MC)
            var chartPointEntries = result.Where(r => r.InSigns).ToList();
            Assert.That(chartPointEntries, Has.Count.GreaterThan(0));
            
            // Should have Ascendant and MC entries
            var ascendantEntries = result.Where(r => r.Point == ChartPoints.Ascendant).ToList();
            var mcEntries = result.Where(r => r.Point == ChartPoints.Mc).ToList();
            Assert.That(ascendantEntries, Has.Count.EqualTo(1));
            Assert.That(mcEntries, Has.Count.EqualTo(1));
        });
    }
} 