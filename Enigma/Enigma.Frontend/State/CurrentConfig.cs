// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.State;


/// <summary>Central vault for calculated charts and other data.</summary>
/// <remarks>Implemented as singleton, based on code by Jon Skeet: https://csharpindepth.com/articles/singleton .</remarks>
public sealed class CurrentConfig
{
    private static readonly CurrentConfig instance = new();


    private AstroConfig? _currentConfig;
    private ConfigProg? _currentConfigProg;    

    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static CurrentConfig()
    {
    }

    private CurrentConfig()
    {
    }

    // ReSharper disable once ConvertToAutoProperty
    public static CurrentConfig Instance => instance;       // instance is singleton

    public void ChangeConfig(AstroConfig newConfig)
    {
        _currentConfig = newConfig;
    }

    public void ChangeConfigProg(ConfigProg newConfig)
    {
        _currentConfigProg = newConfig;
    }

    public AstroConfig GetConfig()
    {
        IConfigurationApi configApi = App.ServiceProvider.GetRequiredService<IConfigurationApi>();
        Log.Information("CurrentConfig.GetConfig(): requesting config from config api");
        return _currentConfig ?? configApi.GetCurrentConfiguration();
    }

    public ConfigProg GetConfigProg()
    {
        IConfigurationApi configApi = App.ServiceProvider.GetRequiredService<IConfigurationApi>();
        return _currentConfigProg ?? configApi.GetCurrentProgConfiguration();
    }
    
}


