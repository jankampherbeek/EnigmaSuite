﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.CelestialPoints.Helpers;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems.Coordinates;

namespace Enigma.Test.Core.Handlers.Calc.CelestialPoints.Helpers;



[TestFixture]
public class TestSouthPointCalculator
{

    // TODO check differences


    private readonly double _delta = 0.001;     // TODO enlarge value for _delta.

    [Test]
    public void TestHappyFlow()
    {
        double armc = 331.883333333333;
        double obliquity = 23.449614320676233;  // mean obliquity
        double geoLat = 48.8333333333333;
        ISouthPointCalculator calculator = new SouthPointCalculator();
        double expectedLong = 318.50043580207006;
        double expectedLat = -27.562090280566338;
        EclipticCoordinates result = calculator.CalculateSouthPoint(armc, obliquity, geoLat);
        Assert.Multiple(() =>
        {
            Assert.That(result.Longitude, Is.EqualTo(expectedLong).Within(_delta));
            Assert.That(result.Latitude, Is.EqualTo(expectedLat).Within(_delta));
        });
    }

    [Test]
    public void TestSouthernHemisphere()
    {
        double armc = 331.883333333333;
        double obliquity = 23.449614320676233;  // mean obliquity
        double geoLat = -48.8333333333333;
        ISouthPointCalculator calculator = new SouthPointCalculator();
        // double expectedLong = 318.50043580207006;
        double expectedLong = 174.53494810489755;
        // double expectedLat = -27.562090280566338;
        double expectedLat = -48.16467239725159;
        // TODO check values for southern latitude
        EclipticCoordinates result = calculator.CalculateSouthPoint(armc, obliquity, geoLat);
        Assert.Multiple(() =>
        {
            Assert.That(result.Longitude, Is.EqualTo(expectedLong).Within(_delta));
            Assert.That(result.Latitude, Is.EqualTo(expectedLat).Within(_delta));
        });
    }
}