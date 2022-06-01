// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.DateTime.CheckDateTime;
using Enigma.Core.Calc.SeFacades;
using Enigma.Core.Calc.Services;
using Enigma.Domain.DateTime;
using Enigma.Frontend.Calculators;
using Enigma.Frontend.Calculators.JulDay;
using Enigma.Frontend.Calculators.Obliquity;
using Enigma.Frontend.Charts;
using Enigma.Frontend.InputSupport.Services;
using Enigma.Frontend.Support;
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

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTransient<MainWindow>();
        serviceCollection.AddTransient<CalcStartView>();
        serviceCollection.AddTransient<ChartsStartView>();
        serviceCollection.AddTransient<ChartDataInputView>();
        serviceCollection.AddTransient<JulDayView>();
        serviceCollection.AddTransient<JulDayController>();
        serviceCollection.AddTransient<ObliquityView>();
        serviceCollection.AddTransient<ObliquityController>();
        serviceCollection.AddTransient<HelpWindow>();
        serviceCollection.AddTransient<IRosetta, Rosetta>();
        serviceCollection.AddTransient<ITextFileReader, TextFileReader>();
        serviceCollection.AddSingleton<ICheckDateTimeHandler, CheckDateTimeHandler>();
        serviceCollection.AddSingleton<ICheckDateTimeValidator, CheckDateTimeValidator>();
        serviceCollection.AddSingleton<ITimeZoneSpecifications, TimeZoneSpecifications>();

        serviceCollection.RegisterInputSUpportServices();
        serviceCollection.RegisterCalculationServices();

        ServiceProvider = serviceCollection.BuildServiceProvider(true);

        //var mainWindow = ServiceProvider.GetService<StartWindow>();
        //      var mainWindow = new MainWindow();
        //mainWindow?.Show();

        string pathToSeFiles = "./se";
        SeInitializer.SetEphePath(pathToSeFiles);
    }
}

