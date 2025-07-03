// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.Enneagram;
using Enigma.Domain.Exceptions;
using Enigma.Facades.Se;
using FakeItEasy;

namespace Enigma.Test.Core.Slices.Enneagram;

[TestFixture]
public class TestCalcHousesForEnneagram
{
    private CalcHousesForEnneagram _calcHousesForEnneagram = null!;
    private IHousesFacade _housesFacade = null!;
    private const double VALID_JD = 2459580.5; // 2022-01-01
    private const double VALID_LAT = 52.0;
    private const double VALID_LON = 6.53;

    [SetUp]
    public void SetUp()
    {
        _housesFacade = A.Fake<IHousesFacade>();
        _calcHousesForEnneagram = new CalcHousesForEnneagram(_housesFacade);
    }

    [Test]
    public void Constructor_NullFacade_ThrowsArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => 
            new CalcHousesForEnneagram(null!));
        Assert.That(exception.ParamName, Is.EqualTo("housesFacade"));
    }

    [Test]
    public void CalcHouses_ValidParameters_ReturnsExpectedResult()
    {
        // Arrange
        var expectedCusps = new double[] { 0.0, 10.0, 40.0, 70.0, 100.0, 130.0, 160.0, 190.0, 220.0, 250.0, 280.0, 310.0, 340.0 };
        var expectedMundanePoints = new double[] { 10.0, 280.0, 281.0, 192.0, 12.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
        var expectedResult = new double[][] { expectedCusps, expectedMundanePoints };
        
        A.CallTo(() => _housesFacade.RetrieveHouses(VALID_JD, 2, VALID_LAT, VALID_LON, 'P'))
            .Returns(expectedResult);

        // Act
        var result = _calcHousesForEnneagram.CalcHouses(VALID_JD, VALID_LON, VALID_LAT);

        // Assert
        Assert.That(result, Is.EqualTo(expectedCusps));
        A.CallTo(() => _housesFacade.RetrieveHouses(VALID_JD, 2, VALID_LAT, VALID_LON, 'P'))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void CalcHouses_InvalidJulianDay_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _calcHousesForEnneagram.CalcHouses(double.NaN, VALID_LON, VALID_LAT));
        Assert.That(exception.ParamName, Is.EqualTo("julianDay"));
        Assert.That(exception.Message, Does.Contain("must be a finite number"));
    }

    [Test]
    public void CalcHouses_InfinityJulianDay_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _calcHousesForEnneagram.CalcHouses(double.PositiveInfinity, VALID_LON, VALID_LAT));
        Assert.That(exception.ParamName, Is.EqualTo("julianDay"));
        Assert.That(exception.Message, Does.Contain("must be a finite number"));
    }

    [Test]
    public void CalcHouses_NegativeInfinityJulianDay_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _calcHousesForEnneagram.CalcHouses(double.NegativeInfinity, VALID_LON, VALID_LAT));
        Assert.That(exception.ParamName, Is.EqualTo("julianDay"));
        Assert.That(exception.Message, Does.Contain("must be a finite number"));
    }

    [Test]
    public void CalcHouses_InvalidLatitude_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _calcHousesForEnneagram.CalcHouses(VALID_JD, VALID_LON, 91));
        Assert.That(exception.ParamName, Is.EqualTo("geoLat"));
        Assert.That(exception.Message, Does.Contain("must be between -90 and 90 degrees (exclusive)"));
    }

    [Test]
    public void CalcHouses_NegativeLatitude_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _calcHousesForEnneagram.CalcHouses(VALID_JD, VALID_LON, -91));
        Assert.That(exception.ParamName, Is.EqualTo("geoLat"));
        Assert.That(exception.Message, Does.Contain("must be between -90 and 90 degrees (exclusive)"));
    }

    [Test]
    public void CalcHouses_Latitude90_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _calcHousesForEnneagram.CalcHouses(VALID_JD, VALID_LON, 90));
        Assert.That(exception.ParamName, Is.EqualTo("geoLat"));
        Assert.That(exception.Message, Does.Contain("must be between -90 and 90 degrees (exclusive)"));
    }

    [Test]
    public void CalcHouses_LatitudeMinus90_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _calcHousesForEnneagram.CalcHouses(VALID_JD, VALID_LON, -90));
        Assert.That(exception.ParamName, Is.EqualTo("geoLat"));
        Assert.That(exception.Message, Does.Contain("must be between -90 and 90 degrees (exclusive)"));
    }

    [Test]
    public void CalcHouses_InvalidLongitude_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _calcHousesForEnneagram.CalcHouses(VALID_JD, 181, VALID_LAT));
        Assert.That(exception.ParamName, Is.EqualTo("geoLon"));
        Assert.That(exception.Message, Does.Contain("must be between -180 and 180 degrees"));
    }

    [Test]
    public void CalcHouses_NegativeLongitude_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            _calcHousesForEnneagram.CalcHouses(VALID_JD, -181, VALID_LAT));
        Assert.That(exception.ParamName, Is.EqualTo("geoLon"));
    }

    [Test]
    public void CalcHouses_SwissEphException_ReThrowsException()
    {
        // Arrange
        var swissEphException = new SwissEphException("Test SwissEph error");
        A.CallTo(() => _housesFacade.RetrieveHouses(A<double>._, A<int>._, A<double>._, A<double>._, A<char>._))
            .Throws(swissEphException);

        // Act & Assert
        var exception = Assert.Throws<SwissEphException>(() => 
            _calcHousesForEnneagram.CalcHouses(VALID_JD, VALID_LON, VALID_LAT));
        Assert.That(exception, Is.SameAs(swissEphException));
    }

    [Test]
    public void CalcHouses_UnexpectedException_WrapsInEnigmaException()
    {
        // Arrange
        var unexpectedException = new InvalidOperationException("Unexpected error");
        A.CallTo(() => _housesFacade.RetrieveHouses(A<double>._, A<int>._, A<double>._, A<double>._, A<char>._))
            .Throws(unexpectedException);

        // Act & Assert
        var exception = Assert.Throws<EnigmaException>(() => 
            _calcHousesForEnneagram.CalcHouses(VALID_JD, VALID_LON, VALID_LAT));
        Assert.That(exception.Message, Does.Contain("Unexpected error calculating Placidus houses"));
        Assert.That(exception.Message, Does.Contain("Unexpected error"));
    }

    [Test]
    public void CalcHouses_ValidBoundaryValues_ReturnsExpectedResult()
    {
        // Arrange - Test boundary values including negative Julian days
        var testCases = new[]
        {
            (jd: 1.0, lat: 89.9, lon: 180.0), // Near North Pole
            (jd: 1.0, lat: -89.9, lon: -180.0), // Near South Pole
            (jd: 1.0, lat: 0.0, lon: 0.0), // Equator
            (jd: -1000000.0, lat: 45.0, lon: 0.0), // Very old date
            (jd: 0.0, lat: 45.0, lon: 0.0), // Julian day epoch
            (jd: -2451545.0, lat: 45.0, lon: 0.0) // Before Julian day epoch
        };

        var expectedCusps = new double[] { 0.0, 10.0, 40.0, 70.0, 100.0, 130.0, 160.0, 190.0, 220.0, 250.0, 280.0, 310.0, 340.0 };
        var expectedMundanePoints = new double[] { 10.0, 280.0, 281.0, 192.0, 12.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
        var expectedResult = new double[][] { expectedCusps, expectedMundanePoints };

        foreach (var (jd, lat, lon) in testCases)
        {
            A.CallTo(() => _housesFacade.RetrieveHouses(jd, 2, lat, lon, 'P'))
                .Returns(expectedResult);

            // Act
            var result = _calcHousesForEnneagram.CalcHouses(jd, lon, lat);

            // Assert
            Assert.That(result, Is.EqualTo(expectedCusps));
        }
    }

    [Test]
    public void CalcHouses_ReturnsCorrectArrayLength()
    {
        // Arrange
        var expectedCusps = new double[] { 0.0, 10.0, 40.0, 70.0, 100.0, 130.0, 160.0, 190.0, 220.0, 250.0, 280.0, 310.0, 340.0 };
        var expectedMundanePoints = new double[] { 10.0, 280.0, 281.0, 192.0, 12.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
        var expectedResult = new double[][] { expectedCusps, expectedMundanePoints };
        
        A.CallTo(() => _housesFacade.RetrieveHouses(A<double>._, A<int>._, A<double>._, A<double>._, A<char>._))
            .Returns(expectedResult);

        // Act
        var result = _calcHousesForEnneagram.CalcHouses(VALID_JD, VALID_LON, VALID_LAT);

        // Assert
        Assert.That(result, Has.Length.EqualTo(13));
    }

    [Test]
    public void CalcHouses_FirstPositionIsZero_AsDocumented()
    {
        // Arrange
        var expectedCusps = new double[] { 0.0, 10.0, 40.0, 70.0, 100.0, 130.0, 160.0, 190.0, 220.0, 250.0, 280.0, 310.0, 340.0 };
        var expectedMundanePoints = new double[] { 10.0, 280.0, 281.0, 192.0, 12.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
        var expectedResult = new double[][] { expectedCusps, expectedMundanePoints };
        
        A.CallTo(() => _housesFacade.RetrieveHouses(A<double>._, A<int>._, A<double>._, A<double>._, A<char>._))
            .Returns(expectedResult);

        // Act
        var result = _calcHousesForEnneagram.CalcHouses(VALID_JD, VALID_LON, VALID_LAT);

        // Assert
        Assert.That(result[0], Is.EqualTo(0.0), "First position should be 0 as documented");
    }
} 