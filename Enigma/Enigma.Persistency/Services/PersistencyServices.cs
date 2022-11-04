// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Persistency.Converters;
using Enigma.Persistency.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace Enigma.Persistency.Services;

public static class PersistencyServices
{
    public static void RegisterPersistencyServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IDateCheckedConversion, DateCheckedConversion>();
        serviceCollection.AddTransient<ILocationCheckedConversion, LocationCheckedConversion>();
        serviceCollection.AddTransient<ITimeCheckedConversion, TimeCheckedConversion>();

    }
}