// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Astron;
using E4C.Models.Domain;
using E4C.Models.Validations;
using E4C.ViewModels;
using E4C.Models.UiHelpers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace E4C.Views
{
    /// <summary>
    /// Interaction logic for CalcObliquityView.xaml
    /// </summary>
    public partial class CalcObliquityView : Window
    {
        readonly private CalcObliquityViewModel _calcObliquityViewModel;
        readonly private IRosetta _rosetta;
        private List<CalendarDetails> _calendarItems;
        public List<CalendarDetails> CalendarItems { get => _calendarItems; set => _calendarItems = value; }
        private List<YearCountDetails> _yearCountItems;
        public List<YearCountDetails> YearCountItems { get => _yearCountItems; set => _yearCountItems = value; }


        public CalcObliquityView(CalcObliquityViewModel calcObliquityViewModel, IRosetta rosetta)
        {
            InitializeComponent();
            _calcObliquityViewModel = calcObliquityViewModel;
            _rosetta = rosetta;
            _calendarItems = new List<CalendarDetails>();
            _yearCountItems = new List<YearCountDetails>();
            rbtrueobl.IsChecked = true;
            PopulateStaticTexts();
            PopulateReferences();
        }

        private void PopulateStaticTexts()
        {
            Title = _rosetta.TextForId("calc.obliquity.title");
            FormTitle.Text = _rosetta.TextForId("calc.obliquity.formtitle");
            Date.Text = _rosetta.TextForId("calc.obliquity.date");
            DateYear.Text = _rosetta.TextForId("calc.obliquity.year");
            DateMonth.Text = _rosetta.TextForId("calc.obliquity.month");
            DateDay.Text = _rosetta.TextForId("calc.obliquity.day");
            DateCalendar.Text = _rosetta.TextForId("calc.obliquity.calendar");
            DateYearCount.Text = _rosetta.TextForId("calc.obliquity.yearcount");
            ObliquityType.Text = _rosetta.TextForId("calc.obliquity.type");
            rbtrueobl.Content = _rosetta.TextForId("calc.obliquity.true");
            rbmeanobl.Content = _rosetta.TextForId("calc.obliquity.mean");
            BtnCalculate.Content = _rosetta.TextForId("calc.obliquity.btncalc");
            Result.Text = _rosetta.TextForId("calc.obliquity.result");
            ResultValue.Text = "";
            BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
            BtnClose.Content = _rosetta.TextForId("common.btnclose");

        }


        private void PopulateReferences()
        {
            CalendarItems = _calcObliquityViewModel.CalendarItems;
            if (CalendarItems != null)
            {
                foreach (var _calendarItem in CalendarItems)
                {
                    DateCalendarCombo.Items.Add(_rosetta.TextForId(_calendarItem.TextId));
                }
                DateCalendarCombo.SelectedIndex = 0;
            }
            YearCountItems = _calcObliquityViewModel.YearCountItems;
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
                List<int> _errors = _calcObliquityViewModel.ValidateInput();
                if (_errors.Count > 0)
                {
                    HandleErrors(_errors);
                }
                else
                {
                    ResultValue.Text = _calcObliquityViewModel.CalculateObliquity();
                }
        }

        private void UpdateViewModel()
        {
            _calcObliquityViewModel.InputDate = new string[] { DateYearInput.Text, DateMonthInput.Text, DateDayInput.Text };
            _calcObliquityViewModel.InputCalendar = _calendarItems[DateCalendarCombo.SelectedIndex].Calendar;
            _calcObliquityViewModel.InputYearCount = _yearCountItems[DateYearCountCombo.SelectedIndex].YearCount;
            _calcObliquityViewModel.UseTrueObliquity = (bool)rbtrueobl.IsChecked;
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
