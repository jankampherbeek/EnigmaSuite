// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using System.Windows;


namespace Enigma.Frontend.Ui.Charts.Progressive.InputEvent;

/// <summary>
/// Interaction logic for ProgInputEvent.xaml
/// </summary>
public partial class ProgInputEvent : Window
{
    public ProgInputEvent()
    {
        InitializeComponent();
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
        tbCalendar.Text = Rosetta.TextForId("charts.prog.event.calendar");
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

    }


}

