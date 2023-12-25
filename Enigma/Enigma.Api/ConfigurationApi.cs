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
    public AstroConfig GetCurrentConfiguration()
    {
        Log.Information("ConfigurationApi GetCurrentConfiguration");
        return _handler.ReadCurrentConfig();
    }

    /// <inheritdoc/>
    public ConfigProg GetCurrentProgConfiguration()
    {
        Log.Information("ConfigurationApi GetCurrentProgConfiguration");
        return _handler.ReadCurrentConfigProg();
    }
    
    /// <inheritdoc/>
    public AstroConfig GetDefaultConfiguration()
    {
        Log.Information("ConfigurationApi GetDefaultConfiguration");
        return _handler.ConstructDefaultConfiguration();
    }

    /// <inheritdoc/>
    public ConfigProg GetDefaultProgConfiguration()
    {
        Log.Information("ConfigurationApi GetDefaultProgConfiguration");
        return _handler.ConstructDefaultProgConfiguration();
    }

    /// <inheritdoc/>
    public bool WriteDeltasForConfig(AstroConfig config)
    {
        Log.Information("ConfigurationApi WriteDeltasForConfig for astroconfig");
        return _handler.WriteDeltasForConfig(config);
    }
    
    /// <inheritdoc/>
    public bool WriteDeltasForConfig(ConfigProg config)
    {
        Log.Information("ConfigurationApi WriteDeltasForConfig for progressive config");
        return _handler.WriteDeltasForConfig(config);
    }
    
}
