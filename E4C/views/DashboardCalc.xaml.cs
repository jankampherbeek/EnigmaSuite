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
using E4C.be.model;
using E4C.be.astron;
using E4C.be.sefacade;

namespace E4C.views
{
    /// <summary>
    /// Interaction logic for DashboardCalc.xaml
    /// </summary>
    public partial class DashboardCalc : Window
    {
        readonly private DashboardCalcViewModel dashboardCalcViewModel;


        public DashboardCalc(DashboardCalcViewModel dashboardCalcViewModel,  CalcJdView calcJdView)
        {
            InitializeComponent();
            this.dashboardCalcViewModel = dashboardCalcViewModel;
            PopulateStaticTexts();
        }

        private void PopulateStaticTexts()
        {
            Title = "Enigma Calculations";
            FormTitle.Text = "Calculations";
            CalcDashboardExplanation.Content = "Select and click one of the calculations";
            BtnCalcJd.Content = "Julian Day Number";
            BtnCalcObliquity.Content = "Obliquity of the earth axis";
            BtnClose.Content = "Close Calculations";
        }

        private void BtnCalcJd_Click(object sender, RoutedEventArgs e)
        {
            dashboardCalcViewModel.ShowCalcJd();
        }
    }

    public class DashboardCalcViewModel
        {
        readonly private CalcJdView calcJdView;

        public DashboardCalcViewModel(CalcJdView calcJdView)
        {
            this.calcJdView = calcJdView;
        }

        public void ShowCalcJd()
        {
            calcJdView.Show();
        }

    }

      
}
