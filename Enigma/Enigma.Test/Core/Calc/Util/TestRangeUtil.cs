// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Util;

namespace Enigma.Test.Core.Calc.Util;


[TestFixture]

public class TestRangeUtilNoChange
{
    private const double DELTA = 0.00000001;

    [Test]
    public void TestNoChange()
    {
        const double testValue = 12.0;
        const double expectedValue = 12.0;
        const double lowerLimit = 0.0;
        const double upperLimit = 360.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(DELTA));
    }

    [Test]
    public void TestTooLow()
    {
        const double testValue = -10.0;
        const double expectedValue = 350.0;
        const double lowerLimit = 0.0;
        const double upperLimit = 360.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(DELTA));
    }

    [Test]
    public void TestTooHigh()
    {
        const double testValue = 410.0;
        const double expectedValue = 50.0;
        const double lowerLimit = 0.0;
        const double upperLimit = 360.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(DELTA));
    }

    [Test]
    public void TestOnLowerLimit()
    {
        const double testValue = 0.0;
        const double expectedValue = 0.0;
        const double lowerLimit = 0.0;
        const double upperLimit = 360.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(DELTA));
    }

    [Test]
    public void TestOnUpperLimit()
    {
        const double testValue = 360.0;
        const double expectedValue = 0.0;
        const double lowerLimit = 0.0;
        const double upperLimit = 360.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(DELTA));
    }

    [Test]
    public void TestPositiveNegativeRangeNoChange()
    {
        const double testValue = -45.0;
        const double expectedValue = -45.0;
        const double lowerLimit = -90.0;
        const double upperLimit = 90.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(DELTA));
    }

    [Test]
    public void TestPositiveNegativeRangeTooLow()
    {
        const double testValue = -100.0;
        const double expectedValue = 80.0;
        const double lowerLimit = -90.0;
        const double upperLimit = 90.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(DELTA));
    }

    [Test]
    public void TestPositiveNegativeRangeTooHigh()
    {
        const double testValue = 100.0;
        const double expectedValue = -80.0;
        const double lowerLimit = -90.0;
        const double upperLimit = 90.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(DELTA));
    }


    [Test]
    public void TestWrongSequenceLowerUpper()
    {
        const double testValue = 100.0;
        const double lowerLimit = 360.0;
        const double upperLimit = 0.0;
        Assert.Throws<ArgumentException>(() => RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit));
    }

}