// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.Solar;
using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;
using FakeItEasy;

namespace Enigma.Test.Core.Slices.Solar;

[TestFixture]
public class TestJdForPosition
{
    private const double DELTA = 0.00000001;
    private const double ESTIMATED_JD = 2460100.5; // Example Julian Day
    private const double TARGET_POSITION = 180.0; // Example target position in degrees
    private const double SUN_POSITION_1 = 175.0; // Example Sun position
    private const double SUN_POSITION_2 = 185.0; // Example Sun position after adjustment
    
    private ISunCalculator? _sunCalculator;
    private ISeFlags? _seFlags;
    private JdForPosition? _jdForPosition;
    private SolarRequest? _solarRequest;

    [SetUp]
    public void SetUp()
    {
        _sunCalculator = A.Fake<ISunCalculator>();
        _seFlags = A.Fake<ISeFlags>();
        
        // Setup default behavior for SunCalculator
        A.CallTo(() => _sunCalculator.CalcPositionSun(A<double>._, A<int>._))
            .Returns(SUN_POSITION_1);
        
        // Setup default behavior for SeFlags
        A.CallTo(() => _seFlags.DefineFlags(A<CoordinateSystems>._, A<ObserverPositions>._, A<ZodiacTypes>._))
            .Returns(258); // Default flags for tropical geocentric
        
        _jdForPosition = new JdForPosition(_sunCalculator, _seFlags);
        
        // Create a default SolarRequest
        var calculationPreferences = CreateDefaultCalculationPreferences();
        var location = new Location("Test Location", 0.0, 0.0);
        _solarRequest = new SolarRequest(ESTIMATED_JD, 25, false, calculationPreferences, location, null);
    }

    [Test]
    public void TestFindJdForPosition_BasicCalculation()
    {
        // Arrange
        A.CallTo(() => _sunCalculator.CalcPositionSun(ESTIMATED_JD, 258))
            .Returns(SUN_POSITION_1);
        
        // Act
        double result = _jdForPosition!.FindJdForPosition(ESTIMATED_JD, TARGET_POSITION, _solarRequest!);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _sunCalculator.CalcPositionSun(A<double>._, A<int>._)).MustHaveHappened();
        A.CallTo(() => _seFlags!.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .MustHaveHappened();
    }

