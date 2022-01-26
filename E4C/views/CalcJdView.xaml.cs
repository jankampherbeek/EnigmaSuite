// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
using E4C.ViewModels;
using E4C.Views.ViewHelpers;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Media;

namespace E4C.Views
{
    /// <summary>
    /// Interaction logic for CalcJdView.xaml
    /// </summary>
    public partial class CalcJdView : Window
    {
        readonly private CalcJdViewModel calcJdViewModel;
        readonly private IRosetta rosetta;
        private List<CalendarDetails> calendarItems;
        public List<CalendarDetails> CalendarItems { get => calendarItems; set => calendarItems = value; }
        private List<YearCountDetails>? yearCountItems;
        public List <YearCountDetails> YearCountItems { get => yearCountItems; set => yearCountItems = value; }

        public CalcJdView(CalcJdViewModel calcJdViewModel, IRosetta rosetta)
        {
            InitializeComponent();
            this.calcJdViewModel = calcJdViewModel;
            this.rosetta = rosetta;
            PopulateStaticTexts();
        }

        private void PopulateStaticTexts()
        {
            Title = rosetta.TextForId("calc.jdnr.title");
            FormTitle.Text = rosetta.TextForId("calc.jdnr.formtitle");
            DateTime.Text = rosetta.TextForId("calc.jdnr.datetime");
            Date.Text = rosetta.TextForId("calc.jdnr.date");
            DateYear.Text = rosetta.TextForId("calc.jdnr.year");
            DateMonth.Text = rosetta.TextForId("calc.jdnr.month");
            DateDay.Text = rosetta.TextForId("calc.jdnr.day");
            DateCalendar.Text = rosetta.TextForId("calc.jdnr.calendar");
            DateYearCount.Text = rosetta.TextForId("calc.jdnr.yearcount");
            Time.Text = rosetta.TextForId("calc.jdnr.time");
            TimeHour.Text = rosetta.TextForId("calc.jdnr.hour");
            TimeMinute.Text = rosetta.TextForId("calc.jdnr.minute");
            TimeSecond.Text = rosetta.TextForId("calc.jdnr.second");
            BtnCalculate.Content = rosetta.TextForId("calc.jdnr.btncalc");
            Result.Text = rosetta.TextForId("calc.jdnr.result");
            BtnHelp.Content = rosetta.TextForId("common.btnhelp");
            BtnClose.Content = rosetta.TextForId("common.btnclose");
            ResultValue.Text = "";
            PopulateReferences();
        }

        private void PopulateReferences()
        {
            CalendarItems = calcJdViewModel.CalendarItems;
            if (CalendarItems != null)
            {
                foreach (var calendarItem in CalendarItems)
                {
                    DateCalendarCombo.Items.Add(rosetta.TextForId(calendarItem.textId));
                }
                DateCalendarCombo.SelectedIndex = 0;
            }
            YearCountItems = calcJdViewModel.YearCountItems;
            if (YearCountItems != null)
            {
                foreach (var yearCountItem in YearCountItems)
                {
                    DateYearCountCombo.Items.Add(rosetta.TextForId(yearCountItem.textId));
                }
                DateYearCountCombo.SelectedIndex = 0;
            }
        }


        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            UpdateViewModel();
            Date.Foreground = Brushes.Black;
            Time.Foreground = Brushes.Black;
            List<int> errors = calcJdViewModel.ValidateInput();
            if (errors.Count > 0)
            {
                HandleErrors(errors);
            } 
            else
            {
                ResultValue.Text = calcJdViewModel.CalculateJd();
            }
        }

        private void UpdateViewModel()
        {
            calcJdViewModel.InputYear = DateYearValue.Text;
            calcJdViewModel.InputMonth = DateMonthValue.Text;
            calcJdViewModel.InputDay = DateDayValue.Text;
            calcJdViewModel.InputHour = TimeHourValue.Text;
            calcJdViewModel.InputMinute = TimeMinuteValue.Text;
            calcJdViewModel.InputSecond = TimeSecondValue.Text;
            calcJdViewModel.InputCalendar = calendarItems[DateCalendarCombo.SelectedIndex].calendar;
            calcJdViewModel.InputYearCount = yearCountItems[DateYearCountCombo.SelectedIndex].yearCount;
        }

        private void HandleErrors(List<int> errors)
        {
            string MessageText = rosetta.TextForId("common.error.general") + ":\n";
            foreach (int error in errors)
            { 
                if (error == ErrorCodes.ERR_INVALID_DATE)
                {
                    MessageText +=  rosetta.TextForId("common.error.date") + ".\n";
                    Date.Foreground = Brushes.Red;        
                }
                if (error == ErrorCodes.ERR_INVALID_TIME)
                {
                    MessageText += rosetta.TextForId("common.error.time") + ".\n";
                    Time.Foreground = Brushes.Red;
                }
                    
            }
            string msgBoxTitle = rosetta.TextForId("common.error.title");
            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBox.Show(MessageText, msgBoxTitle, buttons, icon);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }


}
