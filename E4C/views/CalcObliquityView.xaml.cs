// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Astron;
using E4C.Models.Domain;
using E4C.Models.Validations;
using System.Windows;

namespace E4C.Views
{
    /// <summary>
    /// Interaction logic for CalcObliquityView.xaml
    /// </summary>
    public partial class CalcObliquityView : Window
    {
        readonly private CalcObliquityViewModel _calcObliquityViewModel;

        public CalcObliquityView(CalcObliquityViewModel calcObliquityViewModel)
        {
            InitializeComponent();
            _calcObliquityViewModel = calcObliquityViewModel;
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
            Calendars _calendar = rbgreg.IsChecked == true ? Calendars.Gregorian : Calendars.Julian;
            bool _typeTrueFlag = rbtrueobl.IsChecked == true;
            Result.Text = _calcObliquityViewModel.CalculateObliquity(date.Text, time.Text, _calendar, _typeTrueFlag);
        }
    }

    public class CalcObliquityViewModel
    {
        readonly private ICalendarCalc _calCalc;
        readonly private IObliquityNutationCalc _oblNutCalc;
        readonly private IDateTimeValidations _dateTimeValidations;
        readonly private ValidatedDate? _validatedDate;
        readonly private ValidatedUniversalTime? _validatedTime;
    //    readonly private string _dateErrorText = "Error in date";
    //    readonly private string TimeErrorText = "Error in time";
    //    readonly private string GeneralErrorText = "Error while calculating obliquity.";

        public CalcObliquityViewModel(ICalendarCalc calCalc, IObliquityNutationCalc oblNutCalc, IDateTimeValidations dateTimeValidations)
        {
            _calCalc = calCalc;
            _oblNutCalc = oblNutCalc;
            _dateTimeValidations = dateTimeValidations;
        }

        public string CalculateObliquity(string dateText, string timeText, Calendars calendar, bool obliquityTypeTrue)
        {
            //  List<int> dateErrors = dateTimeValidations.ValidateDate();



            //  validatedDate = dateTimeValidations.ConstructAndValidateDate(DateText, calendar);
            //  validatedTime = dateTimeValidations.ConstructAndValidateTime(TimeText);

            double _fractionaltime = _validatedTime.Hour + _validatedTime.Minute / 60.0 + _validatedTime.Second / 3600.0;
            //            if (validatedDate.noErrors && validatedTime.noErrors)
            //            {
            SimpleDateTime _dateTime = new(_validatedDate.Year, _validatedDate.Month, _validatedDate.Day, _fractionaltime, calendar);
            ResultForDouble _resultJd = _calCalc.CalculateJd(_dateTime);
            ResultForDouble _resultObl = _oblNutCalc.CalculateObliquity(_resultJd.ReturnValue, obliquityTypeTrue);
            if (_resultJd.NoErrors && _resultObl.NoErrors) return _resultObl.ReturnValue.ToString();
            //            }
            return DefineErrorText();
        }

        private string DefineErrorText()
        {
            string _errorText = "";
            /*           if (validatedDate != null && !validatedDate.noErrors) errorText += DateErrorText;
                       if (validatedTime != null && !validatedTime.noErrors)
                       {
                           if (errorText.Length > 0) errorText += "\n";
                           errorText += TimeErrorText;
                       }
                       if (validatedDate != null && validatedDate.noErrors && validatedTime != null && validatedTime.noErrors) errorText += GeneralErrorText;
              */
            return _errorText;
        }

    }
}
