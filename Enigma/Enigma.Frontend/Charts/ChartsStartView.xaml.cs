using Enigma.Frontend.Support;
using Enigma.Frontend.UiDomain;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Enigma.Frontend.Charts
{
    /// <summary>
    /// Interaction logic for ChartsStartWindow.xaml
    /// </summary>
    public partial class ChartsStartView : Window
    {
        private IRosetta _rosetta;
        private ChartsStartController _controller;

        public ChartsStartView(ChartsStartController controller, IRosetta rosetta )
        {
            InitializeComponent();
            _controller = controller;
            _rosetta = rosetta;
            PopulateTexts();
        }


        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            HelpWindow? helpWindow = App.ServiceProvider.GetService<HelpWindow>();
            if (helpWindow != null)
            {
                helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                helpWindow.SetUri("ChartsDashboard");
                helpWindow.ShowDialog();
            }
        }

        private void NewChartClick(object sender, RoutedEventArgs e)
        {
            ChartDataInputView? chartDataInputView = App.ServiceProvider.GetService<ChartDataInputView>();
            if (chartDataInputView != null)
            {
                chartDataInputView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                chartDataInputView.ShowDialog();
    //            CurrentCharts allCurrentCharts = _controller.AllCurrentCharts;
    //            if (allCurrentCharts.IdPrimaryChart > -1)
    //            {
    //                ShowPositions();
    //            }
            }
        }

        private void ShowPositions()
        {
            ChartPositions? chartPositions = App.ServiceProvider.GetService<ChartPositions>();
            if (chartPositions != null)
            { 
             //   chartPositions.ActiveChart = 
                chartPositions.PopulateHouses();
                chartPositions.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                chartPositions.ShowDialog();
            }
        }


        private void PopulateTexts()
        {
            FormTitle.Text = _rosetta.TextForId("charts.startview.formtitle");
            ModesSubTitle.Text = _rosetta.TextForId("charts.startview.modessubtitle");
            ChartsSubTitle.Text = _rosetta.TextForId("charts.startview.chartssubtitle");
            ActiveChartsSubTitle.Text = _rosetta.TextForId("charts.startview.activechartssubtitle");
            textSearchCriteria.Text = _rosetta.TextForId("charts.startview.searchcriteria");
            textWorkMode.Text = _rosetta.TextForId("charts.startview.selectmode");
            BtnNewChart.Content = _rosetta.TextForId("charts.startview.newchart");
            BtnDetailedSearch.Content = _rosetta.TextForId("common.btndetailedsearch");
            BtnSearch.Content = _rosetta.TextForId("common.btnsearch");
            BtnManageCharts.Content = _rosetta.TextForId("charts.startview.managecharts");
            BtnManageModes.Content = _rosetta.TextForId("charts.startview.managemodes");
            BtnClose.Content = _rosetta.TextForId("common.btnclose");
            BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
            PopulateModes();
        }

        private void PopulateModes()
        {
            comboModes.Items.Clear();
            comboModes.Items.Add("Standard Western Astrology");
            comboModes.Items.Add("School of Ram");
            comboModes.SelectedIndex = 0;
        }

    }
}
