// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Research.Helpers;
using Enigma.Core.Handlers.Research.Interfaces;
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
        serviceCollection.AddTransient<IAspectsCounting, AspectsCounting>();
        serviceCollection.AddTransient<ICalculatedResearchPositions, CalculatedResearchPositions>();
        serviceCollection.AddTransient<IControlDataCalendar, ControlDataCalendar>();
        serviceCollection.AddTransient<IControlGroupCreator, StandardShiftControlGroupCreator>();
        serviceCollection.AddTransient<IControlGroupRng, ControlGroupRng>();
        serviceCollection.AddTransient<IInputDataConverter, InputDataConverter>();
        serviceCollection.AddTransient<IPointsInPartsCounting, PointsInPartsCounting>();
        serviceCollection.AddTransient<IProjectDetails, ProjectDetails>();
        serviceCollection.AddTransient<IResearchPaths, ResearchPaths>();
        serviceCollection.AddTransient<IResearchProjectParser, ResearchProjectParser>();
        serviceCollection.AddTransient<IUnaspectedCounting, UnaspectedCounting>();


    }



}