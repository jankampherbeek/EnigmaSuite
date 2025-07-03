// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Slices;
using Enigma.Core.Slices.Enneagram;

namespace Enigma.Test.Integration;

[TestFixture]
public class IntegrationTestEnneagramService : IntegrationTestBase
{
    private readonly EnneagramService _enneagramService;

    public IntegrationTestEnneagramService()
    {
        _enneagramService = new EnneagramService();
    }

    [Test]
    public void DefineEnneagramStrengths_ValidRequest_ReturnsExpectedResults()
    {
        // Arrange
        var request = new EnneagramRequest(2459580.5, 6.53, 52.0, true, false); // 2022-01-01, Amsterdam

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
        var request = new EnneagramRequest(julianDay, longitude, latitude, isTimeKnown, isDoublePluto);

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
        var request = new EnneagramRequest(julianDay, longitude, latitude, isTimeKnown, isDoublePluto);

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
        var request = new EnneagramRequest(julianDay, longitude, latitude, isTimeKnown, isDoublePluto);

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void DefineEnneagramStrengths_NullRequest_ReturnsEmptyList()
    {
        // Arrange
        EnneagramRequest? request = null;

        // Act
        var result = _enneagramService.DefineEnneagramStrengths(request!);

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
        var request = new EnneagramRequest(2459580.5, 6.53, 52.0, isTimeKnown, isDoublePluto);

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
        var request = new EnneagramRequest(julianDay, 6.53, 52.0, true, false);

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
        var request = new EnneagramRequest(2459580.5, 6.53, 52.0, true, false);

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
        var request = new EnneagramRequest(2459580.5, 6.53, 52.0, true, false);

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
} 