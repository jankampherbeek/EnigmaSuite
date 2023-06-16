// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Calc;
using Enigma.Api.Interfaces;
using Enigma.Api.Services;
using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;
using Enigma.Domain.Interfaces;
using Enigma.Frontend.Helpers.Services;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Charts;
using Enigma.Frontend.Ui.Charts.Graphics;
using Enigma.Frontend.Ui.Charts.Progressive;
using Enigma.Frontend.Ui.Charts.Progressive.InputPeriod;
using Enigma.Frontend.Ui.Charts.Progressive.InputTransits;
using Enigma.Frontend.Ui.Configuration;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.Research;
using Enigma.Frontend.Ui.Research.DataFiles;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.SUpport;
using Enigma.Frontend.Uit.Charts;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Windows;

namespace Enigma.Frontend.Ui;


public partial class App : Application
{
    public static ServiceProvider ServiceProvider { get; private set; } = HandleRegistrationForDI();

    private static readonly string EnigmaLogRoot = ApplicationSettings.Instance.LocationLogFiles;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        DefineLogging();
        Log.Information("********************** Enigma starting ***********************");
        ISeApi seApi = ServiceProvider.GetRequiredService<ISeApi>();
        seApi.SetupSe("");
    }

    protected static ServiceProvider HandleRegistrationForDI()
    {
        var serviceCollection = new ServiceCollection();

        // Handle services from project Enigma.Frontend.
        serviceCollection.AddTransient<AppSettingsController>();
        serviceCollection.AddTransient<IAspectForDataGridFactory, AspectForDataGridFactory>();
        serviceCollection.AddTransient<IAspectForWheelFactory, AspectForWheelFactory>();
        serviceCollection.AddTransient<AstroConfigController>();
        serviceCollection.AddTransient<AstroConfigWindow>();
        serviceCollection.AddTransient<ICelPointForDataGridFactory, CelPointForDataGridFactory>();
        serviceCollection.AddTransient<ChartAspectsController>();
        serviceCollection.AddTransient<ChartAspectsWindow>();
        serviceCollection.AddTransient<IChartCalculation, ChartCalculation>();
        serviceCollection.AddTransient<IChartDataConverter, ChartDataConverter>();
        serviceCollection.AddTransient<IChartDataForDataGridFactory, ChartDataForDataGridFactory>();
        serviceCollection.AddTransient<ChartDataInputController>();
        serviceCollection.AddTransient<ChartHarmonicsController>();
        serviceCollection.AddTransient<ChartHarmonicsWindow>();
        serviceCollection.AddTransient<ChartMidpointsController>();
        serviceCollection.AddTransient<ChartMidpointsWindow>();
        serviceCollection.AddTransient<ChartPositionsWindow>();
        serviceCollection.AddTransient<ChartPositionsController>();
        serviceCollection.AddTransient<ChartProgPrimInputController>();
        serviceCollection.AddTransient<ChartProgSecInputController>();
        serviceCollection.AddTransient<ChartProgInputSolarController>();
        serviceCollection.AddTransient<ChartsMainController>();
        serviceCollection.AddTransient<ChartsWheel>();
        serviceCollection.AddTransient<IChartsWheelAspects, ChartsWheelAspects>();
        serviceCollection.AddTransient<IChartsWheelCircles, ChartsWheelCircles>();
        serviceCollection.AddTransient<ChartsWheelController>();
        serviceCollection.AddTransient<IChartsWheelCusps, ChartsWheelCusps>();
        serviceCollection.AddTransient<ChartsWheelMetrics>();
        serviceCollection.AddTransient<IChartsWheelSigns, ChartsWheelSigns>();
        serviceCollection.AddTransient<IChartsWheelCelPoints, ChartsWheelCelPoints>();
        serviceCollection.AddTransient<ICurrentCharts, CurrentCharts>();
        serviceCollection.AddTransient<DataFilesImportController>();
        serviceCollection.AddTransient<DataFilesOverviewController>();
        serviceCollection.AddTransient<IDataNameForDataGridFactory, DataNameForDataGridFactory>();
        serviceCollection.AddTransient<IDateTimeApi, DateTimeApi>();
        serviceCollection.AddTransient<IDescriptiveChartText, DescriptiveChartText>();
        serviceCollection.AddTransient<HarmonicDetailsController>();
        serviceCollection.AddTransient<HarmonicDetailsWindow>();
        serviceCollection.AddTransient<IHarmonicForDataGridFactory, HarmonicForDataGridFactory>();
        serviceCollection.AddTransient<HelpWindow>();
        serviceCollection.AddTransient<IHousePosForDataGridFactory, HousePosForDataGridFactory>();
        serviceCollection.AddTransient<ILocationConversion, LocationConversion>();
        serviceCollection.AddTransient<MainController>();
        serviceCollection.AddTransient<MainWindow>();
        serviceCollection.AddTransient<MidpointDetailsController>();
        serviceCollection.AddTransient<MidpointDetailsWindow>();
        serviceCollection.AddTransient<IMidpointForDataGridFactory, MidpointForDataGridFactory>();
        serviceCollection.AddTransient<IPointsExclusionManager, PointsExclusionManager>();
        serviceCollection.AddTransient<PointSelectController>();
        serviceCollection.AddTransient<PointSelectWindow>();
        serviceCollection.AddTransient<ProgInputPeriodController>();
        serviceCollection.AddTransient<ProgInputTransitsController>();
        serviceCollection.AddTransient<ProjectUsageController>();
        serviceCollection.AddTransient<ProjectUsageWindow>();
        serviceCollection.AddTransient<ProjectInputController>();
        serviceCollection.AddTransient<ResearchMainController>();
        serviceCollection.AddTransient<ResearchMainController>();
        serviceCollection.AddTransient<ResearchMainWindow>();
        serviceCollection.AddTransient<ResearchResultController>();
        serviceCollection.AddTransient<ResearchResultWindow>();
        serviceCollection.AddTransient<SearchChartController>();
        serviceCollection.AddTransient<SearchChartWindow>();
        serviceCollection.AddTransient<ISortedGraphicCelPointsFactory, SortedGraphicCelPointsFactory>();

        // Handle services from other projects.
        serviceCollection.RegisterFrontendHelpersServices();
        serviceCollection.RegisterApiServices();


        return serviceCollection.BuildServiceProvider(true);
    }

    public static void DefineLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(EnigmaLogRoot + @"/enigma_.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

}

