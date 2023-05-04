// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;


namespace Enigma.Frontend.Ui.Charts.Progressive;

/// <summary>Interaction logic for input screen for transits.</summary>
public partial class ChartProgTransInput : Window
{
    private readonly ChartProgTransInputController _controller;

    public ChartProgTransInput()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ChartProgTransInputController>();
    }

    public void Populate()
    {
        PopulateTexts();
        PopulateData();
    }

    private void PopulateTexts()
    {
        this.Title = Rosetta.TextForId("charts.prog.transitinput.title");
        FormTitle.Text = Rosetta.TextForId("charts.prog.transitinput.formtitle");
        tbMoment.Text = Rosetta.TextForId("charts.prog.transitinput.moment");
        tbOrbAspects.Text = Rosetta.TextForId("charts.prog.transitinput.orbaspects");
        tbTransitPoints.Text = Rosetta.TextForId("charts.prog.transitinput.transitpoints");
        tbAspects.Text = Rosetta.TextForId("charts.prog.transitinput.aspects");
        BtnCalculate.Content = Rosetta.TextForId("common.btncalc");
        BtnCancel.Content = Rosetta.TextForId("common.btncancel");
        BtnHelp.Content = Rosetta.TextForId("common.btnhelp");
    }

    public void PopulateData()
    {
        List<SelectableChartPointDetails> celPointDetails = _controller.GetChartPointDetails();
        lbTransitPoints.ItemsSource = celPointDetails;
        List<SelectableAspectDetails> aspectDetails = _controller.GetAspectDetails();
        lbAspects.ItemsSource = aspectDetails;

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
