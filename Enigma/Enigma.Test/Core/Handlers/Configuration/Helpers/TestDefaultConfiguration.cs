// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Configuration.Helpers;
using Enigma.Core.Handlers.Configuration.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;

namespace Enigma.Test.Core.Handlers.Configuration.Helpers;

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
        Assert.That(_astroConfig.ProjectionType, Is.EqualTo(ProjectionTypes.TwoDimensional));
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
        Dictionary<ChartPoints, ChartPointConfigSpecs> celPoints = _astroConfig.ChartPoints;

        ChartPointConfigSpecs celPointSpecs = celPoints[ChartPoints.Sun];     
        Assert.Multiple(() =>
        {
            Assert.That(celPointSpecs.IsUsed, Is.True);
            Assert.That(celPointSpecs.PercentageOrb, Is.EqualTo(100).Within(_delta));
        });
    }

    [Test]
    public void TestAspects()
    {
        Dictionary<AspectTypes, AspectConfigSpecs> aspects = _astroConfig.Aspects;
        Assert.That(_astroConfig.Aspects, Has.Count.EqualTo(22));

        AspectConfigSpecs aspectSpecs = aspects[AspectTypes.Opposition];
        Assert.Multiple(() =>
        {
            Assert.That(aspectSpecs.IsUsed, Is.True);
            Assert.That(aspectSpecs.PercentageOrb, Is.EqualTo(100));
        });
    }
}