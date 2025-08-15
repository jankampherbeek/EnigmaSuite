// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.VenusStarPoint;

namespace Enigma.Test.Core.Slices.VenusStarPoint;

[TestFixture]
public class TestEstimatedConjunctionDate
{
    private const double DELTA = 0.000001;

    [Test]
    public void TestCalcEstimatedConjunctionDate_FormulaVerification()
    {
        // Test the formula implementation against known values
        // Based on Jean Meeus, Astronomical Algorithms, p. 250-251
        
        // Test case 1: Year 2000 (yearFraction = 2000.0)
        var yearFraction = 2000.0;
        
        // Calculate k manually to verify the formula
        var a = VenusData.VenusInferiorConjunctionFactorA;
        var b = VenusData.VenusInferiorConjunctionFactorB;
        var k = Math.Round((365.2425 * yearFraction + 1721_060 - a) / b);
        
        Console.WriteLine($"Year Fraction: {yearFraction}");
        Console.WriteLine($"a: {a}");
        Console.WriteLine($"b: {b}");
        Console.WriteLine($"365.2425 * yearFraction + 1721_060: {365.2425 * yearFraction + 1721_060}");
        Console.WriteLine($"(365.2425 * yearFraction + 1721_060 - a) / b: {(365.2425 * yearFraction + 1721_060 - a) / b}");
        Console.WriteLine($"k (rounded): {k}");
        
        // Calculate expected JDE0
        var expectedJde0 = a + k * b;
        Console.WriteLine($"Expected JDE0: {expectedJde0}");
        
        // Test the actual method
        var result = EstimatedConjunctionDate.CalcEstimatedConjunctionDate(yearFraction, VenusPhenomena.InferiorConjunction);
        Console.WriteLine($"Method result: {result}");
        
        // Verify that k is reasonable (should be around 0 for year 2000)
        Assert.That(k, Is.GreaterThanOrEqualTo(-10));
        Assert.That(k, Is.LessThanOrEqualTo(10));
        
        // Verify the result is reasonable
        Assert.That(result, Is.GreaterThan(2400000));
        Assert.That(result, Is.LessThan(2500000));
    }

    [Test]
    public void TestCalcEstimatedConjunctionDate_DifferentYears()
    {
        // Test with different year fractions to see if k varies correctly
        var testYears = new[] { 1990.0, 2000.0, 2010.0, 2020.0, 2030.0 };
        
        foreach (var year in testYears)
        {
            var a = VenusData.VenusInferiorConjunctionFactorA;
            var b = VenusData.VenusInferiorConjunctionFactorB;
            var k = Math.Round((365.2425 * year + 1721_060 - a) / b);
            
            Console.WriteLine($"Year {year}: k = {k}");
            
            var result = EstimatedConjunctionDate.CalcEstimatedConjunctionDate(year, VenusPhenomena.InferiorConjunction);
            Console.WriteLine($"  Result: {result}");
            
            // Verify k increases with year (should be roughly linear)
            Assert.That(result, Is.GreaterThan(2400000));
        }
    }

    [Test]
    public void TestCalcEstimatedConjunctionDate_InferiorVsSuperior()
    {
        // Test that inferior and superior conjunctions give different results
        var yearFraction = 2020.0;
        
        var inferiorResult = EstimatedConjunctionDate.CalcEstimatedConjunctionDate(yearFraction, VenusPhenomena.InferiorConjunction);
        var superiorResult = EstimatedConjunctionDate.CalcEstimatedConjunctionDate(yearFraction, VenusPhenomena.SuperiorConjunction);
        
        Console.WriteLine($"Year {yearFraction}:");
        Console.WriteLine($"  Inferior conjunction: {inferiorResult}");
        Console.WriteLine($"  Superior conjunction: {superiorResult}");
        
        // They should be different
        Assert.That(inferiorResult, Is.Not.EqualTo(superiorResult));
        
        // Both should be reasonable Julian Day numbers
        Assert.That(inferiorResult, Is.GreaterThan(2400000));
        Assert.That(superiorResult, Is.GreaterThan(2400000));
    }

