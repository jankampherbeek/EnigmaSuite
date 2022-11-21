// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Helpers.Analysis.Aspects;
using Enigma.Core.Helpers.Conversions;
using Enigma.Core.Helpers.Interfaces;
using Enigma.Core.Helpers.Persistency;
using Enigma.Core.Helpers.Research;
using Enigma.Domain.Analysis;
using Enigma.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Core.Helpers.Services;


/// <summary>
/// Definitions for Dependency Injection for classes and interfaces for persistency..
/// </summary>

public static class HelperServices
{
    public static void RegisterHelperServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAnalysisPointsMapping, AnalysisPointsMapping>();
        serviceCollection.AddTransient<IAspectChecker, AspectChecker>();
        serviceCollection.AddTransient<IAspectOrbConstructor, AspectOrbConstructor>();
        serviceCollection.AddTransient<IControlDataCalendar, ControlDataCalendar>();
        serviceCollection.AddTransient<IControlGroupCreator, StandardShiftControlGroupCreator>();
        serviceCollection.AddTransient<IControlGroupRng, ControlGroupRng>();
        serviceCollection.AddTransient<ICsv2JsonConverter, Csv2JsonConverter>();
        serviceCollection.AddTransient<IDataFilePreparator, DataFilePreparator>();
        serviceCollection.AddTransient<IFileCopier, FileCopier>();
        serviceCollection.AddTransient<IFoldersInfo, FoldersInfo>();
        serviceCollection.AddTransient<IInputDataConverter, InputDataConverter>();
        serviceCollection.AddTransient<IMundanePointToAnalysisPointMap, MundanePointToAnalysisPointMap>();
        serviceCollection.AddTransient<IProjectDetails, ProjectDetails>();
        serviceCollection.AddTransient<IResearchProjectParser, ResearchProjectParser>();
        serviceCollection.AddTransient<ISolSysPointToAnalysisPointMap, SolSysPointToAnalysisPointMap>();
        serviceCollection.AddTransient<ITextFileReader, TextFileReader>();
        serviceCollection.AddTransient<ITextFileWriter, TextFileWriter>();

    }
}