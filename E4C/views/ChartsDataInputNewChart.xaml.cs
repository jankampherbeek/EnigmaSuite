// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Views.ViewHelpers;
using System.Windows;

namespace E4C.Views
{
    /// <summary>
    /// Interaction logic for ChartsDataInputNewChart.xaml
    /// </summary>
    public partial class ChartsDataInputNewChart : Window
    {
        readonly private ChartsDataInputNewChartViewModel _viewModel;
        readonly private IRosetta _rosetta;
        private string _generalName = "";
        private string _sourceValue = "";

        public ChartsDataInputNewChart(ChartsDataInputNewChartViewModel viewModel, IRosetta rosetta)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _rosetta = rosetta;
            PopulateStaticTexts();
        }


        public string GeneralName
        {
            get { return _generalName; }
            set { _generalName = value; }
        }

        public string SourceValue
        {
            get { return _sourceValue; }
            set { _sourceValue = value; }
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello " + GeneralName);
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
            LongDirText.Text = _rosetta.TextForId("charts.datainputchart.longdir");
            LatDegreeText.Text = _rosetta.TextForId("charts.datainputchart.degree");
            LatMinuteText.Text = _rosetta.TextForId("charts.datainputchart.minute");
            LatSecondText.Text = _rosetta.TextForId("charts.datainputchart.second");
            LatDirText.Text = _rosetta.TextForId("charts.datainputchart.latdir");
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
            LmtDirText.Text = _rosetta.TextForId("charts.datainputchart.longdir");
            BtnCalculate.Content = _rosetta.TextForId("charts.datainputchart.btncalculate");
            BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
            BtnCancel.Content = _rosetta.TextForId("common.btncancel");
        }

    }

    public class ChartsDataInputNewChartViewModel
    {
        public ChartsDataInputNewChartViewModel()
        {

        }


    }


}