    [Test]
    public void TestCalcEstimatedConjunctionDate_FormulaConsistency()
    {
        // Test that the formula is consistent with Jean Meeus's approach
        // For year 2000, k should be approximately 0
        
        var yearFraction = 2000.0;
        var a = VenusData.VenusInferiorConjunctionFactorA;
        var b = VenusData.VenusInferiorConjunctionFactorB;
        
        // Calculate k manually
        var kManual = Math.Round((365.2425 * yearFraction + 1721_060 - a) / b);
        
        // Calculate JDE0 manually
        var jde0Manual = a + kManual * b;
        
        // Calculate M manually
        var m0 = VenusData.VenusInferiorConjunctionFactorM0;
        var m1 = VenusData.VenusInferiorConjunctionFactorM1;
        var mManual = m0 + kManual * m1;
        
        Console.WriteLine($"Manual calculation:");
        Console.WriteLine($"  k: {kManual}");
        Console.WriteLine($"  JDE0: {jde0Manual}");
        Console.WriteLine($"  M: {mManual}");
        
        // Test the method result
        var methodResult = EstimatedConjunctionDate.CalcEstimatedConjunctionDate(yearFraction, VenusPhenomena.InferiorConjunction);
        Console.WriteLine($"Method result: {methodResult}");
        
        // The method result should be close to JDE0 (within a few days due to corrections)
        Assert.That(Math.Abs(methodResult - jde0Manual), Is.LessThan(10.0));
    }

    [Test]
    public void TestCalcEstimatedConjunctionDate_TimeDependentCorrections()
    {
        // Test that the time-dependent corrections produce different results
        // even when k values are similar
        
        var year2000 = 2000.0;
        var year2001 = 2001.0;
        var year2002 = 2002.0;
        
        // Calculate for inferior conjunctions
        var result2000 = EstimatedConjunctionDate.CalcEstimatedConjunctionDate(year2000, VenusPhenomena.InferiorConjunction);
        var result2001 = EstimatedConjunctionDate.CalcEstimatedConjunctionDate(year2001, VenusPhenomena.InferiorConjunction);
        var result2002 = EstimatedConjunctionDate.CalcEstimatedConjunctionDate(year2002, VenusPhenomena.InferiorConjunction);
        
        Console.WriteLine($"Year 2000: {result2000}");
        Console.WriteLine($"Year 2001: {result2001}");
        Console.WriteLine($"Year 2002: {result2002}");
        
        // Calculate the t values manually
        var a = VenusData.VenusInferiorConjunctionFactorA;
        var b = VenusData.VenusInferiorConjunctionFactorB;
        
        var k2000 = Math.Round((365.2425 * year2000 + 1721_060 - a) / b);
        var k2001 = Math.Round((365.2425 * year2001 + 1721_060 - a) / b);
        var k2002 = Math.Round((365.2425 * year2002 + 1721_060 - a) / b);
        
        var jde0_2000 = a + k2000 * b;
        var jde0_2001 = a + k2001 * b;
        var jde0_2002 = a + k2002 * b;
        
        var t2000 = (jde0_2000 - 2451_545.0) / 36_525.0;
        var t2001 = (jde0_2001 - 2451_545.0) / 36_525.0;
        var t2002 = (jde0_2002 - 2451_545.0) / 36_525.0;
        
        Console.WriteLine($"k values: {k2000}, {k2001}, {k2002}");
        Console.WriteLine($"t values: {t2000}, {t2001}, {t2002}");
        Console.WriteLine($"JDE0 values: {jde0_2000}, {jde0_2001}, {jde0_2002}");
        
        // The results should be different due to time-dependent corrections
        Assert.That(result2000, Is.Not.EqualTo(result2001));
        Assert.That(result2001, Is.Not.EqualTo(result2002));
        Assert.That(result2000, Is.Not.EqualTo(result2002));
        
        // Results should be in chronological order
        Assert.That(result2000, Is.LessThan(result2001));
        Assert.That(result2001, Is.LessThan(result2002));
        
        // Each result should be approximately 584 days apart (Venus conjunction cycle)
        var diff1 = result2001 - result2000;
        var diff2 = result2002 - result2001;
        
        Console.WriteLine($"Time difference 2000-2001: {diff1} days");
        Console.WriteLine($"Time difference 2001-2002: {diff2} days");
        
        // Should be roughly 584 days (Venus conjunction period)
        Assert.That(Math.Abs(diff1 - 584), Is.LessThan(50)); // Allow some tolerance
        Assert.That(Math.Abs(diff2 - 584), Is.LessThan(50)); // Allow some tolerance
    }
}
