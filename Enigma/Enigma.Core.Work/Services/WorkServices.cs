// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Api.Calc;
using Enigma.Configuration.Handlers;
using Enigma.Configuration.Parsers;
using Enigma.Core.Work.Analysis.Harmonics;
using Enigma.Core.Work.Analysis.Interfaces;
using Enigma.Core.Work.Analysis.Midpoints;
using Enigma.Core.Work.Calc.CelestialPoints;
using Enigma.Core.Work.Calc.Coordinates;
using Enigma.Core.Work.Calc.DateTime;
using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Core.Work.Calc.Specials;
using Enigma.Core.Work.Calc.Util;
using Enigma.Core.Work.Configuration;
using Enigma.Core.Work.Configuration.Interfaces;
using Enigma.Core.Work.Conversions;
using Enigma.Core.Work.Persistency;
using Enigma.Core.Work.Persistency.Interfaces;
using Enigma.Core.Work.Research;
using Enigma.Core.Work.Research.Interfaces;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Core.Work.Services;


/// <summary>
/// Definitions for Dependency Injection for classes and interfaces for enigma.core.work.
/// </summary>

public static class WorkServices
{
    public static void RegisterWorkServices(this ServiceCollection serviceCollection)
    {


        serviceCollection.AddTransient<IAnalysisPointsForMidpoints, AnalysisPointsForMidpoints>();
        serviceCollection.AddTransient<IAnalysisPointsMapping, AnalysisPointsMapping>();
        serviceCollection.AddTransient<IAstroConfigParser, AstroConfigParser>();
        serviceCollection.AddTransient<IBaseMidpointsCreator, BaseMidpointsCreator>();
        serviceCollection.AddTransient<ICalcHelioPos, CalcHelioPos>();
        serviceCollection.AddTransient<IDateTimeValidator, DateTimeValidator>();
        serviceCollection.AddTransient<IConfigReader, ConfigReader>();
        serviceCollection.AddTransient<IConfigWriter, ConfigWriter>();
        serviceCollection.AddTransient<IControlDataCalendar, ControlDataCalendar>();
        serviceCollection.AddTransient<IControlGroupCreator, StandardShiftControlGroupCreator>();
        serviceCollection.AddTransient<IControlGroupRng, ControlGroupRng>();
        serviceCollection.AddTransient<ICoordinateConversionCalc, CoordinateConversionCalc>();
        serviceCollection.AddTransient<ICsv2JsonConverter, Csv2JsonConverter>();
        serviceCollection.AddTransient<IDateCheckedConversion, DateCheckedConversion>();
        serviceCollection.AddTransient<IDataFilePreparator, DataFilePreparator>();
        serviceCollection.AddTransient<IDateTimeCalc, DateTimeCalc>();
        serviceCollection.AddTransient<IDefaultConfiguration, DefaultConfiguration>();
        serviceCollection.AddTransient<IFileCopier, FileCopier>();
        serviceCollection.AddTransient<IFoldersInfo, FoldersInfo>();
        serviceCollection.AddTransient<IHarmonicsCalculator, HarmonicsCalculator>();
        serviceCollection.AddTransient<IHorizontalCalc, HorizontalCalc>();
        serviceCollection.AddTransient<IHousesCalc, HousesCalc>();
        serviceCollection.AddTransient<IInputDataConverter, InputDataConverter>();
        serviceCollection.AddTransient<IJulDayCalc, JulDayCalc>();
        serviceCollection.AddTransient<ILocationCheckedConversion, LocationCheckedConversion>();
        serviceCollection.AddTransient<IMundanePointToAnalysisPointMap, MundanePointToAnalysisPointMap>();
        serviceCollection.AddTransient<IObliquityCalc, ObliquityCalc>();
        serviceCollection.AddTransient<IOccupiedMidpoints, OccupiedMidpoints>();
        serviceCollection.AddTransient<IProjectDetails, ProjectDetails>();
        serviceCollection.AddTransient<IResearchPaths, ResearchPaths>();
        serviceCollection.AddTransient<IResearchProjectParser, ResearchProjectParser>();
        serviceCollection.AddTransient<ISeFlags, SeFlags>();
        serviceCollection.AddTransient<ICelPointSECalc, CelPointSECalc>();
        serviceCollection.AddTransient<ICelPointsElementsCalc, CelPointsElementsCalc>();
        serviceCollection.AddTransient<ICelPointToAnalysisPointMap, CelPointToAnalysisPointMap>();
        serviceCollection.AddTransient<ITextFileReader, TextFileReader>();
        serviceCollection.AddTransient<ITextFileWriter, TextFileWriter>();
        serviceCollection.AddTransient<ITimeCheckedConversion, TimeCheckedConversion>();


    }
}
