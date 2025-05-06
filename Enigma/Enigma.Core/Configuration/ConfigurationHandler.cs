// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Persistency;
using Enigma.Domain.Dtos;
using Serilog;

namespace Enigma.Core.Configuration;


/// <summary>Handler for configurations.</summary>
public interface IConfigurationHandler
{

    /// <summary>Defines a default configuration.</summary>
    /// <returns>Default configuration.</returns>
    public AstroConfig ConstructDefaultConfiguration();

    /// <summary>Defines a default configuration for progressions.</summary>
    /// <returns>Default progressive configuration.</returns>
    public ConfigProg ConstructDefaultProgConfiguration();
    
    /// <summary>Write deltas for configuration to file.</summary>
    /// <param name="astroConfig">The configuration.</param>
    /// <returns>True if no error occurred, otherwise false.</returns>
    public bool WriteDeltasForConfig(AstroConfig astroConfig);
    
    
    /// <summary>Write progressive configuration to file.</summary>
    /// <param name="configProg">The progressive configuration.</param>
    /// <returns>True if no error occurred, otherwise false.</returns>
    public bool WriteDeltasForConfig(ConfigProg configProg);
    
    /// <summary>Read current configuration.</summary>
    /// <returns>Configuration: default with applied deltas.</returns>
    public AstroConfig ReadCurrentConfig();

    /// <summary>Read current configuration for progressions.</summary>
    /// <returns>Default configurationf or progressions with applied deltas.</returns>
    public ConfigProg ReadCurrentConfigProg();


}


/// <inheritdoc/>
public sealed class ConfigurationHandler(
    IDefaultConfiguration config,
    IDefaultProgConfiguration defaultProgCopnfig,
    IConfigurationDelta configDelta,
    IActualConfigCreator configCreator,
    IConfigWriter configWriter,
    IConfigReader configReader)
    : IConfigurationHandler
{
    /// <inheritdoc/>
    public AstroConfig ConstructDefaultConfiguration()
    {
        return config.CreateDefaultConfig();
    }

    /// <inheritdoc/>
    public ConfigProg ConstructDefaultProgConfiguration()
    {
        return defaultProgCopnfig.CreateDefaultConfig();
    }

 /// <inheritdoc/>
    public bool WriteDeltasForConfig(AstroConfig astroConfig)
    {
        var defaultConfig = config.CreateDefaultConfig();
        var deltas = configDelta.RetrieveTextsForDeltas(defaultConfig, astroConfig);
        return configWriter.WriteConfigDeltas(deltas);
    }

    /// <inheritdoc/>
    public bool WriteDeltasForConfig(ConfigProg configProg)
    {
        var defaultConfig = defaultProgCopnfig.CreateDefaultConfig();
        var deltas = configDelta.RetrieveTextsForProgDeltas(defaultConfig, configProg);
        return configWriter.WriteConfigDeltasProg(deltas);
    }


    /// <inheritdoc/>
    public AstroConfig ReadCurrentConfig()
    {
        AstroConfig actualConfig;
        try
        {
            var defaultConfig = config.CreateDefaultConfig();
            var deltas = configReader.ReadDeltasForConfig();
            actualConfig = configCreator.CreateActualConfig(defaultConfig, deltas);
        }
        catch (Exception e)
        {
            Log.Error($"Could not read actualconfigz: {e.Message}");
            throw;
        }
        return actualConfig;
    }

    /// <inheritdoc/>
    public ConfigProg ReadCurrentConfigProg()
    {
        var defaultConfig = defaultProgCopnfig.CreateDefaultConfig();
        var deltas = configReader.ReadDeltasForConfigProg();
        return configCreator.CreateActualProgConfig(defaultConfig, deltas);
    }
}