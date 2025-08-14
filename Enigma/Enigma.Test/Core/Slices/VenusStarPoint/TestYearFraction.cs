// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.VenusStarPoint;

namespace Enigma.Test.Core.Slices.VenusStarPoint;

[TestFixture]
public class TestYearFraction
{
    private const double DELTA = 0.00000001;

    [Test]
    public void TestCalcYearAndFraction_BeginningOfYear()
    {
        // Arrange
        const int year = 2025;
        const double jd = 2460100.5; // January 1, 2025
        const double jdNewYear = 2460100.5; // January 1, 2025
        
        // Act
        double result = YearFraction.CalcYearAndFraction(year, jd, jdNewYear);
        
        // Assert
        Assert.That(result, Is.EqualTo(2025.0).Within(DELTA));
    }

    [Test]
    public void TestCalcYearAndFraction_MiddleOfYear()
    {
        // Arrange
        const int year = 2025;
        const double jd = 2460265.5; // July 1, 2025
        const double jdNewYear = 2460100.5; // January 1, 2025
        
        // Act
        double result = YearFraction.CalcYearAndFraction(year, jd, jdNewYear);
        
        // Assert
        // Expected: 2025 + (165 / 365.25) = 2025 + 0.451745 = 2025.451745
        Assert.That(result, Is.EqualTo(2025.451745).Within(0.001));
    }

    [Test]
    public void TestCalcYearAndFraction_EndOfYear()
    {
        // Arrange
        const int year = 2025;
        const double jd = 2460465.5; // December 31, 2025 (approximately 365 days into the year)
        const double jdNewYear = 2460100.5; // January 1, 2025
        
        // Act
        double result = YearFraction.CalcYearAndFraction(year, jd, jdNewYear);
        
        // Assert
        // Expected: 2025 + (365 / 365.25) = 2025 + 0.999315 = 2025.999315
        Assert.That(result, Is.EqualTo(2025.999315).Within(0.001));
    }

    [Test]
    public void TestCalcYearAndFraction_LeapYear()
    {
        // Arrange
        const int year = 2024; // Leap year
        const double jd = 2460366.5; // February 29, 2024
        const double jdNewYear = 2460100.5; // January 1, 2024
        
        // Act
        double result = YearFraction.CalcYearAndFraction(year, jd, jdNewYear);
        
        // Assert
        // Expected: 2024 + (266 / 365.25) = 2024 + 0.728268 = 2024.728268
        Assert.That(result, Is.EqualTo(2024.728268).Within(0.001));
    }

    [Test]
    public void TestCalcYearAndFraction_ExactHalfYear()
    {
        // Arrange
        const int year = 2025;
        const double jd = 2460282.625; // July 2, 2025 (approximately 182.625 days into the year)
        const double jdNewYear = 2460100.5; // January 1, 2025
        
        // Act
        double result = YearFraction.CalcYearAndFraction(year, jd, jdNewYear);
        
        // Assert
        // Expected: 2025 + (182.125 / 365.25) = 2025 + 0.498630 = 2025.498630
        Assert.That(result, Is.EqualTo(2025.498630).Within(0.001));
    }

    [Test]
    public void TestCalcYearAndFraction_OneQuarterYear()
    {
        // Arrange
        const int year = 2025;
        const double jd = 2460191.125; // April 1, 2025 (approximately 91.25 days into the year)
        const double jdNewYear = 2460100.5; // January 1, 2025
        
        // Act
        double result = YearFraction.CalcYearAndFraction(year, jd, jdNewYear);
        
        // Assert
        // Expected: 2025 + (90.625 / 365.25) = 2025 + 0.248117 = 2025.248117
        Assert.That(result, Is.EqualTo(2025.248117).Within(0.001));
    }

    [Test]
    public void TestCalcYearAndFraction_ThreeQuarterYear()
    {
        // Arrange
        const int year = 2025;
        const double jd = 2460373.625; // October 1, 2025 (approximately 273.125 days into the year)
        const double jdNewYear = 2460100.5; // January 1, 2025
        
        // Act
        double result = YearFraction.CalcYearAndFraction(year, jd, jdNewYear);
        
        // Assert
        // Expected: 2025 + (273.125 / 365.25) = 2025 + 0.747776 = 2025.747776
        Assert.That(result, Is.EqualTo(2025.747776).Within(0.001));
    }

