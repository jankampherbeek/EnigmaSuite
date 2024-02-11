// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Configuration;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Configuration;

[TestFixture]
public class TestActualConfigCreator
{
    private const double DELTA = 0.000001;
    private IActualConfigCreator _configCreator = new ActualConfigCreator();

    [Test]
    public void TestDefaultReferencesInConfig()
    {
        AstroConfig updatedConfig = _configCreator.CreateActualConfig(CreateDefaultConfig(), CreateDeltas());
        Assert.Multiple(() =>
        {
            Assert.That(updatedConfig.ObserverPosition, Is.EqualTo(ObserverPositions.TopoCentric));
            Assert.That(updatedConfig.ProjectionType, Is.EqualTo(ProjectionTypes.TwoDimensional));
        });
    }

    [Test]
    public void TestUpdatedReferencesInConfig()
    {
        AstroConfig updatedConfig = _configCreator.CreateActualConfig(CreateDefaultConfig(), CreateDeltas());
        Assert.Multiple(() =>
        {
            Assert.That(updatedConfig.HouseSystem, Is.EqualTo(HouseSystems.Vehlow));
            Assert.That(updatedConfig.Ayanamsha, Is.EqualTo(Ayanamshas.Lahiri));
            Assert.That(updatedConfig.ZodiacType, Is.EqualTo(ZodiacTypes.Sidereal));
        });
    }
    
    [Test]
    public void TestDefaultValuesInConfig()
    {
        AstroConfig updatedConfig = _configCreator.CreateActualConfig(CreateDefaultConfig(), CreateDeltas());
        Assert.Multiple(() =>
        {
            Assert.That(updatedConfig.BaseOrbMidpoints, Is.EqualTo(1.6).Within(DELTA));
            Assert.That(updatedConfig.UseCuspsForAspects, Is.False);
        });
    }
    
    [Test]
    public void TestUpdatedValuesInConfig()
    {
        AstroConfig updatedConfig = _configCreator.CreateActualConfig(CreateDefaultConfig(), CreateDeltas());
        Assert.That(updatedConfig.BaseOrbAspects, Is.EqualTo(7.0).Within(DELTA));
    }

