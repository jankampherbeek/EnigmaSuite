// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;


namespace Enigma.Frontend.Ui.Charts.Progressive.InputTransits;

/// <summary>Interaction logic for input screen for transits.</summary>
public partial class ProgInputTransits : Window
{
    private readonly ProgInputTransitsController _controller;
    private List<TimeZoneDetails> _timeZoneDetails = TimeZones.Ut.AllDetails();

    public ProgInputTransits()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ProgInputTransitsController>();
    }

    public void Populate()
    {
        PopulateTexts();
        PopulateData();
    }

    private void PopulateTexts()
    {
        this.Title = Rosetta.TextForId("charts.prog.transitinput.title");
        TbFormTitle.Text = Rosetta.TextForId("charts.prog.transitinput.formtitle");
        TbExplanation.Text = Rosetta.TextForId("charts.prog.transitinput.explanation");
        BtnPeriod.Content = Rosetta.TextForId("charts.prog.transitinput.btnperiod");
        BtnEvent.Content = Rosetta.TextForId("charts.prog.transitinput.btnevent");
        BtnSearchEvent.Content = Rosetta.TextForId("charts.prog.transitinput.btnsearchevent");
        TbCelPoints.Text = Rosetta.TextForId("charts.prog.transitinput.celpoints");
        BtnCalculate.Content = Rosetta.TextForId("common.btncalc");
        BtnCancel.Content = Rosetta.TextForId("common.btncancel");
        BtnHelp.Content = Rosetta.TextForId("common.btnhelp");
    }

    public void PopulateData()
    {

    List<SelectableChartPointDetails> celPointDetails = _controller.GetChartPointDetails();
    LbCelPoints.ItemsSource = celPointDetails;

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
