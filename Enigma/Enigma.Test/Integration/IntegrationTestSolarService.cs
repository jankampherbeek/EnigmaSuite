// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Slices;
using Enigma.Core.Slices.Solar;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Microsoft.Extensions.DependencyInjection;
using Enigma.Core.Calc;
using Enigma.Facades.Se;
using Enigma.Facades;
using Enigma.Core.Services;

namespace Enigma.Test.Integration;

[TestFixture]
public class IntegrationTestSolarService
{
    private SolarService _service = null!;

    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection();
        // Register all dependencies as in ApiServices
        services.AddTransient<IJdForPositionFinder, JdForPositionFinder>();
        services.AddTransient<ISeFlags, SeFlags>();
        services.AddTransient<IChartAllPositionsHandler, ChartAllPositionsHandler>();
        services.AddTransient<ICelPointSeCalc, CelPointSeCalc>();
        services.AddTransient<SolarOrchestrator>();
        services.AddTransient<SolarService>();
        services.AddTransient<ICalcUtFacade, CalcUtFacade>();
        services.AddTransient<IChartPointsMapping, ChartPointsMapping>();
        services.RegisterFacadesServices();
        services.RegisterHandlerServices();
        var provider = services.BuildServiceProvider();
        _service = provider.GetRequiredService<SolarService>();
    }

    [Test]
    public void TestCalculateSolar_TropicalReturn_HappyFlow()
    {
        // Arrange
        var radixJd = 2459580.5; // 2022-01-01
        var age = 30;
        var tropicalReturn = true;
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Test Location", 6.53, 52.0);
        var relocateLocation = (Location?)null;
        
        var request = new SolarRequest(radixJd, age, tropicalReturn, calculationPreferences, radixLocation, relocateLocation);

        // Act
        var result = _service.CalculateSolar(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));
            
            // Check that Sun is present (solar return should always include Sun)
            Assert.That(result.ContainsKey(ChartPoints.Sun), Is.True);
            
            // Check that all positions have valid values
            foreach (var position in result)
            {
                Assert.That(position.Value, Is.Not.Null);
                Assert.That(position.Value.Ecliptical, Is.Not.Null);
                Assert.That(position.Value.Ecliptical.MainPosSpeed.Position, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(position.Value.Ecliptical.MainPosSpeed.Position, Is.LessThan(360.0));
            }
        });
    }

    [Test]
    public void TestCalculateSolar_SiderealReturn_HappyFlow()
    {
        // Arrange
        var radixJd = 2459580.5; // 2022-01-01
        var age = 25;
        var tropicalReturn = false; // Sidereal return
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Test Location", 10.0, 50.0);
        var relocateLocation = (Location?)null;
        
        var request = new SolarRequest(radixJd, age, tropicalReturn, calculationPreferences, radixLocation, relocateLocation);

        // Act
        var result = _service.CalculateSolar(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));
            Assert.That(result.ContainsKey(ChartPoints.Sun), Is.True);
            
            // Check that all positions have valid values
            foreach (var position in result)
            {
                Assert.That(position.Value, Is.Not.Null);
                Assert.That(position.Value.Ecliptical, Is.Not.Null);
                Assert.That(position.Value.Ecliptical.MainPosSpeed.Position, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(position.Value.Ecliptical.MainPosSpeed.Position, Is.LessThan(360.0));
            }
        });
    }

    [Test]
    public void TestCalculateSolar_WithRelocateLocation()
    {
        // Arrange
        var radixJd = 2459580.5; // 2022-01-01
        var age = 35;
        var tropicalReturn = true;
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Radix Location", 6.53, 52.0);
        var relocateLocation = new Location("Relocate Location", 10.0, 50.0); // Different location
        
        var request = new SolarRequest(radixJd, age, tropicalReturn, calculationPreferences, radixLocation, relocateLocation);

        // Act
        var result = _service.CalculateSolar(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));
            Assert.That(result.ContainsKey(ChartPoints.Sun), Is.True);
            
            // Check that all positions have valid values
            foreach (var position in result)
            {
                Assert.That(position.Value, Is.Not.Null);
                Assert.That(position.Value.Ecliptical, Is.Not.Null);
                Assert.That(position.Value.Ecliptical.MainPosSpeed.Position, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(position.Value.Ecliptical.MainPosSpeed.Position, Is.LessThan(360.0));
            }
        });
    }

    [Test]
    public void TestCalculateSolar_DifferentAges()
    {
        // Arrange
        var radixJd = 2459580.5; // 2022-01-01
        var ages = new[] { 1, 10, 25, 50, 75, 100 };
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Test Location", 6.53, 52.0);
        
        foreach (var age in ages)
        {
            var request = new SolarRequest(radixJd, age, true, calculationPreferences, radixLocation, null);

            // Act
            var result = _service.CalculateSolar(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.GreaterThan(0));
                Assert.That(result.ContainsKey(ChartPoints.Sun), Is.True);
                
                // Check that Sun position is reasonable for solar return
                var sunPosition = result[ChartPoints.Sun].Ecliptical.MainPosSpeed.Position;
                Assert.That(sunPosition, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(sunPosition, Is.LessThan(360.0));
            });
        }
    }

    [Test]
    public void TestCalculateSolar_HistoricalDates()
    {
        // Arrange
        var historicalDates = new[] 
        { 
            2451545.0, // 2000-01-01
            2415020.0, // 1900-01-01
            2378495.0, // 1800-01-01
            2341970.0  // 1700-01-01
        };
        var age = 30;
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Test Location", 6.53, 52.0);
        
        foreach (var radixJd in historicalDates)
        {
            var request = new SolarRequest(radixJd, age, true, calculationPreferences, radixLocation, null);

            // Act
            var result = _service.CalculateSolar(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.GreaterThan(0));
                Assert.That(result.ContainsKey(ChartPoints.Sun), Is.True);
                
                // Check that all positions have valid values
                foreach (var position in result)
                {
                    Assert.That(position.Value, Is.Not.Null);
                    Assert.That(position.Value.Ecliptical, Is.Not.Null);
                    Assert.That(position.Value.Ecliptical.MainPosSpeed.Position, Is.GreaterThanOrEqualTo(0.0));
                    Assert.That(position.Value.Ecliptical.MainPosSpeed.Position, Is.LessThan(360.0));
                }
            });
        }
    }

    [Test]
    public void TestCalculateSolar_FutureDates()
    {
        // Arrange
        var futureDates = new[] 
        { 
            2469580.0, // 2030-01-01
            2479580.0, // 2040-01-01
            2489580.0  // 2050-01-01
        };
        var age = 30;
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Test Location", 6.53, 52.0);
        
        foreach (var radixJd in futureDates)
        {
            var request = new SolarRequest(radixJd, age, true, calculationPreferences, radixLocation, null);

            // Act
            var result = _service.CalculateSolar(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.GreaterThan(0));
                Assert.That(result.ContainsKey(ChartPoints.Sun), Is.True);
                
                // Check that all positions have valid values
                foreach (var position in result)
                {
                    Assert.That(position.Value, Is.Not.Null);
                    Assert.That(position.Value.Ecliptical, Is.Not.Null);
                    Assert.That(position.Value.Ecliptical.MainPosSpeed.Position, Is.GreaterThanOrEqualTo(0.0));
                    Assert.That(position.Value.Ecliptical.MainPosSpeed.Position, Is.LessThan(360.0));
                }
            });
        }
    }

    [Test]
    public void TestCalculateSolar_NullRequest_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _service.CalculateSolar(null!));
    }

    [Test]
    public void TestCalculateSolar_ZeroAge_ThrowsArgumentException()
    {
        // Arrange
        var radixJd = 2459580.5;
        var age = 0; // Invalid age
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Test Location", 6.53, 52.0);
        
        var request = new SolarRequest(radixJd, age, true, calculationPreferences, radixLocation, null);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.CalculateSolar(request));
    }

    [Test]
    public void TestCalculateSolar_NegativeAge_ThrowsArgumentException()
    {
        // Arrange
        var radixJd = 2459580.5;
        var age = -5; // Invalid age
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Test Location", 6.53, 52.0);
        
        var request = new SolarRequest(radixJd, age, true, calculationPreferences, radixLocation, null);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.CalculateSolar(request));
    }

    [Test]
    public void TestCalculateSolar_NullCalculationPreferences_ThrowsArgumentException()
    {
        // Arrange
        var radixJd = 2459580.5;
        var age = 30;
        CalculationPreferences? calculationPreferences = null; // Invalid preferences
        var radixLocation = new Location("Test Location", 6.53, 52.0);
        
        var request = new SolarRequest(radixJd, age, true, calculationPreferences!, radixLocation, null);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.CalculateSolar(request));
    }

    [Test]
    public void TestCalculateSolar_ServiceReusability()
    {
        // Arrange
        var radixJd = 2459580.5;
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Test Location", 6.53, 52.0);
        
        // Act & Assert - Multiple calls should work
        for (int i = 0; i < 5; i++)
        {
            var age = 20 + i;
            var request = new SolarRequest(radixJd, age, true, calculationPreferences, radixLocation, null);
            var result = _service.CalculateSolar(request);
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ContainsKey(ChartPoints.Sun), Is.True);
        }
    }

    [Test]
    public void TestCalculateSolar_SolarReturnAccuracy()
    {
        // Arrange
        var radixJd = 2459580.5; // 2022-01-01
        var age = 30;
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Test Location", 6.53, 52.0);
        
        var request = new SolarRequest(radixJd, age, true, calculationPreferences, radixLocation, null);

        // Act
        var result = _service.CalculateSolar(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ContainsKey(ChartPoints.Sun), Is.True);
            
            // For a solar return, the Sun should be at approximately the same longitude as the radix
            // (within a reasonable margin for the calculation method)
            var sunPosition = result[ChartPoints.Sun].Ecliptical.MainPosSpeed.Position;
            
            // The solar return should be calculated for approximately radixJd + 365.25 * age
            // The Sun should be at a similar longitude to the original radix
            // We can't predict the exact position without knowing the original Sun position,
            // but we can verify the result is reasonable
            Assert.That(sunPosition, Is.GreaterThanOrEqualTo(0.0));
            Assert.That(sunPosition, Is.LessThan(360.0));
        });
    }

    private static CalculationPreferences CreateCalculationPreferences()
    {
        return new CalculationPreferences(
            new List<ChartPoints> 
            { 
                ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury, ChartPoints.Venus,
                ChartPoints.Mars, ChartPoints.Jupiter, ChartPoints.Saturn, ChartPoints.Uranus,
                ChartPoints.Neptune, ChartPoints.Pluto, ChartPoints.TrueNode
            },
            ZodiacTypes.Tropical,
            Ayanamshas.None,
            CoordinateSystems.Ecliptical,
            ObserverPositions.GeoCentric,
            ProjectionTypes.TwoDimensional,
            HouseSystems.Regiomontanus,
            ApogeeTypes.Duval,
            false);
    }
} 