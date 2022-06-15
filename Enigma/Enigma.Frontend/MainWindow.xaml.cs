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

    public MainWindow()
    {        
        InitializeComponent();
        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        PopulateStaticTexts();
    }

    private void PopulateStaticTexts()
    {
        IRosetta? rosetta = App.ServiceProvider.GetService<IRosetta>();
        if (rosetta != null)
        {
            Title = rosetta.TextForId("dashboard.title");
            FormTitle.Text = rosetta.TextForId("dashboard.formtitle");
            btnCharts.Content = rosetta.TextForId("dashboard.charts");
            btnCycles.Content = rosetta.TextForId("dashboard.cycles");
            btnCalc.Content = rosetta.TextForId("dashboard.calculations");
            btnCount.Content = rosetta.TextForId("dashboard.counts");
            tbCharts.Text = rosetta.TextForId("dashboard.charts");
            tbCycles.Text = rosetta.TextForId("dashboard.cycles");
            tbCalc.Text = rosetta.TextForId("dashboard.calculations");
            tbCount.Text = rosetta.TextForId("dashboard.counts");
            btnHelp.Content = rosetta.TextForId("common.btnhelp");
            btnExit.Content = rosetta.TextForId("dashboard.exit");
        }

    }

    private void BtnCalc_Click(object sender, RoutedEventArgs e)
    {   
        CalcStartView? calcStartView = App.ServiceProvider.GetService<CalcStartView>();
        if (calcStartView != null)
        {
            calcStartView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            calcStartView.ShowDialog();
        }
    }

    private void BtnExit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void BtnCharts_Click(object sender, RoutedEventArgs e)
    {
        ChartsStartView? chartsStartView = App.ServiceProvider.GetService<ChartsStartView>();
        if (chartsStartView != null)
        {
            chartsStartView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            chartsStartView.ShowDialog();
        }
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
        HelpWindow? helpWindow = App.ServiceProvider.GetService<HelpWindow>();
        if (helpWindow != null)
        {
            helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            helpWindow.SetUri("Dashboard");
            helpWindow.ShowDialog();
        }
    }
}

