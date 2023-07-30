// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Constants;

namespace Enigma.Core.Handlers.Configuration;

/// <inheritdoc/>
public sealed class ConfigurationHandler : IConfigurationHandler
{
    private readonly IDefaultConfiguration _defaultConfig;
    private readonly IConfigWriter _configWriter;
    private readonly IConfigReader _configReader;

    public ConfigurationHandler(IDefaultConfiguration defaultConfig, IConfigWriter configWriter, IConfigReader configReader)
    {
        _defaultConfig = defaultConfig;
        _configWriter = configWriter;
        _configReader = configReader;
    }

    /// <inheritdoc/>
    public AstroConfig ConstructDefaultConfiguration()
    {
        return _defaultConfig.CreateDefaultConfig();
    }

    /// <inheritdoc/>
    public bool DoesConfigExist()
    {
        return File.Exists(EnigmaConstants.CONFIG_LOCATION);
    }

    /// <inheritdoc/>
    public bool WriteConfig(AstroConfig astroConfig)
    {
        return _configWriter.WriteConfig(astroConfig);
    }

    /// <inheritdoc/>
    public bool WriteDefaultConfig()
    {
        AstroConfig config = _defaultConfig.CreateDefaultConfig();
        return _configWriter.WriteConfig(config);
    }

    /// <inheritdoc/>
    public AstroConfig ReadConfig()
    {
        return _configReader.ReadConfig();
    }


}