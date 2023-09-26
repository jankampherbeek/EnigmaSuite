// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Handlers;

/// <inheritdoc/>
public sealed class ConfigurationHandler : IConfigurationHandler
{
    private readonly IDefaultConfiguration _defaultConfig;
    private readonly IDefaultProgConfiguration _defaultProgConfig;
    private readonly IConfigWriter _configWriter;
    private readonly IConfigReader _configReader;

    public ConfigurationHandler(IDefaultConfiguration defaultConfig, IDefaultProgConfiguration defaultProgCopnfig,
        IConfigWriter configWriter, IConfigReader configReader)
    {
        _defaultConfig = defaultConfig;
        _defaultProgConfig = defaultProgCopnfig;
        _configWriter = configWriter;
        _configReader = configReader;
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
    public bool DoesConfigExist()
    {
        return File.Exists(EnigmaConstants.CONFIG_LOCATION);
    }

    /// <inheritdoc/>
    public bool DoesProgConfigExist()
    {
        return File.Exists(EnigmaConstants.CONFIG_PROG_LOCATION);
    }

    /// <inheritdoc/>
    public bool WriteConfig(AstroConfig astroConfig)
    {
        return _configWriter.WriteConfig(astroConfig);
    }

    /// <inheritdoc/>
    public bool WriteConfig(ConfigProg configProg)
    {
        return _configWriter.WriteConfig(configProg);
    }

    /// <inheritdoc/>
    public bool WriteDefaultConfig()
    {
        AstroConfig config = _defaultConfig.CreateDefaultConfig();
        return _configWriter.WriteConfig(config);
    }

    /// <inheritdoc/>
    public bool WriteDefaultProgConfig()
    {
        ConfigProg config = _defaultProgConfig.CreateDefaultConfig();
        return _configWriter.WriteConfig(config);
    }

    /// <inheritdoc/>
    public AstroConfig ReadConfig()
    {
        return _configReader.ReadConfig();
    }

    /// <inheritdoc/>
    public ConfigProg ReadConfigProg()
    {
        return _configReader.ReadProgConfig();
    }
}