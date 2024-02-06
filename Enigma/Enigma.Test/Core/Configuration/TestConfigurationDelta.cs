// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Configuration;
using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Moq;

namespace Enigma.Test.Core.Configuration;

[TestFixture]
public class TestConfigurationDelta
{
    private IConfigurationDelta _configDelta;
    private IDeltaTexts _deltaTextsMock;
    private Dictionary<ChartPoints, ChartPointConfigSpecs> _chartPointsDefault;
    private Dictionary<ChartPoints, ChartPointConfigSpecs> _chartPointsUpdated;
    private Dictionary<AspectTypes, AspectConfigSpecs> _aspectsDefault;
    private Dictionary<AspectTypes, AspectConfigSpecs> _aspectsUpdated;
    
    private ConfigProg _configProgDefault;
    private ConfigProg _configProgUpdated;
    private AstroConfig _configDefault;
    private AstroConfig _configUpdated;
    
    [SetUp]
    public void SetUp()
    {
        _deltaTextsMock = CreateDeltaTextsMock();
        _configDelta = new ConfigurationDelta(_deltaTextsMock);
        _chartPointsDefault = CreateChartPointsDefault();
        _chartPointsUpdated = CreateChartPointsUpdated();
        _aspectsDefault = CreateAspectsDefault();
        _aspectsUpdated = CreateAspectsUpdated();
        _configDefault = CreateDefaultConfig();
        _configUpdated = CreateUpdatedConfig();
        _configProgDefault = CreateDefaultProgConfig();
        _configProgUpdated = CreateUpdatedProgConfig();
    }

    [Test]
    public void TestDeltaCount()
    {
        Dictionary<string, string> result = _configDelta.RetrieveTextsForDeltas(_configDefault, _configUpdated);
        Assert.That(result, Has.Count.EqualTo(9));
    }
    
    [Test]
    public void TestDeltaCountProg()
    {
        Dictionary<string, string> result = _configDelta.RetrieveTextsForProgDeltas(_configProgDefault, _configProgUpdated);
        Assert.That(result, Has.Count.EqualTo(6));
    }

    [Test]
    public void TestOrbsProg()
    {
        Dictionary<string, string> result = _configDelta.RetrieveTextsForProgDeltas(_configProgDefault, _configProgUpdated);
        Assert.Multiple(() =>
        {
            Assert.That(result.Keys, Does.Contain("TR_ORB"));
            Assert.That(result.Keys, Does.Contain("SM_ORB"));
            Assert.That(result["TR_ORB"], Is.EqualTo("1"));
            Assert.That(result["SM_ORB"], Is.EqualTo("1.1"));
        });        
    }
    
    [Test]
    public void TestSymKeyProg()
    {
        Dictionary<string, string> result = _configDelta.RetrieveTextsForProgDeltas(_configProgDefault, _configProgUpdated);
        Assert.Multiple(() =>
        {
            Assert.That(result.Keys, Does.Contain("SM_KEY"));
            Assert.That(result["SM_KEY"], Is.EqualTo("1"));
        });        
    }
    
    [Test]
    public void TestChartPoints()
    {
        Dictionary<string, string> result = _configDelta.RetrieveTextsForDeltas(_configDefault, _configUpdated);
        Assert.Multiple(() =>
        {
            Assert.That(result.Keys, Does.Contain("CP_0"));
            Assert.That(result.Keys, Does.Contain("CP_1"));
            Assert.That(result.Keys, Does.Contain("CP_2"));
            Assert.That(result["CP_0"], Is.EqualTo("y||a||100||y"));
            Assert.That(result["CP_1"], Is.EqualTo("y||b||100||y"));
            Assert.That(result["CP_2"], Is.EqualTo("y||c||100||y"));
        });
    }
    
    [Test]
    public void TestChartPointsTransits()
    {
        Dictionary<string, string> result = _configDelta.RetrieveTextsForProgDeltas(_configProgDefault, _configProgUpdated);
        Assert.Multiple(() =>
        {
            Assert.That(result.Keys, Does.Contain("TR_CP_5"));
            Assert.That(result["TR_CP_5"], Is.EqualTo("n||f"));
        });
    }
    
    [Test]
    public void TestChartPointsSecDir()
    {
        Dictionary<string, string> result = _configDelta.RetrieveTextsForProgDeltas(_configProgDefault, _configProgUpdated);
        Assert.Multiple(() =>
        {
            Assert.That(result.Keys, Does.Contain("SC_CP_2"));
            Assert.That(result["SC_CP_2"], Is.EqualTo("n||c"));
        });
    }
    
    [Test]
    public void TestChartPointsSymDir()
    {
        Dictionary<string, string> result = _configDelta.RetrieveTextsForProgDeltas(_configProgDefault, _configProgUpdated);
        Assert.Multiple(() =>
        {
            Assert.That(result.Keys, Does.Contain("SM_CP_1001"));
            Assert.That(result["SM_CP_1001"], Is.EqualTo("n||A"));
        });
    }

