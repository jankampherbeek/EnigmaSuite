// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Configuration;

namespace Enigma.Test.Core.Configuration;

[TestFixture]
public class TestConfigParser
{
    private IConfigParser _parser;
    private IDefaultConfiguration? _defaultConfig;
   
    [SetUp]
    public void SetUp()
    {
        _parser = new ConfigParser();
        _defaultConfig = new DefaultConfiguration();
    }

    [Test]
    public void TestParsingDeltas()
    {
        Dictionary<string, string> deltas = CreateDeltas();
        string jsonText = _parser.MarshallDeltasForConfig(deltas);
        Dictionary<string, string> newDeltas = _parser.UnMarshallDeltasForConfig(jsonText);
        Assert.That(newDeltas, Is.EqualTo(deltas));        
        
    }

    private static Dictionary<string, string> CreateDeltas()
    {
        return new Dictionary<string, string>
        {
            { "HouseSystem", "7" },
            { "CP_11", "y||65||{||y\\" },
            { "CP_12", "n||65||{||y\\" },
            { "AT_6", "y||30||Q||y\\" },
            { "AT_10", "y||30||L||y\\" }
        };
    }
}