// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.DateTime.CheckDateTime;
using Enigma.Core.Calc.SeFacades;
using Enigma.Core.Calc.Services;
using Enigma.Domain.DateTime;
using Enigma.Domain.Services;
using Enigma.Frontend.Charts;
using Enigma.Frontend.Charts.Graphics;
using Enigma.Frontend.PresentationFactories;
using Enigma.InputSupport.Services;
using Enigma.Frontend.Support;
using Enigma.Frontend.UiDomain;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Enigma.Core.Analysis.Services;
using Enigma.Frontend.DataFiles;
using Enigma.Frontend.Settings;
using Enigma.Persistency.Services;
using Enigma.Configuration.Services;


namespace Enigma.Frontend;


public partial class App : Application
{
    public static ServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        string pathToSeFiles = @"c:\sweph";                    // TODO make path to SE files configurable
        SeInitializer.SetEphePath(pathToSeFiles);
        HandleRegistrationForDI();
    }

    protected void HandleRegistrationForDI()
    {
        var serviceCollection = new ServiceCollection();

        // Handle services from project Enigma.Frontend.
        serviceCollection.AddTransient<AboutWindow>();
        serviceCollection.AddTransient<AppSettingsController>();
        serviceCollection.AddTransient<AppSettingsWindow>();
        serviceCollection.AddTransient<IAspectForDataGridFactory, AspectForDataGridFactory>();
        serviceCollection.AddTransient<IAspectForWheelFactory, AspectForWheelFactory>();
        serviceCollection.AddTransient<AstroConfigController>();
        serviceCollection.AddTransient<AstroConfigWindow>();
        serviceCollection.AddTransient<ICelPointForDataGridFactory, CelPointForDataGridFactory>();
        serviceCollection.AddTransient<ChartAspectsWindow>();
        serviceCollection.AddTransient<ChartAspectsController>();
        serviceCollection.AddTransient<ChartDataInputController>();
        serviceCollection.AddTransient<ChartDataInputWindow>();
        serviceCollection.AddTransient<ChartPositionsController>();
        serviceCollection.AddTransient<ChartPositionsWindow>();
        serviceCollection.AddTransient<IChartsEnumFacade, ChartsEnumFacade>();
        serviceCollection.AddTransient<ChartsWheel>();
        serviceCollection.AddTransient<IChartsWheelAspects, ChartsWheelAspects>();
        serviceCollection.AddTransient<IChartsWheelCircles, ChartsWheelCircles>();
        serviceCollection.AddTransient<ChartsWheelController>();
        serviceCollection.AddTransient<IChartsWheelCusps, ChartsWheelCusps>();
        serviceCollection.AddTransient<ChartsWheelMetrics>();
        serviceCollection.AddTransient<IChartsWheelSigns, ChartsWheelSigns>();
        serviceCollection.AddTransient<IChartsWheelSolSysPoints, ChartsWheelSolSysPoints>();
        serviceCollection.AddTransient<DataFilesExportController>();
        serviceCollection.AddTransient<DataFilesExportWindow>();
        serviceCollection.AddTransient<DataFilesImportController>();
        serviceCollection.AddTransient<DataFilesImportWindow>();
        serviceCollection.AddTransient<DataFilesOverviewController>();
        serviceCollection.AddTransient<DataFilesOverviewWindow>();
        serviceCollection.AddSingleton<MainController>();
        serviceCollection.AddTransient<MainWindow>();
        serviceCollection.AddSingleton<ICheckDateTimeHandler, CheckDateTimeHandler>();
        serviceCollection.AddSingleton<ICheckDateTimeValidator, CheckDateTimeValidator>();
        serviceCollection.AddTransient<ICurrentCharts, CurrentCharts>();
        serviceCollection.AddTransient<HelpWindow>();
        serviceCollection.AddSingleton<IHousePosForDataGridFactory, HousePosForDataGridFactory>();
        serviceCollection.AddTransient<IRangeCheck, RangeCheck>();
        serviceCollection.AddTransient<IRosetta, Rosetta>();
        serviceCollection.AddTransient<ISortedGraphicSolSysPointsFactory, SortedGraphicSolSysPointsFactory>();
        serviceCollection.AddTransient<StartWindow>();
        serviceCollection.AddTransient<ITextFileReader, TextFileReader>();
        serviceCollection.AddSingleton<ITimeZoneSpecifications, TimeZoneSpecifications>();
        
        // Handle services from other projects.
        serviceCollection.RegisterCalculationServices();
        serviceCollection.RegisterDomainServices();
        serviceCollection.RegisterInputSupportServices();
        serviceCollection.RegisterAnalysisServices();
        serviceCollection.RegisterPersistencyServices();
        serviceCollection.RegisterConfigurationServices();

        ServiceProvider = serviceCollection.BuildServiceProvider(true);

    }
}

