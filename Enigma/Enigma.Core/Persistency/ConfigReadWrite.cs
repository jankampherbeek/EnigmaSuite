// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Persistency;

/// <inheritdoc/>
public sealed class ConfigWriter : IConfigWriter
{
    private readonly ITextFileWriter _textFileWriter;
    private readonly IConfigParser _configParser;

    public ConfigWriter(ITextFileWriter textFileWriter, IConfigParser configParser)
    {
        _textFileWriter = textFileWriter;
        _configParser = configParser;
    }

    /// <inheritdoc/>
    public bool WriteConfig(AstroConfig astroConfig)
    {
        string parsedConfig = _configParser.MarshallConfig(astroConfig);
        return _textFileWriter.WriteFile(EnigmaConstants.CONFIG_LOCATION, parsedConfig);
    }

    /// <inheritdoc/>
    public bool WriteConfig(ConfigProg configProg)
    {
        string parsedConfig = _configParser.MarshallConfig(configProg);
        return _textFileWriter.WriteFile(EnigmaConstants.CONFIG_PROG_LOCATION, parsedConfig);
    }
}

/// <inheritdoc/>
public sealed class ConfigReader : IConfigReader
{
    private readonly ITextFileReader _textFileReader;
    private readonly IConfigParser _configParser;


    public ConfigReader(ITextFileReader textFileReader, IConfigParser configParser)
    {
        _textFileReader = textFileReader;
        _configParser = configParser;
    }

    /// <inheritdoc/>
    public AstroConfig ReadConfig()
    {
        string configText = _textFileReader.ReadFile(EnigmaConstants.CONFIG_LOCATION);
        return _configParser.UnMarshallAstroConfig(configText);
    }

    /// <inheritdoc/>
    public ConfigProg ReadProgConfig()
    {
        string configText = _textFileReader.ReadFile(EnigmaConstants.CONFIG_PROG_LOCATION);
        return _configParser.UnMarshallConfigProg(configText);
    }
}