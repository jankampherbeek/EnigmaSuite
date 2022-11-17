// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Enigma.Core.Handlers.Analysis;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Persistency;
using Enigma.Core.Handlers.Research;
using Enigma.Core.Helpers.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Core.Handlers.Services;


/// <summary>
/// Definitions for Dependency Injection for classes and interfaces for persistency..
/// </summary>

public static class HandlerServices
{
    public static void RegisterHandlerServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAspectsHandler, AspectsHandler>();
        serviceCollection.AddTransient<IDataFilePreparationHandler, DataFilePreparationHandler>();
        serviceCollection.AddTransient<IDataImportHandler, DataImportHandler>();
        serviceCollection.AddTransient<IDataNamesHandler, DataNamesHandler>();
        serviceCollection.AddTransient<MidpointsHandler, MidpointsHandler>();
        serviceCollection.AddTransient<IProjectCreationHandler, ProjectCreationHandler>();
        serviceCollection.AddTransient<IProjectsOverviewHandler, ProjectsOverviewHandler>();

        serviceCollection.RegisterHelperServices();
    }



}