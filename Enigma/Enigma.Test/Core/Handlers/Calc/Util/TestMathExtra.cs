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

    [Test]
    public void TestAscensionalDifference()
    {
        // Values based on example by Gansten in Primary Directions, p. 151.
        double decl = 18.16666666667;
        double geoLat = 58.2666666666667;
        double expected = 32.04675727201;
        double actual = MathExtra.AscensionalDifference(geoLat, decl);
        Assert.That(actual, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestObliqueAscension()
    {
        // Values based on example by Gansten in Primary Directions, p. 151.
        bool north = true;
        bool east = true;
        double raPoint = 130.83333333333;
        double ascDiff = 32.04675727201;
        double expected = 98.78657606132;
        double actual = MathExtra.ObliqueAscension(raPoint, ascDiff, east, north);
        Assert.That(actual, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestHorizontalDistance()
    {
        bool easternHemiSphere = false;
        double oaPoint = 288.0;
        double oaAsc = 247.0;
        double expected = 41.0;
        double actual = MathExtra.HorizontalDistance(oaPoint, oaAsc, easternHemiSphere);
        Assert.That(actual, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestIsEasternHemiSphereIsTrue()
    {
        double raMc = 337.966666666667;
        double raPoint = 130.83333333333;
        Assert.That(MathExtra.IsEasternHemiSphere(raPoint, raMc), Is.True);
    }

    [Test]
    public void TestIsEasternHemiSphereIsFalse()
    {
        double raMc = 337.966666666667;
        double raPoint = 230.83333333333;
        Assert.That(MathExtra.IsEasternHemiSphere(raPoint, raMc), Is.False);
    }

    [Test]
    public void TestRegiomontanianPole()
    {
        double declFixPoint = 18.166666666667;
        double upperMdFixPoint = 152.866666666667;
        double geoLat = 58.2666666666667;
        double expected = 51.78626254060;
        double actual = MathExtra.RegiomontanianPole(geoLat, declFixPoint, upperMdFixPoint);
        Assert.That(actual, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestBianchinianLatitude()
    {
        double angle = 20.0;
        double latitude = 3;
        double expected = 2.33333333333;
        double actual = MathExtra.BianchinianLatitude(latitude, angle);
        Assert.That(actual, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestBianchinianLatitudeForSquare()
    {
        double angle = 90.0;
        double latitude = 3;
        double expected = 0.0;
        double actual = MathExtra.BianchinianLatitude(latitude, angle);
        Assert.That(actual, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestBianchinianLatitudeForSextile()
    {
        double angle = 60.0;
        double latitude = 3;
        double expected = 1.0;
        double actual = MathExtra.BianchinianLatitude(latitude, angle);
        Assert.That(actual, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestBianchinianLatitudeForTrine()
    {
        double angle = 120.0;
        double latitude = 3;
        double expected = -1.0;
        double actual = MathExtra.BianchinianLatitude(latitude, angle);
        Assert.That(actual, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestBianchinianLatitudeForOpposition()
    {
        double angle = 180.0;
        double latitude = 3;
        double expected = -3.0;
        double actual = MathExtra.BianchinianLatitude(latitude, angle);
        Assert.That(actual, Is.EqualTo(expected).Within(_delta));
    }

}

