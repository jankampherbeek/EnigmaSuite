// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.Util;

namespace Enigma.Test.Core.Handlers.Calc.Util;


[TestFixture]
internal class TestMathExtra
{
    private const double Delta = 0.00000001;

    [Test]
    public void TestRectangular2PolarHappyFlow()
    {
        var rectangularValues = new[] { 1.0, 2.0, 3.0 };
        var expectedValues = new[] { 1.107148717794, 0.930274014115, 3.741657386774 };

        double[] result = MathExtra.Rectangular2Polar(rectangularValues);
        Assert.Multiple(() =>
        {
            Assert.That(result[0], Is.EqualTo(expectedValues[0]).Within(Delta));
            Assert.That(result[1], Is.EqualTo(expectedValues[1]).Within(Delta));
            Assert.That(result[2], Is.EqualTo(expectedValues[2]).Within(Delta));
        });
    }

    [Test]
    public void TestRectangular2PolarWrongLengthOfArray()
    {
        var rectangularValues = new[] { 1.0, 2.0 };
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
        var polarValues = new[] { 1.107148717794, 0.930274014115, 3.741657386774 };
        var expectedValues = new[] { 1.0, 2.0, 3.0 };

        double[] result = MathExtra.Polar2Rectangular(polarValues);
        Assert.Multiple(() =>
        {
            Assert.That(result[0], Is.EqualTo(expectedValues[0]).Within(Delta));
            Assert.That(result[1], Is.EqualTo(expectedValues[1]).Within(Delta));
            Assert.That(result[2], Is.EqualTo(expectedValues[2]).Within(Delta));
        });
    }

    [Test]
    public void TestDegrees2Radians()
    {
        const double degrees = 100.0;
        const double expectedRadians = 1.74532925199;
        Assert.That(MathExtra.DegToRad(degrees), Is.EqualTo(expectedRadians).Within(Delta));
    }

    [Test]
    public void TestRadians2Degrees()
    {
        const double radians = 2.0;
        const double expectedDegrees = 114.591559026;
        Assert.That(MathExtra.RadToDeg(radians), Is.EqualTo(expectedDegrees).Within(Delta));
    }

    [Test]
    public void TestAscensionalDifference()
    {
        // Values based on example by Gansten in Primary Directions, p. 151.
        const double decl = 18.16666666667;
        const double geoLat = 58.2666666666667;
        const double expected = 32.04675727201;
        double actual = MathExtra.AscensionalDifference(geoLat, decl);
        Assert.That(actual, Is.EqualTo(expected).Within(Delta));
    }

    [Test]
    public void TestObliqueAscension()
    {
        // Values based on example by Gansten in Primary Directions, p. 151.
        const bool north = true;
        const bool east = true;
        const double raPoint = 130.83333333333;
        const double ascDiff = 32.04675727201;
        const double expected = 98.78657606132;
        double actual = MathExtra.ObliqueAscension(raPoint, ascDiff, east, north);
        Assert.That(actual, Is.EqualTo(expected).Within(Delta));
    }

    [Test]
    public void TestHorizontalDistance()
    {
        const bool easternHemiSphere = false;
        const double oaPoint = 288.0;
        const double oaAsc = 247.0;
        const double expected = 41.0;
        double actual = MathExtra.HorizontalDistance(oaPoint, oaAsc, easternHemiSphere);
        Assert.That(actual, Is.EqualTo(expected).Within(Delta));
    }

    [Test]
    public void TestIsEasternHemiSphereIsTrue()
    {
        const double raMc = 337.966666666667;
        const double raPoint = 130.83333333333;
        Assert.That(MathExtra.IsEasternHemiSphere(raPoint, raMc), Is.True);
    }

    [Test]
    public void TestIsEasternHemiSphereIsFalse()
    {
        const double raMc = 337.966666666667;
        const double raPoint = 230.83333333333;
        Assert.That(MathExtra.IsEasternHemiSphere(raPoint, raMc), Is.False);
    }

    [Test]
    public void TestRegiomontanianPole()
    {
        const double declFixPoint = 18.166666666667;
        const double upperMdFixPoint = 152.866666666667;
        const double geoLat = 58.2666666666667;
        const double expected = 51.78626254060;
        double actual = MathExtra.RegiomontanianPole(geoLat, declFixPoint, upperMdFixPoint);
        Assert.That(actual, Is.EqualTo(expected).Within(Delta));
    }

    [Test]
    public void TestBianchinianLatitude()
    {
        const double angle = 20.0;
        const double latitude = 3;
        const double expected = 2.33333333333;
        double actual = MathExtra.BianchinianLatitude(latitude, angle);
        Assert.That(actual, Is.EqualTo(expected).Within(Delta));
    }

    [Test]
    public void TestBianchinianLatitudeForSquare()
    {
        const double angle = 90.0;
        const double latitude = 3;
        const double expected = 0.0;
        double actual = MathExtra.BianchinianLatitude(latitude, angle);
        Assert.That(actual, Is.EqualTo(expected).Within(Delta));
    }

    [Test]
    public void TestBianchinianLatitudeForSextile()
    {
        const double angle = 60.0;
        const double latitude = 3;
        const double expected = 1.0;
        double actual = MathExtra.BianchinianLatitude(latitude, angle);
        Assert.That(actual, Is.EqualTo(expected).Within(Delta));
    }

    [Test]
    public void TestBianchinianLatitudeForTrine()
    {
        const double angle = 120.0;
        const double latitude = 3;
        const double expected = -1.0;
        double actual = MathExtra.BianchinianLatitude(latitude, angle);
        Assert.That(actual, Is.EqualTo(expected).Within(Delta));
    }

    [Test]
    public void TestBianchinianLatitudeForOpposition()
    {
        const double angle = 180.0;
        const double latitude = 3;
        const double expected = -3.0;
        double actual = MathExtra.BianchinianLatitude(latitude, angle);
        Assert.That(actual, Is.EqualTo(expected).Within(Delta));
    }

}

