// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Slices.SingleCoordinateCalc;
using Enigma.Domain.References;
using Enigma.Facades.Se;
using FakeItEasy;

namespace Enigma.Test.Core.Slices.SingleCoordinateCalc;

[TestFixture]
public class TestSingleCoordinateOrchestrator
{
    private ICalcUtFacade _calcUtFacade = null!;
    private ISeFlags _seFlags = null!;
    private SinglePositionCalculator _positionCalculator = null!;
    private SingleCoordinateOrchestrator _orchestrator = null!;
    private const double DELTA = 0.00000001;
    private const double JD = 2459580.5; // 2022-01-01

    [SetUp]
    public void SetUp()
    {
        _calcUtFacade = A.Fake<ICalcUtFacade>();
        _seFlags = A.Fake<ISeFlags>();
        _positionCalculator = new SinglePositionCalculator(_calcUtFacade);
        _orchestrator = new SingleCoordinateOrchestrator(_positionCalculator, _seFlags);
    }

    [Test]
    public void TestCalcSinglePositions_Longitude_HappyFlow()
    {
        // Arrange
        var coordinate = Coordinates.Longitude;
        var points = new List<ChartPoints> { ChartPoints.Sun, ChartPoints.Moon };
        var expectedPositions = new[] { 280.5, 45.2 };
        const int expectedFlags = 0;

        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(expectedFlags);
        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, expectedFlags))
            .ReturnsNextFromSequence(
                new[] { expectedPositions[0], 0.0, 1.0, 0.0, 0.0, 0.0 }, // Sun positions
                new[] { expectedPositions[1], 0.0, 1.0, 0.0, 0.0, 0.0 }  // Moon positions
            );

        // Act
        var result = _orchestrator.CalcSinglePositions(coordinate, points, JD);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result[0].Value, Is.EqualTo(expectedPositions[0]).Within(DELTA));
            Assert.That(result[1].Key, Is.EqualTo(ChartPoints.Moon));
            Assert.That(result[1].Value, Is.EqualTo(expectedPositions[1]).Within(DELTA));
        });

        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, expectedFlags))
            .MustHaveHappenedTwiceExactly();
    }

    [Test]
    public void TestCalcSinglePositions_Latitude_HappyFlow()
    {
        // Arrange
        var coordinate = Coordinates.Latitude;
        var points = new List<ChartPoints> { ChartPoints.Mars };
        const double expectedPosition = -2.3;
        const int expectedFlags = 0;

        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(expectedFlags);
        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, expectedFlags))
            .Returns(new[] { 0.0, expectedPosition, 1.0, 0.0, 0.0, 0.0 });

        // Act
        var result = _orchestrator.CalcSinglePositions(coordinate, points, JD);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Mars));
            Assert.That(result[0].Value, Is.EqualTo(expectedPosition).Within(DELTA));
        });

        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, expectedFlags))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void TestCalcSinglePositions_RightAscension_HappyFlow()
    {
        // Arrange
        var coordinate = Coordinates.RightAscension;
        var points = new List<ChartPoints> { ChartPoints.Jupiter };
        const double expectedPosition = 185.7;
        const int expectedFlags = 64; // SEFLG_EQUATORIAL

        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Equatorial, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(expectedFlags);
        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, expectedFlags))
            .Returns(new[] { expectedPosition, 0.0, 1.0, 0.0, 0.0, 0.0 });

        // Act
        var result = _orchestrator.CalcSinglePositions(coordinate, points, JD);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Jupiter));
            Assert.That(result[0].Value, Is.EqualTo(expectedPosition).Within(DELTA));
        });

        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Equatorial, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void TestCalcSinglePositions_Declination_HappyFlow()
    {
        // Arrange
        var coordinate = Coordinates.Declination;
        var points = new List<ChartPoints> { ChartPoints.Saturn };
        const double expectedPosition = -15.8;
        const int expectedFlags = 64; // SEFLG_EQUATORIAL

        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Equatorial, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(expectedFlags);
        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, expectedFlags))
            .Returns(new[] { 0.0, expectedPosition, 1.0, 0.0, 0.0, 0.0 });

        // Act
        var result = _orchestrator.CalcSinglePositions(coordinate, points, JD);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Saturn));
            Assert.That(result[0].Value, Is.EqualTo(expectedPosition).Within(DELTA));
        });

        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, expectedFlags))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void TestCalcSinglePositions_Azimuth_HappyFlow()
    {
        // Arrange
        var coordinate = Coordinates.Azimuth;
        var points = new List<ChartPoints> { ChartPoints.Venus };
        const double expectedPosition = 120.3;
        const int expectedFlags = 0; // Horizontal coordinates

        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Horizontal, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(expectedFlags);
        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, expectedFlags))
            .Returns(new[] { expectedPosition, 0.0, 1.0, 0.0, 0.0, 0.0 });

        // Act
        var result = _orchestrator.CalcSinglePositions(coordinate, points, JD);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Venus));
            Assert.That(result[0].Value, Is.EqualTo(expectedPosition).Within(DELTA));
        });

        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Horizontal, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void TestCalcSinglePositions_Altitude_HappyFlow()
    {
        // Arrange
        var coordinate = Coordinates.Altitude;
        var points = new List<ChartPoints> { ChartPoints.Mercury };
        const double expectedPosition = 35.7;
        const int expectedFlags = 0; // Horizontal coordinates

        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Horizontal, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(expectedFlags);
        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, expectedFlags))
            .Returns(new[] { 0.0, expectedPosition, 1.0, 0.0, 0.0, 0.0 });

        // Act
        var result = _orchestrator.CalcSinglePositions(coordinate, points, JD);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Mercury));
            Assert.That(result[0].Value, Is.EqualTo(expectedPosition).Within(DELTA));
        });

        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, expectedFlags))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void TestCalcSinglePositions_MultiplePoints()
    {
        // Arrange
        var coordinate = Coordinates.Longitude;
        var points = new List<ChartPoints> { ChartPoints.Sun, ChartPoints.Moon, ChartPoints.Mars };
        var expectedPositions = new[] { 280.5, 45.2, 125.8 };
        const int expectedFlags = 0;

        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(expectedFlags);
        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, expectedFlags))
            .ReturnsNextFromSequence(
                new[] { expectedPositions[0], 0.0, 1.0, 0.0, 0.0, 0.0 }, // Sun positions
                new[] { expectedPositions[1], 0.0, 1.0, 0.0, 0.0, 0.0 }, // Moon positions
                new[] { expectedPositions[2], 0.0, 1.0, 0.0, 0.0, 0.0 }  // Mars positions
            );

        // Act
        var result = _orchestrator.CalcSinglePositions(coordinate, points, JD);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Sun));
            Assert.That(result[0].Value, Is.EqualTo(expectedPositions[0]).Within(DELTA));
            Assert.That(result[1].Key, Is.EqualTo(ChartPoints.Moon));
            Assert.That(result[1].Value, Is.EqualTo(expectedPositions[1]).Within(DELTA));
            Assert.That(result[2].Key, Is.EqualTo(ChartPoints.Mars));
            Assert.That(result[2].Value, Is.EqualTo(expectedPositions[2]).Within(DELTA));
        });

        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, expectedFlags))
            .MustHaveHappened(3, Times.Exactly);
    }

    [Test]
    public void TestCalcSinglePositions_NullPoints_ThrowsArgumentNullException()
    {
        // Arrange
        var coordinate = Coordinates.Longitude;
        List<ChartPoints>? points = null;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => 
            _orchestrator.CalcSinglePositions(coordinate, points!, JD));
        Assert.That(exception!.ParamName, Is.EqualTo("points"));
    }

    [Test]
    public void TestCalcSinglePositions_EmptyPoints_ThrowsArgumentException()
    {
        // Arrange
        var coordinate = Coordinates.Longitude;
        var points = new List<ChartPoints>();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _orchestrator.CalcSinglePositions(coordinate, points, JD));
        Assert.That(exception!.Message, Does.Contain("cannot be empty"));
        Assert.That(exception.ParamName, Is.EqualTo("points"));
    }

    [Test]
    public void TestCalcSinglePositions_InvalidJulianDay_ThrowsArgumentException()
    {
        // Arrange
        var coordinate = Coordinates.Longitude;
        var points = new List<ChartPoints> { ChartPoints.Sun };
        const double invalidJd = -1.0;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _orchestrator.CalcSinglePositions(coordinate, points, invalidJd));
        Assert.That(exception!.Message, Does.Contain("must be positive"));
        Assert.That(exception.ParamName, Is.EqualTo("jd"));
    }

    [Test]
    public void TestCalcSinglePositions_ZeroJulianDay_ThrowsArgumentException()
    {
        // Arrange
        var coordinate = Coordinates.Longitude;
        var points = new List<ChartPoints> { ChartPoints.Sun };
        const double zeroJd = 0.0;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _orchestrator.CalcSinglePositions(coordinate, points, zeroJd));
        Assert.That(exception!.Message, Does.Contain("must be positive"));
        Assert.That(exception.ParamName, Is.EqualTo("jd"));
    }

    [Test]
    public void TestCalcSinglePositions_NullPositionCalculator_ThrowsArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => 
            new SingleCoordinateOrchestrator(null!, _seFlags));
        Assert.That(exception!.ParamName, Is.EqualTo("positionCalculator"));
    }

    [Test]
    public void TestCalcSinglePositions_NullSeFlags_ThrowsArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => 
            new SingleCoordinateOrchestrator(_positionCalculator, null!));
        Assert.That(exception!.ParamName, Is.EqualTo("seFlags"));
    }

    [Test]
    public void TestCalcSinglePositions_AllCoordinateTypes()
    {
        // Test that all coordinate types are supported
        var allCoordinates = Enum.GetValues<Coordinates>();
        var points = new List<ChartPoints> { ChartPoints.Sun };
        const int expectedFlags = 0;

        A.CallTo(() => _seFlags.DefineFlags(A<CoordinateSystems>._, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(expectedFlags);
        
        // Use a more sophisticated mock that returns different values based on the coordinate type
        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, expectedFlags))
            .ReturnsLazily((double jd, int pointId, int flags) =>
            {
                // Determine if this is a main position or deviation based on the coordinate
                // Since we can't directly access the coordinate in the mock, we'll return both values
                // and let the SinglePositionCalculator choose the correct one
                return new[] { 100.0, 100.0, 1.0, 0.0, 0.0, 0.0 };
            });

        foreach (var coordinate in allCoordinates)
        {
            // Act
            var result = _orchestrator.CalcSinglePositions(coordinate, points, JD);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0].Key, Is.EqualTo(ChartPoints.Sun));
                Assert.That(result[0].Value, Is.EqualTo(100.0).Within(DELTA));
            });
        }
    }

    [Test]
    public void TestCalcSinglePositions_CorrectFlagsForEachCoordinateSystem()
    {
        // Arrange
        var points = new List<ChartPoints> { ChartPoints.Sun };
        const int eclipticalFlags = 0;
        const int equatorialFlags = 64;
        const int horizontalFlags = 0;

        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(eclipticalFlags);
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Equatorial, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(equatorialFlags);
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Horizontal, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .Returns(horizontalFlags);
        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, A<int>._))
            .Returns(new[] { 100.0, 0.0, 1.0, 0.0, 0.0, 0.0 });

        // Act & Assert - Ecliptical coordinates
        _orchestrator.CalcSinglePositions(Coordinates.Longitude, points, JD);
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .MustHaveHappenedOnceExactly();

        _orchestrator.CalcSinglePositions(Coordinates.Latitude, points, JD);
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .MustHaveHappenedTwiceExactly();

        // Act & Assert - Equatorial coordinates
        _orchestrator.CalcSinglePositions(Coordinates.RightAscension, points, JD);
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Equatorial, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .MustHaveHappenedOnceExactly();

        _orchestrator.CalcSinglePositions(Coordinates.Declination, points, JD);
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Equatorial, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .MustHaveHappenedTwiceExactly();

        // Act & Assert - Horizontal coordinates
        _orchestrator.CalcSinglePositions(Coordinates.Azimuth, points, JD);
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Horizontal, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .MustHaveHappenedOnceExactly();

        _orchestrator.CalcSinglePositions(Coordinates.Altitude, points, JD);
        A.CallTo(() => _seFlags.DefineFlags(CoordinateSystems.Horizontal, ObserverPositions.GeoCentric, ZodiacTypes.Tropical))
            .MustHaveHappenedTwiceExactly();
    }
} 