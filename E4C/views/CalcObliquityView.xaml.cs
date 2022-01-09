using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using E4C.be.astron;
using E4C.be.model;

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
            bool gregflag = rbgreg.IsChecked == true;
            bool typeTrueFlag = rbtrueobl.IsChecked == true;
            Result.Text = calcObliquityViewModel.CalculateObliquity(date.Text, time.Text, gregflag, typeTrueFlag);
        }
    }

    public class CalcObliquityViewModel
    {
        readonly private ICalendarCalc calCalc;
        readonly private IObliquityNutationCalc oblNutCalc;

        public CalcObliquityViewModel(ICalendarCalc calCalc, IObliquityNutationCalc oblNutCalc)
        {
            this.calCalc = calCalc;
            this.oblNutCalc = oblNutCalc;
        }

        public string CalculateObliquity(string DateText, string TimeText, bool rbGregChecked, bool obliquityTypeTrue)
        {
            // TODO add validation 
            // TODO extra functionality, same as for calculating jd
            string[] dateItems = DateText.Split('/');
            int year = Int32.Parse(dateItems[0]);
            int month = Int32.Parse(dateItems[1]);
            int day = Int32.Parse(dateItems[2]);

            string[] timeItems = TimeText.Split(':');
            int hour = Int32.Parse(timeItems[0]);
            int minute = Int32.Parse(timeItems[1]);
            int second = Int32.Parse(timeItems[2]);
            double fractionaltime = hour + minute / 60.0 + second / 3600.0;
            bool gregflag = rbGregChecked;

            SimpleDateTime dateTime = new(year, month, day, fractionaltime, gregflag);
            ResultForDouble resultJd = calCalc.CalculateJd(dateTime);

            ResultForDouble resultObl = oblNutCalc.CalculateObliquity(resultJd.returnValue, obliquityTypeTrue);

            if (resultObl.noErrors) return resultObl.returnValue.ToString();
            else return "Error";
            // TODO handle error situations
        }

    }
}
