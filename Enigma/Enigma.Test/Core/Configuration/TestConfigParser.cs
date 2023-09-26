// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Configuration;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;

namespace Enigma.Test.Core.Configuration;

[TestFixture]
public class TestConfigParser
{
    private IConfigParser? _parser;
    private IDefaultConfiguration? _defaultConfig;

    [SetUp]
    public void SetUp()
    {
        _parser = new ConfigParser();
        _defaultConfig = new DefaultConfiguration();
    }

    [Test]
    public void TestMarshallUnmarshall()
    {
        AstroConfig config = _defaultConfig!.CreateDefaultConfig();
        string jsonText = _parser!.MarshallConfig(config);
        AstroConfig parsedConfig = _parser.UnMarshallAstroConfig(jsonText);
        Assert.Multiple(() =>
        {
            Assert.That(parsedConfig, Is.Not.Null);
            Assert.That(parsedConfig.ChartPoints, Is.EquivalentTo(config.ChartPoints));
            Assert.That(parsedConfig.Ayanamsha, Is.EqualTo(config.Ayanamsha));
        });
    }
}