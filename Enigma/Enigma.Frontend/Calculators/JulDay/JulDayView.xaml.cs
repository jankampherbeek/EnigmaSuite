// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.DateTime;
using Enigma.Frontend.Support;
using Enigma.Frontend.UiDomain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Enigma.Frontend.Calculators.JulDay;

/// <summary>View for Julian Day Calculator.</summary>
public partial class JulDayView : Window
{
    private readonly string EMPTY_STRING = "";
    private IRosetta _rosetta;
    private JulDayController _controller;
    private List<CalendarDetails> _calendarDetails;
    private List<YearCountDetails> _yearCountDetails;

    public JulDayView(IRosetta rosetta, JulDayController controller, ICalendarSpecifications calendarSpecifications, IYearCountSpecifications yearCountSpecifications)
    {
        InitializeComponent();
        _rosetta = rosetta;
        _controller = controller;
        _calendarDetails = calendarSpecifications.AllCalendarDetails();
        _yearCountDetails = yearCountSpecifications.AllDetailsForYearCounts();
        PopulateTexts();
        PopulateCalendars();
        PopulateYearCounts();
    }

    public void CalcClick(object sender, RoutedEventArgs e)
    {
        DateInputValue.Background = Brushes.White;
        TimeInputValue.Background = Brushes.White;
        _controller.InputDate = DateInputValue.Text;
        _controller.InputTime = TimeInputValue.Text;

        _controller.Calendar =   _calendarDetails[comboCalendar.SelectedIndex].Calendar;
        _controller.YearCount = _yearCountDetails[comboYearCount.SelectedIndex].YearCount;
        bool calculationOk = _controller.ProcessInput();
        if (calculationOk)
        {
            JulDayResult julDayResult = _controller.Result;
            tbJdResultUtValue.Text = julDayResult.JulDayUtText;
            tbJdResultEtValue.Text = julDayResult.JulDayEtText;
            tbDeltaTSecondsValue.Text = julDayResult.DeltaTTextInSeconds;
            tbDeltaTDaysvalue.Text = julDayResult.DeltaTTextInDays;
        } 
        else
        {
            if (_controller._errorCodes.Contains(ErrorCodes.ERR_INVALID_DATE)){
                DateInputValue.Background = Brushes.Yellow;
            }
            if (_controller._errorCodes.Contains(ErrorCodes.ERR_INVALID_TIME)){
                TimeInputValue.Background = Brushes.Yellow;
            }
        }
    }

    public void ResetClick(object sender, RoutedEventArgs e)
    {
        DateInputValue.Text = EMPTY_STRING;
        TimeInputValue.Text = EMPTY_STRING;
        comboCalendar.SelectedIndex = 0;
        comboYearCount.SelectedIndex = 0;
        CleanResultTexts();
    }

    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        HelpWindow? helpWindow = App.ServiceProvider.GetService<HelpWindow>();
        if (helpWindow != null)
        {
            helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            helpWindow.SetUri("CalcJd");
            helpWindow.ShowDialog();
        }
    }

    private void PopulateTexts()
    {
        FormTitle.Text = _rosetta.TextForId("calc.jdnr.formtitle");
        DateInputTxt.Text = _rosetta.TextForId("common.date");
        TimeInputTxt.Text = _rosetta.TextForId("common.timeinput.ut");
        CalendarTxt.Text = _rosetta.TextForId("common.calendar");
        BtnCalc.Content = _rosetta.TextForId("common.btncalc");
        BtnClose.Content = _rosetta.TextForId("common.btnclose");
        BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
        BtnReset.Content = _rosetta.TextForId("common.btnreset");
        tbDeltaTDaysTxt.Text = _rosetta.TextForId("calc.jdnr.result.deltatday");
        tbDeltaTSecondsTxt.Text = _rosetta.TextForId("calc.jdnr.result.deltatsec");
        tbJdResultUtTxt.Text = _rosetta.TextForId("calc.jdnr.result.jdut");
        tbJdResultEtTxt.Text = _rosetta.TextForId("calc.jdnr.result.jdet");
        tbYearCountTxt.Text = _rosetta.TextForId("common.yearcount");
        CleanResultTexts();
    }

    private void PopulateCalendars()
    {
        comboCalendar.Items.Clear();
        foreach (var calendarDetail in _calendarDetails)
        {
            comboCalendar.Items.Add(_rosetta.TextForId(calendarDetail.TextIdFull));
        }
        comboCalendar.SelectedIndex = 0;
    }

    private void PopulateYearCounts()
    {
        comboYearCount.Items.Clear();
        foreach (var yearCountDetail in _yearCountDetails)
        {
            comboYearCount.Items.Add(_rosetta.TextForId(yearCountDetail.TextId));
        }
        comboYearCount.SelectedIndex = 0;
    }

    private void CleanResultTexts()
    {
        tbDeltaTDaysvalue.Text = EMPTY_STRING;
        tbDeltaTSecondsValue.Text = EMPTY_STRING;
        tbJdResultEtValue.Text = EMPTY_STRING;
        tbJdResultUtValue.Text = EMPTY_STRING;
    }

}
