// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
using E4C.ViewModels;
using E4C.Views.ViewHelpers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace E4C.Views
{
    /// <summary>
    /// Interaction logic for CalcJdView.xaml
    /// </summary>
    public partial class CalcJdView : Window
    {
        readonly private CalcJdViewModel _calcJdViewModel;
        readonly private IRosetta _rosetta;
        private List<CalendarDetails> _calendarItems;
        public List<CalendarDetails> CalendarItems { get => _calendarItems; set => _calendarItems = value; }
        private List<YearCountDetails>? _yearCountItems;
        public List<YearCountDetails> YearCountItems { get => _yearCountItems; set => _yearCountItems = value; }

        public CalcJdView(CalcJdViewModel calcJdViewModel, IRosetta rosetta)
        {
            InitializeComponent();
            _calcJdViewModel = calcJdViewModel;
            _rosetta = rosetta;
            PopulateStaticTexts();
        }

        private void PopulateStaticTexts()
        {
            Title = _rosetta.TextForId("calc.jdnr.title");
            FormTitle.Text = _rosetta.TextForId("calc.jdnr.formtitle");
            DateTime.Text = _rosetta.TextForId("calc.jdnr.datetime");
            Date.Text = _rosetta.TextForId("calc.jdnr.date");
            DateYear.Text = _rosetta.TextForId("calc.jdnr.year");
            DateMonth.Text = _rosetta.TextForId("calc.jdnr.month");
            DateDay.Text = _rosetta.TextForId("calc.jdnr.day");
            DateCalendar.Text = _rosetta.TextForId("calc.jdnr.calendar");
            DateYearCount.Text = _rosetta.TextForId("calc.jdnr.yearcount");
            Time.Text = _rosetta.TextForId("calc.jdnr.time");
            TimeHour.Text = _rosetta.TextForId("calc.jdnr.hour");
            TimeMinute.Text = _rosetta.TextForId("calc.jdnr.minute");
            TimeSecond.Text = _rosetta.TextForId("calc.jdnr.second");
            BtnCalculate.Content = _rosetta.TextForId("calc.jdnr.btncalc");
            Result.Text = _rosetta.TextForId("calc.jdnr.result");
            BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
            BtnClose.Content = _rosetta.TextForId("common.btnclose");
            ResultValue.Text = "";
            PopulateReferences();
        }

        private void PopulateReferences()
        {
            CalendarItems = _calcJdViewModel.CalendarItems;
            if (CalendarItems != null)
            {
                foreach (var _calendarItem in CalendarItems)
                {
                    DateCalendarCombo.Items.Add(_rosetta.TextForId(_calendarItem.TextId));
                }
                DateCalendarCombo.SelectedIndex = 0;
            }
            YearCountItems = _calcJdViewModel.YearCountItems;
            if (YearCountItems != null)
            {
                foreach (var _yearCountItem in YearCountItems)
                {
                    DateYearCountCombo.Items.Add(_rosetta.TextForId(_yearCountItem.TextId));
                }
                DateYearCountCombo.SelectedIndex = 0;
            }
        }


        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            UpdateViewModel();
            Date.Foreground = Brushes.Black;
            Time.Foreground = Brushes.Black;
            List<int> _errors = _calcJdViewModel.ValidateInput();
            if (_errors.Count > 0)
            {
                HandleErrors(_errors);
            }
            else
            {
                ResultValue.Text = _calcJdViewModel.CalculateJd();
            }
        }

        private void UpdateViewModel()
        {
            _calcJdViewModel.InputYear = DateYearValue.Text;
            _calcJdViewModel.InputMonth = DateMonthValue.Text;
            _calcJdViewModel.InputDay = DateDayValue.Text;
            _calcJdViewModel.InputHour = TimeHourValue.Text;
            _calcJdViewModel.InputMinute = TimeMinuteValue.Text;
            _calcJdViewModel.InputSecond = TimeSecondValue.Text;
            _calcJdViewModel.InputCalendar = _calendarItems[DateCalendarCombo.SelectedIndex].Calendar;
            _calcJdViewModel.InputYearCount = _yearCountItems[DateYearCountCombo.SelectedIndex].YearCount;
        }

        private void HandleErrors(List<int> errors)
        {
            string _messageText = _rosetta.TextForId("common.error.general") + ":\n";
            foreach (int error in errors)
            {
                if (error == ErrorCodes.ERR_INVALID_DATE)
                {
                    _messageText += _rosetta.TextForId("common.error.date") + ".\n";
                    Date.Foreground = Brushes.Red;
                }
                if (error == ErrorCodes.ERR_INVALID_TIME)
                {
                    _messageText += _rosetta.TextForId("common.error.time") + ".\n";
                    Time.Foreground = Brushes.Red;
                }

            }
            string _msgBoxTitle = _rosetta.TextForId("common.error.title");
            MessageBoxButton _buttons = MessageBoxButton.OK;
            MessageBoxImage _icon = MessageBoxImage.Error;
            MessageBox.Show(_messageText, _msgBoxTitle, _buttons, _icon);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }


}
