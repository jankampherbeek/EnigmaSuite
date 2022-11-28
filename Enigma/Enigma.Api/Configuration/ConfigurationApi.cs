// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Configuration;

namespace Enigma.Api.Configuration;

/// <inheritdoc/>
public class ConfigurationApi : IConfigurationApi
{
    private readonly IConfigurationHandler _handler;

    public ConfigurationApi(IConfigurationHandler handler)
    {
        _handler = handler;
    }

    public AstroConfig GetDefaultConfiguration()
    {
        return _handler.ConstructDefaultConfiguration();
    }

    public bool DoesConfigExist()
    {
        return _handler.DoesConfigExist();
    }

    public bool WriteConfig(AstroConfig config)
    {
        return _handler.WriteConfig(config);
    }

}


