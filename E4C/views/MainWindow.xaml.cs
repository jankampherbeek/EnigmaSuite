using System.Windows;

namespace E4C.views
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


    }

    public class MainWindowViewModel
    {
        readonly private DashboardCalc dashboardCalc;

        public MainWindowViewModel(DashboardCalc dashboardCalc)
        {
            this.dashboardCalc = dashboardCalc;
        }

        public void ShowCalculations()
        {
            dashboardCalc.Show();
        }

    }
}
