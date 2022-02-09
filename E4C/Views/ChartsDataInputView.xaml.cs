// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using System.Collections.Generic;
using E4C.Views.ViewHelpers;
using E4C.ViewModels;
using E4C.Models.Domain;

namespace E4C.Views
{
    /// <summary>
    /// Interaction logic for ChartsDataInputNewChart.xaml
    /// </summary>
    public partial class ChartsDataInputView : Window
    {
        readonly private ChartsDataInputViewModel _viewModel;
        readonly private IRosetta _rosetta;
        private string _generalName = "";
        private string _sourceValue = "";
        private List<ChartCategoryDetails> _chartCategoryItems;
        private List<RoddenRatingDetails> _roddenRatingItems;
        private List<CalendarDetails> _calendarItems;
        private List<YearCountDetails> _yearCountItems;
        private List<TimeZoneDetails> _timeZoneDetails;
        public List<ChartCategoryDetails> ChartCategoryItems { get => _chartCategoryItems; set => _chartCategoryItems = value; }
        public List<RoddenRatingDetails> RoddenRatingItems{ get => _roddenRatingItems; set => _roddenRatingItems = value; }
        public List<CalendarDetails > CalendarItems { get => _calendarItems; set => _calendarItems = value; }
        public List<YearCountDetails> YearCountItems { get => _yearCountItems; set => _yearCountItems = value; }
        public List<TimeZoneDetails> TimeZoneItems { get => _timeZoneDetails; set => _timeZoneDetails = value; }

        public ChartsDataInputView(ChartsDataInputViewModel viewModel, IRosetta rosetta)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _rosetta = rosetta;
            PopulateStaticTexts();
            PopulateReferences();
        }


        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            UpdateViewModel();


