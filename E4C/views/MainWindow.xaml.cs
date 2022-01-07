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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            base.DataContext = viewModel;
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
           viewModel.BtnCalc_Click(sender, e);
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

        public void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            dashboardCalc.Show();
        }

    }
}
