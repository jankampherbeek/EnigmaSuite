// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Configuration.Helpers;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;

namespace Enigma.Test.Core.Configuration.Helpers;

[TestFixture]
public class TestConfigParser
{
    private IAstroConfigParser? _parser;
    private IDefaultConfiguration? _defaultConfig;

    [SetUp]
    public void SetUp()
    {
        _parser = new AstroConfigParser();
        _defaultConfig = new DefaultConfiguration();
    }

    [Test]
    public void TestMarshallUnmarshall()
    {
        AstroConfig config = _defaultConfig!.CreateDefaultConfig();
        string jsonText = _parser!.Marshall(config);
        AstroConfig parsedConfig = _parser.UnMarshall(jsonText);
        Assert.Multiple(() =>
        {
            Assert.That(parsedConfig, Is.Not.Null);
            Assert.That(parsedConfig.ChartPoints, Is.EquivalentTo(config.ChartPoints));
            Assert.That(parsedConfig.Ayanamsha, Is.EqualTo(config.Ayanamsha));
        });
    }
}