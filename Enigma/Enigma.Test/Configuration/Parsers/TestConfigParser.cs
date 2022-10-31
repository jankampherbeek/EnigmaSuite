// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Configuration.Parsers;
using Enigma.Domain.Configuration;


namespace Enigma.Test.Configuration.Parsers;

[TestFixture]
public class TestConfigParser
{
    private IAstroConfigParser _parser;
    private IDefaultConfiguration _defaultConfig;

    [SetUp]
    public void SetUp()
    {
        _parser = new AstroConfigParser();
        _defaultConfig = new DefaultConfiguration();
    }

    [Test]
    public void TestMarshallUnmarshall()
    {
        AstroConfig config = _defaultConfig.CreateDefaultConfig();
        string jsonText = _parser.Marshall(config);
        AstroConfig parsedConfig = _parser.UnMarshall(jsonText);
        Assert.That(parsedConfig, Is.Not.Null);
        Assert.That(parsedConfig.CelPoints, Is.EquivalentTo(config.CelPoints));
        Assert.That(parsedConfig.Ayanamsha, Is.EqualTo(config.Ayanamsha));
    }
}