    [Test]
    public void TestAspects()
    {
        Dictionary<string, string> result = _configDelta.RetrieveTextsForDeltas(_configDefault, _configUpdated);
        Assert.Multiple(() =>
        {
            Assert.That(result.Keys, Does.Contain("AT_1"));
            Assert.That(result["AT_1"], Does.Contain("y||C||90||y"));
        });
    }

    [Test] 
    public void TestReferences() 
    {
        Dictionary<string, string> result = _configDelta.RetrieveTextsForDeltas(_configDefault, _configUpdated);
        Assert.Multiple(() =>
        {
            Assert.That(result.Keys, Does.Contain(StandardTexts.CFG_HOUSE_SYSTEM));
            Assert.That(result[StandardTexts.CFG_HOUSE_SYSTEM], Does.Contain("4"));
            Assert.That(result.Keys, Does.Contain(StandardTexts.CFG_AYANAMSHA));
            Assert.That(result[StandardTexts.CFG_AYANAMSHA], Does.Contain("0"));
            Assert.That(result.Keys, Does.Contain(StandardTexts.CFG_ZODIAC_TYPE));
            Assert.That(result[StandardTexts.CFG_ZODIAC_TYPE], Does.Contain("1"));
            Assert.That(result.Keys, Does.Not.Contain(StandardTexts.CFG_OBSERVER_POSITION));
            Assert.That(result.Keys, Does.Not.Contain(StandardTexts.CFG_PROJECTION_TYPE));
            Assert.That(result.Keys, Does.Not.Contain(StandardTexts.CFG_ORB_METHOD));
        });
    }
    
    [Test] 
    public void TestConstants() 
    {
        Dictionary<string, string> result = _configDelta.RetrieveTextsForDeltas(_configDefault, _configUpdated);
        Assert.Multiple(() =>
        {
            Assert.That(result.Keys, Does.Contain(StandardTexts.CFG_BASE_ORB_ASPECTS));
            Assert.That(result[StandardTexts.CFG_BASE_ORB_ASPECTS], Does.Contain("8"));
            Assert.That(result.Keys, Does.Contain(StandardTexts.CFG_USE_CUSPS_FOR_ASPECTS));
            Assert.That(result[StandardTexts.CFG_USE_CUSPS_FOR_ASPECTS], Does.Contain("False"));
            Assert.That(result.Keys, Does.Not.Contain(StandardTexts.CFG_BASE_ORB_MIDPOINTS));
        });
    }

    [Test]
    public void TestProgTransitOrb()
    {
       // Dictionary<string, string> result = _configDelta.
    }
    

    private AstroConfig CreateDefaultConfig()
    {
        return new AstroConfig (
            HouseSystems.Alcabitius, 
            Ayanamshas.Fagan, 
            ObserverPositions.GeoCentric, 
            ZodiacTypes.Sidereal, 
            ProjectionTypes.TwoDimensional, 
            OrbMethods.Weighted,
            _chartPointsDefault,
            _aspectsDefault,
            10,
            3,
            true);
    }

    private AstroConfig CreateUpdatedConfig()
    {
        return new AstroConfig (
            HouseSystems.Regiomontanus,         // different 
            Ayanamshas.None,                     // different 
            ObserverPositions.GeoCentric,    // same 
            ZodiacTypes.Tropical,                         // different          
            ProjectionTypes.TwoDimensional,               // same
            OrbMethods.Weighted,                 // same
            _chartPointsUpdated,                          // different   
            _aspectsUpdated,                              // different   
            8,                               // different
            3,                              // same
            false);                       // different
    }

    private static ConfigProg CreateDefaultProgConfig()
    {
        ConfigProgTransits configTransitsDefault = CreateConfigProgTransitsDefault();
        ConfigProgSecDir configSecDirDefault = CreateConfigProgSecDirDefault();
        ConfigProgSymDir configProgSymDirDefault = CreateConfigProgSymDirDefault();
        return new ConfigProg(configTransitsDefault, configSecDirDefault, configProgSymDirDefault);
    }
    
    private static ConfigProg CreateUpdatedProgConfig()
    {
        ConfigProgTransits configTransitsUpdated = CreateConfigProgTransitsUpdated();
        ConfigProgSecDir configSecDirUpdated = CreateConfigProgSecDirUpdated();
        ConfigProgSymDir configProgSymDirUpdated = CreateConfigProgSymDirUpdated();
        return new ConfigProg(configTransitsUpdated, configSecDirUpdated, configProgSymDirUpdated);
    }

