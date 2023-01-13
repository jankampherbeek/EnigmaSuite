// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;

namespace Enigma.Core.Handlers.Configuration.Interfaces;

/// <summary>Handler for configurations.</summary>
public interface IConfigurationHandler
{

    /// <summary>Defines a default configuration.</summary>
    /// <returns>Default configuration.</returns>
    public AstroConfig ConstructDefaultConfiguration();

    /// <summary>Check existence of a configuration file.</summary>
    /// <returns>True if the file exists, otherwise false.</returns>
    public bool DoesConfigExist();

    /// <summary>Write configuration to file.</summary>
    /// <param name="astroConfig">The configuration to save.</param>
    /// <returns>True if no error occurred, otherwise false.</returns>
    public bool WriteConfig(AstroConfig astroConfig);

    /// <summary>Write default configuration to file.</summary>
    /// <returns>True if no error occurred, otherwise false.</returns>
    public bool WriteDefaultConfig();

    /// <summary>Read configuration from file.</summary>
    /// <returns>Configuration as read from file.</returns>
    public AstroConfig ReadConfig();

}


/// <summary>Create default configuration.</summary>
public interface IDefaultConfiguration
{
    /// <returns>Default configuration.</returns>
    public AstroConfig CreateDefaultConfig();
}

/// <summary>Parser for a configuration.</summary>
public interface IAstroConfigParser
{
    /// <summary>Parse config: from object to Json.</summary>
    /// <param name="astroConfig">The configuration.</param>
    /// <returns>The Json.</returns>
    public string Marshall(AstroConfig astroConfig);

    /// <summary>Parse config: from Json to object.</summary>
    /// <param name="jsonString">The Json.</param>
    /// <returns>The configuration.</returns>
    public AstroConfig UnMarshall(string jsonString);
}

/// <summary>Handles writing a configuration to a disk file.</summary>
public interface IConfigWriter
{
    /// <param name="astroConfig">The configuration to write.</param>
    /// <returns>True if the write was succesfull, otherwise false.</returns>
    public bool WriteConfig(AstroConfig astroConfig);
}


/// <summary>Handles reading a configuration from a disk file.</summary>
public interface IConfigReader
{
    /// <summary>Reads a configuration from a disk file.</summary>
    /// <returns>The configuration that was read.</returns>
    public AstroConfig ReadConfig();
}

