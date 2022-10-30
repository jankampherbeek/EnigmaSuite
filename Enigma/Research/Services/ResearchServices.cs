// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Research.ControlGroups;
using Enigma.Research.Handlers;
using Enigma.Research.Interfaces;
using Enigma.Research.Parsers;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Research.Services;

public static class ResearchServices
{
    public static void RegisterResearchServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IControlDataCalendar, ControlDataCalendar>();
        serviceCollection.AddTransient<IControlGroupCreator, StandardShiftControlGroupCreator>();
        serviceCollection.AddTransient<IProjectCreationHandler, ProjectCreationHandler>();
        serviceCollection.AddTransient<IResearchProjectParser, ResearchProjectParser>();
    }
}

