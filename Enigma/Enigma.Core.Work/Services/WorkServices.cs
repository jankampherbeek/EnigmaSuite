// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Helpers.Conversions;
using Enigma.Core.Work.Analysis.Harmonics;
using Enigma.Core.Work.Analysis.Midpoints;
using Enigma.Core.Work.Analysis.Midpoints.Interfaces;
using Enigma.Core.Work.Research;
using Enigma.Core.Work.Research.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Core.Work.Services;


/// <summary>
/// Definitions for Dependency Injection for classes and interfaces for enigma.core.work.
/// </summary>

public static class WorkServices
{
    public static void RegisterWorkServices(this ServiceCollection serviceCollection)
    {
        // Handlers
        serviceCollection.AddTransient<IHarmonicsHandler, HarmonicsHandler>();
        serviceCollection.AddTransient<IMidpointsHandler, MidpointsHandler>();

        // Additional classes
        serviceCollection.AddTransient<IAnalysisPointsForMidpoints, AnalysisPointsForMidpoints>();
        serviceCollection.AddTransient<IBaseMidpointsCreator, BaseMidpointsCreator>();
        serviceCollection.AddTransient<IControlDataCalendar, ControlDataCalendar>();
        serviceCollection.AddTransient<IControlGroupCreator, StandardShiftControlGroupCreator>();
        serviceCollection.AddTransient<IControlGroupRng, ControlGroupRng>();

        serviceCollection.AddTransient<IHarmonicsCalculator, HarmonicsCalculator>();
        serviceCollection.AddTransient<IInputDataConverter, InputDataConverter>();
        serviceCollection.AddTransient<IOccupiedMidpoints, OccupiedMidpoints>();
        serviceCollection.AddTransient<IProjectCreationHandler, ProjectCreationHandler>();
        serviceCollection.AddTransient<IProjectDetails, ProjectDetails>();
        serviceCollection.AddTransient<IProjectsOverviewHandler, ProjectsOverviewHandler>();
        serviceCollection.AddTransient<IResearchProjectParser, ResearchProjectParser>();
    }
}
 