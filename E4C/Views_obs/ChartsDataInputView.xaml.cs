// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using domain.shared;
using E4C.Models.UiHelpers;
using E4C.Shared.References;
using E4C.Ui.Charts;
using E4C.Ui.Shared;
using E4C.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace E4C.Views
{
    /// <summary>
    /// Interaction logic for ChartsDataInputNewChart.xaml
    /// </summary>
    public partial class ChartsDataInputView : Window
    {
        readonly private ChartsDataInputViewModel _viewModel;
        readonly private IRosetta _rosetta;
        private List<ChartCategoryDetails> _chartCategoryItems;
        private List<RoddenRatingDetails> _roddenRatingItems;
        private List<CalendarDetails> _calendarItems;
        private List<YearCountDetails> _yearCountItems;
        private List<TimeZoneDetails> _timeZoneDetails;
        public List<ChartCategoryDetails> ChartCategoryItems { get => _chartCategoryItems; set => _chartCategoryItems = value; }
        public List<RoddenRatingDetails> RoddenRatingItems { get => _roddenRatingItems; set => _roddenRatingItems = value; }
        public List<CalendarDetails> CalendarItems { get => _calendarItems; set => _calendarItems = value; }
        public List<YearCountDetails> YearCountItems { get => _yearCountItems; set => _yearCountItems = value; }
        public List<TimeZoneDetails> TimeZoneItems { get => _timeZoneDetails; set => _timeZoneDetails = value; }

        public ChartsDataInputView(ChartsDataInputViewModel viewModel, IRosetta rosetta)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _rosetta = rosetta;
            _chartCategoryItems = new List<ChartCategoryDetails>();
            _roddenRatingItems = new List<RoddenRatingDetails>();
            _timeZoneDetails = new List<TimeZoneDetails>();
            _calendarItems = new List<CalendarDetails>();
            _yearCountItems = new List<YearCountDetails>();
            PopulateStaticTexts();
            PopulateReferences();
        }


        private void RemoveErrorIndications()
        {
            ShowDateIsValid(true);
            ShowTimeIsValid(true);
            ShowGeoLongIsValid(true);
            ShowGeoLatIsValid(true);
            ShowLmtLongIsValid(true);
        }

        private void ShowDateIsValid(bool valid)
        {
            SolidColorBrush _brush = valid ? Brushes.Black : Brushes.Red;
            DateYearInput.Foreground = _brush;
            DateMonthInput.Foreground = _brush;
            DateDayInput.Foreground = _brush;
        }

        private void ShowTimeIsValid(bool valid)
        {
            SolidColorBrush _brush = valid ? Brushes.Black : Brushes.Red;
            TimeHourInput.Foreground = _brush;
            TimeMinuteInput.Foreground = _brush;
            TimeSecondInput.Foreground = _brush;
        }

        private void ShowGeoLongIsValid(bool valid)
        {
            SolidColorBrush _brush = valid ? Brushes.Black : Brushes.Red;
            LongDegreeInput.Foreground = _brush;
            LongMinuteInput.Foreground = _brush;
            LongSecondInput.Foreground = _brush;
        }

        private void ShowGeoLatIsValid(bool valid)
        {
            SolidColorBrush _brush = valid ? Brushes.Black : Brushes.Red;
            LatDegreeInput.Foreground = _brush;
            LatMinuteInput.Foreground = _brush;
            LatSecondInput.Foreground = _brush;
        }

        private void ShowLmtLongIsValid(bool valid)
        {
            SolidColorBrush _brush = valid ? Brushes.Black : Brushes.Red;
            LmtHourInput.Foreground = _brush;
            LmtMinuteInput.Foreground = _brush;
            LmtSecondInput.Foreground = _brush;
        }



        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            UpdateViewModel();
            RemoveErrorIndications();

            List<int> _errors = _viewModel.ValidateInput();
            if (_errors.Count > 0)
            {
                HandleErrors(_errors);
            }
            else
            {
                _viewModel.SignalNewChartInputCompleted();
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
                    ShowDateIsValid(false);
                }
                if (error == ErrorCodes.ERR_INVALID_TIME)
                {
                    _messageText += _rosetta.TextForId("common.error.time") + ".\n";
                    ShowTimeIsValid(false);
                }
                if (error == ErrorCodes.ERR_INVALID_GEOLON)
                {
                    _messageText += _rosetta.TextForId("common.error.geolong") + ".\n";
                    ShowGeoLongIsValid(false);
                }
                if (error == ErrorCodes.ERR_INVALID_GEOLAT)
                {
                    _messageText += _rosetta.TextForId("common.error.geolat") + ".\n";
                    ShowGeoLatIsValid(false);
                }
                if (error == ErrorCodes.ERR_INVALID_GEOLON_LMT)
                {
                    _messageText += _rosetta.TextForId("common.error.lmtlong") + ".\n";
                    ShowLmtLongIsValid(false);
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
            ChartCatText.Text = _rosetta.TextForId("charts.datainputchart.subject");
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
                    ChartCatInput.Items.Add(_rosetta.TextForId(_chartCategoryItem.TextId));
                }
                ChartCatInput.SelectedIndex = 0;
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
                    DateCalendarInput.Items.Add(_rosetta.TextForId(_calendarItem.TextId));
                }
                DateCalendarInput.SelectedIndex = 0;
            }
        }

        private void PopulateYearCountItems()
        {
            YearCountItems = _viewModel.YearCountItems;
            if (YearCountItems != null)
            {
                foreach (var _yearCountItem in YearCountItems)
                {
                    DateYearCountInput.Items.Add(_rosetta.TextForId(_yearCountItem.TextId));
                }
                DateYearCountInput.SelectedIndex = 0;
            }
        }

        private void PopulateTimeZoneItems()
        {
            TimeZoneItems = _viewModel.TimeZoneItems;
            if (TimeZoneItems != null)
            {
                foreach (var _timeZoneItem in TimeZoneItems)
                {
                    TimeZoneInput.Items.Add(_rosetta.TextForId(_timeZoneItem.TextId));
                }
                TimeZoneInput.SelectedIndex = 0;
            }
        }

        public void UpdateViewModel()
        {
            _viewModel.ChartCategoryIndex = ChartCatInput.SelectedIndex;
            _viewModel.RoddenRatingIndex = RatingInput.SelectedIndex;
            _viewModel.CalendarIndex = DateCalendarInput.SelectedIndex;
            _viewModel.YearCountIndex = DateYearCountInput.SelectedIndex;
            _viewModel.TimeZoneIndex = TimeZoneInput.SelectedIndex;
            _viewModel.InputName = NameInput.Text;
            _viewModel.InputSource = SourceInput.Text;
            _viewModel.InputDescription = DescriptionInput.Text;
            _viewModel.InputLocation = LocationNameInput.Text;
            _viewModel.InputGeoLong = new string[] { LongDegreeInput.Text, LongMinuteInput.Text, LongSecondInput.Text };
            _viewModel.InputGeoLat = new string[] { LatDegreeInput.Text, LatMinuteInput.Text, LatSecondInput.Text };
            _viewModel.InputDate = new string[] { DateYearInput.Text, DateMonthInput.Text, DateDayInput.Text };
            _viewModel.InputTime = new string[] { TimeHourInput.Text, TimeMinuteInput.Text, TimeSecondInput.Text };
            _viewModel.InputLmtOffset = new string[] { LmtHourInput.Text, LmtMinuteInput.Text, LmtSecondInput.Text };
            _viewModel.InputRbEastSelected = (bool)RbEast.IsChecked;
            _viewModel.InputRbLmtPlusSelected = (bool)RbZoneEast.IsChecked;
            _viewModel.InputRbNorthSelected = (bool)RbNorth.IsChecked;
            _viewModel.InputCbDstSelected = (bool)TimeDstInput.IsChecked;
            _viewModel.RetrieveComboBoxValues();
        }

    }




}
