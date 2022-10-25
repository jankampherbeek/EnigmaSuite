// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;
using Enigma.Configuration.Parsers;
using Enigma.Domain.Constants;
using Enigma.Persistency.FileHandling;


namespace Enigma.Configuration.Handlers;

/// <summary>Handles writing a configuration to a disk file.</summary>
public interface IConfigWriter
{
    /// <summary>Writes the default configuration to a disk file.</summary>
    /// <returns>True if the write was succesfull, otherwise false.</returns>
    public bool WriteDefaultConfig();

    /// <summary>Writes a new/changed configuration to a disk file.</summary>
    /// <param name="astroConfig">The configuration to write.</param>
    /// <returns>True if the write was succesfull, otherwise false.</returns>
    public bool WriteChangedConfig(AstroConfig astroConfig);
}


/// <summary>Handles reading a configuration from a disk file.</summary>
public interface IConfigReader
{
    /// <summary>Reads a configuration from a disk file.</summary>
    /// <returns>The configuration that was read.</returns>
    public AstroConfig ReadConfig();
}


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
    public bool WriteChangedConfig(AstroConfig astroConfig)
    {
        string parsedConfig = _configParser.Marshall(astroConfig);
        return _textFileWriter.WriteFile(EnigmaConstants.CONFIG_LOCATION, parsedConfig);
    }

    /// <inheritdoc/>
    public bool WriteDefaultConfig()
    {
        AstroConfig astroConfig = _defaultConfiguration.CreateDefaultConfig();
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
        _configParser= configParser;
    }
    public AstroConfig ReadConfig()
    {
        string configText = _textFileReader.ReadFile(EnigmaConstants.CONFIG_LOCATION);
        return _configParser.UnMarshall(configText);
    }
}