// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Enigma.Domain.Configuration;

namespace Enigma.Core.Handlers.Interfaces;

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