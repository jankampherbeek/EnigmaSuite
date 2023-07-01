// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;


namespace Enigma.Frontend.Ui.Charts.Progressive;

/// <summary>Interaction logic for input screen for solar returns.</summary>
public partial class ChartProgSolarInput : Window
{
    private ChartProgInputSolarController _controller;

    public ChartProgSolarInput()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ChartProgInputSolarController>();
    }


    public void Populate()
    {
        PopulateTexts();
        PopulateData();
    }


    private void PopulateTexts()
    {
        this.Title = Rosetta.TextForId("charts.prog.solarinput.title");
        FormTitle.Text = Rosetta.TextForId("charts.prog.solarinput.formtitle");
        tbAge.Text = Rosetta.TextForId("charts.prog.solarinput.age");
        tbOrbAspects.Text = Rosetta.TextForId("charts.prog.solarinput.orbaspects");
        tbRelocation.Text = Rosetta.TextForId("charts.prog.solarinput.relocation");
        tbGeoLat.Text = Rosetta.TextForId("charts.prog.solarinput.geolat");
        tbGeoLong.Text = Rosetta.TextForId("charts.prog.solarinput.geolong");
        BtnCalculate.Content = Rosetta.TextForId("common.btncalc");
        BtnCancel.Content = Rosetta.TextForId("common.btncancel");
        BtnHelp.Content = Rosetta.TextForId("common.btnhelp");
    }

    private void PopulateData()
    {
        comboLongDir.Items.Clear();
        List<string> longItems = _controller.GetDirections4GeoLong();
        foreach(string longItem in longItems)
        {
            comboLongDir.Items.Add(longItem);
        }
        comboLongDir.SelectedIndex = 0;

        comboLatDir.Items.Clear();
        List<string> latItems = _controller.GetDirections4GeoLat();
        foreach (string latItem in latItems)
        {
            comboLatDir.Items.Add(latItem);
        }
        comboLatDir.SelectedIndex = 0;
    }

    private void CancelClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        //  ...Controller.ShowHelp();
    }

    private void CalculateClick(object sender, RoutedEventArgs e)
    {

    }

}
