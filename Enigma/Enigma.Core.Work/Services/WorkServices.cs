// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Work.Analysis.Midpoints;
using Enigma.Core.Work.Analysis.Midpoints.Interfaces;
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
        serviceCollection.AddTransient<IMidpointsHandler, MidpointsHandler>();

        // Additional classes
        serviceCollection.AddTransient<IAnalysisPointsForMidpoints, AnalysisPointsForMidpoints>();
        serviceCollection.AddTransient<IBaseMidpointsCreator, BaseMidpointsCreator>();
        serviceCollection.AddTransient<IOccupiedMidpoints, OccupiedMidpoints>();
    }
}
 