// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.DateTime;
using Enigma.Frontend.Support;
using Enigma.Frontend.UiDomain;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Enigma.Frontend.Calculators.Obliquity
{
    /// <summary>View for Obliquity Calculator.</summary>
    public partial class ObliquityView : Window
    {
        private readonly string EMPTY_STRING = "";
        private IRosetta _rosetta;
        private ObliquityController _controller;
        private ObliquityResult _obliquityResult;
        private List<CalendarDetails> _calendarDetails;
        private List<YearCountDetails> _yearCountDetails;

        public ObliquityView(IRosetta rosetta, ObliquityController controller, ICalendarSpecifications calendarSpecifications, IYearCountSpecifications yearCountSpecifications)
        {
            InitializeComponent();
            _rosetta = rosetta;
            _controller = controller;
            _calendarDetails = calendarSpecifications.AllCalendarDetails();
            _yearCountDetails = yearCountSpecifications.AllDetailsForYearCounts();
            PopulateTexts();
            PopulateCalendars();
            PopulateYearCounts();
        }

        public void CalcClick(object sender, RoutedEventArgs e)
        {
            DateInputValue.Background = Brushes.White;
            _controller.InputDate = DateInputValue.Text;
            _controller.Calendar = _calendarDetails[comboCalendar.SelectedIndex].Calendar;
            _controller.YearCount = _yearCountDetails[comboYearCount.SelectedIndex].YearCount;
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
            comboCalendar.SelectedIndex = 0;
            comboYearCount.SelectedIndex = 0;
            CleanResultTexts();
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
                helpWindow.SetUri("CalcObliquity");
                helpWindow.ShowDialog();
            }
        }


        private void PopulateTexts()
        {
            FormTitle.Text = _rosetta.TextForId("calc.obliquity.formtitle");
            DateInputTxt.Text = _rosetta.TextForId("common.date");
            CalendarTxt.Text = _rosetta.TextForId("common.calendar.full");
            BtnCalc.Content = _rosetta.TextForId("common.btncalc");
            BtnClose.Content = _rosetta.TextForId("common.btnclose");
            BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
            BtnReset.Content = _rosetta.TextForId("common.btnreset");
            tbOblResultMeanTxt.Text = _rosetta.TextForId("calc.obliquity.result.mean");
            tbOblResultTrueTxt.Text = _rosetta.TextForId("calc.obliquity.result.true");
            tbYearCountTxt.Text = _rosetta.TextForId("common.yearcount");
            CleanResultTexts();
        }

        private void PopulateCalendars()
        {
            comboCalendar.Items.Clear();
            foreach (var calendarDetail in _calendarDetails)
            {
                comboCalendar.Items.Add(_rosetta.TextForId(calendarDetail.TextIdFull));
            }
            comboCalendar.SelectedIndex = 0;
        }

        private void PopulateYearCounts()
        {
            comboYearCount.Items.Clear();
            foreach (var yearCountDetail in _yearCountDetails)
            {
                comboYearCount.Items.Add(_rosetta.TextForId(yearCountDetail.TextId));
            }
            comboYearCount.SelectedIndex = 0;
        }


        private void CleanResultTexts()
        {
            tbOblResultMeanValue.Text = EMPTY_STRING;
            tbOblResultTrueValue.Text = EMPTY_STRING;
        }



    }
}
