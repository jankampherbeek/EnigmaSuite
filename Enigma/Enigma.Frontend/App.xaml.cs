// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
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
      //  StartModel.HandleCheckDirForSettings();
        //    StartModel.HandleCheckNewVersion();   // todo enable check for new version
      //  bool rdbmsOk = StartModel.HandleCheckRdbms();  // todo handle situations where rdbms was not properly handled.
        ISeApi seApi = ServiceProvider.GetRequiredService<ISeApi>();
        seApi.SetupSe("se");
    }

    private static ServiceProvider HandleRegistrationForDi()
    {
        var serviceCollection = new ServiceCollection();
        // Handle services from project Enigma.Frontend.
        serviceCollection.AddSingleton<AppSettingsModel>();
        serviceCollection.AddSingleton<IAspectForDataGridFactory, AspectForDataGridFactory>();
        serviceCollection.AddSingleton<IAspectForWheelFactory, AspectForWheelFactory>();
        serviceCollection.AddSingleton<ICelPointForDataGridFactory, CelPointForDataGridFactory>();
        serviceCollection.AddSingleton<IChartCalculation, ChartCalculation>();
        serviceCollection.AddSingleton<IChartDataConverter, ChartDataConverter>();
        serviceCollection.AddSingleton<IChartDataForDataGridFactory, ChartDataForDataGridFactory>();
        serviceCollection.AddSingleton<ChartsMainModel>();
        serviceCollection.AddTransient<ChartsWheelWindow>();
        serviceCollection.AddTransient<IChartsWheelAspects, ChartsWheelAspects>();
        serviceCollection.AddTransient<IChartsWheelCircles, ChartsWheelCircles>();
        serviceCollection.AddTransient<ChartsWheelCanvasController>();
        serviceCollection.AddTransient<IChartsWheelCusps, ChartsWheelCusps>();
        serviceCollection.AddTransient<ChartsWheelMetrics>();
        serviceCollection.AddTransient<IChartsWheelSigns, ChartsWheelSigns>();
        serviceCollection.AddTransient<IChartsWheelCelPoints, ChartsWheelCelPoints>();
        serviceCollection.AddSingleton<ChartsWindowsFlow>();
        serviceCollection.AddSingleton<IConfigPreferencesConverter, ConfigPreferencesConverter>();
        serviceCollection.AddSingleton<ConfigurationModel>();
        serviceCollection.AddSingleton<ConfigProgModel>();
        serviceCollection.AddSingleton<ICurrentCharts, CurrentCharts>();
        serviceCollection.AddSingleton<DatafileOverviewModel>();
        serviceCollection.AddSingleton<DatafileImportModel>();
        serviceCollection.AddSingleton<IDateInputParser, DateInputParser>();
        serviceCollection.AddSingleton<IDataNameForPresentationFactory, DataNameForPresentationFactory>();
        serviceCollection.AddSingleton<IDateValidator, DateValidator>();
        serviceCollection.AddSingleton<IDescriptiveChartText, DescriptiveChartText>();
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
        serviceCollection.AddSingleton<IMidpointForDataGridFactory, MidpointForDataGridFactory>();
        serviceCollection.AddSingleton<IPointsExclusionManager, PointsExclusionManager>();
        serviceCollection.AddSingleton<IProgAspectForPresentationFactory, ProgAspectForPresentationFactory>();
        serviceCollection.AddSingleton<IProgDatesForPresentationFactory, ProgDatesForPresentationFactory>();
        serviceCollection.AddSingleton<ProgEventModel>();
        serviceCollection.AddTransient<ProgEventResultsModel>();
        serviceCollection.AddSingleton<IProgPositionsForPresentationFactory, ProgPositionsForPresentationFactory>();
        serviceCollection.AddSingleton<ProgressiveMainModel>();
        serviceCollection.AddSingleton<ProjectUsageModel>();
        serviceCollection.AddSingleton<ProjectInputModel>();
        serviceCollection.AddTransient<RadixAspectsModel>();
        serviceCollection.AddSingleton<RadixDataInputModel>();
        serviceCollection.AddTransient<RadixHarmonicsModel>();
        serviceCollection.AddTransient<RadixMidpointsModel>();
        serviceCollection.AddTransient<RadixPositionsModel>();
        serviceCollection.AddSingleton<RadixSearchModel>();
        serviceCollection.AddSingleton<ResearchHarmonicDetailsModel>();
        serviceCollection.AddSingleton<ResearchMainModel>();
        serviceCollection.AddSingleton<ResearchWindowsFlow>();
        serviceCollection.AddSingleton<ResearchMidpointDetailsModel>();
        serviceCollection.AddSingleton<ResearchPointSelectionModel>();
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

