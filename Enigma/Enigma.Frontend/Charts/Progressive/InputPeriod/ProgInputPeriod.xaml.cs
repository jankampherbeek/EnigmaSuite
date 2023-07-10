// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Enigma.Frontend.Ui.Charts.Progressive.InputPeriod;


public partial class ProgInputPeriod : Window
{
    private readonly ProgInputPeriodController _controller;

    public bool IsCompleted { get; set; } = false;
    public FullDate? FullStartDate { get; set; }
    public FullDate? FullEndDate { get; set; }
    private readonly List<CalendarDetails> _calendarDetails = Calendars.Gregorian.AllDetails();
    private readonly List<YearCountDetails> _yearCountDetails = YearCounts.CE.AllDetails();

    public ProgInputPeriod()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ProgInputPeriodController>();
        PopulateTexts();
        PopulateData();
    }


    private void PopulateTexts()
    {
        this.Title = Rosetta.TextForId("charts.prog.period.title");
        FormTitle.Text = Rosetta.TextForId("charts.prog.period.formtitle");
        tbExplanation.Text = Rosetta.TextForId("charts.prog.period.explanation");
        tbStartDatePeriod.Text = Rosetta.TextForId("charts.prog.period.startdate");
        tbEndDatePeriod.Text = Rosetta.TextForId("charts.prog.period.enddate");
        tbCalendar.Text = Rosetta.TextForId("common.calendar.full");
        tbYearCount.Text = Rosetta.TextForId("common.yearcount");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
        btnCancel.Content = Rosetta.TextForId("common.btncancel");
        btnOk.Content = Rosetta.TextForId("common.btnok");
    }


    private void PopulateData()
    {
        PopulateYearCounts();
        PopulateCalendars();
    }


    private void PopulateYearCounts()
    {
        comboYearCount.Items.Clear();
        foreach (var yearCountDetail in _yearCountDetails)
        {
            comboYearCount.Items.Add(Rosetta.TextForId(yearCountDetail.TextId));
        }
        comboYearCount.SelectedIndex = 0;
    }

    private void PopulateCalendars()
    {
        comboCalendar.Items.Clear();
        foreach (var calendarDetail in _calendarDetails)
        {
            comboCalendar.Items.Add(Rosetta.TextForId(calendarDetail.TextId));
        }
        comboCalendar.SelectedIndex = 0;
    }

    private void OkClick(object sender, RoutedEventArgs e)
    {
        TransferValues();
        bool calculationOk = _controller.ProcessInput();
        if (calculationOk)
        {
            FullStartDate = _controller.FullStartDate;
            FullEndDate = _controller.FullEndDate;
            IsCompleted = true;
            Close();
        }
        else
        {
            HandleErrors();
        }
    }

    private void CancelClick(object sender, RoutedEventArgs e)
    {
        IsCompleted = false;
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        // TODO implement help for ProgInputPeriod
    }


    private void TransferValues()
    {
        _controller.InputStartDate = tbStartDatePeriodValue.Text;
        _controller.InputEndDate = tbEndDatePeriodValue.Text;
        _controller.Calendar = _calendarDetails[comboCalendar.SelectedIndex].Calendar;
        _controller.YearCount = _yearCountDetails[comboYearCount.SelectedIndex].YearCount;
    }

    private void HandleErrors()
    {
        tbStartDatePeriodValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.InvalidStartdate) ? Brushes.Yellow : Brushes.White;
        tbEndDatePeriodValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.InvalidEnddate) ? Brushes.Yellow : Brushes.White;
    }

}