            //Date.Foreground = Brushes.Black;
            //Time.Foreground = Brushes.Black;
            List<int> _errors = _viewModel.ValidateInput();
            if (_errors.Count > 0)
            {
                HandleErrors(_errors);
            }
            else
            {
                // perform calculation
            }
        }

        private void HandleErrors(List<int> errors)
        {
            string _messageText = _rosetta.TextForId("common.error.general") + ":\n";
            foreach (int error in errors)
            {
                if (error == ErrorCodes.ERR_INVALID_DATE)
                {
                    _messageText += _rosetta.TextForId("common.error.date") + ".\n";
      //              Date.Foreground = Brushes.Red;
                }
                if (error == ErrorCodes.ERR_INVALID_TIME)
                {
                    _messageText += _rosetta.TextForId("common.error.time") + ".\n";
      //              Time.Foreground = Brushes.Red;
                }

            }
            string _msgBoxTitle = _rosetta.TextForId("common.error.title");
            MessageBoxButton _buttons = MessageBoxButton.OK;
            MessageBoxImage _icon = MessageBoxImage.Error;
            MessageBox.Show(_messageText, _msgBoxTitle, _buttons, _icon);
        }

        private void PopulateStaticTexts()
        {
            FormTitle.Text = _rosetta.TextForId("charts.datainputchart.titleform");
            GeneralTitle.Text = _rosetta.TextForId("charts.datainputchart.titlegeneral");
            NameText.Text = _rosetta.TextForId("charts.datainputchart.name");
            SubjectText.Text = _rosetta.TextForId("charts.datainputchart.subject");
            RatingText.Text = _rosetta.TextForId("charts.datainputchart.rating");
            SourceText.Text = _rosetta.TextForId("charts.datainputchart.source");
            DescriptionText.Text = _rosetta.TextForId("charts.datainputchart.description");
            LocationTitle.Text = _rosetta.TextForId("charts.datainputchart.locationtitle");
            LocationName.Text = _rosetta.TextForId("charts.datainputchart.locationname");
            LongitudeText.Text = _rosetta.TextForId("charts.datainputchart.longitude");
            LatitudeText.Text = _rosetta.TextForId("charts.datainputchart.latitude");
            LongDegreeText.Text = _rosetta.TextForId("charts.datainputchart.degree");
            LongMinuteText.Text = _rosetta.TextForId("charts.datainputchart.minute");
            LongSecondText.Text = _rosetta.TextForId("charts.datainputchart.second");
            LongEastText.Text = _rosetta.TextForId("charts.datainputchart.east");
            LongWestText.Text = _rosetta.TextForId("charts.datainputchart.west");
            LatDegreeText.Text = _rosetta.TextForId("charts.datainputchart.degree");
            LatMinuteText.Text = _rosetta.TextForId("charts.datainputchart.minute");
            LatSecondText.Text = _rosetta.TextForId("charts.datainputchart.second");
            LatNorthText.Text = _rosetta.TextForId("charts.datainputchart.north");
            LatSouthText.Text = _rosetta.TextForId("charts.datainputchart.south");
            DateTimeTitle.Text = _rosetta.TextForId("charts.datainputchart.datetimetitle");
            DateText.Text = _rosetta.TextForId("charts.datainputchart.date");
            DateYearText.Text = _rosetta.TextForId("charts.datainputchart.year");
            DateMonthText.Text = _rosetta.TextForId("charts.datainputchart.month");
            DateDayText.Text = _rosetta.TextForId("charts.datainputchart.day");
            DateCalendarText.Text = _rosetta.TextForId("charts.datainputchart.calendar");
            DateYearCount.Text = _rosetta.TextForId("charts.datainputchart.yearcount");
            TimeText.Text = _rosetta.TextForId("charts.datainputchart.time");
            TimeHourText.Text = _rosetta.TextForId("charts.datainputchart.hour");
            TimeMinuteText.Text = _rosetta.TextForId("charts.datainputchart.minute");
            TimeSecondText.Text = _rosetta.TextForId("charts.datainputchart.second");
            TimeDstText.Text = _rosetta.TextForId("charts.datainputchart.dst");
            TimeZoneText.Text = _rosetta.TextForId("charts.datainputchart.timezone");
            LmtZoneText.Text = _rosetta.TextForId("charts.datainputchart.lmtzone");
            LmtDegreeText.Text = _rosetta.TextForId("charts.datainputchart.degree");
            LmtMinuteText.Text = _rosetta.TextForId("charts.datainputchart.minute");
            LmtSecondText.Text = _rosetta.TextForId("charts.datainputchart.second");
            LmtDirEastText.Text = _rosetta.TextForId("charts.datainputchart.east");
            LmtDirWestText.Text = _rosetta.TextForId("charts.datainputchart.west");
            BtnCalculate.Content = _rosetta.TextForId("charts.datainputchart.btncalculate");
            BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
            BtnCancel.Content = _rosetta.TextForId("common.btncancel");
        }

        private void PopulateReferences()
        {
            PopulateChartCategoryItems();
            PopulateRoddenRatingItems();
            PopulateCalendarItems();
            PopulateYearCountItems();
            PopulateTimeZoneItems();
        }

        private void PopulateChartCategoryItems()
        {
            ChartCategoryItems = _viewModel.ChartCategoryItems;
            if (ChartCategoryItems != null)
            {
                foreach (var _chartCategoryItem in ChartCategoryItems)
                {
                    SubjectInput.Items.Add(_rosetta.TextForId(_chartCategoryItem.TextId));
                }
                SubjectInput.SelectedIndex = 0;
            }
        }

        private void PopulateRoddenRatingItems()
        {
            RoddenRatingItems = _viewModel.RoddenRatingItems;
            if (RoddenRatingItems != null)
            {
                foreach (var _roddenRatingItem in RoddenRatingItems)
                {
                    RatingInput.Items.Add(_rosetta.TextForId(_roddenRatingItem.TextId));
                }
                RatingInput.SelectedIndex = 0;
            }
        }

        private void PopulateCalendarItems()
        {
            CalendarItems = _viewModel.CalendarItems;
            if (CalendarItems != null)
            {
                foreach (var _calendarItem in CalendarItems)
                {
                    DateCalendarValue.Items.Add(_rosetta.TextForId(_calendarItem.TextId));
                }
                DateCalendarValue.SelectedIndex = 0;
            }
        }

        private void PopulateYearCountItems()
        {
            YearCountItems = _viewModel.YearCountItems;
            if (YearCountItems != null)
            {
                foreach (var _yearCountItem in YearCountItems)
                {
                    DateYearCountValue.Items.Add(_rosetta.TextForId(_yearCountItem.TextId));
                }
                DateYearCountValue.SelectedIndex = 0;
            }
        }

        private void PopulateTimeZoneItems()
        {
            TimeZoneItems = _viewModel.TimeZoneItems;
            if (TimeZoneItems != null)
            {
                foreach (var _timeZoneItem in TimeZoneItems)
                {
                    TimeZoneValue.Items.Add(_rosetta.TextForId(_timeZoneItem.TextId));  
                }
                TimeZoneValue.SelectedIndex = 0;
            }
        }

        public void UpdateViewModel()
        {
            _viewModel.ChartCategoryIndex = SubjectInput.SelectedIndex;
            _viewModel.RoddenRatingIndex = RatingInput.SelectedIndex;
            _viewModel.CalendarIndex = DateCalendarValue.SelectedIndex;
            _viewModel.YearCountIndex = DateYearCountValue.SelectedIndex;
            _viewModel.TimeZoneIndex = TimeZoneValue.SelectedIndex;
            _viewModel.InputName = NameInput.Text;
            _viewModel.InputSource = SourceInput.Text;
            _viewModel.InputDescription = DescriptionInput.Text;
            _viewModel.InputLocation = LocationNameInput.Text;
            _viewModel.InputLongDegrees = LongDegreeValue.Text;
            _viewModel.InputLongMinutes = LongMinuteValue.Text;
            _viewModel.InputLongSeconds = LongSecondsValue.Text;
            _viewModel.InputRbEastSelected = RbEast.IsChecked;
            _viewModel.InputLatDegrees = LatDegreeValue.Text;
            _viewModel.InputLatMinutes = LatMinuteValue.Text;
            _viewModel.InputLatSeconds = LatSecondsValue.Text;
            _viewModel.InputRbNorthSelected = RbNorth.IsChecked;
            _viewModel.InputYear = DateYearValue.Text;
            _viewModel.InputMonth = DateMonthValue.Text;
            _viewModel.InputDay = DateDayValue.Text;
            _viewModel.InputHour = TimeHourValue.Text;
            _viewModel.InputMinute = TimeMinuteValue.Text;
            _viewModel.InputSecond = TimeSecondValue.Text;
            _viewModel.InputCbDstSelected = TimeDstValue.IsChecked;
            _viewModel.InputLmtLongDegrees = LmtDegreeValue.Text;
            _viewModel.InputLmtLongMinutes = LmtMinuteValue.Text;
            _viewModel.InputLmtLongSeconds = LmtSecondValue.Text;
            _viewModel.InputRbLmtEastSelected = RbZoneEast.IsChecked;
            _viewModel.RetrieveComboBoxValues();
        }

    }




}
