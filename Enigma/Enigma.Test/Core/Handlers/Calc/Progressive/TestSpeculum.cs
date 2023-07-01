// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.Progressive;

namespace Enigma.Test.Core.Handlers.Calc.Progressive;


[TestFixture]
public class TestSpeculumItemPlacidus
{
    private readonly double _delta = 0.00000001;
    private readonly double _geoLat = 52.0;
    private readonly double _raPlanet = 100.0;
    private readonly double _raPlanetWest = 310.0;
    private readonly double _declPlanet = 15.0;
    private readonly double _declPlanetWest = -10.0;
    private readonly double _raMc = 70.0;
    private readonly double _raIc = 250.0;
    private readonly double _oaAsc = 160.0;
    private readonly double _odDesc = 340.0;
    

    private SpeculumItemPlacidus _speculumItem;
    private SpeculumItemPlacidus _speculumItemWest;

    [SetUp]
    public void SetUp()
    {
        _speculumItem = new SpeculumItemPlacidus(_geoLat, _raMc, _raIc, _oaAsc, _odDesc, _raPlanet, _declPlanet);
        _speculumItemWest = new SpeculumItemPlacidus(_geoLat, _raMc, _raIc, _oaAsc, _odDesc, _raPlanetWest, _declPlanetWest);
    }

    [Test]
    public void TestMeridianDistanceMc()
    {
        double expected = 30.0;
        Assert.That(_speculumItem.MeridianDistanceMc, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestMeridianDistanceIc()
    {
        double expected = 210.0;
        Assert.That(_speculumItem.MeridianDistanceIc, Is.EqualTo(expected).Within(_delta));
    }


    [Test]
    public void TestMeridianDistancePromissorWest()
    {
        double expected = 240.0;
        Assert.That(_speculumItemWest.MeridianDistanceMc, Is.EqualTo(expected).Within(_delta));
    }


    [Test]
    public void TestAscensionalDifference()
    {
        double expected = 20.0572751586;    // asin(tan decl * tan geoLat) 
        Assert.That(_speculumItem.AscensionalDifference, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestAscensionalDifferencePromissorWest()
    {
        double expected = -13.0433526457;    // asin(tan decl * tan geoLat) 
        Assert.That(_speculumItemWest.AscensionalDifference, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestOaPromissorPlanetEast()
    {
        double expected = 79.9427248414;      //  ra - ad 
        Assert.That(_speculumItem.ObliqueAscensionPromissor, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestOaPromissorPlanetWest()
    {
        double expected = 296.956647354;              // ra + ad    310.0 +  -13.0433526457
        Assert.That(_speculumItemWest.ObliqueAscensionPromissor, Is.EqualTo(expected).Within(_delta));
    }

 /*
    [Test]
    public void TestOaAscendant()
    {
      double expected = 160.0;        // ramc + 90
      Assert.That((_speculumItem.ObliqueAscensionAscendant, Is.EqualTo(expected).Within(_delta));
    }
 */
    [Test]
    public void TestHorizontalDistance()
    {
        double expected = -80.0572751586;  // oa planet - oa asc = 79.9427248414 - 160.0
        Assert.That(_speculumItem.HorizontalDistance, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestHorizontalDistancePlanetWest()
    {
        double expected = -223.043352646;      // od planet - od descendant   =  (296.956647354 - 180.0)  - ( 160.0 + 180.0)
        Assert.That(_speculumItemWest.HorizontalDistance, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestNocturnalSemiArc()
    {
        double expected = 290.0572751586;    // abs(horizontal distance) + abs(meridian distance) = 80.0572751586 + 210.0
        Assert.That(_speculumItem.NocturnalSemiArc, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestNocturnalSemiArcPlanetWest()
    {
        double expected = 103.043352646;     // abs(horizontal distance) + abs(meridian distance) =  223.043352646 + 240.0
        Assert.That(_speculumItemWest.DiurnalSemiArc, Is.EqualTo(expected).Within(_delta));
    }

    [Test]
    public void TestProportionalSemiArc()    // planet below horizon
    {
        double expected = 0.72399494164;     // mdic / nsa = 210.0 / 290.0572751586    planet below horizon
        Assert.That(_speculumItem.ProportionalSemiArc, Is.EqualTo(expected).Within(_delta));
    }


}