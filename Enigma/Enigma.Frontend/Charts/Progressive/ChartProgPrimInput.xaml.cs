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

/// <summary>Interaction logic for input screen for primary directions.</summary>
public partial class ChartProgPrimInput : Window
{
    private readonly ChartProgPrimInputController _controller;
    public ChartProgPrimInput()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ChartProgPrimInputController>();


    }

    public void Populate()
    {
        PopulateTexts();
        PopulateData();
    }

    private void PopulateTexts()
    {
        this.Title = Rosetta.TextForId("charts.prog.priminput.title");
        FormTitle.Text = Rosetta.TextForId("charts.prog.priminput.formtitle");
        tbPrDirType.Text = Rosetta.TextForId("charts.prog.priminput.primdirtype");
        tbTimeKey.Text = Rosetta.TextForId("charts.prog.priminput.timekey");
        tbMoment1.Text = Rosetta.TextForId("charts.prog.priminput.moment1");
        tbMoment2.Text = Rosetta.TextForId("charts.prog.priminput.moment2");
        tbConverse.Text = Rosetta.TextForId("charts.prog.priminput.converse");
        tbPromissors.Text = Rosetta.TextForId("charts.prog.priminput.promissors");
        tbSignificators.Text = Rosetta.TextForId("charts.prog.priminput.significators");
        tbAspects.Text = Rosetta.TextForId("charts.prog.priminput.aspects");
        tbSelections.Text = Rosetta.TextForId("charts.prog.priminput.selections");
        tbOrbAspects.Text = Rosetta.TextForId("charts.prog.priminput.orbaspects");
        BtnCalculate.Content = Rosetta.TextForId("common.btncalc");
        BtnCancel.Content = Rosetta.TextForId("common.btncancel");
        BtnHelp.Content = Rosetta.TextForId("common.btnhelp");
    }

    public void PopulateData()
    {
        List<SelectableChartPointDetails> celPointDetails = _controller.GetChartPointDetails();
        lbPromissors.ItemsSource = celPointDetails;
        lbSignificators.ItemsSource = celPointDetails;
        List<SelectableAspectDetails> aspectDetails = _controller.GetAspectDetails();
        lbAspects.ItemsSource = aspectDetails;
    }


    public void PrDirTypeChanged(object sender, RoutedEventArgs e)
    {

    }

    public void TimeKeyChanged(object sender, RoutedEventArgs e)
    {

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
