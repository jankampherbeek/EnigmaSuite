// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using E4C.Models;
using E4C.ViewModels;
using E4C.Views.ViewHelpers;

namespace E4C.Views
{
    /// <summary>
    /// Interaction logic for CalcStartView.xaml
    /// </summary>
    public partial class CalcStartView : Window
    {
        readonly private ICalcStartViewModel viewModel;
        readonly private IRosetta rosetta;

        public CalcStartView(ICalcStartViewModel viewModel, IRosetta rosetta)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.rosetta = rosetta;
            PopulateStaticTexts();
        }

        private void PopulateStaticTexts()
        {
            Title = rosetta.TextForId("calc.startview.title"); 
            FormTitle.Text = rosetta.TextForId("calc.startview.formtitle"); 
            CalcDashboardExplanation.Text = rosetta.TextForId("calc.startview.explanation"); 
            BtnCalcJd.Content = rosetta.TextForId("calc.startview.btnjd");
            BtnCalcObliquity.Content = rosetta.TextForId("calc.startview.btnobliquity");
            BtnClose.Content = rosetta.TextForId("calc.startview.btnclose");
            BtnHelp.Content = rosetta.TextForId("common.btnhelp");
        }

        private void BtnCalcJd_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowCalcJd();
        }

        private void BtnCalcObliquity_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowCalcObliquity();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }




}
