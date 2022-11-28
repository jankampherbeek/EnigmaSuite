// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Configuration.Interfaces;
using Enigma.Core.Work.Persistency.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Constants;

namespace Enigma.Configuration.Handlers;



/// <inheritdoc/>
public class ConfigWriter : IConfigWriter
{
    private readonly ITextFileWriter _textFileWriter;
    private readonly IAstroConfigParser _configParser;
    private readonly IDefaultConfiguration _defaultConfiguration;


    public ConfigWriter(ITextFileWriter textFileWriter, IAstroConfigParser configParser, IDefaultConfiguration defaultConfiguration)
    {
        _textFileWriter = textFileWriter;
        _configParser = configParser;
        _defaultConfiguration = defaultConfiguration;
    }

    /// <inheritdoc/>
    public bool WriteConfig(AstroConfig astroConfig)
    {
        string parsedConfig = _configParser.Marshall(astroConfig);
        return _textFileWriter.WriteFile(EnigmaConstants.CONFIG_LOCATION, parsedConfig);
    }

}

/// <inheritdoc/>
public class ConfigReader : IConfigReader
{
    private readonly ITextFileReader _textFileReader;
    private readonly IAstroConfigParser _configParser;

    /// <inheritdoc/>
    public ConfigReader(ITextFileReader textFileReader, IAstroConfigParser configParser)
    {
        _textFileReader = textFileReader;
        _configParser = configParser;
    }
    public AstroConfig ReadConfig()
    {
        string configText = _textFileReader.ReadFile(EnigmaConstants.CONFIG_LOCATION);
        return _configParser.UnMarshall(configText);
    }
}