    [Test]
    public void TestCalcYearAndFraction_HistoricalYear()
    {
        // Arrange
        const int year = 1900;
        const double jd = 2415020.5; // January 1, 1900
        const double jdNewYear = 2415020.5; // January 1, 1900
        
        // Act
        double result = YearFraction.CalcYearAndFraction(year, jd, jdNewYear);
        
        // Assert
        Assert.That(result, Is.EqualTo(1900.0).Within(DELTA));
    }

    [Test]
    public void TestCalcYearAndFraction_FutureYear()
    {
        // Arrange
        const int year = 2100;
        const double jd = 2488069.5; // January 1, 2100
        const double jdNewYear = 2488069.5; // January 1, 2100
        
        // Act
        double result = YearFraction.CalcYearAndFraction(year, jd, jdNewYear);
        
        // Assert
        Assert.That(result, Is.EqualTo(2100.0).Within(DELTA));
    }

    [Test]
    public void TestCalcYearAndFraction_ZeroYear()
    {
        // Arrange
        const int year = 0;
        const double jd = 1721057.5; // January 1, 0 (astronomical year)
        const double jdNewYear = 1721057.5; // January 1, 0
        
        // Act
        double result = YearFraction.CalcYearAndFraction(year, jd, jdNewYear);
        
        // Assert
        Assert.That(result, Is.EqualTo(0.0).Within(DELTA));
    }

    [Test]
    public void TestCalcYearAndFraction_NegativeYear()
    {
        // Arrange
        const int year = -1000;
        const double jd = 1355671.5; // January 1, -1000
        const double jdNewYear = 1355671.5; // January 1, -1000
        
        // Act
        double result = YearFraction.CalcYearAndFraction(year, jd, jdNewYear);
        
        // Assert
        Assert.That(result, Is.EqualTo(-1000.0).Within(DELTA));
    }

    [Test]
    public void TestCalcYearAndFraction_EdgeCaseOneDay()
    {
        // Arrange
        const int year = 2025;
        const double jd = 2460101.5; // January 2, 2025 (1 day into the year)
        const double jdNewYear = 2460100.5; // January 1, 2025
        
        // Act
        double result = YearFraction.CalcYearAndFraction(year, jd, jdNewYear);
        
        // Assert
        // Expected: 2025 + (1 / 365.25) = 2025 + 0.002738 = 2025.002738
        Assert.That(result, Is.EqualTo(2025.002738).Within(0.000001));
    }

    [Test]
    public void TestCalcYearAndFraction_EdgeCaseLastDay()
    {
        // Arrange
        const int year = 2025;
        const double jd = 2460464.5; // December 30, 2025 (364 days into the year)
        const double jdNewYear = 2460100.5; // January 1, 2025
        
        // Act
        double result = YearFraction.CalcYearAndFraction(year, jd, jdNewYear);
        
        // Assert
        // Expected: 2025 + (364 / 365.25) = 2025 + 0.996578 = 2025.996578
        Assert.That(result, Is.EqualTo(2025.996578).Within(0.000001));
    }

    [Test]
    public void TestCalcYearAndFraction_ConsistentWithYearLength()
    {
        // Arrange
        const int year = 2025;
        const double jdNewYear = 2460100.5; // January 1, 2025
        
        // Test that using exactly 365.25 days gives us year + 1.0
        const double jdEndOfYear = jdNewYear + 365.25;
        
        // Act
        double result = YearFraction.CalcYearAndFraction(year, jdEndOfYear, jdNewYear);
        
        // Assert
        Assert.That(result, Is.EqualTo(2026.0).Within(DELTA));
    }

    [Test]
    public void TestCalcYearAndFraction_MathematicalProperties()
    {
        // Arrange
        const int year = 2025;
        const double jdNewYear = 2460100.5; // January 1, 2025
        
        // Test that the function is linear with respect to the day difference
        const double jd1 = jdNewYear + 100.0; // 100 days into the year
        const double jd2 = jdNewYear + 200.0; // 200 days into the year
        
        // Act
        double result1 = YearFraction.CalcYearAndFraction(year, jd1, jdNewYear);
        double result2 = YearFraction.CalcYearAndFraction(year, jd2, jdNewYear);
        
        // Assert
        // The difference should be exactly 100/365.25 = 0.273785
        Assert.That(result2 - result1, Is.EqualTo(0.273785).Within(0.000001));
    }
}
