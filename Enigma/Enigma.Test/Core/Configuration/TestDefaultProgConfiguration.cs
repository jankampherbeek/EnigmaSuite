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
    private const double DELTA = 0.00000001;


    [SetUp]
    public void SetUp()
    {
        _defaultProgConfiguration = new DefaultProgConfiguration();
        _configProg = _defaultProgConfiguration.CreateDefaultConfig();
    }
    
    [Test]
    public void TestOrbTransits()
    {
        Assert.That(_configProg!.ConfigTransits.Orb, Is.EqualTo(1.0).Within(DELTA));
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
    
}