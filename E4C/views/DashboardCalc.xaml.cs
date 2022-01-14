using System.Windows;

namespace E4C.views
{
    /// <summary>
    /// Interaction logic for DashboardCalc.xaml
    /// </summary>
    public partial class DashboardCalc : Window
    {
        readonly private DashboardCalcViewModel dashboardCalcViewModel;


        public DashboardCalc(DashboardCalcViewModel dashboardCalcViewModel)
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

        private void BtnCalcObliquity_Click(object sender, RoutedEventArgs e)
        {
            dashboardCalcViewModel.ShowCalcObliquity();
        }
    }

    public class DashboardCalcViewModel
    {
        readonly private CalcJdView calcJdView;
        readonly private CalcObliquityView calcOblView;

        public DashboardCalcViewModel(CalcJdView calcJdView, CalcObliquityView calcOblView)
        {
            this.calcJdView = calcJdView;
            this.calcOblView = calcOblView;
        }

        public void ShowCalcJd()
        {
            calcJdView.Show();
        }

        public void ShowCalcObliquity()
        {
            calcOblView.Show();
        }

    }


}
