// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.DateTime.CheckDateTime;
using Enigma.Core.Calc.SeFacades;
using Enigma.Core.Calc.Services;
using Enigma.Domain.DateTime;
using Enigma.Domain.Services;
using Enigma.Frontend.Charts;
using Enigma.Frontend.Charts.Graphics;
using Enigma.Frontend.InputSupport.PresentationFactories;
using Enigma.Frontend.InputSupport.Services;
using Enigma.Frontend.Support;
using Enigma.Frontend.UiDomain;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Enigma.Frontend;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static ServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        string pathToSeFiles = @"c:\sweph";
        SeInitializer.SetEphePath(pathToSeFiles);

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTransient<ICelPointForDataGridFactory, CelPointForDataGridFactory>();
        serviceCollection.AddTransient<ChartDataInputController>();
        serviceCollection.AddTransient<ChartDataInputWindow>();
        serviceCollection.AddTransient<ChartPositionsController>();
        serviceCollection.AddTransient<ChartPositionsWindow>();
        serviceCollection.AddTransient<IChartsEnumFacade, ChartsEnumFacade>();
        serviceCollection.AddTransient<ChartsWheel>();
        serviceCollection.AddTransient<ChartsWheelController>();
        serviceCollection.AddTransient<ChartsWheelMetrics>();
        serviceCollection.AddSingleton<MainController>();
        serviceCollection.AddTransient<MainWindow>();
        serviceCollection.AddSingleton<ICheckDateTimeHandler, CheckDateTimeHandler>();
        serviceCollection.AddSingleton<ICheckDateTimeValidator, CheckDateTimeValidator>();
        serviceCollection.AddTransient<ICurrentCharts, CurrentCharts>();
        serviceCollection.AddTransient<HelpWindow>();
        serviceCollection.AddSingleton<IHousePosForDataGridFactory, HousePosForDataGridFactory>();
        serviceCollection.AddTransient<IRosetta, Rosetta>();
        serviceCollection.AddTransient<StartWindow>();
        serviceCollection.AddTransient<ITextFileReader, TextFileReader>();
        serviceCollection.AddSingleton<ITimeZoneSpecifications, TimeZoneSpecifications>();

        serviceCollection.RegisterCalculationServices();
        serviceCollection.RegisterDomainServices();
        serviceCollection.RegisterInputSupportServices();

        ServiceProvider = serviceCollection.BuildServiceProvider(true);


    }
}

