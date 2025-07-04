// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Slices;
using Enigma.Core.Slices.Enneagram;
using Enigma.Domain.References;

namespace Enigma.Test.Integration;

[TestFixture]
public class IntegrationTestEnneagramService : IntegrationTestBase
{
    private readonly EnneagramService _enneagramService;

    public IntegrationTestEnneagramService()
    {
        _enneagramService = new EnneagramService();
    }

    /// <summary>
    /// Helper method to create the default chart points list that was previously hardcoded
    /// </summary>
    /// <returns>List of default chart points</returns>
    private static List<ChartPoints> GetDefaultChartPoints()
    {
        return new List<ChartPoints>
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
    }

    [Test]
    public void DefineEnneagramStrengths_ValidRequest_ReturnsExpectedResults()
    {
        // Arrange
        var request = new EnneagramRequest(2459580.5, 6.53, 52.0, GetDefaultChartPoints(), true, false); // 2022-01-01, Amsterdam

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // Verify all Enneagram types (1-9) are present
            for (int i = 1; i <= 9; i++)
            {
                Assert.That(result.Any(kvp => kvp.Key == i), $"Enneagram type {i} should be present");
            }
            
            // Verify all strengths are non-negative
            Assert.That(result.All(kvp => kvp.Value >= 0), "All strengths should be non-negative");
        });
    }

    [Test]
    [TestCase(2459580.5, 0.0, 0.0, true, false)] // Equator, Prime Meridian
    [TestCase(2459580.5, 139.6917, 35.6895, true, false)] // Tokyo
    [TestCase(2459580.5, -74.0060, 40.7128, true, false)] // New York
    [TestCase(2459580.5, 151.2093, -33.8688, true, false)] // Sydney
    [TestCase(2459580.5, -0.1276, 51.5074, true, false)] // London
    public void DefineEnneagramStrengths_VariousLocations_ReturnsValidResults(double julianDay, double longitude, double latitude, bool isTimeKnown, bool isDoublePluto)
    {
        // Arrange
        var request = new EnneagramRequest(julianDay, longitude, latitude, GetDefaultChartPoints(), isTimeKnown, isDoublePluto);

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            Assert.That(result.All(kvp => kvp.Value >= 0), "All strengths should be non-negative");
        });
    }

    [Test]
    [TestCase(2459580.5, 179.9, 65.9, true, false)] // Near max longitude, near max latitude
    [TestCase(2459580.5, -179.9, -65.9, true, false)] // Near min longitude, near min latitude
    [TestCase(2459580.5, 0.0, 0.0, true, false)] // Zero coordinates
    [TestCase(2459580.5, 100.0, 30.0, true, false)] // Mid-range positive
    [TestCase(2459580.5, -100.0, -30.0, true, false)] // Mid-range negative
    public void DefineEnneagramStrengths_BoundaryValues_Valid(double julianDay, double longitude, double latitude, bool isTimeKnown, bool isDoublePluto)
    {
        // Arrange
        var request = new EnneagramRequest(julianDay, longitude, latitude, GetDefaultChartPoints(), isTimeKnown, isDoublePluto);

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            Assert.That(result.All(kvp => kvp.Value >= 0), "All strengths should be non-negative");
        });
    }

    [Test]
    [TestCase(2459580.5, 180.1, 52.0, true, false)] // Longitude too high
    [TestCase(2459580.5, -180.1, 52.0, true, false)] // Longitude too low
    [TestCase(2459580.5, 6.53, 66.0, true, false)] // Latitude at upper bound (exclusive)
    [TestCase(2459580.5, 6.53, -66.0, true, false)] // Latitude at lower bound (exclusive)
    [TestCase(2459580.5, 6.53, 90.0, true, false)] // Latitude too high
    [TestCase(2459580.5, 6.53, -90.0, true, false)] // Latitude too low
    public void DefineEnneagramStrengths_InvalidCoordinates_ReturnsEmptyList(double julianDay, double longitude, double latitude, bool isTimeKnown, bool isDoublePluto)
    {
        // Arrange
        var request = new EnneagramRequest(julianDay, longitude, latitude, GetDefaultChartPoints(), isTimeKnown, isDoublePluto);

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }


    [Test]
    [TestCase(true, false)] // Time known, Pluto not doubled
    [TestCase(false, false)] // Time not known, Pluto not doubled
    [TestCase(true, true)] // Time known, Pluto doubled
    [TestCase(false, true)] // Time not known, Pluto doubled
    public void DefineEnneagramStrengths_DifferentFlags_ReturnsValidResults(bool isTimeKnown, bool isDoublePluto)
    {
        // Arrange
        var request = new EnneagramRequest(2459580.5, 6.53, 52.0, GetDefaultChartPoints(), isTimeKnown, isDoublePluto);

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            Assert.That(result.All(kvp => kvp.Value >= 0), "All strengths should be non-negative");
        });
    }

    [Test]
    [TestCase(2440587.5)] // 1969-07-20 (Moon landing)
    [TestCase(2459580.5)] // 2022-01-01 (Recent)
    [TestCase(2469807.5)] // 2050-01-01 (Future)
    public void DefineEnneagramStrengths_DifferentDates_ReturnsValidResults(double julianDay)
    {
        // Arrange
        var request = new EnneagramRequest(julianDay, 6.53, 52.0, GetDefaultChartPoints(), true, false);

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            Assert.That(result.All(kvp => kvp.Value >= 0), "All strengths should be non-negative");
        });
    }

    [Test]
    public void DefineEnneagramStrengths_ConsistencyCheck_SameInputSameOutput()
    {
        // Arrange
        var request = new EnneagramRequest(2459580.5, 6.53, 52.0, GetDefaultChartPoints(), true, false);

        // Act
        var result1 = _enneagramService.DefineEnneagramStrengths(request);
        var result2 = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result1, Is.Not.Null);
            Assert.That(result2, Is.Not.Null);
            Assert.That(result1, Has.Count.EqualTo(9));
            Assert.That(result2, Has.Count.EqualTo(9));
            
            // Verify results are identical
            for (int i = 0; i < result1.Count; i++)
            {
                Assert.That(result1[i].Key, Is.EqualTo(result2[i].Key), $"Enneagram type should match at index {i}");
                Assert.That(result1[i].Value, Is.EqualTo(result2[i].Value).Within(1E-10), $"Strength should match at index {i}");
            }
        });
    }

    [Test]
    public void DefineEnneagramStrengths_ResultFormat_Verification()
    {
        // Arrange
        var request = new EnneagramRequest(2459580.5, 6.53, 52.0, GetDefaultChartPoints(), true, false);

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            
            // Verify all Enneagram types are present and in order
            var expectedTypes = Enumerable.Range(1, 9).ToList();
            var actualTypes = result.Select(kvp => kvp.Key).OrderBy(x => x).ToList();
            Assert.That(actualTypes, Is.EqualTo(expectedTypes), "All Enneagram types 1-9 should be present");
            
            // Verify all strengths are finite numbers
            Assert.That(result.All(kvp => !double.IsNaN(kvp.Value)), "No strengths should be NaN");
            Assert.That(result.All(kvp => !double.IsInfinity(kvp.Value)), "No strengths should be infinite");
        });
    }

    [Test]
    public void DefineEnneagramStrengths_PlutoNotInList_IsDoublePlutoIgnored()
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
        
        var requestWithoutPluto = new EnneagramRequest(2459580.5, 6.53, 52.0, chartPointsWithoutPluto, true, true);
        var requestWithPluto = new EnneagramRequest(2459580.5, 6.53, 52.0, GetDefaultChartPoints(), true, true);

        // Act
        var resultWithoutPluto = _enneagramService.DefineEnneagramStrengths(requestWithoutPluto);
        var resultWithPluto = _enneagramService.DefineEnneagramStrengths(requestWithPluto);

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
                if (Math.Abs(resultWithoutPluto[i].Value - resultWithPluto[i].Value) > 0.001)
                {
                    hasDifferences = true;
                    break;
                }
            }
            Assert.That(hasDifferences, Is.True, "Results should differ when Pluto is only doubled in one case");
        });
    }

    [Test]
    public void DefineEnneagramStrengths_DifferentChartPoints_ReturnsDifferentResults()
    {
        // Arrange - Supported and unsupported points mixed
        var chartPointsWithUnsupported = new List<ChartPoints>
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
            ChartPoints.ApogeeMean,
            ChartPoints.ApogeeInterpolated, // Unsupported
            ChartPoints.PerigeeInterpolated // Unsupported
        };
        var request = new EnneagramRequest(2459580.5, 6.53, 52.0, chartPointsWithUnsupported, true, false);

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void DefineEnneagramStrengths_OnlySun_ReturnsValidResults()
    {
        // Arrange - Test with only Sun
        var onlySun = new List<ChartPoints> { ChartPoints.Sun };
        var request = new EnneagramRequest(2459580.5, 6.53, 52.0, onlySun, true, false);

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            Assert.That(result.All(kvp => kvp.Value >= 0), "All strengths should be non-negative");
        });
    }

    [Test]
    public void DefineEnneagramStrengths_EmptyPointsList_ReturnsEmptyList()
    {
        // Arrange
        var request = new EnneagramRequest(2459580.5, 6.53, 52.0, new List<ChartPoints>(), true, false);

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.That(result, Is.Empty);
    }

  

    [Test]
    public void DefineEnneagramStrengths_UnsupportedChartPoints_ReturnsEmptyList()
    {
        // Arrange - Include unsupported chart points
        var chartPointsWithUnsupported = new List<ChartPoints>
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
            ChartPoints.ApogeeMean,
            ChartPoints.ApogeeInterpolated, // Unsupported
            ChartPoints.PerigeeInterpolated // Unsupported
        };
        
        var request = new EnneagramRequest(2459580.5, 6.53, 52.0, chartPointsWithUnsupported, true, false);

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void DefineEnneagramStrengths_OnlyUnsupportedChartPoints_ReturnsEmptyList()
    {
        // Arrange - Only unsupported chart points
        var onlyUnsupportedPoints = new List<ChartPoints>
        {
            ChartPoints.ApogeeInterpolated,
            ChartPoints.PerigeeInterpolated
        };
        
        var request = new EnneagramRequest(2459580.5, 6.53, 52.0, onlyUnsupportedPoints, true, false);

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void DefineEnneagramStrengths_AllSupportedChartPoints_ReturnsValidResults()
    {
        // Arrange - All supported chart points
        var allSupportedPoints = new List<ChartPoints>
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
        
        var request = new EnneagramRequest(2459580.5, 6.53, 52.0, allSupportedPoints, true, false);

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.That(result, Is.Not.Empty);
        Assert.That(result.Count, Is.EqualTo(9)); // 9 Enneagram types
        Assert.That(result.All(kvp => kvp.Key >= 1 && kvp.Key <= 9), Is.True); // Valid type indices
        Assert.That(result.All(kvp => !double.IsNaN(kvp.Value) && !double.IsInfinity(kvp.Value)), Is.True); // Valid values
    }
} 