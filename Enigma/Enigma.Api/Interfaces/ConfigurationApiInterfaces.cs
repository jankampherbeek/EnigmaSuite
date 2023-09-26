// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Api.Interfaces;

/// <summary>Interface for configurations.</summary>
public interface IConfigurationApi
{
    /// <returns>Default configuration.</returns>
    public AstroConfig GetDefaultConfiguration();

    /// <returns>Default progressive configuration.</returns>
    public ConfigProg GetDefaultProgConfiguration();
    
    /// <returns>True if configuration file exists, otherwise false.</returns>
    public bool DoesConfigExist();

    /// <returns>True if progressive configuration file exists, otherwise false.</returns>
    public bool DoesConfigProgExist();
    
    /// <summary>Save configuration.</summary>
    /// <returns>True if save was successful, otherwise false.</returns>
    public bool WriteConfig(AstroConfig config);
    
    /// <summary>Save preogressive configuration.</summary>
    /// <returns>True if save was successful, otherwise false.</returns>
    public bool WriteConfig(ConfigProg config);
}