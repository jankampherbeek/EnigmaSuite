// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Charts.Prog.PrimDir;

namespace Enigma.Test.Core.Charts.PrimDir;

[TestFixture]
public class TestPrimDirCalcAssist
{
// TODO TestPrimdirCalcAssist: add tests for southern hemisphere
    
    
    private const double DELTA = 1E-7;
    
    
    [Test]
    public void TestIsChartLeftScenario1()
    {
        const double raMc = 337.966666666667;
        const double raPoint = 130.83333333333;
        Assert.That(PrimDirCalcAssist.IsChartLeft(raPoint, raMc), Is.True);
    }
    
    [Test]
    public void TestIsChartLeftScenario2()
    {
        const double raMc = 337.966666666667;
        const double raPoint = 230.83333333333;
        Assert.That(PrimDirCalcAssist.IsChartLeft(raPoint, raMc), Is.False);
    }
    
    [Test]
    public void TestIsChartLeftScenario3()
    {
        const double raMc = 337.966666666667;
        const double raPoint = 30.83333333333;
        Assert.That(PrimDirCalcAssist.IsChartLeft(raPoint, raMc), Is.True);
    }
    
    [Test]
    public void TestIsChartLeftScenario4()
    {
        const double raMc = 10.0;
        const double raPoint = 350.0;
        Assert.That(PrimDirCalcAssist.IsChartLeft(raPoint, raMc), Is.False);
    }
    
    [Test]
    public void TestHorizontalDistance()
    {
        const bool easternHemiSphere = false;
        const double oaPoint = 288.0;
        const double oaAsc = 247.0;
        const double expected = 41.0;
        double actual = PrimDirCalcAssist.HorizontalDistance(oaPoint, oaAsc, easternHemiSphere);
        Assert.That(actual, Is.EqualTo(expected).Within(DELTA));
    }
    
        
    [Test]
    public void TestAscensionalDifference()
    {
        // Values based on example by Gansten in Primary Directions, p. 151.
        const double decl = 18.16666666667;
        const double geoLat = 58.2666666666667;
        const double expected = 32.04675727201;
        double actual = PrimDirCalcAssist.AscensionalDifference(decl, geoLat);
        Assert.That(actual, Is.EqualTo(expected).Within(DELTA));
    }
    
    [Test]
    public void TestAscensionalDifferenceDeclSouth()
    {
        const double decl = -12.0;
        const double geoLat = 50.0;
        const double expected = -14.6737666261;
        double actual = PrimDirCalcAssist.AscensionalDifference(decl, geoLat);
        Assert.That(actual, Is.EqualTo(expected).Within(DELTA));
    }
    
    [Test]
    public void TestAscensionalDifferenceGeoLatSouth()
    {
        const double decl = 12.0;
        const double geoLat = -50.0;
        const double expected = -14.6737666261;
        double actual = PrimDirCalcAssist.AscensionalDifference( decl, geoLat);
        Assert.That(actual, Is.EqualTo(expected).Within(DELTA));
    }

    [Test]
    public void TestPoleRegiomontanus()
    {
        // values from Gansten, Prim dir, p. 156
        const double decl = 18.1666666666667;
        const double mdUpper = 152.8666666666667;
        const double geoLat = 58.2666666666667;
        const double expected = 51.7833333333333;
        double actual = PrimDirCalcAssist.PoleRegiomontanus(decl, mdUpper, geoLat);
        Assert.That(actual, Is.EqualTo(expected).Within(1E-2));   // small delta because the example values are rounded
    }

   
    [Test]
    public void TestAdUnderRegPole()
    {
        // values from Gansten, Prim dir, p. 156
        const double decl = 18.1666666666667;
        const double regPole = 51.78333333;
        const double expected = 24.63333333;
        double actual = PrimDirCalcAssist.AdUnderRegPole(regPole, decl);
        Assert.That(actual, Is.EqualTo(expected).Within(1E-2));   // small delta because the example values are rounded
    }

    [Test]
    public void TestElevationOfThePolePlac()
    {
        // values from Gansten, Prim dir, p. 156
        const double adPole = 15.0;
        const double decl = 18.1666666666667;
        const double expected = 38.2666666666667;
        double actual = PrimDirCalcAssist.ElevationOfThePolePlac(adPole, decl);
        Assert.That(actual, Is.EqualTo(expected).Within(1E-2));   // small delta because the example values are rounded
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
        double actual = PrimDirCalcAssist.ObliqueAscdesc(raPoint, ascDiff, east, north);
        Assert.That(actual, Is.EqualTo(expected).Within(DELTA));
    }
}