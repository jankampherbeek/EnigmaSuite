// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Services;
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
        ISeApi seApi = ServiceProvider.GetRequiredService<ISeApi>();
        seApi.SetupSe("se");
    }

    private static ServiceProvider HandleRegistrationForDi()
    {
        var serviceCollection = new ServiceCollection();
        // Handle services from project Enigma.Frontend.
        serviceCollection.AddTransient<AppSettingsModel>();
        serviceCollection.AddSingleton<IAspectForDataGridFactory, AspectForDataGridFactory>();
        serviceCollection.AddSingleton<IAspectForWheelFactory, AspectForWheelFactory>();
        serviceCollection.AddSingleton<ICelPointForDataGridFactory, CelPointForDataGridFactory>();
        serviceCollection.AddTransient<IChartCalculation, ChartCalculation>();
        serviceCollection.AddTransient<IChartDataConverter, ChartDataConverter>();
        serviceCollection.AddSingleton<IChartDataForDataGridFactory, ChartDataForDataGridFactory>();
        serviceCollection.AddTransient<ChartsImportModel>();
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
        serviceCollection.AddSingleton<IColorMapper, ColorMapper>();
        serviceCollection.AddTransient<IConfigPreferencesConverter, ConfigPreferencesConverter>();
        serviceCollection.AddTransient<ConfigurationModel>();
        serviceCollection.AddTransient<ConfigProgModel>();
        serviceCollection.AddSingleton<ICurrentCharts, CurrentCharts>();
        serviceCollection.AddTransient<CyclesMainModel>();
        serviceCollection.AddTransient<DatafileOverviewModel>();
        serviceCollection.AddTransient<DatafileImportModel>();
        serviceCollection.AddTransient<IDateInputParser, DateInputParser>();
        serviceCollection.AddSingleton<IDataNameForPresentationFactory, DataNameForPresentationFactory>();
        serviceCollection.AddSingleton<IDateValidator, DateValidator>();
        serviceCollection.AddTransient<DeclDiagramCanvasController>();
        serviceCollection.AddTransient<DeclDiagramMetrics>();
        serviceCollection.AddTransient<IDescriptiveChartText, DescriptiveChartText>();
        serviceCollection.AddSingleton<IDoubleToDmsConversions, DoubleToDmsConversions>();
        serviceCollection.AddSingleton<IEventDataConverter, EventDataConverter>();
        serviceCollection.AddSingleton<GeneralWindowsFlow>();
        serviceCollection.AddSingleton<IGeoLatInputParser, GeoLatInputParser>();
        serviceCollection.AddSingleton<IGeoLatValidator, GeoLatValidator>();
        serviceCollection.AddSingleton<IGeoLongInputParser, GeoLongInputParser>();
        serviceCollection.AddSingleton<IGeoLongValidator, GeoLongValidator>();   
        serviceCollection.AddSingleton<GlyphsForChartPoints>();
        serviceCollection.AddSingleton<IHarmonicForDataGridFactory, HarmonicForDataGridFactory>();
        serviceCollection.AddTransient<HelpModel>();
        serviceCollection.AddSingleton<IHousePosForDataGridFactory, HousePosForDataGridFactory>();
        serviceCollection.AddSingleton<ILocationConversion, LocationConversion>();
        serviceCollection.AddSingleton<ILongitudeEquivalentsForDataGridFactory, LongitudeEquivalentsForDataGridFactory>();
        serviceCollection.AddSingleton<IMidpointForDataGridFactory, MidpointForDataGridFactory>();
        serviceCollection.AddSingleton<IParallelsForDataGridFactory, ParallelsForDataGridFactory>();
        serviceCollection.AddSingleton<IPointsExclusionManager, PointsExclusionManager>();
        serviceCollection.AddSingleton<IProgAspectForPresentationFactory, ProgAspectForPresentationFactory>();
        serviceCollection.AddSingleton<IProgDatesForPresentationFactory, ProgDatesForPresentationFactory>();
        serviceCollection.AddSingleton<ProgEventModel>();
        serviceCollection.AddTransient<ProgEventResultsModel>();
        serviceCollection.AddSingleton<IProgPositionsForPresentationFactory, ProgPositionsForPresentationFactory>();
        serviceCollection.AddTransient<ProgressiveMainModel>();
        serviceCollection.AddTransient<ProjectUsageModel>();
        serviceCollection.AddTransient<ProjectInputModel>();
        serviceCollection.AddTransient<RadixAspectsModel>();
        serviceCollection.AddTransient<RadixDataInputModel>();
        serviceCollection.AddTransient<RadixDeclMidpointsModel>();
        serviceCollection.AddTransient<RadixHarmonicsModel>();
        serviceCollection.AddTransient<RadixLongitudeEquivalentsModel>();
        serviceCollection.AddTransient<RadixMidpointsModel>();
        serviceCollection.AddTransient<RadixParallelsModel>();
        serviceCollection.AddTransient<RadixPositionsModel>();
        serviceCollection.AddTransient<RadixSearchModel>();
        serviceCollection.AddTransient<ResearchHarmonicDetailsModel>();
        serviceCollection.AddTransient<ResearchMainModel>();
        serviceCollection.AddSingleton<ResearchWindowsFlow>();
        serviceCollection.AddTransient<ResearchMidpointDetailsModel>();
        serviceCollection.AddTransient<ResearchPointSelectionModel>();
        serviceCollection.AddTransient<ResearchResultModel>();
        serviceCollection.AddSingleton<ISexagesimalConversions, SexagesimalConversions>();        
        serviceCollection.AddSingleton<ISortedGraphicCelPointsFactory, SortedGraphicCelPointsFactory>();
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

