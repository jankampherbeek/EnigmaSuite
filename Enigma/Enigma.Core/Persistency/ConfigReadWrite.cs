// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Configuration;
using Enigma.Domain.Constants;

namespace Enigma.Core.Persistency;

/// <summary>Handles writing a configuration to a disk file.</summary>
public interface IConfigWriter
{

    /// <summary>Writes the configuration, defined as deltas from the default configuration, to disk.</summary>
    /// <param name="deltas">Dictionary with thedeltas.</param>
    /// <returns>True if the write was succesfull, otherwise false.</returns>
    public bool WriteConfigDeltas(Dictionary<string, string> deltas);
    
    /// <summary>Writes the configuration, defined as deltas from the default configuration for progressions, to disk.</summary>
    /// <param name="deltas">Dictionary with the deltas.</param>
    /// <returns>True if the write was succesfull, otherwise false.</returns>
    public bool WriteConfigDeltasProg(Dictionary<string, string> deltas);
}

/// <summary>Handles reading a configuration from a disk file.</summary>
public interface IConfigReader
{

    /// <summary>Reads the deltas for the configuration.</summary>
    /// <returns>The deltas that were read.</returns>
    public Dictionary<string, string> ReadDeltasForConfig();

    /// <summary>Reads the deltas for the configuration for progressions.</summary>
    /// <returns>The deltas that were read.</returns>
    public Dictionary<string, string> ReadDeltasForConfigProg();
    
}




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
    public bool WriteConfigDeltas(Dictionary<string, string> deltas)
    {
        string parsedDeltas = _configParser.MarshallDeltasForConfig(deltas);
        return _textFileWriter.WriteFile(EnigmaConstants.CONFIG_DELTA_LOCATION, parsedDeltas);
    }

    public bool WriteConfigDeltasProg(Dictionary<string, string> deltas)
    {
        string parsedDeltas = _configParser.MarshallDeltasForConfig(deltas);
        return _textFileWriter.WriteFile(EnigmaConstants.CONFIG_PROG_DELTA_LOCATION, parsedDeltas);
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

    public Dictionary<string, string> ReadDeltasForConfig()
    {
        string deltaText = _textFileReader.ReadFile(EnigmaConstants.CONFIG_DELTA_LOCATION);
        return _configParser.UnMarshallDeltasForConfig(deltaText);
    }

    public Dictionary<string, string> ReadDeltasForConfigProg()
    {
        string deltaText = _textFileReader.ReadFile(EnigmaConstants.CONFIG_PROG_DELTA_LOCATION);
        return _configParser.UnMarshallDeltasForConfig(deltaText);
    }
}