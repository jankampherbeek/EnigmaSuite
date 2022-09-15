// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Persistency.FileHandling;
using Microsoft.Extensions.DependencyInjection;


namespace Enigma.Persistency.Services;

public static class PersistencyServices
{
    public static void RegisterPersistencyServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ITextFileReader, TextFileReader>();
        serviceCollection.AddTransient<ITextFileWriter, TextFileWriter>();

    }
}