// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.State;


/// <summary>Central vault for calculated charts and other data.</summary>
/// <remarks>Implemented as singleton, based on code by Jon Skeet: https://csharpindepth.com/articles/singleton .</remarks>
public sealed class CurrentConfig
{
    private static readonly CurrentConfig instance = new();


    private AstroConfig? _currentConfig;

    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static CurrentConfig()
    {
    }

    private CurrentConfig()
    {
    }

    public static CurrentConfig Instance => instance;

    public void ChangeConfig(AstroConfig newConfig)
    {
        _currentConfig = newConfig;
    }


    public AstroConfig GetConfig()
    {
        if (_currentConfig != null) return _currentConfig;
        IConfigReader configReader = App.ServiceProvider.GetRequiredService<IConfigReader>();   // TODO 0.2 use API to read config
        _currentConfig = configReader.ReadConfig();
        return _currentConfig;
    }

}


