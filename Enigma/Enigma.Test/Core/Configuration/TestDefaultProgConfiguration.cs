// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Configuration;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Configuration;

[TestFixture]
public class TestDefaultProgConfiguration
{
    private IDefaultProgConfiguration? _defaultProgConfiguration;
    private ConfigProg? _configProg;
    private const double Delta = 0.00000001;


    [SetUp]
    public void SetUp()
    {
        _defaultProgConfiguration = new DefaultProgConfiguration();
        _configProg = _defaultProgConfiguration.CreateDefaultConfig();
    }
    
    [Test]
    public void TestOrbTransits()
    {
        Assert.That(_configProg!.ConfigTransits.Orb, Is.EqualTo(1.0).Within(Delta));
    }

    [Test]
    public void TestPrimaryTimeKey()
    {
        Assert.That(_configProg!.ConfigPrimDir.TimeKey, Is.EqualTo(PrimaryKeys.Naibod));
    }
    
    [Test]
    public void TestPrimaryMethod()
    {
        Assert.That(_configProg!.ConfigPrimDir.DirMethod, Is.EqualTo(PrimaryDirMethods.SemiArcMundane));
    }

    [Test]
    public void TestSolarMethod()
    {
        Assert.That(_configProg!.ConfigSolar.SolarMethod, Is.EqualTo(SolarMethods.TropicalNoParallax));
    }
    
    [Test]
    public void TestProgPointsSymbolic()
    {
        Dictionary<ChartPoints, ProgPointConfigSpecs> progPoints = _configProg!.ConfigSymDir.ProgPoints;
        ProgPointConfigSpecs progPointSpecsMars = progPoints[ChartPoints.Mars];
        Assert.That(progPointSpecsMars.IsUsed, Is.True);
        ProgPointConfigSpecs progPointSpecsMakeMake = progPoints[ChartPoints.Makemake];
        Assert.That(progPointSpecsMakeMake.IsUsed, Is.False);
    }
    
    [Test]
    public void TestSignificatorsy()
    {
        Dictionary<ChartPoints, ProgPointConfigSpecs> significators = _configProg!.ConfigPrimDir.Significators;
        ProgPointConfigSpecs progPointSpecsMercury = significators[ChartPoints.Mercury];
        Assert.That(progPointSpecsMercury.IsUsed, Is.True);
        ProgPointConfigSpecs progPointSpecsPluto = significators[ChartPoints.Pluto];
        Assert.That(progPointSpecsPluto.IsUsed, Is.False);
    }
    
    [Test]
    public void TestPromissors()
    {
        Dictionary<ChartPoints, ProgPointConfigSpecs> promissors = _configProg!.ConfigPrimDir.Promissors;
        ProgPointConfigSpecs progPointSpecsFortunaSect = promissors[ChartPoints.FortunaSect];
        Assert.That(progPointSpecsFortunaSect.IsUsed, Is.True);
        ProgPointConfigSpecs progPointSpecsEris = promissors[ChartPoints.Eris];
        Assert.That(progPointSpecsEris.IsUsed, Is.False);
    }
    
}