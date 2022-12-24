// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Research.Helpers;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Core.Work.Research;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Core.Handlers.Research.Services;


/// <summary>
/// Definitions for Dependency Injection for classes and interfaces. Contains handlers and helpers for research functionality.
/// </summary>

public static class ResearchServices
{
    public static void RegisterResearchServices(this ServiceCollection serviceCollection)
    {
        // Handlers
        serviceCollection.AddTransient<IProjectCreationHandler, ProjectCreationHandler>();
        serviceCollection.AddTransient<IProjectsOverviewHandler, ProjectsOverviewHandler>();
        serviceCollection.AddTransient<IResearchDataHandler, ResearchDataHandler>();
        serviceCollection.AddTransient<IResearchMethodHandler, ResearchMethodHandler>();
        serviceCollection.AddTransient<IResearchPathHandler, ResearchPathHandler>();

        // Helpers
        serviceCollection.AddTransient<ICalculatedResearchPositions, CalculatedResearchPositions>();
        serviceCollection.AddTransient<IPointsInZodiacPartsCounting, PointsInZodiacPartsCounting>();
    }



}