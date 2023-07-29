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
using Enigma.Frontend.Ui.Charts.Graphics;
using Enigma.Frontend.Ui.Charts.Progressive;
using Enigma.Frontend.Ui.Charts.Progressive.InputEvent;
using Enigma.Frontend.Ui.Charts.Progressive.InputPeriod;
using Enigma.Frontend.Ui.Charts.Progressive.InputTransits;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.Research;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.SUpport;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Windows;
using Enigma.Frontend.Ui.Charts.Shared;
using Enigma.Frontend.Ui.Models;

namespace Enigma.Frontend.Ui;


public partial class App
{
    public static ServiceProvider ServiceProvider { get; } = HandleRegistrationForDi();

    private static readonly string EnigmaLogRoot = ApplicationSettings.Instance.LocationLogFiles;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        DefineLogging();
        Log.Information("********************** Enigma starting ***********************");
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
        serviceCollection.AddTransient<CalYearCountController>();
        serviceCollection.AddTransient<ICelPointForDataGridFactory, CelPointForDataGridFactory>();
        serviceCollection.AddTransient<IChartCalculation, ChartCalculation>();
        serviceCollection.AddTransient<IChartDataConverter, ChartDataConverter>();
        serviceCollection.AddTransient<IChartDataForDataGridFactory, ChartDataForDataGridFactory>();
        serviceCollection.AddTransient<ChartProgPrimInputController>();
        serviceCollection.AddTransient<ChartProgSecInputController>();
        serviceCollection.AddTransient<ChartProgInputSolarController>();
        serviceCollection.AddTransient<ChartsMainModel>();
        serviceCollection.AddTransient<ChartsWheel>();
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
        serviceCollection.AddTransient<HarmonicDetailsController>();
        serviceCollection.AddTransient<HarmonicDetailsWindow>();
        serviceCollection.AddTransient<IHarmonicForDataGridFactory, HarmonicForDataGridFactory>();
        serviceCollection.AddTransient<HelpModel>();
        serviceCollection.AddTransient<IHousePosForDataGridFactory, HousePosForDataGridFactory>();
        serviceCollection.AddTransient<ILocationConversion, LocationConversion>();
        serviceCollection.AddTransient<MidpointDetailsController>();
        serviceCollection.AddTransient<MidpointDetailsWindow>();
        serviceCollection.AddTransient<IMidpointForDataGridFactory, MidpointForDataGridFactory>();
        serviceCollection.AddTransient<IPointsExclusionManager, PointsExclusionManager>();
        serviceCollection.AddTransient<PointSelectController>();
        serviceCollection.AddTransient<PointSelectWindow>();
        serviceCollection.AddTransient<ProgInputEvent>();
        serviceCollection.AddTransient<ProgInputEventController>();
        serviceCollection.AddTransient<ProgInputPeriod>();
        serviceCollection.AddTransient<ProgInputPeriodController>();
        serviceCollection.AddTransient<ProgInputTransitsController>();
        serviceCollection.AddTransient<ProjectUsageController>();
        serviceCollection.AddTransient<ProjectUsageWindow>();
        serviceCollection.AddTransient<ProjectInputController>();
        serviceCollection.AddTransient<RadixAspectsModel>();
        serviceCollection.AddTransient<RadixDataInputModel>();
        serviceCollection.AddTransient<RadixHarmonicsModel>();
        serviceCollection.AddTransient<RadixMidpointsModel>();
        serviceCollection.AddTransient<RadixPositionsModel>();
        serviceCollection.AddTransient<RadixSearchModel>();
        serviceCollection.AddTransient<ResearchMainModel>();
        serviceCollection.AddTransient<ResearchResultController>();
        serviceCollection.AddTransient<ResearchResultWindow>();
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

