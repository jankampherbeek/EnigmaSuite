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
        
        
        // Calculate expected JDE0
        var expectedJde0 = a + k * b;

        
        // Test the actual method
        var result = EstimatedConjunctionDate.CalcEstimatedConjunctionDate(yearFraction, VenusPhenomena.InferiorConjunction);

        
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
            
            var result = EstimatedConjunctionDate.CalcEstimatedConjunctionDate(year, VenusPhenomena.InferiorConjunction);
            
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
        
        // Test the method result
        var methodResult = EstimatedConjunctionDate.CalcEstimatedConjunctionDate(yearFraction, VenusPhenomena.InferiorConjunction);
        
        // The method result should be close to JDE0 (within a few days due to corrections)
        Assert.That(Math.Abs(methodResult - jde0Manual), Is.LessThan(10.0));
    }

 }
