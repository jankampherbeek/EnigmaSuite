// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Ui.Calculators;
using E4C.Ui.Charts;
using System.Windows;

namespace E4C.Ui.Shared;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    readonly private MainWindowViewModel _viewModel;

    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        PopulateStaticTexts();
    }

    private void PopulateStaticTexts()
    {
        Title = "Enigma Suite";
        FormTitle.Text = "Enigma Suite 2022.0";
        btnCharts.Content = "Charts";
        btnCalc.Content = "Calculations";
        btnExit.Content = "Exit and close Enigma";
    }

    private void BtnCalc_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.ShowCalculations();
    }

    private void BtnExit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void BtnCharts_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.ShowCharts();
    }
}

public class MainWindowViewModel
{
    readonly private CalcStartView _dashboardCalc;
    readonly private DashboardCharts _dashboardCharts;

    public MainWindowViewModel(CalcStartView dashboardCalc, DashboardCharts dashboardCharts)
    {
        _dashboardCharts = dashboardCharts;
        _dashboardCalc = dashboardCalc;

    }

    public void ShowCalculations()
    {
        _dashboardCalc.Show();
    }

    public void ShowCharts()
    {
        _dashboardCharts.Show();
    }

}
