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
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.SUpport;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Windows;
using Enigma.Frontend.Ui.Graphics;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.ViewModels;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui;


public partial class App
{
    public static ServiceProvider ServiceProvider { get; } = HandleRegistrationForDi();

    private static readonly string EnigmaLogRoot = ApplicationSettings.LocationLogFiles;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        DefineLogging();
        Log.Information("********************** Enigma starting ***********************");
        StartModel.HandleCheckForConfig();
        StartModel.HandleCheckDirForSettings();
        //    StartModel.HandleCheckNewVersion();
        ISeApi seApi = ServiceProvider.GetRequiredService<ISeApi>();
        seApi.SetupSe("se");
    }

    private static ServiceProvider HandleRegistrationForDi()
    {
        var serviceCollection = new ServiceCollection();

        // Handle services from project Enigma.Frontend.
        //serviceCollection.AddTransient<AppSettingsController>();
        serviceCollection.AddTransient<AppSettingsModel>();
        serviceCollection.AddTransient<IAspectForDataGridFactory, AspectForDataGridFactory>();
        serviceCollection.AddTransient<IAspectForWheelFactory, AspectForWheelFactory>();
        serviceCollection.AddTransient<ICelPointForDataGridFactory, CelPointForDataGridFactory>();
        serviceCollection.AddTransient<IChartCalculation, ChartCalculation>();
        serviceCollection.AddTransient<IChartDataConverter, ChartDataConverter>();
        serviceCollection.AddTransient<IChartDataForDataGridFactory, ChartDataForDataGridFactory>();
        serviceCollection.AddTransient<ChartsMainModel>();
        serviceCollection.AddTransient<ChartsWheelWindow>();
        serviceCollection.AddTransient<IChartsWheelAspects, ChartsWheelAspects>();
        serviceCollection.AddTransient<IChartsWheelCircles, ChartsWheelCircles>();
        serviceCollection.AddTransient<ChartsWheelController>();
        serviceCollection.AddTransient<IChartsWheelCusps, ChartsWheelCusps>();
        serviceCollection.AddTransient<ChartsWheelMetrics>();
        serviceCollection.AddTransient<IChartsWheelSigns, ChartsWheelSigns>();
        serviceCollection.AddTransient<IChartsWheelCelPoints, ChartsWheelCelPoints>();
        serviceCollection.AddTransient<ConfigurationModel>();
        serviceCollection.AddTransient<ICurrentCharts, CurrentCharts>();
        serviceCollection.AddTransient<DatafileOverviewModel>();
        serviceCollection.AddTransient<DatafileImportModel>();
        serviceCollection.AddTransient<IDataNameForPresentationFactory, DataNameForPresentationFactory>();
        serviceCollection.AddTransient<IDateTimeApi, DateTimeApi>();
        serviceCollection.AddTransient<IDescriptiveChartText, DescriptiveChartText>();
        serviceCollection.AddTransient<IEventDataConverter, EventDataConverter>();
        serviceCollection.AddTransient<IHarmonicForDataGridFactory, HarmonicForDataGridFactory>();
        serviceCollection.AddTransient<HelpModel>();
        serviceCollection.AddTransient<IHousePosForDataGridFactory, HousePosForDataGridFactory>();
        serviceCollection.AddTransient<ILocationConversion, LocationConversion>();
        serviceCollection.AddTransient<IMidpointForDataGridFactory, MidpointForDataGridFactory>();
        serviceCollection.AddTransient<IPeriodDataConverter, PeriodDataConverter>();
        serviceCollection.AddTransient<IPointsExclusionManager, PointsExclusionManager>();
        serviceCollection.AddTransient<IProgDatesForPresentationFactory, ProgDatesForPresentationFactory>();
        serviceCollection.AddTransient<ProgEventModel>();
        serviceCollection.AddTransient<IProgDatesForPresentationFactory, ProgDatesForPresentationFactory>();
        serviceCollection.AddTransient<ProgPeriodModel>();
        serviceCollection.AddTransient<ProgressiveMainModel>();
        serviceCollection.AddTransient<ProjectUsageModel>();
        serviceCollection.AddTransient<ProjectInputModel>();
        serviceCollection.AddTransient<RadixAspectsModel>();
        serviceCollection.AddTransient<RadixDataInputModel>();
        serviceCollection.AddTransient<RadixHarmonicsModel>();
        serviceCollection.AddTransient<RadixMidpointsModel>();
        serviceCollection.AddTransient<RadixPositionsModel>();
        serviceCollection.AddTransient<RadixSearchModel>();
        serviceCollection.AddTransient<ResearchHarmonicDetailsModel>();
        serviceCollection.AddTransient<ResearchMainModel>();
        serviceCollection.AddTransient<ResearchMidpointDetailsModel>();
        serviceCollection.AddTransient<ResearchPointSelectionModel>();
        serviceCollection.AddTransient<ResearchResultModel>();
        serviceCollection.AddTransient<ISortedGraphicCelPointsFactory, SortedGraphicCelPointsFactory>();
        serviceCollection.AddTransient<StartModel>();

        // Handle services from other projects.
        serviceCollection.RegisterFrontendHelpersServices();
        serviceCollection.RegisterApiServices();


        return serviceCollection.BuildServiceProvider(true);
    }

    private static void DefineLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(EnigmaLogRoot + @"/enigma_.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

}

