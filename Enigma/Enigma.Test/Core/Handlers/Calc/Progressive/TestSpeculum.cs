// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.Progressive;

namespace Enigma.Test.Core.Handlers.Calc.Progressive;


[TestFixture]
public class TestSpeculumItemPlacidus
{
    private const double DELTA = 0.00000001;
    private const double GEO_LAT = 52.0;
    private const double RA_PLANET = 100.0;
    private const double RA_PLANET_WEST = 310.0;
    private const double DECL_PLANET = 15.0;
    private const double DECL_PLANET_WEST = -10.0;
    private const double RA_MC = 70.0;
    private const double RA_IC = 250.0;
    private const double OA_ASC = 160.0;
    private const double OD_DESC = 340.0;
    

    private SpeculumItemPlacidus? _speculumItem;
    private SpeculumItemPlacidus? _speculumItemWest;

    [SetUp]
    public void SetUp()
    {
        _speculumItem = new SpeculumItemPlacidus(GEO_LAT, RA_MC, RA_IC, OA_ASC, OD_DESC, RA_PLANET, DECL_PLANET);
        _speculumItemWest = new SpeculumItemPlacidus(GEO_LAT, RA_MC, RA_IC, OA_ASC, OD_DESC, RA_PLANET_WEST, DECL_PLANET_WEST);
    }

    [Test]
    public void TestMeridianDistanceMc()
    {
        const double expected = 30.0;
        Assert.That(_speculumItem!.MeridianDistanceMc, Is.EqualTo(expected).Within(DELTA));
    }

    [Test]
    public void TestMeridianDistanceIc()
    {
        const double expected = 210.0;
        Assert.That(_speculumItem!.MeridianDistanceIc, Is.EqualTo(expected).Within(DELTA));
    }


    [Test]
    public void TestMeridianDistancePromissorWest()
    {
        const double expected = 240.0;
        Assert.That(_speculumItemWest!.MeridianDistanceMc, Is.EqualTo(expected).Within(DELTA));
    }


    [Test]
    public void TestAscensionalDifference()
    {
        const double expected = 20.0572751586;    // asin(tan decl * tan geoLat) 
        Assert.That(_speculumItem!.AscensionalDifference, Is.EqualTo(expected).Within(DELTA));
    }

    [Test]
    public void TestAscensionalDifferencePromissorWest()
    {
        const double expected = -13.0433526457;    // asin(tan decl * tan geoLat) 
        Assert.That(_speculumItemWest!.AscensionalDifference, Is.EqualTo(expected).Within(DELTA));
    }

    [Test]
    public void TestOaPromissorPlanetEast()
    {
        const double expected = 79.9427248414;      //  ra - ad 
        Assert.That(_speculumItem!.ObliqueAscensionPromissor, Is.EqualTo(expected).Within(DELTA));
    }

    [Test]
    public void TestOaPromissorPlanetWest()
    {
        const double expected = 296.956647354;              // ra + ad    310.0 +  -13.0433526457
        Assert.That(_speculumItemWest!.ObliqueAscensionPromissor, Is.EqualTo(expected).Within(DELTA));
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
        const double expected = -80.0572751586;  // oa planet - oa asc = 79.9427248414 - 160.0
        Assert.That(_speculumItem!.HorizontalDistance, Is.EqualTo(expected).Within(DELTA));
    }

    [Test]
    public void TestHorizontalDistancePlanetWest()
    {
        const double expected = -223.043352646;      // od planet - od descendant   =  (296.956647354 - 180.0)  - ( 160.0 + 180.0)
        Assert.That(_speculumItemWest!.HorizontalDistance, Is.EqualTo(expected).Within(DELTA));
    }

    [Test]
    public void TestNocturnalSemiArc()
    {
        const double expected = 290.0572751586;    // abs(horizontal distance) + abs(meridian distance) = 80.0572751586 + 210.0
        Assert.That(_speculumItem!.NocturnalSemiArc, Is.EqualTo(expected).Within(DELTA));
    }

    [Test]
    public void TestNocturnalSemiArcPlanetWest()
    {
        const double expected = 103.043352646;     // abs(horizontal distance) + abs(meridian distance) =  223.043352646 + 240.0
        Assert.That(_speculumItemWest!.DiurnalSemiArc, Is.EqualTo(expected).Within(DELTA));
    }

    [Test]
    public void TestProportionalSemiArc()    // planet below horizon
    {
        const double expected = 0.72399494164;     // mdic / nsa = 210.0 / 290.0572751586    planet below horizon
        Assert.That(_speculumItem!.ProportionalSemiArc, Is.EqualTo(expected).Within(DELTA));
    }


}