// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Handlers;

/// <inheritdoc/>
public sealed class ConfigurationHandler : IConfigurationHandler
{
    private readonly IDefaultConfiguration _defaultConfig;
    private readonly IDefaultProgConfiguration _defaultProgConfig;
    private readonly IConfigWriter _configWriter;
    private readonly IConfigReader _configReader;
    private readonly IConfigurationDelta _configDelta;
    private readonly IActualConfigCreator _configCreator;

    public ConfigurationHandler(IDefaultConfiguration defaultConfig, IDefaultProgConfiguration defaultProgCopnfig,
        IConfigurationDelta configDelta, IActualConfigCreator configCreator,
        IConfigWriter configWriter, IConfigReader configReader)
    {
        _defaultConfig = defaultConfig;
        _defaultProgConfig = defaultProgCopnfig;
        _configWriter = configWriter;
        _configReader = configReader;
        _configDelta = configDelta;
        _configCreator = configCreator;
    }

    /// <inheritdoc/>
    public AstroConfig ConstructDefaultConfiguration()
    {
        return _defaultConfig.CreateDefaultConfig();
    }

    /// <inheritdoc/>
    public ConfigProg ConstructDefaultProgConfiguration()
    {
        return _defaultProgConfig.CreateDefaultConfig();
    }

 /// <inheritdoc/>
    public bool WriteDeltasForConfig(AstroConfig astroConfig)
    {
        AstroConfig defaultConfig = _defaultConfig.CreateDefaultConfig();
        Dictionary<string, string> deltas = _configDelta.RetrieveTextsForDeltas(defaultConfig, astroConfig);
        return _configWriter.WriteConfigDeltas(deltas);
    }

    /// <inheritdoc/>
    public bool WriteDeltasForConfig(ConfigProg configProg)
    {
        ConfigProg defaultConfig = _defaultProgConfig.CreateDefaultConfig();
        Dictionary<string, string> deltas = _configDelta.RetrieveTextsForProgDeltas(defaultConfig, configProg);
        return _configWriter.WriteConfigDeltasProg(deltas);
    }


    /// <inheritdoc/>
    public AstroConfig ReadCurrentConfig()
    {
        AstroConfig defaultConfig = _defaultConfig.CreateDefaultConfig();
        Dictionary<string, string> deltas = _configReader.ReadDeltasForConfig();
        return _configCreator.CreateActualConfig(defaultConfig, deltas);
    }

    /// <inheritdoc/>
    public ConfigProg ReadCurrentConfigProg()
    {
        ConfigProg defaultConfig = _defaultProgConfig.CreateDefaultConfig();
        Dictionary<string, string> deltas = _configReader.ReadDeltasForConfigProg();
        return _configCreator.CreateActualProgConfig(defaultConfig, deltas);
    }
}