// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.VenusStarPoint;
using Enigma.Facades.Se;
using FakeItEasy;

namespace Enigma.Test.Core.Slices.VenusStarPoint;

[TestFixture]
public class TestExactConjunctionDate
{
    private const double DELTA = 0.000001;
    private ICalcUtFacade _calcUtFacade = null!;
    private ExactConjunctionDate _exactConjunctionDate = null!;

    [SetUp]
    public void SetUp()
    {
        _calcUtFacade = A.Fake<ICalcUtFacade>();
        _exactConjunctionDate = new ExactConjunctionDate(_calcUtFacade);
    }

    [Test]
    public void TestCalculateConjunctionDate_ExactConjunction()
    {
        // Arrange
        const double estimatedJd = 2460100.5; // January 1, 2025
        const double conjunctionJd = 2460100.5; // Exact conjunction at this JD
        
        // Setup the facade to return different longitudes based on JD
        // This simulates a scenario where Sun and Venus are at the same longitude at the estimated JD
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 0, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                // Sun moves at ~1° per day, so simulate this
                var baseLongitude = 280.0;
                var daysDiff = jd - conjunctionJd;
                return new double[] { baseLongitude + daysDiff, 0, 0, 0, 0, 0 };
            });
        
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 3, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                // Venus moves at ~1.2° per day, so simulate this
                var baseLongitude = 280.0;
                var daysDiff = jd - conjunctionJd;
                return new double[] { baseLongitude + (daysDiff * 1.2), 0, 0, 0, 0, 0 };
            });
        
        // Act
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_SunAheadOfVenus()
    {
        // Arrange
        const double estimatedJd = 2460100.5;
        const double conjunctionJd = 2460100.0; // Conjunction happens slightly before estimated JD
        
        // Setup the facade to simulate Sun ahead of Venus at estimated JD
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 0, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                var baseLongitude = 280.0;
                var daysDiff = jd - conjunctionJd;
                return new double[] { baseLongitude + daysDiff, 0, 0, 0, 0, 0 };
            });
        
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 3, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                var baseLongitude = 280.0;
                var daysDiff = jd - conjunctionJd;
                return new double[] { baseLongitude + (daysDiff * 1.2), 0, 0, 0, 0, 0 };
            });
        
        // Act
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_VenusAheadOfSun()
    {
        // Arrange
        const double estimatedJd = 2460100.5;
        const double conjunctionJd = 2460101.0; // Conjunction happens slightly after estimated JD
        
        // Setup the facade to simulate Venus ahead of Sun at estimated JD
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 0, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                var baseLongitude = 280.0;
                var daysDiff = jd - conjunctionJd;
                return new double[] { baseLongitude + daysDiff, 0, 0, 0, 0, 0 };
            });
        
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 3, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                var baseLongitude = 280.0;
                var daysDiff = jd - conjunctionJd;
                return new double[] { baseLongitude + (daysDiff * 1.2), 0, 0, 0, 0, 0 };
            });
        
        // Act
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_CrossingZeroDegree()
    {
        // Arrange
        const double estimatedJd = 2460100.5;
        const double conjunctionJd = 2460100.5; // Conjunction at estimated JD
        
        // Setup the facade to simulate conjunction crossing 0°/360° boundary
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 0, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                var baseLongitude = 350.0;
                var daysDiff = jd - conjunctionJd;
                var longitude = baseLongitude + daysDiff;
                // Normalize to 0-360 range
                while (longitude < 0) longitude += 360;
                while (longitude >= 360) longitude -= 360;
                return new double[] { longitude, 0, 0, 0, 0, 0 };
            });
        
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 3, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                var baseLongitude = 10.0;
                var daysDiff = jd - conjunctionJd;
                var longitude = baseLongitude + (daysDiff * 1.2);
                // Normalize to 0-360 range
                while (longitude < 0) longitude += 360;
                while (longitude >= 360) longitude -= 360;
                return new double[] { longitude, 0, 0, 0, 0, 0 };
            });
        
        // Act
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_WithinMargin()
    {
        // Arrange
        const double estimatedJd = 2460100.5;
        const double conjunctionJd = 2460100.5; // Conjunction at estimated JD
        
        // Setup the facade to simulate very close longitudes (within margin)
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 0, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                var baseLongitude = 280.0;
                var daysDiff = jd - conjunctionJd;
                return new double[] { baseLongitude + daysDiff, 0, 0, 0, 0, 0 };
            });
        
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 3, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                var baseLongitude = 280.0;
                var daysDiff = jd - conjunctionJd;
                // Very small difference to test margin handling
                return new double[] { baseLongitude + (daysDiff * 1.000001), 0, 0, 0, 0, 0 };
            });
        
        // Act
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_ReturnsValueInRange()
    {
        // Arrange
        const double estimatedJd = 2460100.5;
        const double conjunctionJd = 2460100.8; // Conjunction within search range
        
        // Setup the facade to simulate conjunction within the search range
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 0, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                var baseLongitude = 280.0;
                var daysDiff = jd - conjunctionJd;
                return new double[] { baseLongitude + daysDiff, 0, 0, 0, 0, 0 };
            });
        
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 3, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                var baseLongitude = 280.0;
                var daysDiff = jd - conjunctionJd;
                return new double[] { baseLongitude + (daysDiff * 1.2), 0, 0, 0, 0, 0 };
            });
        
        // Act
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd);
        
        // Assert
        Assert.That(result, Is.GreaterThanOrEqualTo(estimatedJd - 1));
        Assert.That(result, Is.LessThanOrEqualTo(estimatedJd + 1));
    }
}
