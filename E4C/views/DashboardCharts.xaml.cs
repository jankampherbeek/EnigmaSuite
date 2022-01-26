using System.Windows;

namespace E4C.Views
{

    /// <summary>
    /// Interaction logic for DashboardCharts.xaml
    /// </summary>
    public partial class DashboardCharts : Window
    {

        readonly private DashboardChartsViewModel dashboardChartsViewModel;

        public DashboardCharts(DashboardChartsViewModel dashboardChartsViewModel)
        {
            InitializeComponent();
            this.dashboardChartsViewModel = dashboardChartsViewModel;
        }

        private void BtnNewChart_Click(object sender, RoutedEventArgs e)
        {
            dashboardChartsViewModel.HandleNewChart();
        }
    }

    public class DashboardChartsViewModel

    {
        readonly private ChartsDataInputNewChart chartsDataInputNewChart;

        public DashboardChartsViewModel(ChartsDataInputNewChart chartsDataInputNewChart)
        {
            this.chartsDataInputNewChart = chartsDataInputNewChart;
        }

        public void HandleNewChart()
        {
            chartsDataInputNewChart.Show();
        }


    }
}