    [Test]
    public void TestFindJdForPosition_SiderealZodiac()
    {
        // Arrange
        var siderealPreferences = CreateCalculationPreferences(ZodiacTypes.Sidereal);
        var siderealRequest = new SolarRequest(ESTIMATED_JD, 25, true, siderealPreferences, 
            new Location("Test", 0.0, 0.0), null);
        
        A.CallTo(() => _sunCalculator.CalcPositionSun(A<double>._, A<int>._))
            .Returns(SUN_POSITION_1);
        
        // Act
        double result = _jdForPosition!.FindJdForPosition(ESTIMATED_JD, TARGET_POSITION, siderealRequest);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _seFlags!.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Sidereal))
            .MustHaveHappened();
    }

    [Test]
    public void TestFindJdForPosition_TopocentricObserver()
    {
        // Arrange
        var topoPreferences = CreateCalculationPreferences(ZodiacTypes.Tropical, ObserverPositions.TopoCentric);
        var location = new Location("Test Location", 10.0, 50.0);
        var topoRequest = new SolarRequest(ESTIMATED_JD, 25, false, topoPreferences, location, null);
        
        A.CallTo(() => _sunCalculator.CalcPositionSun(A<double>._, A<int>._))
            .Returns(SUN_POSITION_1);
        
        // Act
        double result = _jdForPosition!.FindJdForPosition(ESTIMATED_JD, TARGET_POSITION, topoRequest);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _seFlags!.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.TopoCentric, ZodiacTypes.Tropical))
            .MustHaveHappened();
    }

    [Test]
    public void TestFindJdForPosition_WithRelocateLocation()
    {
        // Arrange
        var radixLocation = new Location("Radix Location", 0.0, 0.0);
        var relocateLocation = new Location("Relocate Location", 20.0, 60.0);
        var requestWithRelocate = new SolarRequest(ESTIMATED_JD, 25, false, 
            CreateDefaultCalculationPreferences(), radixLocation, relocateLocation);
        
        A.CallTo(() => _sunCalculator.CalcPositionSun(A<double>._, A<int>._))
            .Returns(SUN_POSITION_1);
        
        // Act
        double result = _jdForPosition!.FindJdForPosition(ESTIMATED_JD, TARGET_POSITION, requestWithRelocate);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
    }

    [Test]
    public void TestFindJdForPosition_ExactMatch()
    {
        // Arrange
        A.CallTo(() => _sunCalculator.CalcPositionSun(ESTIMATED_JD, 258))
            .Returns(TARGET_POSITION); // Exact match
        
        // Act
        double result = _jdForPosition!.FindJdForPosition(ESTIMATED_JD, TARGET_POSITION, _solarRequest!);
        
        // Assert
        Assert.That(result, Is.EqualTo(ESTIMATED_JD).Within(DELTA));
    }

    [Test]
    public void TestFindJdForPosition_CrossingZeroDegree()
    {
        // Arrange
        A.CallTo(() => _sunCalculator.CalcPositionSun(ESTIMATED_JD, 258))
            .Returns(350.0); // Sun at 350°
        A.CallTo(() => _sunCalculator.CalcPositionSun(A<double>._, 258))
            .Returns(355.0); // Subsequent calls return 355°
        
        // Act
        double result = _jdForPosition!.FindJdForPosition(ESTIMATED_JD, 5.0, _solarRequest!); // Target at 5°
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
    }

    [Test]
    public void TestFindJdForPosition_Crossing360Degree()
    {
        // Arrange
        A.CallTo(() => _sunCalculator.CalcPositionSun(ESTIMATED_JD, 258))
            .Returns(10.0); // Sun at 10°
        A.CallTo(() => _sunCalculator.CalcPositionSun(A<double>._, 258))
            .Returns(5.0); // Subsequent calls return 5°
        
        // Act
        double result = _jdForPosition!.FindJdForPosition(ESTIMATED_JD, 350.0, _solarRequest!); // Target at 350°
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
    }

    [Test]
    public void TestFindJdForPosition_Convergence()
    {
        // Arrange
        var positions = new[] { 175.0, 180.0, 179.5, 180.1, 179.9, 180.0 };
        var callCount = 0;
        
        A.CallTo(() => _sunCalculator.CalcPositionSun(A<double>._, A<int>._))
            .ReturnsLazily(() => positions[Math.Min(callCount++, positions.Length - 1)]);
        
        // Act
        double result = _jdForPosition!.FindJdForPosition(ESTIMATED_JD, TARGET_POSITION, _solarRequest!);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        Assert.That(callCount, Is.GreaterThan(1));
    }

    [Test]
    public void TestFindJdForPosition_EdgeCaseWithInvalidInputs()
    {
        // Arrange
        const double invalidJd = -1000.0;
        const double invalidPosition = 400.0; // Beyond 360°
        
        // Act - The method doesn't validate inputs, so it should not throw
        double result1 = _jdForPosition!.FindJdForPosition(invalidJd, TARGET_POSITION, _solarRequest!);
        double result2 = _jdForPosition!.FindJdForPosition(ESTIMATED_JD, invalidPosition, _solarRequest!);
        
        // Assert - Should return values (even if invalid)
        Assert.That(result1, Is.Not.NaN);
        Assert.That(result2, Is.Not.NaN);
    }

    [Test]
    public void TestFindJdForPosition_NullRequestThrowsNullReferenceException()
    {
        // Act & Assert
        Assert.Throws<NullReferenceException>(() => 
            _jdForPosition!.FindJdForPosition(ESTIMATED_JD, TARGET_POSITION, null!));
    }

    private static CalculationPreferences CreateDefaultCalculationPreferences()
    {
        return CreateCalculationPreferences(ZodiacTypes.Tropical, ObserverPositions.GeoCentric);
    }

    private static CalculationPreferences CreateCalculationPreferences(ZodiacTypes zodiacType, 
        ObserverPositions observerPosition = ObserverPositions.GeoCentric)
    {
        List<ChartPoints> points = [ChartPoints.Sun];
        return new CalculationPreferences(points, zodiacType, Ayanamshas.Fagan, 
            CoordinateSystems.Ecliptical, observerPosition, ProjectionTypes.TwoDimensional, 
            HouseSystems.Apc, ApogeeTypes.Interpolated, false);
    }
} 