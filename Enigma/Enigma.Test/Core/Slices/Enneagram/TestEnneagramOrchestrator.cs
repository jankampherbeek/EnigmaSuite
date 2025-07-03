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

    [Test]
    public void CalcEnneagramStrengths_ValidRequest_ReturnsExpectedResults()
    {
        // Arrange
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, true, false);

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
            var request = new EnneagramRequest(date, VALID_LON, VALID_LAT, true, false);
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
            var request = new EnneagramRequest(VALID_JD, lon, lat, true, false);
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
        var request = new EnneagramRequest(double.NaN, VALID_LON, VALID_LAT, true, false);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            _orchestrator.CalcEnneagramStrengths(request));
    }

    [Test]
    public void CalcEnneagramStrengths_InvalidLatitude_ThrowsException()
    {
        // Arrange
        var request = new EnneagramRequest(VALID_JD, VALID_LON, 91.0, true, false); // Invalid latitude

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            _orchestrator.CalcEnneagramStrengths(request));
    }

    [Test]
    public void CalcEnneagramStrengths_InvalidLongitude_ThrowsException()
    {
        // Arrange
        var request = new EnneagramRequest(VALID_JD, 181.0, VALID_LAT, true, false); // Invalid longitude

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            _orchestrator.CalcEnneagramStrengths(request));
    }

    [Test]
    public void CalcEnneagramStrengths_HistoricalDate_ReturnsExpectedResults()
    {
        // Arrange
        var historicalDate = 2440587.5; // 1969-07-20 (Moon landing)
        var request = new EnneagramRequest(historicalDate, VALID_LON, VALID_LAT, true, false);

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
        var request = new EnneagramRequest(futureDate, VALID_LON, VALID_LAT, true, false);

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
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, true, false);

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
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, true, false);

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
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, true, false);

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
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, true, false);

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
        var request = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, true, false); // 2022-01-01

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
        var requestWithTime = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, true, false);
        var requestWithoutTime = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, false, false);

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
        var requestWithoutPlutoDouble = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, true, false);
        var requestWithPlutoDouble = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, true, true);

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
        var requestNormal = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, true, false);
        var requestCombined = new EnneagramRequest(VALID_JD, VALID_LON, VALID_LAT, false, true);

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
} 