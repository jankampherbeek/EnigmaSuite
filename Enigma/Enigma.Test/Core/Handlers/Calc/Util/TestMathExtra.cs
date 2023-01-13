// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.Util;

namespace Enigma.Test.Core.Handlers.Calc.Util;


[TestFixture]
internal class TestMathExtra
{
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestRectangular2PolarHappyFlow()
    {
        var rectangularValues = new double[] { 1.0, 2.0, 3.0 };
        var expectedValues = new double[] { 1.107148717794, 0.930274014115, 3.741657386774 };

        double[] result = MathExtra.Rectangular2Polar(rectangularValues);
        Assert.Multiple(() =>
        {
            Assert.That(result[0], Is.EqualTo(expectedValues[0]).Within(_delta));
            Assert.That(result[1], Is.EqualTo(expectedValues[1]).Within(_delta));
            Assert.That(result[2], Is.EqualTo(expectedValues[2]).Within(_delta));
        });
    }

    [Test]
    public void TestRectangular2PolarWrongLengthOfArray()
    {
        var rectangularValues = new double[] { 1.0, 2.0 };
        var _ = Assert.Throws<ArgumentException>(() => MathExtra.Rectangular2Polar(rectangularValues));
    }

    [Test]
    public void TestRectangular2PolarNullArray()
    {
        double[]? rectangularValues = null;
        var _ = Assert.Throws<ArgumentException>(() => MathExtra.Rectangular2Polar(rectangularValues!));
    }


    [Test]
    public void TestPolar2RectangularHappyFlow()
    {
        var polarValues = new double[] { 1.107148717794, 0.930274014115, 3.741657386774 };
        var expectedValues = new double[] { 1.0, 2.0, 3.0 };

        double[] result = MathExtra.Polar2Rectangular(polarValues);
        Assert.Multiple(() =>
        {
            Assert.That(result[0], Is.EqualTo(expectedValues[0]).Within(_delta));
            Assert.That(result[1], Is.EqualTo(expectedValues[1]).Within(_delta));
            Assert.That(result[2], Is.EqualTo(expectedValues[2]).Within(_delta));
        });
    }

    [Test]
    public void TestDegrees2Radians()
    {
        double degrees = 100.0;
        double expectedRadians = 1.74532925199;
        Assert.That(MathExtra.DegToRad(degrees), Is.EqualTo(expectedRadians).Within(_delta));
    }

    [Test]
    public void TestRadians2Degrees()
    {
        double radians = 2.0;
        double expectedDegrees = 114.591559026;
        Assert.That(MathExtra.RadToDeg(radians), Is.EqualTo(expectedDegrees).Within(_delta));
    }

}

