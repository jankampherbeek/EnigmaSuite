// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.SingleCoordinateCalc;
using Enigma.Facades.Se;
using FakeItEasy;

namespace Enigma.Test.Core.Slices.SingleCoordinateCalc;

[TestFixture]
public class TestSinglePositionCalculator
{
    private ICalcUtFacade _calcUtFacade = null!;
    private SinglePositionCalculator _positionCalculator = null!;
    private const double DELTA = 0.00000001;
    private const double JD = 2459580.5; // 2022-01-01

    [SetUp]
    public void SetUp()
    {
        _calcUtFacade = A.Fake<ICalcUtFacade>();
        _positionCalculator = new SinglePositionCalculator(_calcUtFacade);
    }

    [Test]
    public void TestCalcSinglePosition_MainPosition_ReturnsFirstElement()
    {
        // Arrange
        const int pointId = 0; // Sun
        const int flags = 0;
        const bool isMainPos = true;
        var positions = new[] { 280.5, -2.3, 1.0, 0.1, 0.05, 0.001 }; // longitude, latitude, distance, speeds
        const double expectedPosition = 280.5;

        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, pointId, flags))
            .Returns(positions);

        // Act
        var result = _positionCalculator.CalcSinglePosition(JD, pointId, flags, isMainPos);

        // Assert
        Assert.That(result, Is.EqualTo(expectedPosition).Within(DELTA));
        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, pointId, flags))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void TestCalcSinglePosition_DeviationPosition_ReturnsSecondElement()
    {
        // Arrange
        const int pointId = 1; // Moon
        const int flags = 64; // Equatorial
        const bool isMainPos = false;
        var positions = new[] { 185.7, -15.8, 1.0, 0.1, 0.05, 0.001 }; // ra, declination, distance, speeds
        const double expectedPosition = -15.8;

        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, pointId, flags))
            .Returns(positions);

        // Act
        var result = _positionCalculator.CalcSinglePosition(JD, pointId, flags, isMainPos);

        // Assert
        Assert.That(result, Is.EqualTo(expectedPosition).Within(DELTA));
        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, pointId, flags))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void TestCalcSinglePosition_DifferentPointIds()
    {
        // Arrange
        var pointIds = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // Sun, Moon, Mercury, Venus, Mars, Jupiter, Saturn, Uranus, Neptune, Pluto, True Node
        const int flags = 0;
        const bool isMainPos = true;
        const double expectedPosition = 100.0;

        foreach (var pointId in pointIds)
        {
            var positions = new[] { expectedPosition, 0.0, 1.0, 0.0, 0.0, 0.0 };
            A.CallTo(() => _calcUtFacade.PositionFromSe(JD, pointId, flags))
                .Returns(positions);

            // Act
            var result = _positionCalculator.CalcSinglePosition(JD, pointId, flags, isMainPos);

            // Assert
            Assert.That(result, Is.EqualTo(expectedPosition).Within(DELTA));
        }

        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, flags))
            .MustHaveHappened(pointIds.Length, Times.Exactly);
    }

    [Test]
    public void TestCalcSinglePosition_DifferentFlags()
    {
        // Arrange
        const int pointId = 0; // Sun
        var flags = new[] { 0, 64, 128, 256 }; // Different flag combinations
        const bool isMainPos = true;
        const double expectedPosition = 200.0;

        foreach (var flag in flags)
        {
            var positions = new[] { expectedPosition, 0.0, 1.0, 0.0, 0.0, 0.0 };
            A.CallTo(() => _calcUtFacade.PositionFromSe(JD, pointId, flag))
                .Returns(positions);

            // Act
            var result = _positionCalculator.CalcSinglePosition(JD, pointId, flag, isMainPos);

            // Assert
            Assert.That(result, Is.EqualTo(expectedPosition).Within(DELTA));
        }

        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, pointId, A<int>._))
            .MustHaveHappened(flags.Length, Times.Exactly);
    }

    [Test]
    public void TestCalcSinglePosition_DifferentJulianDays()
    {
        // Arrange
        const int pointId = 0; // Sun
        const int flags = 0;
        const bool isMainPos = true;
        var julianDays = new[] { 2459580.5, 2459581.5, 2459582.5, 2459583.5 }; // Different dates
        const double expectedPosition = 150.0;

        foreach (var jd in julianDays)
        {
            var positions = new[] { expectedPosition, 0.0, 1.0, 0.0, 0.0, 0.0 };
            A.CallTo(() => _calcUtFacade.PositionFromSe(jd, pointId, flags))
                .Returns(positions);

            // Act
            var result = _positionCalculator.CalcSinglePosition(jd, pointId, flags, isMainPos);

            // Assert
            Assert.That(result, Is.EqualTo(expectedPosition).Within(DELTA));
        }

        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, pointId, flags))
            .MustHaveHappened(julianDays.Length, Times.Exactly);
    }

    [Test]
    public void TestCalcSinglePosition_NegativeValues()
    {
        // Arrange
        const int pointId = 0; // Sun
        const int flags = 0;
        const bool isMainPos = false; // Get latitude/declination
        var positions = new[] { 280.5, -2.3, 1.0, 0.1, 0.05, 0.001 };
        const double expectedPosition = -2.3;

        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, pointId, flags))
            .Returns(positions);

        // Act
        var result = _positionCalculator.CalcSinglePosition(JD, pointId, flags, isMainPos);

        // Assert
        Assert.That(result, Is.EqualTo(expectedPosition).Within(DELTA));
    }

    [Test]
    public void TestCalcSinglePosition_LargeValues()
    {
        // Arrange
        const int pointId = 0; // Sun
        const int flags = 0;
        const bool isMainPos = true;
        var positions = new[] { 359.999, 0.0, 1.0, 0.0, 0.0, 0.0 }; // Near 360 degrees
        const double expectedPosition = 359.999;

        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, pointId, flags))
            .Returns(positions);

        // Act
        var result = _positionCalculator.CalcSinglePosition(JD, pointId, flags, isMainPos);

        // Assert
        Assert.That(result, Is.EqualTo(expectedPosition).Within(DELTA));
    }

    [Test]
    public void TestCalcSinglePosition_ZeroValues()
    {
        // Arrange
        const int pointId = 0; // Sun
        const int flags = 0;
        const bool isMainPos = true;
        var positions = new[] { 0.0, 0.0, 1.0, 0.0, 0.0, 0.0 }; // Zero longitude
        const double expectedPosition = 0.0;

        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, pointId, flags))
            .Returns(positions);

        // Act
        var result = _positionCalculator.CalcSinglePosition(JD, pointId, flags, isMainPos);

        // Assert
        Assert.That(result, Is.EqualTo(expectedPosition).Within(DELTA));
    }

    [Test]
    public void TestCalcSinglePosition_NullCalcUtFacade_ThrowsArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => 
            new SinglePositionCalculator(null!));
        Assert.That(exception!.ParamName, Is.EqualTo("calcUtFacade"));
    }

    [Test]
    public void TestCalcSinglePosition_RealisticPositions()
    {
        // Test with realistic astronomical positions
        var testCases = new[]
        {
            new { PointId = 0, IsMainPos = true, Expected = 280.5, Positions = new[] { 280.5, -2.3, 1.0, 0.1, 0.05, 0.001 } }, // Sun longitude
            new { PointId = 0, IsMainPos = false, Expected = -2.3, Positions = new[] { 280.5, -2.3, 1.0, 0.1, 0.05, 0.001 } }, // Sun latitude
            new { PointId = 1, IsMainPos = true, Expected = 45.2, Positions = new[] { 45.2, 3.1, 1.0, 0.1, 0.05, 0.001 } }, // Moon longitude
            new { PointId = 1, IsMainPos = false, Expected = 3.1, Positions = new[] { 45.2, 3.1, 1.0, 0.1, 0.05, 0.001 } }, // Moon latitude
            new { PointId = 5, IsMainPos = true, Expected = 125.8, Positions = new[] { 125.8, -1.2, 1.0, 0.1, 0.05, 0.001 } }, // Jupiter longitude
            new { PointId = 5, IsMainPos = false, Expected = -1.2, Positions = new[] { 125.8, -1.2, 1.0, 0.1, 0.05, 0.001 } } // Jupiter latitude
        };

        const int flags = 0;

        foreach (var testCase in testCases)
        {
            A.CallTo(() => _calcUtFacade.PositionFromSe(JD, testCase.PointId, flags))
                .Returns(testCase.Positions);

            // Act
            var result = _positionCalculator.CalcSinglePosition(JD, testCase.PointId, flags, testCase.IsMainPos);

            // Assert
            Assert.That(result, Is.EqualTo(testCase.Expected).Within(DELTA));
        }

        A.CallTo(() => _calcUtFacade.PositionFromSe(JD, A<int>._, flags))
            .MustHaveHappened(testCases.Length, Times.Exactly);
    }
} 