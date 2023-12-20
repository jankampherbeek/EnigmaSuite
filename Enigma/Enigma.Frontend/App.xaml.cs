// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Api.Services;
using Enigma.Domain.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Windows;
using Enigma.Api;
using Enigma.Domain.Dtos;
using Enigma.Frontend.Ui.Graphics;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.Support.Conversions;
using Enigma.Frontend.Ui.Support.Parsers;
using Enigma.Frontend.Ui.Support.Validations;
using Enigma.Frontend.Ui.ViewModels;
using Enigma.Frontend.Ui.Views;
using Enigma.Frontend.Ui.WindowsFlow;

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
        //    StartModel.HandleCheckNewVersion();   // todo enable check for new version
        ISeApi seApi = ServiceProvider.GetRequiredService<ISeApi>();
        seApi.SetupSe("se");
    }

    private static ServiceProvider HandleRegistrationForDi()
    {
        var serviceCollection = new ServiceCollection();
        // Handle services from project Enigma.Frontend.
        serviceCollection.AddTransient<AppSettingsModel>();
        serviceCollection.AddTransient<IAspectForDataGridFactory, AspectForDataGridFactory>();
        serviceCollection.AddTransient<IAspectForWheelFactory, AspectForWheelFactory>();
        serviceCollection.AddTransient<IProgTransitsEventApi, ProgTransitsEventApi>();
        serviceCollection.AddTransient<CalcHeliacalModel>();
        serviceCollection.AddTransient<CalcHouseComparisonModel>();
        serviceCollection.AddTransient<CelestialObjectsSelectionModel>();        
        serviceCollection.AddTransient<ICelPointForDataGridFactory, CelPointForDataGridFactory>();
        serviceCollection.AddTransient<IChartCalculation, ChartCalculation>();
        serviceCollection.AddTransient<IChartDataConverter, ChartDataConverter>();
        serviceCollection.AddTransient<IChartDataForDataGridFactory, ChartDataForDataGridFactory>();
        serviceCollection.AddTransient<ChartsMainModel>();
        serviceCollection.AddTransient<ChartsWheelWindow>();
        serviceCollection.AddTransient<IChartsWheelAspects, ChartsWheelAspects>();
        serviceCollection.AddTransient<IChartsWheelCircles, ChartsWheelCircles>();
        serviceCollection.AddTransient<ChartsWheelCanvasController>();
        serviceCollection.AddTransient<IChartsWheelCusps, ChartsWheelCusps>();
        serviceCollection.AddTransient<ChartsWheelMetrics>();
        serviceCollection.AddTransient<IChartsWheelSigns, ChartsWheelSigns>();
        serviceCollection.AddTransient<IChartsWheelCelPoints, ChartsWheelCelPoints>();
        serviceCollection.AddSingleton<ChartsWindowsFlow>();
        serviceCollection.AddTransient<IConfigPreferencesConverter, ConfigPreferencesConverter>();
        serviceCollection.AddTransient<ConfigurationModel>();
        serviceCollection.AddTransient<ConfigProgModel>();
        serviceCollection.AddTransient<ICurrentCharts, CurrentCharts>();
        serviceCollection.AddTransient<CyclesSinglePositionsModel>();
        serviceCollection.AddTransient<DatafileOverviewModel>();
        serviceCollection.AddTransient<DatafileImportModel>();
        serviceCollection.AddSingleton<IDateInputParser, DateInputParser>();
        serviceCollection.AddTransient<IDataNameForPresentationFactory, DataNameForPresentationFactory>();
        serviceCollection.AddTransient<IDateTimeApi, DateTimeApi>();
        serviceCollection.AddSingleton<IDateValidator, DateValidator>();
        serviceCollection.AddTransient<IDescriptiveChartText, DescriptiveChartText>();
        serviceCollection.AddSingleton<IDoubleToDmsConversions, DoubleToDmsConversions>();
        serviceCollection.AddTransient<IEventDataConverter, EventDataConverter>();
        serviceCollection.AddSingleton<GeneralWindowsFlow>();
        serviceCollection.AddSingleton<IGeoLatInputParser, GeoLatInputParser>();
        serviceCollection.AddSingleton<IGeoLatValidator, GeoLatValidator>();
        serviceCollection.AddSingleton<IGeoLongInputParser, GeoLongInputParser>();
        serviceCollection.AddSingleton<IGeoLongValidator, GeoLongValidator>();   
        serviceCollection.AddTransient<GlyphsForChartPoints>();
        serviceCollection.AddTransient<IHarmonicForDataGridFactory, HarmonicForDataGridFactory>();
        serviceCollection.AddTransient<HelpModel>();
        serviceCollection.AddTransient<IHousePosForDataGridFactory, HousePosForDataGridFactory>();
        serviceCollection.AddTransient<ILocationConversion, LocationConversion>();
        serviceCollection.AddTransient<IMidpointForDataGridFactory, MidpointForDataGridFactory>();
        serviceCollection.AddTransient<IPeriodDataConverter, PeriodDataConverter>();
        serviceCollection.AddTransient<IPointsExclusionManager, PointsExclusionManager>();
        serviceCollection.AddTransient<IProgAspectForPresentationFactory, ProgAspectForPresentationFactory>();
        serviceCollection.AddTransient<IProgDatesForPresentationFactory, ProgDatesForPresentationFactory>();
        serviceCollection.AddTransient<ProgEventModel>();
        serviceCollection.AddTransient<ProgEventResultsModel>();
        serviceCollection.AddTransient<ProgPeriodModel>();
        serviceCollection.AddTransient<IProgPositionsForPresentationFactory, ProgPositionsForPresentationFactory>();
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
        serviceCollection.AddSingleton<ResearchWindowsFlow>();
        serviceCollection.AddTransient<ResearchMidpointDetailsModel>();
        serviceCollection.AddTransient<ResearchPointSelectionModel>();
        serviceCollection.AddTransient<ResearchResultModel>();
        serviceCollection.AddSingleton<ISexagesimalConversions, SexagesimalConversions>();        
        serviceCollection.AddTransient<ISortedGraphicCelPointsFactory, SortedGraphicCelPointsFactory>();
        serviceCollection.AddTransient<StartModel>();
        serviceCollection.AddSingleton<ITimeInputParser, TimeInputParser>();
        serviceCollection.AddSingleton<ITimeValidator, TimeValidator>();
        serviceCollection.AddSingleton<IValueRangeConverter, ValueRangeConverter>();

        serviceCollection.AddTransient<HelpWindow>();
        
        // Handle services from other projects.
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

