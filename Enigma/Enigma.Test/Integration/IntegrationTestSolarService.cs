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
using Enigma.Domain.Services;

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
        
        // Register all required services
        services.RegisterFacadesServices();
        services.RegisterHandlerServices();
        services.RegisterDomainServices();
        
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
        var result = _service.CalculateJdForSolar(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.GreaterThan(0.0), "Julian Day should be positive");
            Assert.That(result, Is.GreaterThan(radixJd), "Solar return JD should be after radix JD");
            
            // The solar return should be approximately radixJd + (age + 1) * 365.25 days
            var expectedJd = radixJd + (age + 1) * 365.25;
            var tolerance = 1.0; // Allow 1 day tolerance for calculation differences
            Assert.That(result, Is.EqualTo(expectedJd).Within(tolerance), 
                "Solar return JD should be approximately radixJd + (age + 1) * 365.25");
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
        var result = _service.CalculateJdForSolar(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.GreaterThan(0.0), "Julian Day should be positive");
            Assert.That(result, Is.GreaterThan(radixJd), "Solar return JD should be after radix JD");
            
            // For sidereal return, the calculation might be different but should still be reasonable
            var expectedJd = radixJd + (age + 1) * 365.25;
            var tolerance = 2.0; // Allow 2 days tolerance for sidereal calculations
            Assert.That(result, Is.EqualTo(expectedJd).Within(tolerance), 
                "Sidereal return JD should be approximately radixJd + (age + 1) * 365.25");
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
        var result = _service.CalculateJdForSolar(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.GreaterThan(0.0), "Julian Day should be positive");
            Assert.That(result, Is.GreaterThan(radixJd), "Solar return JD should be after radix JD");
            
            // The solar return should be approximately radixJd + (age + 1) * 365.25 days
            var expectedJd = radixJd + (age + 1) * 365.25;
            var tolerance = 1.0; // Allow 1 day tolerance for calculation differences
            Assert.That(result, Is.EqualTo(expectedJd).Within(tolerance), 
                "Solar return JD should be approximately radixJd + (age + 1) * 365.25");
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
            var result = _service.CalculateJdForSolar(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.GreaterThan(0.0), $"Julian Day should be positive for age {age}");
                Assert.That(result, Is.GreaterThan(radixJd), $"Solar return JD should be after radix JD for age {age}");
                
                // The solar return should be approximately radixJd + (age + 1) * 365.25 days
                var expectedJd = radixJd + (age + 1) * 365.25;
                var tolerance = 1.0; // Allow 1 day tolerance for calculation differences
                Assert.That(result, Is.EqualTo(expectedJd).Within(tolerance), 
                    $"Solar return JD should be approximately radixJd + (age + 1) * 365.25 for age {age}");
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
            var result = _service.CalculateJdForSolar(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.GreaterThan(0.0), $"Julian Day should be positive for radix JD {radixJd}");
                Assert.That(result, Is.GreaterThan(radixJd), $"Solar return JD should be after radix JD {radixJd}");
                
                // The solar return should be approximately radixJd + (age + 1) * 365.25 days
                var expectedJd = radixJd + (age + 1) * 365.25;
                var tolerance = 1.0; // Allow 1 day tolerance for calculation differences
                Assert.That(result, Is.EqualTo(expectedJd).Within(tolerance), 
                    $"Solar return JD should be approximately radixJd + (age + 1) * 365.25 for radix JD {radixJd}");
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
            var result = _service.CalculateJdForSolar(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.GreaterThan(0.0), $"Julian Day should be positive for radix JD {radixJd}");
                Assert.That(result, Is.GreaterThan(radixJd), $"Solar return JD should be after radix JD {radixJd}");
                
                // The solar return should be approximately radixJd + (age + 1) * 365.25 days
                var expectedJd = radixJd + (age + 1) * 365.25;
                var tolerance = 1.0; // Allow 1 day tolerance for calculation differences
                Assert.That(result, Is.EqualTo(expectedJd).Within(tolerance), 
                    $"Solar return JD should be approximately radixJd + (age + 1) * 365.25 for radix JD {radixJd}");
            });
        }
    }

    [Test]
    public void TestCalculateSolar_NullRequest_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _service.CalculateJdForSolar(null!));
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
        Assert.Throws<ArgumentException>(() => _service.CalculateJdForSolar(request));
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
        Assert.Throws<ArgumentException>(() => _service.CalculateJdForSolar(request));
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
        Assert.Throws<ArgumentException>(() => _service.CalculateJdForSolar(request));
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
            var result = _service.CalculateJdForSolar(request);
            
            Assert.That(result, Is.GreaterThan(0.0), $"Julian Day should be positive for iteration {i}");
            Assert.That(result, Is.GreaterThan(radixJd), $"Solar return JD should be after radix JD for iteration {i}");
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
        var result = _service.CalculateJdForSolar(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.GreaterThan(0.0), "Julian Day should be positive");
            Assert.That(result, Is.GreaterThan(radixJd), "Solar return JD should be after radix JD");
            
            // The solar return should be approximately radixJd + (age + 1) * 365.25 days
            var expectedJd = radixJd + (age + 1) * 365.25;
            var tolerance = 1.0; // Allow 1 day tolerance for calculation differences
            Assert.That(result, Is.EqualTo(expectedJd).Within(tolerance), 
                "Solar return JD should be approximately radixJd + (age + 1) * 365.25");
            
            // Verify the result is a reasonable Julian Day number
            Assert.That(result, Is.GreaterThan(2400000.0), "Julian Day should be in a reasonable range");
            Assert.That(result, Is.LessThan(2500000.0), "Julian Day should be in a reasonable range");
        });
    }

    [Test]
    public void TestCalculateSolar_ConsistencyBetweenCalls()
    {
        // Arrange
        var radixJd = 2459580.5;
        var age = 25;
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Test Location", 6.53, 52.0);
        
        var request = new SolarRequest(radixJd, age, true, calculationPreferences, radixLocation, null);

        // Act - Multiple calls with same parameters
        var result1 = _service.CalculateJdForSolar(request);
        var result2 = _service.CalculateJdForSolar(request);
        var result3 = _service.CalculateJdForSolar(request);

        // Assert - Results should be consistent
        Assert.Multiple(() =>
        {
            Assert.That(result1, Is.EqualTo(result2), "First and second calls should return same JD");
            Assert.That(result2, Is.EqualTo(result3), "Second and third calls should return same JD");
            Assert.That(result1, Is.EqualTo(result3), "First and third calls should return same JD");
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