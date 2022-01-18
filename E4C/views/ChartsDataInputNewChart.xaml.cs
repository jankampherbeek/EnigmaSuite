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
    /// Interaction logic for ChartsDataInputNewChart.xaml
    /// </summary>
    public partial class ChartsDataInputNewChart : Window
    {
        readonly private ChartsDataInputNewChartViewModel viewModel;

        public ChartsDataInputNewChart(ChartsDataInputNewChartViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            
        }
    }

    public class ChartsDataInputNewChartViewModel
    {
        public ChartsDataInputNewChartViewModel()
        {
     
        }


    }


}
