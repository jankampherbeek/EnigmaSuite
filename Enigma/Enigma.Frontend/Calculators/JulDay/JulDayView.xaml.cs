using Enigma.Frontend.Support;
using Enigma.Frontend.UiDomain;
using System.Windows;


namespace Enigma.Frontend.Calculators.JulDay;

/// <summary>View for Julian Day Calculator.</summary>
public partial class JulDayView : Window
{
    private IRosetta _rosetta;
    private JulDayController _controller;
    private JulDayResult _julDayResult;

    public JulDayView(IRosetta rosetta, JulDayController controller)
    {
        InitializeComponent();
        _rosetta = rosetta;
        _controller = controller;
        PopulateTexts();
    }

    public void CalcClick(object sender, RoutedEventArgs e)
    {
        _controller.InputDate = DateInputValue.Text;
        _controller.InputTime = TimeInputValue.Text;
        _controller.GregorianCalendar = rbGregorian.IsChecked == true;
        _controller.HistoricalTimeCount = rbHistorical.IsChecked == true;
        bool calculationOk = _controller.ProcessInput();
        if (calculationOk)
        {
            _julDayResult = _controller.Result;
            tbJdResultUtValue.Text = _julDayResult.JulDayUtText;
            tbJdResultEtValue.Text = _julDayResult.JulDayEtText;
            tbDeltaTSecondsValue.Text = _julDayResult.DeltaTTextInSeconds;
            tbDeltaTDaysvalue.Text = _julDayResult.DeltaTTextInDays;  
        }

    }

    private void PopulateTexts()
    {
        FormTitle.Text = _rosetta.TextForId("calc.jdnr.formtitle");
        DateInputTxt.Text = _rosetta.TextForId("common.dateinput");
        TimeInputTxt.Text = _rosetta.TextForId("common.timeinput");
        CalendarTxt.Text = _rosetta.TextForId("common.calendarinput");
        rbJulian.Content = _rosetta.TextForId("common.calendar.rb.jul");
        rbGregorian.Content = _rosetta.TextForId("common.calendar.rb.greg");
        BtnCalc.Content = _rosetta.TextForId("common.btncalc");
        BtnClose.Content = _rosetta.TextForId("common.btnclose");
        BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
        BtnReset.Content = _rosetta.TextForId("common.btnreset");
        tbDeltaTDaysTxt.Text = _rosetta.TextForId("calc.jdnr.result.deltatday");
        tbDeltaTSecondsTxt.Text = _rosetta.TextForId("calc.jdnr.result.deltatsec");
        tbJdResultUtTxt.Text = _rosetta.TextForId("calc.jdnr.result.jdut");
        tbJdResultEtTxt.Text = _rosetta.TextForId("calc.jdnr.result.jdet");
        tbYearCountTxt.Text = _rosetta.TextForId("common.yearcountinput");
        rbAstronomical.Content = _rosetta.TextForId("common.yearcount.rb.astron");
        rbHistorical.Content = _rosetta.TextForId("common.yearcount.rb.hist");
    }

}
