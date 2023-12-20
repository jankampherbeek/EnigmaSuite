// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Serilog;

namespace Enigma.Api;

/// <inheritdoc/>
public sealed class ConfigurationApi : IConfigurationApi
{
    private readonly IConfigurationHandler _handler;

    public ConfigurationApi(IConfigurationHandler handler) => _handler = handler;

    /// <inheritdoc/>
    public AstroConfig GetDefaultConfiguration()
    {
        Log.Information("ConfigurationApi GetDefaultConfiguration");
        return _handler.ConstructDefaultConfiguration();
    }

    public ConfigProg GetDefaultProgConfiguration()
    {
        Log.Information("ConfigurationApi GetDefaultProgConfiguration");
        return _handler.ConstructDefaultProgConfiguration();
    }

    /// <inheritdoc/>
    public bool DoesConfigExist()
    {
        Log.Information("ConfigurationApi DoesConfigExist");
        return _handler.DoesConfigExist();
    }

    public bool DoesConfigProgExist()
    {
        Log.Information("ConfigurationApi DoesConfigProgExist");
        return _handler.DoesProgConfigExist();
    }

    /// <inheritdoc/>
    public bool WriteConfig(AstroConfig config)
    {
        Log.Information("ConfigurationApi WriteConfig for standard config");
        return _handler.WriteConfig(config);
    }

    public bool WriteConfig(ConfigProg config)
    {
        Log.Information("ConfigurationApi WriteConfig for progressive config");
        return _handler.WriteConfig(config);
    }
    
}
