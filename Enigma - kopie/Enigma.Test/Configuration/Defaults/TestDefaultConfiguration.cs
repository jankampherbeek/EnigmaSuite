// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;
using Enigma.Configuration.Parsers;
using Enigma.Domain.Analysis;
using Enigma.Domain.CalcVars;

namespace Enigma.Test.Configuration.Defaults;

[TestFixture]
public class TestDefaultConfiguration
{

    private IDefaultConfiguration _defaultConfiguration;
    private AstroConfig _astroConfig;
    private readonly double _delta = 0.00000001;
    


    [SetUp]
    public void SetUp() 
    {
        _defaultConfiguration = new DefaultConfiguration();
        _astroConfig = _defaultConfiguration.CreateDefaultConfig();
    }


    [Test]
    public void TestHouseSystem()
    {
        Assert.That(_astroConfig.HouseSystem, Is.EqualTo(HouseSystems.Placidus));
    }

    [Test]
    public void TestAyanamsha()
    {
        Assert.That(_astroConfig.Ayanamsha, Is.EqualTo(Ayanamshas.None));
    }

    [Test]
    public void TestZodiacType()
    {
        Assert.That(_astroConfig.ZodiacType, Is.EqualTo(ZodiacTypes.Tropical));
    }

    [Test]
    public void TestObserverPosition()
    {
        Assert.That(_astroConfig.ObserverPosition, Is.EqualTo(ObserverPositions.GeoCentric));
    }

    [Test]
    public void TestProjectionType()
    {
        Assert.That(_astroConfig.ProjectionType, Is.EqualTo(ProjectionTypes.twoDimensional));
    }

    [Test]
    public void TestOrbMethod()
    {
        Assert.That(_astroConfig.OrbMethod, Is.EqualTo(OrbMethods.Weighted));
    }

    [Test]
    public void TestBaseOrbAspects()
    {
        Assert.That(_astroConfig.BaseOrbAspects, Is.EqualTo(10.0).Within(_delta));
    }

    [Test]
    public void TestBaseOrbMidpoints()
    {
        Assert.That(_astroConfig.BaseOrbMidpoints, Is.EqualTo(1.6).Within(_delta));
    }

    [Test]
    public void TestCelPoints()
    {
        List<CelPointSpecs> celPoints = _astroConfig.CelPoints;
        Assert.That(_astroConfig.CelPoints.Count, Is.EqualTo(52));

        CelPointSpecs celPointSpecs = celPoints[0];     // Sun
        Assert.That(celPointSpecs.SolarSystemPoint, Is.EqualTo(SolarSystemPoints.Sun));
        Assert.IsTrue(celPointSpecs.IsUsed);
        Assert.That(celPointSpecs.PercentageAspectOrb, Is.EqualTo(100).Within(_delta));
        
    }

    [Test]
    public void TestAspects()
    {
        List<AspectSpecs> aspects = _astroConfig.Aspects;
        Assert.That(_astroConfig.Aspects.Count, Is.EqualTo(22));

        AspectSpecs aspectSpecs = aspects[1];           // Opposition
        Assert.That(aspectSpecs.AspectType, Is.EqualTo(AspectTypes.Opposition));
        Assert.IsTrue(aspectSpecs.IsUsed);
        Assert.That(aspectSpecs.PercentageAspectOrb, Is.EqualTo(100));
    }

}