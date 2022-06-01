// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Calculators;
using Enigma.Frontend.Charts;
using Enigma.Frontend.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;


namespace Enigma.Frontend;



/// <summary>Dashboard, start window for application, provides access to the four main functionalities of the Enigma Suite.</summary>
public partial class MainWindow : Window
{
    private IRosetta _rosetta;
    private CalcStartView _calcStartWindow;
    private ChartsStartView _chartsStartWindow;
    private HelpWindow _helpWindow;

    public MainWindow()
    {
        _rosetta = App.ServiceProvider.GetService<IRosetta>();
        _calcStartWindow = App.ServiceProvider.GetService<CalcStartView>();
        _chartsStartWindow = App.ServiceProvider.GetService<ChartsStartView>();
        _helpWindow = App.ServiceProvider.GetService<HelpWindow>();

        InitializeComponent();
        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        PopulateStaticTexts();
    }

    private void PopulateStaticTexts()
    {
        Title = _rosetta.TextForId("dashboard.title");
        FormTitle.Text = _rosetta.TextForId("dashboard.formtitle");
        btnCharts.Content = _rosetta.TextForId("dashboard.charts");
        btnCycles.Content = _rosetta.TextForId("dashboard.cycles");
        btnCalc.Content = _rosetta.TextForId("dashboard.calculations");
        btnCount.Content = _rosetta.TextForId("dashboard.counts");
        tbCharts.Text = _rosetta.TextForId("dashboard.charts");
        tbCycles.Text = _rosetta.TextForId("dashboard.cycles");
        tbCalc.Text = _rosetta.TextForId("dashboard.calculations");
        tbCount.Text = _rosetta.TextForId("dashboard.counts");
        btnHelp.Content = _rosetta.TextForId("common.btnhelp");
        btnExit.Content = _rosetta.TextForId("dashboard.exit");
    }

    private void BtnCalc_Click(object sender, RoutedEventArgs e)
    {
        _calcStartWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        _calcStartWindow.ShowDialog();
    }

    private void BtnExit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void BtnCharts_Click(object sender, RoutedEventArgs e)
    {
        _chartsStartWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        _chartsStartWindow.ShowDialog();
    }


    private void BtnCycles_Click(object sender, RoutedEventArgs e)
    {
        //      CyclesStartWindow cyclesStartWindow = UiContainer.Instance.getContainer().GetInstance<CyclesStartWindow>();
        //      cyclesStartWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        //      cyclesStartWindow.ShowDialog();
    }


    private void BtnCounts_Click(object sender, RoutedEventArgs e)
    {
        //      CountsStartWindow countsStartWindow = UiContainer.Instance.getContainer().GetInstance<CountsStartWindow>();
        //      countsStartWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        //      countsStartWindow.ShowDialog();
    }

    private void BtnHelp_Click(object sender, RoutedEventArgs e)
    {
        _helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        _helpWindow.SetUri("Dashboard");
        _helpWindow.ShowDialog();
    }
}

