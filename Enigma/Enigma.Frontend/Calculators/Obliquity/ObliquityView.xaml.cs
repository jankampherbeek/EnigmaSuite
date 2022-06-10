// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Frontend.Support;
using Enigma.Frontend.UiDomain;
using System.Windows;
using System.Windows.Media;

namespace Enigma.Frontend.Calculators.Obliquity
{
    /// <summary>View for Obliquity Calculator.</summary>
    public partial class ObliquityView : Window
    {
        private readonly string EMPTY_STRING = "";
        private IRosetta _rosetta;
        private HelpWindow _helpWindow;
        private ObliquityController _controller;
        private ObliquityResult _obliquityResult;

        public ObliquityView(IRosetta rosetta, ObliquityController controller, HelpWindow helpWindow)
        {
            InitializeComponent();
            _rosetta = rosetta;
            _controller = controller;
            _helpWindow = helpWindow;
            PopulateTexts();
        }

        public void CalcClick(object sender, RoutedEventArgs e)
        {
            DateInputValue.Background = Brushes.White;
            _controller.InputDate = DateInputValue.Text;
            bool calculationOk = _controller.ProcessInput();
            if (calculationOk)
            {
                _obliquityResult = _controller.Result;
                tbOblResultMeanValue.Text = _obliquityResult.ObliquityMeanText;
                tbOblResultTrueValue.Text = _obliquityResult.ObliquityTrueText;
            }
            else
            {
                if (_controller._errorCodes.Contains(ErrorCodes.ERR_INVALID_DATE))
                {
                    DateInputValue.Background = Brushes.Yellow;
                }
            }
        }

        public void ResetClick(object sender, RoutedEventArgs e)
        {
            DateInputValue.Text = EMPTY_STRING;
            rbGregorian.IsChecked = true;
            rbHistorical.IsChecked = true;
            CleanResultTexts();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            _helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _helpWindow.SetUri("CalcObliquity");
            _helpWindow.ShowDialog();
        }


        private void PopulateTexts()
        {
            FormTitle.Text = _rosetta.TextForId("calc.obliquity.formtitle");
            DateInputTxt.Text = _rosetta.TextForId("common.dateinput");
            CalendarTxt.Text = _rosetta.TextForId("common.calendarinput");
            rbJulian.Content = _rosetta.TextForId("common.calendar.rb.jul");
            rbGregorian.Content = _rosetta.TextForId("common.calendar.rb.greg");
            BtnCalc.Content = _rosetta.TextForId("common.btncalc");
            BtnClose.Content = _rosetta.TextForId("common.btnclose");
            BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
            BtnReset.Content = _rosetta.TextForId("common.btnreset");
            tbOblResultMeanTxt.Text = _rosetta.TextForId("calc.obliquity.result.mean");
            tbOblResultTrueTxt.Text = _rosetta.TextForId("calc.obliquity.result.true");
            tbYearCountTxt.Text = _rosetta.TextForId("common.yearcountinput");
            rbAstronomical.Content = _rosetta.TextForId("common.yearcount.rb.astron");
            rbHistorical.Content = _rosetta.TextForId("common.yearcount.rb.hist");
            CleanResultTexts();
        }

        private void CleanResultTexts()
        {
            tbOblResultMeanValue.Text = EMPTY_STRING;
            tbOblResultTrueValue.Text = EMPTY_STRING;
        }



    }
}
