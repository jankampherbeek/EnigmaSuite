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
        readonly private CalcJdView calcJdView;

        public DashboardCalc(CalcJdView calcJdView)
        {
            InitializeComponent();
            this.calcJdView = calcJdView;
        }

        public void ShowCalcJd(object sender, RoutedEventArgs e)
        {
            calcJdView.Show();
        }
    }
       
}