    [Test]
    public void TestDefaultChartPoints()
    {
        AstroConfig updatedConfig = _configCreator.CreateActualConfig(CreateDefaultConfig(), CreateDeltas());
        Assert.Multiple(() =>
        {
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Mercury].IsUsed, Is.True);
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Mercury].Glyph, Is.EqualTo('c'));
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Mercury].PercentageOrb, Is.EqualTo(80));
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Mercury].ShowInChart, Is.True);
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Venus].IsUsed, Is.True);
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Venus].Glyph, Is.EqualTo('d'));
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Venus].PercentageOrb, Is.EqualTo(80));
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Venus].ShowInChart, Is.True);
        });
    }
    
    [Test]
    public void TestUpdatedChartPoints()
    {
        AstroConfig updatedConfig = _configCreator.CreateActualConfig(CreateDefaultConfig(), CreateDeltas());
        Assert.Multiple(() =>
        {
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Sun].IsUsed, Is.True);
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Sun].Glyph, Is.EqualTo('a'));
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Sun].PercentageOrb, Is.EqualTo(90));
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Sun].ShowInChart, Is.True);
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Moon].IsUsed, Is.True);
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Moon].Glyph, Is.EqualTo('b'));
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Moon].PercentageOrb, Is.EqualTo(90));
            Assert.That(updatedConfig.ChartPoints[ChartPoints.Moon].ShowInChart, Is.True);
        });
    }

    [Test]
    public void TestDefaultAspects()
    {
        AstroConfig updatedConfig = _configCreator.CreateActualConfig(CreateDefaultConfig(), CreateDeltas());
        Assert.Multiple(() =>
        {
            Assert.That(updatedConfig.Aspects[AspectTypes.Triangle].IsUsed, Is.True);
            Assert.That(updatedConfig.Aspects[AspectTypes.Triangle].Glyph, Is.EqualTo('D'));
            Assert.That(updatedConfig.Aspects[AspectTypes.Triangle].PercentageOrb, Is.EqualTo(85));
            Assert.That(updatedConfig.Aspects[AspectTypes.Triangle].ShowInChart, Is.True);
            Assert.That(updatedConfig.Aspects[AspectTypes.Opposition].IsUsed, Is.True);
            Assert.That(updatedConfig.Aspects[AspectTypes.Opposition].Glyph, Is.EqualTo('C'));
            Assert.That(updatedConfig.Aspects[AspectTypes.Opposition].PercentageOrb, Is.EqualTo(100));
            Assert.That(updatedConfig.Aspects[AspectTypes.Opposition].ShowInChart, Is.True);
        });
    }
    
    [Test]
    public void TestUpdatedAspects()
    {
        AstroConfig updatedConfig = _configCreator.CreateActualConfig(CreateDefaultConfig(), CreateDeltas());
        Assert.Multiple(() =>
        {
            Assert.That(updatedConfig.Aspects[AspectTypes.Conjunction].IsUsed, Is.True);
            Assert.That(updatedConfig.Aspects[AspectTypes.Conjunction].Glyph, Is.EqualTo('B'));
            Assert.That(updatedConfig.Aspects[AspectTypes.Conjunction].PercentageOrb, Is.EqualTo(90));
            Assert.That(updatedConfig.Aspects[AspectTypes.Conjunction].ShowInChart, Is.True);
        });
    }
    
    
    private AstroConfig CreateDefaultConfig()
    {
        const HouseSystems houseSystem = HouseSystems.Regiomontanus;
        const Ayanamshas ayanamsha = Ayanamshas.None;
        const ObserverPositions observerPosition = ObserverPositions.TopoCentric;
        const ZodiacTypes zodiacType = ZodiacTypes.Tropical;
        const ProjectionTypes projectionType = ProjectionTypes.TwoDimensional;
        const OrbMethods orbMethod = OrbMethods.Weighted;
        Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointsSpecs = CreateChartPoints();
        Dictionary<AspectTypes, AspectConfigSpecs> aspectSpecs = CreateAspects();
        const double baseOrbAspects = 10.0;
        const double baseOrbMidpoints = 1.6;
        const bool useCuspsForAspects = false;
        return new AstroConfig(houseSystem, ayanamsha, observerPosition, zodiacType, projectionType, orbMethod,
            chartPointsSpecs, aspectSpecs, baseOrbAspects, baseOrbMidpoints, useCuspsForAspects);
    }

    private static Dictionary<ChartPoints, ChartPointConfigSpecs> CreateChartPoints()
    {
        return new Dictionary<ChartPoints, ChartPointConfigSpecs>
        {
            { ChartPoints.Sun, new ChartPointConfigSpecs(true, 'a', 100, true) },
            { ChartPoints.Moon, new ChartPointConfigSpecs(true, 'b', 100, true) },
            { ChartPoints.Mercury, new ChartPointConfigSpecs(true, 'c', 80, true) },
            { ChartPoints.Venus, new ChartPointConfigSpecs(true, 'd', 80, true) }
        };
    }

    private static Dictionary<AspectTypes, AspectConfigSpecs> CreateAspects()
    {
        return new Dictionary<AspectTypes, AspectConfigSpecs>
        {
            { AspectTypes.Conjunction, new AspectConfigSpecs(true, 'B', 100, true) },
            { AspectTypes.Opposition, new AspectConfigSpecs(true, 'C', 100, true) },
            { AspectTypes.Triangle, new AspectConfigSpecs(true, 'D', 85, true) }
        };
    }
    
    
    private static Dictionary<string, string> CreateDeltas()
    {
        return new Dictionary<string, string>
        {
            { StandardTexts.CFG_HOUSE_SYSTEM, "15" },       // Vehlow
            { StandardTexts.CFG_AYANAMSHA, "2" },           // Lahiri
            { StandardTexts.CFG_ZODIAC_TYPE, "0" },         // Sidereal
            { StandardTexts.CFG_BASE_ORB_ASPECTS, "7" },    // base orb aspects, changed
            { "CP_0", "y||a||90||y" },                      // Sun - changed
            { "CP_1", "y||b||90||y" },                      // Moon - changed
            { "AT_0", "y||B||90||y" }                       // Conjunction - changed
        };

    }
    
}