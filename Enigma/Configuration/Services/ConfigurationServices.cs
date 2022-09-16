// Jan Kampherbeek, (c) 2022.
// Enigma Research is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Enigma.Configuration.Handlers;
using Enigma.Configuration.Parsers;
using Enigma.Persistency.Parsers;
using Microsoft.Extensions.DependencyInjection;


namespace Enigma.Configuration.Services;

public static class ConfigurationServices
{
    public static void RegisterConfigurationServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAstroConfigParser, AstroConfigParser>();
        serviceCollection.AddTransient<IConfigReader, ConfigReader>();
        serviceCollection.AddTransient<IConfigWriter, ConfigWriter>();
        serviceCollection.AddTransient<IDefaultConfiguration, DefaultConfiguration>();
    }
}