    private static IDeltaTexts CreateDeltaTextsMock()
    {
        ChartPointConfigSpecs configSun = new(true, 'a', 100, true); 
        ChartPointConfigSpecs configMoon = new(true, 'b', 90, true);
        ChartPointConfigSpecs configMercury = new(true, 'c', 100, true);
        AspectConfigSpecs configConjunction = new(true, 'B', 100, true);
        AspectConfigSpecs configOpposition = new(true, 'C', 90, true);
        AspectConfigSpecs configTriangle = new(true, 'D', 100, true);
        ProgPointConfigSpecs configTransitMars = new(false, 'f');
        ProgPointConfigSpecs configSecDirMercury = new(false, 'c');
        ProgPointConfigSpecs configSymDirAsc = new(false, 'A');
        Tuple<string, string> resultTextsSun = new("CP_0", "y||a||100||y");
        Tuple<string, string> resultTextsMoon = new("CP_1", "y||b||100||y");
        Tuple<string, string> resultTextsMercury = new("CP_2", "y||c||100||y");        
        Tuple<string, string> resultTextsConjunction = new("AT_0", "y||B||100||y");
        Tuple<string, string> resultTextsOpposition = new("AT_1", "y||C||90||y");
        Tuple<string, string> resultTextsTriangle = new("AT_2", "y||D||100||y");
        Tuple<string, string> resultTextsTransitOrb = new("TR_ORB", "1.0");
        Tuple<string, string> resultTextsSymDirOrb = new("SM_ORB", "1.1");
        Tuple<string, string> resultTextsTransitMars = new("TR_CP_5", "n||f");
        Tuple<string, string> resultTextsSecDirMercury = new("SC_CP_2", "n||c");
        Tuple<string, string> resultTextsSymDirAsc = new("SM_CP_1001", "n||A");
        Tuple<string, string> resultTextsSymDirKey = new("SM_KEY", "2");
        
        var mock = new Mock<IDeltaTexts>();
        mock.Setup((p => p.CreateDeltaForPoint(ChartPoints.Sun, configSun )))
            .Returns(resultTextsSun);
        mock.Setup((p => p.CreateDeltaForPoint(ChartPoints.Moon, configMoon )))
            .Returns(resultTextsMoon);
        mock.Setup((p => p.CreateDeltaForPoint(ChartPoints.Mercury, configMercury)))
            .Returns(resultTextsMercury);
        mock.Setup((p => p.CreateDeltaForAspect(AspectTypes.Conjunction, configConjunction )))
            .Returns(resultTextsConjunction);
        mock.Setup((p => p.CreateDeltaForAspect(AspectTypes.Opposition, configOpposition )))
            .Returns(resultTextsOpposition);
        mock.Setup((p => p.CreateDeltaForAspect(AspectTypes.Triangle, configTriangle )))
            .Returns(resultTextsTriangle);
        mock.Setup(p => p.CreateDeltaForProgOrb(ProgresMethods.Transits, 1.0))
            .Returns(resultTextsTransitOrb);
        mock.Setup(p => p.CreateDeltaForProgOrb(ProgresMethods.Symbolic, 1.1))
            .Returns(resultTextsSymDirOrb);
        mock.Setup(p => p.CreateDeltaForProgChartPoint(ProgresMethods.Transits, ChartPoints.Mars, configTransitMars))
            .Returns(resultTextsTransitMars);
        mock.Setup(p => p.CreateDeltaForProgChartPoint(ProgresMethods.Secondary, ChartPoints.Mercury, configSecDirMercury))
            .Returns(resultTextsSecDirMercury);
        mock.Setup(p => p.CreateDeltaForProgChartPoint(ProgresMethods.Symbolic, ChartPoints.Ascendant, configSymDirAsc))
            .Returns(resultTextsSymDirAsc);
        mock.Setup(p => p.CreateDeltaForProgSymKey(SymbolicKeys.TrueSun))
            .Returns(resultTextsSymDirKey);
        
        return mock.Object;
    }

    private static Dictionary<ChartPoints, ChartPointConfigSpecs> CreateChartPointsDefault()
    {
        return new Dictionary<ChartPoints, ChartPointConfigSpecs>
        {
            { ChartPoints.Sun, new ChartPointConfigSpecs(true, 'a', 90, true) },
            { ChartPoints.Moon, new ChartPointConfigSpecs(true, 'x', 100, true) },
            { ChartPoints.Mercury, new ChartPointConfigSpecs(true, 'c', 90, true) },
            { ChartPoints.Venus, new ChartPointConfigSpecs(true, 'd', 100, true) }
        };
    }
    
    private static Dictionary<ChartPoints, ChartPointConfigSpecs> CreateChartPointsUpdated()
    {
        return new Dictionary<ChartPoints, ChartPointConfigSpecs>
        {
            { ChartPoints.Sun, new ChartPointConfigSpecs(true, 'a', 100, true) },
            { ChartPoints.Moon, new ChartPointConfigSpecs(true, 'b', 90, true) },
            { ChartPoints.Mercury, new ChartPointConfigSpecs(true, 'c', 100, true) }
        };
    }

