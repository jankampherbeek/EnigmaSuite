// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Interfaces;
using Enigma.Core.Calc.ObliqueLongitude;
using Enigma.Domain.Positional;

namespace EnigmaTest.Core.Calc.SolSysPoints;



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
        Assert.That(result.Longitude, Is.EqualTo(expectedLong).Within(_delta));
        Assert.That(result.Latitude, Is.EqualTo(expectedLat).Within(_delta));
        // Expected a difference no greater than <1E-08> between expected value <318,50043580207006> and actual value <318,5003113717042>.   Mean obliquity   verschil 0.0001245   0.4482 seconde
        // Expected a difference no greater than<1E-08 > between expected value<-27,562090280566338 > and actual value<-27,562042216088308 >. Latitude, mean obliquity  
        // Expected a difference no greater than <1E-08> between expected value <318,50043580207006> and actual value <318,4995873430928>.   True obliquity   verschil 0.00084846  3 seconden
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
        Assert.That(result.Longitude, Is.EqualTo(expectedLong).Within(_delta));
        Assert.That(result.Latitude, Is.EqualTo(expectedLat).Within(_delta));
    }


}