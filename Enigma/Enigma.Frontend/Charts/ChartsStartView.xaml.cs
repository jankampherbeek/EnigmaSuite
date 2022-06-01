using Enigma.Frontend.Support;
using System.Windows;

namespace Enigma.Frontend.Charts
{
    /// <summary>
    /// Interaction logic for ChartsStartWindow.xaml
    /// </summary>
    public partial class ChartsStartView : Window
    {

        private readonly string EMPTY_STRING = "";
        private IRosetta _rosetta;
        private HelpWindow _helpWindow;
        private ChartDataInputView _dataInputView;

        public ChartsStartView(ChartDataInputView dataInputView, IRosetta rosetta, HelpWindow helpWindow)
        {
            InitializeComponent();
            _dataInputView = dataInputView;
            _rosetta = rosetta;
            _helpWindow = helpWindow;
            PopulateTexts();
        }


        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            _helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _helpWindow.SetUri("ChartsDashboard");
            _helpWindow.ShowDialog();
        }

        private void NewChartClick(object sender, RoutedEventArgs e)
        {
            _dataInputView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _dataInputView.ShowDialog();
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
