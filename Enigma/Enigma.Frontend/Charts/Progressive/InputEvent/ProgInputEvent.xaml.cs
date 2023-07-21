// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Enigma.Frontend.Ui.Charts.Progressive.InputEvent;


public partial class ProgInputEvent : Window
{
    public bool IsCompleted { get; set; } = false;
    public FullDate? CheckedDate { get; set; }
    public FullTime? CheckedTime { get; set; }
    public Location? CheckedLocation { get; set; }

    private readonly ProgInputEventController _controller;
    private readonly List<CalendarDetails> _calendarDetails = Calendars.Gregorian.AllDetails();
    private readonly List<YearCountDetails> _yearCountDetails = YearCounts.CE.AllDetails();
    private readonly List<Directions4GeoLongDetails> _directions4GeoLongDetails = Directions4GeoLong.East.AllDetails();
    private readonly List<Directions4GeoLatDetails> _directions4GeoLatDetails = Directions4GeoLat.North.AllDetails();
    private readonly List<TimeZoneDetails> _timeZoneDetails = TimeZones.Ut.AllDetails();

    public ProgInputEvent()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ProgInputEventController>();
        PopulateTexts();
        PopulateData();
    }


    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("charts.prog.event.title");
        FormTitle.Text = Rosetta.TextForId("charts.prog.event.formtitle");
        tbExplanation.Text = Rosetta.TextForId("charts.prog.event.explanation");
        tbDescription.Text = Rosetta.TextForId("charts.prog.event.description");
        tbLocation.Text = Rosetta.TextForId("charts.prog.event.location");
        tbGeoLong.Text = Rosetta.TextForId("charts.prog.event.geolong");
        tbGeoLat.Text = Rosetta.TextForId("charts.prog.event.geoLat");
        tbDate.Text = Rosetta.TextForId("charts.prog.event.date");
        tbCal.Text = Rosetta.TextForId("common.calendar");
        tbYearCount.Text = Rosetta.TextForId("common.yearcount");
        tbTime.Text = Rosetta.TextForId("charts.prog.event.time");
        tbDst.Text = Rosetta.TextForId("charts.prog.event.dst");
        tbTimezone.Text = Rosetta.TextForId("charts.prog.event.timezone");
        tbGeoLongLmt.Text = Rosetta.TextForId("charts.prog.event.geolonglmt");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
        btnCancel.Content = Rosetta.TextForId("common.btncancel");
        btnOk.Content = Rosetta.TextForId("common.btnok");
    }

    private void PopulateData()
    {
        PopulateYearCounts();
        PopulateCalendars();
        PopulateDirections4GeoLong();
        PopulateDirections4GeoLat();
    }


    private void PopulateYearCounts()
    {
        comboYearCount.Items.Clear();
        foreach (var yearCountDetail in _yearCountDetails)
        {
            comboYearCount.Items.Add(Rosetta.TextForId(yearCountDetail.Text));
        }
        comboYearCount.SelectedIndex = 0;
    }

    private void PopulateCalendars()
    {
        comboCal.Items.Clear();
        foreach (var calendarDetail in _calendarDetails)
        {
            comboCal.Items.Add(Rosetta.TextForId(calendarDetail.TextShort));
        }
        comboCal.SelectedIndex = 0;
    }

    private void PopulateDirections4GeoLong()
    {
        comboLongDir.Items.Clear();
        comboLongDirLmt.Items.Clear();
        for (int i = 0; i < _directions4GeoLongDetails.Count; i++)
        {
            Directions4GeoLongDetails? direction4GeoLongDetail = _directions4GeoLongDetails[i];
            comboLongDir.Items.Add(Rosetta.TextForId(direction4GeoLongDetail.Text));
            comboLongDirLmt.Items.Add(Rosetta.TextForId(direction4GeoLongDetail.Text));
        }
        comboLongDir.SelectedIndex = 0;
        comboLongDirLmt.SelectedIndex = 0;
    }

    private void PopulateDirections4GeoLat()
    {
        comboLatDir.Items.Clear();
        foreach (var direction4GeoLatDetail in _directions4GeoLatDetails)
        {
            comboLatDir.Items.Add(Rosetta.TextForId(direction4GeoLatDetail.Text));
        }
        comboLatDir.SelectedIndex = 0;
    }




    private void OkClick(object sender, RoutedEventArgs e)
    {
        TransferValues();
        bool calculationOk = _controller.ProcessInput();
        if (calculationOk)
        {
            CheckedDate = _controller.CheckedDate;
            CheckedTime = _controller.CheckedTime;
            // todo define location
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
        // TODO implement help for ProgInputEvent
    }

    private void TransferValues()
    {
        _controller.InputDate = tbDateValue.Text;
        _controller.Calendar = _calendarDetails[comboCal.SelectedIndex].Calendar;
        _controller.YearCount = _yearCountDetails[comboYearCount.SelectedIndex].YearCount;
        _controller.Latitude = tbGeoLat.Text;
        _controller.Longitude = tbGeoLong.Text;
        _controller.LmtOffset = tbGeoLongLmt.Text;
        _controller.Direction4GeoLat = _directions4GeoLatDetails[comboLatDir.SelectedIndex].Direction;
        _controller.Direction4GeoLong = _directions4GeoLongDetails[comboLongDir.SelectedIndex].Direction;
        _controller.LmtDirection4GeoLong = _directions4GeoLongDetails[comboLongDirLmt.SelectedIndex].Direction;
        _controller.InputTime = tbTime.Text;
        _controller.Dst = checkDst.IsChecked != null && checkDst.IsChecked == true;
        TimeZones timeZone = _timeZoneDetails[comboTimezone.SelectedIndex].TimeZone;
        _controller.TimeZone = timeZone;
        _controller.LmtOffset = (timeZone == TimeZones.Lmt) ? tbGeoLongLmt.Text : "00:00:00";
        _controller.LmtDirection4GeoLong = _directions4GeoLongDetails[comboLongDirLmt.SelectedIndex].Direction;
    }

    private void HandleErrors()
    {
        tbDateValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.INVALID_DATE) ? Brushes.Yellow : Brushes.White;
        tbGeoLongValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.INVALID_GEOLON) ? Brushes.Yellow : Brushes.White;
        tbGeoLatValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.INVALID_GEOLAT) ? Brushes.Yellow : Brushes.White;
        tbTimeValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.INVALID_TIME) ? Brushes.Yellow : Brushes.White;
        tbGeoLongLmtValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.INVALID_GEOLON_LMT) ? Brushes.Yellow : Brushes.White;

    }


}

