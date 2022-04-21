// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.calc.util;
using NUnit.Framework;

namespace E4CTest.calc.util;


[TestFixture]
internal class TestMathExtra
{
    private double _delta = 0.00000001;

    [Test]
    public void TestRectangular2PolarHappyFlow()
    {
        var rectangularValues = new double[] { 1.0, 2.0, 3.0 };
        var expectedValues = new double[] { 1.107148717794, 0.930274014115, 3.741657386774 };

        double[] result = MathExtra.Rectangular2Polar(rectangularValues);
        Assert.AreEqual(expectedValues[0], result[0], _delta);
        Assert.AreEqual(expectedValues[1], result[1], _delta);
        Assert.AreEqual(expectedValues[2], result[2], _delta);
    }

    [Test]
    public void TestPolar2RectangularHappyFlow()
    {
        var polarValues = new double[] { 1.107148717794, 0.930274014115, 3.741657386774 };
        var expectedValues = new double[] { 1.0, 2.0, 3.0 };

        double[] result = MathExtra.Polar2Rectangular(polarValues);
        Assert.AreEqual(expectedValues[0], result[0], _delta);
        Assert.AreEqual(expectedValues[1], result[1], _delta);
        Assert.AreEqual(expectedValues[2], result[2], _delta);
    }
}
