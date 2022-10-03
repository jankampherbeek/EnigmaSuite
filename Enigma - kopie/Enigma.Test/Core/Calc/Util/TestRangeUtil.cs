// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Calc.Util;

namespace Enigma.Test.Core.Calc.Util;


[TestFixture]

public class TestRangeUtilNoChange
{
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestNoChange()
    {
        double testValue = 12.0;
        double expectedValue = 12.0;
        double lowerLimit = 0.0;
        double upperLimit = 360.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(_delta));
    }

    [Test]
    public void TestTooLow()
    {
        double testValue = -10.0;
        double expectedValue = 350.0;
        double lowerLimit = 0.0;
        double upperLimit = 360.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(_delta));
    }

    [Test]
    public void TestTooHigh()
    {
        double testValue = 410.0;
        double expectedValue = 50.0;
        double lowerLimit = 0.0;
        double upperLimit = 360.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(_delta));
    }

    [Test]
    public void TestOnLowerLimit()
    {
        double testValue = 0.0;
        double expectedValue = 0.0;
        double lowerLimit = 0.0;
        double upperLimit = 360.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(_delta));
    }

    [Test]
    public void TestOnUpperLimit()
    {
        double testValue = 360.0;
        double expectedValue = 0.0;
        double lowerLimit = 0.0;
        double upperLimit = 360.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(_delta));
    }

    [Test]
    public void TestPositiveNegativeRangeNoChange()
    {
        double testValue = -45.0;
        double expectedValue = -45.0;
        double lowerLimit = -90.0;
        double upperLimit = 90.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(_delta));
    }

    [Test]
    public void TestPositiveNegativeRangeTooLow()
    {
        double testValue = -100.0;
        double expectedValue = 80.0;
        double lowerLimit = -90.0;
        double upperLimit = 90.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(_delta));
    }

    [Test]
    public void TestPositiveNegativeRangeTooHigh()
    {
        double testValue = 100.0;
        double expectedValue = -80.0;
        double lowerLimit = -90.0;
        double upperLimit = 90.0;
        Assert.That(RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit), Is.EqualTo(expectedValue).Within(_delta));
    }


    [Test]
    public void TestWrongSequenceLowerUpper()
    {
        double testValue = 100.0;
        double lowerLimit = 360.0;
        double upperLimit = 0.0;
        Assert.Throws<ArgumentException>(() => RangeUtil.ValueToRange(testValue, lowerLimit, upperLimit));
    }




}