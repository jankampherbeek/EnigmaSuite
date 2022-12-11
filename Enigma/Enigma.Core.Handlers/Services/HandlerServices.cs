// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Core.Handlers.Calc.CelestialPoints;
using Enigma.Core.Calc;
using Enigma.Core.Handlers.Analysis;
using Enigma.Core.Handlers.Calc;
using Enigma.Core.Handlers.Calc.Celestialpoints;
using Enigma.Core.Handlers.Calc.CelestialPoints.ObliqueLongitude;
using Enigma.Core.Handlers.Calc.Coordinates;
using Enigma.Core.Handlers.Calc.DateTime;
using Enigma.Core.Handlers.Calc.Specials;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Persistency;
using Enigma.Core.Handlers.Research;
using Enigma.Core.Work.Handlers.Calc.CelestialPoints;
using Enigma.Core.Work.Research;
using Enigma.Core.Work.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Core.Handlers.Services;


/// <summary>
/// Definitions for Dependency Injection for classes and interfaces for handlers.
/// </summary>

public static class HandlerServices
{
    public static void RegisterHandlerServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAspectsHandler, AspectsHandler>();
        serviceCollection.AddTransient<ICalcChartsRangeHandler, CalcChartsRangeHandler>();
        serviceCollection.AddTransient<IChartAllPositionsHandler, ChartAllPositionsHandler>();
        serviceCollection.AddTransient<IConfigurationHandler, ConfigurationHandler>();
        serviceCollection.AddTransient<ICoordinateConversionHandler, CoordinateConversionHandler>();
        serviceCollection.AddTransient<IDataFilePreparationHandler, DataFilePreparationHandler>();
        serviceCollection.AddTransient<IDataImportHandler, DataImportHandler>();
        serviceCollection.AddTransient<IDataNamesHandler, DataNamesHandler>();
        serviceCollection.AddTransient<IDateTimeHandler, DateTimeHandler>();
        serviceCollection.AddTransient<IFilePersistencyHandler, FilePersistencyHandler>();
        serviceCollection.AddTransient<IHarmonicsHandler, HarmonicsHandler>();
        serviceCollection.AddTransient<IHorizontalHandler, HorizontalHandler>();
        serviceCollection.AddTransient<IHousesHandler, HousesHandler>();
        serviceCollection.AddTransient<IJulDayHandler, JulDayHandler>();
        serviceCollection.AddTransient<IMidpointsHandler, MidpointsHandler>();
        serviceCollection.AddTransient<IObliqueLongitudeHandler, ObliqueLongitudeHandler>();
        serviceCollection.AddTransient<IObliquityHandler, ObliquityHandler>();
        serviceCollection.AddTransient<IProjectCreationHandler, ProjectCreationHandler>();
        serviceCollection.AddTransient<IProjectsOverviewHandler, ProjectsOverviewHandler>();
        serviceCollection.AddTransient<IResearchDataHandler, ResearchDataHandler>();
        serviceCollection.AddTransient<ITestMethodHandler, TestMethodHandler>();
        serviceCollection.AddTransient<ISeHandler, SeHandler>();
        serviceCollection.AddTransient<ICelPointsHandler, CelPointsHandler>();


        serviceCollection.RegisterWorkServices();


    }



}