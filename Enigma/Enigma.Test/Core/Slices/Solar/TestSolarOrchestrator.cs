// Enigma Astrology Research.
// Unit tests for SolarOrchestrator
// Jan Kampherbeek, 2025

using Enigma.Core.Slices.Solar;
using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Facades.Se;
using FakeItEasy;
using NUnit.Framework;

namespace Enigma.Test.Core.Slices.Solar;

[TestFixture]
public class TestSolarOrchestrator
{
    private IJdForPositionFinder _jdForPositionFinder;
    private ISeFlags _seFlags;
    private IChartAllPositionsHandler _chartAllPositionsHandler;
    private ICelPointSeCalc _celPointSeCalc;
    private SolarOrchestrator _orchestrator;

    [SetUp]
    public void SetUp()
    {
        _jdForPositionFinder = A.Fake<IJdForPositionFinder>();
        _seFlags = A.Fake<ISeFlags>();
        _chartAllPositionsHandler = A.Fake<IChartAllPositionsHandler>();
        _celPointSeCalc = A.Fake<ICelPointSeCalc>();
        
        _orchestrator = new SolarOrchestrator(
            _jdForPositionFinder,
            _seFlags,
            _chartAllPositionsHandler,
            _celPointSeCalc);
    }

    [Test]
    public void CalculateSolar_TropicalReturn_ReturnsExpectedPositions()
    {
        // Arrange
        var radixJd = 2459580.5; // 2022-01-01
        var age = 30;
        var tropicalReturn = true;
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Test", 6.53, 52.0);
        var relocateLocation = (Location?)null;
        
        var request = new SolarRequest(radixJd, age, tropicalReturn, calculationPreferences, radixLocation, relocateLocation);
        
        // Mock the Sun's position at radix time
        var radixSunPosition = 280.5;
        A.CallTo(() => _celPointSeCalc.CalculateCelPoint(0, radixJd, A<int>._))
            .Returns(new[] { new PosSpeed(radixSunPosition, 0.0), new PosSpeed(0, 0), new PosSpeed(0, 0) });
        
        // Mock the flags calculation
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(258);
        
        // Mock the JdForPositionFinder to return a target JD
        var targetJd = radixJd + 365.25 * age;
        A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258))
            .Returns(targetJd);
        
        // Mock the chart calculation
        var expectedPositions = new Dictionary<ChartPoints, FullPointPos>
        {
            { ChartPoints.Sun, CreateFullPointPos(280.5) },
            { ChartPoints.Moon, CreateFullPointPos(45.2) }
        };
        A.CallTo(() => _chartAllPositionsHandler.CalcFullChart(A<CelPointsRequest>._))
            .Returns(expectedPositions);

        // Act
        var result = _orchestrator.CalculateSolar(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result.ContainsKey(ChartPoints.Sun), Is.True);
            Assert.That(result.ContainsKey(ChartPoints.Moon), Is.True);
        });

        // Verify the correct flags were used for tropical return
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .MustHaveHappened();
    }

    [Test]
    public void CalculateSolar_SiderealReturn_ReturnsExpectedPositions()
    {
        // Arrange
        var radixJd = 2459580.5; // 2022-01-01
        var age = 30;
        var tropicalReturn = false; // Sidereal return
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Test", 6.53, 52.0);
        var relocateLocation = (Location?)null;
        
        var request = new SolarRequest(radixJd, age, tropicalReturn, calculationPreferences, radixLocation, relocateLocation);
        
        // Mock the Sun's position at radix time (sidereal)
        var radixSunPosition = 275.2;
        A.CallTo(() => _celPointSeCalc.CalculateCelPoint(0, radixJd, A<int>._))
            .Returns(new[] { new PosSpeed(radixSunPosition, 0.0), new PosSpeed(0, 0), new PosSpeed(0, 0) });
        
        // Mock the flags calculation for sidereal
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Sidereal))
            .Returns(65536 + 258); // SEFLG_SIDEREAL + base flags
        
        // Mock the JdForPositionFinder to return a target JD
        var targetJd = radixJd + 365.25 * age;
        A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 65536 + 258))
            .Returns(targetJd);
        
        // Mock the chart calculation
        var expectedPositions = new Dictionary<ChartPoints, FullPointPos>
        {
            { ChartPoints.Sun, CreateFullPointPos(275.2) },
            { ChartPoints.Moon, CreateFullPointPos(42.8) }
        };
        A.CallTo(() => _chartAllPositionsHandler.CalcFullChart(A<CelPointsRequest>._))
            .Returns(expectedPositions);

        // Act
        var result = _orchestrator.CalculateSolar(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result.ContainsKey(ChartPoints.Sun), Is.True);
            Assert.That(result.ContainsKey(ChartPoints.Moon), Is.True);
        });

        // Verify the correct flags were used for sidereal return
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Sidereal))
            .MustHaveHappened();
    }

    [Test]
    public void CalculateSolar_WithRelocateLocation_UsesRelocateLocation()
    {
        // Arrange
        var radixJd = 2459580.5;
        var age = 30;
        var tropicalReturn = true;
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Radix", 6.53, 52.0);
        var relocateLocation = new Location("Relocate", 10.0, 50.0); // Different location
        
        var request = new SolarRequest(radixJd, age, tropicalReturn, calculationPreferences, radixLocation, relocateLocation);
        
        // Mock the Sun's position at radix time
        var radixSunPosition = 280.5;
        A.CallTo(() => _celPointSeCalc.CalculateCelPoint(0, radixJd, A<int>._))
            .Returns(new[] { new PosSpeed(radixSunPosition, 0.0), new PosSpeed(0, 0), new PosSpeed(0, 0) });
        
        // Mock the flags calculation
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(258);
        
        // Mock the JdForPositionFinder to return a target JD
        var targetJd = radixJd + 365.25 * age;
        A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258))
            .Returns(targetJd);
        
        // Mock the chart calculation
        var expectedPositions = new Dictionary<ChartPoints, FullPointPos>
        {
            { ChartPoints.Sun, CreateFullPointPos(280.5) }
        };
        A.CallTo(() => _chartAllPositionsHandler.CalcFullChart(A<CelPointsRequest>._))
            .Returns(expectedPositions);

        // Act
        var result = _orchestrator.CalculateSolar(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        
        // Verify that the chart calculation was called with the relocate location
        A.CallTo(() => _chartAllPositionsHandler.CalcFullChart(A<CelPointsRequest>.That.Matches(
            req => req.Location!.Equals(relocateLocation))))
            .MustHaveHappened();
    }

    [Test]
    public void CalculateSolar_WithNullRelocateLocation_UsesRadixLocation()
    {
        // Arrange
        var radixJd = 2459580.5;
        var age = 30;
        var tropicalReturn = true;
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Radix", 6.53, 52.0);
        Location? relocateLocation = null;
        
        var request = new SolarRequest(radixJd, age, tropicalReturn, calculationPreferences, radixLocation, relocateLocation);
        
        // Mock the Sun's position at radix time
        var radixSunPosition = 280.5;
        A.CallTo(() => _celPointSeCalc.CalculateCelPoint(0, radixJd, A<int>._))
            .Returns(new[] { new PosSpeed(radixSunPosition, 0.0), new PosSpeed(0, 0), new PosSpeed(0, 0) });
        
        // Mock the flags calculation
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(258);
        
        // Mock the JdForPositionFinder to return a target JD
        var targetJd = radixJd + 365.25 * age;
        A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258))
            .Returns(targetJd);
        
        // Mock the chart calculation
        var expectedPositions = new Dictionary<ChartPoints, FullPointPos>
        {
            { ChartPoints.Sun, CreateFullPointPos(280.5) }
        };
        A.CallTo(() => _chartAllPositionsHandler.CalcFullChart(A<CelPointsRequest>._))
            .Returns(expectedPositions);

        // Act
        var result = _orchestrator.CalculateSolar(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        
        // Verify that the chart calculation was called with the radix location
        A.CallTo(() => _chartAllPositionsHandler.CalcFullChart(A<CelPointsRequest>.That.Matches(
            req => req.Location!.Equals(radixLocation))))
            .MustHaveHappened();
    }

    [Test]
    public void CalculateSolar_WithEqualRelocateLocation_UsesRadixLocation()
    {
        // Arrange
        var radixJd = 2459580.5;
        var age = 30;
        var tropicalReturn = true;
        var calculationPreferences = CreateCalculationPreferences();
        var radixLocation = new Location("Radix", 6.53, 52.0);
        var relocateLocation = new Location("Radix", 6.53, 52.0); // Same as radix location
        
        var request = new SolarRequest(radixJd, age, tropicalReturn, calculationPreferences, radixLocation, relocateLocation);
        
        // Mock the Sun's position at radix time
        var radixSunPosition = 280.5;
        A.CallTo(() => _celPointSeCalc.CalculateCelPoint(0, radixJd, A<int>._))
            .Returns(new[] { new PosSpeed(radixSunPosition, 0.0), new PosSpeed(0, 0), new PosSpeed(0, 0) });
        
        // Mock the flags calculation
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(258);
        
        // Mock the JdForPositionFinder to return a target JD
        var targetJd = radixJd + 365.25 * age;
        A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258))
            .Returns(targetJd);
        
        // Mock the chart calculation
        var expectedPositions = new Dictionary<ChartPoints, FullPointPos>
        {
            { ChartPoints.Sun, CreateFullPointPos(280.5) }
        };
        A.CallTo(() => _chartAllPositionsHandler.CalcFullChart(A<CelPointsRequest>._))
            .Returns(expectedPositions);

        // Act
        var result = _orchestrator.CalculateSolar(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        
        // Verify that the chart calculation was called with the radix location (not relocate)
        A.CallTo(() => _chartAllPositionsHandler.CalcFullChart(A<CelPointsRequest>.That.Matches(
            req => req.Location!.Equals(radixLocation))))
            .MustHaveHappened();
    }

    private static CalculationPreferences CreateCalculationPreferences()
    {
        return new CalculationPreferences(
            new List<ChartPoints> { ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury },
            ZodiacTypes.Tropical,
            Ayanamshas.None,
            CoordinateSystems.Ecliptical,
            ObserverPositions.GeoCentric,
            ProjectionTypes.TwoDimensional,
            HouseSystems.Regiomontanus,
            ApogeeTypes.Duval,
            false);
    }

    private static FullPointPos CreateFullPointPos(double longitude)
    {
        var ecliptical = new PointPosSpeeds(
            new PosSpeed(longitude, 0.0),
            new PosSpeed(0.0, 0.0),
            new PosSpeed(1.0, 0.0));
        
        var equatorial = new PointPosSpeeds(
            new PosSpeed(longitude, 0.0),
            new PosSpeed(0.0, 0.0),
            new PosSpeed(1.0, 0.0));
        
        var horizontal = new PointPosSpeeds(
            new PosSpeed(0.0, 0.0),
            new PosSpeed(0.0, 0.0),
            new PosSpeed(1.0, 0.0));
        
        return new FullPointPos(ecliptical, equatorial, horizontal);
    }
} 