using E4C.Models;
using System.Windows;
using E4C.Views.ViewHelpers;

namespace E4C.Views
{
    /// <summary>
    /// Interaction logic for ChartsDataInputNewChart.xaml
    /// </summary>
    public partial class ChartsDataInputNewChart : Window
    {
        readonly private ChartsDataInputNewChartViewModel viewModel;
        readonly private IRosetta rosetta;
        private string generalName = "";
        private string sourceValue = "";

        public ChartsDataInputNewChart(ChartsDataInputNewChartViewModel viewModel, IRosetta rosetta)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.rosetta = rosetta;
            PopulateStaticTexts();
        }


        public string GeneralName
        {
            get { return generalName; }
            set { generalName = value; }
        }

        public string SourceValue
        {
            get { return sourceValue; }
            set { sourceValue = value; }
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello " + GeneralName);
        }

        private void PopulateStaticTexts()
        {
            FormTitle.Text = rosetta.TextForId("charts.datainputchart.titleform");
            GeneralTitle.Text = rosetta.TextForId("charts.datainputchart.titlegeneral");
            NameText.Text = rosetta.TextForId("charts.datainputchart.name");
            SubjectText.Text = rosetta.TextForId("charts.datainputchart.subject");
            RatingText.Text = rosetta.TextForId("charts.datainputchart.rating");
            SourceText.Text = rosetta.TextForId("charts.datainputchart.source");
            DescriptionText.Text = rosetta.TextForId("charts.datainputchart.description");
            LocationTitle.Text = rosetta.TextForId("charts.datainputchart.locationtitle");
            LocationName.Text = rosetta.TextForId("charts.datainputchart.locationname");
            LongitudeText.Text = rosetta.TextForId("charts.datainputchart.longitude");
            LatitudeText.Text = rosetta.TextForId("charts.datainputchart.latitude");
            LongDegreeText.Text = rosetta.TextForId("charts.datainputchart.degree");
            LongMinuteText.Text = rosetta.TextForId("charts.datainputchart.minute");
            LongSecondText.Text = rosetta.TextForId("charts.datainputchart.second");
            LongDirText.Text = rosetta.TextForId("charts.datainputchart.longdir");
            LatDegreeText.Text = rosetta.TextForId("charts.datainputchart.degree");
            LatMinuteText.Text = rosetta.TextForId("charts.datainputchart.minute");
            LatSecondText.Text = rosetta.TextForId("charts.datainputchart.second");
            LatDirText.Text = rosetta.TextForId("charts.datainputchart.latdir");
            DateTimeTitle.Text = rosetta.TextForId("charts.datainputchart.datetimetitle");
            DateText.Text = rosetta.TextForId("charts.datainputchart.date");
            DateYearText.Text = rosetta.TextForId("charts.datainputchart.year");
            DateMonthText.Text = rosetta.TextForId("charts.datainputchart.month");
            DateDayText.Text = rosetta.TextForId("charts.datainputchart.day");
            DateCalendarText.Text = rosetta.TextForId("charts.datainputchart.calendar");
            DateYearCount.Text = rosetta.TextForId("charts.datainputchart.yearcount");
            TimeText.Text = rosetta.TextForId("charts.datainputchart.time");
            TimeHourText.Text = rosetta.TextForId("charts.datainputchart.hour");
            TimeMinuteText.Text = rosetta.TextForId("charts.datainputchart.minute");
            TimeSecondText.Text = rosetta.TextForId("charts.datainputchart.second");
            TimeDstText.Text = rosetta.TextForId("charts.datainputchart.dst");
            TimeZoneText.Text = rosetta.TextForId("charts.datainputchart.timezone");
            LmtZoneText.Text = rosetta.TextForId("charts.datainputchart.lmtzone");
            LmtDegreeText.Text = rosetta.TextForId("charts.datainputchart.degree");
            LmtMinuteText.Text = rosetta.TextForId("charts.datainputchart.minute");
            LmtSecondText.Text = rosetta.TextForId("charts.datainputchart.second");
            LmtDirText.Text = rosetta.TextForId("charts.datainputchart.longdir");
            BtnCalculate.Content = rosetta.TextForId("charts.datainputchart.btncalculate");
            BtnHelp.Content = rosetta.TextForId("common.btnhelp");
            BtnCancel.Content = rosetta.TextForId("common.btncancel");
        }

    }

    public class ChartsDataInputNewChartViewModel
    {
        public ChartsDataInputNewChartViewModel()
        {

        }


    }


}
