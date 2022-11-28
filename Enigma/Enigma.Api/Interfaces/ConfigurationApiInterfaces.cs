// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;


/// <summary>Interface for configurations.</summary>
public interface IConfigurationApi
{
    /// <returns>Default configuration.</returns>
    public AstroConfig GetDefaultConfiguration();

    /// <returns>True if configuration file exists, otherwise false.</returns>
    public bool DoesConfigExist();

    /// <summary>Save configuration.</summary>
    /// <returns>True if save was successful, otherwise false.</returns>
    public bool WriteConfig(AstroConfig config);
}