    private static Dictionary<AspectTypes, AspectConfigSpecs> CreateAspectsDefault()
    {
        return new Dictionary<AspectTypes, AspectConfigSpecs>
        {
            { AspectTypes.Conjunction, new AspectConfigSpecs(true, 'B', 100, true) },
            { AspectTypes.Opposition, new AspectConfigSpecs(true, 'x', 100, true) },
            { AspectTypes.Triangle, new AspectConfigSpecs(true, 'D', 100, true) },
            { AspectTypes.Square, new AspectConfigSpecs(true, 'E', 100, true) }
        };
    }
    
    private static Dictionary<AspectTypes, AspectConfigSpecs> CreateAspectsUpdated()
    {
        return new Dictionary<AspectTypes, AspectConfigSpecs>
        {
            { AspectTypes.Conjunction, new AspectConfigSpecs(true, 'B', 100, true) },
            { AspectTypes.Opposition, new AspectConfigSpecs(true, 'C', 90, true) },
            { AspectTypes.Triangle, new AspectConfigSpecs(true, 'D', 100, true) },
            { AspectTypes.Square, new AspectConfigSpecs(true, 'E', 100, true) }
        };
    }
    
    
    private static ConfigProgTransits CreateConfigProgTransitsDefault()
    {
        const double orb = 0.5;
        Dictionary<ChartPoints, ProgPointConfigSpecs> points = new()
        {
            { ChartPoints.Mars, new ProgPointConfigSpecs(true, 'f') },
            { ChartPoints.Jupiter, new ProgPointConfigSpecs(true, 'g') },
            { ChartPoints.Saturn, new ProgPointConfigSpecs(true, 'h') }
        };
        return new ConfigProgTransits(orb, points);
    }
    
    private static ConfigProgTransits CreateConfigProgTransitsUpdated()
    {
        const double orb = 1.0;
        Dictionary<ChartPoints, ProgPointConfigSpecs> points = new()
        {
            { ChartPoints.Mars, new ProgPointConfigSpecs(false, 'f') },
            { ChartPoints.Jupiter, new ProgPointConfigSpecs(true, 'g') },
            { ChartPoints.Saturn, new ProgPointConfigSpecs(true, 'h') }
        };
        return new ConfigProgTransits(orb, points);
    }
    
    private static ConfigProgSecDir CreateConfigProgSecDirDefault()
    {
        const double orb = 0.75;
        Dictionary<ChartPoints, ProgPointConfigSpecs> points = new()
        {
            { ChartPoints.Sun, new ProgPointConfigSpecs(true, 'a') },
            { ChartPoints.Moon, new ProgPointConfigSpecs(true, 'b') },
            { ChartPoints.Mercury, new ProgPointConfigSpecs(true, 'c') }
        };
        return new ConfigProgSecDir(orb, points);
    }

    private static ConfigProgSecDir CreateConfigProgSecDirUpdated()
    {
        const double orb = 0.75;
        Dictionary<ChartPoints, ProgPointConfigSpecs> points = new()
        {
            { ChartPoints.Sun, new ProgPointConfigSpecs(true, 'a') },
            { ChartPoints.Moon, new ProgPointConfigSpecs(true, 'b') },
            { ChartPoints.Mercury, new ProgPointConfigSpecs(false, 'c') }
        };
        return new ConfigProgSecDir(orb, points);
    }
    
    private static ConfigProgSymDir CreateConfigProgSymDirDefault()
    {
        const double orb = 0.9;
        const SymbolicKeys key = SymbolicKeys.MeanSun;
        Dictionary<ChartPoints, ProgPointConfigSpecs> points = new()
        {
            { ChartPoints.Ascendant, new ProgPointConfigSpecs(true, 'A') },
            { ChartPoints.Saturn, new ProgPointConfigSpecs(true, 'h') },
            { ChartPoints.Chiron, new ProgPointConfigSpecs(true, 'w') }
        };
        return new ConfigProgSymDir(orb, key, points);
    }
    
    private static ConfigProgSymDir CreateConfigProgSymDirUpdated()
    {
        const double orb = 1.1;
        const SymbolicKeys key = SymbolicKeys.TrueSun;
        Dictionary<ChartPoints, ProgPointConfigSpecs> points = new()
        {
            { ChartPoints.Ascendant, new ProgPointConfigSpecs(false, 'A') },
            { ChartPoints.Saturn, new ProgPointConfigSpecs(true, 'h') },
            { ChartPoints.Chiron, new ProgPointConfigSpecs(true, 'w') }
        };
        return new ConfigProgSymDir(orb, key, points);
    }
    
}