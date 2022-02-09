// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;

namespace E4C.Views
{

    /// <summary>
    /// Interaction logic for DashboardCharts.xaml
    /// </summary>
    public partial class DashboardCharts : Window
    {

        readonly private DashboardChartsViewModel _dashboardChartsViewModel;

        public DashboardCharts(DashboardChartsViewModel dashboardChartsViewModel)
        {
            InitializeComponent();
            _dashboardChartsViewModel = dashboardChartsViewModel;
        }

        private void BtnNewChart_Click(object sender, RoutedEventArgs e)
        {
            _dashboardChartsViewModel.HandleNewChart();
        }
    }

    public class DashboardChartsViewModel

    {
        readonly private ChartsDataInputView _chartsDataInputNewChart;

        public DashboardChartsViewModel(ChartsDataInputView chartsDataInputNewChart)
        {
            _chartsDataInputNewChart = chartsDataInputNewChart;
        }

        public void HandleNewChart()
        {
            _chartsDataInputNewChart.Show();
        }


    }
}
