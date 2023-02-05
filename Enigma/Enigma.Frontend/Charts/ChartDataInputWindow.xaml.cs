// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Charts;
using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Enigma.Frontend.Ui.Charts;


public partial class ChartDataInputWindow : Window
{
    private readonly ChartDataInputController _controller;
    private readonly List<RoddenRatingDetails> _ratingDetails = RoddenRatings.Unknown.AllDetails();
    private readonly List<ChartCategoryDetails> _categoryDetails = ChartCategories.Unknown.AllDetails();
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
        this.Title = Rosetta.TextForId("charts.datainput.title");
        FormTitle.Text = Rosetta.TextForId("charts.datainput.formtitle");
        MetaTxt.Text = Rosetta.TextForId("charts.datainput.meta");
        NameIdTxt.Text = Rosetta.TextForId("charts.datainput.nameid");
        SourceTxt.Text = Rosetta.TextForId("charts.datainput.source");
        RatingTxt.Text = Rosetta.TextForId("charts.datainput.rating");
        CategoryTxt.Text = Rosetta.TextForId("charts.datainput.category");
        LocationTxt.Text = Rosetta.TextForId("charts.datainput.location");
        LocationNameTxt.Text = Rosetta.TextForId("charts.datainput.locationname");
        LongitudeTxt.Text = Rosetta.TextForId("common.location.longitude");
        LatitudeTxt.Text = Rosetta.TextForId("common.location.latitude");
        DateTimeTxt.Text = Rosetta.TextForId("charts.datainput.datetime");
        DateTxt.Text = Rosetta.TextForId("common.date");
        CalTxt.Text = Rosetta.TextForId("common.calendar");
        YearCountTxt.Text = Rosetta.TextForId("common.yearcount");
        TimeTxt.Text = Rosetta.TextForId("common.time");
        DstTxt.Text = Rosetta.TextForId("common.time.dst");
        TimeZoneTxt.Text = Rosetta.TextForId("common.timezone");
        LmtTxt.Text = Rosetta.TextForId("common.time.lmt");
        BtnCalculate.Content = Rosetta.TextForId("common.btncalc");
        BtnClose.Content = Rosetta.TextForId("common.btnclose");
        BtnHelp.Content = Rosetta.TextForId("common.btnhelp");
    }


    private void PopulateLists()
    {
        PopulateRatings();
        PopulateCategories();
        PopulateCalendars();
        PopulateDirections4GeoLat();
        PopulateDirections4GeoLong();
        PopulateYearCounts();
        PopulateTimeZones();
    }

    private void PopulateRatings()
    {
        comboRating.Items.Clear();
        foreach (var ratingDetail in _ratingDetails)
        {
            comboRating.Items.Add(Rosetta.TextForId(ratingDetail.TextId));
        }
        comboRating.SelectedIndex = 0;
    }

    private void PopulateCategories()
    {
        comboCategory.Items.Clear();
        foreach(var categoryDetail in _categoryDetails) 
        { 
            comboCategory.Items.Add(Rosetta.TextForId(categoryDetail.TextId));
        }
        comboCategory.SelectedIndex = 0;
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

    private void PopulateDirections4GeoLong()
    {
        comboLongDir.Items.Clear();
        comboLmtLongDir.Items.Clear();
        for (int i = 0; i < _directions4GeoLongDetails.Count; i++)
        {
            Directions4GeoLongDetails? direction4GeoLongDetail = _directions4GeoLongDetails[i];
            comboLongDir.Items.Add(Rosetta.TextForId(direction4GeoLongDetail.TextId));
            comboLmtLongDir.Items.Add(Rosetta.TextForId(direction4GeoLongDetail.TextId));
        }
        comboLongDir.SelectedIndex = 0;
        comboLmtLongDir.SelectedIndex = 0;
    }

    private void PopulateDirections4GeoLat()
    {
        comboLatDir.Items.Clear();
        foreach (var direction4GeoLatDetail in _directions4GeoLatDetails)
        {
            comboLatDir.Items.Add(Rosetta.TextForId(direction4GeoLatDetail.TextId));
        }
        comboLatDir.SelectedIndex = 0;
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

    private void PopulateTimeZones()
    {
        comboTimezone.Items.Clear();
        foreach (var timeZoneDetail in _timeZoneDetails)
        {
            comboTimezone.Items.Add(Rosetta.TextForId(timeZoneDetail.TextId));
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
        _controller.Source = SourceValue.Text;
        _controller.ChartCategory = _categoryDetails[comboCategory.SelectedIndex].Category;
        _controller.RoddenRating = _ratingDetails[comboRating.SelectedIndex].Rating;
        _controller.LocationName = LocationNameValue.Text;
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
        ChartDataInputController.ShowHelp();
    }

}




