using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace E4C.views
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
