// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Api.Interfaces;

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