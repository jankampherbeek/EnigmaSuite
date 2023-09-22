// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Serilog;

namespace Enigma.Api;

/// <inheritdoc/>
public sealed class ConfigurationApi : IConfigurationApi
{
    private readonly IConfigurationHandler _handler;

    public ConfigurationApi(IConfigurationHandler handler) => _handler = handler;

    /// <inheritdoc/>
    public AstroConfig GetDefaultConfiguration()
    {
        Log.Information("ConfigurationApi GetDefaultConfiguration");
        return _handler.ConstructDefaultConfiguration();
    }

    /// <inheritdoc/>
    public bool DoesConfigExist()
    {
        Log.Information("ConfigurationApi DoesConfigExist");
        return _handler.DoesConfigExist();
    }

    /// <inheritdoc/>
    public bool WriteConfig(AstroConfig config)
    {
        Guard.Against.Null(config);
        Log.Information("ConfigurationApi WriteConfig");
        return _handler.WriteConfig(config);
    }

}


