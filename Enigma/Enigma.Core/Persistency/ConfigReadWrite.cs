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