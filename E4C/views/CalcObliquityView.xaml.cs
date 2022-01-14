// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.be.astron;
using E4C.be.domain;
using E4C.be.validations;
using System.Windows;

namespace E4C.views
{
    /// <summary>
    /// Interaction logic for CalcObliquityView.xaml
    /// </summary>
    public partial class CalcObliquityView : Window
    {
        readonly private CalcObliquityViewModel calcObliquityViewModel;

        public CalcObliquityView(CalcObliquityViewModel calcObliquityViewModel)
        {
            InitializeComponent();
            this.calcObliquityViewModel = calcObliquityViewModel;
            PopulateStaticTexts();
        }

        private void PopulateStaticTexts()
        {
            Title = "Enigma Calculations: Obliquity";
            ObliquityFormTitle.Text = "Calculate Obliquity";
            LblDate.Content = "Enter the date (format yyyy/mm/dd), use the astronomical yearcount.";
            LblTime.Content = "Enter the time (format hh:mm:ss) using UT and 24-hour notation.";
            LblCalendar.Content = "Select the Gregorian or Julian Calendar.";
            rbgreg.Content = "Gregorian";
            rbjul.Content = "Julian";
            rbtrueobl.Content = "True obliquity";
            rbmeanobl.Content = "Mean obliquity";
            BtnCalcObliquity.Content = "Calculate Obliquity";
        }

        private void BtnCalcObliquity_Click(object sender, RoutedEventArgs e)
        {
            Calendars calendar = rbgreg.IsChecked == true ? Calendars.Gregorian : Calendars.Julian;
            bool typeTrueFlag = rbtrueobl.IsChecked == true;
            Result.Text = calcObliquityViewModel.CalculateObliquity(date.Text, time.Text, calendar, typeTrueFlag);
        }
    }

    public class CalcObliquityViewModel
    {
        readonly private ICalendarCalc calCalc;
        readonly private IObliquityNutationCalc oblNutCalc;
        readonly private IDateTimeValidations dateTimeValidations;
        private ValidatedDate? validatedDate;
        private ValidatedUniversalTime? validatedTime;
        readonly private string DateErrorText = "Error in date";
        readonly private string TimeErrorText = "Error in time";
        readonly private string GeneralErrorText = "Error while calculating obliquity.";

        public CalcObliquityViewModel(ICalendarCalc calCalc, IObliquityNutationCalc oblNutCalc, IDateTimeValidations dateTimeValidations)
        {
            this.calCalc = calCalc;
            this.oblNutCalc = oblNutCalc;
            this.dateTimeValidations = dateTimeValidations;
        }

        public string CalculateObliquity(string DateText, string TimeText, Calendars calendar, bool obliquityTypeTrue)
        {
            validatedDate = dateTimeValidations.ConstructAndValidateDate(DateText, calendar);
            validatedTime = dateTimeValidations.ConstructAndValidateTime(TimeText);

            //    string[] dateItems = DateText.Split('/');
            //    int year = Int32.Parse(dateItems[0]);
            //    int month = Int32.Parse(dateItems[1]);
            //    int day = Int32.Parse(dateItems[2]);

            //   string[] timeItems = TimeText.Split(':');
            //    int hour = Int32.Parse(timeItems[0]);
            //    int minute = Int32.Parse(timeItems[1]);
            //    int second = Int32.Parse(timeItems[2]);

            double fractionaltime = validatedTime.hour + validatedTime.minute / 60.0 + validatedTime.second / 3600.0;
            if (validatedDate.noErrors && validatedTime.noErrors)
            {
                SimpleDateTime dateTime = new(validatedDate.year, validatedDate.month, validatedDate.day, fractionaltime, calendar);
                ResultForDouble resultJd = calCalc.CalculateJd(dateTime);
                ResultForDouble resultObl = oblNutCalc.CalculateObliquity(resultJd.returnValue, obliquityTypeTrue);
                if (resultJd.noErrors && resultObl.noErrors) return resultObl.returnValue.ToString();
            }
            return DefineErrorText();
        }

        private string DefineErrorText()
        {
            string errorText = "";
            if (validatedDate != null && !validatedDate.noErrors) errorText += DateErrorText;
            if (validatedTime != null && !validatedTime.noErrors)
            {
                if (errorText.Length > 0) errorText += "\n";
                errorText += TimeErrorText;
            }
            if (validatedDate != null && validatedDate.noErrors && validatedTime != null && validatedTime.noErrors) errorText += GeneralErrorText;
            return errorText;
        }

    }
}
