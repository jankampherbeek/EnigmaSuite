// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Services;
using Enigma.Core.Calc.DateTime.CheckDateTime;
using Enigma.Core.Calc.Interfaces;
using Enigma.Core.Calc.SeFacades;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;
using Enigma.Frontend.Ui.Charts;
using Enigma.Frontend.Ui.Charts.Graphics;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.ResearchProjects;
using Enigma.Frontend.Ui.Settings;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Windows;
using Enigma.Frontend.Ui.Datafiles;
using Enigma.Frontend.Ui.DataFiles;

namespace Enigma.Frontend.Ui;


public partial class App : Application
{
    public static ServiceProvider ServiceProvider { get; private set; } = HandleRegistrationForDI();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        DefineLogging();
        Log.Information("********************** Enigma starting ***********************");
        string pathToSeFiles = @"c:\sweph";                    // TODO make path to SE files configurable
        SeInitializer.SetEphePath(pathToSeFiles);
        Log.Information("Using path to SE: {path}.", pathToSeFiles);
    }

    protected static ServiceProvider HandleRegistrationForDI()
    {
        var serviceCollection = new ServiceCollection();

        // Handle services from project Enigma.Frontend.
        serviceCollection.AddTransient<AppSettingsController>();
        serviceCollection.AddTransient<IAspectForDataGridFactory, AspectForDataGridFactory>();
        serviceCollection.AddTransient<IAspectForWheelFactory, AspectForWheelFactory>();
        serviceCollection.AddTransient<AstroConfigController>();
        serviceCollection.AddTransient<ICelPointForDataGridFactory, CelPointForDataGridFactory>();
        serviceCollection.AddTransient<ChartAspectsWindow>();
        serviceCollection.AddTransient<ChartAspectsController>();
        serviceCollection.AddTransient<ChartDataInputController>();
        serviceCollection.AddTransient<ChartPositionsWindow>();
        serviceCollection.AddTransient<ChartPositionsController>();
        serviceCollection.AddTransient<IChartsEnumFacade, ChartsEnumFacade>();
        serviceCollection.AddTransient<ChartsMainController>();
        serviceCollection.AddTransient<ChartsWheel>();
        serviceCollection.AddTransient<IChartsWheelAspects, ChartsWheelAspects>();
        serviceCollection.AddTransient<IChartsWheelCircles, ChartsWheelCircles>();
        serviceCollection.AddTransient<ChartsWheelController>();
        serviceCollection.AddTransient<IChartsWheelCusps, ChartsWheelCusps>();
        serviceCollection.AddTransient<ChartsWheelMetrics>();
        serviceCollection.AddTransient<IChartsWheelSigns, ChartsWheelSigns>();
        serviceCollection.AddTransient<IChartsWheelSolSysPoints, ChartsWheelSolSysPoints>();
        serviceCollection.AddTransient<DataFilesExportController>();
        serviceCollection.AddTransient<DataFilesImportController>();
        serviceCollection.AddTransient<DataFilesOverviewController>();
        serviceCollection.AddTransient<IDataNameForDataGridFactory, DataNameForDataGridFactory>();
        serviceCollection.AddSingleton<MainController>();
        serviceCollection.AddTransient<MainWindow>();
        serviceCollection.AddSingleton<ICheckDateTimeHandler, CheckDateTimeHandler>();
        serviceCollection.AddSingleton<ICheckDateTimeValidator, CheckDateTimeValidator>();
        serviceCollection.AddTransient<ICurrentCharts, CurrentCharts>();
        serviceCollection.AddTransient<HelpWindow>();
        serviceCollection.AddSingleton<IHousePosForDataGridFactory, HousePosForDataGridFactory>();
        serviceCollection.AddTransient<ProjectInputController>();
        serviceCollection.AddTransient<ProjectsOverviewController>();
        serviceCollection.AddTransient<IRangeCheck, RangeCheck>();
        serviceCollection.AddTransient<IRosetta, Rosetta>();
        serviceCollection.AddTransient<ISortedGraphicSolSysPointsFactory, SortedGraphicSolSysPointsFactory>();
        serviceCollection.AddTransient<ITextFileReaderFE, TextFileReader>();
        serviceCollection.AddSingleton<ITimeZoneSpecifications, TimeZoneSpecifications>();

        // Handle services from other projects.
        serviceCollection.RegisterApiServices();

        return serviceCollection.BuildServiceProvider(true);
    }

    public static void DefineLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File("logs\\enigma_.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}

