// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Interfaces;

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


/// <summary>Create default configuration for radix.</summary>
public interface IDefaultConfiguration
{
    /// <returns>Default configuration for radix.</returns>
    public AstroConfig CreateDefaultConfig();
}

/// <summary>Create default configuration for progressions.</summary>
public interface IDefaultProgConfiguration
{
    /// <returns>Default configuration for progressions.</returns>
    public ConfigProg CreateDefaultConfig();
}


/// <summary>Parser for configurations.</summary>
public interface IConfigParser
{

    /// <summary>Parse deltas for config: from Dictionary to Json. Supports standard config and prog config.</summary>
    /// <param name="deltas">Deltas for the configuration.</param>
    /// <returns>The Json.</returns>
    public string MarshallDeltasForConfig(Dictionary<string, string> deltas);
    
    /// <summary>Parse deltas for config: from json to dictionary. Supports standard config and prog config.</summary>
    /// <param name="jsonString">The Json.</param>
    /// <returns>The deltas.</returns>
    public Dictionary<string, string> UnMarshallDeltasForConfig(string jsonString);

}

/// <summary>Handles the delta's between the default configuration and an updated configuration.</summary>
public interface IConfigurationDelta
{
    /// <summary>Create a dictionary with delta's that can be saved as the configuration delta file.</summary>
    /// <param name="defaultConfig">The default config.</param>
    /// <param name="updatedConfig">The config with the updates.</param>
    /// <returns>Dictionary with key values for configuration delta's.</returns>
    public Dictionary<string, string> RetrieveTextsForDeltas(AstroConfig defaultConfig, AstroConfig updatedConfig);

    /// <summary>Create a dictionary with delta's that can be saved as the configuration delta file for progressions.</summary>
    /// <param name="defaultProgConfig">The default config,</param>
    /// <param name="updatedProgConfig">The config with the updates.</param>
    /// <returns>Dictionary with key values for progressive configuration delta's.</returns>
    public Dictionary<string, string> RetrieveTextsForProgDeltas(ConfigProg defaultProgConfig,
        ConfigProg updatedProgConfig);
}

/// <summary>Helper for creating texts that describe the delta in a configuration.</summary>
public interface IDeltaTexts
{
    /// <summary>Handle the delta for a chart point.</summary>
    /// <param name="point">The chart point.</param>
    /// <param name="pointSpecs">Specifications for the updated chart point.</param>
    /// <returns>A key and value for the delta of a chart point.</returns>
    public Tuple<string, string> CreateDeltaForPoint(ChartPoints point, ChartPointConfigSpecs? pointSpecs);
    
    /// <summary>Handle the delta for an aspect.</summary>
    /// <param name="aspect">The aspect.</param>
    /// <param name="aspectSpecs">Specifications for the updated aspect.</param>
    /// <returns>A key and value for the delta of an aspect.</returns>
    public Tuple<string, string> CreateDeltaForAspect(AspectTypes aspect, AspectConfigSpecs? aspectSpecs);

    /// <summary>Handle the delta for a chartpoint for a specific progression method.</summary>
    /// <param name="progresMethod">The progression method.</param>
    /// <param name="point">The chart point.</param>
    /// <param name="pointSpecs">Specifications for the chart point.</param>
    /// <returns>A key and value for the delta of a chart point.</returns>
    public Tuple<string, string> CreateDeltaForProgChartPoint(ProgresMethods progresMethod, ChartPoints point,
        ProgPointConfigSpecs? pointSpecs);

    /// <summary>Handle the delta for a progressive orb.</summary>
    /// <param name="progresMethod">The progression method.</param>
    /// <param name="orb">Value for the orb.</param>
    /// <returns>A key and value for the orb.</returns>
    public Tuple<string, string> CreateDeltaForProgOrb(ProgresMethods progresMethod, double orb);

    /// <summary>Handle the delta for a timekey for symbolic directions.</summary>
    /// <param name="key">The key</param>
    /// <returns>Key and value for the timekey.</returns>
    public Tuple<string, string> CreateDeltaForProgSymKey(SymbolicKeys timeKey);

}

/// <summary>Create actual configuration.</summary>
public interface IActualConfigCreator
{
    /// <summary>Combine default configuration and deltas into actual configuration.</summary>
    /// <param name="defaultConfig">The default configuration.</param>
    /// <param name="deltas">Dictionary with the deltas.</param>
    /// <returns>Actual config.</returns>
    public AstroConfig CreateActualConfig(AstroConfig defaultConfig, Dictionary<string, string> deltas);

    /// <summary>Combine default configuration for progressions and deltas into actual configuration for progressions.</summary>
    /// <param name="defaultConfig">The default configuration.</param>
    /// <param name="deltas">Dictionary with the deltas.</param>
    /// <returns>Actual config for progressions.</returns>
    public ConfigProg CreateActualProgConfig(ConfigProg defaultConfig, Dictionary<string, string> deltas);
}