// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.DateTime;
using Enigma.Domain.Locational;
using Enigma.Frontend.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Enigma.Frontend.Charts;


public partial class ChartDataInputWindow : Window
{
    private IRosetta _rosetta;
    private ChartDataInputController _controller;
    private List<CalendarDetails> _calendarDetails;
    private List<Directions4GeoLongDetails> _directions4GeoLongDetails;
    private List<Directions4GeoLatDetails> _directions4GeoLatDetails;
    private List<YearCountDetails> _yearCountDetails;
    private List<TimeZoneDetails> _timeZoneDetails;

    public ChartDataInputWindow(ChartDataInputController controller, IRosetta rosetta, IChartsEnumFacade chartsEnumFacade)
    {
        InitializeComponent();
        _controller = controller;
        _rosetta = rosetta;
        _calendarDetails = chartsEnumFacade.AllCalendarDetails();
        _directions4GeoLongDetails = chartsEnumFacade.AllDirections4GeoLongDetails();
        _directions4GeoLatDetails = chartsEnumFacade.AllDirections4GeoLatDetails();
        _yearCountDetails = chartsEnumFacade.AllYearCountDetails();
        _timeZoneDetails = chartsEnumFacade.AllTimeZoneDetails();
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
        foreach(var direction4GeoLongDetail in _directions4GeoLongDetails)
        {
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
        foreach(var timeZoneDetail in _timeZoneDetails)
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
        } else
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
        Hide();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        HelpWindow? helpWindow = App.ServiceProvider.GetService<HelpWindow>();
        if (helpWindow != null)
        {
            helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            helpWindow.SetHelpPage("ChartsDataInput"); 
            helpWindow.ShowDialog();
        }
    }

}




