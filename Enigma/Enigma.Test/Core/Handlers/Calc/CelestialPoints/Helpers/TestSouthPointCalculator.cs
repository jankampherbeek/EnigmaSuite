// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.CelestialPoints.Helpers;
using Enigma.Core.Interfaces;
using Enigma.Domain.Calc.ChartItems;

namespace Enigma.Test.Core.Handlers.Calc.CelestialPoints.Helpers;



[TestFixture]
public class TestSouthPointCalculator
{

    // TODO 0.2 check differences


    private const double DELTA = 0.001; // TODO 0.2 enlarge value for _delta.

    [Test]
    public void TestHappyFlow()
    {
        const double armc = 331.883333333333;
        const double obliquity = 23.449614320676233;  // mean obliquity
        const double geoLat = 48.8333333333333;
        ISouthPointCalculator calculator = new SouthPointCalculator();
        const double expectedLong = 318.50043580207006;
        const double expectedLat = -27.562090280566338;
        EclipticCoordinates result = calculator.CalculateSouthPoint(armc, obliquity, geoLat);
        Assert.Multiple(() =>
        {
            Assert.That(result.Longitude, Is.EqualTo(expectedLong).Within(DELTA));
            Assert.That(result.Latitude, Is.EqualTo(expectedLat).Within(DELTA));
        });
    }

    [Test]
    public void TestSouthernHemisphere()
    {
        const double armc = 331.883333333333;
        const double obliquity = 23.449614320676233;  // mean obliquity
        const double geoLat = -48.8333333333333;
        ISouthPointCalculator calculator = new SouthPointCalculator();
        const double expectedLong = 174.53494810489755;
        const double expectedLat = -48.16467239725159;
        EclipticCoordinates result = calculator.CalculateSouthPoint(armc, obliquity, geoLat);
        Assert.Multiple(() =>
        {
            Assert.That(result.Longitude, Is.EqualTo(expectedLong).Within(DELTA));
            Assert.That(result.Latitude, Is.EqualTo(expectedLat).Within(DELTA));
        });
    }
}