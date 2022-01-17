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
    /// Interaction logic for CalcJdView.xaml
    /// </summary>
    public partial class CalcJdView : Window
    {
        readonly private CalcJdViewModel calcJdViewModel;

        public CalcJdView(CalcJdViewModel calcJdViewModel)
        {
            InitializeComponent();
            this.calcJdViewModel = calcJdViewModel;
            PopulateStaticTexts();
        }

        private void PopulateStaticTexts()
        {
            Title = "Enigma Calculations: Julian Day Number";
            JdFormTitle.Text = "Calculate Julian Day Number";
            LblDate.Content = "Enter the date (format yyyy/mm/dd), use the astronomical yearcount.";
            LblTime.Content = "Enter the time (format hh:mm:ss) using UT and 24-hour notation.";
            LblCalendar.Content = "Select the Gregorian or Julian Calendar.";
            rbgreg.Content = "Gregorian";
            rbjul.Content = "Julian";
            BtnCalcJd.Content = "Calculate Julian Day Number";
        }


        private void BtnCalcJd_Click(object sender, RoutedEventArgs e)
        {
            Calendars calendar = rbgreg.IsChecked == true ? Calendars.Gregorian : Calendars.Julian;
            Result.Text = calcJdViewModel.CalculateJd(date.Text, time.Text, calendar);
        }
    }

    public class CalcJdViewModel
    {
        readonly private string DateErrorText = "Error in date";
        readonly private string TimeErrorText = "Error in time";
        readonly private string GeneralErrorText = "Error while calculating Julian Day Number.";
        readonly private ICalendarCalc calCalc;
        readonly private IDateTimeValidations dateTimeValidations;
        private ValidatedDate? validatedDate;
        private ValidatedUniversalTime? validatedTime;

        public CalcJdViewModel(ICalendarCalc calCalc, IDateTimeValidations dateTimeValidations)
        {
            this.calCalc = calCalc;
            this.dateTimeValidations = dateTimeValidations;
        }


        public string CalculateJd(string DateText, string TimeText, Calendars calendar)
        {
            validatedDate = dateTimeValidations.ConstructAndValidateDate(DateText, calendar);
            validatedTime = dateTimeValidations.ConstructAndValidateTime(TimeText);
            double fractionaltime = validatedTime.hour + validatedTime.minute / 60.0 + validatedTime.second / 3600.0;
            if (validatedDate.noErrors && validatedTime.noErrors)
            {
                SimpleDateTime dateTime = new(validatedDate.year, validatedDate.month, validatedDate.day, fractionaltime, calendar);
                ResultForDouble resultJd = calCalc.CalculateJd(dateTime);
                if (resultJd.noErrors) return resultJd.returnValue.ToString();
            }
            // If no error occurred, the correct value has already been returned.
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
            if (validatedDate != null && validatedTime != null && validatedDate.noErrors && validatedTime.noErrors) errorText += GeneralErrorText;
            return errorText;
        }

    }
}
