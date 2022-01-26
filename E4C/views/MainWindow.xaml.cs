using System.Windows;

namespace E4C.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly private MainWindowViewModel viewModel;

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
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
            viewModel.ShowCalculations();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnCharts_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowCharts();
        }
    }

    public class MainWindowViewModel
    {
        readonly private CalcStartView dashboardCalc;
        readonly private DashboardCharts dashboardCharts;

        public MainWindowViewModel(CalcStartView dashboardCalc, DashboardCharts dashboardCharts)
        {
            this.dashboardCharts = dashboardCharts;
            this.dashboardCalc = dashboardCalc;

        }

        public void ShowCalculations()
        {
            dashboardCalc.Show();
        }

        public void ShowCharts()
        {
            dashboardCharts.Show();
        }

    }
}
