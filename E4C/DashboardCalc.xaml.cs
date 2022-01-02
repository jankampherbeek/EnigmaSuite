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
using E4C.be.model;
using E4C.be.astron;
using E4C.be.sefacade;

namespace E4C
{
    /// <summary>
    /// Interaction logic for DashboardCalc.xaml
    /// </summary>
    public partial class DashboardCalc : Window
    {
        public DashboardCalc()
        {
            InitializeComponent();
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            // TODO add validation 
            string[] dateItems = date.Text.Split('/');
            int year = Int32.Parse(dateItems[0]);
            int month = Int32.Parse(dateItems[1]);
            int day = Int32.Parse(dateItems[2]);

            string[] timeItems = time.Text.Split(':');
            int hour = Int32.Parse(timeItems[0]);
            int minute = Int32.Parse(timeItems[1]);
            int second = Int32.Parse(timeItems[2]);
            double fractionaltime = hour + (double)minute / 60.0 + (double)second / 3600.0;
            bool gregflag = (bool)(rbgreg.IsChecked == true);

            ISeDateTimeFacade dtFacade = new SeDateTimeFacade();
            ICalendarCalc calc = new CalendarCalc(dtFacade);
            SimpleDateTime dateTime = new (year, month, day, fractionaltime, gregflag);
            ResultForDouble resultJd = calc.CalculateJd(dateTime);
            if (resultJd.noErrors) result.Text = resultJd.returnValue.ToString();
            // TODO handle error situations

        }
    }
}
