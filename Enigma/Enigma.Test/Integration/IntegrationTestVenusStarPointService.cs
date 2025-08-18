// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Slices;
using Enigma.Core.Slices.VenusStarPoint;
using Enigma.Domain.References;
using Enigma.Facades.Se;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Test.Integration;

[TestFixture]
public class IntegrationTestVenusStarPointService
{
    private VenusStarPointService _service = null!;
    private const double DELTA = 0.00000001;

    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();
        
        var orchestrator = serviceProvider.GetRequiredService<VenusStarPointOrchestrator>();
        _service = new VenusStarPointService(orchestrator);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Core services needed for VenusStarPoint
        services.AddScoped<DefineJdRange>();
        services.AddScoped<ExactConjunctionDate>();
        services.AddScoped<VenusStarPointOrchestrator>();
        
        // Facade services
        services.AddScoped<ICalcUtFacade, CalcUtFacade>();
        services.AddScoped<IJulDayFacade, JulDayFacade>();
        
        // Additional services that might be needed
        services.AddScoped<IAyanamshaFacade, AyanamshaFacade>();
        services.AddScoped<ICoTransFacade, CoTransFacade>();
        services.AddScoped<IDateConversionFacade, DateConversionFacade>();
    }

    [Test]
    public void TestVenusStarPointCalculation_Tropical_HappyFlow()
    {
        // Arrange
        var request = new VenusStarPointRequest(2459580.5, false, Ayanamshas.None); // 2022-01-01, postnatal, tropical

        // Act
        var result = _service.VenusStarPointCalculation(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));
            Assert.That(result, Has.Count.LessThanOrEqualTo(5)); // Should return up to 5 positions
            
            // Check each position
            foreach (var position in result)
            {
                Assert.That(position.SequenceId, Is.GreaterThanOrEqualTo(0));
                Assert.That(position.Jd, Is.GreaterThan(0));
                Assert.That(position.Longitude, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(position.Longitude, Is.LessThan(360.0));
                Assert.That(position.Phenomenon, Is.AnyOf(VenusPhenomena.InferiorConjunction, VenusPhenomena.SuperiorConjunction));
            }
        });
    }

    [Test]
    public void TestVenusStarPointCalculation_Sidereal_HappyFlow()
    {
        // Arrange
        var request = new VenusStarPointRequest(2459580.5, false, Ayanamshas.Lahiri); // 2022-01-01, postnatal, sidereal

        // Act
        var result = _service.VenusStarPointCalculation(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));
            Assert.That(result, Has.Count.LessThanOrEqualTo(5));
            
            foreach (var position in result)
            {
                Assert.That(position.SequenceId, Is.GreaterThanOrEqualTo(0));
                Assert.That(position.Jd, Is.GreaterThan(0));
                Assert.That(position.Longitude, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(position.Longitude, Is.LessThan(360.0));
                Assert.That(position.Phenomenon, Is.AnyOf(VenusPhenomena.InferiorConjunction, VenusPhenomena.SuperiorConjunction));
            }
        });
    }

    [Test]
    public void TestVenusStarPointCalculation_Prenatal_HappyFlow()
    {
        // Arrange
        var request = new VenusStarPointRequest(2459580.5, true, Ayanamshas.None); // 2022-01-01, prenatal, tropical

        // Act
        var result = _service.VenusStarPointCalculation(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));
            Assert.That(result, Has.Count.LessThanOrEqualTo(5));
            
            foreach (var position in result)
            {
                Assert.That(position.SequenceId, Is.GreaterThanOrEqualTo(0));
                Assert.That(position.Jd, Is.GreaterThan(0));
                Assert.That(position.Longitude, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(position.Longitude, Is.LessThan(360.0));
                Assert.That(position.Phenomenon, Is.AnyOf(VenusPhenomena.InferiorConjunction, VenusPhenomena.SuperiorConjunction));
            }
        });
    }

    [Test]
    public void TestVenusStarPointCalculation_DifferentDates()
    {
        // Arrange
        var dates = new[]
        {
            2459580.5, // 2022-01-01
            2460000.5, // 2022-07-01
            2460100.5, // 2022-10-01
            2460200.5  // 2023-01-01
        };

        foreach (var jd in dates)
        {
            var request = new VenusStarPointRequest(jd, false, Ayanamshas.None);

            // Act
            var result = _service.VenusStarPointCalculation(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.GreaterThan(0));
                Assert.That(result, Has.Count.LessThanOrEqualTo(5));
                
                foreach (var position in result)
                {
                    Assert.That(position.SequenceId, Is.GreaterThanOrEqualTo(0));
                    Assert.That(position.Jd, Is.GreaterThan(0));
                    Assert.That(position.Longitude, Is.GreaterThanOrEqualTo(0.0));
                    Assert.That(position.Longitude, Is.LessThan(360.0));
                    Assert.That(position.Phenomenon, Is.AnyOf(VenusPhenomena.InferiorConjunction, VenusPhenomena.SuperiorConjunction));
                }
            });
        }
    }

    [Test]
    public void TestVenusStarPointCalculation_DifferentAyanamshas()
    {
        // Arrange
        var ayanamshas = new[]
        {
            Ayanamshas.None,
            Ayanamshas.Lahiri,
            Ayanamshas.Raman,
            Ayanamshas.Krishnamurti
        };

        foreach (var ayanamsha in ayanamshas)
        {
            var request = new VenusStarPointRequest(2459580.5, false, ayanamsha);

            // Act
            var result = _service.VenusStarPointCalculation(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.GreaterThan(0));
                Assert.That(result, Has.Count.LessThanOrEqualTo(5));
                
                foreach (var position in result)
                {
                    Assert.That(position.SequenceId, Is.GreaterThanOrEqualTo(0));
                    Assert.That(position.Jd, Is.GreaterThan(0));
                    Assert.That(position.Longitude, Is.GreaterThanOrEqualTo(0.0));
                    Assert.That(position.Longitude, Is.LessThan(360.0));
                    Assert.That(position.Phenomenon, Is.AnyOf(VenusPhenomena.InferiorConjunction, VenusPhenomena.SuperiorConjunction));
                }
            });
        }
    }

    [Test]
    public void TestVenusStarPointCalculation_HistoricalDate()
    {
        // Arrange
        var request = new VenusStarPointRequest(2440000.5, false, Ayanamshas.None); // 1968-05-01

        // Act
        var result = _service.VenusStarPointCalculation(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));
            Assert.That(result, Has.Count.LessThanOrEqualTo(5));
            
            foreach (var position in result)
            {
                Assert.That(position.SequenceId, Is.GreaterThanOrEqualTo(0));
                Assert.That(position.Jd, Is.GreaterThan(0));
                Assert.That(position.Longitude, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(position.Longitude, Is.LessThan(360.0));
                Assert.That(position.Phenomenon, Is.AnyOf(VenusPhenomena.InferiorConjunction, VenusPhenomena.SuperiorConjunction));
            }
        });
    }

    [Test]
    public void TestVenusStarPointCalculation_FutureDate()
    {
        // Arrange
        var request = new VenusStarPointRequest(2480000.5, false, Ayanamshas.None); // 2050-01-01

        // Act
        var result = _service.VenusStarPointCalculation(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));
            Assert.That(result, Has.Count.LessThanOrEqualTo(5));
            
            foreach (var position in result)
            {
                Assert.That(position.SequenceId, Is.GreaterThanOrEqualTo(0));
                Assert.That(position.Jd, Is.GreaterThan(0));
                Assert.That(position.Longitude, Is.GreaterThanOrEqualTo(0.0));
                Assert.That(position.Longitude, Is.LessThan(360.0));
                Assert.That(position.Phenomenon, Is.AnyOf(VenusPhenomena.InferiorConjunction, VenusPhenomena.SuperiorConjunction));
            }
        });
    }

    [Test]
    public void TestVenusStarPointCalculation_ContainsBothPhenomena()
    {
        // Arrange
        var request = new VenusStarPointRequest(2459580.5, false, Ayanamshas.None);

        // Act
        var result = _service.VenusStarPointCalculation(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));
            
            var phenomena = result.Select(p => p.Phenomenon).Distinct().ToList();
            Assert.That(phenomena, Has.Count.GreaterThanOrEqualTo(1));
            Assert.That(phenomena.All(p => p == VenusPhenomena.InferiorConjunction || p == VenusPhenomena.SuperiorConjunction), Is.True);
        });
    }

    [Test]
    public void TestVenusStarPointCalculation_ChronologicalOrder()
    {
        // Arrange
        var request = new VenusStarPointRequest(2459580.5, false, Ayanamshas.None);

        // Act
        var result = _service.VenusStarPointCalculation(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0));
            
            // Check that Julian Days are in chronological order
            var jds = result.Select(p => p.Jd).ToList();
            for (int i = 1; i < jds.Count; i++)
            {
                Assert.That(jds[i], Is.GreaterThanOrEqualTo(jds[i - 1]));
            }
        });
    }

    [Test]
    public void TestVenusStarPointCalculation_NullRequest_ThrowsException()
    {
        // Arrange
        VenusStarPointRequest? request = null;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => _service.VenusStarPointCalculation(request!));
        Assert.That(exception.Message, Does.Contain("Venus Star Point request cannot be null"));
    }

    [Test]
    public void TestVenusStarPointCalculation_ConsistencyCheck()
    {
        // Arrange
        var request = new VenusStarPointRequest(2459580.5, false, Ayanamshas.None);

        // Act
        var result1 = _service.VenusStarPointCalculation(request);
        var result2 = _service.VenusStarPointCalculation(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result1, Is.Not.Null);
            Assert.That(result2, Is.Not.Null);
            Assert.That(result1.Count, Is.EqualTo(result2.Count));
            
            for (int i = 0; i < result1.Count; i++)
            {
                Assert.That(result1[i].SequenceId, Is.EqualTo(result2[i].SequenceId));
                Assert.That(result1[i].Jd, Is.EqualTo(result2[i].Jd).Within(DELTA));
                Assert.That(result1[i].Phenomenon, Is.EqualTo(result2[i].Phenomenon));
                Assert.That(result1[i].Longitude, Is.EqualTo(result2[i].Longitude).Within(DELTA));
            }
        });
    }
}

