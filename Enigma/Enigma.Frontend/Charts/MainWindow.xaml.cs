// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Enigma.Frontend.Charts
{
    /// <summary>Main view for the application.</summary>
    public partial class MainWindow : Window
    {
        private IRosetta _rosetta;
        private MainController _controller;

        public MainWindow(MainController controller, IRosetta rosetta)
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
            _controller.NewChart();
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
