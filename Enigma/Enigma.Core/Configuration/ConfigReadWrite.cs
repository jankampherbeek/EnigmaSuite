// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Configuration;



/// <inheritdoc/>
public sealed class ConfigWriter : IConfigWriter
{
    private readonly ITextFileWriter _textFileWriter;
    private readonly IAstroConfigParser _configParser;

    public ConfigWriter(ITextFileWriter textFileWriter, IAstroConfigParser configParser)
    {
        _textFileWriter = textFileWriter;
        _configParser = configParser;
    }

    /// <inheritdoc/>
    public bool WriteConfig(AstroConfig astroConfig)
    {
        string parsedConfig = _configParser.Marshall(astroConfig);
        return _textFileWriter.WriteFile(EnigmaConstants.CONFIG_LOCATION, parsedConfig);
    }

}

/// <inheritdoc/>
public sealed class ConfigReader : IConfigReader
{
    private readonly ITextFileReader _textFileReader;
    private readonly IAstroConfigParser _configParser;


    public ConfigReader(ITextFileReader textFileReader, IAstroConfigParser configParser)
    {
        _textFileReader = textFileReader;
        _configParser = configParser;
    }

    /// <inheritdoc/>
    public AstroConfig ReadConfig()
    {
        string configText = _textFileReader.ReadFile(EnigmaConstants.CONFIG_LOCATION);
        return _configParser.UnMarshall(configText);
    }
}