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
    private ICelPointsHandler _celPointsHandler;

    // [SetUp]
    // public void SetUp()
    // {
    //     _jdForPositionFinder = A.Fake<IJdForPositionFinder>();
    //     _seFlags = A.Fake<ISeFlags>();
    //     _chartAllPositionsHandler = A.Fake<IChartAllPositionsHandler>();
    //     _celPointSeCalc = A.Fake<ICelPointSeCalc>();
    //     _celPointsHandler = A.Fake<ICelPointsHandler>();
    //     
    //     _orchestrator = new SolarOrchestrator(
    //         _jdForPositionFinder,
    //         _seFlags,
    //         _chartAllPositionsHandler,
    //         _celPointsHandler,
    //         _celPointSeCalc);
    // }
    //
    // [Test]
    // public void CalculateJdForSolar_TropicalReturn_ReturnsCorrectJulianDay()
    // {
    //     // Arrange
    //     var radixJd = 2459580.5; // 2022-01-01
    //     var age = 30;
    //     var tropicalReturn = true;
    //     var calculationPreferences = CreateCalculationPreferences();
    //     var radixLocation = new Location("Test", 6.53, 52.0);
    //     var relocateLocation = (Location?)null;
    //     
    //     var request = new SolarRequest(radixJd, age, tropicalReturn, calculationPreferences, radixLocation, relocateLocation);
    //     
    //     // Mock the Sun's position at radix time using ICelPointsHandler
    //     var radixSunPosition = 280.5;
    //     var mockSunPositions = CreateMockFullPointPos(radixSunPosition);
    //     A.CallTo(() => _celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, radixJd, A<Location>._, calculationPreferences))
    //         .Returns(mockSunPositions);
    //     
    //     // Mock the flags calculation
    //     A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
    //         .Returns(258);
    //     
    //     // Mock the JdForPositionFinder to return a calculated solar return JD
    //     var expectedSolarReturnJd = radixJd + (age + 1) * 365.25;
    //     A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258))
    //         .Returns(expectedSolarReturnJd);
    //     
    //     // Act
    //     var result = _orchestrator.CalculateJdForSolar(request);
    //
    //     // Assert
    //     Assert.Multiple(() =>
    //     {
    //         Assert.That(result, Is.EqualTo(expectedSolarReturnJd), "Should return the calculated solar return JD");
    //         Assert.That(result, Is.GreaterThan(radixJd), "Solar return JD should be after radix JD");
    //     });
    //
    //     // Verify the correct calls were made
    //     A.CallTo(() => _celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, radixJd, A<Location>._, calculationPreferences)).MustHaveHappened();
    //     A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical)).MustHaveHappened();
    //     A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258)).MustHaveHappened();
    // }
    //
    // [Test]
    // public void CalculateJdForSolar_SiderealReturn_ReturnsCorrectJulianDay()
    // {
    //     // Arrange
    //     var radixJd = 2459580.5; // 2022-01-01
    //     var age = 25;
    //     var tropicalReturn = false; // Sidereal return
    //     var calculationPreferences = CreateCalculationPreferences();
    //     var radixLocation = new Location("Test", 6.53, 52.0);
    //     var relocateLocation = (Location?)null;
    //     
    //     var request = new SolarRequest(radixJd, age, tropicalReturn, calculationPreferences, radixLocation, relocateLocation);
    //     
    //     // Mock the Sun's position at radix time using ICelPointsHandler
    //     var radixSunPosition = 275.3;
    //     var mockSunPositions = CreateMockFullPointPos(radixSunPosition);
    //     A.CallTo(() => _celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, radixJd, A<Location>._, calculationPreferences))
    //         .Returns(mockSunPositions);
    //     
    //     // Mock the flags calculation
    //     A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
    //         .Returns(258);
    //     
    //     // Mock the JdForPositionFinder to return a calculated solar return JD
    //     var expectedSolarReturnJd = radixJd + (age + 1) * 365.25;
    //     A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258))
    //         .Returns(expectedSolarReturnJd);
    //     
    //     // Act
    //     var result = _orchestrator.CalculateJdForSolar(request);
    //
    //     // Assert
    //     Assert.Multiple(() =>
    //     {
    //         Assert.That(result, Is.EqualTo(expectedSolarReturnJd), "Should return the calculated solar return JD");
    //         Assert.That(result, Is.GreaterThan(radixJd), "Solar return JD should be after radix JD");
    //     });
    //
    //     // Verify the correct calls were made
    //     A.CallTo(() => _celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, radixJd, A<Location>._, calculationPreferences)).MustHaveHappened();
    //     A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical)).MustHaveHappened();
    //     A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258)).MustHaveHappened();
    // }
    //
    // [Test]
    // public void CalculateJdForSolar_DifferentAges_ReturnsCorrectJulianDays()
    // {
    //     // Arrange
    //     var radixJd = 2459580.5;
    //     var ages = new[] { 1, 10, 25, 50, 75, 100 };
    //     var calculationPreferences = CreateCalculationPreferences();
    //     var radixLocation = new Location("Test", 6.53, 52.0);
    //     
    //     foreach (var age in ages)
    //     {
    //         var request = new SolarRequest(radixJd, age, true, calculationPreferences, radixLocation, null);
    //         
    //         // Mock the Sun's position at radix time using ICelPointsHandler
    //         var radixSunPosition = 280.5;
    //         var mockSunPositions = CreateMockFullPointPos(radixSunPosition);
    //         A.CallTo(() => _celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, radixJd, A<Location>._, calculationPreferences))
    //             .Returns(mockSunPositions);
    //         
    //         // Mock the flags calculation
    //         A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
    //             .Returns(258);
    //         
    //         // Mock the JdForPositionFinder to return a calculated solar return JD
    //         var expectedSolarReturnJd = radixJd + (age + 1) * 365.25;
    //         A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258))
    //             .Returns(expectedSolarReturnJd);
    //         
    //         // Act
    //         var result = _orchestrator.CalculateJdForSolar(request);
    //
    //         // Assert
    //         Assert.Multiple(() =>
    //         {
    //             Assert.That(result, Is.EqualTo(expectedSolarReturnJd), $"Should return correct JD for age {age}");
    //             Assert.That(result, Is.GreaterThan(radixJd), $"Solar return JD should be after radix JD for age {age}");
    //         });
    //     }
    // }
    //
    // [Test]
    // public void CalculateJdForSolar_WithRelocateLocation_UsesRelocateLocation()
    // {
    //     // Arrange
    //     var radixJd = 2459580.5;
    //     var age = 30;
    //     var tropicalReturn = true;
    //     var calculationPreferences = CreateCalculationPreferences();
    //     var radixLocation = new Location("Radix", 6.53, 52.0);
    //     var relocateLocation = new Location("Relocate", 10.0, 50.0); // Different location
    //     
    //     var request = new SolarRequest(radixJd, age, tropicalReturn, calculationPreferences, radixLocation, relocateLocation);
    //     
    //     // Mock the Sun's position at radix time using ICelPointsHandler
    //     var radixSunPosition = 280.5;
    //     var mockSunPositions = CreateMockFullPointPos(radixSunPosition);
    //     A.CallTo(() => _celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, radixJd, A<Location>._, calculationPreferences))
    //         .Returns(mockSunPositions);
    //     
    //     // Mock the flags calculation
    //     A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
    //         .Returns(258);
    //     
    //     // Mock the JdForPositionFinder to return a calculated solar return JD
    //     var expectedSolarReturnJd = radixJd + (age + 1) * 365.25;
    //     A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258))
    //         .Returns(expectedSolarReturnJd);
    //     
    //     // Act
    //     var result = _orchestrator.CalculateJdForSolar(request);
    //
    //     // Assert
    //     Assert.Multiple(() =>
    //     {
    //         Assert.That(result, Is.EqualTo(expectedSolarReturnJd), "Should return the calculated solar return JD");
    //         Assert.That(result, Is.GreaterThan(radixJd), "Solar return JD should be after radix JD");
    //     });
    //
    //     // Verify the correct calls were made
    //     A.CallTo(() => _celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, radixJd, A<Location>._, calculationPreferences)).MustHaveHappened();
    //     A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical)).MustHaveHappened();
    //     A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258)).MustHaveHappened();
    // }
    //
    // [Test]
    // public void CalculateJdForSolar_WithNullRelocateLocation_UsesRadixLocation()
    // {
    //     // Arrange
    //     var radixJd = 2459580.5;
    //     var age = 30;
    //     var tropicalReturn = true;
    //     var calculationPreferences = CreateCalculationPreferences();
    //     var radixLocation = new Location("Radix", 6.53, 52.0);
    //     Location? relocateLocation = null;
    //     
    //     var request = new SolarRequest(radixJd, age, tropicalReturn, calculationPreferences, radixLocation, relocateLocation);
    //     
    //     // Mock the Sun's position at radix time using ICelPointsHandler
    //     var radixSunPosition = 280.5;
    //     var mockSunPositions = CreateMockFullPointPos(radixSunPosition);
    //     A.CallTo(() => _celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, radixJd, A<Location>._, calculationPreferences))
    //         .Returns(mockSunPositions);
    //     
    //     // Mock the flags calculation
    //     A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
    //         .Returns(258);
    //     
    //     // Mock the JdForPositionFinder to return a calculated solar return JD
    //     var expectedSolarReturnJd = radixJd + (age + 1) * 365.25;
    //     A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258))
    //         .Returns(expectedSolarReturnJd);
    //     
    //     // Act
    //     var result = _orchestrator.CalculateJdForSolar(request);
    //
    //     // Assert
    //     Assert.Multiple(() =>
    //     {
    //         Assert.That(result, Is.EqualTo(expectedSolarReturnJd), "Should return the calculated solar return JD");
    //         Assert.That(result, Is.GreaterThan(radixJd), "Solar return JD should be after radix JD");
    //     });
    //
    //     // Verify the correct calls were made
    //     A.CallTo(() => _celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, radixJd, A<Location>._, calculationPreferences)).MustHaveHappened();
    //     A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical)).MustHaveHappened();
    //     A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258)).MustHaveHappened();
    // }
    //
    // [Test]
    // public void CalculateJdForSolar_HistoricalDates_ReturnsCorrectJulianDays()
    // {
    //     // Arrange
    //     var historicalDates = new[] 
    //     { 
    //         2451545.0, // 2000-01-01
    //         2415020.0, // 1900-01-01
    //         2378495.0, // 1800-01-01
    //         2341970.0  // 1700-01-01
    //     };
    //     var age = 30;
    //     var calculationPreferences = CreateCalculationPreferences();
    //     var radixLocation = new Location("Test", 6.53, 52.0);
    //     
    //     foreach (var radixJd in historicalDates)
    //     {
    //         var request = new SolarRequest(radixJd, age, true, calculationPreferences, radixLocation, null);
    //         
    //         // Mock the Sun's position at radix time using ICelPointsHandler
    //         var radixSunPosition = 280.5;
    //         var mockSunPositions = CreateMockFullPointPos(radixSunPosition);
    //         A.CallTo(() => _celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, radixJd, A<Location>._, calculationPreferences))
    //             .Returns(mockSunPositions);
    //         
    //         // Mock the flags calculation
    //         A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
    //             .Returns(258);
    //         
    //         // Mock the JdForPositionFinder to return a calculated solar return JD
    //         var expectedSolarReturnJd = radixJd + (age + 1) * 365.25;
    //         A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258))
    //             .Returns(expectedSolarReturnJd);
    //         
    //         // Act
    //         var result = _orchestrator.CalculateJdForSolar(request);
    //
    //         // Assert
    //         Assert.Multiple(() =>
    //         {
    //             Assert.That(result, Is.EqualTo(expectedSolarReturnJd), $"Should return correct JD for radix JD {radixJd}");
    //             Assert.That(result, Is.GreaterThan(radixJd), $"Solar return JD should be after radix JD {radixJd}");
    //         });
    //     }
    // }
    //
    // [Test]
    // public void CalculateJdForSolar_FutureDates_ReturnsCorrectJulianDays()
    // {
    //     // Arrange
    //     var futureDates = new[] 
    //     { 
    //         2469580.0, // 2030-01-01
    //         2479580.0, // 2040-01-01
    //         2489580.0  // 2050-01-01
    //     };
    //     var age = 30;
    //     var calculationPreferences = CreateCalculationPreferences();
    //     var radixLocation = new Location("Test", 6.53, 52.0);
    //     
    //     foreach (var radixJd in futureDates)
    //     {
    //         var request = new SolarRequest(radixJd, age, true, calculationPreferences, radixLocation, null);
    //         
    //         // Mock the Sun's position at radix time using ICelPointsHandler
    //         var radixSunPosition = 280.5;
    //         var mockSunPositions = CreateMockFullPointPos(radixSunPosition);
    //         A.CallTo(() => _celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, radixJd, A<Location>._, calculationPreferences))
    //             .Returns(mockSunPositions);
    //         
    //         // Mock the flags calculation
    //         A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
    //             .Returns(258);
    //         
    //         // Mock the JdForPositionFinder to return a calculated solar return JD
    //         var expectedSolarReturnJd = radixJd + (age + 1) * 365.25;
    //         A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258))
    //             .Returns(expectedSolarReturnJd);
    //         
    //         // Act
    //         var result = _orchestrator.CalculateJdForSolar(request);
    //
    //         // Assert
    //         Assert.Multiple(() =>
    //         {
    //             Assert.That(result, Is.EqualTo(expectedSolarReturnJd), $"Should return correct JD for radix JD {radixJd}");
    //             Assert.That(result, Is.GreaterThan(radixJd), $"Solar return JD should be after radix JD {radixJd}");
    //         });
    //     }
    // }
    //
    // [Test]
    // public void CalculateJdForSolar_ConsistencyBetweenCalls_ReturnsSameResult()
    // {
    //     // Arrange
    //     var radixJd = 2459580.5;
    //     var age = 25;
    //     var calculationPreferences = CreateCalculationPreferences();
    //     var radixLocation = new Location("Test", 6.53, 52.0);
    //     
    //     var request = new SolarRequest(radixJd, age, true, calculationPreferences, radixLocation, null);
    //     
    //     // Mock the Sun's position at radix time using ICelPointsHandler
    //     var radixSunPosition = 280.5;
    //     var mockSunPositions = CreateMockFullPointPos(radixSunPosition);
    //     A.CallTo(() => _celPointsHandler.CalcSinglePointWithSe(ChartPoints.Sun, radixJd, A<Location>._, calculationPreferences))
    //         .Returns(mockSunPositions);
    //     
    //     // Mock the flags calculation
    //     A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
    //         .Returns(258);
    //     
    //     // Mock the JdForPositionFinder to return a calculated solar return JD
    //     var expectedSolarReturnJd = radixJd + (age + 1) * 365.25;
    //     A.CallTo(() => _jdForPositionFinder.FindJulianDay(radixSunPosition, A<double>._, 258))
    //         .Returns(expectedSolarReturnJd);
    //     
    //     // Act - Multiple calls with same parameters
    //     var result1 = _orchestrator.CalculateJdForSolar(request);
    //     var result2 = _orchestrator.CalculateJdForSolar(request);
    //     var result3 = _orchestrator.CalculateJdForSolar(request);
    //
    //     // Assert - Results should be consistent
    //     Assert.Multiple(() =>
    //     {
    //         Assert.That(result1, Is.EqualTo(result2), "First and second calls should return same JD");
    //         Assert.That(result2, Is.EqualTo(result3), "Second and third calls should return same JD");
    //         Assert.That(result1, Is.EqualTo(result3), "First and third calls should return same JD");
    //         Assert.That(result1, Is.EqualTo(expectedSolarReturnJd), "Result should match expected solar return JD");
    //     });
    // }
    //
    // private static CalculationPreferences CreateCalculationPreferences()
    // {
    //     return new CalculationPreferences(
    //         new List<ChartPoints> { ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mercury },
    //         ZodiacTypes.Tropical,
    //         Ayanamshas.None,
    //         CoordinateSystems.Ecliptical,
    //         ObserverPositions.GeoCentric,
    //         ProjectionTypes.TwoDimensional,
    //         HouseSystems.Regiomontanus,
    //         ApogeeTypes.Duval,
    //         false);
    // }
    //
    // private static FullPointPos CreateMockFullPointPos(double longitude)
    // {
    //     var mainPosSpeed = new PosSpeed(longitude, 0.0);
    //     var deviationPosSpeed = new PosSpeed(0.0, 0.0);
    //     var distancePosSpeed = new PosSpeed(1.0, 0.0);
    //     var eclipticalPosSpeeds = new PointPosSpeeds(mainPosSpeed, deviationPosSpeed, distancePosSpeed);
    //     
    //     var equatorialPosSpeeds = new PointPosSpeeds(
    //         new PosSpeed(0.0, 0.0), // RA
    //         new PosSpeed(0.0, 0.0), // Declination
    //         new PosSpeed(1.0, 0.0)  // Distance
    //     );
    //     
    //     var horizontalPosSpeeds = new PointPosSpeeds(
    //         new PosSpeed(0.0, 0.0), // Azimuth
    //         new PosSpeed(0.0, 0.0), // Altitude
    //         new PosSpeed(1.0, 0.0)  // Distance
    //     );
    //     
    //     return new FullPointPos(eclipticalPosSpeeds, equatorialPosSpeeds, horizontalPosSpeeds);
    // }
} 