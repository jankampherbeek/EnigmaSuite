// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Configuration;
using Enigma.Domain.Dtos;
using Serilog;

namespace Enigma.Api.Configuration;

/// <summary>Interface for configurations.</summary>
public interface IConfigurationApi
{

    /// <summary>Retrieve current configuration (default with deltas applied).</summary>
    /// <returns>Current configuration.</returns>
    public AstroConfig GetCurrentConfiguration();
    
    /// <returns>Default configuration.</returns>
    public AstroConfig GetDefaultConfiguration();

    /// <summary>Retrieve current configuration for progressions (default with deltas applied).</summary>
    /// <returns>Current configuration for progresions.</returns>
    public ConfigProg GetCurrentProgConfiguration();

    /// <returns>Default progressive configuration.</returns>
    public ConfigProg GetDefaultProgConfiguration();
    
    /// <summary>Save preogressive configuration.</summary>
    /// <returns>True if save was successful, otherwise false.</returns>
    public bool WriteDeltasForConfig(ConfigProg config);

    public bool WriteDeltasForConfig(AstroConfig config);
}

/// <inheritdoc/>
public sealed class ConfigurationApi(IConfigurationHandler handler) : IConfigurationApi
{
    /// <inheritdoc/>
    public AstroConfig GetCurrentConfiguration()
    {
        Log.Information("ConfigurationApi GetCurrentConfiguration");
        return handler.ReadCurrentConfig();
    }

    /// <inheritdoc/>
    public ConfigProg GetCurrentProgConfiguration()
    {
        Log.Information("ConfigurationApi GetCurrentProgConfiguration");
        return handler.ReadCurrentConfigProg();
    }
    
    /// <inheritdoc/>
    public AstroConfig GetDefaultConfiguration()
    {
        Log.Information("ConfigurationApi GetDefaultConfiguration");
        return handler.ConstructDefaultConfiguration();
    }

    /// <inheritdoc/>
    public ConfigProg GetDefaultProgConfiguration()
    {
        Log.Information("ConfigurationApi GetDefaultProgConfiguration");
        return handler.ConstructDefaultProgConfiguration();
    }

    /// <inheritdoc/>
    public bool WriteDeltasForConfig(AstroConfig config)
    {
        Log.Information("ConfigurationApi WriteDeltasForConfig for astroconfig");
        return handler.WriteDeltasForConfig(config);
    }
    
    /// <inheritdoc/>
    public bool WriteDeltasForConfig(ConfigProg config)
    {
        Log.Information("ConfigurationApi WriteDeltasForConfig for progressive config");
        return handler.WriteDeltasForConfig(config);
    }
    
}
