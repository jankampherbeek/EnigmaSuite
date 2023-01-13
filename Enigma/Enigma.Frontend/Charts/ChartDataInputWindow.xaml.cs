// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Enigma.Frontend.Ui.Charts;


public partial class ChartDataInputWindow : Window
{
    private readonly Rosetta _rosetta = Rosetta.Instance;
    private readonly ChartDataInputController _controller;
    private readonly List<CalendarDetails> _calendarDetails = Calendars.Gregorian.AllDetails();
    private readonly List<Directions4GeoLongDetails> _directions4GeoLongDetails = Directions4GeoLong.East.AllDetails();
    private readonly List<Directions4GeoLatDetails> _directions4GeoLatDetails = Directions4GeoLat.North.AllDetails();
    private readonly List<YearCountDetails> _yearCountDetails = YearCounts.Astronomical.AllDetails();
    private readonly List<TimeZoneDetails> _timeZoneDetails = TimeZones.UT.AllDetails();

    public ChartDataInputWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ChartDataInputController>();
        _controller.InitializeDataVault();
        PopulateTexts();
        PopulateLists();
        EnableLmt(false);
    }

    private void PopulateTexts()
    {
        FormTitle.Text = _rosetta.TextForId("charts.datainput.formtitle");
        NameLocationTxt.Text = _rosetta.TextForId("charts.datainput.namelocation");
        NameIdTxt.Text = _rosetta.TextForId("charts.datainput.nameid");
        LongitudeTxt.Text = _rosetta.TextForId("common.location.longitude");
        LatitudeTxt.Text = _rosetta.TextForId("common.location.latitude");
        DateTimeTxt.Text = _rosetta.TextForId("charts.datainput.datetime");
        DateTxt.Text = _rosetta.TextForId("common.date");
        CalTxt.Text = _rosetta.TextForId("common.calendar");
        YearCountTxt.Text = _rosetta.TextForId("common.yearcount");
        TimeTxt.Text = _rosetta.TextForId("common.time");
        DstTxt.Text = _rosetta.TextForId("common.time.dst");
        TimeZoneTxt.Text = _rosetta.TextForId("common.timezone");
        LmtTxt.Text = _rosetta.TextForId("common.time.lmt");
        BtnCalculate.Content = _rosetta.TextForId("common.btncalc");
        BtnClose.Content = _rosetta.TextForId("common.btnclose");
        BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
    }


    private void PopulateLists()
    {
        PopulateCalendars();
        PopulateDirections4GeoLat();
        PopulateDirections4GeoLong();
        PopulateYearCounts();
        PopulateTimeZones();
    }

    private void PopulateCalendars()
    {
        comboCalendar.Items.Clear();
        foreach (var calendarDetail in _calendarDetails)
        {
            comboCalendar.Items.Add(_rosetta.TextForId(calendarDetail.TextId));
        }
        comboCalendar.SelectedIndex = 0;
    }

    private void PopulateDirections4GeoLong()
    {
        comboLongDir.Items.Clear();
        comboLmtLongDir.Items.Clear();
        for (int i = 0; i < _directions4GeoLongDetails.Count; i++)
        {
            Directions4GeoLongDetails? direction4GeoLongDetail = _directions4GeoLongDetails[i];
            comboLongDir.Items.Add(_rosetta.TextForId(direction4GeoLongDetail.TextId));
            comboLmtLongDir.Items.Add(_rosetta.TextForId(direction4GeoLongDetail.TextId));
        }
        comboLongDir.SelectedIndex = 0;
        comboLmtLongDir.SelectedIndex = 0;
    }

    private void PopulateDirections4GeoLat()
    {
        comboLatDir.Items.Clear();
        foreach (var direction4GeoLatDetail in _directions4GeoLatDetails)
        {
            comboLatDir.Items.Add(_rosetta.TextForId(direction4GeoLatDetail.TextId));
        }
        comboLatDir.SelectedIndex = 0;
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

    private void PopulateTimeZones()
    {
        comboTimezone.Items.Clear();
        foreach (var timeZoneDetail in _timeZoneDetails)
        {
            comboTimezone.Items.Add(_rosetta.TextForId(timeZoneDetail.TextId));
        }
        comboTimezone.SelectedIndex = 0;
    }

    private void TimeZoneChanged(object sender, RoutedEventArgs e)
    {
        bool lmtSelected = false;
        int tzIndex = comboTimezone.SelectedIndex;
        if (_timeZoneDetails[tzIndex].TimeZone == TimeZones.LMT)
        {
            lmtSelected = true;
        }
        else
        {
            LmtValue.Text = "";
        }
        EnableLmt(lmtSelected);
    }
    private void EnableLmt(bool active)
    {
        comboLmtLongDir.IsEnabled = active;
        LmtValue.IsEnabled = active;
    }

    private void CalculateClick(object sender, RoutedEventArgs e)
    {
        TransferValues();
        bool calculationOk = _controller.ProcessInput();
        if (calculationOk)
        {
            Close();
        }
        else
        {
            HandleErrors();
        }
    }

    private void HandleErrors()
    {
        DateValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.ERR_INVALID_DATE) ? Brushes.Yellow : Brushes.White;
        TimeValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.ERR_INVALID_TIME) ? Brushes.Yellow : Brushes.White;
        LongitudeValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.ERR_INVALID_GEOLON) ? Brushes.Yellow : Brushes.White;
        LatitudeValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.ERR_INVALID_GEOLAT) ? Brushes.Yellow : Brushes.White;
        LmtValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.ERR_INVALID_GEOLON_LMT) ? Brushes.Yellow : Brushes.White;
    }

    private void TransferValues()
    {
        _controller.NameId = NameIdValue.Text;
        _controller.Longitude = LongitudeValue.Text;
        _controller.Latitude = LatitudeValue.Text;
        _controller.Direction4GeoLong = _directions4GeoLongDetails[comboLongDir.SelectedIndex].Direction;
        _controller.Direction4GeoLat = _directions4GeoLatDetails[comboLatDir.SelectedIndex].Direction;
        _controller.InputDate = DateValue.Text;
        _controller.InputTime = TimeValue.Text;
        _controller.Calendar = _calendarDetails[comboCalendar.SelectedIndex].Calendar;
        _controller.YearCount = _yearCountDetails[comboYearCount.SelectedIndex].YearCount;
        _controller.Dst = checkDst.IsChecked != null && checkDst.IsChecked == true;
        TimeZones timeZone = _timeZoneDetails[comboTimezone.SelectedIndex].TimeZone;
        _controller.TimeZone = timeZone;
        _controller.LmtOffset = (timeZone == TimeZones.LMT) ? LmtValue.Text : "00:00:00";
        _controller.LmtDirection4GeoLong = _directions4GeoLongDetails[comboLmtLongDir.SelectedIndex].Direction;
    }

    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("ChartsDataInput");
        helpWindow.ShowDialog();
    }

}




