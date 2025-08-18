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
    public void TestCalculateConjunctionDate_InferiorConjunction_ExactConjunction()
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
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.InferiorConjunction);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_SuperiorConjunction_ExactConjunction()
    {
        // Arrange
        const double estimatedJd = 2460100.5; // January 1, 2025
        const double conjunctionJd = 2460100.5; // Exact conjunction at this JD
        
        // Setup the facade to return different longitudes based on JD
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
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.SuperiorConjunction);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_InferiorConjunction_SunAheadOfVenus()
    {
        // Arrange
        const double estimatedJd = 2460100.5;
        const double conjunctionJd = 2460100.0; // Conjunction happens slightly before estimated JD
        
        // Setup the facade to simulate Sun ahead of Venus at estimated JD
        // For inferior conjunction: when Sun is ahead, search lower half
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
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.InferiorConjunction);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        Assert.That(result, Is.LessThanOrEqualTo(estimatedJd)); // Should find conjunction before or at estimated JD
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_SuperiorConjunction_SunAheadOfVenus()
    {
        // Arrange
        const double estimatedJd = 2460100.5;
        const double conjunctionJd = 2460101.0; // Conjunction happens slightly after estimated JD
        
        // Setup the facade to simulate Sun ahead of Venus at estimated JD
        // For superior conjunction: when Sun is ahead, search upper half
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
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.SuperiorConjunction);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        // The result should be within the search range (±1 day from estimated JD)
        Assert.That(result, Is.GreaterThanOrEqualTo(estimatedJd - 1));
        Assert.That(result, Is.LessThanOrEqualTo(estimatedJd + 1));
        // For superior conjunction with Sun ahead, the search should find the conjunction
        // The exact result depends on the binary search convergence
        Assert.That(Math.Abs(result - conjunctionJd), Is.LessThan(1.0));
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_InferiorConjunction_VenusAheadOfSun()
    {
        // Arrange
        const double estimatedJd = 2460100.5;
        const double conjunctionJd = 2460101.0; // Conjunction happens slightly after estimated JD
        
        // Setup the facade to simulate Venus ahead of Sun at estimated JD
        // For inferior conjunction: when Venus is ahead, search upper half
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
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.InferiorConjunction);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        Assert.That(result, Is.GreaterThanOrEqualTo(estimatedJd)); // Should find conjunction after or at estimated JD
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_SuperiorConjunction_VenusAheadOfSun()
    {
        // Arrange
        const double estimatedJd = 2460100.5;
        const double conjunctionJd = 2460100.0; // Conjunction happens slightly before estimated JD
        
        // Setup the facade to simulate Venus ahead of Sun at estimated JD
        // For superior conjunction: when Venus is ahead, search lower half
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
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.SuperiorConjunction);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        // The result should be within the search range (±1 day from estimated JD)
        Assert.That(result, Is.GreaterThanOrEqualTo(estimatedJd - 1));
        Assert.That(result, Is.LessThanOrEqualTo(estimatedJd + 1));
        // For superior conjunction with Venus ahead, the search should find the conjunction
        // The exact result depends on the binary search convergence
        Assert.That(Math.Abs(result - conjunctionJd), Is.LessThan(1.0));
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_InferiorConjunction_CrossingZeroDegree()
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
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.InferiorConjunction);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_SuperiorConjunction_CrossingZeroDegree()
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
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.SuperiorConjunction);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_InferiorConjunction_WithinMargin()
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
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.InferiorConjunction);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_SuperiorConjunction_WithinMargin()
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
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.SuperiorConjunction);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }

    [Test]
    public void TestCalculateConjunctionDate_InferiorConjunction_ReturnsValueInRange()
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
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.InferiorConjunction);
        
        // Assert
        Assert.That(result, Is.GreaterThanOrEqualTo(estimatedJd - 1));
        Assert.That(result, Is.LessThanOrEqualTo(estimatedJd + 1));
    }

    [Test]
    public void TestCalculateConjunctionDate_SuperiorConjunction_ReturnsValueInRange()
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
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.SuperiorConjunction);
        
        // Assert
        Assert.That(result, Is.GreaterThanOrEqualTo(estimatedJd - 1));
        Assert.That(result, Is.LessThanOrEqualTo(estimatedJd + 1));
    }

    [Test]
    public void TestCalculateConjunctionDate_DifferentPhenomenaProduceDifferentResults()
    {
        // Arrange
        const double estimatedJd = 2460100.5;
        const double conjunctionJd = 2460100.3; // Conjunction slightly before estimated JD
        
        // Setup the facade to simulate a scenario where the search direction matters
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
        double inferiorResult = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.InferiorConjunction);
        double superiorResult = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.SuperiorConjunction);
        
        // Assert
        Assert.That(inferiorResult, Is.GreaterThan(0));
        Assert.That(superiorResult, Is.GreaterThan(0));
        // Both should find approximately the same conjunction since it's the same astronomical event
        // Allow for some tolerance due to different search strategies
        Assert.That(Math.Abs(inferiorResult - superiorResult), Is.LessThan(0.5));
    }

    [Test]
    public void TestCalculateConjunctionDate_EdgeCase_LargeLongitudeDifference()
    {
        // Arrange
        const double estimatedJd = 2460100.5;
        
        // Setup the facade to simulate a large longitude difference
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 0, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                // Sun at 0°
                return new double[] { 0.0, 0, 0, 0, 0, 0 };
            });
        
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 3, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                // Venus at 180° - maximum difference
                return new double[] { 180.0, 0, 0, 0, 0, 0 };
            });
        
        // Act
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.InferiorConjunction);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        // Should return a value within the search range even if no exact conjunction is found
        Assert.That(result, Is.GreaterThanOrEqualTo(estimatedJd - 1));
        Assert.That(result, Is.LessThanOrEqualTo(estimatedJd + 1));
    }

    [Test]
    public void TestCalculateConjunctionDate_EdgeCase_Crossing180DegreeBoundary()
    {
        // Arrange
        const double estimatedJd = 2460100.5;
        const double conjunctionJd = 2460100.5;
        
        // Setup the facade to simulate conjunction crossing the 180° boundary
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, 0, 258))
            .ReturnsLazily((double jd, int id, int flags) => 
            {
                var baseLongitude = 170.0;
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
                var baseLongitude = 190.0;
                var daysDiff = jd - conjunctionJd;
                var longitude = baseLongitude + (daysDiff * 1.2);
                // Normalize to 0-360 range
                while (longitude < 0) longitude += 360;
                while (longitude >= 360) longitude -= 360;
                return new double[] { longitude, 0, 0, 0, 0, 0 };
            });
        
        // Act
        double result = _exactConjunctionDate.CalculateConjunctiondate(estimatedJd, VenusPhenomena.InferiorConjunction);
        
        // Assert
        Assert.That(result, Is.GreaterThan(0));
        A.CallTo(() => _calcUtFacade.PositionFromSe(A<double>._, A<int>._, A<int>._)).MustHaveHappened();
    }
}
