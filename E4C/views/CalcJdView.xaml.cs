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
            bool gregflag = rbgreg.IsChecked == true;
            Result.Text = calcJdViewModel.CalculateJd(date.Text, time.Text, gregflag);
        }
    }

    public class CalcJdViewModel
    {
        readonly private ICalendarCalc calCalc;

        public CalcJdViewModel(ICalendarCalc calCalc)
        {
            this.calCalc = calCalc;
        }


        public string CalculateJd(string DateText, string TimeText, bool rbGregChecked)
        {
            // TODO add validation 
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
            if (resultJd.noErrors) return resultJd.returnValue.ToString();
            else return "Error";
            // TODO handle error situations
        }

    }
}
