// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

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
    
    /// <summary>Check existence of a configuration file.</summary>
    /// <returns>True if the file exists, otherwise false.</returns>
    public bool DoesConfigExist();

    /// <summary>Check existence of a configuration file for progressions.</summary>
    /// <returns>True if the file exists, otherwise false.</returns>
    public bool DoesProgConfigExist();
    
    /// <summary>Write configuration to file.</summary>
    /// <param name="astroConfig">The configuration to save.</param>
    /// <returns>True if no error occurred, otherwise false.</returns>
    public bool WriteConfig(AstroConfig astroConfig);

    /// <summary>Write progressive configuration to file.</summary>
    /// <param name="configProg">The progressive configuration to save.</param>
    /// <returns>True if no error occurred, otherwise false.</returns>
    public bool WriteConfig(ConfigProg configProg);
    
    /// <summary>Write default configuration to file.</summary>
    /// <returns>True if no error occurred, otherwise false.</returns>
    public bool WriteDefaultConfig();

    /// <summary>Write default configuration for progressions to file.</summary>
    /// <returns>True if no error occurred, otherwise false.</returns>
    public bool WriteDefaultProgConfig();
    
    /// <summary>Read configuration from file.</summary>
    /// <returns>Configuration as read from file.</returns>
    public AstroConfig ReadConfig();

    /// <summary>Read progressive configuration from file.</summary>
    /// <returns>Progressive configuration as read from file.</returns>
    public ConfigProg ReadConfigProg();
    
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
    /// <summary>Parse config: from object to Json.</summary>
    /// <param name="astroConfig">The configuration.</param>
    /// <returns>The Json.</returns>
    public string MarshallConfig(AstroConfig astroConfig);

    /// <summary>Parse progressive config: from object to Json.</summary>
    /// <param name="configProg">The progressive configuration.</param>
    /// <returns>The Json.</returns>
    public string MarshallConfig(ConfigProg configProg);
    
    /// <summary>Parse config: from Json to object.</summary>
    /// <param name="jsonString">The Json.</param>
    /// <returns>The configuration.</returns>
    public AstroConfig UnMarshallAstroConfig(string jsonString);
    
    /// <summary>Parse progressive config: from Json to object.</summary>
    /// <param name="jsonString">The Json.</param>
    /// <returns>The configuration.</returns>
    public ConfigProg UnMarshallConfigProg(string jsonString);
}

