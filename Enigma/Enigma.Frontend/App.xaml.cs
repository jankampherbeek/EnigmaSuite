// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Calc;
using Enigma.Api.Interfaces;
using Enigma.Api.Services;
using Enigma.Core.Work.Calc.DateTime;
using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Domain.Charts;
using Enigma.Domain.Interfaces;
using Enigma.Frontend.Helpers.Services;
using Enigma.Frontend.Ui.Charts;
using Enigma.Frontend.Ui.Charts.Graphics;
using Enigma.Frontend.Ui.Datafiles;
using Enigma.Frontend.Ui.DataFiles;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.ResearchProjects;
using Enigma.Frontend.Ui.Settings;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Windows;

namespace Enigma.Frontend.Ui;


public partial class App : Application
{
    public static ServiceProvider ServiceProvider { get; private set; } = HandleRegistrationForDI();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        DefineLogging();
        Log.Information("********************** Enigma starting ***********************");

        ISeApi seApi = ServiceProvider.GetRequiredService<ISeApi>();

        string pathToSeFiles = @"c:\sweph";                    // TODO make path to SE files configurable
        seApi.SetupSe(pathToSeFiles);
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
        serviceCollection.AddTransient<ChartAspectsController>();
        serviceCollection.AddTransient<ChartAspectsWindow>();
        serviceCollection.AddTransient<ChartDataInputController>();
        serviceCollection.AddTransient<ChartHarmonicsController>();
        serviceCollection.AddTransient<ChartHarmonicsWindow>();
        serviceCollection.AddTransient<ChartMidpointsController>();
        serviceCollection.AddTransient<ChartMidpointsWindow>();
        serviceCollection.AddTransient<ChartPositionsWindow>();
        serviceCollection.AddTransient<ChartPositionsController>();
        serviceCollection.AddTransient<ChartsMainController>();
        serviceCollection.AddTransient<ChartsWheel>();
        serviceCollection.AddTransient<IChartsWheelAspects, ChartsWheelAspects>();
        serviceCollection.AddTransient<IChartsWheelCircles, ChartsWheelCircles>();
        serviceCollection.AddTransient<ChartsWheelController>();
        serviceCollection.AddTransient<IChartsWheelCusps, ChartsWheelCusps>();
        serviceCollection.AddTransient<ChartsWheelMetrics>();
        serviceCollection.AddTransient<IChartsWheelSigns, ChartsWheelSigns>();
        serviceCollection.AddTransient<IChartsWheelCelPoints, ChartsWheelCelPoints>();
        serviceCollection.AddTransient<IDateTimeValidator, DateTimeValidator>();
        serviceCollection.AddTransient<ICurrentCharts, CurrentCharts>();
        serviceCollection.AddTransient<DataFilesExportController>();
        serviceCollection.AddTransient<DataFilesImportController>();
        serviceCollection.AddTransient<DataFilesOverviewController>();
        serviceCollection.AddTransient<IDataNameForDataGridFactory, DataNameForDataGridFactory>();
        serviceCollection.AddTransient<IDateTimeApi, DateTimeApi>();
        serviceCollection.AddTransient<IHarmonicForDataGridFactory, HarmonicForDataGridFactory>();
        serviceCollection.AddTransient<HelpWindow>();
        serviceCollection.AddTransient<IHousePosForDataGridFactory, HousePosForDataGridFactory>();
        serviceCollection.AddTransient<MainController>();
        serviceCollection.AddTransient<MainWindow>();
        serviceCollection.AddTransient<IMidpointForDataGridFactory, MidpointForDataGridFactory>();
        serviceCollection.AddTransient<ProjectInputController>();
        serviceCollection.AddTransient<ProjectsOverviewController>();
        serviceCollection.AddTransient<IRangeCheck, RangeCheck>();

        serviceCollection.AddTransient<ResearchMethodInputWindow>();
        serviceCollection.AddTransient<ResearchMethodInputController>();
        serviceCollection.AddTransient<IRosetta, Rosetta>();
        serviceCollection.AddTransient<ISortedGraphicCelPointsFactory, SortedGraphicCelPointsFactory>();
        serviceCollection.AddTransient<ITextFileReaderFE, TextFileReader>();

        // Handle services from other projects.
        serviceCollection.RegisterFrontendHelpersServices();
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

