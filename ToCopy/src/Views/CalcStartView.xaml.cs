// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.UiHelpers;
using E4C.ViewModels;
using System.Windows;

namespace E4C.Views
{
    /// <summary>
    /// Interaction logic for CalcStartView.xaml
    /// </summary>
    public partial class CalcStartView : Window
    {
        readonly private ICalcStartViewModel _viewModel;
        readonly private IRosetta _rosetta;

        public CalcStartView(ICalcStartViewModel viewModel, IRosetta rosetta)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _rosetta = rosetta;
            PopulateStaticTexts();
        }

        private void PopulateStaticTexts()
        {
            Title = _rosetta.TextForId("calc.startview.title");
            FormTitle.Text = _rosetta.TextForId("calc.startview.formtitle");
            CalcDashboardExplanation.Text = _rosetta.TextForId("calc.startview.explanation");
            BtnCalcJd.Content = _rosetta.TextForId("calc.startview.btnjd");
            BtnCalcObliquity.Content = _rosetta.TextForId("calc.startview.btnobliquity");
            BtnClose.Content = _rosetta.TextForId("calc.startview.btnclose");
            BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
        }

        private void BtnCalcJd_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowCalcJd();
        }

        private void BtnCalcObliquity_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowCalcObliquity();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }




}
